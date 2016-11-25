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

namespace PSCPortal.Systems.CMS
{
    public partial class LinkWebsiteDetail : PSCPortal.Framework.PSCDialog
    {
        private static LinkWebsiteArgs Args
        {
            get
            {
                return DataShare as LinkWebsiteArgs;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            LoadData();
        }
        protected void LoadData()
        {
            txtId.Text = Args.LinkWebsite.Id.ToString();

            if (Args.IsEdit)
            {
                txtName.Text = Args.LinkWebsite.Name;
                txtLink.Text = Args.LinkWebsite.Link;
            }
        }
        [System.Web.Services.WebMethod]
        public static void Save(string name, string link)
        {
            Args.LinkWebsite.Name = name;
            Args.LinkWebsite.Link = link;
        }
    }
}