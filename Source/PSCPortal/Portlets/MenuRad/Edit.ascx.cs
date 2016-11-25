using PSCPortal.Engine;
using PSCPortal.Framework.Helpler;
using System;
using System.Data.SqlClient;

namespace PSCPortal.Portlets.MenuRad
{
    public partial class Edit : Engine.PortletEditControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            Guid subId = SessionHelper.GetSession(SessionKey.SubDomain) == string.Empty ? Guid.Empty : new Guid(SessionHelper.GetSession(SessionKey.SubDomain));
            SubDomain subDomain = new SubDomain { Id = subId };
            CMS.MenuMasterCollection listMenuMaster = subId == Guid.Empty ? CMS.MenuMasterCollection.GetMenuMasterCollection() : subDomain.GetMenuMastersBelongTo();
            ddlListMenuMaster.DataSource = listMenuMaster;
            ddlListMenuMaster.DataTextField = "Name";
            ddlListMenuMaster.DataValueField = "Id";
            ddlListMenuMaster.DataBind();

            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSCPortalConnectionString"].ConnectionString))
            {
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                con.Open();
                com.Parameters.AddWithValue("@DataId", DataId);
                com.CommandText = "Select MenuMasterId from PortletMenu where DataId=@DataId";

                Object menuMaster = com.ExecuteScalar();
                if (menuMaster != null)
                {
                    Guid menuMasterId = (Guid)menuMaster;
                    for (int i = 0; i < listMenuMaster.Count; i++)
                    {
                        if (listMenuMaster[i].Id == menuMasterId)
                        {
                            ddlListMenuMaster.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSCPortalConnectionString"].ConnectionString))
            {
                con.Open();
                SqlCommand comDataIdExist = new SqlCommand { Connection = con };
                comDataIdExist.Parameters.AddWithValue("@DataId", DataId);
                comDataIdExist.CommandText = "Select MenuMasterId from PortletMenu where DataId=@DataId";

                Object menuMasterGuid = comDataIdExist.ExecuteScalar();
                if (menuMasterGuid == null)
                {
                    //neu khong ton tai record nao chua dataId, thi ta them moi vao PortletMenu
                    SqlCommand com = new SqlCommand { Connection = con };
                    com.Parameters.AddWithValue("@DataId", DataId);
                    com.Parameters.AddWithValue("@MenuMasterId", new Guid(ddlListMenuMaster.SelectedValue));
                    com.CommandText = "Insert Into PortletMenu(DataId,MenuMasterId) Values (@DataId,@MenuMasterId)";
                    com.ExecuteNonQuery();
                }
                else
                {
                    //nguoc lai ta update thong tin ve TopicId cho portlet
                    SqlCommand com = new SqlCommand { Connection = con };
                    com.Parameters.AddWithValue("@DataId", DataId);
                    com.Parameters.AddWithValue("@TopicId", new Guid(ddlListMenuMaster.SelectedValue));
                    com.Parameters.AddWithValue("@MenuMasterId", new Guid(ddlListMenuMaster.SelectedValue));
                    com.CommandText = "Update PortletMenu Set MenuMasterId=@MenuMasterId where DataId=@DataId";
                    com.ExecuteNonQuery();
                }
            }
            Accept();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Cancel();
        }
    }
}