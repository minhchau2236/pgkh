using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PSCPortal.CMS;
using PSCPortal.Framework;
using Telerik.Web.UI;

namespace PSCPortal.Systems.CMS
{
    public partial class ArticleChangeTopicPrimary : PSCDialog
    {
        private static ArticleArgs Args
        {
            get
            {
                return DataShare as ArticleArgs;
            }
        }
        private static List<Topic> TopicList
        {
            get
            {
                if (DataStatic["TopicList"] == null)
                    DataStatic["TopicList"] = Args.Article.GetTopicBelong();
                return DataStatic["TopicList"] as List<Topic>;
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
            rcbTopic.Items.Insert(0,new RadComboBoxItem(" ", Guid.Empty.ToString()));
            Topic temp = Args.Article.GetTopicBelongPrimary();
            if (temp != null)
                rcbTopic.Items.FindItemByValue(temp.Id.ToString()).Selected = true;

        }
        [System.Web.Services.WebMethod]
        public static void Save(Guid id)
        {
                Args.Article.SetTopicBelongPrimary(TopicList.Where(t => t.Id == id).Single());
        }        
    }
}
