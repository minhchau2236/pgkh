using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using PSCPortal.CMS;
using System.Web.Services;
using System.IO;

namespace PSCPortal.Systems.CMS
{
    public partial class VideoClipDetail : PSCPortal.Framework.PSCDialog
    {
        private static VideoClipArgs Args
        {
            get
            {
                return DataShare as VideoClipArgs;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            LoadData();
        }

        protected void LoadData()
        {
            rdLink.Checked = true;
            txtId.Text = Args.VideoClip.Id.ToString();
            if (Args.IsEdit)
            {
                txtTitle.Text = Args.VideoClip.Title;
                if (string.IsNullOrWhiteSpace(Args.VideoClip.FileExtension))
                {
                    txtLink.Text = Args.VideoClip.Path;
                    rdLink.Checked = true;
                    trLink.Style["display"] = "table-row";
                    trFile.Style["display"] = "none";
                }
                else
                {
                    rdFile.Checked = true;
                    trFile.Style["display"] = "table-row";
                    trLink.Style["display"] = "none";
                }
                int idx = 0;
                for (int i = Args.VideoClip.Path.Length - 1; i >= 0; i--)
                    if (Args.VideoClip.Path[i] == '.')
                    {
                        idx = i + 1;
                        break;
                    }
                string fileEx = Args.VideoClip.Path.Substring(idx).ToLower();
                if (fileEx == "flv")
                    fileEx = "x-flv";
                fileEx = "video/" + fileEx;
                videoPreview.InnerHtml = "<video id='myVideo' class='video-js vjs-default-skin' width='450' height='300' controls style='background-color: black;' data-setup='{}'><source src='" + Args.VideoClip.Path + "' type='" + fileEx + "'>Trình duyệt của bạn không hổ trợ xem video.</video>";
            }
        }

        [WebMethod]
        public static void Cancel()
        {

        }

        [WebMethod]
        public static void Save(string title)
        {
            //DirectoryInfo dir = new DirectoryInfo(videoPath);

            Args.VideoClip.Title = title;
            //Args.VideoClip.Path = "../../Resources/VideoClips/HomePage/" + videoName.Substring(5);
            if (!Args.IsEdit)
            {
                Args.VideoClip.CreationDate = DateTime.Now;
                Args.VideoClip.Priority = 1;
            }
        }

        public static string videoName, videoPath;

        protected void UploadLink_Click(object sender, EventArgs e)
        {
            StatusLabel.Visible = false;
            if (txtLink.Text.Trim() != "")
            {
                videoPath = txtLink.Text;
                Args.VideoClip.Path = videoPath;
                int idx = 0;
                for (int i = videoPath.Length - 1; i >= 0; i--)
                    if (videoPath[i] == '.')
                    {
                        idx = i + 1;
                        break;
                    }
                string fileEx = videoPath.Substring(idx).ToLower();
                if (fileEx == "flv")
                    fileEx = "x-flv";
                fileEx = "video/" + fileEx;
                Args.VideoClip.FileExtension = "";
                //videoPreview.InnerHtml = "<video id='myVideo' class='video-js vjs-default-skin' width='450' height='300' controls style='background-color: black;' data-setup='{" + '"'.ToString() + "autoplay" + '"'.ToString() + " : true}'><source src='" + videoPath + "' type='" + fileEx + "'>Trình duyệt của bạn không hổ trợ xem video.</video>";
                videoPreview.InnerHtml = "<video id='myVideo' class='video-js vjs-default-skin' width='450' height='300' controls style='background-color: black;' data-setup='{}'><source src='" + videoPath + "' type='" + fileEx + "'>Trình duyệt của bạn không hổ trợ xem video.</video>";
                StatusLabel.Text = "Upload thành công khi khung video hiện thị được nội dung bạn mong muốn!";
            }
            else
                StatusLabel.Text = "Upload thất bại, đường dẫn ko hợp lệ";
            StatusLabel.Visible = true;
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            StatusLabel.Visible = false;
            if (FileUploadControl.HasFile)
            {
                try
                {
                    string fileType = Path.GetExtension(FileUploadControl.FileName);
                    if (fileType == ".mpg" || fileType == ".mp4" || fileType == ".avi" || fileType == ".flv")
                    {
                        if (FileUploadControl.PostedFile.ContentLength < 1073741824)
                        {
                            string fileName = Args.VideoClip.Id + Path.GetExtension(FileUploadControl.FileName);
                            string fileEx = Path.GetExtension(FileUploadControl.FileName).Substring(1);
                            if (fileEx == "flv")
                                fileEx = "x-flv";
                            fileEx = "video/" + fileEx;
                            //videoName = "Temp_" + fileName;
                            videoName = fileName;
                            videoPath = Server.MapPath("../../Resources/VideoClips/HomePage/" + videoName); // tra ve duong dan tuyet doi                            
                            string filePath = Server.MapPath("../../Resources/VideoClips/HomePage/") + videoName;
                            FileUploadControl.SaveAs(filePath);
                            //videoPreview.InnerHtml = "<video id='myVideo' width='250' height='180' controls style='background-color: black;'><source src='../../Resources/VideoClips/HomePage/" + videoName + "' type='video/mp4'>Trình duyệt của bạn không hổ trợ xem video.</video>";
                            videoPreview.InnerHtml = "<video id='myVideo' class='video-js vjs-default-skin' width='450' height='300' controls style='background-color: black;' data-setup='{}'><source src='../../Resources/VideoClips/HomePage/" + videoName + "' type='" + fileEx + "'>Trình duyệt của bạn không hổ trợ xem video.</video>";
                            StatusLabel.Text = "Upload thành công!";
                            Args.VideoClip.Path = "../../Resources/VideoClips/HomePage/" + videoName;
                            Args.VideoClip.FileExtension = Path.GetExtension(FileUploadControl.FileName);
                        }
                        else
                            StatusLabel.Text = "Upload thất bại, chỉ upload file nhỏ hơn 1GB";
                    }
                    else
                        StatusLabel.Text = "Upload thất bại, chỉ upload file .mpg/mp4/avi";
                }
                catch (Exception ex)
                {
                    StatusLabel.Text = "Upload thất bại, đã gặp lỗi : " + ex.Message;
                }
            }
            else
            {
                StatusLabel.Text = "Upload thất bại, bạn chưa nhập file";
            }
            StatusLabel.Visible = true;
        }
    }
}