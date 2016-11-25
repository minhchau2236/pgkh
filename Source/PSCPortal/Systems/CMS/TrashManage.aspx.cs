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
using PSCPortal.Engine;
using PSCPortal.Framework.Helpler;

namespace PSCPortal.Systems.CMS
{
    public partial class TrashManage : PSCPortal.Framework.PSCPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //DataBind();
        }
       
        protected static ArticleCollection ArticleTrashList
        {
            get
            {
                //if (DataStatic["ArticleTrashList"] == null)
                    DataStatic["ArticleTrashList"] = ArticleCollection.GetArticleTrashCollection();
                return DataStatic["ArticleTrashList"] as ArticleCollection;
            }
        }
        protected static List<Article> DisplayArticleTrashList
        {
            get
            {
                return DataStatic["DisplayArticleTrashList"] as List<Article>;
            }
            set
            {
                DataStatic["DisplayArticleTrashList"] = value;
            }
        }
        protected static ArticleCollection ArticleSendList
        {
            get
            {
                //if (DataStatic["ArticleSendList"] == null)
                //{
                    Guid subId = SessionHelper.GetSession(SessionKey.SubDomain) == string.Empty ? Guid.Empty : new Guid(SessionHelper.GetSession(SessionKey.SubDomain));
                    SubDomain subDomain = new SubDomain { Id = subId };
                    DataStatic["ArticleSendList"] = subDomain.GetArticlesBelongTo();
                //}
                return DataStatic["ArticleSendList"] as ArticleCollection;
            }
        }
        [System.Web.Services.WebMethod]
        public static string GetArticleTrashList(int startIndex, int maximumRows, string sortExpressions)
        {
            Guid subId = SessionHelper.GetSession(SessionKey.SubDomain) == string.Empty ? Guid.Empty : new Guid(SessionHelper.GetSession(SessionKey.SubDomain));
            if (subId == Guid.Empty)
            {
                DisplayArticleTrashList = ArticleTrashList.ToList();
            }
            else
            {
                DisplayArticleTrashList = new List<Article>();
                PSCPortal.Engine.SubDomain subDomain = new PSCPortal.Engine.SubDomain {Id = subId};
                PageCollection listPage = subDomain.GetPagesBelongTo();
                foreach (var item in listPage)
                {
                    foreach (var article in ArticleTrashList.Where(ar => ar.PageId == item.Id))
                    {
                        DisplayArticleTrashList.Add(article);
                    }
                }
            }
            // kiem tra nhung bai viet duoc goi den da xu ly ?
            foreach (var item in ArticleSendList.Where(ar => ar.IsCheck == false))
            {
                Article article = DisplayArticleTrashList.SingleOrDefault(ar => ar.Id == item.Id);
                if (article != null)
                    DisplayArticleTrashList.Remove(article);
            }
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            return js.Serialize(Libs.IEnumerableExtentionMethods.GetSegmentList(DisplayArticleTrashList, startIndex, maximumRows, sortExpressions));
        }
        [System.Web.Services.WebMethod]
        public static int GetArticleTrashCount()
        {
            return DisplayArticleTrashList.Count;
        }
        [System.Web.Services.WebMethod]
        public static void ArticleTrashRestore(string[] arrId)
        {
            foreach (string articleId in arrId)
            {
                Guid ArticleId = new Guid(articleId);
                Article article = ArticleTrashList.Where(a => a.Id == ArticleId).Single();
                Topic topic = PSCPage.DataShare as Topic;
                article.AddTopicBelong(topic);
                article.SetTopicBelongPrimary(topic);
                ArticleTrashList.Remove(article);
            }
        }
        [System.Web.Services.WebMethod]
        public static void ArticleTrashDelete(string[] arrId)
        {
            foreach (string id in arrId)
            {
                Guid idArticle = new Guid(id);
                ArticleTrashList.Remove(ArticleTrashList.Where(a => a.Id == idArticle).Single());
                PSCPortal.CMS.ArticleCollection.DeleteArticleFromTrash(id);
            }
        }
        [System.Web.Services.WebMethod]
        public static string GetPermission(int[]arr)
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
