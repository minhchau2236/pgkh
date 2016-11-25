using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PSCPortal.Framework;
using System.Data.Common;

namespace PSCPortal.CMS
{
    [Serializable]
    public class LinkWebsiteCollection : PSCPortal.Framework.BusinessObjectCollection<LinkWebsiteCollection, LinkWebsite>
    {
        private LinkWebsiteCollection()
            : base()
        {
        }

        protected override DbCommand GetSelectAllCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            command.CommandText = "SELECT * FROM dbo.LinkWebsite";
            return command;
        }

        public static LinkWebsiteCollection GetLinkWebsiteCollection()
        {
            Database database = new Database(ConnectionStringName);
            LinkWebsiteCollection result = new LinkWebsiteCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = result.GetSelectAllCommand();
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    LinkWebsite item = new LinkWebsite(reader);
                    result.Add(item);
                }
            }
            return result;
        }
        public static LinkWebsiteCollection GetLinkWebsiteCollectionBySubDomain(Guid subGuid)
        {
            Database database = new Database(ConnectionStringName);
            LinkWebsiteCollection result = new LinkWebsiteCollection();
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
	                                        dbo.LinkWebsite
                                        WHERE 
	                                        [SubDomainId] = @SubDomainId";
                command.CommandType = CommandType.Text;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    LinkWebsite item = new LinkWebsite(reader);
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
