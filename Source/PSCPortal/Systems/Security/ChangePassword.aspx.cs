using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PSCPortal.Security;
using System.Security;

namespace PSCPortal.Systems.Security
{
    public partial class ChangePassword : PSCPortal.Framework.PSCDialog
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataBind();    
        }

        [System.Web.Services.WebMethod]
        public static bool Save(string oldPass,string newPass)
        {
            string userName = System.Web.HttpContext.Current.User.Identity.Name;
            CustomMembershipProvider customMembership = new CustomMembershipProvider();
            if (customMembership.ChangePassword(userName, oldPass, newPass))
            {
                System.Web.Security.FormsAuthentication.SignOut();
                return true;
            }
            return false;
        }
    }
}
