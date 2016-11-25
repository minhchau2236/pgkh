using System;
using System.Data;
using System.Data.SqlClient;
using PSCPortal.CMS;
using PSCPortal.Framework.Core;
using Telerik.Web.UI;

namespace PSCPortal.Portlets.TabMenu
{
    public partial class Display : Engine.PortletControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadTabControl();
        }

        private void LoadTabControl()
        {
            Guid menuParent;
            using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSCPortalConnectionString"].ConnectionString))
            {
                var com = new SqlCommand { Connection = con };
                con.Open();
                com.CommandType = CommandType.Text;
                com.Parameters.AddWithValue("@dataId", Portlet.PortletInstance.Id);
                com.CommandText = "Select MenuId from PortletMenu p INNER JOIN MenuMaster m ON p.MenuMasterId =  m.MenuMasterId Where DataId = @dataId";
                menuParent = com.ExecuteScalar() != null ? new Guid(com.ExecuteScalar().ToString()) : Guid.Empty;
            }
            if (menuParent == Guid.Empty)
                return;
            MenuCollection listMenu = MenuCollection.GetMenuChildCollection(menuParent);
            radTabMenu.Tabs.Clear();
            radMultiPageMenu.PageViews.Clear();
            BusinessObjectHierarchicalCollection childs = listMenu.Search(o => ((Menu)o).Id == menuParent).Childs;
            if (childs.Count > 0)
            {
                for (int i = 0; i < childs.Count; i++)
                {
                    var tab = new RadTab();
                    var sub = (Menu) childs[i];
                    if (sub == null)
                        return;
                    tab.Text = sub.Name;
                    radTabMenu.Tabs.Add(tab);
                    var pvTopic = new RadPageView {ID = "pvSub" + i};
                    radMultiPageMenu.PageViews.Add(pvTopic);
                    const string userControlName = "Portlets/TabMenu/SubPanelBar.ascx";
                    var userControl = (SubPanelBar) Page.LoadControl(userControlName);
                    pvTopic.Controls.Add(userControl);
                    userControl.Menu = sub;
                    userControl.LoadData();

                }
            }
        }
        protected override void DeleteData()
        {
           
        }
    }
}