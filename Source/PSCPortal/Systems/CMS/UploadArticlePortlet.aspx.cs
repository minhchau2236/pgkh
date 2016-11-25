using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PSCPortal.Framework;
using PSCPortal.CMS;


namespace PSCPortal.Systems.CMS
{
    public partial class UploadArticlePortlet : PSCSubDialog
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private static ArticleArgs Args
        {
            get
            {
                return DataShare as ArticleArgs;
            }           
        }

        protected void btnUploadPic_Click(object sender, EventArgs e)
        {
            string extention = System.IO.Path.GetExtension(FileUpLoadPic.FileName);
            string s = HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["ArticleImage"] + Guid.NewGuid().ToString() + extention);
            Args.Article.LinkImgPortlet = s;
            Session["LinkImgPortlet"] = Args.Article.LinkImgPortlet;
            FileUpLoadPic.SaveAs(Args.Article.LinkImgPortlet);

        }
    }
}