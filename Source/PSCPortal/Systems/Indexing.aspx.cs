using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PSCPortal.Framework;
using PSCPortal.CMS;

namespace PSCPortal.Systems
{
    public partial class Indexing : PSCPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [System.Web.Services.WebMethod]
        public static void IndexingArticle()
        {
            ArticleCollection articleList = ArticleCollection.GetArticleCollection();
            Lucene.Net.Index.IndexWriter lwriter = new Lucene.Net.Index.IndexWriter(System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["IndexingArticle"]), new Lucene.Net.Analysis.Standard.StandardAnalyzer(), true);
            lwriter.Close();
            foreach (Article item in articleList)
                Libs.Ultility.IndexingArticle(item);
        }
    }
}
