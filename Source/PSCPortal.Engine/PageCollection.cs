using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PSCPortal.Framework;
using System.Data.Common;
using PSCPortal.CMS;
namespace PSCPortal.Engine
{
    [Serializable]
    public class PageCollection : PSCPortal.Framework.BusinessObjectCollection<PageCollection, Page>
    {
        public PageCollection()
            : base()
        {
        }
        public void PageCopy(Page page, PageArgs args)
        {
            Database database = new Database("PSCPortalConnectionString");
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand(connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                #region SourcePageId
                DbParameter prSourcePageId = database.GetParameter(System.Data.DbType.Guid, "@SourcePageId", page.Id);
                command.Parameters.Add(prSourcePageId);
                #endregion
                #region DestPageId
                DbParameter prDestPageId = database.GetParameter(System.Data.DbType.Guid, "@DestPageId", args.Page.Id);
                command.Parameters.Add(prDestPageId);
                #endregion
                #region NewPageName
                DbParameter prNewPageName = database.GetParameter(System.Data.DbType.String, "@NewPageName", args.Page.Name);
                command.Parameters.Add(prNewPageName);
                #endregion
                #region PageNewPageTitle
                DbParameter prNewPageTitle = database.GetParameter(System.Data.DbType.String, "@NewPageTitle", args.Page.Title);
                command.Parameters.Add(prNewPageTitle);
                #endregion
                #region PageTemplate
                DbParameter prPageTemplate = database.GetParameter(System.Data.DbType.Int32, "@PageTemplate", page.Template);
                command.Parameters.Add(prPageTemplate);
                #endregion
                #region PageLanguage
                DbParameter prLanguage = database.GetParameter(System.Data.DbType.Int32, "@PageLanguage", page.Language);
                command.Parameters.Add(prLanguage);
                #endregion

                #region LayoutId
                DbParameter prLayoutId = database.GetParameter(System.Data.DbType.Guid, "@LayoutId", args.Page.LayoutId);
                command.Parameters.Add(prLayoutId);
                #endregion
                command.CommandText = "Page_CopyTo";

                connection.Open();
                command.ExecuteNonQuery();
                Add(args.Page);
            }
        }

        protected override DbCommand GetSelectAllCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            command.CommandText = "Page_GetAll";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            return command;
        }
        public static PageCollection GetPageCollection()
        {
            Database database = new Database("PSCPortalConnectionString");
            PageCollection result = new PageCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = result.GetSelectAllCommand();
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



        protected static new string ConnectionStringName
        {
            get
            {
                return "PSCPortalConnectionString";
            }
        }
    }
}