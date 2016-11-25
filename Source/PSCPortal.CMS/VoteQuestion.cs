using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;
using System.Data;

namespace PSCPortal.CMS
{
    [Serializable]
    public class VoteQuestion : PSCPortal.Framework.BusinessObject<VoteQuestion>
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

        private Guid _SubDomainId;
        public Guid SubDomainId
        {
            get
            {
                return _SubDomainId;
            }
            set
            {
                _SubDomainId = value;
            }
        }

        private bool _isActive;
        public bool IsActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                _isActive = value;
            }
        }

        #endregion

        #region Constructions
        public VoteQuestion()
            : base()
        {
        }

        public VoteQuestion(DbDataReader reader)
            : base(reader)
        {
        }
        #endregion
        #region Abstract Methods

        protected override void MappingData(System.Data.Common.DbDataReader reader)
        {
            if (!reader.IsDBNull(reader.GetOrdinal("VoteQuestionId")))
                _id = (Guid) reader["VoteQuestionId"];
            if (!reader.IsDBNull(reader.GetOrdinal("VoteQuestionName")))
                _name = (string) reader["VoteQuestionName"];
            if (!reader.IsDBNull(reader.GetOrdinal("SubDomainId")))
                _SubDomainId = (Guid) reader["SubDomainId"];
            if (!reader.IsDBNull(reader.GetOrdinal("IsActive")))
                _isActive = (bool) reader["IsActive"];
        }

        protected override System.Data.Common.DbCommand GetInsertCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region VoteQuestionId
            DbParameter prId = database.GetParameter();
            prId.DbType = System.Data.DbType.Guid;
            prId.Direction = System.Data.ParameterDirection.InputOutput;
            prId.ParameterName = "@VoteQuestionId";
            prId.Value = _id;
            command.Parameters.Add(prId);
            #endregion
            #region VoteQuestionName
            DbParameter prName = database.GetParameter();
            prName.DbType = System.Data.DbType.String;
            prName.Direction = System.Data.ParameterDirection.InputOutput;
            prName.ParameterName = "@VoteQuestionName";
            prName.Value = _name;
            command.Parameters.Add(prName);
            #endregion

            #region IsActive
            DbParameter prIsActive = database.GetParameter(System.Data.DbType.Boolean, "@IsActive", _isActive);
            command.Parameters.Add(prIsActive);
            #endregion

            #region SubDomainId
            DbParameter prSubDomainId = database.GetParameter(System.Data.DbType.Guid, "@SubDomainId", _SubDomainId);
            command.Parameters.Add(prSubDomainId);
            #endregion

            #region Command Insert Data
            command.CommandText = "INSERT INTO [VoteQuestion] ([VoteQuestionId],[VoteQuestionName],[SubDomainId],[IsActive]) VALUES (@VoteQuestionId,@VoteQuestionName,@SubDomainId,@IsActive)";
            #endregion

            return command;
        }

        protected override System.Data.Common.DbCommand GetUpdateCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region VoteQuestionId
            DbParameter prId = database.GetParameter();
            prId.DbType = System.Data.DbType.Guid;
            prId.Direction = System.Data.ParameterDirection.InputOutput;
            prId.ParameterName = "@VoteQuestionId";
            prId.Value = _id;
            command.Parameters.Add(prId);
            #endregion
            #region VoteQuestionName
            DbParameter prName = database.GetParameter();
            prName.DbType = System.Data.DbType.String;
            prName.Direction = System.Data.ParameterDirection.InputOutput;
            prName.ParameterName = "@VoteQuestionName";
            prName.Value = _name;
            command.Parameters.Add(prName);
            #endregion

            #region SubDomainId
            DbParameter prSubDomainId = database.GetParameter(System.Data.DbType.Guid, "@SubDomainId", _SubDomainId);
            command.Parameters.Add(prSubDomainId);
            #endregion

            #region IsActive
            DbParameter prIsActive = database.GetParameter(System.Data.DbType.Boolean, "@IsActive", _isActive);
            command.Parameters.Add(prIsActive);
            #endregion

            #region Command Update Data
            command.CommandText = "UPDATE [VoteQuestion] SET [VoteQuestionName] = @VoteQuestionName, [SubDomainId]=@SubDomainId, [IsActive]=@IsActive WHERE [VoteQuestionId] = @VoteQuestionId";
            #endregion

            return command;
        }

        protected override System.Data.Common.DbCommand GetDeleteCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region Command Delete Data
            #region VoteQuestionId
            DbParameter prId = database.GetParameter();
            prId.DbType = System.Data.DbType.Guid;
            prId.Direction = System.Data.ParameterDirection.InputOutput;
            prId.ParameterName = "@VoteQuestionId";
            prId.Value = _id;
            command.Parameters.Add(prId);
            #endregion

            command.CommandText = "DELETE [VoteQuestion] WHERE [VoteQuestionId] = @VoteQuestionId";
            #endregion

            return command;
        }

        public static VoteQuestion GetVoteQuestionBySubDomainActive(Guid subGuid)
        {
            Database database = new Database(ConnectionStringName);
            VoteQuestion result = new VoteQuestion();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand(connection);
                #region UserName
                DbParameter prUserName = database.GetParameter(System.Data.DbType.Guid, "@SubDomainId", subGuid);
                command.Parameters.Add(prUserName);
                #endregion
                command.CommandText = @"SELECT 
	                                        * 
                                        FROM 
	                                        dbo.VoteQuestion
                                        WHERE 
	                                        [SubDomainId] = @SubDomainId and IsActive=1";
                command.CommandType = CommandType.Text;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result = new VoteQuestion(reader);
                }
            }
            return result;
        }
        public static VoteQuestion GetVoteQuestionActive()
        {
            Database database = new Database(ConnectionStringName);
            VoteQuestion result = new VoteQuestion();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand(connection);

                command.CommandText = @"SELECT 
	                                        * 
                                        FROM 
	                                        dbo.VoteQuestion
                                        WHERE 
	                                        IsActive=1";
                command.CommandType = CommandType.Text;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result = new VoteQuestion(reader);
                }
            }
            return result;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(VoteQuestion)
                && ((VoteQuestion)obj)._id == _id
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
        #endregion
    }
}