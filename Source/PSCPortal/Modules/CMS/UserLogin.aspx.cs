using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PSCPortal.CMS;

namespace PSCPortal.Modules.CMS
{
    public partial class UserLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public static string TopicId
        {
            get; set;
        }

        public static string ArticleId
        {
            get;
            set;
        }

        [System.Web.Services.WebMethod]
        public static bool Save(string username, string password)
        {
            bool ischeck = false;
            if (!String.IsNullOrEmpty(TopicId))
            {
                TopicLogin topicLogin = PSCPortal.CMS.TopicLogin.GetTopicLogin(TopicId);
                if (topicLogin != null)
                {
                    if (string.Equals(username, topicLogin.Name) && string.Equals(password, topicLogin.Password))
                    {
                        HttpContext.Current.Session["UserWatchTopic"] = topicLogin.Name;
                        ischeck = true;
                        TopicId = null;
                    }
                }
            }
            if (!String.IsNullOrEmpty(ArticleId))
            {
                ArticleLogin articleLogin = PSCPortal.CMS.ArticleLogin.GetArticleLogin(ArticleId);
                if (articleLogin != null)
                {
                    if (string.Equals(username, articleLogin.Name) && string.Equals(password, articleLogin.Password))
                    {
                        HttpContext.Current.Session["UserWatchArticle"] = articleLogin.Name;
                        ischeck = true;
                        ArticleId = null;
                    }
                }
            }
            return ischeck;
        }
    }
}