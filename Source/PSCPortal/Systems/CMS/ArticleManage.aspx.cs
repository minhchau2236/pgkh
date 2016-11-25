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
using Telerik.Web.UI;
using System.Collections.Generic;
using PSCPortal.Framework.Helpler;

namespace PSCPortal.Systems.CMS
{
    public partial class ArticleManage : PSCPortal.Framework.PSCPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadData();

        }

        [System.Web.Services.WebMethod]
        public static string SecurityUI(string id)
        {
            Guid topicId = new Guid(id);
            Topic topic = (Topic)TopicList.Search(t => ((Topic)t).Id == topicId);
            TopicAuthentication ta = TopicAuthentication.GetTopicAuthentication(topic);
            bool allowNew = ta.IsAllow(TopicPermission.PERMISSION.ARTICLE_NEW);
            bool allowEdit = ta.IsAllow(TopicPermission.PERMISSION.ARTICLE_EDIT);
            bool allowDelete = ta.IsAllow(TopicPermission.PERMISSION.ARTICLE_DELETE);
            bool allowAprove = ta.IsAllow(TopicPermission.PERMISSION.ARTICLE_APROVE);
            var result = new { AllowNew = allowNew, AllowEdit = allowEdit, AllowDelete = allowDelete, AllowAprove = allowAprove };
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            return js.Serialize(result);
        }
        public string GetUserName(string userId)
        {
            string result = string.Empty;
            result = ((PSCPortal.Security.User)PSCPortal.Security.UserCollection.GetUserCollection().Where(u => u.Id.ToString() == userId).Single()).Name;
            return result;
        }


        protected void LoadData()
        {
            if (System.Web.HttpContext.Current.Request.QueryString["Id"] == null)
            {
                rtvTopic.DataSource = TopicList.GetBindingSource();
                rtvTopic.DataBind();
            }
            else
            {
                art = PSCPortal.CMS.Article.GetArticle(System.Web.HttpContext.Current.Request.QueryString["Id"]);
                rtvTopic.DataSource = TopicBelongArticle.GetBindingSource();
                rtvTopic.DataBind();

            }

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
        protected static Topic topic1
        {
            get
            {
                return DataStatic["topic"] as Topic;
            }
            set
            {
                DataStatic["topic"] = value;
            }
        }
        protected static TopicCollection TopicBelongArticle
        {
            get
            {
                if (DataStatic["TopicCollection"] == null)
                    DataStatic["TopicCollection"] = TopicCollection.GetTopicCollectionByArticleId(art.Id.ToString());
                return DataStatic["TopicCollection"] as TopicCollection;
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
        protected static ArticleCollection ArticleListChuaXB
        {
            get
            {
                return DataStatic["ArticleListChuaXB"] as ArticleCollection;
            }
            set
            {
                DataStatic["ArticleListChuaXB"] = value;
            }
        }
        protected static ArticleCollection ArticleListCommentNew
        {
            get
            {
                return DataStatic["ArticleListCommentNew"] as ArticleCollection;
            }
            set
            {
                DataStatic["ArticleListCommentNew"] = value;
            }
        }
        public static ArticleLoginCollection ArticleLoginList
        {
            get
            {
                if (DataStatic["ArticleLoginList"] == null)
                    DataStatic["ArticleLoginList"] = ArticleLoginCollection.GetArticleLoginCollection();
                return DataStatic["ArticleLoginList"] as ArticleLoginCollection;
            }
            set
            {
                DataStatic["ArticleLoginList"] = value;
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
        public static string GetArticleListChuaXB(string topicId, int startIndex, int maximumRows, string sortExpressions)
        {
            Guid idTopic = new Guid(topicId);
            Topic topic = (Topic)TopicList.Search(o => ((Topic)o).Id == idTopic);
            ArticleListChuaXB = ArticleCollection.GetArticleCollectionUnPublish(topic);
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            return js.Serialize(ArticleListChuaXB.GetSegment(startIndex, maximumRows, sortExpressions));
        }
        [System.Web.Services.WebMethod]
        public static string GetArticleCommentNewList(string topicId, int startIndex, int maximumRows, string sortExpressions)
        {
            Guid idTopic = new Guid(topicId);
            Topic topic = (Topic)TopicList.Search(o => ((Topic)o).Id == idTopic);
            ArticleListCommentNew = ArticleCollection.GetArticleCollectionPublishCommentNew(topic);
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            return js.Serialize(ArticleListCommentNew.GetSegment(startIndex, maximumRows, sortExpressions));
        }
        [System.Web.Services.WebMethod]
        public static int GetArticleCount()
        {
            return ArticleList.Count;
        }
        [System.Web.Services.WebMethod]
        public static int GetArticleCountChuaXB()
        {
            return ArticleListChuaXB.Count;
        }
        [System.Web.Services.WebMethod]
        public static int GetArticleCommentNewListCount()
        {
            return ArticleListCommentNew.Count;
        }
        [System.Web.Services.WebMethod]
        public static void ArticleNew(Guid idTopic)
        {
            Article item = new Article();
            item.Id = Guid.NewGuid();
            item.UserAdd = System.Web.HttpContext.Current.User.Identity.Name;
            item.CreatedDate = DateTime.Now;
            item.ModifiedDate = DateTime.Now;
            Topic topic = (Topic)TopicList.Search(o => ((Topic)o).Id == idTopic);
            item.PageId = topic.PageId;
            PSCDialog.DataShare = new ArticleArgs(item, item.GetDescription(), item.GetContent(), false);
        }
        [System.Web.Services.WebMethod]
        public static void ArticleAdd()
        {
            ArticleArgs item = (ArticleArgs)PSCDialog.DataShare;
            ArticleList.AddDB(item.Article);
            item.Article.UpdateDescription(item.Description);
            item.Article.UpdateContent(item.Content);
            //item.Article.UpdateAvatar(item.Avatar);   
            //item.Article.UpdateImagePortlet();
            item.Article.UpdateImage();
            if (item.Article.ArticleHangDate != null)
                item.Article.UpdateArticleHang(item.Article.ArticleHangDate);

        }
        [System.Web.Services.WebMethod]
        public static void ArticleEdit(string id)
        {
            Guid idArticle = new Guid(id);
            Article article = ArticleList.Where(a => a.Id == idArticle).Single();
            article.UserAdd = System.Web.HttpContext.Current.User.Identity.Name;
            article.ArticleHangDate = article.GetArticleHang();
            PSCDialog.DataShare = new ArticleArgs(article, article.GetDescription(), article.GetContent(), true);
        }
        [System.Web.Services.WebMethod]
        public static void ArticleEditTopic(string id)
        {
            Guid idArticle = new Guid(id);
            Article article = ArticleList.Where(a => a.Id == idArticle).Single();
            article.UserAdd = System.Web.HttpContext.Current.User.Identity.Name;
            PSCDialog.DataShare = new ArticleArgs(article, article.GetDescription(), article.GetContent(), true);
        }
        [System.Web.Services.WebMethod]
        public static void ArticleUpdate()
        {
            ArticleArgs item = (ArticleArgs)PSCDialog.DataShare;
            item.Article.Update();
            item.Article.UpdateDescription(item.Description);
            item.Article.UpdateContent(item.Content);
            item.Article.UpdateImage();
            //item.Article.UpdateAvatar(item.Avatar);         
            //item.Article.UpdateImagePortlet();
            if (item.Article.ArticleHangDate != null)
                item.Article.UpdateArticleHang(item.Article.ArticleHangDate);
            else
                item.Article.DeleteArticleHang();

        }
        [System.Web.Services.WebMethod]
        public static void ArticleDelete(string[] arrId)
        {
            foreach (string id in arrId)
            {
                Guid idArticle = new Guid(id);

                ArticleList.RemoveDB(ArticleList.Where(a => a.Id == idArticle).Single());
                Libs.Ultility.DeleteArticleIndexing(idArticle);
            }
        }
        [System.Web.Services.WebMethod]
        public static void ArticlePublish(string[] arrId)
        {
            foreach (string id in arrId)
            {
                Guid idArticle = new Guid(id);
                Article article = ArticleList.Where(a => a.Id == idArticle).Single();
                Article model = Article.GetArticleAlbum(idArticle);
                article.IsPublish = true;
                article.AlbumPath = model.AlbumPath;
                article.DocumentPath = model.DocumentPath;
                article.Update();
                Libs.Ultility.IndexingArticle(article);
            }
        }
        [System.Web.Services.WebMethod]
        public static void ArticleUnPublish(string[] arrId)
        {
            foreach (string id in arrId)
            {
                Guid idArticle = new Guid(id);
                Article article = ArticleList.Where(a => a.Id == idArticle).Single();
                Article model = Article.GetArticleAlbum(idArticle);
                article.IsPublish = false;
                article.AlbumPath = model.AlbumPath;
                article.DocumentPath = model.DocumentPath;
                article.Update();
                Libs.Ultility.DeleteArticleIndexing(idArticle);
            }
        }

        [System.Web.Services.WebMethod]
        public static bool ArticleChangeTopicPrimary(Guid id)
        {
            bool result = false;
            Article article = ArticleList.Where(a => a.Id == id).Single();
            List<Topic> listTopic = article.GetTopicBelong();
            if (listTopic.Count > 1)
            {
                result = true;
                PSCDialog.DataShare = new ArticleArgs(article, article.GetDescription(), article.GetContent(), true);
            }
            return result;
        }
        [System.Web.Services.WebMethod]
        public static bool GetCommentList(string id)
        {
            bool result = false;
            Guid idArticle = new Guid(id);
            Article article = ArticleList.Where(a => a.Id == idArticle).Single();
            PSCPortal.CMS.ArticleCommentCollection CommentList = PSCPortal.CMS.ArticleCommentCollection.GetArticleCommentCollection(article.Id);
            // PSCPortal.Modules.FeedBack.Libs.FeedBackCollection CommentList1=PSCPortal.Modules.FeedBack.Libs.FeedBackCollection.GetFeedBackPublicCollectionOfArticle(id);
            if (CommentList.Count != 0)
            {
                result = true;
                PSCDialog.DataShare = new ArticleArgs(article, article.GetDescription(), article.GetContent(), true);
            }
            return result;
        }
        [System.Web.Services.WebMethod]
        public static string GetArticleNoBelongTopicPrimary(int startIndex, int maximumRows, string sortExpressions)
        {
            ArticleCollection articles = ArticleCollection.GetArticlesNoBelongTopicPrimary();
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            return js.Serialize(articles.GetSegment(startIndex, maximumRows, sortExpressions));
        }
        [System.Web.Services.WebMethod]
        public static void ArticleSend(string id)
        {
            Guid idArticle = new Guid(id);
            Article article = ArticleList.Where(a => a.Id == idArticle).Single();
            Article model = Article.GetArticleAlbum(idArticle);
            article.ArticleHangDate = article.GetArticleHang();
            article.AlbumPath = model.AlbumPath;
            article.DocumentPath = model.DocumentPath;
            PSCDialog.DataShare = new ArticleArgs(article, article.GetDescription(), article.GetContent(), true);
        }

        [System.Web.Services.WebMethod]
        public static void ArticleLoginEdit(string id)
        {
            Guid idArticle = new Guid(id);
            ArticleLogin articleLogin = ArticleLoginList.Where(t => t.Id == idArticle).SingleOrDefault();
            if (articleLogin != null)
                PSCDialog.DataShare = new ArticleLoginArgs(articleLogin, true);
            else
            {
                articleLogin = new ArticleLogin();
                articleLogin.Id = idArticle;
                PSCDialog.DataShare = new ArticleLoginArgs(articleLogin, false);
            }
        }

        [System.Web.Services.WebMethod]
        public static string ArticleSearch(string ArticleTitle, int startIndex, int maximumRows, string sortExpressions)
        {
            ArticleCollection ArticleListSearch = new ArticleCollection();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            ArticleList = ArticleCollection.GetArticleBySearch(ArticleTitle);
            Guid subId = SessionHelper.GetSession(SessionKey.SubDomain) == string.Empty ? Guid.Empty : new Guid(SessionHelper.GetSession(SessionKey.SubDomain));
            if (subId == Guid.Empty)
            {
                ArticleListSearch = ArticleList;
            }
            else
            {
                PSCPortal.Engine.SubDomain subDomain = new PSCPortal.Engine.SubDomain { Id = subId };
                PSCPortal.Engine.PageCollection listPage = subDomain.GetPagesBelongTo();
                foreach (var item in listPage)
                {
                    foreach (var article in ArticleList.Where(ar => ar.PageId == item.Id))
                    {
                        ArticleListSearch.Add(article);
                    }
                }
            }
            dic.Add("Data", ArticleListSearch.GetSegment(startIndex, maximumRows, sortExpressions));
            dic.Add("Count", ArticleListSearch.Count);
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            return js.Serialize(dic);
        }
    }
}

