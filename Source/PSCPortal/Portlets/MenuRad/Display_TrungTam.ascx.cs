using System;
using System.Data;
using System.Data.SqlClient;
using PSCPortal.CMS;

namespace PSCPortal.Portlets.MenuRad
{
    public partial class Display_TrungTam : Engine.PortletControl
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
                if(com.ExecuteScalar()!=null)
                    menuMasterId = new Guid(com.ExecuteScalar().ToString());
            }
            RadMenu.DataSource = MenuCollection.GetMenuCollection(new MenuMaster { Id = menuMasterId }).GetBindingSource();
            RadMenu.DataBind();
       
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