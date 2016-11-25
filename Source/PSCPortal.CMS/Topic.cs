using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;
namespace PSCPortal.CMS
{
    [Serializable]
    public class Topic : PSCPortal.Framework.BusinessObjectHierarchical<Topic>
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

        private string _description = string.Empty;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                _description = value;
            }
        }
        private Guid _pageId = Guid.Empty;
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

        private bool _rss;
        public bool Rss
        {
            get
            {
                return _rss;
            }
            set
            {
                _rss = value;
            }
        }
        #endregion

        #region Constructions
        public Topic()
            : base()
        {
        }

        public Topic(DbDataReader reader)
            : base(reader)
        {
        }
        #endregion
        #region Abstract Methods
        protected override void MappingData(System.Data.Common.DbDataReader reader)
        {
            _id = (Guid)reader["TopicId"];
            _name = (string)reader["TopicName"];
            _description = (string)reader["TopicDescription"];
            try
            {
                _pageId = (Guid)reader["PageId"];
            }
            catch { }
        }
        protected override System.Data.Common.DbCommand GetInsertCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region TopicId
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@TopicId", _id);
            command.Parameters.Add(prId);
            #endregion
            #region TopicName
            DbParameter prName = database.GetParameter(System.Data.DbType.String, "@TopicName", _name);
            command.Parameters.Add(prName);
            #endregion
            #region TopicDescription
            DbParameter prDescription = database.GetParameter(System.Data.DbType.String, "@TopicDescription", _description);
            command.Parameters.Add(prDescription);
            #endregion
            #region TopicParent
            DbParameter prParent = database.GetParameter(System.Data.DbType.Guid, "@TopicParent", ((Topic)_parent).Id);
            command.Parameters.Add(prParent);
            #endregion
            #region PageId
            DbParameter prPageId = database.GetParameter(System.Data.DbType.Guid, "@PageId", _pageId);
            command.Parameters.Add(prPageId);
            #endregion

            #region Rss
            DbParameter prRss = database.GetParameter(System.Data.DbType.Boolean, "@Rss", _rss);
            command.Parameters.Add(prRss);
            #endregion

            #region Command Insert Data
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "Topic_Insert";

            #endregion

            return command;
        }

        protected override System.Data.Common.DbCommand GetUpdateCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region TopicId
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@TopicId", _id);
            command.Parameters.Add(prId);
            #endregion
            #region TopicName
            DbParameter prName = database.GetParameter(System.Data.DbType.String, "@TopicName", _name);
            command.Parameters.Add(prName);
            #endregion
            #region TopicDescription
            DbParameter prDescription = database.GetParameter(System.Data.DbType.String, "@TopicDescription", _description);
            command.Parameters.Add(prDescription);
            #endregion
            #region TopicParent
            DbParameter prParent = database.GetParameter(System.Data.DbType.Guid, "@TopicParent", ((Topic)_parent).Id);
            command.Parameters.Add(prParent);
            #endregion
            #region PageId
            DbParameter prPageId = database.GetParameter(System.Data.DbType.Guid, "@PageId", _pageId);
            command.Parameters.Add(prPageId);
            #endregion
            #region Rss
            DbParameter prRss = database.GetParameter(System.Data.DbType.Boolean, "@Rss", _rss);
            command.Parameters.Add(prRss);
            #endregion
            #region Command Update Data
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "Topic_Update";
            #endregion

            return command;
        }

        protected override System.Data.Common.DbCommand GetDeleteCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region Command Delete Data
            #region TopicId
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@TopicId", _id);
            command.Parameters.Add(prId);
            #endregion

            #region Command Delete Data
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "Topic_Delete";
            #endregion

            #endregion

            return command;
        }

        public override string ToString()
        {
            return _name;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(Topic)
                && ((Topic)obj)._id == _id
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
        public void SetContentTemplate(string html)
        {
            Database database = new Database(ConnectionStringName);
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand(connection);
                #region TopicId
                DbParameter prTopicId = database.GetParameter(System.Data.DbType.Guid, "@TopicId", _id);
                command.Parameters.Add(prTopicId);
                #endregion
                #region TopicContentTemplate
                DbParameter prTopicContentTemplate = database.GetParameter(System.Data.DbType.String, "@TopicContentTemplate", html);
                command.Parameters.Add(prTopicContentTemplate);
                #endregion
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "Topic_SetContentTemplate";
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public string GetContentTemplate()
        {
            string result = string.Empty;
            Database database = new Database(ConnectionStringName);
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand(connection);
                #region TopicId
                DbParameter prTopicId = database.GetParameter(System.Data.DbType.Guid, "@TopicId", _id);
                command.Parameters.Add(prTopicId);
                #endregion
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "Topic_GetContentTemplate";
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                    result = (string)reader["TopicContentTemplate"];
            }
            return result;
        }
        public static Topic GetTopicPrimary(string articleId)
        {
            Database database = new Database("PSCPortalConnectionString");
            Topic result = null;
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();
                #region PrArticleId
                DbParameter PrArticleId = database.GetParameter(System.Data.DbType.Guid, "@ArticleId", new Guid(articleId));
                command.Parameters.Add(PrArticleId);
                #endregion

                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "Topic_GetTopicPrimaryOfArticle";
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Topic tp = new Topic(reader);
                    result = tp;
                }
            }
            return result;
        }
        public void ChangeRoot()
        {
            Parent = ((TopicCollection)_tree).TopicRoot;
            Update();
        }

        public static Topic GetTopicByDescription(string topicDescription)
        {
            Database database = new Database("PSCPortalConnectionString");
            Topic result = null;
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();
                #region TopicDescription
                DbParameter prTopicId = database.GetParameter(System.Data.DbType.String, "@TopicDescription", topicDescription);
                command.Parameters.Add(prTopicId);
                #endregion

                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "Topic_GetTopicByDescription";
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Topic tp = new Topic(reader);
                    if (reader["PageId"].ToString() == "")
                        tp._pageId = Guid.Empty;
                    else
                    {
                        tp._pageId = (Guid)reader["PageId"];
                    }
                    result = tp;
                }
            }
            return result;
        }

        public static Topic GetTopic(string topicId)
        {
            Database database = new Database("PSCPortalConnectionString");
            Topic result = null;
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();
                #region TopicId
                DbParameter prTopicId = database.GetParameter(System.Data.DbType.Guid, "@TopicId", new Guid(topicId));
                command.Parameters.Add(prTopicId);
                #endregion

                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "Topic_GetTopicById";
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Topic tp = new Topic(reader);
                    if (reader["PageId"].ToString() == "")
                        tp._pageId = Guid.Empty;
                    else
                    {
                        tp._pageId = (Guid)reader["PageId"];
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("Rss")))
                        tp.Rss = (bool)reader["Rss"];
                    result = tp;
                }
            }
            return result;
        }

    }
}