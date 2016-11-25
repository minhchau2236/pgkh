using System;
using System.Linq;
using PSCPortal.CMS;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using PSCPortal.CMS;

namespace PSCPortal.Portlets.SliderTopicDisplay
{
    public partial class TopicSliderPro : Engine.PortletControl
    {//public string TopicName
        //{
        //    get
        //    {
        //        if (ViewState["TopicName"] == null)
        //            ViewState["TopicName"] = string.Empty;
        //        return (string)ViewState["TopicName"];
        //    }
        //    set
        //    {
        //        ViewState["TopicName"] = value;
        //    }
        //}
        //public string TopicId
        //{
        //    get
        //    {
        //        if (ViewState["TopicId"] == null)
        //            ViewState["TopicId"] = string.Empty;
        //        return (string)ViewState["TopicId"];
        //    }
        //    set
        //    {
        //        ViewState["TopicId"] = value;
        //    }
        //}

        class SliderTopic
        {
            public SliderTopic()
            {
                SliderArticleList = new List<Article>();
            }
            public Guid TopicId { get; set; }
            public string TopicName { get; set; }
            public List<Article> SliderArticleList { get; set; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadData();
            DataBind();
        }
        public string ListArticle
        {
            get;
            set;
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
            SliderTopic result = new SliderTopic();
            if (topicDisplay != null)
            {
                //thay đổi ngày 6/5/2014

                CMS.ArticleCollection arList = CMS.ArticleCollection.GetArticleCollectionPublish(topicDisplay);
                CMS.ArticleCollection arListHang = CMS.ArticleCollection.GetArticleCollectionPublishHang(topicDisplay);
                IEnumerable<CMS.Article> iArt1 = arList.Where(a => !arListHang.Contains(a)).Take(numberOfArticlesDisplay - arListHang.Count());
                foreach (var item in iArt1)
                {
                    arListHang.Add(item);
                }
                var js = new JavaScriptSerializer();
                Dictionary<string, object> resultDic = new Dictionary<string, object>();
                resultDic["ResultList"] = arListHang;
                resultDic["TopicId"] = topicDisplay.Id;
                resultDic["TopicName"] = topicDisplay.Name;
                ListArticle = js.Serialize(resultDic);
                //rptArticleRelation.DataSource = arListHang;
                //rptArticleRelation.DataBind();
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