using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;
namespace PSCPortal.CMS
{
    [Serializable]
    public class MenuMaster : PSCPortal.Framework.BusinessObject<MenuMaster>
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

        private string _description = string.Empty;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                _description = value;
            }
        }
        private bool _isCheck = false;
        public bool IsCheck
        {
            get
            {
                return _isCheck;
            }
            set
            {
                _isCheck = value;
            }
        }
        #endregion

        #region Constructions
        public MenuMaster()
            : base()
        {
        }

        public MenuMaster(DbDataReader reader)
            : base(reader)
        {
        }
        #endregion
        #region Abstract Methods
        protected override void MappingData(System.Data.Common.DbDataReader reader)
        {
            _id = (Guid)reader["MenuMasterId"];
            _name = (string)reader["MenuMasterName"];
            _description = (string)reader["MenuMasterDescription"];
        }
        protected override System.Data.Common.DbCommand GetInsertCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region MenuMasterId
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@MenuMasterId", _id);
            command.Parameters.Add(prId);
            #endregion
            #region MenuMasterName
            DbParameter prName = database.GetParameter(System.Data.DbType.String, "@MenuMasterName", _name);
            command.Parameters.Add(prName);
            #endregion
            #region MenuMasterDescription
            DbParameter prDescription = database.GetParameter(System.Data.DbType.String, "@MenuMasterDescription", _description);
            command.Parameters.Add(prDescription);
            #endregion
            #region MenuId
            DbParameter prMenuId = database.GetParameter(System.Data.DbType.Guid, "@MenuId", Guid.NewGuid());
            command.Parameters.Add(prMenuId);
            #endregion
            #region MenuName
            DbParameter prMenuName = database.GetParameter(System.Data.DbType.String, "@MenuName", "ROOT of Master [" + _name + "]");
            command.Parameters.Add(prMenuName);
            #endregion
            #region MenuDescription
            DbParameter prMenuDescription = database.GetParameter(System.Data.DbType.String, "@MenuDescription", "ROOT of Master [" + _name + "]");
            command.Parameters.Add(prMenuDescription);
            #endregion
            #region MenuNavigationURL
            DbParameter prNavigationURL = database.GetParameter(System.Data.DbType.String, "@MenuNavigationURL", "#ROOT");
            command.Parameters.Add(prNavigationURL);
            #endregion
            #region MenuOrder
            DbParameter prMenuOrder = database.GetParameter(System.Data.DbType.Int32, "@MenuOrder", 0);
            command.Parameters.Add(prMenuOrder);
            #endregion

            #region Command Insert Data 
            command.CommandType = System.Data.CommandType.StoredProcedure;
            string strQuery = "MenuMaster_Insert";
            command.CommandText = strQuery;
            #endregion
            return command;
        }

        protected override System.Data.Common.DbCommand GetUpdateCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region MenuMasterId
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@MenuMasterId", _id);
            command.Parameters.Add(prId);
            #endregion
            #region MenuMasterName
            DbParameter prName = database.GetParameter(System.Data.DbType.String, "@MenuMasterName", _name);
            command.Parameters.Add(prName);
            #endregion
            #region MenuMasterDescription
            DbParameter prDescription = database.GetParameter(System.Data.DbType.String, "@MenuMasterDescription", _description);
            command.Parameters.Add(prDescription);
            #endregion

            #region Command Update Data
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "MenuMaster_Update";
            #endregion

            return command;
        }

        protected override System.Data.Common.DbCommand GetDeleteCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region Command Delete Data
            #region MenuMasterId
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@MenuMasterId", _id);
            command.Parameters.Add(prId);
            #endregion

            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "MenuMaster_Delete";
            #endregion

            return command;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(MenuMaster)
                && ((MenuMaster)obj)._id == _id
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