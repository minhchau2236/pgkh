using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;
using System.Data.SqlClient;

namespace PSCPortal.CMS
{
    [Serializable]
    public class ArticleComment : PSCPortal.Framework.BusinessObject<ArticleComment>
    {
        private int _id;
        public int ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        private Guid _idArticle;
        public Guid IDArticle
        {
            get
            {
                return _idArticle;
            }
            set
            {
                _idArticle = value;
            }
        }

        private string _nameSender = string.Empty;
        public string NameSender
        {
            get
            {
                return _nameSender;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                _nameSender = value;
            }
        }

        private string _emailSender = string.Empty;
        public string EmailSender
        {
            get
            {
                return _emailSender;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                _emailSender = value;
            }
        }

        private string _title = string.Empty;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                _title = value;
            }
        }

        private string _content = string.Empty;
        public string Content
        {
            get
            {
                return _content;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                _content = value;
            }
        }        

        private string _contentReply=string.Empty;
        public string ContentReply
        {
            get
            {
                return _contentReply;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                _contentReply = value;
            }
        }

        private DateTime _feedBackDate;
        public DateTime FeedBackDate
        {
            get
            {
                return _feedBackDate;
            }
            set
            {
                _feedBackDate = value;
            }
        }

        private Boolean _isPublish = false;
        public Boolean IsPublish
        {
            get
            {
                return _isPublish;
            }
            set
            {
                _isPublish = value;
            }
        }

        public string PathImage
        {
            get
            {
                if (_isPublish)
                {
                    return "Images/choduyet.png";
                }
                else
                {
                    return "Images/boduyet.png";
                }
            }
        }

        #region Constructions
        public ArticleComment()
            : base()
        {
        }

        public ArticleComment(DbDataReader reader)
            : base(reader)
        {
        }
        #endregion
        protected override void MappingData(System.Data.Common.DbDataReader reader)
        {
             _id = (int)reader["FeedBackId"];
            _idArticle = (Guid)reader["ArticleId"];
            _nameSender = (string)reader["NameSender"];
            _emailSender = (string)reader["EmailSender"];
            _title = (string)reader["FeedBackTitle"];
            _content = (string)reader["FeedBackContent"];
            _contentReply =(string) reader["FeedBackContentReply"];
            _feedBackDate = (DateTime) reader["FeedBackDate"];
            _isPublish = (bool)reader["IsPublish"];
        }
        protected override System.Data.Common.DbCommand GetInsertCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();            
                       
            #region FeedBackId
            DbParameter prFeedBackId = database.GetParameter(System.Data.DbType.Int32, "@FeedBackID", _id);
            command.Parameters.Add(prFeedBackId);
            #endregion
            #region ArticleId
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@ArticleId", _idArticle);
            command.Parameters.Add(prId);
            #endregion
            #region NameSender
            DbParameter prName = database.GetParameter(System.Data.DbType.String, "@NameSender", _nameSender);
            command.Parameters.Add(prName);
            #endregion
            #region EmailSender
            DbParameter premailName = database.GetParameter(System.Data.DbType.String, "@EmailSender", _emailSender);
            command.Parameters.Add(premailName);
            #endregion
            #region FeedBackTitle
            DbParameter prFeedBackTitle = database.GetParameter(System.Data.DbType.String, "@FeedBackTitle", _title);
            command.Parameters.Add(prFeedBackTitle);
            #endregion
            #region FeedBackContent
            DbParameter prFeedBackContent = database.GetParameter(System.Data.DbType.String, "@FeedBackContent", _content);
            command.Parameters.Add(prFeedBackContent);
            #endregion
            #region FeedBackContentReply
            DbParameter prFeedBackContentReply = database.GetParameter(System.Data.DbType.String, "@FeedBackContentReply", _contentReply);
            command.Parameters.Add(prFeedBackContentReply);
            #endregion
            #region FeedBackDate
            DbParameter prCreatedDate = database.GetParameter(System.Data.DbType.DateTime, "@FeedBackDate", _feedBackDate);
            command.Parameters.Add(prCreatedDate);
            #endregion
            #region IsPublish
            DbParameter prIsPublish = database.GetParameter(System.Data.DbType.Boolean, "@IsPublish", _isPublish);
            command.Parameters.Add(prIsPublish);
            #endregion
            #region Command Insert Data
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = @"INSERT INTO [dbo].[ArticleFeedBack]
                                            (		
                                                ArticleId,
	                                            NameSender,	
	                                            EmailSender,	
	                                            FeedBackTitle,	
	                                            FeedBackContent,	
	                                            FeedBackContentReply,
	                                            FeedBackDate,
	                                            IsPublish	
                                            )
                                            VALUES
                                            (	
                                                @ArticleId,
	                                            @NameSender,	
	                                            @EmailSender,	
	                                            @FeedBackTitle,	
	                                            @FeedBackContent,
	                                            @FeedBackContentReply,
	                                            @FeedBackDate,
	                                            @IsPublish	
                                            )

                                            set @FeedBackID=@@identity
                                            ";
            #endregion
            return command;
        }
        protected override System.Data.Common.DbCommand GetUpdateCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region FeedBackId
            DbParameter prFeedBackId = database.GetParameter(System.Data.DbType.Int32, "@FeedBackID", _id);
            command.Parameters.Add(prFeedBackId);
            #endregion
            #region ArticleId
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@ArticleId", _idArticle);
            command.Parameters.Add(prId);
            #endregion
            #region NameSender
            DbParameter prName = database.GetParameter(System.Data.DbType.String, "@NameSender", _nameSender);
            command.Parameters.Add(prName);
            #endregion
            #region EmailSender
            DbParameter premailName = database.GetParameter(System.Data.DbType.String, "@EmailSender", _emailSender);
            command.Parameters.Add(premailName);
            #endregion
            #region FeedBackTitle
            DbParameter prFeedBackTitle = database.GetParameter(System.Data.DbType.String, "@FeedBackTitle", _title);
            command.Parameters.Add(prFeedBackTitle);
            #endregion
            #region FeedBackContent
            DbParameter prFeedBackContent = database.GetParameter(System.Data.DbType.String, "@FeedBackContent", _content);
            command.Parameters.Add(prFeedBackContent);
            #endregion
            #region FeedBackContentReply
            DbParameter prFeedBackContentReply = database.GetParameter(System.Data.DbType.String, "@FeedBackContentReply", _contentReply);
            command.Parameters.Add(prFeedBackContentReply);
            #endregion
            #region FeedBackDate
            DbParameter prCreatedDate = database.GetParameter(System.Data.DbType.DateTime, "@FeedBackDate", _feedBackDate);
            command.Parameters.Add(prCreatedDate);
            #endregion
            #region IsPublish
            DbParameter prIsPublish = database.GetParameter(System.Data.DbType.Boolean, "@IsPublish", _isPublish);
            command.Parameters.Add(prIsPublish);
            #endregion
            #region Command update Data
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = @"UPDATE [dbo].[ArticleFeedBack]
                                    SET	
                                        ArticleId=@ArticleId,
	                                    NameSender=@NameSender,	
	                                    EmailSender=@EmailSender,	
	                                    FeedBackTitle=@FeedBackTitle,	
	                                    FeedBackContent=@FeedBackContent,
	                                    FeedBackContentReply=@FeedBackContentReply,
	                                    FeedBackDate = @FeedBackDate,
	                                    IsPublish=@IsPublish
                                    WHERE
	                                    FeedBackID=@FeedBackID";
            #endregion
            return command;
        }
        protected override System.Data.Common.DbCommand GetDeleteCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region Command Delete Data

            #region FeedBackId
            DbParameter prFeedBackId = database.GetParameter(System.Data.DbType.Int32, "@FeedBackID", _id);
            command.Parameters.Add(prFeedBackId);
            #endregion

            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = @"DELETE [dbo].[ArticleFeedBack]
                                    WHERE
	                                    FeedBackId=@FeedBackId";
            #endregion

            return command;
        }                            


