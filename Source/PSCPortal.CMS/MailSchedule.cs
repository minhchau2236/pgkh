using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;

namespace PSCPortal.CMS
{
     [Serializable]
    public class MailSchedule : PSCPortal.Framework.BusinessObject<MailSchedule>
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

        private string _mail = string.Empty;
        public string Mail
        {
            get
            {
                return _mail;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                _mail = value;
            }
        }
        #endregion

        #region Constructions
        public MailSchedule()
            : base()
        {
        }

        public MailSchedule(DbDataReader reader)
            : base(reader)
        {
        }
        #endregion
        #region Abstract Methods
        protected override void MappingData(System.Data.Common.DbDataReader reader)
        {
            _id = (Guid)reader["Id"];
            _name = (string)reader["Name"];
            _mail = (string)reader["Mail"];
        }
        protected override System.Data.Common.DbCommand GetInsertCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region MenuMasterId
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@Id", _id);
            command.Parameters.Add(prId);
            #endregion
            #region MenuMasterName
            DbParameter prName = database.GetParameter(System.Data.DbType.String, "@Name", _name);
            command.Parameters.Add(prName);
            #endregion
            #region MenuMasterDescription
            DbParameter prDescription = database.GetParameter(System.Data.DbType.String, "@Mail", _mail);
            command.Parameters.Add(prDescription);
            #endregion           

            #region Command Insert Data 
            command.CommandType = System.Data.CommandType.Text;
            string strQuery = @"INSERT INTO dbo.[MailSchedule] 
                                (
	                                [Id],
	                                [Name],
	                                [Mail]
                                ) 
                                VALUES 
                                (
	                                @Id,
	                                @Name,
	                                @Mail
                                )";
            command.CommandText = strQuery;
            #endregion
            return command;
        }

        protected override System.Data.Common.DbCommand GetUpdateCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region MenuMasterId
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@Id", _id);
            command.Parameters.Add(prId);
            #endregion
            #region MenuMasterName
            DbParameter prName = database.GetParameter(System.Data.DbType.String, "@Name", _name);
            command.Parameters.Add(prName);
            #endregion
            #region MenuMasterDescription
            DbParameter prDescription = database.GetParameter(System.Data.DbType.String, "@Mail", _mail);
            command.Parameters.Add(prDescription);
            #endregion           

            #region Command Update Data
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = @"UPDATE dbo.[MailSchedule] 
                                    SET 
	                                    [Name] = @Name, 
	                                    [Mail] = @Mail 
                                    WHERE 
	                                    [Id] = @Id
                                    ";
            #endregion

            return command;
        }

        protected override System.Data.Common.DbCommand GetDeleteCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region Command Delete Data
            #region MenuMasterId
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@Id", _id);
            command.Parameters.Add(prId);
            #endregion

            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = @"DELETE dbo.[MailSchedule] 
                                    WHERE [Id] = @Id";
            #endregion

            return command;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(MailSchedule)
                && ((MailSchedule)obj)._id == _id
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
