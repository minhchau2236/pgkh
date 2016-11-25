using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using PSCPortal.Framework;
using System.Data.Common;
namespace PSCPortal.Security
{
    [Serializable]
    public class RoleCollection : PSCPortal.Framework.BusinessObjectCollection<RoleCollection, Role>
    {
        public RoleCollection()
            : base()
        {
        }

        protected override DbCommand GetSelectAllCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            command.CommandText = "Role_GetAll";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            return command;
        }

        public static RoleCollection GetRoleCollection()
        {
            Database database = new Database("PSCPortalConnectionString");
            RoleCollection result = new RoleCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = result.GetSelectAllCommand();
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

        public static RoleCollection GetRoleCollection(string username)
        {
            Database database = new Database("PSCPortalConnectionString");
            RoleCollection result = new RoleCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand(connection);
                #region UserName
                DbParameter prUserName = database.GetParameter(System.Data.DbType.String, "@UserName", username);
                command.Parameters.Add(prUserName);
                #endregion
                command.CommandText = "Role_GetAllByUserName";
                command.CommandType = CommandType.StoredProcedure;
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

        public static RoleCollection GetRoleCollectionBySubDomain(Guid subGuid)
        {
            Database database = new Database("PSCPortalConnectionString");
            RoleCollection result = new RoleCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand(connection);
                #region UserName
                DbParameter prUserName = database.GetParameter(System.Data.DbType.Guid, "@SubDomainId", subGuid);
                command.Parameters.Add(prUserName);
                #endregion
                command.CommandText = @"SELECT 
	                                        r.RoleId, 
	                                        RoleName, 
	                                        RoleDescription
                                        FROM 
	                                        [Role] r
	                                        INNER JOIN dbo.[SubDomainInRole] uir ON r.RoleId = uir.RoleId
                                        WHERE 
	                                        [SubDomainId] = @SubDomainId";
                command.CommandType = CommandType.Text;
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

        protected static new string ConnectionStringName
        {
            get
            {
                return "PSCPortalConnectionString";
            }
        }
    }
}