//        public string GetContent()
//        {
//            return GetContent(_id);
//        }
//        public static string GetContent(int id)
//        {
//            Database database = new Database(ConnectionStringName);
//            string result = string.Empty;
//            using (DbConnection connection = database.GetConnection())
//            {
//                DbCommand command = database.GetCommand();
//                #region FeedBackId
//                DbParameter prId = database.GetParameter(System.Data.DbType.Int32, "@FeedBackId", id);
//                command.Parameters.Add(prId);
//                #endregion

//                command.CommandText = @"select FeedBackContent from ArticleFeedBack where FeedBackId=@FeedBackId";
//                command.CommandType = System.Data.CommandType.Text;

//                command.Connection = connection;
//                connection.Open();
//                DbDataReader reader = command.ExecuteReader();
//                while (reader.Read())
//                {
//                    result = (string)reader["FeedBackContent"];
//                }
//            }
//            return result;
//        }
//        public void UpdateContent(string Content)
//        {
//            Database database = new Database(ConnectionStringName);
//            using (DbConnection connection = database.GetConnection())
//            {
//                DbCommand command = database.GetCommand();
//                #region FeedBackId
//                DbParameter prId = database.GetParameter(System.Data.DbType.Int32, "@FeedBackId", _id);
//                command.Parameters.Add(prId);
//                #endregion
//                #region FeedBackContent
//                DbParameter prContent = database.GetParameter(System.Data.DbType.String, "@FeedBackContent", Content);
//                command.Parameters.Add(prContent);
//                #endregion

