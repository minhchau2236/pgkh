using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace PSCPortal.Modules.VideoClip
{
    [Serializable]
    public class DirInfo
    {
        public DirInfo()
        { }
        public DirInfo(string name, string path)
        {
            Name = name;
            Path = path;
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

        public int NumberOfFile { get; set; }
    }

    public partial class Main : System.Web.UI.UserControl
    {
        protected string SubDomainName
        {
            get
            {
                return Libs.Ultility.GetSubDomain();
            }
        }

        public List<DirInfo> DirInfos
        {
            get
            {
                string dirRoot = SubDomainName != string.Empty
                    ? string.Format("~/Resources/VideoClips/SubDomain/{0}/", SubDomainName)
                    : "/Resources/VideoClips/SubDomain/HomePage";
                List<DirInfo> ListDirInfo = new List<DirInfo>();
                string[] videoDirs = Directory.GetDirectories(Server.MapPath(dirRoot));
                DirInfo dirinfo = null;
                foreach (var dir in videoDirs)
                {
                    DirectoryInfo info = new DirectoryInfo(dir);
                    dirinfo = new DirInfo();
                    dirinfo.Name = info.Name;
                    dirinfo.Path = info.FullName.Replace(Server.MapPath(dirRoot), "").Replace("\\", "||");
                    string[] videosPath = Directory.GetFiles(dir);
                    foreach (var filestring in videosPath)
                    {
                        FileInfo videoinfo = new FileInfo(filestring);
                        if (videoinfo.Extension.ToLower() == ".flv")
                            dirinfo.NumberOfFile++;
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


        protected void Page_Load(object sender, EventArgs e)
        {
            hplArrivalTypeName.Text = "Video Clip";
            TotalRecord = DirInfos.Count;
            RenderPager();
            if (!IsPostBack)
            {
                CurrentPage = 0;
                LoadData();
            }
        }

        protected void LoadData()
        {
            if (CurrentPage > TotalPage)
                CurrentPage = 0;

            List<DirInfo> temp = new List<DirInfo>();
            int length = (CurrentPage + 1) * RPP;
            if (length > DirInfos.Count)
                length = DirInfos.Count;
            for (int i = CurrentPage * RPP; i < length; i++)
                temp.Add(DirInfos[i]);
            rptDocumentsInCategory.DataSource = temp;
            rptDocumentsInCategory.DataBind();
        }

        //************* Phan Trang**************
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
        }

        private void RenderPager()
        {
            pnPager.Controls.Clear();
            LinkButton lbtFirst = new LinkButton();
            lbtFirst.CommandArgument = "0";
            lbtFirst.Text = "Trang đầu ";
            lbtFirst.ID = "lbtFirst";
            lbtFirst.CssClass = "paging";
            lbtFirst.Click += new EventHandler(Page_Changing);
            pnPager.Controls.Add(lbtFirst);

            LinkButton lbtPrev = new LinkButton();
            lbtPrev.Text = "<<--";
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
            lbtNext.CssClass = "paging";
            lbtNext.Text = "-->>";
            lbtNext.ID = "lbtNext";
            lbtNext.CommandArgument = CurrentPage + 1 + "";
            lbtNext.Click += new EventHandler(Page_Changing);
            pnPager.Controls.Add(lbtNext);

            LinkButton lbtLast = new LinkButton();
            lbtLast.CssClass = "paging";
            lbtLast.Text = " Trang cuối";
            lbtLast.ID = "lbtLast";
            lbtLast.CommandArgument = TotalPage - 1 + "";
            lbtLast.Click += new EventHandler(Page_Changing);
            pnPager.Controls.Add(lbtLast);
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
                return 10;
            }
        }

        private int TotalPage
        {
            get
            {
                return (int)((TotalRecord - 1) / RPP) + 1;
            }
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
    }
}