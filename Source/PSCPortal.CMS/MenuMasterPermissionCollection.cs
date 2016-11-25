using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PSCPortal.Framework;
using System.Data.Common;

namespace PSCPortal.CMS
{
    
    [Serializable]
    public class MenuMasterPermissionCollection : List<MenuMasterPermission>
    {
        public MenuMasterPermissionCollection()
            : base()
        {
        }

        public static MenuMasterPermissionCollection GetMenuMasterPermissionCollection()
        {
            MenuMasterPermissionCollection result = new MenuMasterPermissionCollection();
            Database database = new Database("PSCPortalConnectionString");
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand(connection);
                command.CommandText = "MenuMasterPermission_GetAll";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                connection.Open();
                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new MenuMasterPermission(reader));
                    }
                }
                
            }
            
            return result;
        }
    }
}
