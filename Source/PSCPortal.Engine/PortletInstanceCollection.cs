using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PSCPortal.Framework;
using System.Data.Common;
namespace PSCPortal.Engine
{
    [Serializable]
    public class PortletInstanceCollection : PSCPortal.Framework.BusinessObjectCollection<PortletInstanceCollection, PortletInstance>
    {
        private PortletInstanceCollection()
            : base()
        {
        }

        protected override DbCommand GetSelectAllCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            command.CommandText = "PortletInstance_GetAll";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            return command;
        }

        public static PortletInstanceCollection GetPortletInstanceCollection()
        {
            Database database = new Database("PSCPortalConnectionString");
            PortletInstanceCollection result = new PortletInstanceCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = result.GetSelectAllCommand();
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    PortletInstance item = new PortletInstance(reader);
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
        public static PortletInstanceCollection GetPortletInstanceCollection(Page page)
        {
            Database database = new Database("PSCPortalConnectionString");
            PortletInstanceCollection result = new PortletInstanceCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand(connection);

                #region PageId
                DbParameter prPageId = database.GetParameter(System.Data.DbType.Guid, "@PageId", page.Id);
                command.Parameters.Add(prPageId);
                #endregion               

                command.CommandText = "PortletInstance_GetAllByPageId";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    PortletInstance item = new PortletInstance(reader);
                    result.Add(item);
                }
            }
            return result;
        }
    }
}