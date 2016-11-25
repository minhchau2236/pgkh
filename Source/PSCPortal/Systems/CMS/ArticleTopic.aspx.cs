using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PSCPortal.Framework;
using PSCPortal.CMS;

namespace PSCPortal.Systems.CMS
{
    public partial class ArticleTopic : PSCDialog
    {
        private static ArticleArgs Args
        {
            get
            {
                return DataShare as ArticleArgs;
            }
        }
        protected static TopicCollection TopicList
        {
            get
            {
                if (DataStatic["TopicList"] == null)
                {
                    DataStatic["TopicList"] = TopicCollection.GetTopicCollection();
                }
                return DataStatic["TopicList"] as TopicCollection;
            }
        }
        protected static List<Topic> TopicBelongList
        {
            get
            {
                if (DataStatic["TopicBelongList"] == null)
                    DataStatic["TopicBelongList"] = Args.Article.GetTopicBelong();
                return DataStatic["TopicBelongList"] as List<Topic>;
            }

        }
        protected static List<Topic> TopicBelongOriginalList
        {
            get
            {
                return DataStatic["TopicBelongOriginalList"] as List<Topic>;
            }
            set
            {
                DataStatic["TopicBelongOriginalList"] = value;
            }
        }
       
        protected void Page_Load(object sender, EventArgs e)
        {
            DataBind();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            LoadData();            
        }
        protected void LoadData()
        {
            rcbTopic.DataSource = TopicList;
            rcbTopic.DataBind();

            TopicBelongOriginalList = new List<Topic>();
            foreach (Topic topic in TopicBelongList)
            {
                TopicBelongOriginalList.Add(topic);
            }
        }
        [System.Web.Services.WebMethod]
        public static void Save()
        {           
            IEnumerable<Topic> AddList = TopicBelongList.OfType<Topic>().Except(TopicBelongOriginalList.OfType<Topic>());

            foreach (Topic topic in AddList)
            {
                Args.Article.AddTopicBelong(topic);
            }
            IEnumerable<Topic> SubtractList = TopicBelongOriginalList.OfType<Topic>().Except(TopicBelongList.OfType<Topic>());
            foreach (Topic topic in SubtractList)
            {
                Args.Article.DeleteTopicBelong(topic);
            }
        }
        [System.Web.Services.WebMethod]
        public static string GetTopicBelong()
        {
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            return js.Serialize(TopicBelongList);
        }
        [System.Web.Services.WebMethod]
        public static string AddTopicBelong(string Id)
        {
            Guid topicId = new Guid(Id);
            Topic topic = (Topic)TopicList.Search(t => ((Topic)t).Id == topicId);
            TopicBelongList.Add(topic);
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            return js.Serialize(TopicBelongList);
        }
        [System.Web.Services.WebMethod]
        public static string DeleteTopicBelong(string[] arrId)
        {
            foreach (string Id in arrId)
            {
                Guid topicId = new Guid(Id);
                Topic topic = (Topic)TopicList.Search(t => ((Topic)t).Id == topicId);
                TopicBelongList.Remove(topic);
            }
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            return js.Serialize(TopicBelongList);
        }
    }
}
