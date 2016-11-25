using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PSCPortal.Framework;
using System.Data.Common;
namespace PSCPortal.Security
{
    [Serializable]
    public class RoleInRoleCollection : PSCPortal.Framework.BusinessObjectCollection<RoleInRoleCollection, RoleInRole>
    {
        private RoleInRoleCollection()
            : base()
        {
        }

        protected override DbCommand GetSelectAllCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            command.CommandText = "RoleInRole_GetAll";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            return command;
        }

        public static RoleInRoleCollection GetRoleInRoleCollection()
        {
            Database database = new Database(ConnectionStringName);
            RoleInRoleCollection result = new RoleInRoleCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = result.GetSelectAllCommand();
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    RoleInRole item = new RoleInRole(reader);
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