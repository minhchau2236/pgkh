using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PSCPortal.Security;

namespace PSCPortal.Systems.Security
{
    public partial class ResetPassword : PSCPortal  .Framework.PSCDialog
    {
        private static UserArgs Args
        {
            get
            {
                return DataShare as UserArgs;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DataBind();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
        }

        [System.Web.Services.WebMethod]
        public static bool Save(string newPass)
        {
            CustomMembershipProvider customMembership = new CustomMembershipProvider();
            if (customMembership.ChangePass(Args.User, newPass))
                return true;
            return false;
        }
    }
}
