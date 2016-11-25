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
    public class UserInSubDomainCollection : PSCPortal.Framework.BusinessObjectCollection<UserInSubDomainCollection, UserInSubDomain>
    {
        public UserInSubDomainCollection()
            : base()
        {
        }

        protected override DbCommand GetSelectAllCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            command.CommandText = "SELECT [UserId],[SubDomainId] FROM dbo.UserInSubDomain";
            return command;
        }

        public static UserInSubDomainCollection GetUserInSubDomainCollection()
        {
            Database database = new Database("PSCPortalConnectionString");
            UserInSubDomainCollection result = new UserInSubDomainCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = result.GetSelectAllCommand();
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    UserInSubDomain item = new UserInSubDomain(reader);
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
