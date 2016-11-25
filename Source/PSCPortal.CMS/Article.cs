using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;
using System.Data.SqlClient;
using System.Data;
namespace PSCPortal.CMS
{
    [Serializable]
    public class Article : PSCPortal.Framework.BusinessObject<Article>
    {
        #region Properties
        private Guid _id;
        public Guid Id
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
        private string _userAdd;
        public string UserAdd
        {
            get
            {
                return _userAdd;
            }
            set
            {
                _userAdd = value;
            }
        }
        private string _name = string.Empty;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                _name = value;
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

        private DateTime _createdDate;
        public DateTime CreatedDate
        {
            get
            {
                return _createdDate;
            }
            set
            {
                _createdDate = value;
            }
        }

        private DateTime _modifiedDate;
        public DateTime ModifiedDate
        {
            get
            {
                return _modifiedDate;
            }
            set
            {
                _modifiedDate = value;
            }
        }

        private bool _isPublish;
        public bool IsPublish
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
        private Guid _pageId;
        public Guid PageId
        {
            get
            {
                return _pageId;
            }
            set
            {
                _pageId = value;
            }
        }
        private string _link = string.Empty;
        public string Link
        {
            get
            {
                return _link;
            }
            set
            {
                _link = value;
            }
        }
        private string _linkImgPortlet = string.Empty;
        public string LinkImgPortlet
        {
            get
            {
                return _linkImgPortlet;
            }
            set
            {
                _linkImgPortlet = value;
            }
        }
        public int ViewTime
        {
            get;
            set;
        }
        private bool _isCheck = false;
        public bool IsCheck
        {
            get
            {
                return _isCheck;
            }
            set
            {
                _isCheck = value;
            }
        }
        private string _subDomainFromName = string.Empty;
        public string SubDomainFromName
        {
            get
            {
                return _subDomainFromName;
            }
            set
            {
                _subDomainFromName = value;
            }
        }
        private bool? _isVisibleCreateDate = null;
        public bool? IsVisibleCreateDate
        {
            get
            {
                return _isVisibleCreateDate;
            }
            set
            {
                _isVisibleCreateDate = value;
            }
        }

        private int? _articleTemplate;
        public int? ArticleTemplate
        {
            get
            {
                return _articleTemplate;
            }
            set
            {
                _articleTemplate = value;
            }
        }
        private DateTime? _articleHangDate;
        public DateTime? ArticleHangDate
        {
            get
            {
                return _articleHangDate;
            }
            set
            {
                _articleHangDate = value;
            }
        }
        private Guid _topicId;
        public Guid TopicId
        {
            get
            {
                return _topicId;
            }
            set
            {
                _topicId = value;
            }
        }

        private bool? _isVisibleComment;
        public bool? IsVisibleComment
        {
            get
            {
                return _isVisibleComment;
            }
            set
            {
                _isVisibleComment = value;
            }
        }

        private string _imageUrl = string.Empty;
        public string ImageUrl
        {
            get
            {
                return _imageUrl;
            }
            set
            {
                _imageUrl = value;
            }
        }

        private string _documentPath;
        public string DocumentPath
        {
            get
            {
                return _documentPath;
            }
            set
            {
                _documentPath = value;
            }
        }

        private string _albumPath;
        public string AlbumPath
        {
            get
            {
                return _albumPath;
            }
            set
            {
                _albumPath = value;
            }
        }
        #endregion

        #region Constructions
        public Article()
            : base()
        {
        }

        public Article(DbDataReader reader)
            : base(reader)
        {
        }
        #endregion
        #region Abstract Methods
        protected override void MappingData(System.Data.Common.DbDataReader reader)
        {
            _id = (Guid)reader["ArticleId"];
            _name = (string)reader["ArticleName"];
            _title = (string)reader["ArticleTitle"];
            _createdDate = (DateTime)reader["ArticleCreatedDate"];
            _modifiedDate = (DateTime)reader["ArticleModifiedDate"];
            _isPublish = (bool)reader["ArticleIsPublish"];
            _isVisibleCreateDate = reader["IsVisibleCreateDate"] != DBNull.Value ? (bool?)reader["IsVisibleCreateDate"] : null;
            _articleTemplate = reader["ArticleTemplate"] != DBNull.Value ? (int?)reader["ArticleTemplate"] : null;
            try
            {
                _pageId = (Guid)reader["PageId"];
                _imageUrl = (string)reader["ImageDescription"];
            }
            catch
            {

            }
        }
        protected override System.Data.Common.DbCommand GetInsertCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();

