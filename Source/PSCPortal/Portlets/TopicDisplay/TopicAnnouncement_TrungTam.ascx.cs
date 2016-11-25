using System;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace PSCPortal.Portlets.TopicDisplay
{
    public partial class TopicAnnouncement_TrungTam : Engine.PortletControl
    {
        public string TopicName
        {
            get
            {
                if (ViewState["TopicName"] == null)
                    ViewState["TopicName"] = string.Empty;
                return (string)ViewState["TopicName"];
            }
            set
            {
                ViewState["TopicName"] = value;
            }
        }
        public string TopicId
        {
            get
            {
                if (ViewState["TopicId"] == null)
                    ViewState["TopicId"] = string.Empty;
                return (string)ViewState["TopicId"];
            }
            set
            {
                ViewState["TopicId"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadData();
            DataBind();
        }

        private void LoadData()
        {
            Object topicGuid = null;
            int numberOfArticlesDisplay = 0;
            using (
                SqlConnection con =
                    new SqlConnection(
                        System.Configuration.ConfigurationManager.ConnectionStrings["PSCPortalConnectionString"]
                            .ConnectionString))
            {
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                con.Open();
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.AddWithValue("@dataId", Portlet.PortletInstance.Id);
                com.CommandText = "select TopicId,NumberOfArticlesDisplay From PortletTopicDisplay Where DataId=@dataId";

                SqlDataReader reader = com.ExecuteReader();
                if (reader.Read())
                {
                    topicGuid = (Guid)reader["TopicId"];
                    numberOfArticlesDisplay = int.Parse(reader["NumberOfArticlesDisplay"].ToString());
                }
            }
            if (topicGuid == null)
                return;

            CMS.Topic topicDisplay = CMS.Topic.GetTopic(topicGuid.ToString());
            if (topicDisplay != null)
            {
                //thay đổi ngày 6/5/2014
                TopicName = topicDisplay.Name;
                TopicId = topicDisplay.Id.ToString();
                CMS.ArticleCollection arList = CMS.ArticleCollection.GetArticleCollectionPublish(topicDisplay);
                CMS.ArticleCollection arListHang = CMS.ArticleCollection.GetArticleCollectionPublishHang(topicDisplay);
                IEnumerable<CMS.Article> iArt1 = arList.Where(a => !arListHang.Contains(a)).Take(numberOfArticlesDisplay - arListHang.Count());
                foreach (var item in iArt1)
                {
                    arListHang.Add(item);
                }
                rptArticleRelation.DataSource = arListHang;
                rptArticleRelation.DataBind();
            }
        }

        protected override void DeleteData()
        {
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSCPortalConnectionString"].ConnectionString))
            {
                SqlCommand com = new SqlCommand { Connection = con };
                con.Open();
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.AddWithValue("@dataId", Portlet.PortletInstance.Id);
                com.CommandText = "Delete PortletTopicDisplay Where DataId=@dataId";
            }
        }
    }
}