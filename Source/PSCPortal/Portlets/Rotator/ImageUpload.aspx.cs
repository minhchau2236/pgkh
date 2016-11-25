using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PSCPortal.CMS;
using PSCPortal.Framework;
using PSCPortal.Portlets.Rotator.Libs;

namespace PSCPortal.Portlets.Rotator
{
    public partial class ImageUpload : PSCSubDialog
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private static ImagePortletArgs Args
        {
            get
            {
                return DataShare as ImagePortletArgs;
            }
        }

        protected void btnUploadPic_Click(object sender, EventArgs e)
        {
            string pathFolder = HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["ImagePortlet"] + "/" + Args.ImagePortlet.DataId);
            if (!System.IO.Directory.Exists(pathFolder))
                System.IO.Directory.CreateDirectory(pathFolder);
            Args.ImagePortlet.Link = pathFolder + "/" + System.IO.Path.GetFileName(FileUpLoadPic.FileName);
            FileUpLoadPic.SaveAs(Args.ImagePortlet.Link);
            Session["ImageUrl"] = System.Configuration.ConfigurationManager.AppSettings["ImagePortlet"].Replace("~/","") + "/" + Args.ImagePortlet.DataId + "/" + System.IO.Path.GetFileName(FileUpLoadPic.FileName);
        }
    }
}