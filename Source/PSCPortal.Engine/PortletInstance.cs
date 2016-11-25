using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;
namespace PSCPortal.Engine
{
    [Serializable]
    public class PortletInstance : PSCPortal.Framework.BusinessObject<PortletInstance>
    {
        #region Properties
        private Portlet _portlet;
        public Portlet Portlet
        {
            get
            {
                return _portlet;
            }
            set
            {
                _portlet = value;
            }
        }
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


        #endregion

        #region Constructions
        public PortletInstance()
            : base()
        {
        }
        public PortletInstance(Portlet portlet, string name)
        {
            _id = Guid.NewGuid();
            _portlet = portlet;
            _name = name;
        }

        public PortletInstance(DbDataReader reader)
            : base(reader)
        {
        }
        #endregion
        #region Abstract Methods
        protected override void MappingData(System.Data.Common.DbDataReader reader)
        {
            _id = (Guid)reader["PortletInstanceId"];
            _name = (string)reader["PortletInstanceName"];
            _portlet = new Portlet(reader);
        }
        protected override System.Data.Common.DbCommand GetInsertCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region PortletInstanceId
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@PortletInstanceId", _id);
            command.Parameters.Add(prId);
            #endregion
            #region PortletInstanceName
            DbParameter prName = database.GetParameter(System.Data.DbType.String, "@PortletInstanceName", _name);
            command.Parameters.Add(prName);
            #endregion

            #region PortletId
            DbParameter prPortletId = database.GetParameter(System.Data.DbType.Guid, "@PortletId", _id);
            command.Parameters.Add(prPortletId);
            #endregion

            #region Command Insert Data
            command.CommandText = "PortletInstance_Insert";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            #endregion

            return command;
        }

        protected override System.Data.Common.DbCommand GetUpdateCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();

            #region PortletInstanceId
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@PortletInstanceId", _id);
            command.Parameters.Add(prId);
            #endregion

            #region PortletInstanceName
            DbParameter prName = database.GetParameter(System.Data.DbType.String, "@PortletInstanceName", _name);
            command.Parameters.Add(prName);
            #endregion

            #region PortletId
            DbParameter prPortletId = database.GetParameter(System.Data.DbType.Guid, "@PortletId", Portlet.Id);
            command.Parameters.Add(prPortletId);
            #endregion

            #region Command Update Data
            command.CommandText = "PortletInstance_Update";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            #endregion

            return command;
        }

        protected override System.Data.Common.DbCommand GetDeleteCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();

            #region PortletInstanceId
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@PortletInstanceId", _id);
            command.Parameters.Add(prId);
            #endregion

            #region Command Delete Data
            command.CommandText = "PortletInstance_Delete";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            #endregion

            return command;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(PortletInstance)
                && ((PortletInstance)obj)._id == _id
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
        public static PortletInstance GetPortletInstance(Guid id)
        {
            PortletInstance result = null;
            Database database = new Database(ConnectionStringName);
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand(connection);

                #region PortletInstanceId
                DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@PortletInstanceId", id);
                command.Parameters.Add(prId);
                #endregion

                command.CommandText = "PortletInstance_GetById";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                    result = new PortletInstance(reader);
                return result;
            }
        }
    }
}