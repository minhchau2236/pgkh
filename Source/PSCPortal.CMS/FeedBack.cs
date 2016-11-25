using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;
namespace PSCPortal.CMS
{
    [Serializable]
    public class FeedBack : PSCPortal.Framework.BusinessObject<FeedBack>
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

        private string _senderName = string.Empty;
        public string SenderName
        {
            get
            {
                return _senderName;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                _senderName = value;
            }
        }

        private string _senderEmail = string.Empty;
        public string SenderEmail
        {
            get
            {
                return _senderEmail;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                _senderEmail = value;
            }
        }

        private string _phone = string.Empty;
        public string Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                _phone = value;
            }
        }

        private string _content = string.Empty;
        public string Content
        {
            get
            {
                return _content;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                _content = value;
            }
        }

        #endregion

        #region Constructions
        public FeedBack()
            : base()
        {
        }

        public FeedBack(DbDataReader reader)
            : base(reader)
        {
        }
        #endregion
        #region Abstract Methods
        protected override void MappingData(System.Data.Common.DbDataReader reader)
        {
            _id = (Guid)reader["FeedBackId"];
            _senderName = (string)reader["FeedBackSenderName"];
            _senderEmail = (string)reader["FeedBackSenderEmail"];
            _phone = (string)reader["FeedBackPhone"];
            _content = (string)reader["FeedBackContent"];
        }
        protected override System.Data.Common.DbCommand GetInsertCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region FeedBackId
            DbParameter prId = database.GetParameter();
            prId.DbType = System.Data.DbType.Guid;
            prId.Direction = System.Data.ParameterDirection.InputOutput;
            prId.ParameterName = "@FeedBackId";
            prId.Value = _id;
            command.Parameters.Add(prId);
            #endregion
            #region FeedBackSenderName
            DbParameter prSenderName = database.GetParameter();
            prSenderName.DbType = System.Data.DbType.String;
            prSenderName.Direction = System.Data.ParameterDirection.InputOutput;
            prSenderName.ParameterName = "@FeedBackSenderName";
            prSenderName.Value = _senderName;
            command.Parameters.Add(prSenderName);
            #endregion
            #region FeedBackSenderEmail
            DbParameter prSenderEmail = database.GetParameter();
            prSenderEmail.DbType = System.Data.DbType.String;
            prSenderEmail.Direction = System.Data.ParameterDirection.InputOutput;
            prSenderEmail.ParameterName = "@FeedBackSenderEmail";
            prSenderEmail.Value = _senderEmail;
            command.Parameters.Add(prSenderEmail);
            #endregion
            #region FeedBackPhone
            DbParameter prPhone = database.GetParameter();
            prPhone.DbType = System.Data.DbType.String;
            prPhone.Direction = System.Data.ParameterDirection.InputOutput;
            prPhone.ParameterName = "@FeedBackPhone";
            prPhone.Value = _phone;
            command.Parameters.Add(prPhone);
            #endregion
            #region FeedBackContent
            DbParameter prContent = database.GetParameter();
            prContent.DbType = System.Data.DbType.String;
            prContent.Direction = System.Data.ParameterDirection.InputOutput;
            prContent.ParameterName = "@FeedBackContent";
            prContent.Value = _content;
            command.Parameters.Add(prContent);
            #endregion

            #region Command Insert Data
            command.CommandText = "INSERT INTO [FeedBack] ([FeedBackId],[FeedBackSenderName],[FeedBackSenderEmail],[FeedBackPhone],[FeedBackContent]) VALUES (@FeedBackId,@FeedBackSenderName,@FeedBackSenderEmail,@FeedBackPhone,@FeedBackContent)";
            #endregion

            return command;
        }

        protected override System.Data.Common.DbCommand GetUpdateCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region FeedBackId
            DbParameter prId = database.GetParameter();
            prId.DbType = System.Data.DbType.Guid;
            prId.Direction = System.Data.ParameterDirection.InputOutput;
            prId.ParameterName = "@FeedBackId";
            prId.Value = _id;
            command.Parameters.Add(prId);
            #endregion
            #region FeedBackSenderName
            DbParameter prSenderName = database.GetParameter();
            prSenderName.DbType = System.Data.DbType.String;
            prSenderName.Direction = System.Data.ParameterDirection.InputOutput;
            prSenderName.ParameterName = "@FeedBackSenderName";
            prSenderName.Value = _senderName;
            command.Parameters.Add(prSenderName);
            #endregion
            #region FeedBackSenderEmail
            DbParameter prSenderEmail = database.GetParameter();
            prSenderEmail.DbType = System.Data.DbType.String;
            prSenderEmail.Direction = System.Data.ParameterDirection.InputOutput;
            prSenderEmail.ParameterName = "@FeedBackSenderEmail";
            prSenderEmail.Value = _senderEmail;
            command.Parameters.Add(prSenderEmail);
            #endregion
            #region FeedBackPhone
            DbParameter prPhone = database.GetParameter();
            prPhone.DbType = System.Data.DbType.String;
            prPhone.Direction = System.Data.ParameterDirection.InputOutput;
            prPhone.ParameterName = "@FeedBackPhone";
            prPhone.Value = _phone;
            command.Parameters.Add(prPhone);
            #endregion
            #region FeedBackContent
            DbParameter prContent = database.GetParameter();
            prContent.DbType = System.Data.DbType.String;
            prContent.Direction = System.Data.ParameterDirection.InputOutput;
            prContent.ParameterName = "@FeedBackContent";
            prContent.Value = _content;
            command.Parameters.Add(prContent);
            #endregion

            #region Command Update Data
            command.CommandText = "UPDATE [FeedBack] SET [FeedBackSenderName] = @FeedBackSenderName, [FeedBackSenderEmail] = @FeedBackSenderEmail, [FeedBackPhone] = @FeedBackPhone, [FeedBackContent] = @FeedBackContent WHERE [FeedBackId] = @FeedBackId";
            #endregion

            return command;
        }

        protected override System.Data.Common.DbCommand GetDeleteCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region Command Delete Data
            #region FeedBackId
            DbParameter prId = database.GetParameter();
            prId.DbType = System.Data.DbType.Guid;
            prId.Direction = System.Data.ParameterDirection.InputOutput;
            prId.ParameterName = "@FeedBackId";
            prId.Value = _id;
            command.Parameters.Add(prId);
            #endregion

            command.CommandText = "DELETE [FeedBack] WHERE [FeedBackId] = @FeedBackId";
            #endregion

            return command;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(FeedBack)
                && ((FeedBack)obj)._id == _id
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
            Database database = new Database(ConnectionStringName);
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = GetInsertCommand();
                command.Connection = connection;
                connection.Open();
                command.ExecuteNonQuery();
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