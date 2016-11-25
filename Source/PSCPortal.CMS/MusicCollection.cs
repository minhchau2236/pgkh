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
    public class MusicCollection : PSCPortal.Framework.BusinessObjectCollection<MusicCollection, Music>
    {
        private MusicCollection()
            : base()
        {
        }

        protected override DbCommand GetSelectAllCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            command.CommandText = "select * from Music order by CreationDate desc";
            return command;
        }

        protected DbCommand GetTop10Command()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            command.CommandText = "select top 10 ms.* from Music ms order by CreationDate desc";
            return command;
        }

        public static MusicCollection GetMusicClipCollection()
        {
            Database database = new Database(ConnectionStringName);
            MusicCollection result = new MusicCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = result.GetSelectAllCommand();
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Music item = new Music(reader);
                    result.Add(item);
                }
            }
            return result;
        }

        public static MusicCollection GetTop10MusicClipCollection()
        {
            Database database = new Database(ConnectionStringName);
            MusicCollection result = new MusicCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = result.GetTop10Command();
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Music item = new Music(reader);
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
