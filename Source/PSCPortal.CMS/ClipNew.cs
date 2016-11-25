#region ClipNew
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;
namespace PSCPortal.CMS
{
    [Serializable]
    public class ClipNew : PSCPortal.Framework.BusinessObject<ClipNew>
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
        private bool _editClip;
        public bool EditClip
        {
            get
            {
                return _editClip;
            }
            set
            {
                _editClip = value;
            }
        }
        private DateTime _dateUpload;
        public DateTime DateUpload
        {
            get
            {
                return _dateUpload;
            }
            set
            {
                _dateUpload = value;
            }
        }

        private string _link = string.Empty;
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

        private bool _isPublish;
        public bool IsPublish
        {
            get
            {
                return _isPublish;
            }
            set
            {
                _isPublish = value;
            }
        }
        public string PathImage
        {
            get
            {
                if (_isPublish)
                {
                    return "Images/choduyet.png";
                }
                else
                {
                    return "Images/boduyet.png";
                }
            }
        }
        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }
        private byte[] _picture;
        public byte[] Picture
        {
            get
            {
                return _picture;
            }
            set
            {
                _picture = value;
            }
        }

        private FolderClipNew _folder;
        public FolderClipNew Folder
        {
            get
            {
                return _folder;
            }
            set
            {
                _folder = value;
            }
        }
        #endregion

        #region Constructions
        public ClipNew()
            : base()
        {
        }

        public ClipNew(DbDataReader reader)
            : base(reader)
        {
        }
        #endregion
        #region Abstract Methods
        protected override void MappingData(System.Data.Common.DbDataReader reader)
        {
            _id = (Guid)reader["ClipNewId"];
            _name = (string)reader["ClipNewName"];
            _dateUpload = (DateTime)reader["DateUpload"];
            _link = (string)reader["Link"];
            _description = (string) reader["Description"];
            _isPublish = (bool)reader["IsPublish"];
            _folder = new FolderClipNew() { Id = (Guid)reader["FolderId"] };
        }
        protected override System.Data.Common.DbCommand GetInsertCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region ClipNewId
            DbParameter prId = database.GetParameter();
            prId.DbType = System.Data.DbType.Guid;
            prId.Direction = System.Data.ParameterDirection.InputOutput;
            prId.ParameterName = "@ClipNewId";
            prId.Value = _id;
            command.Parameters.Add(prId);
            #endregion
            #region ClipNewName
            DbParameter prName = database.GetParameter();
            prName.DbType = System.Data.DbType.String;
            prName.Direction = System.Data.ParameterDirection.InputOutput;
            prName.ParameterName = "@ClipNewName";
            prName.Value = _name;
            command.Parameters.Add(prName);
            #endregion
            #region ClipNewDateUpload
            DbParameter prDateUpload = database.GetParameter();
            prDateUpload.DbType = System.Data.DbType.DateTime;
            prDateUpload.Direction = System.Data.ParameterDirection.InputOutput;
            prDateUpload.ParameterName = "@DateUpload";
            prDateUpload.Value = _dateUpload;
            command.Parameters.Add(prDateUpload);
            #endregion
            #region ClipNewLink
            DbParameter prLink = database.GetParameter();
            prLink.DbType = System.Data.DbType.String;
            prLink.Direction = System.Data.ParameterDirection.InputOutput;
            prLink.ParameterName = "@Link";
            prLink.Value = _link;
            command.Parameters.Add(prLink);
            #endregion

            #region ClipNewDescription
            DbParameter prDescription = database.GetParameter();
            prDescription.DbType = System.Data.DbType.String;
            prDescription.Direction = System.Data.ParameterDirection.InputOutput;
            prDescription.ParameterName = "@Description";
            prDescription.Value = _description;
            command.Parameters.Add(prDescription);
            #endregion

            //#region ClipNewPicture
            //DbParameter prPicture = database.GetParameter();
            //prPicture.DbType = System.Data.DbType.Binary;
            //prPicture.Direction = System.Data.ParameterDirection.InputOutput;
            //prPicture.ParameterName = "@Picture";
            //prPicture.Value = _picture;
            //command.Parameters.Add(prPicture);
            //#endregion

            #region ClipNewIsPublish
            DbParameter prIsPublish = database.GetParameter();
            prIsPublish.DbType = System.Data.DbType.Boolean;
            prIsPublish.Direction = System.Data.ParameterDirection.InputOutput;
            prIsPublish.ParameterName = "@IsPublish";
            prIsPublish.Value = _isPublish;
            command.Parameters.Add(prIsPublish);
            #endregion

            #region FolderId
            DbParameter prTypeId = database.GetParameter(System.Data.DbType.Guid, "@FolderId", _folder.Id);
            command.Parameters.Add(prTypeId);
            #endregion

            #region Command Insert Data
            command.CommandText = "INSERT INTO [ClipNew] ([ClipNewId],[ClipNewName],[DateUpload],[Link],[IsPublish],[Description],[FolderId]) VALUES (@ClipNewId,@ClipNewName,@DateUpload,@Link,@IsPublish,@Description,@FolderId)";
            #endregion

            return command;
        }

        protected override System.Data.Common.DbCommand GetUpdateCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region ClipNewId
            DbParameter prId = database.GetParameter();
            prId.DbType = System.Data.DbType.Guid;
            prId.Direction = System.Data.ParameterDirection.InputOutput;
            prId.ParameterName = "@ClipNewId";
            prId.Value = _id;
            command.Parameters.Add(prId);
            #endregion
            #region ClipNewName
            DbParameter prName = database.GetParameter();
            prName.DbType = System.Data.DbType.String;
            prName.Direction = System.Data.ParameterDirection.InputOutput;
            prName.ParameterName = "@ClipNewName";
            prName.Value = _name;
            command.Parameters.Add(prName);
            #endregion
            #region ClipNewDateUpload
            DbParameter prDateUpload = database.GetParameter();
            prDateUpload.DbType = System.Data.DbType.DateTime;
            prDateUpload.Direction = System.Data.ParameterDirection.InputOutput;
            prDateUpload.ParameterName = "@DateUpload";
            prDateUpload.Value = _dateUpload;
            command.Parameters.Add(prDateUpload);
            #endregion
            #region ClipNewLink
            DbParameter prLink = database.GetParameter();
            prLink.DbType = System.Data.DbType.String;
            prLink.Direction = System.Data.ParameterDirection.InputOutput;
            prLink.ParameterName = "@Link";
            prLink.Value = _link;
            command.Parameters.Add(prLink);
            #endregion

            #region ClipNewDescription
            DbParameter prDescription = database.GetParameter();
            prDescription.DbType = System.Data.DbType.String;
            prDescription.Direction = System.Data.ParameterDirection.InputOutput;
            prDescription.ParameterName = "@Description";
            prDescription.Value = _description;
            command.Parameters.Add(prDescription);
            #endregion
            
            #region ClipNewIsPublish
            DbParameter prIsPublish = database.GetParameter();
            prIsPublish.DbType = System.Data.DbType.Boolean;
            prIsPublish.Direction = System.Data.ParameterDirection.InputOutput;
            prIsPublish.ParameterName = "@IsPublish";
            prIsPublish.Value = _isPublish;
            command.Parameters.Add(prIsPublish);
            #endregion

            #region FolderId
            DbParameter prTypeId = database.GetParameter(System.Data.DbType.Guid, "@FolderId", _folder.Id);
            command.Parameters.Add(prTypeId);
            #endregion
            if (_editClip == true)
            {
               /* #region ClipNewPicture
                DbParameter prPicture = database.GetParameter();
                prPicture.DbType = System.Data.DbType.Binary;
                prPicture.Direction = System.Data.ParameterDirection.InputOutput;
                prPicture.ParameterName = "@Picture";
                prPicture.Value = _picture;
                command.Parameters.Add(prPicture);
                #endregion*/
                #region Command Update Data
                command.CommandText = "UPDATE [ClipNew] SET [ClipNewName] = @ClipNewName, [DateUpload] = @DateUpload, [Link] = @Link, [IsPublish] = @IsPublish , [Description]=@Description WHERE [ClipNewId] = @ClipNewId";
                #endregion
            }
            else
            {
                command.CommandText = "UPDATE [ClipNew] SET [ClipNewName] = @ClipNewName, [DateUpload] = @DateUpload, [Link] = @Link, [IsPublish] = @IsPublish , [Description]=@Description, FolderId=@FolderId WHERE [ClipNewId] = @ClipNewId";
            }

            return command;
        }

        protected override System.Data.Common.DbCommand GetDeleteCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region Command Delete Data
            #region ClipNewId
            DbParameter prId = database.GetParameter();
            prId.DbType = System.Data.DbType.Guid;
            prId.Direction = System.Data.ParameterDirection.InputOutput;
            prId.ParameterName = "@ClipNewId";
            prId.Value = _id;
            command.Parameters.Add(prId);
            #endregion

            command.CommandText = "DELETE [ClipNew] WHERE [ClipNewId] = @ClipNewId";
            #endregion

            return command;
        }

        public void InsertPicture(byte[] picture)
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            
            command.Connection = database.GetConnection();
            command.Connection.Open();

            #region ClipNewsClipId
            DbParameter prClipId = database.GetParameter();
            prClipId.DbType = System.Data.DbType.Guid;
            prClipId.Direction = System.Data.ParameterDirection.InputOutput;
            prClipId.ParameterName = "@ClipId";
            prClipId.Value = _id;
            command.Parameters.Add(prClipId);
            #endregion

            #region picture
            DbParameter picturePara = database.GetParameter();
            picturePara.DbType = System.Data.DbType.Binary;
            picturePara.Direction = System.Data.ParameterDirection.InputOutput;
            picturePara.ParameterName = "@picture";
            picturePara.Value = picture;
            command.Parameters.Add(picturePara);
            #endregion

            #region Command Update Data
            command.CommandText = "Insert Into ClipImage(ClipNewId,Picture) values(@ClipId,@picture)";
            command.ExecuteNonQuery();
            #endregion
            command.Connection.Close();
        }
        public static void DeletetPicture(Guid Id)
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            command.Connection = database.GetConnection();
            command.Connection.Open();
            command.CommandText = "Delete from ClipImage where [ClipNewid]= '" + Id.ToString() + "'";
            command.ExecuteNonQuery();
            command.Connection.Close();

        }
        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(ClipNew)
                && ((ClipNew)obj)._id == _id
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
        public static byte[] GetPicture(Guid Id)
        {
            Database database = new Database(ConnectionStringName);

            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();
                command.CommandText = "select [picture] from ClipImage where [ClipNewid]= '" + Id.ToString() + "'";
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    byte[] result = (byte[])reader["picture"];
                    return result;
                }
            }
            return null;
        }       
        public static ClipNew GetClipNew(Guid Id)
        {
            Database database = new Database(ConnectionStringName);

            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();
                command.CommandText = "SELECT  [ClipNewId],[ClipNewName],[DateUpload],[Link],[IsPublish],[Description],[FolderId] FROM dbo.ClipNew where [ClipNewId] = '" + Id.ToString() + "'";
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ClipNew item = new ClipNew(reader);
                    return item;
                }
            }
            return null;
        }
        
        #endregion
    }
}
#endregion