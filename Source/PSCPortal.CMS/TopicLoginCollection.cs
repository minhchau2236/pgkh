using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;

namespace PSCPortal.CMS
{
    [Serializable]
    public class TopicLoginCollection : PSCPortal.Framework.BusinessObjectCollection<TopicLoginCollection, TopicLogin>
    {
        private Topic _topic;
        public Topic Topic
        {
            get { return _topic; }            
        }
        private TopicLoginCollection()
            : base()
        { }
        protected override DbCommand GetSelectAllCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            command.CommandText = "select * from TopicLogin";
            command.CommandType = System.Data.CommandType.Text;
            return command;
        }
        protected DbCommand GetSelectAllCommand(Topic topic)
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();

            #region TopicId
            DbParameter prTopicId = database.GetParameter(System.Data.DbType.Guid, "@TopicId", topic.Id);
            command.Parameters.Add(prTopicId);
            #endregion

            command.CommandText = @"SELECT 
	                                        a.[TopicId],	                                        
	                                        a.[UserName],
                                            a.[Password]
                                        FROM dbo.[TopicLogin] a
	                                        INNER JOIN dbo.[Topic] b ON b.TopicId = a.TopicId
                                        where  
	                                        b.TopicId = @TopicId";
            command.CommandType = System.Data.CommandType.Text;
            return command;
        }
        public static TopicLoginCollection GetTopicLoginCollection(Topic topic)
        {
            Database database = new Database("PSCPortalConnectionString");
            TopicLoginCollection result = new TopicLoginCollection();
            result._topic = topic;

            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = result.GetSelectAllCommand(topic);
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    TopicLogin item = new TopicLogin(reader);
                    result.Add(item);
                }
            }
            return result;
        }
        public static TopicLoginCollection GetTopicLoginCollection()
        {
            Database database = new Database("PSCPortalConnectionString");
            TopicLoginCollection result = new TopicLoginCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = result.GetSelectAllCommand();
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    TopicLogin item = new TopicLogin(reader);
                    result.Add(item);
                }
            }
            return result;
        }
        public static List<CMS.Topic> GetTopicUnLoginCollection(Topic topic)
        {
            List<Topic> result = new List<Topic>();
            List<CMS.Topic> listTopicLogin = new List<CMS.Topic>();            
            IList<Topic> topicList = (IList<Topic>)CMS.TopicCollection.GetTopicCollectionByTopicParent(topic.Id.ToString());
            CMS.TopicLoginCollection TopicLoginList = CMS.TopicLoginCollection.GetTopicLoginCollection(topic);

            foreach (CMS.TopicLogin item in TopicLoginList)
            {
                CMS.Topic a = topicList.SingleOrDefault(t => t.Id == item.Id);
                listTopicLogin.Add(a);
            }

            result = topicList.Except(listTopicLogin).ToList();
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