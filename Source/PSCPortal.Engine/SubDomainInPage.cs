using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;
namespace PSCPortal.Engine
{
    [Serializable]
    public class SubDomainInPage : PSCPortal.Framework.BusinessObject<SubDomainInPage>
    {
        #region Properties
        private Guid _subDomain;
        public Guid SubDomainId
        {
            get
            {
                return _subDomain;
            }
            set
            {
                _subDomain = value;
            }
        }

        private Guid _pageId;
        public Guid PageId
        {
            get
            {
                return _pageId;
            }
            set
            {
                _pageId = value;
            }
        }

        #endregion
        #region Constructions
        public SubDomainInPage()
            : base()
        {
        }

        public SubDomainInPage(DbDataReader reader)
            : base(reader)
        {
        }
        #endregion
        #region Abstract Methods
        protected override void MappingData(System.Data.Common.DbDataReader reader)
        {
            _subDomain = (Guid)reader["SubDomainId"];
            _pageId = (Guid)reader["PageId"];
        }
        protected override System.Data.Common.DbCommand GetInsertCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region SubDomainInPageSubDomainId
            DbParameter prSubDomainId = database.GetParameter(System.Data.DbType.Guid, "@SubDomainId", _subDomain);
            command.Parameters.Add(prSubDomainId);
            #endregion
            #region SubDomainInPagePageId
            DbParameter prPageId = database.GetParameter(System.Data.DbType.Guid, "@PageId", _pageId);
            command.Parameters.Add(prPageId);
            #endregion
            #region Command Insert Data
            command.CommandText = @"INSERT INTO [dbo].[SubDomainInPage]
                                           ([SubDomainId]
                                           ,[PageId])
                                     VALUES
                                           (@SubDomainId
                                           ,@PageId)";
            command.CommandType = System.Data.CommandType.Text;
            #endregion

            return command;
        }
        public void AddDB()
        {
            Database database = new Database(ConnectionStringName);
            using (DbConnection connection = database.GetConnection())
            {
                connection.Open();
                DbCommand command = GetInsertCommand();
                command.Connection = connection;
                command.ExecuteNonQuery();
            }
        }
        protected override System.Data.Common.DbCommand GetUpdateCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();

            #region SubDomainInPageSubDomainId
            DbParameter prSubDomainId = database.GetParameter(System.Data.DbType.Guid, "@SubDomainId", _subDomain);
            command.Parameters.Add(prSubDomainId);
            #endregion

            #region SubDomainInPagePageId
            DbParameter prPageId = database.GetParameter(System.Data.DbType.Guid, "@PageId", _pageId);
            command.Parameters.Add(prPageId);
            #endregion

            #region Command Update Data
            command.CommandText = "Update SubDomain Set IsHomePage = @IsHomePage Where SubDomainId = @SubDomainId And PageId = @PageId";
            command.CommandType = System.Data.CommandType.Text;
            #endregion

            return command;
        }
        protected override System.Data.Common.DbCommand GetDeleteCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();

            #region SubDomainInPageSubDomainId
            DbParameter prSubDomainId = database.GetParameter(System.Data.DbType.Guid, "@SubDomainId", _subDomain);
            command.Parameters.Add(prSubDomainId);
            #endregion
            #region SubDomainInPagePageId
            DbParameter prPageId = database.GetParameter(System.Data.DbType.Guid, "@PageId", _pageId);
            command.Parameters.Add(prPageId);
            #endregion

            #region Command Delete Data
            command.CommandText = @"DELETE FROM [dbo].[SubDomainInPage]
                                         WHERE SubDomainId = @SubDomainId And PageId = @PageId";
            command.CommandType = System.Data.CommandType.Text;
            #endregion

            return command;
        }
        public void RemoveDB()
        {
            Database database = new Database(ConnectionStringName);
            using (DbConnection connection = database.GetConnection())
            {
                connection.Open();
                DbCommand command = GetDeleteCommand();
                command.Connection = connection;
                command.ExecuteNonQuery();
            }
        }
        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(SubDomainInPage)
                && ((SubDomainInPage)obj)._subDomain == _subDomain && ((SubDomainInPage)obj)._pageId == _pageId)
                return true;
            return false;
        }
        public override int GetHashCode()
        {
            int hashCode = 0;
            hashCode += _subDomain.GetHashCode();
            return hashCode;
        }
        protected static new string ConnectionStringName
        {
            get
            {
                return "PSCPortalConnectionString";
            }
        }
        public static string GetSub(Guid pageId)
        {
            Database database = new Database("PSCPortalConnectionString");
            string result = null;
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();
                #region Id

                DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@PageId", pageId);
                command.Parameters.Add(prId);
                #endregion

                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = @"SELECT Name FROM SubDomain s Inner Join SubDomainInPage sip on s.Id = sip.SubDomainId 
                                            WHERE sip.PageId = @PageId";
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    result = reader["Name"].ToString();
                }
            }
            return result;
        }
        #endregion
    }
}