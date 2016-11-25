using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;
namespace PSCPortal.Engine
{
    [Serializable]
    public class SubDomainInRole : PSCPortal.Framework.BusinessObject<SubDomainInRole>
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
        public SubDomainInRole()
            : base()
        {
        }

        public SubDomainInRole(DbDataReader reader)
            : base(reader)
        {
        }
        #endregion
        #region Abstract Methods
        protected override void MappingData(System.Data.Common.DbDataReader reader)
        {
            _subDomain = (Guid)reader["SubDomainId"];
            _roleId = (Guid)reader["RoleId"];
        }
        protected override System.Data.Common.DbCommand GetInsertCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region SubDomainInRoleSubDomainId
            DbParameter prSubDomainId = database.GetParameter(System.Data.DbType.Guid, "@SubDomainId", _subDomain);
            command.Parameters.Add(prSubDomainId);
            #endregion
            #region SubDomainInRoleRoleId
            DbParameter prRoleId = database.GetParameter(System.Data.DbType.Guid, "@RoleId", _roleId);
            command.Parameters.Add(prRoleId);
            #endregion

            #region Command Insert Data
            command.CommandText = @"INSERT INTO [dbo].[SubDomainInRole]
                                           ([SubDomainId]
                                           ,[RoleId])
                                     VALUES
                                           (@SubDomainId
                                           ,@RoleId)";
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

            #region SubDomainInRoleSubDomainId
            DbParameter prSubDomainId = database.GetParameter(System.Data.DbType.Guid, "@SubDomainId", _subDomain);
            command.Parameters.Add(prSubDomainId);
            #endregion

            #region SubDomainInRoleRoleId
            DbParameter prRoleId = database.GetParameter(System.Data.DbType.Guid, "@RoleId", _roleId);
            command.Parameters.Add(prRoleId);
            #endregion

            #region Command Update Data
            command.CommandText = @"UPDATE [dbo].[SubDomainInRole] SET RoleId = @RoleId WHERE SubDomainID = @SubDomainID";
            command.CommandType = System.Data.CommandType.Text;
            #endregion

            return command;
        }

        protected override System.Data.Common.DbCommand GetDeleteCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();

            #region SubDomainInRoleSubDomainId
            DbParameter prSubDomainId = database.GetParameter(System.Data.DbType.Guid, "@SubDomainId", _subDomain);
            command.Parameters.Add(prSubDomainId);
            #endregion
            #region SubDomainInRoleRoleId
            DbParameter prRoleId = database.GetParameter(System.Data.DbType.Guid, "@RoleId", _roleId);
            command.Parameters.Add(prRoleId);
            #endregion

            #region Command Delete Data
            command.CommandText = @"DELETE FROM [dbo].[SubDomainInRole]
                                         WHERE SubDomainId = @SubDomainId And RoleId = @RoleId";
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
            if (obj.GetType() == typeof(SubDomainInRole)
                && ((SubDomainInRole)obj)._subDomain == _subDomain && ((SubDomainInRole)obj)._roleId == _roleId)
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