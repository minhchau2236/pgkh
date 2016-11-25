using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PSCPortal.Framework;
using PSCPortal.Engine;

namespace PSCPortal.Systems.Engine
{
    public partial class PortletDetail : PSCDialog
    {
        private static PortletArgs Args
        {
            get
            {
                return DataShare as PortletArgs;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DataBind();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            LoadData();
        }
        protected void LoadData()
        {
            txtId.Text = Args.Portlet.Id.ToString();
            txtName.Text = Args.Portlet.Name;
            txtDisplayURL.Text = Args.Portlet.DisplayURL;
            txtEditURL.Text = Args.Portlet.EditURL;
        }
        [System.Web.Services.WebMethod]
        public static bool Save(string name, string displayURL, string editURL)
        {
            //17122015
            bool result = false;
            if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(displayURL)) && System.IO.File.Exists(HttpContext.Current.Server.MapPath(editURL)))
            {
                Args.Portlet.Name = name;
                Args.Portlet.DisplayURL = displayURL;
                Args.Portlet.EditURL = editURL;
                result = true; 
            }
            return result;
        }
    }
}
