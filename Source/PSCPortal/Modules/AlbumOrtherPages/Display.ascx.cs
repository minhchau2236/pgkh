﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace PSCPortal.Modules.AlbumOrtherPages
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
                string dirRoot = "~/Resources/Albums/HomePage";
                string path = Request.QueryString["Path"] == null ? dirRoot : dirRoot + "/" + Request.QueryString["Path"];
                return path;
            }
        }
        public List<DirInfo> DirInfos
        {
            get
            {
                List<DirInfo> ListDirInfo = new List<DirInfo>();
                string[] albumDirs = Directory.GetDirectories(Server.MapPath(FolderRoot));
                DirInfo dirinfo = null;
                foreach (var dir in albumDirs)
                {
                    DirectoryInfo info = new DirectoryInfo(dir);
                    dirinfo = new DirInfo();
                    dirinfo.Name = info.Name;                    
                    string path = string.Empty;
                    if (Request.QueryString["Path"] != null)
                    {
                        path = info.FullName.Replace(Server.MapPath(FolderRoot), Request.QueryString["Path"]);
                    }

                    dirinfo.Path = path == string.Empty ? dirinfo.Name : path.Replace("\\", "/");
                    string[] photoAlbums = System.IO.Directory.GetFiles(Server.MapPath(FolderRoot + "/" + dirinfo.Name));
                    if (photoAlbums.Length > 0)
                    {
                        FileInfo photo = new FileInfo(photoAlbums[0]);
                        string pathImgFirst = string.Empty;
                        pathImgFirst = path == string.Empty ? "/Resources/Albums/HomePage/" + dirinfo.Name : "/Resources/Albums/HomePage/" + path.Replace("\\", "/");
                        pathImgFirst += "/" + photo.Name;
                        dirinfo.PathImgFirst = pathImgFirst;
                        dirinfo.NumberOfImages = photoAlbums.Count();
                    }
                    else
                    {
                        dirinfo.PathImgFirst = "/Resources/Albums/NoImage.jpg";
                        dirinfo.NumberOfImages = 0;
                    }

                    ListDirInfo.Add(dirinfo);
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
                string[] photoAlbums = System.IO.Directory.GetFiles(Server.MapPath(FolderRoot));
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
                    
                    photoinfo.Path = FolderRoot.Replace("~","") +"/"+info.Name;
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
            string titleAlbum=string.Empty;            
            string titleAllAlbum = string.Empty;
            if (HttpContext.Current.Request.QueryString["Path"] == null)
                Path = "~/Resources/Albums/HomePage/";
            else
            {
                Path = HttpContext.Current.Request.QueryString["Path"].ToString();
                titleAlbum= FindName(Path);
            }
            titleAllAlbum +="<a href='#' class='StypeNews' onclick='chuyentrang(" + "\"" + "" + "\"" + ");'>" + "Album </a>" + titleAlbum;
            hplArrivalTypeName.Text = titleAllAlbum;
            TotalRecord = PhotoInfos.Count;
            TotalRecordAlbum = DirInfos.Count;
            RenderPager();
            RenderPagerAlbum();
            if (!IsPostBack)
            {
                CurrentPage = 0;
                CurrentPageAlbum = 0;
                LoadData();
            }
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

            if (path.Contains("/"))
            {
                string[] arr = path.Split('/');
                string url = string.Empty;
                for (int i=0;i<arr.Length;i++)
                {
                    titleAlbum = arr[i];
                    url += arr[i] + "/";
                    if(i==0)                    
                        titleAllAlbum += "<span> » </span> " + "<a href='#' class='StypeNews' onclick='chuyentrang(" + "\"" + titleAlbum + "\"" + ");'>" + titleAlbum + "</a>";
                    else
                        titleAllAlbum +="<span> » </span> " + "<a href='#' class='StypeNews' onclick='chuyentrang(" + "\"" + url + "\"" + ");'>" + titleAlbum + "</a>";
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
            int length = (CurrentPage + 1) * RPP;
            if (length > PhotoInfos.Count)
                length = PhotoInfos.Count;
            for (int i = CurrentPage * RPP; i < length; i++)
                temp.Add(PhotoInfos[i]);

            List<DirInfo> temp1 = new List<DirInfo>();
            int l = (CurrentPageAlbum + 1) * RPPAlbum;
            if (l > DirInfos.Count)
                l = DirInfos.Count;
            for (int i = CurrentPageAlbum * RPPAlbum; i < l; i++)
                temp1.Add(DirInfos[i]);

            //ilist = ilist.Take(CurrentPage * RPP + RPP > TotalRecord ? TotalRecord % RPP : RPP);

            if (temp1.Count == 0)
            {
                Conten.Visible = false;
            }
            else
                Conten.Visible = true;
            Pictype.DataSource = temp1;
            Pictype.DataBind();

            Pic.DataSource = temp;
            Pic.DataBind();
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            LinkButton lbtFirst = (LinkButton)pnPager.FindControl("lbtFirst");
            LinkButton lbtPrev = (LinkButton)pnPager.FindControl("lbtPrev");
            if (CurrentPage == 0)
            {
                lbtFirst.Enabled = false;
                lbtPrev.Enabled = false;
            }
            else
            {
                lbtFirst.Enabled = true;
                lbtPrev.Enabled = true;
            }

            LinkButton lbtNext = (LinkButton)pnPager.FindControl("lbtNext");
            LinkButton lbtLast = (LinkButton)pnPager.FindControl("lbtLast");
            if (CurrentPage == TotalPage - 1)
            {
                lbtLast.Enabled = false;
                lbtNext.Enabled = false;
            }
            else
            {
                lbtLast.Enabled = true;
                lbtNext.Enabled = true;
            }

            LinkButton lbtNumber = (LinkButton)pnPager.FindControl("lbt" + CurrentPage);
            lbtNumber.ForeColor = System.Drawing.Color.Red;
            lbtNumber.Enabled = false;

            pnPager.Visible = TotalPage > 1;

            ////// Pageing Album
            LinkButton lbtFirst1 = (LinkButton)pnAlbum.FindControl("lbtFirst1");
            LinkButton lbtPrev1 = (LinkButton)pnAlbum.FindControl("lbtPrev1");
            if (CurrentPageAlbum == 0)
            {
                lbtFirst1.Enabled = false;
                lbtPrev1.Enabled = false;
            }
            else
            {
                lbtFirst1.Enabled = true;
                lbtPrev1.Enabled = true;
            }

            LinkButton lbtNext1 = (LinkButton)pnAlbum.FindControl("lbtNext1");
            LinkButton lbtLast1 = (LinkButton)pnAlbum.FindControl("lbtLast1");
            if (CurrentPageAlbum == TotalPageAlbum - 1)
            {
                lbtLast1.Enabled = false;
                lbtNext1.Enabled = false;
            }
            else
            {
                lbtLast1.Enabled = true;
                lbtNext1.Enabled = true;
            }

            LinkButton lbtNumber1 = (LinkButton)pnAlbum.FindControl("lbt1" + CurrentPageAlbum);
            lbtNumber1.ForeColor = System.Drawing.Color.Red;
            lbtNumber1.Enabled = false;

            pnAlbum.Visible = TotalPageAlbum > 1;
        }
        private void RenderPager()
        {
            pnPager.Controls.Clear();
            LinkButton lbtFirst = new LinkButton();
            //lbtFirst.ForeColor = System.Drawing.Color.Yellow;
            //lbtFirst.Font.Name = "Verdana";
            lbtFirst.CommandArgument = "0";
            lbtFirst.Text = "Trang đầu ";
            lbtFirst.ID = "lbtFirst";
            lbtFirst.CssClass = "paging";
            lbtFirst.Click += new EventHandler(Page_Changing);
            pnPager.Controls.Add(lbtFirst);

            LinkButton lbtPrev = new LinkButton();
            //lbtPrev.ForeColor = System.Drawing.Color.Yellow;
            //lbtPrev.Font.Name = "Verdana";
            lbtPrev.Text = "&laquo;";
            lbtPrev.CommandArgument = CurrentPage - 1 + "";
            lbtPrev.ID = "lbtPrev";
            lbtPrev.CssClass = "paging";
            lbtPrev.Click += new EventHandler(Page_Changing);
            pnPager.Controls.Add(lbtPrev);


            int temp1 = CurrentPage - 2;
            if (temp1 < 0)
                temp1 = 0;
            int temp2 = CurrentPage + 3;
            if (temp1 == 0)
                temp2 = 5;

            if (temp2 > TotalPage)
                temp2 = TotalPage;

            for (int i = temp1; i < temp2; i++)
            {
                LinkButton lbtNumber = new LinkButton();
                //lbtNumber.ForeColor = System.Drawing.Color.Yellow;
                lbtNumber.CommandArgument = i + "";
                lbtNumber.CssClass = "paging";
                lbtNumber.Text = " " + (i + 1) + " ";
                lbtNumber.ID = "lbt" + i;
                lbtNumber.Click += new EventHandler(Page_Changing);
                pnPager.Controls.Add(lbtNumber);
                lbtNumber.Enabled = true;
            }

            LinkButton lbtNext = new LinkButton();
            //lbtNext.ForeColor = System.Drawing.Color.Yellow;
            //lbtNext.Font.Name = "Verdana";
            lbtNext.CssClass = "paging";
            lbtNext.Text = "&raquo;";
            lbtNext.ID = "lbtNext";
            lbtNext.CommandArgument = CurrentPage + 1 + "";
            lbtNext.Click += new EventHandler(Page_Changing);
            pnPager.Controls.Add(lbtNext);

            LinkButton lbtLast = new LinkButton();
            //lbtLast.ForeColor = System.Drawing.Color.Yellow;
            //lbtLast.Font.Name = "Verdana";
            lbtLast.CssClass = "paging";
            lbtLast.Text = " Trang cuối";
            lbtLast.ID = "lbtLast";
            lbtLast.CommandArgument = TotalPage - 1 + "";
            lbtLast.Click += new EventHandler(Page_Changing);
            pnPager.Controls.Add(lbtLast);
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
            RenderPager();
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
                return 6;
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