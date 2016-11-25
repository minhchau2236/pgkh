using System;
using System.Linq;

namespace PSCPortal.Portlets.TabTopicDisplay.Lib
{
    public partial class DisplayArticleTopic : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadData();
            DataBind();
        }

        public CMS.Topic Topic
        {
            get
            {
                return ViewState["Topic"] as CMS.Topic;
            }
            set
            {
                ViewState["Topic"] = value;
            }
        }
        public int Number
        {
            get
            {
                return (int)ViewState["Number"];
            }
            set
            {
                ViewState["Number"] = value;
            }
        }
        public CMS.ArticleCollection ListArticle
        {
            get
            {
                if (ViewState["ListArticle"] == null)
                    ViewState["ListArticle"] = CMS.ArticleCollection.GetArticleCollectionPublish(Topic);
                return ViewState["ListArticle"] as CMS.ArticleCollection;
            }
            set
            {
                ViewState["ListArticle"] = value;
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

        public string ArticleName
        {
            get
            {
                if (ViewState["ArticleName"] == null)
                    ViewState["ArticleName"] = string.Empty;
                return (string)ViewState["ArticleName"];
            }
            set
            {
                ViewState["ArticleName"] = value;
            }
        }
        public string ArticleDescription
        {
            get
            {
                if (ViewState["ArticleDescription"] == null)
                    ViewState["ArticleDescription"] = string.Empty;
                return (string)ViewState["ArticleDescription"];
            }
            set
            {
                ViewState["ArticleDescription"] = value;
            }
        }

        public DateTime ArticleCreatedDate
        {
            get
            {
                if (ViewState["ArticleCreatedDate"] == null)
                    ViewState["ArticleCreatedDate"] = DateTime.Now;
                return (DateTime)ViewState["ArticleCreatedDate"];
            }
            set
            {
                ViewState["ArticleCreatedDate"] = value;
            }
        }        
        public void LoadData()
        {
            System.Collections.Generic.IEnumerable<CMS.Article> it = ListArticle.Take(Number);
            rptTabTopic.DataSource = it;
            rptTabTopic.DataBind();
        }
    }
}