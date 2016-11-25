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
    public class VideoClipCollection : PSCPortal.Framework.BusinessObjectCollection<VideoClipCollection, VideoClip>
    {
        private VideoClipCollection()
            : base()
        {
        }

        protected override DbCommand GetSelectAllCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            command.CommandText = "select * from VideoClip order by CreationDate desc";
            return command;
        }

        protected DbCommand GetTop4Command()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            command.CommandText = "select top 4 vc.* from VideoClip vc order by CreationDate desc";
            return command;
        }

        public static VideoClipCollection GetVideoClipCollection()
        {
            Database database = new Database(ConnectionStringName);
            VideoClipCollection result = new VideoClipCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = result.GetSelectAllCommand();
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    VideoClip item = new VideoClip(reader);
                    result.Add(item);
                }
            }
            return result;
        }

        public static VideoClipCollection GetTop4VideoClipCollection()
        {
            Database database = new Database(ConnectionStringName);
            VideoClipCollection result = new VideoClipCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = result.GetTop4Command();
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    VideoClip item = new VideoClip(reader);
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