            #region ArticleId
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@ArticleId", _id);
            command.Parameters.Add(prId);
            #endregion

            #region ArticleName
            DbParameter prName = database.GetParameter(System.Data.DbType.String, "@ArticleName", _name);
            command.Parameters.Add(prName);
            #endregion

            #region ArticleTitle
            DbParameter prTitle = database.GetParameter(System.Data.DbType.String, "@ArticleTitle", _title);
            command.Parameters.Add(prTitle);
            #endregion

            #region ArticleCreatedDate
            DbParameter prCreatedDate = database.GetParameter(System.Data.DbType.DateTime, "@ArticleCreatedDate", _createdDate);
            command.Parameters.Add(prCreatedDate);
            #endregion

            #region ArticleModifiedDate
            DbParameter prModifiedDate = database.GetParameter(System.Data.DbType.DateTime, "@ArticleModifiedDate", _modifiedDate);
            command.Parameters.Add(prModifiedDate);
            #endregion

            #region ArticleIsPublish
            DbParameter prIsPublish = database.GetParameter(System.Data.DbType.Boolean, "@ArticleIsPublish", _isPublish);
            command.Parameters.Add(prIsPublish);
            #endregion

            #region PageId
            DbParameter prPageId = database.GetParameter(System.Data.DbType.Guid, "@PageId", _pageId);
            command.Parameters.Add(prPageId);
            #endregion

            #region IsVisibleCreateDate
            DbParameter prIsVisibleCD = database.GetParameter(System.Data.DbType.Boolean, "@IsVisibleCreateDate", _isVisibleCreateDate);
            command.Parameters.Add(prIsVisibleCD);
            #endregion

            #region ArticleTemplate
            DbParameter prArticleTemplate = database.GetParameter(System.Data.DbType.Int32, "@ArticleTemplate", _articleTemplate);
            command.Parameters.Add(prArticleTemplate);
            #endregion

            #region  ArticleComment
            DbParameter prArticleComment = database.GetParameter(System.Data.DbType.Boolean, "@IsVisibleComment", _isVisibleComment);
            command.Parameters.Add(prArticleComment);
            #endregion

            #region  albumPath
            DbParameter prAlbumComment = database.GetParameter(System.Data.DbType.String, "@AlbumPath", _albumPath);
            command.Parameters.Add(prAlbumComment);
            #endregion

            #region  documentPath
            DbParameter prDocumentComment = database.GetParameter(System.Data.DbType.String, "@DocumentPath", _documentPath);
            command.Parameters.Add(prDocumentComment);
            #endregion

            #region TopicId
            DbParameter prTopicId = database.GetParameter(System.Data.DbType.Guid, "@TopicId", ((ArticleCollection)_collection).Topic.Id);
            command.Parameters.Add(prTopicId);
            #endregion

            #region UserAdd
            DbParameter prUserAdd = database.GetParameter(System.Data.DbType.String, "@UserAdd", _userAdd);
            command.Parameters.Add(prUserAdd);
            #endregion

            #region Command Insert Data
            command.CommandText = "Article_Insert";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            #endregion

            return command;
        }

        protected override System.Data.Common.DbCommand GetUpdateCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region ArticleId
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@ArticleId", _id);
            command.Parameters.Add(prId);
            #endregion

            #region ArticleName
            DbParameter prName = database.GetParameter(System.Data.DbType.String, "@ArticleName", _name);
            command.Parameters.Add(prName);
            #endregion

            #region ArticleTitle
            DbParameter prTitle = database.GetParameter(System.Data.DbType.String, "@ArticleTitle", _title);
            command.Parameters.Add(prTitle);
            #endregion

