using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;
namespace PSCPortal.Engine
{
    [Serializable]
    public class PortletInstanceInPanel : PSCPortal.Framework.BusinessObject<PortletInstanceInPanel>
    {
        #region Properties

        private PortletInstance _portletInstance;
        public PortletInstance PortletInstance
        {
            get
            {
                return _portletInstance;
            }
            set
            {
                _portletInstance = value;
            }
        }

        private string _style = string.Empty;
        public string Style
        {
            get
            {
                return _style;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                _style = value;
            }
        }
        

        #endregion

        #region Constructions
        public PortletInstanceInPanel()
            : base()
        {
        }

        public PortletInstanceInPanel(DbDataReader reader)
            : base(reader)
        {
        }
        #endregion
        #region Abstract Methods
        protected override void MappingData(System.Data.Common.DbDataReader reader)
        {
            _style = (string)reader["Style"];
            _portletInstance = new PortletInstance(reader);
        }
        protected override System.Data.Common.DbCommand GetInsertCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region PortletInstanceId
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@PortletInstanceId", _portletInstance.Id);
            command.Parameters.Add(prId);
            #endregion

            #region PortletInstanceName
            DbParameter prPortletInstanceName = database.GetParameter(System.Data.DbType.String, "@PortletInstanceName", PortletInstance.Name);
            command.Parameters.Add(prPortletInstanceName);
            #endregion

            #region PortletId
            DbParameter prPortletId = database.GetParameter(System.Data.DbType.Guid, "@PortletId", PortletInstance.Portlet.Id);
            command.Parameters.Add(prPortletId);
            #endregion

            #region PageId
            DbParameter prPageId = database.GetParameter(System.Data.DbType.Guid, "@PageId", ((PortletInstanceInPanelCollection)_collection).PanelInPage.Page.Id);
            command.Parameters.Add(prPageId);
            #endregion

            #region PanelId
            DbParameter prPanelId = database.GetParameter(System.Data.DbType.Int32, "@PanelId", ((PortletInstanceInPanelCollection)_collection).PanelInPage.Panel.Id);
            command.Parameters.Add(prPanelId);
            #endregion         
   
            #region Order
            DbParameter prOrder = database.GetParameter(System.Data.DbType.Int32, "@Order", ((PortletInstanceInPanelCollection)_collection).Count);
            command.Parameters.Add(prOrder);
            #endregion

            #region PortletInstanceInPanelStyle
            DbParameter prStyle = database.GetParameter(System.Data.DbType.String, "@Style", _style);
            command.Parameters.Add(prStyle);
            #endregion           

            #region Command Insert Data  
            command.CommandText = "PortletInstanceInPanel_Insert";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            #endregion

            return command;
        }

        protected override System.Data.Common.DbCommand GetUpdateCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();

            #region PortletInstanceId
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@PortletInstanceId", _portletInstance.Id);
            command.Parameters.Add(prId);
            #endregion

            #region PageId
            DbParameter prPageId = database.GetParameter(System.Data.DbType.Guid, "@PageId", ((PortletInstanceInPanelCollection)_collection).PanelInPage.Page.Id);
            command.Parameters.Add(prPageId);
            #endregion

            #region PanelId
            DbParameter prPanelId = database.GetParameter(System.Data.DbType.Int32, "@PanelId", ((PortletInstanceInPanelCollection)_collection).PanelInPage.Panel.Id);
            command.Parameters.Add(prPanelId);
            #endregion 

            #region PortletInstanceInPanelStyle
            DbParameter prStyle = database.GetParameter(System.Data.DbType.String, "@Style", _style);
            command.Parameters.Add(prStyle);
            #endregion 

            #region Command Update Data
            command.CommandText = "PortletInstanceInPanel_Update";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            #endregion

            return command;
        }

        protected override System.Data.Common.DbCommand GetDeleteCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            

            #region PortletInstanceId
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@PortletInstanceId", _portletInstance.Id);
            command.Parameters.Add(prId);
            #endregion

            #region PageId
            DbParameter prPageId = database.GetParameter(System.Data.DbType.Guid, "@PageId", ((PortletInstanceInPanelCollection)_collection).PanelInPage.Page.Id);
            command.Parameters.Add(prPageId);
            #endregion

            #region PanelId
            DbParameter prPanelId = database.GetParameter(System.Data.DbType.Int32, "@PanelId", ((PortletInstanceInPanelCollection)_collection).PanelInPage.Panel.Id);
            command.Parameters.Add(prPanelId);
            #endregion

            #region Command Delete Data
            command.CommandText = "PortletInstanceInPanel_Delete";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            #endregion

            return command;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(PortletInstanceInPanel)&&((PortletInstanceInPanel)obj).PortletInstance.Id==PortletInstance.Id)               
                return true;
            return false;
        }
        public override int GetHashCode()
        {
            int hashCode = 0;
            return hashCode;
        }
        protected static new string ConnectionStringName
        {
            get
            {
                return "PSCPortalConnectionString";
            }
        }        
        #endregion
        public System.Web.UI.Control RenderPortletTable(bool enableEdit)
        {
                  System.Web.UI.UserControl uc = new System.Web.UI.UserControl();
                  //            string displayURL = string.Format("~/Portlets/{0}/{1}", _portletInstance.Portlet.Id, _portletInstance.Portlet.DisplayFileName);
                  PortletControl result = (PortletControl)uc.LoadControl(_portletInstance.Portlet.DisplayURL);
                  //result.ID = string.Format("portlet{0}{1}{2}", _parent.Parent.Panel.Id, _order, _name);
                  result.ID = string.Format("portlet_{0}", _portletInstance.Id);
                  result.Portlet = this;
                  result.EnableEdit = enableEdit;
                  return result;
            }

            public System.Web.UI.Control RenderPortletDiv(bool enableEdit)
            {

                 

                  System.Web.UI.UserControl uc = new System.Web.UI.UserControl();

                  // ham nay load usercontrol(porlet) theo duong dan trong class RenderTitlePorletInDiv

                  PortletControl result = (PortletControl)uc.LoadControl(_portletInstance.Portlet.DisplayURL);
                  result.ID = string.Format("psc-portlet-{0}", _portletInstance.Id);
                  result.Portlet = this;
                  result.EnableEdit = enableEdit;
                  return result;
            }
      }
}