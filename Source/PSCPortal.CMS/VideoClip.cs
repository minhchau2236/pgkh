using PSCPortal.Framework;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace PSCPortal.CMS
{
    [Serializable]
    public class VideoClip : PSCPortal.Framework.BusinessObject<VideoClip>
    {
        #region Properties
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreationDateString { get; set; }
        public int Priority { get; set; }
        public string FileExtension { get; set; }
        #endregion

        #region Constructions
        public VideoClip()
            : base()
        {
        }

        public VideoClip(DbDataReader reader)
            : base(reader)
        {
        }
        #endregion

        #region Abstract Methods
        protected override void MappingData(System.Data.Common.DbDataReader reader)
        {
            if (!reader.IsDBNull(reader.GetOrdinal("Id")))
                Id = (Guid)reader["Id"];
            if (!reader.IsDBNull(reader.GetOrdinal("Title")))
                Title = (string)reader["Title"];
            if (!reader.IsDBNull(reader.GetOrdinal("Path")))
                Path = (string)reader["Path"];
            if (!reader.IsDBNull(reader.GetOrdinal("CreationDate")))
            {
                CreationDate = (DateTime)reader["CreationDate"];
                CreationDateString = string.Format("{0:dd/MM/yyyy}", (DateTime)reader["CreationDate"]);
            }
            if (!reader.IsDBNull(reader.GetOrdinal("Priority")))
                Priority = (int)reader["Priority"];
            if (!reader.IsDBNull(reader.GetOrdinal("FileExtension")))
                FileExtension = (string)reader["FileExtension"];
        }

        protected override System.Data.Common.DbCommand GetInsertCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();

            #region Id
            DbParameter prId = database.GetParameter();
            prId.DbType = System.Data.DbType.Guid;
            prId.Direction = System.Data.ParameterDirection.InputOutput;
            prId.ParameterName = "@Id";
            prId.Value = Id;
            command.Parameters.Add(prId);
            #endregion

            #region Title
            DbParameter prTitle = database.GetParameter();
            prTitle.DbType = System.Data.DbType.String;
            prTitle.Direction = System.Data.ParameterDirection.InputOutput;
            prTitle.ParameterName = "@Title";
            prTitle.Value = Title;
            command.Parameters.Add(prTitle);
            #endregion

            #region Path
            DbParameter prPath = database.GetParameter(System.Data.DbType.String, "@Path", Path);
            command.Parameters.Add(prPath);
            #endregion

            #region CreationDate
            DbParameter prCreationDate = database.GetParameter(System.Data.DbType.DateTime, "@CreationDate", CreationDate);
            command.Parameters.Add(prCreationDate);
            #endregion

            #region Priority
            DbParameter prPriority = database.GetParameter(System.Data.DbType.String, "@Priority", Priority);
            command.Parameters.Add(prPriority);
            #endregion

            #region FileExtension
            DbParameter prFileExtension = database.GetParameter(System.Data.DbType.String, "@FileExtension", FileExtension);
            command.Parameters.Add(prFileExtension);
            #endregion

            #region Command Insert Data
            command.CommandText = "insert into [VideoClip] ([Id], [Title], [Path], [CreationDate], [Priority], [FileExtension]) VALUES (@Id, @Title, @Path, @CreationDate, @Priority, @FileExtension)";
            #endregion

            return command;
        }

        protected override System.Data.Common.DbCommand GetUpdateCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();

            #region Id
            DbParameter prId = database.GetParameter();
            prId.DbType = System.Data.DbType.Guid;
            prId.Direction = System.Data.ParameterDirection.InputOutput;
            prId.ParameterName = "@Id";
            prId.Value = Id;
            command.Parameters.Add(prId);
            #endregion

            #region Title
            DbParameter prTitle = database.GetParameter();
            prTitle.DbType = System.Data.DbType.String;
            prTitle.Direction = System.Data.ParameterDirection.InputOutput;
            prTitle.ParameterName = "@Title";
            prTitle.Value = Title;
            command.Parameters.Add(prTitle);
            #endregion

            #region Path
            DbParameter prPath = database.GetParameter(System.Data.DbType.String, "@Path", Path);
            command.Parameters.Add(prPath);
            #endregion

            #region Priority
            DbParameter prPriority = database.GetParameter(System.Data.DbType.String, "@Priority", Priority);
            command.Parameters.Add(prPriority);
            #endregion

            #region FileExtension
            DbParameter prFileExtension = database.GetParameter(System.Data.DbType.String, "@FileExtension", FileExtension);
            command.Parameters.Add(prFileExtension);
            #endregion

            #region Command Update Data
            command.CommandText = "update [VideoClip] set [Title]=@Title, [Path]=@Path, [Priority]=@Priority, [FileExtension]=@FileExtension where [Id]=@Id";
            #endregion

            return command;
        }

        protected override System.Data.Common.DbCommand GetDeleteCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();

            #region Id
            DbParameter prId = database.GetParameter();
            prId.DbType = System.Data.DbType.Guid;
            prId.Direction = System.Data.ParameterDirection.InputOutput;
            prId.ParameterName = "@Id";
            prId.Value = Id;
            command.Parameters.Add(prId);
            #endregion

            #region Command Delate Data
            command.CommandText = "delete from [VideoClip] where [Id]=@Id";
            #endregion

            return command;
        }
        #endregion


        protected static new string ConnectionStringName
        {
            get
            {
                return "PSCPortalConnectionString";
            }
        }
    }
}
