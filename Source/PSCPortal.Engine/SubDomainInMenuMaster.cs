using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;
namespace PSCPortal.Engine
{
    [Serializable]
    public class SubDomainInMenuMaster : PSCPortal.Framework.BusinessObject<SubDomainInMenuMaster>
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

        private Guid _menuMasterId;
        public Guid MenuMasterId
        {
            get
            {
                return _menuMasterId;
            }
            set
            {
                _menuMasterId = value;
            }
        }

        #endregion

        #region Constructions
        public SubDomainInMenuMaster()
            : base()
        {
        }

        public SubDomainInMenuMaster(DbDataReader reader)
            : base(reader)
        {
        }
        #endregion
        #region Abstract Methods
        protected override void MappingData(System.Data.Common.DbDataReader reader)
        {
            _subDomain = (Guid)reader["SubDomainId"];
            _menuMasterId = (Guid)reader["MenuMasterId"];
        }
        protected override System.Data.Common.DbCommand GetInsertCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region SubDomainInMenuMasterSubDomainId
            DbParameter prSubDomainId = database.GetParameter(System.Data.DbType.Guid, "@SubDomainId", _subDomain);
            command.Parameters.Add(prSubDomainId);
            #endregion
            #region SubDomainInMenuMasterMenuMasterId
            DbParameter prMenuMasterId = database.GetParameter(System.Data.DbType.Guid, "@MenuMasterId", _menuMasterId);
            command.Parameters.Add(prMenuMasterId);
            #endregion

            #region Command Insert Data
            command.CommandText = @"INSERT INTO [dbo].[SubDomainInMenuMaster]
                                           ([SubDomainId]
                                           ,[MenuMasterId])
                                     VALUES
                                           (@SubDomainId
                                           ,@MenuMasterId)";
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

            #region SubDomainInMenuMasterSubDomainId
            DbParameter prSubDomainId = database.GetParameter(System.Data.DbType.Guid, "@SubDomainId", _subDomain);
            command.Parameters.Add(prSubDomainId);
            #endregion

            #region SubDomainInMenuMasterMenuMasterId
            DbParameter prMenuMasterId = database.GetParameter(System.Data.DbType.Guid, "@MenuMasterId", _menuMasterId);
            command.Parameters.Add(prMenuMasterId);
            #endregion

            #region Command Update Data
            command.CommandText = "SubDomainInMenuMaster_Update";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            #endregion

            return command;
        }

        protected override System.Data.Common.DbCommand GetDeleteCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();

            #region SubDomainInMenuMasterSubDomainId
            DbParameter prSubDomainId = database.GetParameter(System.Data.DbType.Guid, "@SubDomainId", _subDomain);
            command.Parameters.Add(prSubDomainId);
            #endregion
            #region SubDomainInMenuMasterMenuMasterId
            DbParameter prMenuMasterId = database.GetParameter(System.Data.DbType.Guid, "@MenuMasterId", _menuMasterId);
            command.Parameters.Add(prMenuMasterId);
            #endregion

            #region Command Delete Data
            command.CommandText = @"DELETE FROM [dbo].[SubDomainInMenuMaster]
                                         WHERE SubDomainId = @SubDomainId And MenuMasterId = @MenuMasterId";
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
            if (obj.GetType() == typeof(SubDomainInMenuMaster)
                && ((SubDomainInMenuMaster)obj)._subDomain == _subDomain && ((SubDomainInMenuMaster)obj)._menuMasterId == _menuMasterId)
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