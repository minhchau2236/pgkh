using System;
using System.Data.SqlClient;

namespace PSCPortal.Portlets.NewsinBrief
{
    public partial class Edit : Engine.PortletEditControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
                LoadData();
        }

        private void LoadData()
        {
            CMS.TopicCollection listTopic = CMS.TopicCollection.GetTopicCollection();
            ddlListTopic.DataSource = listTopic;
            ddlListTopic.DataBind();
            using (
                SqlConnection con =
                    new SqlConnection(
                        System.Configuration.ConfigurationManager.ConnectionStrings["PSCPortalConnectionString"]
                            .ConnectionString))
            {
                SqlCommand com = new SqlCommand {Connection = con};
                con.Open();
                com.Parameters.AddWithValue("@DataId", DataId);
                com.CommandText = "Select TopicId from PortletNewsInBrief where DataId=@DataId";

                Object topicGuid = com.ExecuteScalar();
                if (topicGuid != null)
                {
                    Guid topic = (Guid) topicGuid;

                    int index = 0;
                    foreach (CMS.Topic t in listTopic)
                    {
                        if (t.Id == topic)
                        {
                            break;
                        }
                        index++;
                    }
                    ddlListTopic.SelectedIndex = index;

                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSCPortalConnectionString"].ConnectionString))
            {
                con.Open();
                SqlCommand comDataIdExist = new SqlCommand {Connection = con};
                comDataIdExist.Parameters.AddWithValue("@DataId", DataId);
                comDataIdExist.CommandText = "Select TopicId from PortletNewsInBrief where DataId=@DataId";
                Object topicGuid = comDataIdExist.ExecuteScalar();
                if (topicGuid == null)
                {
                    //neu khong ton tai record nao chua dataId, thi ta them moi vao PortletNewsInBrief
                    SqlCommand com = new SqlCommand {Connection = con};
                    com.Parameters.AddWithValue("@DataId", DataId);
                    com.Parameters.AddWithValue("@TopicId", new Guid(ddlListTopic.SelectedValue));
                    com.CommandText = "Insert Into PortletNewsInBrief(DataId,TopicId) Values (@DataId,@TopicId)";
                    com.ExecuteNonQuery();
                }
                else
                {
                    //nguoc lai ta update thong tin ve TopicId cho portlet
                    SqlCommand com = new SqlCommand {Connection = con};
                    com.Parameters.AddWithValue("@DataId", DataId);
                    com.Parameters.AddWithValue("@TopicId", new Guid(ddlListTopic.SelectedValue));
                    com.CommandText = "Update PortletNewsInBrief Set TopicId=@TopicId where DataId=@DataId";
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