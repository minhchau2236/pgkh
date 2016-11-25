using System;
using Telerik.Web;

namespace PSCPortal.Portlets.TabMenu
{
    public partial class SubPanelBar : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadData();
            DataBind();
        }

        public CMS.Menu Menu
        {
            get
            {
                return ViewState["Menu"] as CMS.Menu;
            }
            set
            {
                ViewState["Menu"] = value;
            }
        }
        public void LoadData()
        {
            radSubMenu.DataSource = Menu.GetChildren();
            radSubMenu.DataBind();
        }
    }
}