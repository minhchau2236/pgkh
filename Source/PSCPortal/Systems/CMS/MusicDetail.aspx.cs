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
    public partial class MusicDetail : PSCPortal.Framework.PSCDialog
    {
        private static MusicArgs Args
        {
            get
            {
                return DataShare as MusicArgs;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            LoadData();
        }

        protected void LoadData()
        {
            txtId.Text = Args.Music.Id.ToString();
            if (Args.IsEdit)
            {
                txtTitle.Text = Args.Music.Title;
                musicPreview.InnerHtml = "<audio id='myAudio' controls style='width: 100%; margin-top: 5px;'><source src='" + Args.Music.Path + "' type='audio/mpeg'>Trình duyệt của bạn không hổ trợ nghe nhạc.</audio>";
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

            Args.Music.Title = title;
            Args.Music.Path = "../../Resources/Music/HomePage/" + musicName;
            //Args.VideoClip.Path = "../../Resources/VideoClips/HomePage/" + videoName.Substring(5);
            if (!Args.IsEdit)
            {
                Args.Music.CreationDate = DateTime.Now;
                Args.Music.Priority = 1;
            }
        }

        public static string musicName, musicPath;

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            StatusLabel.Visible = false;
            if (FileUploadControl.HasFile)
            {
                try
                {
                    string fileType = FileUploadControl.PostedFile.ContentType;
                    if (fileType == "audio/mp3")
                    {
                        if (FileUploadControl.PostedFile.ContentLength < 1073741824)
                        {
                            string fileName = Args.Music.Id + Path.GetExtension(FileUploadControl.FileName);
                            //videoName = "Temp_" + fileName;
                            musicName = fileName;
                            musicPath = Server.MapPath("../../Resources/Music/HomePage/" + musicName); // tra ve duong dan tuyet doi
                            string filePath = Server.MapPath("../../Resources/Music/HomePage/") + musicName;
                            FileUploadControl.SaveAs(filePath);
                            musicPreview.InnerHtml = "<audio id='myAudio' controls style='width: 100%; margin-top: 5px;'><source src='../../Resources/Music/HomePage/" + musicName + "' type='audio/mpeg'>Trình duyệt của bạn không hổ trợ nghe nhạc.</audio>";
                            StatusLabel.Text = "Upload thành công!";
                        }
                        else
                            StatusLabel.Text = "Upload thất bại, chỉ upload file nhỏ hơn 1GB";
                    }
                    else
                        StatusLabel.Text = "Upload thất bại, chỉ upload file .mp3";
                }
                catch (Exception ex)
                {
                    StatusLabel.Text = "Upload thất bại, đã gặp lỗi : " + ex.Message;
                }
            }
            StatusLabel.Visible = true;
        }
    }
}