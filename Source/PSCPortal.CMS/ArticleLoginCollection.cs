using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;

namespace PSCPortal.CMS
{
    
    [Serializable]
    public class ArticleLoginCollection : PSCPortal.Framework.BusinessObjectCollection<ArticleLoginCollection, ArticleLogin>
    { private Topic _topic;
        public Topic Topic
        {
            get { return _topic; }            
        }
        private ArticleLoginCollection()
            : base()
        { }
        protected override DbCommand GetSelectAllCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            command.CommandText = "select * from ArticleLogin";
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

            command.CommandText = @"SELECT 
	                                        a.[ArticleId],	                                        
	                                        a.[UserName],
                                            a.[Password]
                                        FROM dbo.[ArticleLogin] a
	                                        INNER JOIN dbo.[ArticleBelongTopic] b ON b.ArticleId = a.ArticleId
                                        where  
	                                        b.TopicId = @TopicId";
            command.CommandType = System.Data.CommandType.Text;
            return command;
        }
        public static ArticleLoginCollection GetArticleLoginCollection(Topic topic)
        {
            Database database = new Database("PSCPortalConnectionString");
            ArticleLoginCollection result = new ArticleLoginCollection();
            result._topic = topic;

            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = result.GetSelectAllCommand(topic);
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ArticleLogin item = new ArticleLogin(reader);
                    result.Add(item);
                }
            }
            return result;
        }
        public static ArticleLoginCollection GetArticleLoginCollection()
        {
            Database database = new Database("PSCPortalConnectionString");
            ArticleLoginCollection result = new ArticleLoginCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = result.GetSelectAllCommand();
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ArticleLogin item = new ArticleLogin(reader);
                    result.Add(item);
                }
            }
            return result;
        }
        public static List<CMS.Article> GetArticleUnLoginCollection(Topic topic)
        {
            List<Article> result = new List<Article>();
            List<CMS.Article> listarticleEx = new List<CMS.Article>();
            CMS.ArticleCollection arList = CMS.ArticleCollection.GetArticleCollectionPublish(topic);
            CMS.ArticleLoginCollection ArExList = CMS.ArticleLoginCollection.GetArticleLoginCollection(topic);

            foreach (CMS.ArticleLogin item in ArExList)
            {               
                CMS.Article a = arList.SingleOrDefault(ar => ar.Id == item.Id);
                listarticleEx.Add(a);                
            }

            result = arList.Except(listarticleEx).ToList();
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