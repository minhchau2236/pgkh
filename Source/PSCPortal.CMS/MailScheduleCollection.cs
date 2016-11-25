using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PSCPortal.Framework;
using System.Data.Common;

namespace PSCPortal.CMS
{
     [Serializable]
    public class MailScheduleCollection : PSCPortal.Framework.BusinessObjectCollection<MailScheduleCollection, MailSchedule>
    {
         private MailScheduleCollection()
            : base()
        {
        }

        protected override DbCommand GetSelectAllCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();

            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = @"SELECT *
                                FROM dbo.[MailSchedule] Order by Name";
            return command;
        }

        public static MailScheduleCollection GetMailScheduleCollection()
        {
            Database database = new Database("PSCPortalConnectionString");
            MailScheduleCollection result = new MailScheduleCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = result.GetSelectAllCommand();
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    MailSchedule item = new MailSchedule(reader);
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
