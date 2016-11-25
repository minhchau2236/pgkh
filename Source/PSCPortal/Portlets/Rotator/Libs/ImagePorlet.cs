using System;
using System.Data.Common;
using PSCPortal.Framework;

namespace PSCPortal.Portlets.Rotator.Libs
{
    [Serializable]
    public class ImagePortlet : BusinessObject<ImagePortlet>
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
        private string _url = string.Empty;
        public string Url
        {
            get
            {
                return _url;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                _url = value;
            }
        }
        private string _link;
        public string Link
        {
            get
            {
                return _link;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                _link = value;
            }
        }
        private int _order;
        public int Order
        {
            get
            {
                return _order;
            }
            set
            {
                _order = value;
            }
        }
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
        #endregion
        #region Constructions
        public ImagePortlet()
        {
        }

        public ImagePortlet(DbDataReader reader)
            : base(reader)
        {
        }
        #endregion
        #region Abstract Methods
        protected override void MappingData(DbDataReader reader)
        {
            _id = (Guid)reader["Id"];
            _title = (string)reader["Title"];
            _url = (string)reader["ImageUrl"];
            _link = (string)reader["Link"];
            _order = (int)reader["Order"];
            _dataId = (Guid) reader["DataId"];
        }
        protected override DbCommand GetInsertCommand()
        {
            var database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region Id
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@Id", _id);
            command.Parameters.Add(prId);
            #endregion
            #region Title
            DbParameter prTitle = database.GetParameter(System.Data.DbType.String, "@Title", _title);
            command.Parameters.Add(prTitle);
            #endregion
            #region Url
            DbParameter prUrl = database.GetParameter(System.Data.DbType.String, "@ImageUrl", _url);
            command.Parameters.Add(prUrl);
            #endregion
            #region Order
            DbParameter prOrder = database.GetParameter(System.Data.DbType.Int32, "@Order", _order);
            command.Parameters.Add(prOrder);
            #endregion
            #region Link
            DbParameter prLink = database.GetParameter(System.Data.DbType.String, "@Link", _link);
            command.Parameters.Add(prLink);
            #endregion
            #region DataId
            DbParameter prDataId = database.GetParameter(System.Data.DbType.Guid, "@DataId", _dataId);
            command.Parameters.Add(prDataId);
            #endregion
            #region Command Insert Data
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = @"INSERT INTO [dbo].[PortletImage]
                                                                   ([Id]
                                                                   ,[Title]
                                                                   ,[ImageUrl]
                                                                   ,[Link]
                                                                   ,[Order]
                                                                   ,[DataId])
                                                             VALUES
                                                                   (@Id
                                                                   ,@Title
                                                                   ,@ImageUrl
                                                                   ,@Link
                                                                   ,@Order
                                                                   ,@DataId)";
            #endregion

            return command;
        }

        protected override DbCommand GetUpdateCommand()
        {
            var database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region Id
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@Id", _id);
            command.Parameters.Add(prId);
            #endregion
            #region Title
            DbParameter prTitle = database.GetParameter(System.Data.DbType.String, "@Title", _title);
            command.Parameters.Add(prTitle);
            #endregion
            #region Url
            DbParameter prUrl = database.GetParameter(System.Data.DbType.String, "@ImageUrl", _url);
            command.Parameters.Add(prUrl);
            #endregion
            #region Order
            DbParameter prOrder = database.GetParameter(System.Data.DbType.Int32, "@Order", _order);
            command.Parameters.Add(prOrder);
            #endregion
            #region Link
            DbParameter prLink = database.GetParameter(System.Data.DbType.String, "@Link", _link);
            command.Parameters.Add(prLink);
            #endregion
            #region DataId
            DbParameter prDataId = database.GetParameter(System.Data.DbType.Guid, "@DataId", _dataId);
            command.Parameters.Add(prDataId);
            #endregion
            #region Command Insert Data
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = @"UPDATE [dbo].[PortletImage]
                                       SET [Title] = @Title
                                          ,[ImageUrl] = @ImageUrl
                                          ,[Link] = @Link
                                          ,[Order] = @Order
                                          ,[DataId] = @DataId
                                     WHERE Id = @Id";
            #endregion

            return command;
        }

        protected override DbCommand GetDeleteCommand()
        {
            var database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region Command Delete Data
            #region Id
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@Id", _id);
            command.Parameters.Add(prId);
            #endregion
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "Delete PortletImage Where Id = @Id";
            #endregion

            return command;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(ImagePortlet)
                && ((ImagePortlet)obj)._id == _id
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
        public void Insert()
        {
            var database = new Database(ConnectionStringName);
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = GetInsertCommand();
                command.Connection = connection;
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public static int GetImageOrderMax(Guid dataId)
        {
           var database = new Database(ConnectionStringName);
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();
                #region DataId
                DbParameter prDataId = database.GetParameter(System.Data.DbType.Guid, "@DataId", dataId);
                command.Parameters.Add(prDataId);
                #endregion
                #region Command Insert Data
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = @"select MAX([Order]) from PortletImage Where DataId = @DataId";
                #endregion
                command.Connection = connection;
                connection.Open();
                object maxOrder = command.ExecuteScalar();
                if (maxOrder.ToString() != string.Empty)
                    return (int) maxOrder;
                return 0;
            }
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
