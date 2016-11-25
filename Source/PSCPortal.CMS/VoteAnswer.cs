using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;
namespace PSCPortal.CMS
{
    [Serializable]
    public class VoteAnswer : PSCPortal.Framework.BusinessObject<VoteAnswer>
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

        private int _number;
        public int Number
        {
            get
            {
                return _number;
            }
            set
            {
                _number = value;
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


        #endregion

        #region Constructions
        public VoteAnswer()
            : base()
        {
        }

        public VoteAnswer(DbDataReader reader)
            : base(reader)
        {
        }
        #endregion
        #region Abstract Methods
        protected override void MappingData(System.Data.Common.DbDataReader reader)
        {
            _id = (Guid)reader["VoteAnswerId"];
            _name = (string)reader["VoteAnswerName"];
            _number = (int)reader["VoteAnswerNumber"];
            _order = (int)reader["VoteAnswerOrder"];
        }
        protected override System.Data.Common.DbCommand GetInsertCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region VoteAnswerId
            DbParameter prId = database.GetParameter();
            prId.DbType = System.Data.DbType.Guid;
            prId.Direction = System.Data.ParameterDirection.InputOutput;
            prId.ParameterName = "@VoteAnswerId";
            prId.Value = _id;
            command.Parameters.Add(prId);
            #endregion
            #region VoteAnswerName
            DbParameter prName = database.GetParameter();
            prName.DbType = System.Data.DbType.String;
            prName.Direction = System.Data.ParameterDirection.InputOutput;
            prName.ParameterName = "@VoteAnswerName";
            prName.Value = _name;
            command.Parameters.Add(prName);
            #endregion
            #region VoteAnswerNumber
            DbParameter prNumber = database.GetParameter();
            prNumber.DbType = System.Data.DbType.Int32;
            prNumber.Direction = System.Data.ParameterDirection.InputOutput;
            prNumber.ParameterName = "@VoteAnswerNumber";
            prNumber.Value = _number;
            command.Parameters.Add(prNumber);
            #endregion
            #region VoteAnswerOrder
            DbParameter prOrder = database.GetParameter();
            prOrder.DbType = System.Data.DbType.Int32;
            prOrder.Direction = System.Data.ParameterDirection.InputOutput;
            prOrder.ParameterName = "@VoteAnswerOrder";
            prOrder.Value = ((VoteAnswerCollection)_collection).Count;
            command.Parameters.Add(prOrder);
            #endregion
            #region VoteQuestionId
            DbParameter prVoteQuestionId = database.GetParameter();
            prVoteQuestionId.DbType = System.Data.DbType.Guid;
            prVoteQuestionId.Direction = System.Data.ParameterDirection.InputOutput;
            prVoteQuestionId.ParameterName = "@VoteQuestionId";
            prVoteQuestionId.Value = ((VoteAnswerCollection)_collection).Parent.Id;
            command.Parameters.Add(prVoteQuestionId);
            #endregion

            #region Command Insert Data
            command.CommandText = "INSERT INTO [VoteAnswer] ([VoteAnswerId],[VoteAnswerName],[VoteAnswerNumber],[VoteAnswerOrder],[VoteQuestionId]) VALUES (@VoteAnswerId,@VoteAnswerName,@VoteAnswerNumber,@VoteAnswerOrder,@VoteQuestionId)";
            #endregion

            return command;
        }

        protected override System.Data.Common.DbCommand GetUpdateCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region VoteAnswerId
            DbParameter prId = database.GetParameter();
            prId.DbType = System.Data.DbType.Guid;
            prId.Direction = System.Data.ParameterDirection.InputOutput;
            prId.ParameterName = "@VoteAnswerId";
            prId.Value = _id;
            command.Parameters.Add(prId);
            #endregion
            #region VoteAnswerName
            DbParameter prName = database.GetParameter();
            prName.DbType = System.Data.DbType.String;
            prName.Direction = System.Data.ParameterDirection.InputOutput;
            prName.ParameterName = "@VoteAnswerName";
            prName.Value = _name;
            command.Parameters.Add(prName);
            #endregion
            #region VoteAnswerNumber
            DbParameter prNumber = database.GetParameter();
            prNumber.DbType = System.Data.DbType.Int32;
            prNumber.Direction = System.Data.ParameterDirection.InputOutput;
            prNumber.ParameterName = "@VoteAnswerNumber";
            prNumber.Value = _number;
            command.Parameters.Add(prNumber);
            #endregion
            #region VoteAnswerOrder
            DbParameter prOrder = database.GetParameter();
            prOrder.DbType = System.Data.DbType.Int32;
            prOrder.Direction = System.Data.ParameterDirection.InputOutput;
            prOrder.ParameterName = "@VoteAnswerOrder";
            prOrder.Value = _order;
            command.Parameters.Add(prOrder);
            #endregion

            #region Command Update Data
            command.CommandText = "UPDATE [VoteAnswer] SET [VoteAnswerName] = @VoteAnswerName, [VoteAnswerNumber] = @VoteAnswerNumber, [VoteAnswerOrder] = @VoteAnswerOrder WHERE [VoteAnswerId] = @VoteAnswerId";
            #endregion

            return command;
        }

        protected override System.Data.Common.DbCommand GetDeleteCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region Command Delete Data
            #region VoteAnswerId
            DbParameter prId = database.GetParameter();
            prId.DbType = System.Data.DbType.Guid;
            prId.Direction = System.Data.ParameterDirection.InputOutput;
            prId.ParameterName = "@VoteAnswerId";
            prId.Value = _id;
            command.Parameters.Add(prId);
            #endregion

            command.CommandText = "DELETE [VoteAnswer] WHERE [VoteAnswerId] = @VoteAnswerId";
            #endregion

            return command;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(VoteAnswer)
                && ((VoteAnswer)obj)._id == _id
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