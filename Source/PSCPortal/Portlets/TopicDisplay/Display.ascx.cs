using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace PSCPortal.Portlets.TopicDisplay
{
    public partial class Display : Engine.PortletControl
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


        public CMS.Topic Topic
        {
            get {                
                return ViewState["TopicDisplay"] as CMS.Topic; 
            }
            set { ViewState["TopicDisplay"] = value; }
        }
        
        public string ListArticle
        {
            get
            {
                string results = string.Empty;
                List<CMS.ArticleArgs> ListArgs = new List<CMS.ArticleArgs>();
                if (Topic == null)
                    return results;
                CMS.ArticleCollection arList = CMS.ArticleCollection.GetArticleCollectionPublish(Topic);
                IEnumerable<CMS.Article> it = arList.Take(12);
                foreach (CMS.Article item in it)
                {
                    string des = CMS.Article.GetDescription(item.Id);
                    CMS.ArticleArgs arg = new CMS.ArticleArgs(item, des);
                    string url = Regex.Match(Server.UrlDecode(arg.Article.ImageUrl), "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
                    if (arg.Article.ImageUrl == string.Empty || !System.IO.File.Exists(Server.MapPath(url)))
                        arg.Article.ImageUrl = string.Empty;
                    else
                        arg.Article.ImageUrl = url;
                    ListArgs.Add(arg);
                }
                System.Web.Script.Serialization.JavaScriptSerializer serialize = new System.Web.Script.Serialization.JavaScriptSerializer();
                results = serialize.Serialize(ListArgs);

                return results;
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
            using (
                SqlConnection con =
                    new SqlConnection(
                        System.Configuration.ConfigurationManager.ConnectionStrings["PSCPortalConnectionString"]
                            .ConnectionString))
            {
                SqlCommand com = new SqlCommand {Connection = con};
                con.Open();
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.AddWithValue("@dataId", Portlet.PortletInstance.Id);
                com.CommandText = "select TopicId,NumberOfArticlesDisplay From PortletTopicDisplay Where DataId=@dataId";

                SqlDataReader reader = com.ExecuteReader();
                if (reader.Read())
                {
                    topicGuid = (Guid) reader["TopicId"];
                }
            }
            if (topicGuid == null)
                return;
            CMS.Topic topicDisplay = CMS.Topic.GetTopic(topicGuid.ToString());
            if (topicDisplay != null)
            {
                Topic = topicDisplay;
                TopicName = topicDisplay.Name;
                TopicId = topicDisplay.Id.ToString();
            }
        }

        protected override void DeleteData()
        {
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSCPortalConnectionString"].ConnectionString))
            {
                SqlCommand com = new SqlCommand {Connection = con};
                con.Open();
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.AddWithValue("@dataId", Portlet.PortletInstance.Id);
                com.CommandText = "Delete PortletTopicDisplay Where DataId=@dataId";
            }
        }

    }
}