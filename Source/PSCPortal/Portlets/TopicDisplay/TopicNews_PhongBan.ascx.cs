using System;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace PSCPortal.Portlets.TopicDisplay
{
    public partial class TopicNews_PhongBan : Engine.PortletControl
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

        public string ArticleId
        {
            get
            {
                if (ViewState["ArticleId"] == null)
                    ViewState["ArticleId"] = string.Empty;
                return (string)ViewState["ArticleId"];
            }
            set
            {
                ViewState["ArticleId"] = value;
            }
        }

        public string ArticleTitle
        {
            get
            {
                if (ViewState["ArticleTitle"] == null)
                    ViewState["ArticleTitle"] = string.Empty;
                return (string)ViewState["ArticleTitle"];
            }
            set
            {
                ViewState["ArticleTitle"] = value;
            }
        }

        public string ArticleId2
        {
            get
            {
                if (ViewState["ArticleId2"] == null)
                    ViewState["ArticleId2"] = string.Empty;
                return (string)ViewState["ArticleId2"];
            }
            set
            {
                ViewState["ArticleId2"] = value;
            }
        }

        public string ArticleTitle2
        {
            get
            {
                if (ViewState["ArticleTitle2"] == null)
                    ViewState["ArticleTitle2"] = string.Empty;
                return (string)ViewState["ArticleTitle2"];
            }
            set
            {
                ViewState["ArticleTitle2"] = value;
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
                if (arListHang.Count() == 0)
                    return;
                CMS.Article article = arListHang.Take(1).Single();
                ArticleId = article.Id.ToString();
                ArticleTitle = article.Title;
                CMS.Article article2 = arListHang.Skip(1).Take(1).Single();
                ArticleId2 = article2.Id.ToString();
                ArticleTitle2 = article2.Title;
                rptArticleRelation.DataSource = arListHang.Skip(2);
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