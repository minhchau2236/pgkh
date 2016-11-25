using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using PSCPortal.Engine;
using System.Web.UI.WebControls;
using PSCPortal.Framework;
using PSCPortal.Framework.Helpler;

namespace PSCPortal.Systems
{
    public partial class Default : PSCPage
    {
        public string Username
        {
            get
            {
                return System.Web.HttpContext.Current.User.Identity.Name;
            }
        }
        protected void ShowMenu()
        {
            Guid subId = SessionHelper.GetSession(SessionKey.SubDomain) == string.Empty ? Guid.Empty : new Guid(SessionHelper.GetSession(SessionKey.SubDomain));
            string groupAdmin = System.Configuration.ConfigurationManager.AppSettings["GroupAdmin"];
            //if (System.Web.HttpContext.Current.User.IsInRole(groupAdmin))
            //{
            //    if (!(subId == Guid.Empty))
            //    {
            //        SubDomain subdomain = SubDomainCollection.GetSubDomainCollection().SingleOrDefault(sub => sub.Id == subId);
            //        if (subdomain == null)//homepage
            //        {
            //            Panel3.Visible = false;
            //        }
            //        else
            //        {
            //            Panel3.Visible = false;
            //        }
            //    }
            //}
            if (!System.Web.HttpContext.Current.User.IsInRole(groupAdmin))
            {
                Panel1.Visible = false;
                Panel2.Visible = false;
                Panel3.Visible = false;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!IsPostBack)
            {              
                ShowMenu();
            }           
        }
    }
}
