using System;
using System.Data.SqlClient;
using PSCPortal.Engine;
using System.Data;
using PSCPortal.CMS;

namespace PSCPortal.Portlets.PanelBar
{
    public partial class Display : PortletControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {

        }
        private void LoadData()
        {

            Guid menuMasterId = new Guid();
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSCPortalConnectionString"].ConnectionString))
            {
                SqlCommand com = new SqlCommand {Connection = con};
                con.Open();
                com.CommandType = CommandType.Text;
                com.Parameters.AddWithValue("@dataId", Portlet.PortletInstance.Id);
                com.CommandText = "Select MenuMasterId from PortletMenu Where DataId=@dataId";
                if (com.ExecuteScalar() != null)
                    menuMasterId = new Guid(com.ExecuteScalar().ToString());
            }
            menu.DataSource = MenuCollection.GetMenuCollection(new MenuMaster { Id = menuMasterId }).GetBindingSource();
            menu.DataTextField = "Name";
            menu.DataBind();
           
        }
        protected override void DeleteData()
        {
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSCPortalConnectionString"].ConnectionString))
            {
                SqlCommand com = new SqlCommand {Connection = con};
                con.Open();
                com.CommandType = CommandType.Text;
                com.Parameters.AddWithValue("@dataId", Portlet.PortletInstance.Id);
                com.CommandText = "Delete PortletMenu Where DataId=@dataId";
                com.ExecuteNonQuery();
            }
        }
    }
}