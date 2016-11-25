using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework.Helpler;
using PSCPortal.Security;
using PSCPortal.Framework;

namespace PSCPortal.CMS
{
    [Serializable]
    public class TopicAuthentication
    {
        private Topic _topic;
        public Topic Topic
        {
            get
            {
                return _topic;
            }
        }
        private Dictionary<TopicPermission, List<Role>> _listAuthentication = new Dictionary<TopicPermission, List<Role>>();
        private TopicAuthentication()
        {
        }
        public List<Role> GetRolesForPermission(TopicPermission permission)
        {
            return _listAuthentication[permission];
        }
        public void AddPermission(TopicPermission topicpermission, Role role)
        {
            _listAuthentication[topicpermission].Add(role);
            Database database = new Database("PSCPortalConnectionString");
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand(connection);
                #region TopicId
                DbParameter prTopicId = database.GetParameter(System.Data.DbType.Guid, "@TopicId", _topic.Id);
                command.Parameters.Add(prTopicId);
                #endregion

                #region TopicPermissionId
                DbParameter prTopicPermissionId = database.GetParameter(System.Data.DbType.Int32, "@TopicPermissionId", topicpermission.Id);
                command.Parameters.Add(prTopicPermissionId);
                #endregion

                #region RoleId
                DbParameter prRoleId = database.GetParameter(System.Data.DbType.Guid, "@RoleId", role.Id);
                command.Parameters.Add(prRoleId);
                #endregion

                command.CommandText = "TopicAuthentication_Insert";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void RemovePermission(TopicPermission topicpermission, Role role)
        {
            _listAuthentication[topicpermission].Remove(role);
            Database database = new Database("PSCPortalConnectionString");
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand(connection);
                #region TopicId
                DbParameter prTopicId = database.GetParameter(System.Data.DbType.Guid, "@TopicId", _topic.Id);
                command.Parameters.Add(prTopicId);
                #endregion

                #region TopicPermissionId
                DbParameter prTopicPermissionId = database.GetParameter(System.Data.DbType.Int32, "@TopicPermissionId", topicpermission.Id);
                command.Parameters.Add(prTopicPermissionId);
                #endregion

                #region RoleId
                DbParameter prRoleId = database.GetParameter(System.Data.DbType.Guid, "@RoleId", role.Id);
                command.Parameters.Add(prRoleId);
                #endregion

                command.CommandText = "TopicAuthentication_Delete";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public bool IsAllow(TopicPermission.PERMISSION permission)
        {
            if (System.Web.HttpContext.Current.User.IsInRole(System.Configuration.ConfigurationManager.AppSettings["GroupAdmin"]))
                return true;

            List<Role> listRole = GetRolesForPermission(TopicPermission.Parse(permission));
            foreach (Role role in listRole)
                if (System.Web.HttpContext.Current.User.IsInRole(role.Name))
                    return true;
            return false;
        }
       
        public static TopicAuthentication GetTopicAuthentication(Topic topic)
        {
            TopicAuthentication result = new TopicAuthentication();
            result._topic = topic;
            TopicPermissionCollection topicPermissionList = TopicPermissionCollection.GetTopicPermissionCollection();
            foreach (TopicPermission item in topicPermissionList)
                result._listAuthentication.Add(item, new List<Role>());
            RoleCollection roleList = RoleCollection.GetRoleCollection();
            Database database = new Database("PSCPortalConnectionString");
            using (DbConnection connection = database.GetConnection())
            {                
                DbCommand command = database.GetCommand(connection);

                #region TopicId
                DbParameter prTopicId = database.GetParameter(System.Data.DbType.Guid, "@TopicId", topic.Id);
                command.Parameters.Add(prTopicId);
                #endregion

                command.CommandText = "TopicAuthentication_GetAllByTopicId";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                int idtptemp;
                Guid idrtemp;
                TopicPermission tptemp;
                Role rtemp;
                while (reader.Read())
                {
                    idtptemp = (int)reader["TopicPermissionId"];
                    idrtemp = (Guid)reader["RoleId"];
                    tptemp = topicPermissionList.Where(tp => tp.Id == idtptemp).Single();
                    rtemp = roleList.Where(r => r.Id == idrtemp).Single();                    
                    result._listAuthentication[tptemp].Add(rtemp);
                }
            }
            return result;
        }
    }
}
