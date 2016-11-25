using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using PSCPortal.CMS;
using System.Collections.Generic;
using PSCPortal.Framework.Core;


namespace PSCPortal.Portlets.TabMenu
{
    public partial class MenuBottom : Engine.PortletControl
    {
        protected MenuCollection ListMenu
        {
            get
            {
                if (ViewState["ListMenu"] == null)
                {
                    Guid menuMasterId = new Guid();
                    using (
                        SqlConnection con =
                            new SqlConnection(
                                System.Configuration.ConfigurationManager.ConnectionStrings["PSCPortalConnectionString"]
                                    .ConnectionString))
                    {
                        SqlCommand com = new SqlCommand { Connection = con };
                        con.Open();
                        com.CommandType = CommandType.Text;
                        com.Parameters.AddWithValue("@dataId", Portlet.PortletInstance.Id);
                        com.CommandText = "Select MenuMasterId from PortletMenu Where DataId=@dataId";
                        if (com.ExecuteScalar() != null)
                            menuMasterId = new Guid(com.ExecuteScalar().ToString());
                    }
                    ViewState["ListMenu"] = MenuCollection.GetMenuCollection(new MenuMaster { Id = menuMasterId });
                }
                return ViewState["ListMenu"] as MenuCollection;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadData();
        }

        private void LoadData()
        {
            rptMenuList.DataSource = ListMenu.GetBindingSource();
            rptMenuList.DataBind();
        }
        protected void rptMenuList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            CMS.Menu menu = (CMS.Menu)e.Item.DataItem;
            if (menu != null)
            {
                Repeater rptSub = (Repeater)e.Item.FindControl("rptChild");
                rptSub.DataSource = menu.Childs;
                rptSub.DataBind();
            }
        }
        protected override void DeleteData()
        {
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSCPortalConnectionString"].ConnectionString))
            {
                SqlCommand com = new SqlCommand { Connection = con };
                con.Open();
                com.CommandType = CommandType.Text;
                com.Parameters.AddWithValue("@dataId", Portlet.PortletInstance.Id);
                com.CommandText = "Delete PortletMenu Where DataId=@dataId";
                com.ExecuteNonQuery();
            }
        }
    }
}