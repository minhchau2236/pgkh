using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;
namespace PSCPortal.Security
{
    public class UserInSubDomainEqualityComparer : IEqualityComparer<UserInSubDomain>
    {
        public bool Equals(UserInSubDomain u1, UserInSubDomain u2)
        {
            if (u1.SubDomainId == u2.SubDomainId && u1.UserId == u2.UserId)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public int GetHashCode(UserInSubDomain u)
        {
            int hCode = u.UserId.GetHashCode() * u.SubDomainId.GetHashCode();
            return hCode.GetHashCode();
        }

    }
    [Serializable]
    public class UserInSubDomain : PSCPortal.Framework.BusinessObject<UserInSubDomain>
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

        private Guid _subDomainId;
        public Guid SubDomainId
        {
            get
            {
                return _subDomainId;
            }
            set
            {
                _subDomainId = value;
            }
        }

        #endregion

        #region Constructions
        public UserInSubDomain()
            : base()
        {
        }

        public UserInSubDomain(DbDataReader reader)
            : base(reader)
        {
        }
        #endregion
        #region Abstract Methods
        protected override void MappingData(System.Data.Common.DbDataReader reader)
        {
            _userId = (Guid)reader["UserId"];
            _subDomainId = (Guid)reader["SubDomainId"];
        }
        protected override System.Data.Common.DbCommand GetInsertCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region UserId
            DbParameter prUserId = database.GetParameter(System.Data.DbType.Guid, "@UserId", _userId);
            command.Parameters.Add(prUserId);
            #endregion
            #region SubDomainId
            DbParameter prRoleId = database.GetParameter(System.Data.DbType.Guid, "@SubDomainId", _subDomainId);
            command.Parameters.Add(prRoleId);
            #endregion

            #region Command Insert Data
            command.CommandText = "UserInSubDomain_Insert";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            #endregion

            return command;
        }

        protected override System.Data.Common.DbCommand GetUpdateCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();

            #region UserId
            DbParameter prUserId = database.GetParameter(System.Data.DbType.Guid, "@UserId", _userId);
            command.Parameters.Add(prUserId);
            #endregion

            #region SubDomainId
            DbParameter prRoleId = database.GetParameter(System.Data.DbType.Guid, "@SubDomainId", _subDomainId);
            command.Parameters.Add(prRoleId);
            #endregion

            #region Command Update Data
            command.CommandText = "UserInSubDomain_Update";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            #endregion

            return command;
        }

        protected override System.Data.Common.DbCommand GetDeleteCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();

            #region UserId
            DbParameter prUserId = database.GetParameter(System.Data.DbType.Guid, "@UserId", _userId);
            command.Parameters.Add(prUserId);
            #endregion
            #region SubDomainId
            DbParameter prRoleId = database.GetParameter(System.Data.DbType.Guid, "@SubDomainId", _subDomainId);
            command.Parameters.Add(prRoleId);
            #endregion

            #region Command Delete Data
            command.CommandText = "UserInSubDomain_Delete";
            command.CommandType = System.Data.CommandType.StoredProcedure;
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
            if (obj.GetType() == typeof(UserInRole)
                && ((UserInSubDomain)obj)._userId == _userId && ((UserInSubDomain)obj)._subDomainId == _subDomainId)
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