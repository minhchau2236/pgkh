using System;
using PSCPortal.CMS;
using System.Configuration;
using PSCPortal.Engine;
namespace PSCPortal.Portlets.LinkWebsite
{
    public partial class Display : PortletControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadData();
            DataBind();
        }
        protected void LoadData()
        {
            string subName = Request.Url.Host.Replace(ConfigurationManager.AppSettings["DomainName"], "");
            if (subName.Length > 0)
                subName = subName.Substring(0, subName.Length - 1);
            SubDomain subDomain = subName == string.Empty ? SubDomain.GetSubByName("HomePage") : SubDomain.GetSubByName(subName);
            ListLinkWebsite.DataSource = subDomain == null ? LinkWebsiteCollection.GetLinkWebsiteCollection() : LinkWebsiteCollection.GetLinkWebsiteCollectionBySubDomain(subDomain.Id);
            
            ListLinkWebsite.DataBind();
        }
        protected override void DeleteData()
        {
        }
    }
}