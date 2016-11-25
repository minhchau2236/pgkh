using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;
namespace PSCPortal.Engine
{
    [Serializable]
    public class Module : PSCPortal.Framework.BusinessObject<Module>
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
        private Guid _pageId;
        public Guid PageId
        {
            get
            {
                return _pageId;
            }
            set
            {
                _pageId = value;
            }
        }
        private string _pageName = string.Empty;
        public string PageName
        {
            get
            {
                return _pageName;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                _pageName = value;
            }
        }
        #endregion

        #region Constructions
        public Module()
            : base()
        {
        }

        public Module(DbDataReader reader)
            : base(reader)
        {
        }
        #endregion
        #region Abstract Methods
        protected override void MappingData(System.Data.Common.DbDataReader reader)
        {
            _id = (Guid)reader["ModuleId"];
            _name = (string)reader["ModuleName"];
            _displayURL = (string)reader["ModuleDisplayURL"];
            _editURL = (string)reader["ModuleEditURL"];
            _pageId = (Guid)reader["PageId"];
        }
        protected override System.Data.Common.DbCommand GetInsertCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region ModuleId
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@ModuleId", _id);
            command.Parameters.Add(prId);
            #endregion
            #region ModuleName
            DbParameter prName = database.GetParameter(System.Data.DbType.String, "@ModuleName", _name);
            command.Parameters.Add(prName);
            #endregion
            #region ModuleDisplayURL
            DbParameter prDisplayURL = database.GetParameter(System.Data.DbType.String, "@ModuleDisplayURL", _displayURL);
            command.Parameters.Add(prDisplayURL);
            #endregion
            #region ModuleEditURL
            DbParameter prEditURL = database.GetParameter(System.Data.DbType.String, "@ModuleEditURL", _editURL);
            command.Parameters.Add(prEditURL);
            #endregion
            #region PageId
            DbParameter prPageId = database.GetParameter(System.Data.DbType.Guid, "@PageId", _pageId);
            command.Parameters.Add(prPageId);
            #endregion

            #region Command Insert Data
            command.CommandText = "Module_Insert";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            #endregion

            return command;
        }

        protected override System.Data.Common.DbCommand GetUpdateCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region ModuleId
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@ModuleId", _id);
            command.Parameters.Add(prId);
            #endregion
            #region ModuleName
            DbParameter prName = database.GetParameter(System.Data.DbType.String, "@ModuleName", _name);
            command.Parameters.Add(prName);
            #endregion
            #region ModuleDisplayURL
            DbParameter prDisplayURL = database.GetParameter(System.Data.DbType.String, "@ModuleDisplayURL", _displayURL);
            command.Parameters.Add(prDisplayURL);
            #endregion
            #region ModuleEditURL
            DbParameter prEditURL = database.GetParameter(System.Data.DbType.String, "@ModuleEditURL", _editURL);
            command.Parameters.Add(prEditURL);
            #endregion
            #region PageId
            DbParameter prPageId = database.GetParameter(System.Data.DbType.Guid, "@PageId", _pageId);
            command.Parameters.Add(prPageId);
            #endregion

            #region Command Update Data
            command.CommandText = "Module_Update";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            #endregion

            return command;
        }
        public static Module GetModule(string moduleId)
        {
            Database database = new Database(ConnectionStringName);
            Module result = new Module();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();
                #region ModuleId
                DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@ModuleId", new Guid(moduleId));
                command.Parameters.Add(prId);
                #endregion

                command.CommandText = "Module_GetById";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result = new Module(reader);
                }
            }
            return result;  
        }
        protected override System.Data.Common.DbCommand GetDeleteCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region Command Delete Data
            #region ModuleId
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@ModuleId", _id);
            command.Parameters.Add(prId);
            #endregion

            command.CommandText = "Module_Delete";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            #endregion

            return command;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(Module)
                && ((Module)obj)._id == _id
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