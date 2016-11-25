using System;
using System.Collections.Generic;
using System.Linq;
using PSCPortal.CMS;
using Telerik.Web.UI;
using System.Data.SqlClient;
using PSCPortal.Portlets.TabTopicDisplay.Lib;
using System.Web.Script.Serialization;
namespace PSCPortal.Portlets.TabTopicDisplay
{
    public partial class Display_TuyenSinh : Engine.PortletControl
    {
        class SliderTopic
        {
            public SliderTopic()
            {
                SliderArticleList = new List<Article>();
                SecondArticleList = new List<Article>();
            }
            public Guid TopicId1 { get; set; }
            public Guid TopicId2 { get; set; }
            public List<Article> SliderArticleList { get; set; }
            public List<Article> SecondArticleList { get; set; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
                LoadControl();
            DataBind();
        }

        public string ListArticle
        {
            get;
            set;
        }

        private void LoadControl()
        {
            SliderTopic result = new SliderTopic();
            TabTopicDisplayCollection listTabTopicDisplay = TabTopicDisplayCollection.GetTabTopicDisplayCollection();
            var sliderTopic = listTabTopicDisplay.Where(item => item.DataId == Portlet.PortletInstance.Id).FirstOrDefault();
            if (sliderTopic != null)
            {
                result.TopicId1 = sliderTopic.TopicId;
                result.SliderArticleList = ArticleCollection.GetArticleCollection(sliderTopic.TopicId.ToString(), 0, sliderTopic.NumberDisplay);
                result.SliderArticleList.ForEach(d => d.Name = d.GetDescription().Replace("\"", "\\\""));
            }
            var secondTopic = listTabTopicDisplay.Where(item => item.DataId == Portlet.PortletInstance.Id).Skip(1).Take(1).SingleOrDefault();
            if (secondTopic != null)
            {
                result.TopicId2 = secondTopic.TopicId;
                result.SecondArticleList = ArticleCollection.GetArticleCollection(secondTopic.TopicId.ToString(), 0, secondTopic.NumberDisplay).Take(3).ToList();
            }
            var js = new JavaScriptSerializer();
            ListArticle = js.Serialize(result);
        }

        protected override void DeleteData()
        {
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSCPortalConnectionString"].ConnectionString))
            {
                SqlCommand com = new SqlCommand { Connection = con };
                con.Open();
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.AddWithValue("@dataId", Portlet.PortletInstance.Id);
                com.CommandText = "Delete PortletTabTopicDisplay Where DataId=@dataId";
                com.ExecuteNonQuery();
            }
        }
    }
}