            #region ArticleCreatedDate
            DbParameter prCreatedDate = database.GetParameter(System.Data.DbType.DateTime, "@ArticleCreatedDate", _createdDate);
            command.Parameters.Add(prCreatedDate);
            #endregion

            #region ArticleModifiedDate
            DbParameter prModifiedDate = database.GetParameter(System.Data.DbType.DateTime, "@ArticleModifiedDate", _modifiedDate);
            command.Parameters.Add(prModifiedDate);
            #endregion

            #region ArticleIsPublish
            DbParameter prIsPublish = database.GetParameter(System.Data.DbType.Boolean, "@ArticleIsPublish", _isPublish);
            command.Parameters.Add(prIsPublish);
            #endregion

            #region PageId
            DbParameter prPageId = database.GetParameter(System.Data.DbType.Guid, "@PageId", _pageId);
            command.Parameters.Add(prPageId);
            #endregion

            #region IsVisibleCreateDate
            DbParameter prIsVisibleCD = database.GetParameter(System.Data.DbType.Boolean, "@IsVisibleCreateDate", _isVisibleCreateDate == null ? false : _isVisibleCreateDate);
            command.Parameters.Add(prIsVisibleCD);
            #endregion

            #region ArticleTemplate
            DbParameter prArticleTemplate = database.GetParameter(System.Data.DbType.Int32, "@ArticleTemplate", _articleTemplate == null ? 0 : _articleTemplate);
            command.Parameters.Add(prArticleTemplate);
            #endregion

            #region  ArticleComment
            DbParameter prArticleComment = database.GetParameter(System.Data.DbType.Boolean, "@IsVisibleComment", _isVisibleComment == null ? false : _isVisibleComment);
            command.Parameters.Add(prArticleComment);
            #endregion

            #region UserAdd
            DbParameter prUserAdd = database.GetParameter(System.Data.DbType.String, "@UserAdd", _userAdd);
            command.Parameters.Add(prUserAdd);
            #endregion

            #region  albumPath
            DbParameter prAlbumComment = database.GetParameter(System.Data.DbType.String, "@AlbumPath", _albumPath);
            command.Parameters.Add(prAlbumComment);
            #endregion

            #region  documentPath
            DbParameter prDocumentComment = database.GetParameter(System.Data.DbType.String, "@DocumentPath", _documentPath);
            command.Parameters.Add(prDocumentComment);
            #endregion

            #region Command Update Data
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "Article_Update";
            #endregion

