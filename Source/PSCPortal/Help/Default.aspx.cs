using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PSCPortal.Engine;
using PSCPortal.Framework.Helpler;

namespace PSCPortal.Systems.CMS
{
    public partial class Default : PSCPortal.Framework.PSCPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadCustomEditor();
        }
        protected void LoadCustomEditor()
        {
           // Libs.Ultility.SettingFileExplorer(RadFileExplorer1, "~/Resources/AlbumBookSlider/", "");
            //string subId = SessionHelper.GetSession(SessionKey.SubDomain);
            //if (subId == string.Empty)
            //    return;
            //int album_manage = 60;
            //string groupAdmin = System.Configuration.ConfigurationManager.AppSettings["GroupAdmin"];
            //if (System.Web.HttpContext.Current.User.IsInRole(groupAdmin))
            //{
            //    RadFileExplorer1.Visible = true;
            //}
            //else
            //{
            //    bool check = PSCPortal.Security.SystemAuthentication.CheckAllowFunction(PSCPortal.Security.Function.Parse(album_manage));
            //    if (check == true)
            //        RadFileExplorer1.Visible = true;
            //    else
            //        RadFileExplorer1.Visible = false;
            //}
            //if (!subId.Equals(Guid.Empty.ToString()))
            //{
            //    SubDomain subdomain = SubDomainCollection.GetSubDomainCollection().SingleOrDefault(sub => sub.Id == new Guid(subId));
            //    Libs.Ultility.SettingFileExplorer(RadFileExplorer1, "~/Resources/Albums/BookSlider/", subdomain.Name);
            //}
            //else
            //{
            //    Libs.Ultility.SettingFileExplorer(RadFileExplorer1, "~/Resources/Albums/", "");
            //}
        }
    }
}