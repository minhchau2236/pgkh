#region ClipNewCollection
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PSCPortal.Framework;
using System.Data.Common;
namespace PSCPortal.CMS
{
    [Serializable]
    public class ClipNewCollection : PSCPortal.Framework.BusinessObjectCollection<ClipNewCollection, ClipNew>
    {
        private ClipNewCollection()
            : base()
        {
        }

        protected override DbCommand GetSelectAllCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            command.CommandText = "SELECT [ClipNewId],[ClipNewName],[DateUpload],[Link],[IsPublish],[Description],[FolderId]  FROM dbo.ClipNew order by DateUpload Desc";
            return command;
        }
       
        public static ClipNewCollection GetClipNewCollectionAll()
        {
            Database database = new Database(ConnectionStringName);
            ClipNewCollection result = new ClipNewCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = result.GetSelectAllCommand();
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ClipNew item = new ClipNew(reader);
                    result.Add(item);
                }
            }
            return result;
        }


        public static ClipNewCollection GetClipNewCollection(FolderClipNew folder)
        {
            Database database = new Database(ConnectionStringName);
            ClipNewCollection result = new ClipNewCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();
                command.CommandText = "select * from ClipNew where FolderId='" + folder.Id + "' order by DateUpload desc";
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ClipNew item = new ClipNew(reader);
                    result.Add(item);
                }
            }
            return result;
        }
        public static ClipNewCollection GetClipNewCollectionIsPublish(string folderId)
        {
            Database database = new Database(ConnectionStringName);
            ClipNewCollection result = new ClipNewCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();
                #region folderId
                DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@FolderId", new Guid(folderId));
                command.Parameters.Add(prId);
                #endregion
                command.CommandText = @"SELECT ClipNewId,ClipNewName,DateUpload,Link,IsPublish,Description,FolderId 
            FROM dbo.ClipNew cn where FolderId=@FolderId and IsPublish = 1 order by DateUpload Desc";
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ClipNew item = new ClipNew(reader);                    
                    byte[] pic = ClipNew.GetPicture(item.Id);
                    item.Picture = pic;
                    result.Add(item);
                }
            }
            return result;
        }

        public static ClipNewCollection GetClipNewCollectionIsPublish()
        {
            Database database = new Database(ConnectionStringName);
            ClipNewCollection result = new ClipNewCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();

                command.CommandText = @"SELECT ClipNewId,ClipNewName,DateUpload,Link,IsPublish,Description,FolderId 
            FROM dbo.ClipNew cn where IsPublish = 1 order by DateUpload Desc";
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ClipNew item = new ClipNew(reader);
                    byte[] pic = ClipNew.GetPicture(item.Id);
                    item.Picture = pic;
                    result.Add(item);
                }
            }
            return result;
        }
        public static ClipNewCollection GetClipNewCollectionIsPublish(int CountNumber)
        {
            Database database = new Database(ConnectionStringName);
            ClipNewCollection result = new ClipNewCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();
                command.CommandText = "SELECT top " + CountNumber.ToString() + " [ClipNewId],[ClipNewName],[DateUpload],[Link],[IsPublish],[Description],[FolderId] FROM dbo.ClipNew where [IsPublish] = 1 order by [DateUpload] desc";
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ClipNew item = new ClipNew(reader);
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
#endregion