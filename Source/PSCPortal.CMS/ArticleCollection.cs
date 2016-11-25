using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PSCPortal.Framework;
using System.Data.Common;
using System.Data;
namespace PSCPortal.CMS
{
    [Serializable]
    public class ArticleCollection : PSCPortal.Framework.BusinessObjectCollection<ArticleCollection, Article>
    {
        private Topic _topic;
        public Topic Topic
        {
            get
            {
                return _topic;
            }
            set
            {
                _topic = value;
            }
        }

        public ArticleCollection()
            : base()
        {
        }
        protected override DbCommand GetSelectAllCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            command.CommandText = "Article_GetSelectAll";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            return command;
        }
        protected DbCommand GetSelectAllCommandPublic()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();

            command.CommandText = @"SELECT *
                                    FROM 
	                                    dbo.[Article]                                 
                                    where 
	                                    ArticleIsPublish=1
                                    ORDER BY 
	                                    ArticleModifiedDate DESC";
            command.CommandType = System.Data.CommandType.Text;
            return command;
        }
        protected DbCommand GetSelectAllCommand(Topic topic)
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();

            #region TopicId
            DbParameter prTopicId = database.GetParameter(System.Data.DbType.Guid, "@TopicId", topic.Id);
            command.Parameters.Add(prTopicId);
            #endregion

            command.CommandText = "Article_GetSelectAllByTopicId";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            return command;
        }

        protected DbCommand GetSelectAllCommandPublic(Topic topic)
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region TopicId
            DbParameter prTopicId = database.GetParameter(System.Data.DbType.Guid, "@TopicId", topic.Id);
            command.Parameters.Add(prTopicId);
            #endregion
            command.CommandText = "Article_GetPublishedArticlesByTopicId";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            return command;
        }

        protected DbCommand GetSelectAllCommandPublicHang(Topic topic)
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region TopicId
            DbParameter prTopicId = database.GetParameter(System.Data.DbType.Guid, "@TopicId", topic.Id);
            command.Parameters.Add(prTopicId);
            #endregion
            command.CommandText = "Article_GetPublishedArticlesByTopicIdHang";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            return command;
        }
        protected DbCommand GetSelectAllCommandUnPublic(Topic topic)
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region TopicId
            DbParameter prTopicId = database.GetParameter(System.Data.DbType.Guid, "@TopicId", topic.Id);
            command.Parameters.Add(prTopicId);
            #endregion
            command.CommandText = @"SELECT * FROM dbo.[Article] a
	                                INNER JOIN dbo.[ArticleBelongTopic] b ON b.ArticleId = a.ArticleId where b.TopicId=@TopicId and a.ArticleIsPublish=0 
                                ORDER BY 
	                                    ArticleModifiedDate DESC";
            command.CommandType = System.Data.CommandType.Text;
            return command;
        }
        protected DbCommand GetSelectAllCommandPublicCommentNew(Topic topic)
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region TopicId
            DbParameter prTopicId = database.GetParameter(System.Data.DbType.Guid, "@TopicId", topic.Id);
            command.Parameters.Add(prTopicId);
            #endregion
            command.CommandText = @"SELECT * FROM dbo.[Article] a, dbo.[ArticleBelongTopic] c
                                    where a.ArticleId in (select ArticleId from ArticleFeedBack where IsPublish=0 group by ArticleId) and a.ArticleId=c.ArticleId and a.ArticleIsPublish=1 
                                    and c.TopicId=@TopicId
                                    order by a.ArticleModifiedDate desc";
            command.CommandType = System.Data.CommandType.Text;
            return command;
        }
        public static ArticleCollection GetArticleCollectionUnPublish(Topic topic)
        {
            Database database = new Database("PSCPortalConnectionString");
            ArticleCollection result = new ArticleCollection();

            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = result.GetSelectAllCommandUnPublic(topic);
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Article item = new Article(reader);
                    result.Add(item);
                }
            }
            return result;
        }
        public static ArticleCollection GetArticleCollection(Topic topic)
        {
            Database database = new Database("PSCPortalConnectionString");
            ArticleCollection result = new ArticleCollection();
            result._topic = topic;
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = result.GetSelectAllCommand(topic);
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Article item = new Article(reader);
                    if (!reader.IsDBNull(reader.GetOrdinal("UserAdd")))
                        item.UserAdd = reader["UserAdd"].ToString();
                    if (!reader.IsDBNull(reader.GetOrdinal("IsVisibleComment")))
                        item.IsVisibleComment = (bool)reader["IsVisibleComment"];
                    result.Add(item);
                }
            }
            return result;
        }
        public static ArticleCollection GetArticleCollection()
        {
            Database database = new Database("PSCPortalConnectionString");
            ArticleCollection result = new ArticleCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = result.GetSelectAllCommand();
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Article item = new Article(reader);
                    result.Add(item);
                }
            }
            return result;
        }

        public static List<Article> GetArticleCollection(string topicId, int startIndex, int size)
        {
            Topic topic = PSCPortal.CMS.TopicCollection.GetTopic(topicId);
            ArticleCollection arts = (ArticleCollection)PSCPortal.CMS.ArticleCollection.GetArticleCollectionPublish(topic);
            List<Article> articleCollection = arts.Skip(startIndex).Take(size).ToList();
            return articleCollection;
        }

        public static ArticleCollection GetArticleCollectionPublish()
        {
            Database database = new Database("PSCPortalConnectionString");
            ArticleCollection result = new ArticleCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = result.GetSelectAllCommandPublic();
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Article item = new Article(reader);
                    result.Add(item);
                }
            }
            return result;
        }
        public static ArticleCollection GetArticleCollectionPublish(Topic topic)
        {
            Database database = new Database("PSCPortalConnectionString");
            ArticleCollection result = new ArticleCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = result.GetSelectAllCommandPublic(topic);
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Article item = new Article(reader);
                    if (!reader.IsDBNull(reader.GetOrdinal("AlbumPath")))
                        item.AlbumPath = (string)reader["AlbumPath"];
                    if (!reader.IsDBNull(reader.GetOrdinal("DocumentPath")))
                        item.DocumentPath = (string)reader["DocumentPath"];
                    result.Add(item);
                }
            }
            return result;
        }

        public static ArticleCollection GetArticleCollectionPublishHang(Topic topic)
        {
            Database database = new Database("PSCPortalConnectionString");
            ArticleCollection result = new ArticleCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = result.GetSelectAllCommandPublicHang(topic);
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Article item = new Article(reader);
                    result.Add(item);
                }
            }
            return result;
        }
        public static ArticleCollection GetArticleCollectionPublishCommentNew(Topic topic)
        {
            Database database = new Database("PSCPortalConnectionString");
            ArticleCollection result = new ArticleCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = result.GetSelectAllCommandPublicCommentNew(topic);
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Article item = new Article(reader);
                    result.Add(item);
                }
            }
            return result;
        }

        protected DbCommand GetSelectAllTrashCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            command.CommandText = "Article_GetAllInTrash";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            return command;
        }
        public static ArticleCollection GetArticleTrashCollection()
        {
            Database database = new Database("PSCPortalConnectionString");
            ArticleCollection result = new ArticleCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = result.GetSelectAllTrashCommand();
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Article item = new Article(reader);
                    result.Add(item);
                }
            }
            return result;
        }
        public static ArticleCollection GetListArticleOld(DateTime date, List<string> listobj)
        {
            Database database = new Database("PSCPortalConnectionString");
            ArticleCollection result = new ArticleCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();

                #region ArticleCreatedDate
                DbParameter prCreatedDate = database.GetParameter(System.Data.DbType.DateTime, "@ArticleCreatedDate", date);
                command.Parameters.Add(prCreatedDate);
                #endregion
                string chuoi = " ";
                for (int i = 0; i < listobj.Count; i++)
                {
                    if (i == 0)
                        chuoi = chuoi + "  at.topicid='" + listobj[i] + "'";
                    else
                        chuoi = chuoi + " or  at.topicid='" + listobj[i] + "'";
                }
                chuoi = chuoi + "  ";
                command.CommandText = "select distinct top 5 a.[ArticleId],a.[ArticleName],a.[ArticleTitle],a.[ArticleCreatedDate],a.[ArticleModifiedDate],a.[ArticleIsPublish],a.[IsVisibleCreateDate],a.[ArticleTemplate]  " +
                                      " from article a, articlebelongtopic at " +
                                      " where [ArticleIsPublish]= 1  and  [ArticleCreatedDate] < @ArticleCreatedDate and a.articleid = at.articleid and ( " + chuoi + " ) " +
                                      " order by [ArticleModifiedDate] desc ";
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Article item = new Article(reader);
                    result.Add(item);
                }
            }
            return result;
        }
        public static void DeleteArticleFromTrash(string articleId)
        {
            Database database = new Database(ConnectionStringName);
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand(connection);
                #region ArticleId
                DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@ArticleId", new Guid(articleId));
                command.Parameters.Add(prId);
                #endregion
                command.CommandText = "Article_DeleteFromDatabase";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public static ArticleCollection GetArticlesNoBelongTopicPrimary()
        {
            Database database = new Database("PSCPortalConnectionString");
            ArticleCollection result = new ArticleCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();
                command.CommandText = "Article_GetArticlesNoBelongTopicPrimary";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Article item = new Article(reader);
                    result.Add(item);
                }
            }
            return result;
        }

        protected DbCommand GetSelectArticleViewTimeCollectionPublish()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            command.CommandText = @"SELECT ArticleId, ArticleName, ArticleTitle, ArticleCreatedDate, ViewTime, PageId  
                                        FROM Article WHERE ArticleIsPublish = 1 ORDER BY ViewTime DESC ";
            command.CommandType = System.Data.CommandType.Text;
            return command;
        }
        public static ArticleCollection GetArticleViewTimeCollectionPublish()
        {
            Database database = new Database("PSCPortalConnectionString");
            ArticleCollection result = new ArticleCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = result.GetSelectArticleViewTimeCollectionPublish();
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Article item = new Article();
                    if (!reader.IsDBNull(reader.GetOrdinal("ArticleId")))
                        item.Id = new Guid(reader["ArticleId"].ToString());
                    if (!reader.IsDBNull(reader.GetOrdinal("ArticleName")))
                        item.Name = (string)reader["ArticleName"].ToString();
                    if (!reader.IsDBNull(reader.GetOrdinal("ArticleTitle")))
                        item.Title = (string)reader["ArticleTitle"].ToString();
                    if (!reader.IsDBNull(reader.GetOrdinal("ArticleCreatedDate")))
                        item.CreatedDate = (DateTime)reader["ArticleCreatedDate"];
                    if (!reader.IsDBNull(reader.GetOrdinal("ViewTime")))
                        item.ViewTime = (int)reader["ViewTime"];
                    if (!reader.IsDBNull(reader.GetOrdinal("PageId")))
                        item.PageId = (Guid)reader["PageId"];
                    result.Add(item);
                }
            }
            return result;
        }

        protected DbCommand GetSelectAllCommandPublicCommentNew()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();

            command.CommandText = @"SELECT * FROM dbo.[Article] a
                                    where a.ArticleId in (select ArticleId from ArticleFeedBack where IsPublish=0 group by ArticleId) and a.ArticleIsPublish=1                                     
                                    order by a.ArticleModifiedDate desc";
            command.CommandType = System.Data.CommandType.Text;
            return command;
        }


        public static ArticleCollection GetArticleCollectionPublishCommentNew()
        {
            Database database = new Database("PSCPortalConnectionString");
            ArticleCollection result = new ArticleCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = result.GetSelectAllCommandPublicCommentNew();
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Article item = new Article(reader);
                    result.Add(item);
                }
            }
            return result;
        }
        public static ArticleCollection GetListArticleCommentByPageId(string pageId)
        {
            Database database = new Database(ConnectionStringName);
            ArticleCollection result = new ArticleCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand(connection);
                #region PageId
                DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@PageId", new Guid(pageId));
                command.Parameters.Add(prId);
                #endregion
                command.CommandText = @"SELECT * FROM dbo.[Article] a
                                    where a.ArticleId in (select ArticleId from ArticleFeedBack where IsPublish=0 group by ArticleId) and a.ArticleIsPublish=1 and a.PageId = @PageId                                    
                                    order by a.ArticleModifiedDate desc";
                command.CommandType = System.Data.CommandType.Text;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Article item = new Article(reader);
                    result.Add(item);
                }
            }
            return result;
        }
        public static ArticleCollection GetArticleBySearch(string articleTitle)
        {
            Database database = new Database(ConnectionStringName);
            ArticleCollection result = new ArticleCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand(connection);
                #region ArticleTitle
                DbParameter prId = database.GetParameter(System.Data.DbType.String, "@ArticleTitle", articleTitle);
                command.Parameters.Add(prId);
                #endregion

                command.CommandText = "Article_SearchAll";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Article item = new Article(reader);
                    if (!reader.IsDBNull(reader.GetOrdinal("UserAdd")))
                        item.UserAdd = reader["UserAdd"].ToString();
                    result.Add(item);
                }
            }
            return result;
        }
        protected static new string ConnectionStringName
        {
            get
            {
                return "PSCPortalConnectionString";
            }
        }
    }
}