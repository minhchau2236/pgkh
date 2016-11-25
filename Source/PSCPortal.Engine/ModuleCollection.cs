using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PSCPortal.Framework;
using System.Data.Common;
namespace PSCPortal.Engine
{
    [Serializable]
    public class ModuleCollection : PSCPortal.Framework.BusinessObjectCollection<ModuleCollection, Module>
    {
        private ModuleCollection()
            : base()
        {
        }

        protected override DbCommand GetSelectAllCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            command.CommandText = "Module_GetAll";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            return command;
        }

        public static ModuleCollection GetModuleCollection()
        {
            Database database = new Database("PSCPortalConnectionString");
            ModuleCollection result = new ModuleCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = result.GetSelectAllCommand();
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Module item = new Module(reader);
                    item.PageName = (string)reader["PageName"];
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