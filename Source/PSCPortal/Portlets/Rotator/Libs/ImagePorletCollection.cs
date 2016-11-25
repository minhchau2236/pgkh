using System;
using PSCPortal.Framework;
using System.Data.Common;
namespace PSCPortal.Portlets.Rotator.Libs
{
    [Serializable]
    public class ImagePortletCollection : BusinessObjectCollection<ImagePortletCollection, ImagePortlet>
    {
        private ImagePortletCollection()
        {
        }

        protected override DbCommand GetSelectAllCommand()
        {
            var database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            command.CommandText = "Select ImagePortletId, ImagePortletName, ImagePortletScript, ImagePortletCreatedDate, DataId From ImagePortlet Order By ImagePortletCreatedDate DESC";
            return command;
        }

        protected  DbCommand GetSelectCommand(Guid dataId)
        {
            var database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region DataId
            DbParameter prDataId = database.GetParameter(System.Data.DbType.Guid, "@DataId", dataId);
            command.Parameters.Add(prDataId);
            #endregion
            command.CommandText = "Select * From PortletImage Where DataId = @DataId Order By [Order] DESC";
            return command;
        }
        public static ImagePortletCollection GetImagePortletCollection(Guid dataId)
        {
            var database = new Database("PSCPortalConnectionString");
            var result = new ImagePortletCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = result.GetSelectCommand(dataId);
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var item = new ImagePortlet(reader);
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