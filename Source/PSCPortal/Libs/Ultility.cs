using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using PSCPortal.CMS;
using PSCPortal.Engine;
using PSCPortal.Security;
using PSCPortal.Systems.Security;

namespace PSCPortal.Libs
{
    public static class Ultility
    {
        public static string ParseHTML(string html)
        {
            string result = System.Text.RegularExpressions.Regex.Replace(html, "<[^>]*>", "");
            List<string> lstFilterUnicode = new List<string>();
            System.IO.StreamReader readerF = new System.IO.StreamReader(HttpContext.Current.Server.MapPath("~/Libs/FilterUnicode.txt"));
            string strFilter;
            while ((strFilter = readerF.ReadLine()) != null)
                lstFilterUnicode.Add(strFilter);
            readerF.Close();
            return lstFilterUnicode.Select(s => s.Split('|')).Aggregate(result, (current, arrF) => current.Replace(arrF[1], arrF[0]));
        }
        public static void IndexingArticle(Article article)
        {
            Lucene.Net.Index.IndexWriter lwriter = new Lucene.Net.Index.IndexWriter(HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["IndexingArticle"]), new Lucene.Net.Analysis.Standard.StandardAnalyzer(), false);
            Lucene.Net.Documents.Document doc = new Lucene.Net.Documents.Document();
            doc.Add(new Lucene.Net.Documents.Field("ArticleId", article.Id.ToString(), Lucene.Net.Documents.Field.Store.YES, Lucene.Net.Documents.Field.Index.UN_TOKENIZED));
            doc.Add(new Lucene.Net.Documents.Field("ArticleTitle", article.Title, Lucene.Net.Documents.Field.Store.YES, Lucene.Net.Documents.Field.Index.TOKENIZED));
            doc.Add(new Lucene.Net.Documents.Field("ArticleDetail", ParseHTML(Article.GetContent(article.Id)), Lucene.Net.Documents.Field.Store.YES, Lucene.Net.Documents.Field.Index.TOKENIZED));
            lwriter.AddDocument(doc);
            lwriter.Close();
        }
        public static void DeleteArticleIndexing(Guid articleId)
        {
            Lucene.Net.Index.IndexReader reader = Lucene.Net.Index.IndexReader.Open(HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["IndexingArticle"]));
            reader.DeleteDocuments(new Lucene.Net.Index.Term("ArticleId", articleId.ToString()));
            reader.Close();
        }

        public static void SettingEditor(Telerik.Web.UI.RadEditor editor, string subName)
        {
            string rootImages = "~/Resources/Images/SubDomain/";
            string rootDocs = "~/Resources/Docs/SubDomain/";
            string rootMedias = "~/Resources/Medias/SubDomain/";
            string rootFlashs = "~/Resources/Flashs/SubDomain/";

            if (subName == string.Empty)
            {
                rootImages = "~/Resources/Images/HomePage/";
                rootDocs = "~/Resources/Docs/HomePage/";
                rootMedias = "~/Resources/Medias/HomePage/";
                rootFlashs = "~/Resources/Flashs/HomePage/";
            }
            RoleCollection roleCollection = RoleCollection.GetRoleCollection(HttpContext.Current.User.Identity.Name);
            bool isGroupAdmin = roleCollection.Any(r => r.Name == ConfigurationManager.AppSettings["GroupAdmin"]);

            editor.ImageManager.ViewPaths = new[] { rootImages + subName };
            editor.ImageManager.UploadPaths = new[] { rootImages + subName };
            if (isGroupAdmin && roleCollection.Count == 1)
                editor.ImageManager.DeletePaths = new[] { rootImages + subName };

            editor.DocumentManager.ViewPaths = new[] { rootDocs + subName };
            editor.DocumentManager.UploadPaths = new[] { rootDocs + subName };
            if (isGroupAdmin && roleCollection.Count == 1)
                editor.DocumentManager.DeletePaths = new[] { rootDocs + subName };

            editor.MediaManager.ViewPaths = new[] { rootMedias + subName };
            editor.MediaManager.UploadPaths = new[] { rootMedias + subName };
            if (isGroupAdmin && roleCollection.Count == 1)
                editor.MediaManager.DeletePaths = new[] { rootMedias + subName };

            editor.FlashManager.ViewPaths = new[] { rootFlashs + subName };
            editor.FlashManager.UploadPaths = new[] { rootFlashs + subName };
            if (isGroupAdmin && roleCollection.Count == 1)
                editor.FlashManager.DeletePaths = new[] { rootFlashs + subName };
            RoleCollection.GetRoleCollection(HttpContext.Current.User.Identity.Name);
        }

        public static void SettingFileExplorer(Telerik.Web.UI.RadFileExplorer editor, string path, string subName)
        {
            editor.Configuration.ViewPaths = new[] { path + subName };
            editor.Configuration.UploadPaths = new[] { path + subName };
            editor.Configuration.DeletePaths = new[] { path + subName };
        }

        public static string GetSubDomain()
        {
            string subName = string.Empty;
            string domain = ConfigurationManager.AppSettings["DomainName"];
            string mainDomain = ConfigurationManager.AppSettings["MainDomainName"];
            if (HttpContext.Current.Request.Url.Host.IndexOf("www") > -1)
                subName = HttpContext.Current.Request.Url.Host.Replace(mainDomain, "");
            else
                subName = HttpContext.Current.Request.Url.Host.Replace(domain, "");
            if (subName.Length > 0)
                subName = subName.Substring(0, subName.Length - 1);
            return subName;
        }

        public static Guid GetPageIdByArticleId(string articleId)
        {
            Article article = Article.GetArticle(articleId);
            //Article article = Article.GetArticleByDescription(articleId, topicDescription);
            return article.PageId;
        }
        public static Guid GetPageIdByArticleIdUnPublish(string articleId)
        {
            Article article = Article.GetArticleUnPublish(articleId);
            return article.PageId;
        }
        public static Guid GetPageIdByTopicId(string topicId)
        {
            Topic topic = Topic.GetTopic(topicId);
            //Topic topic = Topic.GetTopicByDescription(topicId);
            return topic == null ? Guid.Empty : topic.PageId;
        }
        public static Guid GetPageIdByModuleleId(string moduleId)
        {
            Module module = Module.GetModule(moduleId);
            return module.PageId;
        }

        public static string GetQueryStrings()
        {
            string rawUrl = HttpContext.Current.Request.RawUrl;
            int index = rawUrl.IndexOf('?');
            if (index < 0)
                return string.Empty;
            return rawUrl.Substring(index);
        }
    }
}
