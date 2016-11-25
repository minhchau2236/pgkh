using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;
namespace PSCPortal.Engine
{
    [Serializable]
    public class Page : PSCPortal.Framework.BusinessObject<Page>
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

        private string _title = string.Empty;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                _title = value;
            }
        }

        private int _template;
        public int Template
        {
            get
            {
                return _template;
            }
            set
            {
                _template = value;
            }
        }

        private int _language;
        public int Language
        {
            get
            {
                return _language;
            }
            set
            {
                _language = value;
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
        private Guid _layoutId;
        public Guid LayoutId
        {
            get
            {
                return _layoutId;
            }
            set
            {
                _layoutId = value;
            }
        }
        #endregion

        #region Constructions
        public Page()
            : base()
        {
        }

        public Page(DbDataReader reader)
            : base(reader)
        {
        }
        #endregion
        #region Abstract Methods
        protected override void MappingData(System.Data.Common.DbDataReader reader)
        {
            _id = (Guid)reader["PageId"];
            _name = (string)reader["PageName"];
            _title = (string)reader["PageTitle"];
           
            if (!reader.IsDBNull(reader.GetOrdinal("PageLanguage")))
                _language = (int) reader["PageLanguage"];
            if (!reader.IsDBNull(reader.GetOrdinal("PageTemplate")))
                _template = (int)reader["PageTemplate"];
        }
        protected override System.Data.Common.DbCommand GetInsertCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region PageId
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@PageId", _id);
            command.Parameters.Add(prId);
            #endregion
            #region PageName
            DbParameter prName = database.GetParameter(System.Data.DbType.String, "@PageName", _name);
            command.Parameters.Add(prName);
            #endregion
            #region PageTitle
            DbParameter prTitle = database.GetParameter(System.Data.DbType.String, "@PageTitle", _title);
            command.Parameters.Add(prTitle);
            #endregion
            #region PageTemplate
            DbParameter prTemplate = database.GetParameter(System.Data.DbType.Int32, "@PageTemplate", _template);
            command.Parameters.Add(prTemplate);
            #endregion
            #region PageLanguage
            DbParameter prLanguage = database.GetParameter(System.Data.DbType.Int32, "@PageLanguage", _language);
            command.Parameters.Add(prLanguage);
            #endregion

            #region Command Insert Data            
            command.CommandText = "Page_Insert";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            #endregion

            return command;
        }

        protected override System.Data.Common.DbCommand GetUpdateCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region PageId
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@PageId", _id);
            command.Parameters.Add(prId);
            #endregion
            #region PageName
            DbParameter prName = database.GetParameter(System.Data.DbType.String, "@PageName", _name);
            command.Parameters.Add(prName);
            #endregion
            #region PageTitle
            DbParameter prTitle = database.GetParameter(System.Data.DbType.String, "@PageTitle", _title);
            command.Parameters.Add(prTitle);
            #endregion
            #region PageTemplate
            DbParameter prTemplate = database.GetParameter(System.Data.DbType.Int32, "@PageTemplate", _template);
            command.Parameters.Add(prTemplate);
            #endregion
            #region PageLanguage
            DbParameter prLanguage = database.GetParameter(System.Data.DbType.Int32, "@PageLanguage", _language);
            command.Parameters.Add(prLanguage);
            #endregion
            #region Command Update Data
            command.CommandText = "Page_Update";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            #endregion

            return command;
        }

        public void UpdatePageLayout(Guid layoutId)
        {
            Database database = new Database(ConnectionStringName);
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();
                #region PageId
                DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@PageId", _id);
                command.Parameters.Add(prId);
                #endregion

                #region LayouId
                DbParameter prLayoutId = database.GetParameter(System.Data.DbType.Guid, "@LayoutId", layoutId);
                command.Parameters.Add(prLayoutId);
                #endregion

                command.CommandText = "PageLayout_Update";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Connection = connection;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }


        protected override System.Data.Common.DbCommand GetDeleteCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region Command Delete Data
            #region PageId
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@PageId", _id);
            command.Parameters.Add(prId);
            #endregion

            command.CommandText = "Page_Delete";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            #endregion

            return command;
        }

        public static Page GetPage(Guid pageId)
        {
            Database database = new Database(ConnectionStringName);
            Page result = null;
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand(connection);
                #region PageId
                DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@PageId", pageId);
                command.Parameters.Add(prId);
                #endregion
                command.CommandText = "Page_GetById";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                    result = new Page(reader);
            }
            return result;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(Page)
                && ((Page)obj)._id == _id
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