using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;
namespace PSCPortal.CMS
{
    [Serializable]
    public class Menu : PSCPortal.Framework.BusinessObjectHierarchical<Menu>
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

        private string _navigationURL = string.Empty;
        public string NavigationURL
        {
            get
            {
                return _navigationURL;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                _navigationURL = value;
            }
        }        

        #endregion

        #region Constructions
        public Menu()
            : base()
        {
        }

        public Menu(DbDataReader reader)
            : base(reader)
        {
        }
        #endregion
        #region Abstract Methods
        protected override void MappingData(System.Data.Common.DbDataReader reader)
        {
            _id = (Guid)reader["MenuId"];
            _name = (string)reader["MenuName"];
            _description = (string)reader["MenuDescription"];
            _navigationURL = (string)reader["MenuNavigationURL"];   
        }
        protected override System.Data.Common.DbCommand GetInsertCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region MenuId
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@MenuId", _id);
            command.Parameters.Add(prId);
            #endregion
            #region MenuName
            DbParameter prName = database.GetParameter(System.Data.DbType.String, "@MenuName", _name);
            command.Parameters.Add(prName);
            #endregion
            #region MenuDescription
            DbParameter prDescription = database.GetParameter(System.Data.DbType.String, "@MenuDescription", _description);
            command.Parameters.Add(prDescription);
            #endregion
            #region MenuNavigationURL
            DbParameter prNavigationURL = database.GetParameter(System.Data.DbType.String, "@MenuNavigationURL", _navigationURL);
            command.Parameters.Add(prNavigationURL);
            #endregion     
            #region MenuParent
            DbParameter prParent = database.GetParameter(System.Data.DbType.Guid, "@MenuParent", ((Menu)_parent).Id);
            command.Parameters.Add(prParent);
            #endregion

            #region MenuOrder
            DbParameter prMenuOrder = database.GetParameter(System.Data.DbType.Int32, "@MenuOrder", ((Menu)_parent).Childs.Count + 1);
            command.Parameters.Add(prMenuOrder);
            #endregion

            #region Command Insert Data
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "Menu_Insert";
            #endregion

            return command;
        }

        protected override System.Data.Common.DbCommand GetUpdateCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region MenuId
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@MenuId", _id);
            command.Parameters.Add(prId);
            #endregion
            #region MenuName
            DbParameter prName = database.GetParameter(System.Data.DbType.String, "@MenuName", _name);
            command.Parameters.Add(prName);
            #endregion
            #region MenuDescription
            DbParameter prDescription = database.GetParameter(System.Data.DbType.String, "@MenuDescription", _description);
            command.Parameters.Add(prDescription);
            #endregion
            #region MenuNavigationURL
            DbParameter prNavigationURL = database.GetParameter(System.Data.DbType.String, "@MenuNavigationURL", _navigationURL);
            command.Parameters.Add(prNavigationURL);
            #endregion
            #region MenuParent
            DbParameter prParent = database.GetParameter(System.Data.DbType.Guid, "@MenuParent", ((Menu)_parent).Id);
            command.Parameters.Add(prParent);
            #endregion

            #region Command Update Data
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "Menu_Update";
            #endregion

            return command;
        }

        protected override System.Data.Common.DbCommand GetDeleteCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region Command Delete Data
            #region MenuId
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@MenuId", _id);
            command.Parameters.Add(prId);
            #endregion

            #region Command Delete Data
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "Menu_Delete";
            #endregion

            #endregion

            return command;
        }

        public static Menu GetMenu(string menuId)
        {
            Database database = new Database("PSCPortalConnectionString");
            Menu result = null;
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();
                #region menuId
                DbParameter prmenuId = database.GetParameter(System.Data.DbType.Guid, "@MenuId", new Guid(menuId));
                command.Parameters.Add(prmenuId);
                #endregion

                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "select * from Menu where MenuId=@MenuId";
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Menu tp = new Menu(reader);                    
                    result = tp;
                }
            }
            return result;
        }
        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(Menu)
                && ((Menu)obj)._id == _id
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