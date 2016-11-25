using System;
using PSCPortal.Framework;
using System.Data.Common;
namespace PSCPortal.Portlets.TabTopicDisplay.Lib
{
    [Serializable]
    public class TabTopicDisplayCollection : BusinessObjectCollection<TabTopicDisplayCollection, TabTopicDisplay>
    {
        private TabTopicDisplayCollection()
        {
        }

        protected override DbCommand GetSelectAllCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            command.CommandText = "SELECT [DataId],[TopicId],[NumberDisplay],[TabOrder] FROM dbo.PortletTabTopicDisplay order by TabOrder Asc ";
            return command;
        }

        public static TabTopicDisplayCollection GetTabTopicDisplayCollection()
        {
            Database database = new Database(ConnectionStringName);
            TabTopicDisplayCollection result = new TabTopicDisplayCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = result.GetSelectAllCommand();
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    TabTopicDisplay item = new TabTopicDisplay(reader);
                    result.Add(item);
                }
            }
            return result;
        }
        public static TabTopicDisplayCollection GetTabTopicDisplayCollectionByDataId(Guid DataId)
        {
            Database database = new Database(ConnectionStringName);
            TabTopicDisplayCollection result = new TabTopicDisplayCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();
                DbParameter prDataId = database.GetParameter();
                prDataId.DbType = System.Data.DbType.Guid;
                prDataId.Direction = System.Data.ParameterDirection.InputOutput;
                prDataId.ParameterName = "@DataId";
                prDataId.Value = DataId;
                command.Parameters.Add(prDataId);
                command.CommandText = @"SELECT [DataId],[TopicId],[NumberDisplay],[TabOrder] 
                                        FROM dbo.PortletTabTopicDisplay
                                        Where DataId=@DataId order by TabOrder Asc";
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    TabTopicDisplay item = new TabTopicDisplay(reader);
                    result.Add(item);
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