            return command;
        }

        protected override System.Data.Common.DbCommand GetDeleteCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region Command Delete Data

            #region ArticleId
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@ArticleId", _id);
            command.Parameters.Add(prId);
            #endregion

            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "Article_MoveToTrash";
            #endregion

            return command;
        }
        public string GetAvatar()
        {
            return GetAvatar(_id);
        }
        public static string GetAvatar(Guid articleId)
        {
            Database database = new Database(ConnectionStringName);
            string result = string.Empty;
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();

                #region ArticleId
                DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@ArticleId", articleId);
                command.Parameters.Add(prId);
                #endregion


                #region Command Get Avatar
                command.CommandText = "Select [ArticleAvatar] from ArticleDetail  WHERE [ArticleId] = @ArticleId";
                #endregion
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    try
                    {
                        result = (string)reader["ArticleAvatar"];
                    }
                    catch
                    { }
                }
            }
            return result;
        }
        public void UpdateAvatar(string avatar)
        {
            Database database = new Database(ConnectionStringName);
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();
                #region ArticleId
                DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@ArticleId", _id);
                command.Parameters.Add(prId);
                #endregion
                #region ArticleDescription
                DbParameter prDescription = database.GetParameter(System.Data.DbType.String, "@ArticleAvatar", avatar);
                command.Parameters.Add(prDescription);
                #endregion
                #region Command Update Data
                command.CommandText = "UPDATE [ArticleDetail] SET [ArticleAvatar] = @ArticleAvatar   WHERE [ArticleId] = @ArticleId";
                #endregion
                command.Connection = connection;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public string GetDescription()
        {
            return GetDescription(_id);
        }
        public static string GetDescription(Guid articleId)
        {
            Database database = new Database(ConnectionStringName);
            string result = string.Empty;
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();

                #region ArticleId
                DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@ArticleId", articleId);
                command.Parameters.Add(prId);
                #endregion

                command.CommandText = "Article_GetDescription";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    //result = (string)reader["ArticleDescription"];
                    result = (string)reader["Description"];
                }
            }
            return result;
        }

        public static bool CheckVisibleComment(Guid articleId)
        {
            Database database = new Database(ConnectionStringName);
            bool result = false;
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();

                #region ArticleId
                DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@ArticleId", articleId);
                command.Parameters.Add(prId);
                #endregion

                command.CommandText = "select * from Article where ArticleId = @ArticleId";
                command.CommandType = System.Data.CommandType.Text;

                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    //result = (string)reader["ArticleDescription"];
                    result = reader["IsVisibleComment"] != DBNull.Value ? Convert.ToBoolean(reader["IsVisibleComment"]) : false;
                }
            }
            return result;
        }

        public static Article GetArticleAlbum(Guid id)
        {
            Database database = new Database(ConnectionStringName);
            Article result = new Article();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();

                #region ArticleId
                DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@ArticleId", id);
                command.Parameters.Add(prId);
                #endregion

                command.CommandText = "select * from Article where ArticleId = @ArticleId";
                command.CommandType = System.Data.CommandType.Text;

                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    result = new Article(reader);
                    if (!reader.IsDBNull(reader.GetOrdinal("AlbumPath")))
                        result.AlbumPath = (string)reader["AlbumPath"];
                    if (!reader.IsDBNull(reader.GetOrdinal("DocumentPath")))
                        result.DocumentPath = (string)reader["DocumentPath"];                  
                }
            }
            return result;
        }
        public static string GetUser(Guid articleId)
        {
            Database database = new Database(ConnectionStringName);
            string result = string.Empty;
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();

                #region ArticleId
                DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@ArticleId", articleId);
                command.Parameters.Add(prId);
                #endregion

                command.CommandText = "Article_GetUser";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result = reader["UserId"].ToString();
                }
            }
            return result;
        }
        public void UpdateDescription(string Description)
        {
            Database database = new Database(ConnectionStringName);
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();
                #region ArticleId
                DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@ArticleId", _id);
                command.Parameters.Add(prId);
                #endregion

                #region ArticleDescription
                DbParameter prDescription = database.GetParameter(System.Data.DbType.String, "@ArticleDescription", Description);
                command.Parameters.Add(prDescription);
                #endregion

                command.CommandText = "Article_UpdateDescription";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Connection = connection;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public string GetContent()
        {
            return Article.GetContent(_id);
        }
        public static string GetContent(Guid id)
        {
            Database database = new Database(ConnectionStringName);
            string result = string.Empty;
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();
                #region ArticleId
                DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@ArticleId", id);
                command.Parameters.Add(prId);
                #endregion

                command.CommandText = "Article_GetContent";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result = (string)reader["ArticleContent"];
                }
            }
            return result;
        }
        public void UpdateContent(string Content)
        {
            Database database = new Database(ConnectionStringName);
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();
                #region ArticleId
                DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@ArticleId", _id);
                command.Parameters.Add(prId);
                #endregion

                #region ArticleContent
                DbParameter prContent = database.GetParameter(System.Data.DbType.String, "@ArticleContent", Content);
                command.Parameters.Add(prContent);
                #endregion

                command.CommandText = "Article_UpdateContent";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Connection = connection;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public byte[] GetImage()
        {
            return Article.GetImage(_id);
        }


        public static byte[] GetImage(Guid id)
        {
            Database database = new Database(ConnectionStringName);
            byte[] result = null;
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();
                #region ArticleId
                DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@ArticleId", id);
                command.Parameters.Add(prId);
                #endregion

                command.CommandText = @"SELECT [ImageContent] FROM dbo.[ArticleImage] where [ArticleId]=@ArticleId";
                command.CommandType = System.Data.CommandType.Text;

                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();

                if (reader.Read() && !reader.IsDBNull(reader.GetOrdinal("ImageContent")))
                    result = (byte[])reader["ImageContent"];


            }
            return result;
        }

        public byte[] GetImagePortlet()
        {
            return Article.GetImagePortlet(_id);
        }

        public static byte[] GetImagePortlet(Guid id)
        {
            Database database = new Database(ConnectionStringName);
            byte[] result = null;
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();
                #region ArticleId
                DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@ArticleId", id);
                command.Parameters.Add(prId);
                #endregion

                command.CommandText = @"SELECT [ImageContent] FROM dbo.[ArticleImgPortlet] where [ArticleId]=@ArticleId";
                command.CommandType = System.Data.CommandType.Text;

                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();

                if (reader.Read() && !reader.IsDBNull(reader.GetOrdinal("ImageContent")))
                    result = (byte[])reader["ImageContent"];
            }
            return result;
        }


        public void UpdateImage(byte[] buffer)
        {
            if (buffer != null)
            {
                Database database = new Database(ConnectionStringName);
                using (DbConnection connection = database.GetConnection())
                {
                    DbCommand command = database.GetCommand();
                    #region ArticleId
                    DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@ArticleId", _id);
                    command.Parameters.Add(prId);
                    #endregion
                    #region ImageContent
                    DbParameter prContent = database.GetParameter(System.Data.DbType.Binary, "@ImageContent", buffer);
                    command.Parameters.Add(prContent);
                    command.CommandText = "ArticleImage_Update";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Connection = connection;
                    connection.Open();
                    command.ExecuteNonQuery();
                    #endregion
                }
            }
        }
        //public void UpdateImage()
        //{
        //    if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(_link)))
        //    {
        //        byte[] buffer = System.IO.File.ReadAllBytes(_link);
        //        System.IO.File.Delete(_link);
        //        UpdateImage(buffer);
        //    }
        //} 
        public void UpdateImage()
        {
            Database database = new Database(ConnectionStringName);
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();
                #region ArticleId
                DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@ArticleId", _id);
                command.Parameters.Add(prId);
                #endregion

                #region ImageContent
                if (System.IO.File.Exists(_link))
                {
                    byte[] temp = System.IO.File.ReadAllBytes(_link);
                    System.IO.File.Delete(_link);
                    DbParameter prContent = database.GetParameter(System.Data.DbType.Binary, "@ImageContent", temp);
                    command.Parameters.Add(prContent);
                    command.CommandText = "ArticleImage_Update";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Connection = connection;
                    connection.Open();
                    command.ExecuteNonQuery();
                }

                #endregion

            }
        }

        public void DeleteImage()
        {
            Database database = new Database(ConnectionStringName);
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();
                #region ArticleId
                DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@ArticleId", _id);
                command.Parameters.Add(prId);
                #endregion
                command.CommandText = "update ArticleImage set ImageContent = NULL where ArticleId = @ArticleId";
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = connection;
                connection.Open();
                command.ExecuteNonQuery();

            }
        }
        public void UpdateImagePortlet(byte[] buffer)
        {
            if (buffer != null)
            {
                Database database = new Database(ConnectionStringName);
                using (DbConnection connection = database.GetConnection())
                {
                    DbCommand command = database.GetCommand();
                    #region ArticleId
                    DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@ArticleId", _id);
                    command.Parameters.Add(prId);
                    #endregion
                    #region ImageContent
                    DbParameter prContent = database.GetParameter(System.Data.DbType.Binary, "@ImageContent", buffer);
                    command.Parameters.Add(prContent);
                    command.CommandText = "ArticleImagePortlet_Update";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Connection = connection;
                    connection.Open();
                    command.ExecuteNonQuery();
                    #endregion
                }
            }
        }

        public void UpdateImagePortlet()
        {
            Database database = new Database(ConnectionStringName);
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();
                #region ArticleId
                DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@ArticleId", _id);
                command.Parameters.Add(prId);
                #endregion

                #region ImageContent
                if (System.IO.File.Exists(_linkImgPortlet))
                {
                    byte[] temp = System.IO.File.ReadAllBytes(_linkImgPortlet);
                    System.IO.File.Delete(_linkImgPortlet);
                    DbParameter prContent = database.GetParameter(System.Data.DbType.Binary, "@ImageContent", temp);
                    command.Parameters.Add(prContent);
                    command.CommandText = "ArticleImagePortlet_Update";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Connection = connection;
                    connection.Open();
                    command.ExecuteNonQuery();
                }

                #endregion

            }
        }


        //Update 5/5/2014
        public void UpdateArticleHang(DateTime? articleHangDate)
        {
            Database database = new Database(ConnectionStringName);
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();
                #region ArticleId
                DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@ArticleId", _id);
                command.Parameters.Add(prId);
                #endregion

                #region ArticleHangDate
                DbParameter prContent = database.GetParameter(System.Data.DbType.DateTime, "@ExpireDate", articleHangDate);
                command.Parameters.Add(prContent);
                #endregion

                command.CommandText = "ArticleHang_Update";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Connection = connection;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteArticleHang()
        {
            Database database = new Database(ConnectionStringName);
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();
                #region ArticleId
                DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@ArticleId", _id);
                command.Parameters.Add(prId);
                #endregion

                command.CommandText = "delete from ArticleHang where ArticleId = @ArticleId";
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = connection;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void AddTopicBelong(Topic topic)
        {
            Database database = new Database(ConnectionStringName);
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();
                #region ArticleId
                DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@ArticleId", _id);
                command.Parameters.Add(prId);
                #endregion

                #region TopicId
                DbParameter prTopicId = database.GetParameter(System.Data.DbType.Guid, "@TopicId", topic.Id);
                command.Parameters.Add(prTopicId);
                #endregion

                command.CommandText = "Article_AddTopicBelong";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Connection = connection;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public static Article GetArticleUnPublish(string articleid)
        {
            Database database = new Database(ConnectionStringName);
            Article result = new Article();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();
                #region ArticleId
                DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@ArticleId", new Guid(articleid));
                command.Parameters.Add(prId);
                #endregion
                command.CommandText = "Article_GetArticleByIdUnPublish";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result = new Article(reader);
                }
            }
            return result;
        }
        public static Article GetArticle(string articleid)
        {
            Database database = new Database(ConnectionStringName);
            Article result = new Article();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();
                #region ArticleId
                DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@ArticleId", new Guid(articleid));
                command.Parameters.Add(prId);
                #endregion
                command.CommandText = "Article_GetArticleById";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result = new Article(reader);
                }
            }
            return result;
        }

        public static Article GetArticleByDescription(string articleId, string topicId)
        {
            Database database = new Database(ConnectionStringName);
            Article result = new Article();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();
                #region ArticleId
                DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@ArticleId", new Guid(articleId));
                command.Parameters.Add(prId);
                #endregion

                #region ATopicId
                DbParameter prTopic = database.GetParameter(System.Data.DbType.Guid, "@TopicId", new Guid(topicId));
                command.Parameters.Add(prTopic);
                #endregion
                command.CommandText = "GetArticleByDescription";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result = new Article(reader);
                }
            }
            return result;
        }
        public void DeleteTopicBelong(Topic topic)
        {
            Database database = new Database(ConnectionStringName);
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();
                #region ArticleId
                DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@ArticleId", _id);
                command.Parameters.Add(prId);
                #endregion

                #region TopicId
                DbParameter prTopicId = database.GetParameter(System.Data.DbType.Guid, "@TopicId", topic.Id);
                command.Parameters.Add(prTopicId);
                #endregion

                command.CommandText = "Article_DeleteTopicBelong";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Connection = connection;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public List<Topic> GetTopicBelong()
        {
            List<Topic> result = new List<Topic>();
            Database database = new Database(ConnectionStringName);
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand(connection);
                #region ArticleId
                DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@ArticleId", _id);
                command.Parameters.Add(prId);
                #endregion

                command.CommandText = "Article_GetTopicBelong";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new Topic(reader));
                }
            }
            return result;
        }
        public void SetTopicBelongPrimary(Topic topic)
        {
            Database database = new Database(ConnectionStringName);
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand(connection);
                #region ArticleId
                DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@ArticleId", _id);
                command.Parameters.Add(prId);
                #endregion

                #region TopicId
                DbParameter prTopicId = database.GetParameter(System.Data.DbType.Guid, "@TopicId", topic.Id);
                command.Parameters.Add(prTopicId);
                #endregion

                command.CommandText = "Article_SetTopicBelongPrimary";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public Topic GetTopicBelongPrimary()
        {
            Topic result = null;
            Database database = new Database(ConnectionStringName);
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand(connection);
                #region ArticleId
                DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@ArticleId", _id);
                command.Parameters.Add(prId);
                #endregion

                command.CommandText = "Article_GetTopicBelongPrimary";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result = new Topic(reader);
                }
            }
            return result;
        }
        public void IncViewTime()
        {
            Database database = new Database(ConnectionStringName);
            using (DbConnection connection = database.GetConnection())
            {
                connection.Open();
                DbCommand command = database.GetCommand(connection);
                #region ArticleId
                DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@ArticleId", _id);
                command.Parameters.Add(prId);
                #endregion
                command.CommandText = @"DECLARE @ViewTimeCurrent int
                                        SET @ViewTimeCurrent = (select  ViewTime from Article where ArticleId = @ArticleId )
                                        IF(@ViewTimeCurrent is null)
                                            BEGIN
                                                UPDATE Article SET ViewTime = 1 WHERE ArticleId = @ArticleId
                                            END
                                         ELSE 
			                                Begin
			                                    UPDATE Article SET ViewTime = ViewTime + 1 WHERE ArticleId = @ArticleId
			                                END	";
                command.CommandType = System.Data.CommandType.Text;
                command.ExecuteNonQuery();
            }
        }
        public void ArticeCopyNoTopic()
        {

            Database database = new Database(ConnectionStringName);
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand(connection);

                #region ArticleId
                DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@ArticleId", _id);
                command.Parameters.Add(prId);
                #endregion

                #region ArticleName
                DbParameter prName = database.GetParameter(System.Data.DbType.String, "@ArticleName", _name);
                command.Parameters.Add(prName);
                #endregion

                #region ArticleTitle
                DbParameter prTitle = database.GetParameter(System.Data.DbType.String, "@ArticleTitle", _title);
                command.Parameters.Add(prTitle);
                #endregion

                #region ArticleCreatedDate
                DbParameter prCreatedDate = database.GetParameter(System.Data.DbType.DateTime, "@ArticleCreatedDate", _createdDate);
                command.Parameters.Add(prCreatedDate);
                #endregion

                #region ArticleModifiedDate
                DbParameter prModifiedDate = database.GetParameter(System.Data.DbType.DateTime, "@ArticleModifiedDate", _modifiedDate);
                command.Parameters.Add(prModifiedDate);
                #endregion

                #region ArticleIsPublish
                DbParameter prIsPublish = database.GetParameter(System.Data.DbType.Boolean, "@ArticleIsPublish", _isPublish);
                command.Parameters.Add(prIsPublish);
                #endregion

                #region PageId
                DbParameter prPageId = database.GetParameter(System.Data.DbType.Guid, "@PageId", _pageId);
                command.Parameters.Add(prPageId);
                #endregion

                #region IsVisibleCreateDate
                DbParameter prIsVisibleCD = database.GetParameter(System.Data.DbType.Boolean, "@IsVisibleCreateDate", _isVisibleCreateDate);
                command.Parameters.Add(prIsVisibleCD);
                #endregion

                #region ArticleTemplate
                DbParameter prArticleTemplate = database.GetParameter(System.Data.DbType.Int32, "@ArticleTemplate", _articleTemplate == null ? 0 : _articleTemplate);
                command.Parameters.Add(prArticleTemplate);
                #endregion

                #region  ArticleComment
                DbParameter prArticleComment = database.GetParameter(System.Data.DbType.Boolean, "@IsVisibleComment", _isVisibleComment == null ? false : _isVisibleComment);
                command.Parameters.Add(prArticleComment);
                #endregion

                #region UserAdd
                DbParameter prUserAdd = database.GetParameter(System.Data.DbType.String, "@UserAdd", _userAdd);
                command.Parameters.Add(prUserAdd);
                #endregion

                #region DocumentPath
                DbParameter prDocumentPath = database.GetParameter(System.Data.DbType.String, "@DocumentPath", _documentPath);
                command.Parameters.Add(prDocumentPath);
                #endregion

                #region AlbumPath
                DbParameter prAlbumPath = database.GetParameter(System.Data.DbType.String, "@AlbumPath", _albumPath);
                command.Parameters.Add(prAlbumPath);
                #endregion
                connection.Open();

                #region Command Insert Data
                command.CommandText = @"INSERT INTO dbo.[Article] 
                                        (
	                                        [ArticleId],
	                                        [ArticleName],
	                                        [ArticleTitle],
	                                        [ArticleCreatedDate],
	                                        [ArticleModifiedDate],
	                                        [ArticleIsPublish], 
	                                        [PageId],
                                            [IsVisibleCreateDate],
                                            [ArticleTemplate],
                                            [IsVisibleComment],
                                            [UserAdd],
                                            [DocumentPath],
                                            [AlbumPath]
                                        ) 
                                        VALUES 
                                        (
	                                        @ArticleId,
	                                        @ArticleName,
	                                        @ArticleTitle,
	                                        @ArticleCreatedDate,
	                                        @ArticleModifiedDate,
	                                        @ArticleIsPublish, 
	                                        @PageId,
                                            @IsVisibleCreateDate,
                                            @ArticleTemplate,
                                            @IsVisibleComment,
                                            @UserAdd,
                                            @DocumentPath,
                                            @AlbumPath            
                                        )

                                        INSERT INTO dbo.[ArticleDetail]
                                        (
	                                        [ArticleId],
	                                        [ArticleDescription], 
	                                        [ArticleContent] 
                                        ) 
                                        VALUES 
                                        (
	                                        @ArticleId, 
	                                        '',	
	                                        ''
                                        )
                                        INSERT INTO dbo.[ArticleImage]
                                        (
	                                        [ArticleId]
                                        ) 
                                        VALUES 
                                        (
	                                        @ArticleId
                                        )";
                command.CommandType = System.Data.CommandType.Text;
                #endregion

                command.ExecuteNonQuery();
            }
        }
        public void UpdatePageByArticleSend()
        {

            Database database = new Database(ConnectionStringName);
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand(connection);

                #region ArticleId
                DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@ArticleId", _id);
                command.Parameters.Add(prId);
                #endregion

                #region PageId
                DbParameter prPageId = database.GetParameter(System.Data.DbType.Guid, "@PageId", _pageId);
                command.Parameters.Add(prPageId);
                #endregion

                connection.Open();

                #region Command Insert Data
                command.CommandText = @"UPDATE Article SET PageId = @PageId WHERE ArticleId = @ArticleId";
                command.CommandType = System.Data.CommandType.Text;
                #endregion

                command.ExecuteNonQuery();
            }
        }
        public DateTime? GetArticleHang()
        {
            return GetArticleHang(_id);
        }
        public static DateTime? GetArticleHang(Guid articleId)
        {
            Database database = new Database(ConnectionStringName);
            DateTime? result = null;
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();

                #region ArticleId
                DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@ArticleId", articleId);
                command.Parameters.Add(prId);
                #endregion

                command.CommandText = "Select * from ArticleHang where ArticleId = @ArticleId";
                command.CommandType = System.Data.CommandType.Text;

                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    if (!reader.IsDBNull(reader.GetOrdinal("ExpireDate")))
                        result = (DateTime)reader["ExpireDate"];
                }
            }
            return result;
        }

        public static bool CheckImage(Guid id)
        {
            byte[] _buffer = null;
            _buffer = PSCPortal.CMS.Article.GetImage(id);
            if (_buffer == null)
                return false;
            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(Article)
                && ((Article)obj)._id == _id
               )
                return true;
            return false;
        }
        public override int GetHashCode()
        {
            int hashCode = 0;
            hashCode += _id.GetHashCode();
            return hashCode;
        }
        protected static new string ConnectionStringName
        {
            get
            {
                return "PSCPortalConnectionString";
            }
        }
        #endregion
    }
}