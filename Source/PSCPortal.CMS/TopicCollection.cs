using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using NHibernate.Linq;
using PSCPortal.DB.Entities;
using PSCPortal.DB.Helper;
using PSCPortal.Engine;
using PSCPortal.Framework;
using System.Data.Common;
using PSCPortal.Framework.Helpler;

namespace PSCPortal.CMS
{
    [Serializable]
    public class TopicCollection : PSCPortal.Framework.BusinessObjectTree<TopicCollection, Topic>
    {
        internal Topic TopicRoot
        {
            get
            {
                return (Topic)_root;
            }
        }
        public TopicCollection(Topic root)
            : base(root)
        {
        }

        protected override DbCommand GetSelectAllCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            command.CommandText = "Topic_GetAll";
            //command.CommandText = "[GetTopicTree]";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            return command;
        }

        public static TopicCollection GetTopicCollection()
        {
            //Database database = new Database("PSCPortalConnectionString");
            Topic root = null;
            TopicCollection result = null;
            DateTime start = DateTime.Now;
            SessionManager.Query(session =>
            {
                List<DB.Entities.Topic> listTopics = session.Query<DB.Entities.Topic>().ToList();
                Guid subId = SessionHelper.GetSession(SessionKey.SubDomain) == string.Empty ? Guid.Empty : new Guid(SessionHelper.GetSession(SessionKey.SubDomain));
                SubDomain subDomain = new SubDomain { Id = subId };
                Topic topicMaster = subDomain.GetTopic();
                DB.Entities.Topic topic = session.Get<DB.Entities.Topic>(topicMaster.Id);
                root = new Topic { Id = topic.TopicId, Name = topic.TopicName, Description = topic.TopicDescription, PageId = topic.PageId };
                result = new TopicCollection(root);
                if (topic.TopicId == Guid.Empty && subDomain.Id != Guid.Empty)
                    return;
                Stack<DB.Entities.Topic> stack = new Stack<DB.Entities.Topic>();
                stack.Push(topic);
                Dictionary<Guid, Topic> items = new Dictionary<Guid, Topic>();
                items.Add(root.Id, root);
                while (stack.Count > 0)
                {
                    DB.Entities.Topic first = stack.Pop();
                    List<DB.Entities.Topic> children = listTopics.Where(topic1 => topic1.TopicParent == first.TopicId).ToList();
                    foreach (var child in children.OrderBy(a => a.TopicOrder))
                    {
                        stack.Push(child);
                        Topic parent = items[first.TopicId];
                        Topic dir = new Topic { Id = child.TopicId, Name = child.TopicName, Description = child.TopicDescription, PageId = child.PageId,Rss = child.Rss};
                        items.Add(dir.Id, dir);
                        result.Add(parent, dir);
                    }
                }
            });
            TimeSpan timeSpan = DateTime.Now - start;
            var totalSeconds = timeSpan.TotalSeconds;
            return result;
        }
        public static TopicCollection GetTopicCollectionByArticleId(string articleid)
        {
            Database database = new Database("PSCPortalConnectionString");
            TopicCollection result = new TopicCollection(new Topic());
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();
                #region PrArticleId
                DbParameter PrArticleId = database.GetParameter(System.Data.DbType.Guid, "@ArticleId", new Guid(articleid));
                command.Parameters.Add(PrArticleId);
                #endregion
                command.CommandText = "Topic_GetTopicByArticleId";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Topic dir = new Topic(reader);
                    result.Add(dir);
                }
            }
            return result;
        }
        public static TopicCollection GetTopicCollectionByTopicParent(string topicid)
        {
            Database database = new Database("PSCPortalConnectionString");
            TopicCollection result = new TopicCollection(new Topic());
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();
                #region TopicParent
                DbParameter PrTopicParent = database.GetParameter(System.Data.DbType.Guid, "@TopicParent", new Guid(topicid));
                command.Parameters.Add(PrTopicParent);
                #endregion
                command.CommandText = "Topic_GetTopicByTopicParent";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Topic dir = new Topic(reader);
                    result.Add(dir);
                }
            }
            return result;
        }
        public static TopicCollection GetTreeTopicCollectionByTopicParent(string topicid)
        {
            Database database = new Database("PSCPortalConnectionString");
            TopicCollection result = new TopicCollection(new Topic());
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();
                #region TopicParent
                DbParameter PrTopicParent = database.GetParameter(System.Data.DbType.Guid, "@TopicParent", new Guid(topicid));
                command.Parameters.Add(PrTopicParent);
                #endregion
                command.CommandText = "Topic_GetTreeTopicByTopicParent";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Topic dir = new Topic(reader);
                    result.Add(dir);
                }
            }
            return result;
        }
        public void TopicCopy(Topic topic, Topic topicCopy)
        {
            Database database = new Database("PSCPortalConnectionString");
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand(connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                #region SourceTopicId
                DbParameter prSourceTopicId = database.GetParameter(System.Data.DbType.Guid, "@SourceTopicId", topic.Id);
                command.Parameters.Add(prSourceTopicId);
                #endregion

                #region DestTopicId
                DbParameter prDestTopicId = database.GetParameter(System.Data.DbType.Guid, "@DestTopicId", topicCopy.Id);
                command.Parameters.Add(prDestTopicId);
                #endregion

                #region DestTopicName
                DbParameter prNewTopicName = database.GetParameter(System.Data.DbType.String, "@DestTopicName", topicCopy.Name);
                command.Parameters.Add(prNewTopicName);
                #endregion

                #region DestTopicDescription
                DbParameter prDestTopicDescription = database.GetParameter(System.Data.DbType.String, "@DestTopicDescription", topicCopy.Description);
                command.Parameters.Add(prDestTopicDescription);
                #endregion

                #region DestPageId
                DbParameter prDestPageId = database.GetParameter(System.Data.DbType.Guid, "@DestPageId", topicCopy.PageId);
                command.Parameters.Add(prDestPageId);
                #endregion

                command.CommandText = "Topic_CopyTo";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                connection.Open();
                command.ExecuteNonQuery();
                Add(topicCopy);
            }
        }
        public void TopicMakeMenu(Topic topic, MenuMaster menuMaster)
        {
            Database database = new Database("PSCPortalConnectionString");
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand(connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                #region SourceTopicId
                DbParameter prSourceTopicId = database.GetParameter(System.Data.DbType.Guid, "@SourceTopicId", topic.Id);
                command.Parameters.Add(prSourceTopicId);
                #endregion

                #region DestMenuMasterId
                DbParameter prDestMenuMasterId = database.GetParameter(System.Data.DbType.Guid, "@DestMenuMasterId", menuMaster.Id);
                command.Parameters.Add(prDestMenuMasterId);
                #endregion

                #region DestMenuMasterName
                DbParameter prDestMenuMasterName = database.GetParameter(System.Data.DbType.String, "@DestMenuMasterName", menuMaster.Name);
                command.Parameters.Add(prDestMenuMasterName);
                #endregion

                #region DestMenuMasterDescription
                DbParameter prDestMenuMasterDescription = database.GetParameter(System.Data.DbType.String, "@DestMenuMasterDescription", menuMaster.Description);
                command.Parameters.Add(prDestMenuMasterDescription);
                #endregion



                command.CommandText = "Topic_MakeMenu";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                connection.Open();
                command.ExecuteNonQuery();

            }
        }
        public static Topic GetTopicDescription(string topicid)
        {
            Database database = new Database("PSCPortalConnectionString");
            Topic result = new Topic();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();
                command.CommandText = "select [TopicId],[TopicName],[TopicDescription] from topic where [TopicDescription] = '" + topicid + "'";





                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result = new Topic(reader);
                    return result;
                }
            }
            return result;
        }
        public static Topic GetTopic(string topicid)
        {
            Database database = new Database("PSCPortalConnectionString");
            Topic result = new Topic();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();
                command.CommandText = "select [TopicId],[TopicName],[TopicDescription] from topic where [TopicId] = '" + topicid + "'";





                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result = new Topic(reader);
                    return result;
                }
            }
            return result;
        }
        // get tree by topic parent
        //protected DbCommand GetSelectAllCommandByParent(Topic topic)
        //{
        //    Database database = new Database(ConnectionStringName);
        //    DbCommand command = database.GetCommand();
        //    #region prTopic
        //    DbParameter prTopic = database.GetParameter(System.Data.DbType.Guid, "@Parent", topic.Id);
        //    command.Parameters.Add(prTopic);
        //    #endregion
        //    command.CommandText = "Topic_GetAllByParent";
        //    command.CommandType = System.Data.CommandType.StoredProcedure;
        //    return command;
        //}

        public void UpdatePostionChilds(Topic topic)
        {
            Database database = new Database(ConnectionStringName);
            using (DbConnection connection = database.GetConnection())
            {
                List<DbCommand> commandList = new List<DbCommand>();
                int i = 1;
                foreach (Topic item in topic.Childs)
                {
                    DbCommand command = database.GetCommand(connection);
                    #region TopicId
                    DbParameter prTopicId = database.GetParameter(System.Data.DbType.Guid, "@TopicId", item.Id);
                    command.Parameters.Add(prTopicId);
                    #endregion

                    #region TopicOrder
                    DbParameter prTopicOrder = database.GetParameter(System.Data.DbType.Int32, "@TopicOrder", i++);
                    command.Parameters.Add(prTopicOrder);
                    #endregion

                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "UPDATE dbo.[Topic] SET TopicOrder=@TopicOrder WHERE TopicId=@TopicId";
                    commandList.Add(command);
                }
                connection.Open();
                foreach (DbCommand cmd in commandList)
                    cmd.ExecuteNonQuery();
            }
        }
        public void MoveUp(Topic topic)
        {
            Topic topicParent = (Topic)topic.Parent;
            int index = 0;
            for (; index < topicParent.Childs.Count; index++)
                if (topicParent.Childs[index].Equals(topic))
                    break;

            topicParent.Childs.RemoveAt(index);
            topicParent.Childs.Insert(index - 1, topic);

            UpdatePostionChilds(topicParent);
        }
        public void MoveDown(Topic topic)
        {
            Topic topicParent = (Topic)topic.Parent;
            int index = 0;
            for (; index < topicParent.Childs.Count; index++)
                if (topicParent.Childs[index].Equals(topic))
                    break;

            topicParent.Childs.RemoveAt(index);
            topicParent.Childs.Insert(index + 1, topic);

            UpdatePostionChilds(topicParent);
        }

        public void UpdateLastPosition(Topic topic)
        {
            Topic topicParent = (Topic)topic.Parent;
            int? max = 0;
            SessionManager.DoWork(session =>
         {
             max = session.Query<DB.Entities.Topic>().Where(t => t.TopicParent == topicParent.Id).Max(m => (int?)m.TopicOrder);
             DB.Entities.Topic obj = session.Get<DB.Entities.Topic>(topic.Id);
             obj.TopicOrder = max == null ? 0 : (int)max + 1;
             session.Update(obj);
         });
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