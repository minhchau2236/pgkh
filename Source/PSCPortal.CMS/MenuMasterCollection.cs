using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PSCPortal.Framework;
using System.Data.Common;
namespace PSCPortal.CMS
{
    [Serializable]
    public class MenuMasterCollection : PSCPortal.Framework.BusinessObjectCollection<MenuMasterCollection, MenuMaster>
    {
        public MenuMasterCollection()
            : base()
        {
        }

        protected override DbCommand GetSelectAllCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();

            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "MenuMaster_GetAll";
            return command;
        }

        public static MenuMasterCollection GetMenuMasterCollection()
        {
            Database database = new Database("PSCPortalConnectionString");
            MenuMasterCollection result = new MenuMasterCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = result.GetSelectAllCommand();
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    MenuMaster item = new MenuMaster(reader);
                    result.Add(item);
                }
            }
            return result;
        }
        public void MenuMasterCopy(MenuMaster menuMaster, MenuMaster menuMasterCopy)
        {
            Database database = new Database("PSCPortalConnectionString");
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand(connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                #region SourceMenuMasterId
                DbParameter prSourceMenuMasterId = database.GetParameter(System.Data.DbType.Guid, "@SourceMenuMasterId", menuMaster.Id);
                command.Parameters.Add(prSourceMenuMasterId);
                #endregion

                #region DestMenuMasterId
                DbParameter prDestMenuMasterId = database.GetParameter(System.Data.DbType.Guid, "@DestMenuMasterId", menuMasterCopy.Id);
                command.Parameters.Add(prDestMenuMasterId);
                #endregion

                #region DestMenuMasterName
                DbParameter prDestMenuMasterName = database.GetParameter(System.Data.DbType.String, "@DestMenuMasterName", menuMasterCopy.Name);
                command.Parameters.Add(prDestMenuMasterName);
                #endregion

                #region DestMenuMasterDescription
                DbParameter prDestMenuMasterDescription = database.GetParameter(System.Data.DbType.String, "@DestMenuMasterDescription", menuMasterCopy.Description);
                command.Parameters.Add(prDestMenuMasterDescription);
                #endregion

                command.CommandText = "MenuMaster_CopyMenu";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                connection.Open();
                command.ExecuteNonQuery();
                Add(menuMasterCopy);
            }
        }
        public void MenuMakeTopic(MenuMaster menuMaster, Guid pageId)
        {
            Database database = new Database("PSCPortalConnectionString");
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand(connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                #region SourceMenuMasterId
                DbParameter prSourceMenuMasterId = database.GetParameter(System.Data.DbType.Guid, "@SourceMenuMasterId", menuMaster.Id);
                command.Parameters.Add(prSourceMenuMasterId);
                #endregion

                #region PageId
                DbParameter prPageId = database.GetParameter(System.Data.DbType.Guid, "@PageId", pageId);
                command.Parameters.Add(prPageId);
                #endregion

                command.CommandText = "MenuMaster_MakeTopic";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                connection.Open();
                command.ExecuteNonQuery();
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