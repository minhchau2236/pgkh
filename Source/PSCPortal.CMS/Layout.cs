using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;
namespace PSCPortal.CMS
{
    [Serializable]
    public class Layout : PSCPortal.Framework.BusinessObject<Layout>
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

        private string _navigationUrl = string.Empty;
        public string NavigationUrl
        {
            get
            {
                return _navigationUrl;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                _navigationUrl = value;
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

        #endregion

        #region Constructions
        public Layout()
            : base()
        {
        }

        public Layout(DbDataReader reader)
            : base(reader)
        {
        }
        #endregion
        #region Abstract Methods
        protected override void MappingData(System.Data.Common.DbDataReader reader)
        {
            _id = (Guid)reader["Id"];
            _name = (string)reader["Name"];
            _navigationUrl = reader["NavigationURL"].ToString() != null ? reader["NavigationURL"].ToString() : "";
        }
        protected override System.Data.Common.DbCommand GetInsertCommand()
        {

            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();

            #region LayoutId
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@LayoutId", _id);
            command.Parameters.Add(prId);
            #endregion

            #region LayoutName
            DbParameter prName = database.GetParameter(System.Data.DbType.String, "@LayoutName", _name);
            command.Parameters.Add(prName);
            #endregion

            #region Navigation
            DbParameter prNavigation = database.GetParameter(System.Data.DbType.String, "@Navigation", _navigationUrl);
            command.Parameters.Add(prNavigation);
            #endregion


            #region Command Insert Data
            command.CommandText = "Layout_Insert";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            #endregion

            return command;
        }

        protected override System.Data.Common.DbCommand GetUpdateCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region LayoutId
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@LayoutId", _id);
            command.Parameters.Add(prId);
            #endregion

            #region LayoutName
            DbParameter prName = database.GetParameter(System.Data.DbType.String, "@LayoutName", _name);
            command.Parameters.Add(prName);
            #endregion

            #region Command Update Data
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "Layout_Update";
            #endregion

            return command;
        }

        protected override System.Data.Common.DbCommand GetDeleteCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region Command Delete Data

            #region ArticleId
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@LayoutId", _id);
            command.Parameters.Add(prId);
            #endregion

            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "Delete from Layout  where Id = @LayoutId";
            #endregion

            return command;
        }
        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(Layout)
                && ((Layout)obj)._id == _id
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