using System;
using System.Data.Common;
using PSCPortal.Framework;
namespace PSCPortal.Portlets.TabTopicDisplay.Lib
{
    [Serializable]
    public class TabTopicDisplay : BusinessObject<TabTopicDisplay>
    {
        #region Properties
        private Guid _dataId;
        public Guid DataId
        {
            get
            {
                return _dataId;
            }
            set
            {
                _dataId = value;
            }
        }

        private Guid _topicId;
        public Guid TopicId
        {
            get
            {
                return _topicId;
            }
            set
            {
                _topicId = value;
            }
        }

        private int _numberDisplay;
        public int NumberDisplay
        {
            get
            {
                return _numberDisplay;
            }
            set
            {
                _numberDisplay = value;
            }
        }

        private int _tabOrder;
        public int TabOrder
        {
            get
            {
                return _tabOrder;
            }
            set
            {
                _tabOrder = value;
            }
        }

        #endregion

        #region Constructions
        public TabTopicDisplay()
        {
        }

        public TabTopicDisplay(DbDataReader reader)
            : base(reader)
        {
        }
        #endregion
        #region Abstract Methods
        protected override void MappingData(DbDataReader reader)
        {
            _dataId = (Guid)reader["DataId"];
            _topicId = (Guid)reader["TopicId"];
            _numberDisplay = (int)reader["NumberDisplay"];
            _tabOrder = (int)reader["TabOrder"];
        }
        protected override DbCommand GetInsertCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region TabTopicDisplayDataId
            DbParameter prDataId = database.GetParameter();
            prDataId.DbType = System.Data.DbType.Guid;
            prDataId.Direction = System.Data.ParameterDirection.InputOutput;
            prDataId.ParameterName = "@DataId";
            prDataId.Value = _dataId;
            command.Parameters.Add(prDataId);
            #endregion
            #region TabTopicDisplayTopicId
            DbParameter prTopicId = database.GetParameter();
            prTopicId.DbType = System.Data.DbType.Guid;
            prTopicId.Direction = System.Data.ParameterDirection.InputOutput;
            prTopicId.ParameterName = "@TopicId";
            prTopicId.Value = _topicId;
            command.Parameters.Add(prTopicId);
            #endregion
            #region TabTopicDisplayNumberDisplay
            DbParameter prNumberDisplay = database.GetParameter();
            prNumberDisplay.DbType = System.Data.DbType.Int32;
            prNumberDisplay.Direction = System.Data.ParameterDirection.InputOutput;
            prNumberDisplay.ParameterName = "@NumberDisplay";
            prNumberDisplay.Value = _numberDisplay;
            command.Parameters.Add(prNumberDisplay);
            #endregion
            #region TabTopicDisplayTabOrder
            DbParameter prTabOrder = database.GetParameter();
            prTabOrder.DbType = System.Data.DbType.Int32;
            prTabOrder.Direction = System.Data.ParameterDirection.InputOutput;
            prTabOrder.ParameterName = "@TabOrder";
            prTabOrder.Value = _tabOrder;
            command.Parameters.Add(prTabOrder);
            #endregion

            #region Command Insert Data
            command.CommandText = "INSERT INTO [PortletTabTopicDisplay] ([DataId],[TopicId],[NumberDisplay],[TabOrder]) VALUES (@DataId,@TopicId,@NumberDisplay,@TabOrder)";
            #endregion

            return command;
        }

        protected override DbCommand GetUpdateCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region TabTopicDisplayDataId
            DbParameter prDataId = database.GetParameter();
            prDataId.DbType = System.Data.DbType.Guid;
            prDataId.Direction = System.Data.ParameterDirection.InputOutput;
            prDataId.ParameterName = "@DataId";
            prDataId.Value = _dataId;
            command.Parameters.Add(prDataId);
            #endregion
            #region TabTopicDisplayTopicId
            DbParameter prTopicId = database.GetParameter();
            prTopicId.DbType = System.Data.DbType.Guid;
            prTopicId.Direction = System.Data.ParameterDirection.InputOutput;
            prTopicId.ParameterName = "@TopicId";
            prTopicId.Value = _topicId;
            command.Parameters.Add(prTopicId);
            #endregion
            #region TabTopicDisplayNumberDisplay
            DbParameter prNumberDisplay = database.GetParameter();
            prNumberDisplay.DbType = System.Data.DbType.Int32;
            prNumberDisplay.Direction = System.Data.ParameterDirection.InputOutput;
            prNumberDisplay.ParameterName = "@NumberDisplay";
            prNumberDisplay.Value = _numberDisplay;
            command.Parameters.Add(prNumberDisplay);
            #endregion
            #region TabTopicDisplayTabOrder
            DbParameter prTabOrder = database.GetParameter();
            prTabOrder.DbType = System.Data.DbType.Int32;
            prTabOrder.Direction = System.Data.ParameterDirection.InputOutput;
            prTabOrder.ParameterName = "@TabOrder";
            prTabOrder.Value = _tabOrder;
            command.Parameters.Add(prTabOrder);
            #endregion

            #region Command Update Data
            command.CommandText = "UPDATE [PortletTabTopicDisplay] SET [TopicId] = @TopicId, [NumberDisplay] = @NumberDisplay, [TabOrder] = @TabOrder WHERE [DataId] = @DataId and TopicId=@TopicId";
            #endregion

            return command;
        }

        protected override DbCommand GetDeleteCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region Command Delete Data
            #region TabTopicDisplayDataId
            DbParameter prDataId = database.GetParameter();
            prDataId.DbType = System.Data.DbType.Guid;
            prDataId.Direction = System.Data.ParameterDirection.InputOutput;
            prDataId.ParameterName = "@DataId";
            prDataId.Value = _dataId;
            command.Parameters.Add(prDataId);
            #endregion
            #region TabTopicDisplayTopicId
            DbParameter prTopicId = database.GetParameter();
            prTopicId.DbType = System.Data.DbType.Guid;
            prTopicId.Direction = System.Data.ParameterDirection.InputOutput;
            prTopicId.ParameterName = "@TopicId";
            prTopicId.Value = _topicId;
            command.Parameters.Add(prTopicId);
            #endregion
            command.CommandText = "DELETE [PortletTabTopicDisplay] WHERE [DataId] = @DataId and TopicId=@TopicId";
            #endregion

            return command;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(TabTopicDisplay)
                && ((TabTopicDisplay)obj)._dataId == _dataId
               )
                return true;
            return false;
        }
        public override int GetHashCode()
        {
            int hashCode = 0;
            hashCode += _dataId.GetHashCode();
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