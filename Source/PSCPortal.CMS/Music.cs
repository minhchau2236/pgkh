using PSCPortal.Framework;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace PSCPortal.CMS
{
    [Serializable]
    public class Music : PSCPortal.Framework.BusinessObject<Music>
    {
        #region Properties
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreationDateString { get; set; }
        public int Priority { get; set; }
        #endregion

        #region Constructions
        public Music()
            : base()
        {
        }

        public Music(DbDataReader reader)
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

            #region Command Insert Data
            command.CommandText = "insert into [Music] ([Id], [Title], [Path], [CreationDate], [Priority]) VALUES (@Id, @Title, @Path, @CreationDate, @Priority)";
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

            #region Command Update Data
            command.CommandText = "update [Music] set [Title]=@Title, [Path]=@Path, [Priority]=@Priority where [Id]=@Id";
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
            command.CommandText = "delete from [Music] where [Id]=@Id";
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
