using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PSCPortal.Framework;
using System.Data.Common;
namespace PSCPortal.CMS
{
    [Serializable]
    public class FeedBackCollection : PSCPortal.Framework.BusinessObjectCollection<FeedBackCollection, FeedBack>
    {
        private FeedBackCollection()
            : base()
        {
        }

        protected override DbCommand GetSelectAllCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            command.CommandText = "SELECT [FeedBackId],[FeedBackSenderName],[FeedBackSenderEmail],[FeedBackPhone],[FeedBackContent] FROM dbo.FeedBack";
            return command;
        }

        public static FeedBackCollection GetFeedBackCollection()
        {
            Database database = new Database("PSCPortalConnectionString");
            FeedBackCollection result = new FeedBackCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = result.GetSelectAllCommand();
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    FeedBack item = new FeedBack(reader);
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