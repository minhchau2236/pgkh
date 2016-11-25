using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PSCPortal.Framework;
using System.Data.Common;
using PSCPortal.Security;

namespace PSCPortal.Engine
{
    [Serializable]
    public class SubDomainCollection : PSCPortal.Framework.BusinessObjectCollection<SubDomainCollection, SubDomain>
    {
        protected override DbCommand GetSelectAllCommand()
        {
            Database database = new Database(ConnectionStringName);
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();
                command.CommandText = "SELECT * FROM SubDomain Where Id <> '00000000-0000-0000-0000-000000000000' and IsDelete = 0 Order by Name";// lấy subdomain chưa xóa
                command.CommandType = System.Data.CommandType.Text;
                return command;
            }
        }
        public static SubDomainCollection GetSubDomainCollection()
        {
            Database database = new Database("PSCPortalConnectionString");
            SubDomainCollection result = new SubDomainCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = result.GetSelectAllCommand();
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    SubDomain sub = new SubDomain(reader);
                    result.Add(sub);
                }
            }
            return result;
        }
        public static SubDomainCollection GetSubDomainCollection(Role role)
        {
            PSCPortal.Framework.Database database = new PSCPortal.Framework.Database("PSCPortalConnectionString");
            SubDomainCollection result = new SubDomainCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand(connection);
                #region RoleId
                DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@RoleId", role.Id);
                command.Parameters.Add(prId);
                #endregion

                command.CommandText = @"SELECT 
	                                    a.[Id],
	                                    [Name]
                                    FROM 
	                                    dbo.[SubDomain] a
	                                    inner join SubDomainInRole b on a.Id = b.SubDomainId 
                                    WHERE 
	                                    RoleId=@RoleId and a.IsDelete = 0";//Ngọc - 18122015
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    SubDomain item = new SubDomain();
                    if (!reader.IsDBNull(reader.GetOrdinal("Id")))
                        item.Id = new Guid(reader["Id"].ToString());
                    if (!reader.IsDBNull(reader.GetOrdinal("Name")))
                        item.Name = reader["Name"].ToString();
                    result.Add(item);
                }
            }
            return result;
        }

        public static SubDomainCollection GetSubDomainByUser(User user)
        {
            PSCPortal.Framework.Database database = new PSCPortal.Framework.Database("PSCPortalConnectionString");
            SubDomainCollection result = new SubDomainCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand(connection);
                #region RoleId
                DbParameter prId = database.GetParameter(System.Data.DbType.String, "@UserName", user.Name);
                command.Parameters.Add(prId);
                #endregion

                command.CommandText = @"SELECT b.* FROM UserInSubDomain
	                                      a LEFT JOIN SubDomain b
                                          ON a.SubDomainId = b.Id
                                          INNER JOIN [User] c
                                          ON a.UserId = c.UserId
                                          WHERE c.UserName = @UserName and b.IsDelete = 0";//Ngọc - 18122015
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    SubDomain item = new SubDomain();
                    if (!reader.IsDBNull(reader.GetOrdinal("Id")))
                        item.Id = new Guid(reader["Id"].ToString());
                    if (!reader.IsDBNull(reader.GetOrdinal("Name")))
                        item.Name = reader["Name"].ToString();
                    if (!reader.IsDBNull(reader.GetOrdinal("OldVisitors")))
                        item.OldVisitors = (int)reader["OldVisitors"];
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
