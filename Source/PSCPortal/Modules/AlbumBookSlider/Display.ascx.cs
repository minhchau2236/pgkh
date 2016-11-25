using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using PSCPortal.CMS;

namespace PSCPortal.Modules.AlbumBookSlider
{
    [Serializable]
    public class DirInfo
    {
        public DirInfo()
        { }
        public DirInfo(string name, string path, string pathImgFirst)
        {
            Name = name;
            Path = path;
            PathImgFirst = pathImgFirst;
        }

        public string Name
        {
            get;
            set;
        }

        public DateTime CreateDate
        {
            get;
            set;
        }

        public string FileDownload
        {
            get;
            set;
        }
        public string Path
        {
            get;
            set;
        }

        public string PathImgFirst { get; set; }

        public int NumberOfImages { get; set; }
    }
    [Serializable]
    public class PhotoInfo
    {
        public string Name
        {
            get;
            set;
        }

        public string Path
        {
            get;
            set;
        }
    }
    public partial class Display : System.Web.UI.UserControl
    {
        public string FolderRoot
        {
            get
            {
                string dirRoot = "~/Resources/AlbumBookSlider";
                string path = Request.QueryString["Path"] == null ? dirRoot : dirRoot + "/" + Request.QueryString["Path"];
                return path;
            }
        }