//                command.CommandText = @"UPDATE dbo.[ArticleFeedBack] 
//                                    SET [FeedBackContent] = @FeedBackContent 
//                                    where [FeedBackId] = @FeedBackId";
//                command.CommandType = System.Data.CommandType.Text;
//                command.Connection = connection;
//                connection.Open();
//                command.ExecuteNonQuery();
//            }
//        }

//        public string GetContentReplay()
//        {
//            return GetContentReplay(_id);
//        }
//        public static string GetContentReplay(int id)
//        {
//            Database database = new Database(ConnectionStringName);
//            string result = string.Empty;
//            using (DbConnection connection = database.GetConnection())
//            {
//                DbCommand command = database.GetCommand();
//                #region FeedBackId
//                DbParameter prId = database.GetParameter(System.Data.DbType.Int32, "@FeedBackId", id);
//                command.Parameters.Add(prId);
//                #endregion

//                command.CommandText = @"select FeedBackContentReply from ArticleFeedBack where FeedBackId=@FeedBackId";
//                command.CommandType = System.Data.CommandType.Text;

//                command.Connection = connection;
//                connection.Open();
//                DbDataReader reader = command.ExecuteReader();
//                while (reader.Read())
//                {
//                    result = (string)reader["FeedBackContentReply"];
//                }
//            }
//            return result;
//        }
//        public void UpdateContentReplay(string ContentReplay)
//        {
//            Database database = new Database(ConnectionStringName);
//            using (DbConnection connection = database.GetConnection())
//            {
//                DbCommand command = database.GetCommand();
//                #region FeedBackId
//                DbParameter prId = database.GetParameter(System.Data.DbType.Int32, "@FeedBackId", _id);
//                command.Parameters.Add(prId);
//                #endregion
//                #region FeedBackContent
//                DbParameter prContent = database.GetParameter(System.Data.DbType.String, "@FeedBackContentReply", ContentReplay);
//                command.Parameters.Add(prContent);
//                #endregion

//                command.CommandText = @"UPDATE dbo.[ArticleFeedBack] 
//                                    SET [FeedBackContentReply] = @FeedBackContentReply 
//                                    where [FeedBackId] = @FeedBackId";
//                command.CommandType = System.Data.CommandType.Text;
//                command.Connection = connection;
//                connection.Open();
//                command.ExecuteNonQuery();
//            }
//        }

        public void Insert()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSCPortalConnectionString"].ConnectionString))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    connection.Open();
                    command.CommandText = @"INSERT INTO [dbo].[ArticleFeedBack]
                                            (		
                                                ArticleId,
	                                            NameSender,	
	                                            EmailSender,	
	                                            FeedBackTitle,	
	                                            FeedBackContent,	
	                                            FeedBackContentReply,
	                                            FeedBackDate,
	                                            IsPublish	
                                            )
                                            VALUES
                                            (	
                                                @ArticleId,
	                                            @NameSender,	
	                                            @EmailSender,	
	                                            @FeedBackTitle,	
	                                            @FeedBackContent,
	                                            @FeedBackContentReply,
	                                            @FeedBackDate,
	                                            0	
                                            )

                                            set @FeedBackID=@@identity
                                            ";
                    SqlParameter prFeedBackId = command.CreateParameter();
                    prFeedBackId.ParameterName = "@FeedBackID";
                    prFeedBackId.DbType = System.Data.DbType.Int32;
                    prFeedBackId.Direction = System.Data.ParameterDirection.Output;

                    command.Parameters.Add(prFeedBackId); ;
                    command.Parameters.AddWithValue("@ArticleId", _idArticle);
                    command.Parameters.AddWithValue("@NameSender", _nameSender);
                    command.Parameters.AddWithValue("@EmailSender", _emailSender);
                    command.Parameters.AddWithValue("@FeedBackTitle", _title);
                    command.Parameters.AddWithValue("@FeedBackContent", _content);
                    command.Parameters.AddWithValue("@FeedBackContentReply", _contentReply);
                    command.Parameters.AddWithValue("@FeedBackDate", _feedBackDate);
                    command.ExecuteNonQuery();
                    _id = (int)prFeedBackId.Value;
                }
            }
            catch (Exception e)
            {
                string err = e.ToString();
            }
        }



        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(ArticleComment) && ((ArticleComment)obj)._id == _id)
                return true;
            return false;
        }
        public override int GetHashCode()
        {
            return _id.GetHashCode();
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
