using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PSCPortal.Modules.VideoClip
{
    [Serializable]
    public class VideoInfo
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

    public partial class Detail : System.Web.UI.Page
    {
        public List<VideoInfo> VideoInfos
        {
            get
            {
                List<VideoInfo> ListVideoInfo = new List<VideoInfo>();
                try
                {
                    string[] videoVideos = System.IO.Directory.GetFiles(FolderRoot);
                    VideoInfo videoinfo = null;
                    foreach (var file in videoVideos)
                    {
                        FileInfo info = new FileInfo(file);
                        if (info.Extension.ToLower() == ".flv")
                        {
                            videoinfo = new VideoInfo();
                            videoinfo.Name = info.Name;
                            videoinfo.Path = Root + Request.QueryString["Path"] + "/" + info.Name;
                            ListVideoInfo.Add(videoinfo);
                        }
                    }
                }
                catch
                {
                }
                ViewState["VideoInfos"] = ListVideoInfo;
                return ViewState["VideoInfos"] as List<VideoInfo>;
            }
            set
            {
                ViewState["VideoInfos"] = value;
            }
        }
        public string SubDomainName
        {
            get
            {
                return Libs.Ultility.GetSubDomain();
            }
        }
        public string Root
        {
            get
            {
                return SubDomainName != string.Empty
                   ? string.Format("/Resources/VideoClips/SubDomain/{0}/", SubDomainName)
                   : "/Resources/VideoClips/HomePage/";
            }
        }
        public string PathFirst
        {
            get
            {
                if (ViewState["PathFirst"] == null)
                {
                    if (VideoInfos.Count > 0)
                    {
                        FileInfo info = new FileInfo(VideoInfos.First().Path);
                        ViewState["PathFirst"] = Root + Request.QueryString["Path"] + "/" + info.Name;
                    }
                }
                return (string)ViewState["PathFirst"];
            }
        }
        protected void LoadData()
        {
            if (VideoInfos.Count <= 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "clientscript", " $('#empty').show(); $('#pnVideo').hide();", true);
                return;
            }
            rptVideoClip.DataSource = VideoInfos;
            rptVideoClip.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadData();
            DataBind();
        }
        public string FolderRoot
        {
            get
            {
                string dirRoot = Root;
                string path = Request.QueryString["Path"] == null ? string.Empty : Request.QueryString["Path"];
                if (path != string.Empty)
                    path = Server.MapPath(dirRoot) + path.Replace("||", "\\");
                return path;
            }
        }
    }
}