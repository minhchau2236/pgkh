using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;

namespace PSCPortal.CMS
{
    [Serializable]
    public class TopicLogin : PSCPortal.Framework.BusinessObject<TopicLogin>
    {
        #region Properties
        private Guid _id;
        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
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

        private string _password = string.Empty;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                _password = value;
            }
        }
        #endregion

        #region Constructions
        public TopicLogin()
            : base()
        { }
        public TopicLogin(DbDataReader reader)
            : base(reader)
        { }
        #endregion

        #region Abstract Methods
        protected override void MappingData(System.Data.Common.DbDataReader reader)
        {
            _id = (Guid)reader["TopicId"];
            _name = (string)reader["UserName"];
            _password = (string)reader["Password"];
        }
        protected override System.Data.Common.DbCommand GetInsertCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();

            #region TopicId
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@TopicId", _id);
            command.Parameters.Add(prId);
            #endregion

            #region UserName
            DbParameter prName = database.GetParameter(System.Data.DbType.String, "@UserName", _name);
            command.Parameters.Add(prName);
            #endregion

            #region UserPassword
            DbParameter prPassword = database.GetParameter(System.Data.DbType.String, "@Password", _password);
            command.Parameters.Add(prPassword);
            #endregion

            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "Insert into dbo.[TopicLogin](TopicId,UserName,Password) values(@TopicId,@UserName,@Password)";
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

            #region UserName
            DbParameter prName = database.GetParameter(System.Data.DbType.String, "@UserName", _name);
            command.Parameters.Add(prName);
            #endregion

            #region UserPassword
            DbParameter prPassword = database.GetParameter(System.Data.DbType.String, "@Password", _password);
            command.Parameters.Add(prPassword);
            #endregion
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = @"UPDATE dbo.[TopicLogin] 
                                    SET [TopicId] = @TopicId,[UserName]=@UserName,Password=@Password WHERE [TopicId] = @TopicId";
            return command;
        }
        protected override System.Data.Common.DbCommand GetDeleteCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region TopicId
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@TopicId", _id);
            command.Parameters.Add(prId);
            #endregion
            command.CommandText = @"DELETE [dbo].[TopicLogin] WHERE TopicId=@TopicId";
            command.CommandType = System.Data.CommandType.Text;
            return command;
        }
        public static TopicLogin GetTopicLogin(string topicid)
        {
            Database database = new Database(ConnectionStringName);
            TopicLogin result = null;
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();
                #region TopicId
                DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@TopicId", new Guid(topicid));
                command.Parameters.Add(prId);
                #endregion
                command.CommandText = @"SELECT TopicId,UserName,Password FROM TopicLogin WHERE TopicId=@TopicId";
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result = new TopicLogin(reader);
                }
            }
            return result;
        }
        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(TopicLogin)
                && ((TopicLogin)obj)._id == _id
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

