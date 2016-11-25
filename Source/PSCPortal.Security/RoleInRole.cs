using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;
namespace PSCPortal.Security
{
    [Serializable]
    public class RoleInRole : PSCPortal.Framework.BusinessObject<RoleInRole>
    {
        #region Properties
        private Guid _refId;
        public Guid RefId
        {
            get
            {
                return _refId;
            }
            set
            {
                _refId = value;
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

        private Guid _roleParentId;
        public Guid RoleParentId
        {
            get
            {
                return _roleParentId;
            }
            set
            {
                _roleParentId = value;
            }
        }

        #endregion

        #region Constructions
        public RoleInRole()
            : base()
        {
        }

        public RoleInRole(DbDataReader reader)
            : base(reader)
        {
        }
        #endregion
        #region Abstract Methods
        protected override void MappingData(System.Data.Common.DbDataReader reader)
        {
            _refId = (Guid)reader["RefId"];
            _roleId = (Guid)reader["RoleId"];
            _roleParentId = (Guid)reader["RoleParentId"];
        }
        protected override System.Data.Common.DbCommand GetInsertCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region RoleInRoleRefId
            DbParameter prRefId = database.GetParameter(System.Data.DbType.Guid, "@RefId", _refId);
            command.Parameters.Add(prRefId);
            #endregion
            #region RoleInRoleRoleId
            DbParameter prRoleId = database.GetParameter(System.Data.DbType.Guid, "@RoleId", _roleId);
            command.Parameters.Add(prRoleId);
            #endregion
            #region RoleInRoleRoleParentId
            DbParameter prRoleParentId = database.GetParameter(System.Data.DbType.Guid, "@RoleParentId", _roleParentId);
            command.Parameters.Add(prRoleParentId);
            #endregion

            #region Command Insert Data
            command.CommandText = "RoleInRole_Insert";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            #endregion

            return command;
        }

        protected override System.Data.Common.DbCommand GetUpdateCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region RoleInRoleRefId
            DbParameter prRefId = database.GetParameter(System.Data.DbType.Guid, "@RefId", _refId);
            command.Parameters.Add(prRefId);
            #endregion
            #region RoleInRoleRoleId
            DbParameter prRoleId = database.GetParameter(System.Data.DbType.Guid, "@RoleId", _roleId);
            command.Parameters.Add(prRoleId);
            #endregion
            #region RoleInRoleRoleParentId
            DbParameter prRoleParentId = database.GetParameter(System.Data.DbType.Guid, "@RoleParentId", _roleParentId);
            command.Parameters.Add(prRoleParentId);
            #endregion

            #region Command Update Data
            command.CommandText = "RoleInRole_Update";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            #endregion

            return command;
        }

        protected override System.Data.Common.DbCommand GetDeleteCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();

            #region RoleInRoleRefId
            DbParameter prRefId = database.GetParameter(System.Data.DbType.Guid, "@RefId", _refId);
            command.Parameters.Add(prRefId);
            #endregion

            #region Command Delete Data
            command.CommandText = "RoleInRole_Delete";
            #endregion

            return command;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(RoleInRole)
                && ((RoleInRole)obj)._refId == _refId
               )
                return true;
            return false;
        }
        public override int GetHashCode()
        {
            int hashCode = 0;
            hashCode += _refId.GetHashCode();
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