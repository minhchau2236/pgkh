using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PSCPortal.Framework;
using PSCPortal.Portlets.Rotator.Libs;

namespace PSCPortal.Portlets.Rotator
{
    public partial class Detail : PSCDialog
    {
        private static ImagePortletArgs Args
        {
            get
            {
                return HttpContext.Current.Session["ImagePortletArgs"] as ImagePortletArgs;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            LoadData();
        }
        protected void LoadData()
        {
            PSCSubDialog.DataShare = Args;
            txtId.Text = Args.ImagePortlet.Id.ToString();
            txtTitle.Text = Args.ImagePortlet.Title;
            txtLink.Text = Args.ImagePortlet.Link;
            txtOrder.Text = Args.ImagePortlet.Order + "";

        }
        [System.Web.Services.WebMethod]
        public static void Save(string title, string link, int order)
        {
            Args.ImagePortlet.Title = title.Replace("\"", "'");
            Args.ImagePortlet.Link = link;
            Args.ImagePortlet.Order = order;
            if (HttpContext.Current.Session["ImageUrl"] != null)
                Args.ImagePortlet.Url = (string)HttpContext.Current.Session["ImageUrl"];
            HttpContext.Current.Session["ImageUrl"] = null;
        }
    }
}