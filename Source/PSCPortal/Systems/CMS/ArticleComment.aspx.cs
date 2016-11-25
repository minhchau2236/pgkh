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
    public partial class ArticleComment : PSCDialog
    {
        private static ArticleArgs Args
        {
            get
            {
                return DataShare as ArticleArgs;
            }           
        }
        private static Guid artId
        {
            get
            {
                return (Guid)DataStatic["artId"];
            }
            set
            {
                DataStatic["artId"] = value;
            }
        }

        private static PSCPortal.CMS.ArticleCommentCollection FeedBackList
        {
            get
            {            
                return DataStatic["FeedBackList"] as PSCPortal.CMS.ArticleCommentCollection;
            }
            set
            {
                DataStatic["FeedBackList"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {            
            DataBind();
            if (!IsPostBack)
                LoadData();
        }
        protected void LoadData()
        {
            artId = Args.Article.Id;
        }

         [System.Web.Services.WebMethod]
        public static string GetArticleList(int startIndex, int maximumRows, string sortExpressions)
        {
            FeedBackList = PSCPortal.CMS.ArticleCommentCollection.GetArticleCommentCollection(artId);     
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            return js.Serialize(FeedBackList.GetSegment(startIndex, maximumRows, sortExpressions));
        }

         [System.Web.Services.WebMethod]
         public static int GetArticleCount()
         {
             return FeedBackList.Count;
         }

        [System.Web.Services.WebMethod]
         public static void ArticleEdit(int id)
        {
            PSCPortal.CMS.ArticleComment feedback = FeedBackList.Where(a => a.ID == id).Single();
            PSCDialog.DataShare = new PSCPortal.CMS.ArticleCommentArgs(feedback, true);
        }
        [System.Web.Services.WebMethod]
        public static void ArticleUpdate()
        {
            PSCPortal.CMS.ArticleCommentArgs item = (PSCPortal.CMS.ArticleCommentArgs)PSCDialog.DataShare;
            item.FeedBack.Update();
            artId = item.FeedBack.IDArticle;
        }
        [System.Web.Services.WebMethod]
        public static void ArticleDelete(int[] arrId)
        {
            foreach (int id in arrId)
            {
                FeedBackList.RemoveDB(FeedBackList.Where(a => a.ID == id).Single());
                //Libs.Ultility.DeleteArticleIndexing(idArticle);
            }
        }
        [System.Web.Services.WebMethod]
        public static void ArticlePublish(string[] arrId)
        {            
            foreach (string id in arrId)
            {
                int ID = int.Parse(id);
                PSCPortal.CMS.ArticleComment feedback = FeedBackList.Where(a => a.ID == ID).Single();
                feedback.IsPublish = true;
                feedback.Update();
                //Libs.Ultility.IndexingArticle(article);
            }
        }
        [System.Web.Services.WebMethod]
        public static void ArticleUnPublish(int[] arrId)
        {
            foreach (int id in arrId)
            {
                PSCPortal.CMS.ArticleComment article = FeedBackList.Where(a => a.ID == id).Single();
                article.IsPublish = false;
                article.Update();
                //Libs.Ultility.DeleteArticleIndexing(idArticle);
            }
        }
    }
}