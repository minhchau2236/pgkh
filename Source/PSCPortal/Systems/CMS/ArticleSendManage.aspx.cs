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
using System.Collections.Generic;
using PSCPortal.Framework.Helpler;
using PSCPortal.Engine;

namespace PSCPortal.Systems.CMS
{
    public partial class ArticleSendManage : PSCPortal.Framework.PSCPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           if(!IsPostBack)
               DataBind();
        }
        protected static ArticleCollection ArticleList
        {
            get
            {
                if (DataStatic["ArticleList"] == null)
                {
                    Guid subId = SessionHelper.GetSession(SessionKey.SubDomain) == string.Empty ? Guid.Empty : new Guid(SessionHelper.GetSession(SessionKey.SubDomain));
                    SubDomain subDomain = new SubDomain { Id = subId };
                    DataStatic["ArticleList"] = subDomain.GetArticlesBelongTo();
                }
                return DataStatic["ArticleList"] as ArticleCollection;
            }
        }
        [System.Web.Services.WebMethod]
        public static string GetArticleList(int startIndex, int maximumRows, string sortExpressions)
        {
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            return js.Serialize(ArticleList.GetSegment(startIndex, maximumRows, sortExpressions));
        }
        [System.Web.Services.WebMethod]
        public static int GetArticleCount()
        {
            return ArticleList.Count;
        }
        [System.Web.Services.WebMethod]
        public static void ArticleRestore(string[] arrId)
        {
            foreach (string articleId in arrId)
            {
                Guid ArticleId = new Guid(articleId);
                Article article = ArticleList.Where(a => a.Id == ArticleId).Single();
                ArticleSendArgs args = PSCPage.DataShare as ArticleSendArgs;
                //update topic
                article.AddTopicBelong(new Topic { Id = args.TopicId });
                article.SetTopicBelongPrimary(new Topic { Id = args.TopicId });
                //update page
                article.PageId = args.PageId;
                article.UpdatePageByArticleSend();
                //update flag da xu ly
                article.IsCheck = true;
                SubDomainInArticle sdia = new SubDomainInArticle();
                sdia.ArticleNewId = article.Id;
                sdia.IsCheck = true;
                Guid subDomainTo = SessionHelper.GetSession(SessionKey.SubDomain) == string.Empty ? Guid.Empty : new Guid(SessionHelper.GetSession(SessionKey.SubDomain));
                sdia.SubDomainToId = subDomainTo;
                sdia.Update();
                ArticleList.Remove(article);
            }
        }
        [System.Web.Services.WebMethod]
        public static void ArticleDelete(string[] arrId)
        {
            foreach (string id in arrId)
            {
                Guid idArticle = new Guid(id);
                ArticleList.Remove(ArticleList.Where(a => a.Id == idArticle).Single());
                PSCPortal.CMS.ArticleCollection.DeleteArticleFromTrash(id);
            }
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
