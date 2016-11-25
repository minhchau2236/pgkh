using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PSCPortal.Framework;
using System.Data.Common;
namespace PSCPortal.CMS
{
    [Serializable]
    public class MenuCollection : PSCPortal.Framework.BusinessObjectTree<MenuCollection, Menu>
    {
        protected MenuCollection(Menu root)
            : base(root)
        {
        }

        protected override DbCommand GetSelectAllCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            //command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "create table #temp(stt int identity(0,1), MenuId uniqueidentifier, MenuName nvarchar(250), MenuDescription nvarchar(250), MenuNavigationURL nvarchar(250),MenuOrder int, MenuParent uniqueidentifier); ";
            command.CommandText += "insert into #temp(MenuId, MenuName, MenuDescription, MenuNavigationURL,MenuOrder, MenuParent) ";
            command.CommandText += "select MenuId, MenuName, MenuDescription, MenuNavigationURL,MenuOrder, MenuParent ";
            command.CommandText += "from Menu ";
            command.CommandText += "where MenuParent is null; ";
            command.CommandText += "declare @index int; ";
            command.CommandText += "set @index = 0; ";
            command.CommandText += "declare @max int; ";
            command.CommandText += "set @max = (select max(stt) from #temp); ";
            command.CommandText += "declare @temp uniqueidentifier; ";
            command.CommandText += "while(@index<=@max) ";
            command.CommandText += "begin ";
            command.CommandText += "set @temp = (select MenuId from #temp where stt=@index); ";
            command.CommandText += "insert into #temp(MenuId, MenuName, MenuDescription, MenuNavigationURL,MenuOrder, MenuParent) ";
            command.CommandText += "select MenuId, MenuName, MenuDescription, MenuNavigationURL,MenuOrder, MenuParent ";
            command.CommandText += "from Menu ";
            command.CommandText += "where MenuParent=@temp; ";
            command.CommandText += "set @index=@index +1; ";
            command.CommandText += "set @max=(select max(stt) from #temp) order by MenuOrder; ";
            command.CommandText += "end ";
            command.CommandText += "select MenuId, MenuName, MenuDescription, MenuNavigationURL,MenuOrder, MenuParent ";
            command.CommandText += "from #temp ";
            command.CommandText += "where MenuParent is not null; ";
            return command;
        }
        public void MoveUp(Menu menu)
        {
            Menu menuParent = (Menu)menu.Parent;
            int index = 0;
            for (; index < menuParent.Childs.Count; index++) 
                if (menuParent.Childs[index].Equals(menu))                
                    break;

            menuParent.Childs.RemoveAt(index);
            menuParent.Childs.Insert(index - 1, menu);

            UpdatePostionChilds(menuParent);
        }
        public void MoveDown(Menu menu)
        {
            Menu menuParent = (Menu)menu.Parent;
            int index = 0;
            for (; index < menuParent.Childs.Count; index++)
                if (menuParent.Childs[index].Equals(menu))
                    break;

            menuParent.Childs.RemoveAt(index);
            menuParent.Childs.Insert(index + 1, menu);

            UpdatePostionChilds(menuParent);
        }

        public void UpdatePostionChilds(Menu menu)
        {
            Database database = new Database(ConnectionStringName);
            using (DbConnection connection = database.GetConnection())
            {
                List<DbCommand> commandList = new List<DbCommand>();
                int i = 1;
                foreach (Menu item in menu.Childs)
                {
                    DbCommand command = database.GetCommand(connection);
                    #region MenuId
                    DbParameter prMenuId = database.GetParameter(System.Data.DbType.Guid, "@MenuId", item.Id);
                    command.Parameters.Add(prMenuId);
                    #endregion

                    #region MenuOrder
                    DbParameter prMenuOrder = database.GetParameter(System.Data.DbType.Int32, "@MenuOrder", i++);
                    command.Parameters.Add(prMenuOrder);
                    #endregion

                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "Menu_UpdatePostion";
                    commandList.Add(command);
                }
                connection.Open();
                foreach (DbCommand cmd in commandList)
                    cmd.ExecuteNonQuery();
            }
        }
        public override void Remove(PSCPortal.Framework.Core.BusinessObjectHierarchical item)
        {
            base.Remove(item);
            UpdatePostionChilds((Menu)item.Parent);
        }
        protected static DbCommand GetSelectAllByMenuMasterIdCommand(Database database)
        {
            DbCommand command = database.GetCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "Menu_GetAllByMenuMasterId";
            return command;
        }
        public static MenuCollection GetMenuCollection(MenuMaster menuMaster)
        {
            
            Database database = new Database("PSCPortalConnectionString");
            MenuCollection result = new MenuCollection(new Menu());
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = GetSelectAllByMenuMasterIdCommand(database);
                #region MenuMasterId
                DbParameter MenuMasterId = database.GetParameter(System.Data.DbType.Guid, "@MenuMasterId", menuMaster.Id);
                command.Parameters.Add(MenuMasterId);
                #endregion
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Guid idParent = Guid.Empty;
                    if (reader["MenuParent"].ToString() != string.Empty)
                        idParent = (Guid)reader["MenuParent"];
                    if (idParent == System.Guid.Empty)
                    {
                        result = new MenuCollection(new Menu(reader));
                        continue;
                    }
                    Menu parent = (Menu)result.Search(d => ((Menu)d).Id == idParent);
                    Menu dir = new Menu(reader);
                    result.Add(parent, dir);
                }
            }
            return result;
        }

        /// <summary>
        /// //////////////////////
        /// </summary>
        /// <param name="database"></param>
        /// <returns></returns>
        protected static DbCommand GetMenuChildByMenuParent(Database database)
        {
            DbCommand command = database.GetCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "Menu_GetAllByMenuParentId";            
            return command;
        }
        public static MenuCollection GetMenuChildCollection(Guid menuParent)
        {
            Database database = new Database("PSCPortalConnectionString");
            MenuCollection result = new MenuCollection(Menu.GetMenu(menuParent.ToString()));
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = GetMenuChildByMenuParent(database);
                #region MenuId
                DbParameter MenuId = database.GetParameter(System.Data.DbType.Guid, "@MenuParent", menuParent);
                command.Parameters.Add(MenuId);
                #endregion
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Guid idParent = Guid.Empty;
                    if (reader["MenuParent"].ToString() != string.Empty)
                        idParent = (Guid)reader["MenuParent"];
                    if (idParent == System.Guid.Empty)
                    {
                        result = new MenuCollection(new Menu(reader));
                        continue;
                    }
                    // Menu parent = Menu.GetMenu(menuParent.ToString());
                    Menu parent = (Menu)result.Search(d => ((Menu)d).Id == idParent);
                    Menu dir = new Menu(reader);
                    result.Add(parent, dir);

                }
            }
            return result;
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