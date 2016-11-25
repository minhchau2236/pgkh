using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;
namespace PSCPortal.Engine
{
    [Serializable]
    public class Portlet : PSCPortal.Framework.BusinessObject<Portlet>
    {
        #region Properties
        private Guid _id;
        public Guid Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        private string _name = string.Empty;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                _name = value;
            }
        }

        private string _displayURL = string.Empty;
        public string DisplayURL
        {
            get
            {
                return _displayURL;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                _displayURL = value;
            }
        }

        private string _editURL = string.Empty;
        public string EditURL
        {
            get
            {
                return _editURL;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                _editURL = value;
            }
        }

        #endregion

        #region Constructions
        public Portlet()
            : base()
        {
        }

        public Portlet(DbDataReader reader)
            : base(reader)
        {
        }
        #endregion
        #region Abstract Methods
        protected override void MappingData(System.Data.Common.DbDataReader reader)
        {
            _id = (Guid)reader["PortletId"];
            _name = (string)reader["PortletName"];
            _displayURL = (string)reader["PortletDisplayURL"];
            _editURL = (string)reader["PortletEditURL"];
        }
        protected override System.Data.Common.DbCommand GetInsertCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region PortletId
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@PortletId", _id);
            command.Parameters.Add(prId);
            #endregion

            #region PortletName
            DbParameter prName = database.GetParameter(System.Data.DbType.String, "@PortletName", _name);
            command.Parameters.Add(prName);
            #endregion

            #region PortletDisplayURL
            DbParameter prDisplayURL = database.GetParameter(System.Data.DbType.String, "@PortletDisplayURL", _displayURL);
            command.Parameters.Add(prDisplayURL);
            #endregion

            #region PortletEditURL
            DbParameter prEditURL = database.GetParameter(System.Data.DbType.String, "@PortletEditURL", _editURL);
            command.Parameters.Add(prEditURL);
            #endregion

            #region Command Insert Data
            command.CommandText = "Portlet_Insert";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            #endregion

            return command;
        }

        protected override System.Data.Common.DbCommand GetUpdateCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region PortletId
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@PortletId", _id);
            command.Parameters.Add(prId);
            #endregion

            #region PortletName
            DbParameter prName = database.GetParameter(System.Data.DbType.String, "@PortletName", _name);
            command.Parameters.Add(prName);
            #endregion

            #region PortletDisplayURL
            DbParameter prDisplayURL = database.GetParameter(System.Data.DbType.String, "@PortletDisplayURL", _displayURL);
            command.Parameters.Add(prDisplayURL);
            #endregion

            #region PortletEditURL
            DbParameter prEditURL = database.GetParameter(System.Data.DbType.String, "@PortletEditURL", _editURL);
            command.Parameters.Add(prEditURL);
            #endregion


            #region Command Update Data
            command.CommandText = "Portlet_Update";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            #endregion

            return command;
        }

        protected override System.Data.Common.DbCommand GetDeleteCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            
            #region PortletId
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@PortletId", _id);
            command.Parameters.Add(prId);
            #endregion

            #region Command Delete Data
            command.CommandText = "Portlet_Delete";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            #endregion

            return command;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(Portlet)
                && ((Portlet)obj)._id == _id
               )
                return true;
            return false;
        }
        public override int GetHashCode()
        {
            int hashCode = 0;
            hashCode += _id.GetHashCode();
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
    }
}