using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PSCPortal.Framework;
using PSCPortal.CMS;
using PSCPortal.Engine;
using System.Data.Common;
using PSCPortal.Framework.Helpler;

namespace PSCPortal.Systems.CMS
{
    public partial class ArticleSubDomain : PSCDialog
    {
        private static PSCPortal.CMS.ArticleArgs Args
        {
            get
            {
                return DataShare as PSCPortal.CMS.ArticleArgs;
            }
        }
        # region SubDomain
        protected static SubDomainCollection SubDomainList
        {
            get
            {
                if (DataStatic["SubDomainList"] == null)
                {
                    SubDomainCollection list = SubDomainCollection.GetSubDomainCollection();
                    //list.Add(new SubDomain { Id = new Guid("FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF"), Name = "HomePage" });
                    DataStatic["SubDomainList"] = list;
                }
                return DataStatic["SubDomainList"] as SubDomainCollection;
            }
            set
            {
                DataStatic["SubDomainList"] = value;
            }
        }

        public static SubDomainCollection GetSubDomainsBelongTo(Guid articleId)
        {
            PSCPortal.Framework.Database database = new PSCPortal.Framework.Database("PSCPortalConnectionString");
            SubDomainCollection result = new SubDomainCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand(connection);
                #region ArticleId
                DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@ArticleId", articleId);
                command.Parameters.Add(prId);
                #endregion
                command.CommandText = @"SELECT 
	                                    a.[Id],
	                                    [Name],
	                                    [Description],
                                        [PageId] 
                                    FROM 
	                                    dbo.[SubDomain] a
	                                    inner join SubDomainInArticle b on a.Id = b.SubDomainToId 
                                    WHERE 
	                                    ArticleId = @ArticleId";
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    SubDomain item = new SubDomain(reader);
                    result.Add(item);
                }
            }
            return result;
        }
        protected static SubDomainCollection BeforeSubDomainList
        {
            get
            {
                if (DataStatic["BeforeSubDomainList"] == null)
                {
                    DataStatic["BeforeSubDomainList"] = GetSubDomainsBelongTo(Args.Article.Id);
                }
                return DataStatic["BeforeSubDomainList"] as SubDomainCollection;
            }
            set
            {
                DataStatic["BeforeSubDomainList"] = value;
            }
        }
        protected static SubDomainCollection AfterSubDomainList
        {
            get
            {
                return DataStatic["AfterSubDomainList"] as SubDomainCollection;
            }
            set
            {
                DataStatic["AfterSubDomainList"] = value;
            }
        }
        protected static SubDomainCollection DisplaySubDomainList
        {
            get
            {
                SubDomainCollection result = SubDomainList;
                if (DataStatic["DisplaySubDomainList"] == null)
                {
                    foreach (var item in BeforeSubDomainList)
                        result.Single(i => i.Id == item.Id).IsCheck = true;
                    DataStatic["DisplaySubDomainList"] = result;
                }
                return DataStatic["DisplaySubDomainList"] as SubDomainCollection;
            }
            set
            {
                DataStatic["DisplaySubDomainList"] = value;
            }
        }
        [System.Web.Services.WebMethod]
        public static string GetSubDomainList()
        {
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            string results = js.Serialize(DisplaySubDomainList);
            return results;
        }
        public static SubDomainCollection SubDomainListChoice(string[] arrId)
        {
            SubDomainCollection result = new SubDomainCollection();
            foreach (string id in arrId)
            {
                Guid idSubDomain = new Guid(id);
                SubDomain menuMaster = SubDomainList.Where(u => u.Id == idSubDomain).Single();
                result.Add(menuMaster);
            }
            AfterSubDomainList = result;
            return result;
        }
        #endregion
        // goi bai viet ve truong
        public static Article ArticleCopy(Article articleCopy)
        {
            Article article = new Article();
            article.Id = Guid.NewGuid();
            article.Name = articleCopy.Name;
            article.Title = articleCopy.Title;
            article.CreatedDate = articleCopy.CreatedDate;
            article.ModifiedDate = articleCopy.ModifiedDate;
            article.PageId = articleCopy.PageId;
            article.IsPublish = false;
            article.IsVisibleCreateDate = articleCopy.IsVisibleCreateDate;
            article.ArticleTemplate = articleCopy.ArticleTemplate;
            article.IsVisibleComment = articleCopy.IsVisibleComment;
            article.UserAdd = articleCopy.UserAdd;
            article.DocumentPath = articleCopy.DocumentPath;
            article.AlbumPath = articleCopy.AlbumPath;
            article.ArticeCopyNoTopic();
            article.UpdateArticleHang(Args.Article.ArticleHangDate);
            article.UpdateDescription(articleCopy.GetDescription());
            article.UpdateContent(articleCopy.GetContent());
            article.UpdateAvatar(articleCopy.GetAvatar());
            byte[] buffer = articleCopy.GetImage();
            article.UpdateImage(buffer);
            byte[] bufferPortlet = articleCopy.GetImagePortlet();
            article.UpdateImagePortlet(bufferPortlet);

            return article;
        }
        [System.Web.Services.WebMethod]
        public static void Save(string[] arrSubDomainId)
        {
            #region SubDomain
            SubDomainCollection listSubDomainBefore = BeforeSubDomainList;
            SubDomainCollection listSubDomainAfter = SubDomainListChoice(arrSubDomainId);
            IEnumerable<SubDomain> addSubDomainArticle = listSubDomainAfter.Except(listSubDomainBefore);
            IEnumerable<SubDomain> removeSubDomainArticle = listSubDomainBefore.Except(listSubDomainAfter);
            // add list
            Guid subDomainCurrent = SessionHelper.GetSession(SessionKey.SubDomain) == string.Empty ? Guid.Empty : new Guid(SessionHelper.GetSession(SessionKey.SubDomain));
            foreach (var item in addSubDomainArticle)
            {
                Article article = ArticleCopy(Args.Article);
                SubDomainInArticle sdim = new SubDomainInArticle();
                sdim.Id = Guid.NewGuid();
                sdim.SubDomainToId = item.Id;
                sdim.SubDomainFromId = subDomainCurrent;
                sdim.ArticleId = Args.Article.Id;
                sdim.ArticleNewId = article.Id;
                sdim.IsCheck = false;
                sdim.AddDB();
            }
            //remove list
            foreach (var item in removeSubDomainArticle)
            {
                SubDomainInArticle sdim = new SubDomainInArticle();
                sdim.SubDomainFromId = item.Id;
                sdim.ArticleId = Args.Article.Id;
                sdim.RemoveDB();
            }
            #endregion
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DataBind();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            if (!IsPostBack)
            {
                gvSubDomain.MasterTableView.PageSize = DisplaySubDomainList.Count();
            }
        }
    }
}
