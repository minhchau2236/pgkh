using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Microsoft.SqlServer.Server;
using PSCPortal.CMS;
using PSCPortal.Engine;
using System.Web.Routing;
using System.Web.Script.Serialization;

namespace PSCPortal.Services
{
    /// <summary>
    /// Summary description for CMS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class CMS : System.Web.Services.WebService
    {

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public string Search(string strQuery)
        {
            string result = string.Empty;
            Lucene.Net.Index.IndexReader reader = Lucene.Net.Index.IndexReader.Open(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["IndexingArticle"]));
            Lucene.Net.QueryParsers.QueryParser parser = new Lucene.Net.QueryParsers.QueryParser("ArticleDetail", new Lucene.Net.Analysis.Standard.StandardAnalyzer());
            Lucene.Net.Search.Query query = parser.Parse(strQuery);
            Lucene.Net.Search.IndexSearcher searcher = new Lucene.Net.Search.IndexSearcher(reader);
            Lucene.Net.Search.Hits hits = searcher.Search(query);
            Lucene.Net.Highlight.QueryScorer score = new Lucene.Net.Highlight.QueryScorer(query);
            Lucene.Net.Highlight.SimpleHTMLFormatter formater = new Lucene.Net.Highlight.SimpleHTMLFormatter("<span class='Highlight'>", "</span>");
            Lucene.Net.Highlight.Highlighter highlighter = new Lucene.Net.Highlight.Highlighter(formater, score);
            result += "<div align='right' style='background-color:#F0F7F9; padding-right:15px' height='30px'><font style='FONT-WEIGHT: bold; FONT-SIZE: 10pt; COLOR: #005482; FONT-FAMILY: arial'>Kết quả tìm thấy : " + hits.Length() + "  </font></div>";
            result += "<div style='padding: 10px 10px 10px 10px;'>";
            for (int i = 0; i < hits.Length(); i++)
            {
                string id = hits.Doc(i).Get("ArticleId");
                string title = hits.Doc(i).Get("ArticleTitle");
                string detail = hits.Doc(i).Get("ArticleDetail");
                Lucene.Net.Analysis.TokenStream ts = (new Lucene.Net.Analysis.Standard.StandardAnalyzer()).TokenStream("ArticleDetail", new System.IO.StringReader(detail));
                result += string.Format("<div align='left'><font style='FONT-WEIGHT: bold; FONT-SIZE: 10pt; COLOR: #5b5b5b; FONT-FAMILY: arial'><a href='/?ArticleId={0}'>{1}</a></font>", id, title);
                result += string.Format("<div align='left'><font style='FONT-SIZE: 9pt' face='Arial' color='#005482'>...{0}...</font></div></div></br>", highlighter.GetBestFragment(ts, detail));
            }
            result += "</div>";
            reader.Close();
            return result;
        }
        protected class SearchArticle
        {
            public string Id { get; set; }
            public string Title { get; set; }
            public object Highligth { get; set; }
        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public string SearchAndPaging(string strQuery, string index)
        {
            string result = string.Empty;
            try
            {
                List<SearchArticle> searchArticleList = new List<SearchArticle>();
                PSCPortal.CMS.ArticleCollection ArticleList = ArticleCollection.GetArticleCollectionPublish();
                string nameSub = Libs.Ultility.GetSubDomain() == string.Empty ? "HomePage" : Libs.Ultility.GetSubDomain();
                SubDomain subDomain = PSCPortal.Engine.SubDomain.GetSubByName(nameSub);
                PageCollection pagesBelongTo = subDomain.GetPagesBelongTo();
                string strId = string.Empty;
                foreach (var page in pagesBelongTo)
                {
                    foreach (var ar in ArticleList.Where(ar => ar.PageId == page.Id))
                    {
                        strId += ar.Id + " OR ";
                    }
                    if (strId.Length > 0)
                        strId = strId.Remove(strId.Length - 3, 3);
                }
                int pageIndex = Int32.Parse(index);
                string strSearch = " ArticleDetail:(" + strQuery + ") AND ArticleId:" + "( " + strId + " )";
                Lucene.Net.Index.IndexReader reader = Lucene.Net.Index.IndexReader.Open(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["IndexingArticle"]));
                Lucene.Net.QueryParsers.QueryParser parser = new Lucene.Net.QueryParsers.QueryParser("ArticleDetail", new Lucene.Net.Analysis.Standard.StandardAnalyzer());
                Lucene.Net.Search.Query query = parser.Parse(strSearch);
                Lucene.Net.Search.IndexSearcher searcher = new Lucene.Net.Search.IndexSearcher(reader);
                Lucene.Net.Search.Hits hits = searcher.Search(query);
                Lucene.Net.Highlight.QueryScorer score = new Lucene.Net.Highlight.QueryScorer(query);
                Lucene.Net.Highlight.SimpleHTMLFormatter formater = new Lucene.Net.Highlight.SimpleHTMLFormatter("<span class='Highlight'>", "</span>");
                Lucene.Net.Highlight.Highlighter highlighter = new Lucene.Net.Highlight.Highlighter(formater, score);
                result += hits.Length() + "_" + "<div class='blog_news'><div class='topic_news_title1'><div class='topic_news_title'><a href='#'>Kết quả tìm thấy: " + hits.Length() + "</a></div></div>";
                result += "<div class='ct_topic_l'><div class='ct_topic_r1'>";
                for (int i = pageIndex * 20 - 20; i < pageIndex * 20 && i < hits.Length(); i++)
                {
                    string detail = hits.Doc(i).Get("ArticleDetail");
                    Lucene.Net.Analysis.TokenStream ts = (new Lucene.Net.Analysis.Standard.StandardAnalyzer()).TokenStream("ArticleDetail", new System.IO.StringReader(detail));
                    SearchArticle searchArticle = new SearchArticle();
                    searchArticle.Id = hits.Doc(i).Get("ArticleId"); ;
                    searchArticle.Title = hits.Doc(i).Get("ArticleTitle");
                    searchArticle.Highligth = highlighter.GetBestFragment(ts, detail);
                    searchArticleList.Add(searchArticle);
                }
                reader.Close();
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                Dictionary<string, object> resultDic = new Dictionary<string, object>();
                resultDic["Count"] = hits.Length();
                resultDic["Data"] = searchArticleList;
                result = serializer.Serialize(resultDic);
            }
            catch (Exception e)
            {
            }
            return result;
        }
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public void SetLanguage(string languageId)
        {
            Session["UICulture"] = languageId;
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public bool AllowWatchTopic(string topicId)
        {
            TopicLogin topicLogin = PSCPortal.CMS.TopicLogin.GetTopicLogin(topicId);
            if (topicLogin == null)
                return true;
            else
            {
                if (Session["UserWatchTopic"] != null && (string)Session["UserWatchTopic"] == topicLogin.Name)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public bool CheckUserWatchTopic()
        {
            if (Session["UserWatchTopic"] != null)
                return true;
            return false;
        }
        [System.Web.Services.WebMethod(EnableSession = true)]
        public bool AllowWatchArticle(string articleId)
        {
            ArticleLogin articleLogin = PSCPortal.CMS.ArticleLogin.GetArticleLogin(articleId);
            if (articleLogin == null)
                return true;
            else
            {
                if (Session["UserWatchArticle"] != null && (string)Session["UserWatchArticle"] == articleLogin.Name)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        [System.Web.Services.WebMethod(EnableSession = true)]
        public bool CheckUserWatchArticle()
        {
            if (Session["UserWatchArticle"] != null)
                return true;
            return false;
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public string GetFormContent()
        {
            Layout layout = LayoutCollection.GetLayOut("4").SingleOrDefault();
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            string s = js.Serialize(layout.NavigationUrl);
            return s;
        }
        [System.Web.Services.WebMethod(EnableSession = true)]
        public string GetArticleByTopic(string DetailTopic, string articleid, string isPreview)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            PSCPortal.CMS.Article article = isPreview != null ? PSCPortal.CMS.Article.GetArticleUnPublish(articleid) : PSCPortal.CMS.Article.GetArticleByDescription(articleid, DetailTopic);
            //if (articleid == null)
            //    return;
            //if (article.Id == Guid.Empty)
            //    return;
            PSCPortal.CMS.ArticleCollection articleCollection;
            PSCPortal.CMS.Topic topic = PSCPortal.CMS.Topic.GetTopicPrimary(article.Id.ToString());

            if (topic != null)
            {
                string chuoi = "";
                chuoi = chuoi + " <a style='color:#666;' href=/?TopicId=" + topic.Id.ToString() + ">" + topic.Name + " </a> ";
                dic.Add("TopicId", topic.Id.ToString());
                dic.Add("TopicName", topic.Name);
                dic.Add("ListTopic", chuoi);
                dic.Add("ArticleCreatedDate", article.CreatedDate);
                dic.Add("ArticleTitle", article.Title);
                dic.Add("ArticleId", article.Id.ToString());
                dic.Add("ArticleDescription", PSCPortal.CMS.Article.GetDescription(article.Id));
                dic.Add("ArticleContent", PSCPortal.CMS.Article.GetContent(article.Id));
                articleCollection = PSCPortal.CMS.ArticleCollection.GetArticleCollectionPublish(topic);
                int index = 0;
                for (; index < articleCollection.Count; index++)
                {
                    if (articleCollection[index].Id.ToString() == articleid)
                        break;
                }

                IEnumerable<PSCPortal.CMS.Article> it = articleCollection.Skip(index + 1).Take(10);
                dic.Add("oldListArticle", it);

                ///////////Commnet//////////////   

                PSCPortal.CMS.ArticleCommentCollection commentList = PSCPortal.CMS.ArticleCommentCollection.GetArticleCommentPublicCollection(article.Id);
                //if (commentList.Count == 0)
                //    pnCommnet.Visible = false;

                IEnumerable<PSCPortal.CMS.ArticleComment> commentDB = commentList.Take(5);
                dic.Add("commentList", commentDB);
                //RadListView1.DataSource = commentDB;
                //RadListView1.DataBind();
                //if (articleCollection.Count == 1)
                //    pnCactinkhac.Visible = false;


            }
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            return js.Serialize(dic);
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public string GetArticleContent(string id)
        {
            string result = Article.GetContent(new Guid(id));
            //System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            //return js.Serialize(result);
            return result;
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public string GetArticleCollection(string topicId, int startIndex, int size)
        {
            List<Article> resultList = ArticleCollection.GetArticleCollection(topicId, startIndex, size);
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Serialize(resultList);
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public string GetOrtherArticles(string articleId)
        {
            List<Article> articleCollection;
            PSCPortal.CMS.Topic topic = PSCPortal.CMS.Topic.GetTopicPrimary(articleId);
            PSCPortal.CMS.Article article = PSCPortal.CMS.Article.GetArticle(articleId);
            int OrtherArticleNumber = int.Parse(System.Configuration.ConfigurationManager.AppSettings["OrtherArticleNumber"]);
            if (topic != null)
            {
                articleCollection = PSCPortal.CMS.ArticleCollection.GetArticleCollectionPublish(topic).ToList();
                int index = 0;
                for (; index < articleCollection.Count; index++)
                {
                    if (articleCollection[index].Id.ToString() == articleId)
                        break;
                }

                articleCollection = articleCollection.Skip(index + 1).Take(OrtherArticleNumber).ToList();
            }
            else
            {
                PSCPortal.CMS.TopicCollection TopicCollection = PSCPortal.CMS.TopicCollection.GetTopicCollectionByArticleId(articleId);

                List<string> listobj = new List<string>();
                foreach (PSCPortal.CMS.Topic item in TopicCollection)
                {
                    if (item.Id != Guid.Empty)
                    {
                        listobj.Add(item.Id.ToString());
                    }
                }
                articleCollection = PSCPortal.CMS.ArticleCollection.GetListArticleOld(article.CreatedDate, listobj).Take(OrtherArticleNumber).ToList();
            }
            //List<Article> resultList = GetArticleCollectionList(topicId, startIndex, size);
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Serialize(articleCollection);
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public string GetListAllVideoClip(int startIndex, int pageSize, string sortExpression)
        {
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            Dictionary<string, object> result = new Dictionary<string, object>();
            VideoClipCollection VideoClipList = VideoClipCollection.GetVideoClipCollection();
            result["data"] = VideoClipList.GetSegment(startIndex*pageSize, pageSize, sortExpression);
            result["total"] = VideoClipList.Count;
            return js.Serialize(result);
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public string GetListAllMusic(int startIndex, int pageSize, string sortExpression)
        {
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            Dictionary<string, object> result = new Dictionary<string, object>();
            MusicCollection MusicList = MusicCollection.GetMusicClipCollection();
            result["data"] = MusicList.GetSegment(startIndex * pageSize, pageSize, sortExpression);
            result["total"] = MusicList.Count;
            return js.Serialize(result);
        }
    }
}
