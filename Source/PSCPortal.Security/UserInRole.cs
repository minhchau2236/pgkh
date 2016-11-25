using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;
namespace PSCPortal.Security
{
    public class UserInRoleEqualityComparer : IEqualityComparer<UserInRole>
    {
        public bool Equals(UserInRole u1, UserInRole u2)
        {
            if (u1.RoleId==u2.RoleId && u1.UserId==u2.UserId)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public int GetHashCode(UserInRole u)
        {
            int hCode = u.UserId.GetHashCode() * u.RoleId.GetHashCode();
            return hCode.GetHashCode();
        }

    }

    [Serializable]
    public class UserInRole : PSCPortal.Framework.BusinessObject<UserInRole>
    {
        #region Properties
        private Guid _userId;
        public Guid UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                _userId = value;
            }
        }

        private Guid _roleId;
        public Guid RoleId
        {
            get
            {
                return _roleId;
            }
            set
            {
                _roleId = value;
            }
        }

        #endregion

        #region Constructions
        public UserInRole()
            : base()
        {
        }

        public UserInRole(DbDataReader reader)
            : base(reader)
        {
        }
        #endregion
        #region Abstract Methods
        protected override void MappingData(System.Data.Common.DbDataReader reader)
        {
            _userId = (Guid)reader["UserId"];
            _roleId = (Guid)reader["RoleId"];
        }
        protected override System.Data.Common.DbCommand GetInsertCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region UserInRoleUserId
            DbParameter prUserId = database.GetParameter(System.Data.DbType.Guid, "@UserId", _userId);
            command.Parameters.Add(prUserId);
            #endregion
            #region UserInRoleRoleId
            DbParameter prRoleId = database.GetParameter(System.Data.DbType.Guid, "@RoleId", _roleId);
            command.Parameters.Add(prRoleId);
            #endregion

            #region Command Insert Data
            command.CommandText = "UserInRole_Insert";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            #endregion

            return command;
        }

        protected override System.Data.Common.DbCommand GetUpdateCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();

            #region UserInRoleUserId
            DbParameter prUserId = database.GetParameter(System.Data.DbType.Guid, "@UserId", _userId);
            command.Parameters.Add(prUserId);
            #endregion

            #region UserInRoleRoleId
            DbParameter prRoleId = database.GetParameter(System.Data.DbType.Guid, "@RoleId", _roleId);
            command.Parameters.Add(prRoleId);
            #endregion

            #region Command Update Data
            command.CommandText = "UserInRole_Update";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            #endregion

            return command;
        }

        protected override System.Data.Common.DbCommand GetDeleteCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();

            #region UserInRoleUserId
            DbParameter prUserId = database.GetParameter(System.Data.DbType.Guid, "@UserId", _userId);
            command.Parameters.Add(prUserId);
            #endregion
            #region UserInRoleRoleId
            DbParameter prRoleId = database.GetParameter(System.Data.DbType.Guid, "@RoleId", _roleId);
            command.Parameters.Add(prRoleId);
            #endregion

            #region Command Delete Data
            command.CommandText = "UserInRole_Delete";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            #endregion

            return command;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(UserInRole)
                && ((UserInRole)obj)._userId == _userId && ((UserInRole)obj)._roleId == _roleId)
                return true;
            return false;
        }



        public override int GetHashCode()
        {
            int hashCode = 0;
            hashCode += _userId.GetHashCode();
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