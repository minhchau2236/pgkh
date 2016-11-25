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
using PSCPortal.Framework.Helpler;
using Telerik.Web.UI;
using System.Collections.Generic;
using PSCPortal.Engine;
namespace PSCPortal.Systems.CMS
{
    public partial class ArticleCommentManage : PSCPortal.Framework.PSCPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadData();
        }

        public string GetUserName(string userId)
        {
            string result = string.Empty;
            result = ((PSCPortal.Security.User)PSCPortal.Security.UserCollection.GetUserCollection().Where(u => u.Id.ToString() == userId).Single()).Name;
            return result;
        }


        protected void LoadData()
        {


        }
        protected static Article art
        {
            get
            {
                return DataStatic["art"] as Article;
            }
            set
            {
                DataStatic["art"] = value;
            }
        }

        protected static ArticleCollection ArticleList
        {
            get
            {
                if (DataStatic["ArticleList"] == null)
                    DataStatic["ArticleList"] = ArticleCollection.GetArticleCollectionPublish();
                return DataStatic["ArticleList"] as PSCPortal.CMS.ArticleCollection;
            }
            set
            {
                DataStatic["ArticleList"] = value;
            }
        }
        protected static ArticleCollection ArticleListCommentNew
        {
            get
            {
                Guid subId = SessionHelper.GetSession(SessionKey.SubDomain) == string.Empty ? Guid.Empty : new Guid(SessionHelper.GetSession(SessionKey.SubDomain));
                string groupAdmin = System.Configuration.ConfigurationManager.AppSettings["GroupAdmin"];
                if (System.Web.HttpContext.Current.User.IsInRole(groupAdmin))
                {
                    if (subId == Guid.Empty)
                    {
                        DataStatic["ArticleListCommentNew"] = ArticleCollection.GetArticleCollectionPublishCommentNew();
                    }
                    else
                    {
                        var subDomain = new PSCPortal.Engine.SubDomain { Id = subId };
                        var pageList = (PSCPortal.Engine.PageCollection)subDomain.GetPagesBelongTo();
                        var commentArticleList = new PSCPortal.CMS.ArticleCollection();
                        foreach (PSCPortal.Engine.Page page in pageList)
                        {
                            var obj = PSCPortal.CMS.ArticleCollection.GetListArticleCommentByPageId(page.Id.ToString());
                            foreach (var item in obj)
                            {
                                commentArticleList.Add(item);
                            }
                        }
                        DataStatic["ArticleListCommentNew"] = commentArticleList;
                    }
                }
                else
                {
                    var subDomain = new PSCPortal.Engine.SubDomain { Id = subId };
                    var pageList = (PSCPortal.Engine.PageCollection)subDomain.GetPagesBelongTo();
                    var commentArticleList = new PSCPortal.CMS.ArticleCollection();
                    foreach (PSCPortal.Engine.Page page in pageList)
                    {
                        var obj = PSCPortal.CMS.ArticleCollection.GetListArticleCommentByPageId(page.Id.ToString());
                        foreach (var item in obj)
                        {
                            commentArticleList.Add(item);
                        }
                    }
                    DataStatic["ArticleListCommentNew"] = commentArticleList;
                }
                return DataStatic["ArticleListCommentNew"] as ArticleCollection;
            }
            set
            {
                DataStatic["ArticleListCommentNew"] = value;
            }
        }


        [System.Web.Services.WebMethod]
        public static string GetArticleCommentNewList(int startIndex, int maximumRows, string sortExpressions)
        {
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            return js.Serialize(ArticleListCommentNew.GetSegment(startIndex, maximumRows, sortExpressions));
        }
        [System.Web.Services.WebMethod]
        public static int GetArticleCommentNewListCount()
        {
            return ArticleListCommentNew.Count;
        }
        [System.Web.Services.WebMethod]
        public static bool GetCommentList(string id)
        {
            bool result = false;
            Guid idArticle = new Guid(id);
            Article article = Article.GetArticle(idArticle.ToString());
            PSCPortal.CMS.ArticleCommentCollection CommentList = PSCPortal.CMS.ArticleCommentCollection.GetArticleCommentCollection(idArticle);
            if (CommentList.Count != 0)
            {
                result = true;
                PSCDialog.DataShare = new PSCPortal.CMS.ArticleArgs(article, article.GetDescription(), article.GetContent(), true);
            }
            return result;
        }
        [System.Web.Services.WebMethod]
        public static string GetPermission(int[] arr)
        {
            Dictionary<string, bool> result = new Dictionary<string, bool>();
            foreach (int idFun in arr)
            {
                result.Add(idFun.ToString(), PSCPortal.Security.SystemAuthentication.CheckAllowFunction(PSCPortal.Security.Function.Parse(idFun)));
            }
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            return js.Serialize(result);
        }
    }
}