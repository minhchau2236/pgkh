using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PSCPortal.Framework;
using System.Data.Common;
namespace PSCPortal.Security
{
    [Serializable]
    public class UserCollection : PSCPortal.Framework.BusinessObjectCollection<UserCollection, User>
    {
        public UserCollection()
            : base()
        {
        }

        protected override DbCommand GetSelectAllCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            command.CommandText = "User_GetAll";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            return command;
        }
        protected DbCommand GetSelectAllCommandLogin()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            command.CommandText = "User_GetAllLogin";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            return command;
        }
        public static UserCollection GetUserCollection()
        {
            Database database = new Database("PSCPortalConnectionString");
            UserCollection result = new UserCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = result.GetSelectAllCommand();
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

        public static UserCollection GetUserCollectionLogin()
        {
            Database database = new Database("PSCPortalConnectionString");
            UserCollection result = new UserCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = result.GetSelectAllCommandLogin();
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


        public static UserCollection GetAllUserBySubDomain(Guid SubDomainId)
        {
            Database database = new Database("PSCPortalConnectionString");
            UserCollection result = new UserCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand(connection);
                DbParameter prSubDomainID = database.GetParameter(System.Data.DbType.Guid, "@SubDomainId", SubDomainId);
                command.Parameters.Add(prSubDomainID);
                command.CommandText = @"SELECT * FROM
	                                   dbo.[User] a 
	                                   INNER JOIN UserInSubDomain b
                                       on a.UserId = b.UserId
                                    WHERE 
	                                   b.SubDomainId = @SubDomainId";
                command.CommandType = System.Data.CommandType.Text;
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
        protected static new string ConnectionStringName
        {
            get
            {
                return "PSCPortalConnectionString";
            }
        }
    }
}