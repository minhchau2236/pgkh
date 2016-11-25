using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PSCPortal.Framework;
using System.Data.Common;

namespace PSCPortal.Engine
{
    [Serializable]
    public class PagePermissionCollection : List<PagePermission>
    {
        public PagePermissionCollection()
            : base()
        {
        }

        public static PagePermissionCollection GetPagePermissionCollection()
        {
            PagePermissionCollection result = new PagePermissionCollection();
            Database database = new Database("PSCPortalConnectionString");
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand(connection);
                command.CommandText = "PagePermission_GetAll";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                connection.Open();
                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new PagePermission(reader));
                    }
                }
            }
            return result;
        }
    }
}