        public string TopicId
        {
            get
            {
                return Request.QueryString["AlbumTopic"].ToString();
            }

        }
        public List<DirInfo> DirInfos
        {
            get
            {
                List<DirInfo> ListDirInfo = new List<DirInfo>();
                List<Article> albumDirs = ArticleCollection.GetArticleCollectionPublish(new Topic { Id = new Guid(TopicId) }).OrderByDescending(a => a.CreatedDate).ToList();
                if (String.IsNullOrEmpty(Request.QueryString["Path"]))
                {
                    foreach (var dir in albumDirs)
                    {
                        if (!String.IsNullOrEmpty(dir.AlbumPath))
                        {
                            DirectoryInfo info = new DirectoryInfo(dir.AlbumPath);
                            DirInfo dirinfo = new DirInfo();
                            dirinfo.Name = dir.Title;
                            dirinfo.CreateDate = dir.CreatedDate;
                            dirinfo.FileDownload = dir.DocumentPath;
                            dirinfo.Path = info.Name;
                            string[] photoAlbums = System.IO.Directory.GetFiles(Server.MapPath(FolderRoot + "/" + info.Name));
                            if (photoAlbums.Length > 0)
                            {
                                FileInfo photo = new FileInfo(photoAlbums[0]);
                                dirinfo.PathImgFirst = "/Resources/AlbumBookSlider/" + info.Name + "/" + photo.Name;
                            }
                            else
                            {
                                dirinfo.PathImgFirst = "/Resources/Albums/NoImage.jpg";
                            }
                            ListDirInfo.Add(dirinfo);
                        }
                    }
                }

                ViewState["DirInfos"] = ListDirInfo;

                return ViewState["DirInfos"] as List<DirInfo>;
            }
            set
            {
                ViewState["DirInfos"] = value;
            }
        }
        public List<PhotoInfo> PhotoInfos
        {
            get
            {
                List<PhotoInfo> ListPhoToInfo = new List<PhotoInfo>();
                var extension = new List<string> { ".jpg", ".jpg", ".png" };
                string[] photoAlbums = System.IO.Directory.GetFiles(Server.MapPath(FolderRoot)).Where(a => extension.Any(e => a.EndsWith(e))).ToArray();
                PhotoInfo photoinfo = null;
                foreach (var file in photoAlbums)
                {
                    FileInfo info = new FileInfo(file);
                    photoinfo = new PhotoInfo();
                    photoinfo.Name = info.Name;

                    string path = string.Empty;
                    if (Request.QueryString["Path"] != null)
                    {
                        path = info.FullName.Replace(Server.MapPath(FolderRoot), Request.QueryString["Path"]);
                    }
                    photoinfo.Path = FolderRoot.Replace("~", "") + "/" + info.Name;
                    ListPhoToInfo.Add(photoinfo);
                }
                ViewState["PhotoInfos"] = ListPhoToInfo;
                return ViewState["PhotoInfos"] as List<PhotoInfo>;
            }
            set
            {
                ViewState["PhotoInfos"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            string titleAlbum = string.Empty;
            string titleAllAlbum = string.Empty;
            if (HttpContext.Current.Request.QueryString["Path"] == null)
                Path = "~/Resources/AlbumBookSlider/";
            else
            {
                Path = HttpContext.Current.Request.QueryString["Path"].ToString();
                titleAlbum = FindName(Path);
            }
            titleAllAlbum += "<a href='#' class='StypeNews' onclick='chuyentrang(" + "\"" + "" + "\"" + ");'>" + "Bản tin</a>" + titleAlbum;
            hplArrivalTypeName.Text = titleAllAlbum;
            TotalRecord = PhotoInfos.Count;
            TotalRecordAlbum = DirInfos.Count;
            RenderPagerAlbum();
            if (!IsPostBack)
            {
                CurrentPage = 0;
                CurrentPageAlbum = 0;
                LoadData();
            }
            DataBind();
        }

        private string Path
        {
            get
            {
                return ViewState["Path"] as string;
            }
            set
            {
                ViewState["Path"] = value;
            }
        }


        string FindName(string path)
        {
            string titleAllAlbum = string.Empty;
            string titleAlbum = string.Empty;
            if (path == string.Empty)
                return titleAllAlbum;
            if (path.Contains("/"))
            {
                string[] arr = path.Split('/');
                string url = string.Empty;
                for (int i = 0; i < arr.Length; i++)
                {
                    titleAlbum = arr[i];
                    url += arr[i] + "/";
                    if (i == 0)
                        titleAllAlbum += "<span> » </span> " + "<a href='#' class='StypeNews' onclick='chuyentrang(" + "\"" + titleAlbum + "\"" + ");'>" + titleAlbum + "</a>";
                    else
                        titleAllAlbum += "<span> » </span> " + "<a href='#' class='StypeNews' onclick='chuyentrang(" + "\"" + url + "\"" + ");'>" + titleAlbum + "</a>";
                }
            }
            else
                titleAllAlbum += "<span> » </span> " + "<a href='#' class='StypeNews' onclick='chuyentrang(" + "\"" + path + "\"" + ");'>" + path + "</a>";
            return titleAllAlbum;
        }

        public void LoadData()
        {
            if (CurrentPage > TotalPage)
                CurrentPage = 0;
            if (CurrentPageAlbum > TotalPageAlbum)
                CurrentPageAlbum = 0;

            List<PhotoInfo> temp = new List<PhotoInfo>();
            for (int i = 0; i < PhotoInfos.Count; i++)
                temp.Add(PhotoInfos[i]);

            List<DirInfo> temp1 = new List<DirInfo>();
            int l = (CurrentPageAlbum + 1) * RPPAlbum;
            if (l > DirInfos.Count)
                l = DirInfos.Count;
            for (int i = CurrentPageAlbum * RPPAlbum; i < l; i++)
                temp1.Add(DirInfos[i]);

            if (temp1.Count == 0)
                Conten.Visible = false;
            else
                Conten.Visible = true;

            Pictype.DataSource = temp1;
            Pictype.DataBind();

            Pic.DataSource = temp;
            Pic.DataBind();
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            pnAlbum.Visible = TotalPageAlbum > 1;
        }
        private int CurrentPage
        {
            get
            {
                if (ViewState["CurrentPage"] == null)
                    ViewState["CurrentPage"] = 0;
                return (int)ViewState["CurrentPage"];
            }
            set
            {
                ViewState["CurrentPage"] = value;
            }
        }
        protected void Page_Changing(object sender, EventArgs e)
        {
            CurrentPage = int.Parse(((LinkButton)sender).CommandArgument);
            LoadData();
        }
        private int TotalRecord
        {
            get
            {

                return (int)ViewState["TotalRecord"];
            }
            set
            {
                ViewState["TotalRecord"] = value;
            }

        }
        private int RPP
        {
            get
            {
                return 6;
            }
        }
        private int TotalPage
        {
            get
            {
                return (int)((TotalRecord - 1) / RPP) + 1;
            }
        }
        ////////////////////////

        private void RenderPagerAlbum()
        {
            pnAlbum.Controls.Clear();
            LinkButton lbtFirst = new LinkButton();
            //lbtFirst.ForeColor = System.Drawing.Color.Yellow;
            //lbtFirst.Font.Name = "Verdana";
            lbtFirst.CommandArgument = "0";
            lbtFirst.Text = "Trang đầu ";
            lbtFirst.ID = "lbtFirst1";
            lbtFirst.CssClass = "paging";
            lbtFirst.Click += new EventHandler(Page_ChangingAlbum);
            pnAlbum.Controls.Add(lbtFirst);

            LinkButton lbtPrev = new LinkButton();
            //lbtPrev.ForeColor = System.Drawing.Color.Yellow;
            //lbtPrev.Font.Name = "Verdana";
            lbtPrev.Text = "&laquo;";
            lbtPrev.CommandArgument = CurrentPageAlbum - 1 + "";
            lbtPrev.ID = "lbtPrev1";
            lbtPrev.CssClass = "paging";
            lbtPrev.Click += new EventHandler(Page_ChangingAlbum);
            pnAlbum.Controls.Add(lbtPrev);


            int temp1 = CurrentPageAlbum - 2;
            if (temp1 < 0)
                temp1 = 0;
            int temp2 = CurrentPageAlbum + 3;
            if (temp1 == 0)
                temp2 = 5;

            if (temp2 > TotalPageAlbum)
                temp2 = TotalPageAlbum;

            for (int i = temp1; i < temp2; i++)
            {
                LinkButton lbtNumber = new LinkButton();
                //lbtNumber.ForeColor = System.Drawing.Color.Yellow;
                lbtNumber.CommandArgument = i + "";
                lbtNumber.CssClass = "paging";
                lbtNumber.Text = " " + (i + 1) + " ";
                lbtNumber.ID = "lbt1" + i;
                lbtNumber.Click += new EventHandler(Page_ChangingAlbum);
                pnAlbum.Controls.Add(lbtNumber);
                lbtNumber.Enabled = true;
            }

            LinkButton lbtNext = new LinkButton();
            //lbtNext.ForeColor = System.Drawing.Color.Yellow;
            //lbtNext.Font.Name = "Verdana";
            lbtNext.CssClass = "paging";
            lbtNext.Text = "&raquo;";
            lbtNext.ID = "lbtNext1";
            lbtNext.CommandArgument = CurrentPageAlbum + 1 + "";
            lbtNext.Click += new EventHandler(Page_ChangingAlbum);
            pnAlbum.Controls.Add(lbtNext);

            LinkButton lbtLast = new LinkButton();
            //lbtLast.ForeColor = System.Drawing.Color.Yellow;
            //lbtLast.Font.Name = "Verdana";
            lbtLast.CssClass = "paging";
            lbtLast.Text = " Trang cuối";
            lbtLast.ID = "lbtLast1";
            lbtLast.CommandArgument = TotalPageAlbum - 1 + "";
            lbtLast.Click += new EventHandler(Page_ChangingAlbum);
            pnAlbum.Controls.Add(lbtLast);
        }
        private int CurrentPageAlbum
        {
            get
            {
                if (ViewState["CurrentPageAlbum"] == null)
                    ViewState["CurrentPageAlbum"] = 0;
                return (int)ViewState["CurrentPageAlbum"];
            }
            set
            {
                ViewState["CurrentPageAlbum"] = value;
            }
        }
        protected void Page_ChangingAlbum(object sender, EventArgs e)
        {
            CurrentPageAlbum = int.Parse(((LinkButton)sender).CommandArgument);
            RenderPagerAlbum();
            LoadData();
        }
        private int TotalRecordAlbum
        {
            get
            {

                return (int)ViewState["TotalRecordAlbum"];
            }
            set
            {
                ViewState["TotalRecordAlbum"] = value;
            }

        }
        private int RPPAlbum
        {
            get
            {
                return 8;
            }
        }
        private int TotalPageAlbum
        {
            get
            {
                return (int)((TotalRecordAlbum - 1) / RPPAlbum) + 1;
            }
        }

    }
}