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
    public class UserInRoleCollection : PSCPortal.Framework.BusinessObjectCollection<UserInRoleCollection, UserInRole>
    {
        public UserInRoleCollection()
            : base()
        {
        }

        protected override DbCommand GetSelectAllCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            command.CommandText = "SELECT [UserId],[RoleId] FROM dbo.UserInRole";
            return command;
        }

        public static UserInRoleCollection GetUserInRoleCollection()
        {
            Database database = new Database("PSCPortalConnectionString");
            UserInRoleCollection result = new UserInRoleCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = result.GetSelectAllCommand();
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    UserInRole item = new UserInRole(reader);
                    result.Add(item);
                }
            }
            return result;
        }

        protected DbCommand GetSelectUsersNotInRoleCommand(Guid roleId)
        {
            Database database = new Database(ConnectionStringName);

            DbCommand command = database.GetCommand();

            DbParameter prRoleID = database.GetParameter(System.Data.DbType.Guid, "@RoleID", roleId);
            command.Parameters.Add(prRoleID);

            command.CommandText = "UserInRole_FilterByNotEqualingRoleId";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            return command;
        }

        public static UserInRoleCollection GetUserNotInRoleCollection(Guid roleId)
        {
            Database database = new Database("PSCPortalConnectionString");
            UserInRoleCollection result = new UserInRoleCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = result.GetSelectUsersNotInRoleCommand(roleId);
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    UserInRole item = new UserInRole(reader);
                    result.Add(item);
                }
            }
            return result;
        }


        //
        protected DbCommand GetRolesNotOfUserAllCommand(Guid userID)
        {
            Database database = new Database(ConnectionStringName);

            DbCommand command = database.GetCommand();

            DbParameter prUserID = database.GetParameter(System.Data.DbType.Guid, "@UserID", userID);
            command.Parameters.Add(prUserID);

            command.CommandText = "UserInRole_FilterByNotEqualingUserId";
            command.CommandType = CommandType.StoredProcedure;
            return command;
        }

        public static UserInRoleCollection GetRolesNotOfUserCollection(Guid userId)
        {
            Database database = new Database("PSCPortalConnectionString");
            UserInRoleCollection result = new UserInRoleCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = result.GetRolesNotOfUserAllCommand(userId);
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    UserInRole item = new UserInRole(reader);
                    result.Add(item);
                }
            }
            return result;
        }
        //
        
        
        protected static new string ConnectionStringName
        {
            get
            {
                return "PSCPortalConnectionString";
            }
        }

        protected DbCommand GetRolesByUserIDCommand(PSCPortal.Framework.Database database, Guid userID)
        {
            DbCommand command = database.GetCommand();
            DbParameter prUserID = database.GetParameter(System.Data.DbType.Guid, "@UserID", userID);
            command.Parameters.Add(prUserID);

            command.CommandText = "UserInRole_FilterByEqualingUserId";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            return command;
        }

        public static UserInRoleCollection GetRolesByUserID(Guid userID)
        {
            PSCPortal.Framework.Database database = new PSCPortal.Framework.Database("PSCPortalConnectionString");
            UserInRoleCollection result = new UserInRoleCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = result.GetRolesByUserIDCommand(database, userID);
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    UserInRole item = new UserInRole(reader);
                    result.Add(item);
                }
            }
            return result;
        }

        /// <summary>
        /// get user list by userid
        /// </summary>
        /// <param name="database"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        protected DbCommand GetUsersByRoleIDCommand(PSCPortal.Framework.Database database, Guid roleID)
        {
            DbCommand command = database.GetCommand();
            DbParameter prRoleID = database.GetParameter(System.Data.DbType.Guid, "@RoleID", roleID);
            command.Parameters.Add(prRoleID);

            command.CommandText = "UserInRole_FilterByEqualingRoleId";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            return command;
        }

        public static UserInRoleCollection GetUserInRoleCollectionByRoleID(Guid roleID)
        {
            PSCPortal.Framework.Database database = new PSCPortal.Framework.Database("PSCPortalConnectionString");
            UserInRoleCollection result = new UserInRoleCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = result.GetUsersByRoleIDCommand(database, roleID);
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    UserInRole item = new UserInRole(reader);
                    result.Add(item);
                }
            }
            return result;
        }
    }
}