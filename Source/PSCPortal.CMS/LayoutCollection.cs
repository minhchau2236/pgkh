using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PSCPortal.Framework;
using System.Data.Common;
namespace PSCPortal.CMS
{
    [Serializable]
    public class LayoutCollection : PSCPortal.Framework.BusinessObjectCollection<LayoutCollection, Layout>
    {
        public LayoutCollection()
            : base()
        {
        }

        protected override DbCommand GetSelectAllCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            command.CommandText = "Layout_GetAll";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            return command;
        }
        public static LayoutCollection GetAllLayout()
        {
            Database database = new Database("PSCPortalConnectionString");
            LayoutCollection result = new LayoutCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = result.GetSelectAllCommand();
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Layout item = new Layout(reader);
                    result.Add(item);
                }
            }
            return result;
        }

        public static LayoutCollection GetLayOut(string layoutId)
        {
            Database database = new Database("PSCPortalConnectionString");
            LayoutCollection result = new LayoutCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = result.GetSelectAllCommand();

                #region LayoutId
                DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@LayoutId", new Guid(layoutId));
                command.Parameters.Add(prId);
                #endregion

                command.CommandText = "select * from Layout where Id = @LayoutId";
                command.CommandType = System.Data.CommandType.Text;

                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Layout item = new Layout(reader);
                    result.Add(item);
                }
            }
            return result;
        }
        public static LayoutCollection GetPageLayOut(Guid PageId)
        {

            Database database = new Database(ConnectionStringName);
            LayoutCollection result = new LayoutCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();

                #region PageId
                DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@PageId", PageId);
                command.Parameters.Add(prId);
                #endregion

                command.CommandText = "Select Id,Name,NavigationURL,PageId from PageLayOut a join Layout b on a.LayoutId = b.Id where PageId = @PageId";
                command.CommandType = System.Data.CommandType.Text;

                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    Layout item = new Layout(reader);
                    item.PageId = (Guid)reader["PageId"];
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