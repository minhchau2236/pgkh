using System;
using System.Data.SqlClient;
using PSCPortal.CMS;

namespace PSCPortal.Portlets.TopicDisplay
{
    public partial class Edit : Engine.PortletEditControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadData();
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
          
        }
        protected void bntCancel_Click(object sender, EventArgs e)
        {
            Accept();
        }

        protected TopicCollection TopicList
        {
            get
            {
                if (DataSelf["TopicList"] == null)
                    DataSelf["TopicList"] = TopicCollection.GetTopicCollection();
                return DataSelf["TopicList"] as TopicCollection;
            }
        }

        private void LoadData()
        {
            ddlTopic.DataSource = TopicList;
            ddlTopic.DataBind();
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSCPortalConnectionString"].ConnectionString))
            {
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                con.Open();
                com.Parameters.AddWithValue("@DataId", DataId);
                com.CommandText = "Select TopicId,NumberOfArticlesDisplay from PortletTopicDisplay where DataId=@DataId";

                Guid topicGuid = Guid.Empty;
                int numberOfArticlesDisplay = 0;
                using(SqlDataReader reader = com.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        topicGuid = new Guid(reader["TopicId"].ToString());
                        numberOfArticlesDisplay = (int)reader["NumberOfArticlesDisplay"];
                    }
                }
                if (topicGuid != Guid.Empty)
                {
                    txtDisplayNumber.Text = numberOfArticlesDisplay.ToString();
                    Guid topic = topicGuid;
                    int index = 0;
                    foreach (Topic t in TopicList)
                    {
                        if (t.Id == topic)
                        {
                            break;
                        }
                        index++;
                    }
                    ddlTopic.SelectedIndex = index;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSCPortalConnectionString"].ConnectionString))
            {
                con.Open();

                SqlCommand comDataIdExist = new SqlCommand();
                comDataIdExist.Connection = con;
                comDataIdExist.Parameters.AddWithValue("@DataId", DataId);
                comDataIdExist.CommandText = "Select TopicId from PortletTopicDisplay where DataId=@DataId";

                Object topicGuid = comDataIdExist.ExecuteScalar();


                if (topicGuid == null)
                {
                    //neu khong ton tai record nao chua dataId, thi ta them moi vao PortletNewsInBrief
                    SqlCommand com = new SqlCommand();
                    com.Connection = con;

                    com.Parameters.AddWithValue("@DataId", DataId);
                    com.Parameters.AddWithValue("@TopicId", new Guid(ddlTopic.SelectedValue));
                    com.Parameters.AddWithValue("@DisplayNumber", int.Parse(txtDisplayNumber.Text));
                    com.CommandText = "Insert Into PortletTopicDisplay(DataId,TopicId,NumberOfArticlesDisplay) Values (@DataId,@TopicId,@DisplayNumber)";
                    com.ExecuteNonQuery();
                }
                else
                {
                    //nguoc lai ta update thong tin ve TopicId cho portlet
                    SqlCommand com = new SqlCommand();
                    com.Connection = con;

                    com.Parameters.AddWithValue("@DataId", DataId);
                    com.Parameters.AddWithValue("@TopicId", new Guid(ddlTopic.SelectedValue));
                    com.Parameters.AddWithValue("@DisplayNumber", int.Parse(txtDisplayNumber.Text));
                    com.CommandText = "Update PortletTopicDisplay Set TopicId=@TopicId,NumberOfArticlesDisplay=@DisplayNumber where DataId=@DataId";
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