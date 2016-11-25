using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PSCPortal.Framework;
using System.Data.Common;
namespace PSCPortal.CMS
{
     [Serializable]
    public class ArticleCommentCollection : PSCPortal.Framework.BusinessObjectCollection<ArticleCommentCollection, ArticleComment>
    {
         public ArticleCommentCollection()
            : base()
        {
        }

         protected override DbCommand GetSelectAllCommand()
         {
             Database database = new Database(ConnectionStringName);
             DbCommand command = database.GetCommand();
             command.CommandText = "SELECT * from ArticleFeedBack";
             command.CommandType = System.Data.CommandType.Text;
             return command;
         }
        

         protected DbCommand GetSelectAllCommandPublic()
         {
             Database database = new Database(ConnectionStringName);
             DbCommand command = database.GetCommand();
             command.CommandText = "SELECT * from ArticleFeedBack where IsPublish=1";
             command.CommandType = System.Data.CommandType.Text;
             return command;
         }
         
         
         protected DbCommand GetSelectAllCommandPublic(Guid articleId)
         {
             Database database = new Database(ConnectionStringName);
             DbCommand command = database.GetCommand();
             #region ArticleId
             DbParameter prArticleId = database.GetParameter(System.Data.DbType.Guid, "@ArticleId", articleId);
             command.Parameters.Add(prArticleId);
             #endregion
             command.CommandText = @"	SELECT *
	                                        from ArticleFeedBack 
	                                        where IsPublish=1 and ArticleId=@ArticleId
                                        ";
             command.CommandType = System.Data.CommandType.Text;
             return command;
         }
         
         protected DbCommand GetSelectAllCommand(Guid articleId)
         {
             Database database = new Database(ConnectionStringName);
             DbCommand command = database.GetCommand();
             #region ArticleId
             DbParameter prArticleId = database.GetParameter(System.Data.DbType.Guid, "@ArticleId", articleId);
             command.Parameters.Add(prArticleId);
             #endregion
             command.CommandText = @"	SELECT *
	                                        from ArticleFeedBack 
	                                        where ArticleId=@ArticleId
                                        ";
             command.CommandType = System.Data.CommandType.Text;
             return command;
         }

         protected DbCommand GetSelectAllCommandUnPublic()
         {
             Database database = new Database(ConnectionStringName);
             DbCommand command = database.GetCommand();
             command.CommandText = @"SELECT a.[ArticleId],a.[ArticleTitle],b.[FeedBackId],b.EmailSender, b.[IsPublish],
                                b.FeedBackContent,b.FeedBackContentReply,b.FeedBackDate,b.FeedBackTitle,b.NameSender
                                from  dbo.[Article] a, dbo.[ArticleFeedBack] b 
                        where b.[ArticleId]=a.[ArticleId] and b.[IsPublish]=0 GROUP BY a.[ArticleId],a.[ArticleTitle],b.[FeedBackId], b.[IsPublish],b.EmailSender,b.FeedBackContent,b.FeedBackContentReply,b.FeedBackDate,b.FeedBackTitle,b.NameSender";
             command.CommandType = System.Data.CommandType.Text;
             return command;
         }
         public static ArticleCommentCollection GetArticleCommentUnPublicCollection()
         {
             Database database = new Database("PSCPortalConnectionString");
             ArticleCommentCollection result = new ArticleCommentCollection();
             using (DbConnection connection = database.GetConnection())
             {
                 DbCommand command = result.GetSelectAllCommandUnPublic();
                 command.Connection = connection;
                 connection.Open();
                 DbDataReader reader = command.ExecuteReader();
                 while (reader.Read())
                 {
                     ArticleComment item = new ArticleComment(reader);
                     result.Add(item);
                 }
             }
             return result;
         }

         public static ArticleCommentCollection GetArticleCommentPublicCollection()
         {
             Database database = new Database("PSCPortalConnectionString");
             ArticleCommentCollection result = new ArticleCommentCollection();
             using (DbConnection connection = database.GetConnection())
             {
                 DbCommand command = result.GetSelectAllCommandPublic();
                 command.Connection = connection;
                 connection.Open();
                 DbDataReader reader = command.ExecuteReader();
                 while (reader.Read())
                 {
                     ArticleComment item = new ArticleComment(reader);
                     result.Add(item);
                 }
             }
             return result;
         }
         public static ArticleCommentCollection GetArticleCommentPublicCollection(Guid articleId)
         {
             Database database = new Database("PSCPortalConnectionString");
             ArticleCommentCollection result = new ArticleCommentCollection();
             using (DbConnection connection = database.GetConnection())
             {
                 DbCommand command = result.GetSelectAllCommandPublic(articleId);
                 command.Connection = connection;
                 connection.Open();
                 DbDataReader reader = command.ExecuteReader();
                 while (reader.Read())
                 {
                     ArticleComment item = new ArticleComment(reader);
                     result.Add(item);
                 }
             }
             return result;
         }

         public static ArticleCommentCollection GetArticleCommentCollection(Guid articleId)
         {
             Database database = new Database("PSCPortalConnectionString");
             ArticleCommentCollection result = new ArticleCommentCollection();
             using (DbConnection connection = database.GetConnection())
             {
                 DbCommand command = result.GetSelectAllCommand(articleId);
                 command.Connection = connection;
                 connection.Open();
                 DbDataReader reader = command.ExecuteReader();
                 while (reader.Read())
                 {
                     ArticleComment item = new ArticleComment(reader);
                     result.Add(item);
                 }
             }
             return result;
         }
         public static ArticleCommentCollection GetListArticleCommentByPageId(string pageId)
         {
             Database database = new Database(ConnectionStringName);
             ArticleCommentCollection result = new ArticleCommentCollection();
             using (DbConnection connection = database.GetConnection())
             {
                 DbCommand command = database.GetCommand(connection);
                 #region PageId
                 DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@PageId", new Guid(pageId));
                 command.Parameters.Add(prId);
                 #endregion
                 command.CommandText = @"SELECT *
                                    FROM 
	                                    dbo.[Article] a, dbo.[ArticleFeedBack] b
                                    WHERE 
	                                    a.[PageId] = @PageId 
	                                    AND a.[ArticleIspublish] = 1
                                        AND b.IsPublish=0 
                                        AND b.[ArticleId]=a.[ArticleId]
                                        ";
                 command.CommandType = System.Data.CommandType.Text;
                 connection.Open();
                 DbDataReader reader = command.ExecuteReader();
                 while (reader.Read())
                 {
                     ArticleComment item = new ArticleComment(reader);
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
