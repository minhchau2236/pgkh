using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PSCPortal.Framework;
using System.Data.Common;
namespace PSCPortal.Security
{
    [Serializable]
    public class FunctionCollection : List<Function>
    {        

        protected DbCommand GetSelectAllCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            command.CommandText = "SELECT [FunctionId],[FunctionName] FROM [Function]";
            return command;
        }

        public static FunctionCollection GetFunctionCollection()
        {
            Database database = new Database(ConnectionStringName);
            FunctionCollection result = new FunctionCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = result.GetSelectAllCommand();
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Function item = new Function(reader);
                    result.Add(item);
                }
            }
            return result;
        }
        protected static string ConnectionStringName
        {
            get
            {
                return "PSCPortalConnectionString";
            }
        }
    }
}