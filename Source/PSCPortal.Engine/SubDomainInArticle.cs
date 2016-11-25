using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;
namespace PSCPortal.Engine
{
    [Serializable]
    public class SubDomainInArticle : PSCPortal.Framework.BusinessObject<SubDomainInArticle>
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
        private Guid _subDomainToId;
        public Guid SubDomainToId
        {
            get
            {
                return _subDomainToId;
            }
            set
            {
                _subDomainToId = value;
            }
        }

        private Guid _articleId;
        public Guid ArticleId
        {
            get
            {
                return _articleId;
            }
            set
            {
                _articleId = value;
            }
        }
        private Guid _subDomainFromId;
        public Guid SubDomainFromId
        {
            get
            {
                return _subDomainFromId;
            }
            set
            {
                _subDomainFromId = value;
            }
        }
        private Guid _articleNewId;
        public Guid ArticleNewId
        {
            get
            {
                return _articleNewId;
            }
            set
            {
                _articleNewId = value;
            }
        }
        private bool _isCheck = false;
        public bool IsCheck
        {
            get
            {
                return _isCheck;
            }
            set
            {
                _isCheck = value;
            }
        }
        #endregion

        #region Constructions
        public SubDomainInArticle()
            : base()
        {
        }

        public SubDomainInArticle(DbDataReader reader)
            : base(reader)
        {
        }
        #endregion
        #region Abstract Methods
        protected override void MappingData(System.Data.Common.DbDataReader reader)
        {
            _id = (Guid)reader["Id"];
            _subDomainToId = (Guid)reader["SubDomainToId"];
            _articleId = (Guid)reader["ArticleId"];
            _subDomainFromId = (Guid)reader["SubDomainFromId"];
            _articleNewId = (Guid)reader["ArticleNewId"];
            _isCheck = (bool)reader["IsCheck"];
        }
        protected override System.Data.Common.DbCommand GetInsertCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region Id
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@Id", _id);
            command.Parameters.Add(prId);
            #endregion
            #region SubDomainToId
            DbParameter prSubDomainToId = database.GetParameter(System.Data.DbType.Guid, "@SubDomainToId", _subDomainToId);
            command.Parameters.Add(prSubDomainToId);
            #endregion
            #region ArticleId
            DbParameter prArticleId = database.GetParameter(System.Data.DbType.Guid, "@ArticleId", _articleId);
            command.Parameters.Add(prArticleId);
            #endregion
            #region SubDomainToId
            DbParameter prSubDomainFromId = database.GetParameter(System.Data.DbType.Guid, "@SubDomainFromId", _subDomainFromId);
            command.Parameters.Add(prSubDomainFromId);
            #endregion
            #region ArticleNewId
            DbParameter prArticleNewId = database.GetParameter(System.Data.DbType.Guid, "@ArticleNewId", _articleNewId);
            command.Parameters.Add(prArticleNewId);
            #endregion
             #region IsCheck
            DbParameter prIsCheck = database.GetParameter(System.Data.DbType.Boolean, "@IsCheck", _isCheck);
            command.Parameters.Add(prIsCheck);
            #endregion

            #region Command Insert Data
            command.CommandText = @"INSERT INTO [dbo].[SubDomainInArticle]
                                           ([Id]
                                           ,[SubDomainToId]
                                           ,[ArticleId]
                                           ,[SubDomainFromId]
                                           ,[ArticleNewId]
                                           ,[IsCheck])
                                     VALUES
                                           (@Id
                                           ,@SubDomainToId
                                           ,@ArticleId
                                           ,@SubDomainFromId
                                           ,@ArticleNewId
                                           ,@IsCheck)";
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

            #region SubDomainInArticleSubDomainId
            DbParameter prSubDomainFromId = database.GetParameter(System.Data.DbType.Guid, "@SubDomainToId", _subDomainToId);
            command.Parameters.Add(prSubDomainFromId);
            #endregion
             #region ArticleNewId
            DbParameter prArticleNewId = database.GetParameter(System.Data.DbType.Guid, "@ArticleNewId", _articleNewId);
            command.Parameters.Add(prArticleNewId);
            #endregion
            #region IsCheck
            DbParameter prIsCheck = database.GetParameter(System.Data.DbType.Boolean, "@IsCheck", _isCheck);
            command.Parameters.Add(prIsCheck);
            #endregion

            #region Command Update Data
            command.CommandText = @"UPDATE SubDomainInArticle SET IsCheck = @IsCheck WHERE SubDomainToId = @SubDomainToId AND ArticleNewId = @ArticleNewId";
            command.CommandType = System.Data.CommandType.Text;
            #endregion

            return command;
        }

        protected override System.Data.Common.DbCommand GetDeleteCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();

            #region SubDomainInArticleSubDomainId
            DbParameter prSubDomainId = database.GetParameter(System.Data.DbType.Guid, "@SubDomainFromId", _subDomainFromId);
            command.Parameters.Add(prSubDomainId);
            #endregion
            #region SubDomainInArticleArticleId
            DbParameter prArticleId = database.GetParameter(System.Data.DbType.Guid, "@ArticleId", _articleId);
            command.Parameters.Add(prArticleId);
            #endregion

            #region Command Delete Data
            command.CommandText = @"DELETE FROM [dbo].[SubDomainInArticle]
                                         WHERE SubDomainFromId = @SubDomainFromId And ArticleId = @ArticleId";
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
       public void Update()
        {
            Database database = new Database(ConnectionStringName);
            using (DbConnection connection = database.GetConnection())
            {
                connection.Open();
                DbCommand command = GetUpdateCommand();
                command.Connection = connection;
                command.ExecuteNonQuery();
            }
        }
        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(SubDomainInArticle)
                && ((SubDomainInArticle)obj)._id == _id && ((SubDomainInArticle)obj)._id == _id)
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