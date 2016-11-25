using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PSCPortal.Framework;
using System.Data.Common;
namespace PSCPortal.Security
{
    [Serializable]
    public class FunctionCategoryCollection:List<FunctionCategory>
    {
        private FunctionCategoryCollection()
            : base()
        {
        }

        protected DbCommand GetSelectAllCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            command.CommandText = "SELECT [FunctionCategoryId],[FunctionCategoryName] FROM dbo.FunctionCategory ";
            command.CommandText += "SELECT [FunctionId],[FunctionName], [FunctionCategoryId] FROM [Function]";
            return command;
        }

        public static FunctionCategoryCollection GetFunctionCategoryCollection()
        {
            Database database = new Database(ConnectionStringName);
            FunctionCategoryCollection result = new FunctionCategoryCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = result.GetSelectAllCommand();
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    FunctionCategory item = new FunctionCategory(reader);
                    result.Add(item);
                }
                reader.NextResult();
                int functionCategoryId = 0;
                while (reader.Read())
                {
                    functionCategoryId = (int)reader["FunctionCategoryId"];
                    result.Where(fc => fc.Id == functionCategoryId).Single().FunctionList.Add(new Function(reader));
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