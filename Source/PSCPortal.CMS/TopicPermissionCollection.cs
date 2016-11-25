using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PSCPortal.Framework;
using System.Data.Common;

namespace PSCPortal.CMS
{
    [Serializable]
    public class TopicPermissionCollection : List<TopicPermission>
    {
        public TopicPermissionCollection()
            : base()
        {
        }

        public static TopicPermissionCollection GetTopicPermissionCollection()
        {
            TopicPermissionCollection result = new TopicPermissionCollection();
            Database database = new Database("PSCPortalConnectionString");
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand(connection);
                command.CommandText = "TopicPermission_GetAll";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                connection.Open();
                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new TopicPermission(reader));
                    }
                }
            }
            return result;
        }
    }
}
