using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;
namespace PSCPortal.Security
{
    [Serializable]
    public class Role : PSCPortal.Framework.BusinessObject<Role>
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

        private System.Collections.Specialized.StringCollection _assignedUsers = new System.Collections.Specialized.StringCollection();
        public System.Collections.Specialized.StringCollection AssignedUsers
        {
            get
            {
                return _assignedUsers;
            }
            set
            {
                _assignedUsers = value;
            }
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
        #endregion

        private string _subDomainName = string.Empty;
        public string SubDomainName
        {
            get
            {
                return _subDomainName;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                _subDomainName = value;
            }
        }

        #region Constructions
        public Role()
            : base()
        {
        }

        public Role(DbDataReader reader)
            : base(reader)
        {
        }
        #endregion
        #region Abstract Methods
        protected override void MappingData(System.Data.Common.DbDataReader reader)
        {
            _id = (Guid)reader["RoleId"];
            _name = (string)reader["RoleName"];
            _description = (string)reader["RoleDescription"];
        }
        protected override System.Data.Common.DbCommand GetInsertCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region RoleId
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@RoleId", _id);
            command.Parameters.Add(prId);
            #endregion
            #region RoleName
            DbParameter prName = database.GetParameter(System.Data.DbType.String, "@RoleName", _name);
            command.Parameters.Add(prName);
            #endregion
            #region RoleDescription
            DbParameter prDescription = database.GetParameter(System.Data.DbType.String, "@RoleDescription", _description);
            command.Parameters.Add(prDescription);
            #endregion

            #region Command Insert Data
            command.CommandText = "Role_Insert";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            #endregion

            return command;
        }

        protected override System.Data.Common.DbCommand GetUpdateCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region RoleId
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@RoleId", _id);
            command.Parameters.Add(prId);
            #endregion
            #region RoleName
            DbParameter prName = database.GetParameter(System.Data.DbType.String, "@RoleName", _name);
            command.Parameters.Add(prName);
            #endregion
            #region RoleDescription
            DbParameter prDescription = database.GetParameter(System.Data.DbType.String, "@RoleDescription", _description);
            command.Parameters.Add(prDescription);
            #endregion

            #region Command Update Data
            command.CommandText = "Role_Update";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            #endregion

            return command;
        }

        protected override System.Data.Common.DbCommand GetDeleteCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();

            #region RoleId
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@RoleId", _id);
            command.Parameters.Add(prId);
            #endregion

            #region Command Delete Data
            command.CommandText = "Role_Delete";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            #endregion

            return command;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(Role)
                && ((Role)obj)._id == _id
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

        protected DbCommand GetUsersByRoleIDCommand(PSCPortal.Framework.Database database, Guid roleID)
        {
            DbCommand command = database.GetCommand();
            DbParameter prRoleID = database.GetParameter(System.Data.DbType.Guid, "@RoleId", roleID);
            command.Parameters.Add(prRoleID);

            command.CommandText = "User_GetUsersInRole";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            return command;
        }

        public UserCollection GetUsersInRole()
        {
            PSCPortal.Framework.Database database = new PSCPortal.Framework.Database("PSCPortalConnectionString");
            UserCollection result = new UserCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = GetUsersByRoleIDCommand(database, this._id);
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    User item = new User(reader);
                    result.Add(item);
                }
            }
            return result;
        }

        protected DbCommand GetUsersNotInByRoleIDCommand(PSCPortal.Framework.Database database, Guid roleID)
        {
            DbCommand command = database.GetCommand();
            DbParameter prRoleID = database.GetParameter(System.Data.DbType.Guid, "@RoleID", roleID);
            command.Parameters.Add(prRoleID);

            command.CommandText = "User_GetUsersNotInRole";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            return command;
        }

        public UserCollection GetUsersNotInRole()
        {
            PSCPortal.Framework.Database database = new PSCPortal.Framework.Database("PSCPortalConnectionString");
            UserCollection result = new UserCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = GetUsersNotInByRoleIDCommand(database, this._id);
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    User item = new User(reader);
                    result.Add(item);
                }
            }
            return result;
        }
    }
}