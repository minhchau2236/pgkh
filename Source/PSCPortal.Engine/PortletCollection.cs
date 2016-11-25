using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PSCPortal.Framework;
using System.Data.Common;
namespace PSCPortal.Engine
{
    [Serializable]
    public class PortletCollection : PSCPortal.Framework.BusinessObjectCollection<PortletCollection, Portlet>
    {
        private PortletCollection()
            : base()
        {
        }

        protected override DbCommand GetSelectAllCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            command.CommandText = "Portlet_GetAll";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            return command;
        }

        public static PortletCollection GetPortletCollection()
        {
            Database database = new Database("PSCPortalConnectionString");
            PortletCollection result = new PortletCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = result.GetSelectAllCommand();
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Portlet item = new Portlet(reader);
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