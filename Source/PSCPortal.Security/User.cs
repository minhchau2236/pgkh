using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;
using System.Web.Security;
using System.Data.SqlClient;

namespace PSCPortal.Security
{
    [Serializable]
    public class User : PSCPortal.Framework.BusinessObject<User>
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

        private string _email = string.Empty;
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                _email = value;
            }
        }

        private string _passwordQuestion = string.Empty;
        public string PasswordQuestion
        {
            get
            {
                return _passwordQuestion;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                _passwordQuestion = value;
            }
        }

        private string _passwordAnswer = string.Empty;
        public string PasswordAnswer
        {
            get
            {
                return _passwordAnswer;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                _passwordAnswer = value;
            }
        }

        private DateTime _creationDate;
        public DateTime CreationDate
        {
            get
            {
                return _creationDate;
            }
            set
            {
                _creationDate = value;
            }
        }

        private DateTime _lastActivityDate;
        public DateTime LastActivityDate
        {
            get
            {
                return _lastActivityDate;
            }
            set
            {
                _lastActivityDate = value;
            }
        }

        private DateTime _lastLoginDate;
        public DateTime LastLoginDate
        {
            get
            {
                return _lastLoginDate;
            }
            set
            {
                _lastLoginDate = value;
            }
        }

        private DateTime _lastPasswordChangeDate;
        public DateTime LastPasswordChangeDate
        {
            get
            {
                return _lastPasswordChangeDate;
            }
            set
            {
                _lastPasswordChangeDate = value;
            }
        }

        private bool _isApproved;
        public bool IsApproved
        {
            get
            {
                return _isApproved;
            }
            set
            {
                _isApproved = value;
            }
        }

        private bool _isOnline;
        public bool IsOnline
        {
            get
            {
                return _isOnline;
            }
            set
            {
                _isOnline = value;
            }
        }

        private MembershipCreateStatus _status;
        public MembershipCreateStatus Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
            }
        }

        #endregion

        #region Constructions
        public User()
            : base()
        {
        }

        public User(DbDataReader reader)
            : base(reader)
        {
        }
        #endregion
        #region Abstract Methods
        protected override void MappingData(System.Data.Common.DbDataReader reader)
        {
            _id = (Guid)reader["UserId"];
            _name = (string)reader["UserName"];
            _password = (string)reader["Password"];
            _email = (string)reader["Email"];
            _passwordQuestion = (string)reader["PasswordQuestion"];
            _passwordAnswer = (string)reader["PasswordAnswer"];
            _creationDate = (DateTime)reader["CreationDate"];
            _lastActivityDate = (DateTime)reader["LastActivityDate"];
            _lastLoginDate = (DateTime)reader["LastLoginDate"];
            _lastPasswordChangeDate = (DateTime)reader["LastPasswordChangeDate"];
            _isApproved = (bool)reader["IsApproved"];
            _isOnline = (bool)reader["IsOnline"];
        }
        protected override System.Data.Common.DbCommand GetInsertCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region UserId
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@UserID", _id);
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

            #region UserEmail
            DbParameter prEmail = database.GetParameter(System.Data.DbType.String, "@Email", _email);
            command.Parameters.Add(prEmail);
            #endregion

            #region UserPasswordQuestion
            DbParameter prPasswordQuestion = database.GetParameter(System.Data.DbType.String, "@PasswordQuestion", _passwordQuestion);
            command.Parameters.Add(prPasswordQuestion);
            #endregion

            #region UserPasswordAnswer
            DbParameter prPasswordAnswer = database.GetParameter(System.Data.DbType.String, "@PasswordAnswer", _passwordAnswer);
            command.Parameters.Add(prPasswordAnswer);
            #endregion

            #region UserCreationDate
            DbParameter prCreationDate = database.GetParameter(System.Data.DbType.DateTime, "@CreationDate", _creationDate);
            command.Parameters.Add(prCreationDate);
            #endregion

            #region UserLastActivityDate
            DbParameter prLastActivityDate = database.GetParameter(System.Data.DbType.DateTime, "@LastActivityDate", _lastActivityDate);
            command.Parameters.Add(prLastActivityDate);
            #endregion

            #region UserLastLoginDate
            DbParameter prLastLoginDate = database.GetParameter(System.Data.DbType.DateTime, "@LastLoginDate", _lastLoginDate);
            command.Parameters.Add(prLastLoginDate);
            #endregion

            #region UserLastPasswordChangeDate
            DbParameter prLastPasswordChangeDate = database.GetParameter(System.Data.DbType.DateTime, "@LastPasswordChangeDate", _lastPasswordChangeDate);
            command.Parameters.Add(prLastPasswordChangeDate);
            #endregion

            #region UserIsApproved
            DbParameter prIsApproved = database.GetParameter(System.Data.DbType.Boolean, "@IsApproved", _isApproved);
            command.Parameters.Add(prIsApproved);
            #endregion

            #region UserIsOnline
            DbParameter prIsOnline = database.GetParameter(System.Data.DbType.Boolean, "@IsOnline", _isOnline);
            command.Parameters.Add(prIsOnline);
            #endregion

            #region Command Insert Data
            command.CommandText = "User_Insert";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            #endregion

            return command;
        }

        protected override System.Data.Common.DbCommand GetUpdateCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region UserId
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@UserID", _id);
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

            #region UserEmail
            DbParameter prEmail = database.GetParameter(System.Data.DbType.String, "@Email", _email);
            command.Parameters.Add(prEmail);
            #endregion

            #region UserPasswordQuestion
            DbParameter prPasswordQuestion = database.GetParameter(System.Data.DbType.String, "@PasswordQuestion", _passwordQuestion);
            command.Parameters.Add(prPasswordQuestion);
            #endregion

            #region UserPasswordAnswer
            DbParameter prPasswordAnswer = database.GetParameter(System.Data.DbType.String, "@PasswordAnswer", _passwordAnswer);
            command.Parameters.Add(prPasswordAnswer);
            #endregion

            #region UserCreationDate
            DbParameter prCreationDate = database.GetParameter(System.Data.DbType.DateTime, "@CreationDate", _creationDate);
            command.Parameters.Add(prCreationDate);
            #endregion

            #region UserLastActivityDate
            DbParameter prLastActivityDate = database.GetParameter(System.Data.DbType.DateTime, "@LastActivityDate", _lastActivityDate);
            command.Parameters.Add(prLastActivityDate);
            #endregion

            #region UserLastLoginDate
            DbParameter prLastLoginDate = database.GetParameter(System.Data.DbType.DateTime, "@LastLoginDate", _lastLoginDate);
            command.Parameters.Add(prLastLoginDate);
            #endregion

            #region UserLastPasswordChangeDate
            DbParameter prLastPasswordChangeDate = database.GetParameter(System.Data.DbType.DateTime, "@LastPasswordChangeDate", _lastPasswordChangeDate);
            command.Parameters.Add(prLastPasswordChangeDate);
            #endregion

            #region UserIsApproved
            DbParameter prIsApproved = database.GetParameter(System.Data.DbType.Boolean, "@IsApproved", _isApproved);
            command.Parameters.Add(prIsApproved);
            #endregion

            #region UserIsOnline
            DbParameter prIsOnline = database.GetParameter(System.Data.DbType.Boolean, "@IsOnline", _isOnline);
            command.Parameters.Add(prIsOnline);
            #endregion

            #region Command Update Data
            command.CommandText = "User_Update";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            #endregion

            return command;
        }

        protected override System.Data.Common.DbCommand GetDeleteCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();

            #region UserId
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@UserId", _id);
            command.Parameters.Add(prId);
            #endregion

            #region Command Delete Data
            command.CommandText = "User_Delete";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            #endregion

            return command;
        }

        public void UpdatePass()
        {
            Database database = new Database(ConnectionStringName);
            using (DbConnection con = database.GetConnection())
            {
                DbCommand command = database.GetCommand();
                command.Connection = con;
                con.Open();

                #region Password
                DbParameter prId = database.GetParameter(System.Data.DbType.String, "@Password", _password);
                command.Parameters.Add(prId);
                #endregion

                #region UserName
                DbParameter prUserName = database.GetParameter(System.Data.DbType.String, "@UserName", _name);
                command.Parameters.Add(prUserName);
                #endregion

                command.CommandText = "User_UpdatePass";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.ExecuteNonQuery();
            }
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(User)
                && ((User)obj)._id == _id
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

        protected DbCommand GetRolesBelongToCommand(PSCPortal.Framework.Database database)
        {
            DbCommand command = database.GetCommand();
            #region UserId
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@UserId", _id);
            command.Parameters.Add(prId);
            #endregion

            command.CommandText = "Role_GetRolesBelongToUser";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            return command;
        }

        public RoleCollection GetRolesBelongTo()
        {
            PSCPortal.Framework.Database database = new PSCPortal.Framework.Database("PSCPortalConnectionString");
            RoleCollection result = new RoleCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = GetRolesBelongToCommand(database);
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Role item = new Role(reader);
                    result.Add(item);
                }
            }
            return result;
        }

        protected DbCommand GetRolesNotBelongToCommand(PSCPortal.Framework.Database database)
        {
            DbCommand command = database.GetCommand();
            #region UserId
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@UserId", _id);
            command.Parameters.Add(prId);
            #endregion

            command.CommandText = "Role_GetRolesNotBelongToUser";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            return command;
        }

        public RoleCollection GetRolesNotBelongTo()
        {
            PSCPortal.Framework.Database database = new PSCPortal.Framework.Database("PSCPortalConnectionString");
            RoleCollection result = new RoleCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = GetRolesNotBelongToCommand(database);
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Role item = new Role(reader);
                    result.Add(item);
                }
            }
            return result;
        }
    }
}