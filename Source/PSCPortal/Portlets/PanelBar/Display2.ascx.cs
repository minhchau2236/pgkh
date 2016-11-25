using System;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using PSCPortal.Engine;
using System.Data;
using PSCPortal.CMS;

namespace PSCPortal.Portlets.PanelBar
{
    public partial class Display2 : PortletControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Guid menuMasterId = new Guid();
            using (
                SqlConnection con =
                    new SqlConnection(
                        System.Configuration.ConfigurationManager.ConnectionStrings["PSCPortalConnectionString"]
                            .ConnectionString))
            {
                SqlCommand com = new SqlCommand {Connection = con};
                con.Open();
                com.CommandType = CommandType.Text;
                com.Parameters.AddWithValue("@dataId", Portlet.PortletInstance.Id);
                com.CommandText = "Select MenuMasterId from PortletMenu Where DataId=@dataId";
                if (com.ExecuteScalar() != null)
                    menuMasterId = new Guid(com.ExecuteScalar().ToString());
            }
            rptMennu.DataSource =
                MenuCollection.GetMenuCollection(new MenuMaster {Id = menuMasterId}).GetBindingSource();
            rptMennu.DataBind();
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            CMS.Menu dv = (CMS.Menu) e.Item.DataItem;
            if (dv.Childs.Count != 0)
            {
                Repeater repeater2 = (Repeater) e.Item.FindControl("Repeater2");
                MenuCollection listMenuChild = MenuCollection.GetMenuChildCollection(dv.Id);

                repeater2.DataSource = listMenuChild.GetBindingSource();
                repeater2.DataBind();


            }
        }

        protected string Convert(string value)
        {
            string str = "";
            if (value.Length > 1)
                str = value.Substring(2);
            return str;
        }

        protected override void DeleteData()
        {
            using (
                SqlConnection con =
                    new SqlConnection(
                        System.Configuration.ConfigurationManager.ConnectionStrings["PSCPortalConnectionString"]
                            .ConnectionString))
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