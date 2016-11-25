using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;
namespace PSCPortal.Engine
{
    [Serializable]
    public class SubDomainInTopic : PSCPortal.Framework.BusinessObject<SubDomainInTopic>
    {
        #region Properties
        private Guid _subDomain;
        public Guid SubDomainId
        {
            get
            {
                return _subDomain;
            }
            set
            {
                _subDomain = value;
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

        #endregion

        #region Constructions
        public SubDomainInTopic()
            : base()
        {
        }

        public SubDomainInTopic(DbDataReader reader)
            : base(reader)
        {
        }
        #endregion
        #region Abstract Methods
        protected override void MappingData(System.Data.Common.DbDataReader reader)
        {
            _subDomain = (Guid)reader["SubDomainId"];
            _topicId = (Guid)reader["TopicId"];
        }
        protected override System.Data.Common.DbCommand GetInsertCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region SubDomainInTopicSubDomainId
            DbParameter prSubDomainId = database.GetParameter(System.Data.DbType.Guid, "@SubDomainId", _subDomain);
            command.Parameters.Add(prSubDomainId);
            #endregion
            #region SubDomainInTopicTopicId
            DbParameter prTopicId = database.GetParameter(System.Data.DbType.Guid, "@TopicId", _topicId);
            command.Parameters.Add(prTopicId);
            #endregion

            #region Command Insert Data
            command.CommandText = @"INSERT INTO [dbo].[SubDomainInTopic]
                                           ([SubDomainId]
                                           ,[TopicId])
                                     VALUES
                                           (@SubDomainId
                                           ,@TopicId)";
            command.CommandType = System.Data.CommandType.Text;
            #endregion

            return command;
        }

        public void AddDB()
        {
            Database database = new Database(ConnectionStringName);
            using (DbConnection connection = database.GetConnection())
            {
                connection.Open();
                DbCommand command = GetInsertCommand();
                command.Connection = connection;
                command.ExecuteNonQuery();
            }
        }

        protected override System.Data.Common.DbCommand GetUpdateCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();

            #region SubDomainInTopicSubDomainId
            DbParameter prSubDomainId = database.GetParameter(System.Data.DbType.Guid, "@SubDomainId", _subDomain);
            command.Parameters.Add(prSubDomainId);
            #endregion

            #region SubDomainInTopicTopicId
            DbParameter prTopicId = database.GetParameter(System.Data.DbType.Guid, "@TopicId", _topicId);
            command.Parameters.Add(prTopicId);
            #endregion

            #region Command Update Data
            command.CommandText = @"UPDATE [dbo].[SubDomainInTopic] SET TopicId = @TopicId WHERE SubDomainID = @SubDomainID";
            command.CommandType = System.Data.CommandType.Text;
            #endregion

            return command;
        }

        protected override System.Data.Common.DbCommand GetDeleteCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();

            #region SubDomainInTopicSubDomainId
            DbParameter prSubDomainId = database.GetParameter(System.Data.DbType.Guid, "@SubDomainId", _subDomain);
            command.Parameters.Add(prSubDomainId);
            #endregion
            #region SubDomainInTopicTopicId
            DbParameter prTopicId = database.GetParameter(System.Data.DbType.Guid, "@TopicId", _topicId);
            command.Parameters.Add(prTopicId);
            #endregion

            #region Command Delete Data
            command.CommandText = @"DELETE FROM [dbo].[SubDomainInTopic]
                                         WHERE SubDomainId = @SubDomainId And TopicId = @TopicId";
            command.CommandType = System.Data.CommandType.Text;
            #endregion

            return command;
        }

        public void RemoveDB()
        {
            Database database = new Database(ConnectionStringName);
            using (DbConnection connection = database.GetConnection())
            {
                connection.Open();
                DbCommand command = GetDeleteCommand();
                command.Connection = connection;
                command.ExecuteNonQuery();
            }
        }
        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(SubDomainInTopic)
                && ((SubDomainInTopic)obj)._subDomain == _subDomain && ((SubDomainInTopic)obj)._topicId == _topicId)
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            int hashCode = 0;
            hashCode += _subDomain.GetHashCode();
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