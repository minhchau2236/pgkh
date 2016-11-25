using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using PSCPortal.CMS;

namespace PSCPortal.Systems.CMS
{
    public partial class ChooseArticle : PSCPortal.Framework.PSCDialog
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadData();
        }
        protected void LoadData()
        {
            rtvTopic.DataSource = TopicList.GetBindingSource();
            rtvTopic.DataBind();
        }
        //protected static TopicCollection TopicList
        //{
        //    get
        //    {
        //        if (DataStatic["TopicList"] == null)
        //            DataStatic["TopicList"] = TopicCollection.GetTopicCollection();
        //        return DataStatic["TopicList"] as TopicCollection;
        //    }
        //}
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
        protected static ArticleCollection ArticleList
        {
            get
            {               
                return DataStatic["ArticleList"] as ArticleCollection;
            }
            set
            {
                DataStatic["ArticleList"] = value;
            }
        }
        [System.Web.Services.WebMethod]
        public static string GetArticleList(string topicId, int startIndex, int maximumRows, string sortExpressions)
        {
            Guid idTopic = new Guid(topicId);
            Topic topic = (Topic)TopicList.Search(o => ((Topic)o).Id == idTopic);
            ArticleList = ArticleCollection.GetArticleCollection(topic);
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            return js.Serialize(ArticleList.GetSegment(startIndex, maximumRows, sortExpressions));
        }
        [System.Web.Services.WebMethod]
        public static int GetArticleCount()
        {
            return ArticleList.Count;
        }
    }
}
