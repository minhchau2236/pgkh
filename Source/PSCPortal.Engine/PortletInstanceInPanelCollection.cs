using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PSCPortal.Framework;
using System.Data.Common;
namespace PSCPortal.Engine
{
    [Serializable]
    public class PortletInstanceInPanelCollection : PSCPortal.Framework.BusinessObjectCollection<PortletInstanceInPanelCollection, PortletInstanceInPanel>
    {
        protected internal PortletInstanceInPanelCollection(PanelInPage pip)
            : base()
        {
            _panelInPage = pip;
        }
        private PanelInPage _panelInPage;
        public PanelInPage PanelInPage
        {
            get
            {
                return _panelInPage;
            }
        }
        protected override DbCommand GetSelectAllCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region PageId
            DbParameter prPageId = database.GetParameter(System.Data.DbType.Guid, "@PageId", _panelInPage.Page.Id);
            command.Parameters.Add(prPageId);
            #endregion

            #region PanelId
            DbParameter prPanelId = database.GetParameter(System.Data.DbType.Int32, "@PanelId", _panelInPage.Panel.Id);
            command.Parameters.Add(prPanelId);
            #endregion            
            command.CommandText = "PortletInstanceInPanel_GetAllByPageIdAndPanelId";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            return command;
        }

        public static PortletInstanceInPanelCollection GetPortletInstanceInPanelCollection(PanelInPage pip)
        {
            Database database = new Database("PSCPortalConnectionString");
            PortletInstanceInPanelCollection result = new PortletInstanceInPanelCollection(pip);            
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = result.GetSelectAllCommand();
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    PortletInstanceInPanel item = new PortletInstanceInPanel(reader);
                    result.Add(item);                    
                }
            }
            return result;
        }
        public void UpdatePosition()
        {
            Database database = new Database(ConnectionStringName);
            using (DbConnection connection = database.GetConnection())
            {
                connection.Open();
                DbTransaction trans = connection.BeginTransaction();
                List<DbCommand> listCommand = new List<DbCommand>();
                int i = 1;
                foreach (PortletInstanceInPanel item in this)
                {
                    DbCommand command = database.GetCommand(connection);
                    command.Transaction = trans;
                    #region PortletInstanceId
                   
                    DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@PortletInstanceId", item.PortletInstance.Id);
                    command.Parameters.Add(prId);
                    #endregion

                    #region PageId
                    DbParameter prPageId = database.GetParameter(System.Data.DbType.Guid, "@PageId", _panelInPage.Page.Id);
                    command.Parameters.Add(prPageId);
                    #endregion

                    #region PanelId
                    DbParameter prPanelId = database.GetParameter(System.Data.DbType.Int32, "@PanelId", _panelInPage.Panel.Id);
                    command.Parameters.Add(prPanelId);
                    #endregion

                    #region Order
                    DbParameter prOrder = database.GetParameter(System.Data.DbType.Int32, "@Order", i);
                    command.Parameters.Add(prOrder);
                    #endregion

                    command.CommandText = "PortletInstanceInPanel_UpdatePosition";
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    listCommand.Add(command);
                    i++;
                }
                try
                {
                    foreach (DbCommand command in listCommand)
                        command.ExecuteNonQuery();
                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                }
            }
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