using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Common;
using PSCPortal.CMS;
using PSCPortal.Framework;
using PSCPortal.Engine;
using PSCPortal.Framework.Helpler;
using PSCPortal.Libs;

namespace PSCPortal.Systems.CMS
{
    public partial class ArticleViewTimeReport : PSCPortal.Framework.PSCPage
    {
        //05072012
        protected static PSCPortal.CMS.ArticleCollection ArticleList
        {
            get
            {
                if (DataStatic["ArticleList"] == null)
                    DataStatic["ArticleList"] = ArticleCollection.GetArticleViewTimeCollectionPublish();
                return DataStatic["ArticleList"] as PSCPortal.CMS.ArticleCollection;
            }
            set
            {
                DataStatic["ArticleList"] = value;
            }
        }
        protected static List<Article> DisplayArticleList
        {
            get
            {
                return DataStatic["DisplayArticleList"] as List<Article>;
            }
            set
            {
                DataStatic["DisplayArticleList"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [System.Web.Services.WebMethod]
        public static object Search(string tungay, string denngay, int startIndex, int maximumRows, string sortExpressions)
        {
            Guid subId = SessionHelper.GetSession(SessionKey.SubDomain) == string.Empty ? Guid.Empty : new Guid(SessionHelper.GetSession(SessionKey.SubDomain));
            if (subId == Guid.Empty)
            {
                DisplayArticleList = ArticleList.ToList();
            }
            else
            {
                DisplayArticleList = new List<Article>();
                PSCPortal.Engine.SubDomain subDomain = new PSCPortal.Engine.SubDomain { Id = subId };
                PageCollection listPage = subDomain.GetPagesBelongTo();
                foreach (var item in listPage)
                {
                    foreach (var article in ArticleList.Where(ar => ar.PageId == item.Id))
                    {
                        DisplayArticleList.Add(article);
                    }
                }
            }
            if (tungay != string.Empty && denngay != string.Empty)
            {
                IFormatProvider provider = new System.Globalization.CultureInfo("en-CA", true);
                DateTime startDate = DateTime.Parse(tungay, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                DateTime endDate = DateTime.Parse(denngay, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                DisplayArticleList = DisplayArticleList.Where(ar => ar.CreatedDate >= startDate && ar.CreatedDate <= endDate).ToList<Article>();
            }
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                result["Data"] = IEnumerableExtentionMethods.GetSegmentList(DisplayArticleList, startIndex, maximumRows, sortExpressions);
                result["Count"] = DisplayArticleList.Count();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        [System.Web.Services.WebMethod]
        public static object ArticleLoadCommand(int startIndex, int maximumRows, string sortExpressions)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            result["Data"] = IEnumerableExtentionMethods.GetSegmentList(DisplayArticleList, startIndex, maximumRows, sortExpressions);
            result["Count"] = DisplayArticleList.Count();
            return result;
        }
    }
}