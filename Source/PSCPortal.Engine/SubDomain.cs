using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;
using PSCPortal.CMS;
using PSCPortal.Security;

namespace PSCPortal.Engine
{
    [Serializable]
    public class SubDomain : PSCPortal.Framework.BusinessObject<SubDomain>
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
        public string PageName
        {
            get
            {
                return PSCPortal.Engine.Page.GetPage(_pageId).Name;
            }
        }
        private string _description = string.Empty;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                _description = value;
            }
        }
        private Guid _pageId = Guid.Empty;
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
        private int _oldVisitors;
        public int OldVisitors
        {
            get
            {
                return _oldVisitors;
            }
            set
            {
                _oldVisitors = value;
            }
        }
        //Ngọc -18122015 
        public string NameAndDescription { get { return Name + " - " + Description; } }
        #endregion
        #region Constructions
        public SubDomain()
            : base()
        {
        }

        public SubDomain(DbDataReader reader)
            : base(reader)
        {
        }
        #endregion
        #region Abstract Methods
        protected override void MappingData(System.Data.Common.DbDataReader reader)
        {
            _id = (Guid)reader["Id"];
            _name = (string)reader["Name"];
            _description = (string)reader["Description"];
            try
            {
                _pageId = (Guid)reader["PageId"];
            }
            catch { }
        }
        protected override System.Data.Common.DbCommand GetInsertCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region Id
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@Id", _id);
            command.Parameters.Add(prId);
            #endregion
            #region Name
            DbParameter prName = database.GetParameter(System.Data.DbType.String, "@Name", _name);
            command.Parameters.Add(prName);
            #endregion

            #region PageId
            DbParameter prPageId = database.GetParameter(System.Data.DbType.Guid, "@PageId", _pageId);
            command.Parameters.Add(prPageId);
            #endregion
            #region Description
            DbParameter prDescription = database.GetParameter(System.Data.DbType.String, "@Description", _description);
            command.Parameters.Add(prDescription);
            #endregion

            #region Command Insert Data
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = @"Insert into SubDomain(Id,Name,Description,PageId) values(@Id,@Name,@Description,@PageId)";
            #endregion

            return command;
        }
        protected override System.Data.Common.DbCommand GetUpdateCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region Id
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@Id", _id);
            command.Parameters.Add(prId);
            #endregion

            #region Name
            DbParameter prName = database.GetParameter(System.Data.DbType.String, "@Name", _name);
            command.Parameters.Add(prName);
            #endregion
            #region Description
            DbParameter prDescription = database.GetParameter(System.Data.DbType.String, "@Description", _description);
            command.Parameters.Add(prDescription);
            #endregion
            #region PageId
            DbParameter prPageId = database.GetParameter(System.Data.DbType.Guid, "@PageId", _pageId);
            command.Parameters.Add(prPageId);
            #endregion

            #region Command Update Data
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = @"UPDATE SubDomain SET [Name] = @Name, [Description]=@Description, [PageId]=@PageId
                                        WHERE [Id] = @Id";
            #endregion

            return command;
        }
        protected override System.Data.Common.DbCommand GetDeleteCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region Command Delete Data
            #region Id
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@Id", _id);
            command.Parameters.Add(prId);
            #endregion

            #region Command Delete Data
            command.CommandType = System.Data.CommandType.Text;
            //command.CommandText = "Delete SubDomain where Id=@Id";
            command.CommandText = "Update SubDomain set IsDelete= 1 where Id=@Id";//Ngọc -18122015- bật cờ xóa
            #endregion

            #endregion

            return command;
        }

        public static string GetPage(string nameSub)
        {
            Database database = new Database("PSCPortalConnectionString");
            string result = null;
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();
                #region Id

                DbParameter prId = database.GetParameter(System.Data.DbType.String, "@Name", nameSub);
                command.Parameters.Add(prId);
                #endregion

                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = @"SELECT PageId FROM SubDomain
                                            WHERE Name = @Name";
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result = reader["PageId"].ToString();
                }
            }
            return result;
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
                command.CommandText = @"SELECT Name FROM SubDomain
                                            WHERE PageId = @PageId";
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
        public static SubDomain GetSubByName(string subName)
        {
            Database database = new Database("PSCPortalConnectionString");
            SubDomain result = null;
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();
                #region Id

                DbParameter prId = database.GetParameter(System.Data.DbType.String, "@Name", subName);
                command.Parameters.Add(prId);
                #endregion

                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = @"SELECT Id, Name, Description, PageId, OldVisitors FROM SubDomain
                                            WHERE Name = @Name and IsDelete = 0";// Ngọc - 18122015
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    result = new SubDomain(reader);
                    if (!reader.IsDBNull(reader.GetOrdinal("OldVisitors")))
                        result.OldVisitors = (int)reader["OldVisitors"];
                }
            }
            return result;
        }
        public static SubDomain GetSubById(string Id)
        {
            Database database = new Database("PSCPortalConnectionString");
            SubDomain result = null;
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();
                #region Id

                DbParameter prId = database.GetParameter(System.Data.DbType.String, "@Id", Id);
                command.Parameters.Add(prId);
                #endregion

                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = @"SELECT Id, Name, Description, PageId, OldVisitors FROM SubDomain
                                            WHERE Id = @Id and IsDelete = 0";//Ngọc - 18122015
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    result = new SubDomain(reader);
                    if (!reader.IsDBNull(reader.GetOrdinal("OldVisitors")))
                        result.OldVisitors = (int)reader["OldVisitors"];
                }
            }
            return result;
        }

        public override string ToString()
        {
            return _name;
        }
        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(SubDomain)
                && ((SubDomain)obj)._id == _id
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
        protected DbCommand GetMenuMastersBelongToCommand(PSCPortal.Framework.Database database)
        {
            DbCommand command = database.GetCommand();
            #region SubDomainId
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@SubDomainId", _id);
            command.Parameters.Add(prId);
            #endregion

            command.CommandText = @"SELECT 
	                                    a.[MenuMasterId],
	                                    [MenuMasterName],
	                                    [MenuMasterDescription] 
                                    FROM 
	                                    dbo.[MenuMaster] a
	                                    inner join SubDomainInMenuMaster b on a.MenuMasterId = b.MenuMasterId 
                                    WHERE 
	                                    SubDomainId=@SubDomainId";
            command.CommandType = System.Data.CommandType.Text;
            return command;
        }
        public MenuMasterCollection GetMenuMastersBelongTo()
        {
            PSCPortal.Framework.Database database = new PSCPortal.Framework.Database("PSCPortalConnectionString");
            MenuMasterCollection result = new MenuMasterCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = GetMenuMastersBelongToCommand(database);
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    MenuMaster item = new MenuMaster(reader);
                    result.Add(item);
                }
            }
            return result;
        }
        protected DbCommand GetPagesBelongToCommand(PSCPortal.Framework.Database database)
        {
            DbCommand command = database.GetCommand();
            #region SubDomainId
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@SubDomainId", _id);
            command.Parameters.Add(prId);
            #endregion

            command.CommandText = @"SELECT 
	                                    a.[PageId],
	                                    [PageName],
	                                    [PageTitle],
                                        [PageTemplate],
                                        [PageLanguage] 
                                    FROM 
	                                    dbo.[Page] a
	                                    inner join SubDomainInPage b on a.PageId = b.PageId 
                                    WHERE 
	                                    SubDomainId=@SubDomainId";
            command.CommandType = System.Data.CommandType.Text;
            return command;
        }
        public PageCollection GetPagesBelongTo()
        {
            PSCPortal.Framework.Database database = new PSCPortal.Framework.Database("PSCPortalConnectionString");
            PageCollection result = new PageCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = GetPagesBelongToCommand(database);
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Page item = new Page(reader);
                    result.Add(item);
                }
            }
            return result;
        }
        protected DbCommand GetTopicBelongToCommand(PSCPortal.Framework.Database database)
        {
            DbCommand command = database.GetCommand();
            #region SubDomainId
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@SubDomainId", _id);
            command.Parameters.Add(prId);
            #endregion

            command.CommandText = @"SELECT 
	                                    a.[TopicId]
                                    FROM 
	                                    dbo.[Topic] a
	                                    inner join SubDomainInTopic b on a.TopicId = b.TopicId 
                                    WHERE 
	                                    SubDomainId=@SubDomainId";
            command.CommandType = System.Data.CommandType.Text;
            return command;
        }
        public Topic GetTopic()
        {
            PSCPortal.Framework.Database database = new PSCPortal.Framework.Database("PSCPortalConnectionString");
            Topic result = new Topic();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = GetTopicBelongToCommand(database);
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    result = new Topic { Id = new Guid(reader["TopicId"].ToString()) };
                }
            }
            return result;
        }
        public ArticleCollection GetArticlesBelongTo()
        {
            PSCPortal.Framework.Database database = new PSCPortal.Framework.Database("PSCPortalConnectionString");
            ArticleCollection result = new ArticleCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand(connection);
                #region SubDomainId
                DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@SubDomainId", _id);
                command.Parameters.Add(prId);
                #endregion

                command.CommandText = @"SELECT 
	                                    a.[ArticleId],
	                                    [ArticleName],
	                                    [ArticleTitle],
                                        b.[IsCheck],
                                        s.[Name] as SubDomainFromName 
                                    FROM 
	                                    dbo.[Article] a 
	                                    inner join SubDomainInArticle b on a.ArticleId = b.ArticleNewId 
                                        inner join SubDomain s on b.SubDomainFromId = s.Id 
                                    WHERE 
	                                    b.SubDomainToId = @SubDomainId AND b.IsCheck = 'false'";
                command.CommandType = System.Data.CommandType.Text;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Article item = new Article();
                    if (!reader.IsDBNull(reader.GetOrdinal("ArticleId")))
                        item.Id = new Guid(reader["ArticleId"].ToString());
                    if (!reader.IsDBNull(reader.GetOrdinal("ArticleName")))
                        item.Name = (string)reader["ArticleName"].ToString();
                    if (!reader.IsDBNull(reader.GetOrdinal("ArticleTitle")))
                        item.Title = (string)reader["ArticleTitle"].ToString();
                    if (!reader.IsDBNull(reader.GetOrdinal("SubDomainFromName")))
                        item.SubDomainFromName = (string)reader["SubDomainFromName"];
                    if (!reader.IsDBNull(reader.GetOrdinal("IsCheck")))
                        item.IsCheck = (bool)reader["IsCheck"];
                    result.Add(item);
                }
            }
            return result;
        }

        public RoleCollection GetRolesBelongTo()
        {
            PSCPortal.Framework.Database database = new PSCPortal.Framework.Database("PSCPortalConnectionString");
            RoleCollection result = new RoleCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand(connection);
                #region SubDomainId
                DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@SubDomainId", _id);
                command.Parameters.Add(prId);
                #endregion
                command.CommandText = @"SELECT 
	                                    a.[RoleId],
	                                    [RoleName],
	                                    [RoleDescription]
                                    FROM 
	                                    dbo.[Role] a
	                                    inner join SubDomainInRole b on a.RoleId = b.RoleId 
                                    WHERE 
	                                    SubDomainId=@SubDomainId";
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Role item = new Role(reader);
                    result.Add(item);
                }
            }
            return result;
        }
        public RoleCollection GetRolesNotBelongTo()
        {
            PSCPortal.Framework.Database database = new PSCPortal.Framework.Database("PSCPortalConnectionString");
            RoleCollection result = new RoleCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand(connection);
                #region SubDomainId
                DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@SubDomainId", _id);
                command.Parameters.Add(prId);
                #endregion
                command.CommandText = @"SELECT 
	                                    a.[RoleId],
	                                    [RoleName],
	                                    [RoleDescription]
                                    FROM 
	                                    dbo.[Role] a
	                                    inner join SubDomainInRole b on a.RoleId = b.RoleId 
                                    WHERE 
	                                    SubDomainId!=@SubDomainId";
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Role item = new Role(reader);
                    result.Add(item);
                }
            }
            return result;
        }
    }
}
