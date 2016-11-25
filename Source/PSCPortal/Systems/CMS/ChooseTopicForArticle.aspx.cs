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
using PSCPortal.Framework;

namespace PSCPortal.Systems.CMS
{
    public partial class ChooseTopicForArticle : PSCPortal.Framework.PSCDialog
    {
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
        protected void Page_Load(object sender, EventArgs e)
        {
            DataBind();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            LoadData();
        }
        protected void LoadData()
        { 
            rcbTopic.DataSource = TopicList;
            rcbTopic.DataTextField = "Path";
            rcbTopic.DataValueField = "Id";
            rcbTopic.DataBind();
        }
        [System.Web.Services.WebMethod]
        public static void TopicSelect(string idTopic)
        {
            Guid id = new Guid(idTopic);
            Topic topicSelected = (Topic) TopicList.Search(t => ((Topic)t).Id == id);
            PSCPage.DataShare = topicSelected;
        }
    }
}
