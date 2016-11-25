using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PSCPortal.Framework;
using PSCPortal.CMS;
using System.IO;


namespace PSCPortal.Systems.CMS
{
    public partial class UploadArticleImage : PSCSubDialog
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            divImg.InnerHtml = "<img src='/Services/GetArticleImage.ashx?Id=" + Args.Article.Id + "' />";
            DataBind();
        }

        public static ArticleArgs Args
        {
            get
            {
                return DataShare as ArticleArgs;
            }
        }

        protected void btnUploadPic_Click(object sender, EventArgs e)
        {
            string extention = System.IO.Path.GetExtension(FileUpLoadPic.FileName);

            DirectoryInfo dInfoAll = new DirectoryInfo(HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["ArticleImage"]));
            if (!dInfoAll.Exists)
                return;
            foreach (var item in dInfoAll.GetFiles())
            {
                string idFile = item.Name.Substring(0, item.Name.Length - item.Extension.Length);
                if (idFile == Args.Article.Id.ToString())
                    item.Delete();
            }
            if (extention.ToLower() != ".jpg" && extention.ToLower() != ".png" && extention.ToLower() != ".gif" && extention.ToLower() != ".jpeg")
                return;
            string s = HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["ArticleImage"] + Args.Article.Id.ToString() + extention);
            Args.Article.Link = s;
            Session["LinkImage"] = Args.Article.Link;
            FileUpLoadPic.SaveAs(Args.Article.Link);
            divImg.InnerHtml = "<img src='/Temp/ArticleImage/" + Args.Article.Id + extention + "' />";
        }

        [System.Web.Services.WebMethod]
        public static void DeleteImg()
        {
            DirectoryInfo dInfoAll = new DirectoryInfo(HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["ArticleImage"]));
            if (!dInfoAll.Exists)
                return;
            foreach (var item in dInfoAll.GetFiles())
            {
                string idFile = item.Name.Substring(0, item.Name.Length - item.Extension.Length);
                if (idFile == Args.Article.Id.ToString())
                    item.Delete();
            }
            Args.Article.DeleteImage();
        }
    }
}