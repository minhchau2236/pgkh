using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PSCPortal.Systems
{
    public partial class DialogMasterPage : System.Web.UI.MasterPage
    {
        public string Skin
        {
            get
            {
                return (string)Session["Skin"];
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                RadSkinManager1.Skin = Skin;
        }
    }
}
