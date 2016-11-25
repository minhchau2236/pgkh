using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PSCPortal.Framework;
using System.Data.Common;
using PSCPortal.Security;

namespace PSCPortal.Engine
{
    [Serializable]
    public class PageAuthentication
    {
        private Page _Page;
        public Page Page
        {
            get
            {
                return _Page;
            }
        }
        private Dictionary<PagePermission, List<Role>> _listAuthentication = new Dictionary<PagePermission, List<Role>>();
        public PageAuthentication()
        {
        }
        public List<Role> GetRolesForPermission(PagePermission permission)
        {
            return _listAuthentication[permission];
        }
        public void AddPermission(PagePermission Pagepermission, Role role)
        {
            _listAuthentication[Pagepermission].Add(role);
            Database database = new Database("PSCPortalConnectionString");
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand(connection);
                #region PageId
                DbParameter prPageId = database.GetParameter(System.Data.DbType.Guid, "@PageId", _Page.Id);
                command.Parameters.Add(prPageId);
                #endregion

                #region PagePermissionId
                DbParameter prPagePermissionId = database.GetParameter(System.Data.DbType.Int32, "@PagePermissionId", Pagepermission.Id);
                command.Parameters.Add(prPagePermissionId);
                #endregion

                #region RoleId
                DbParameter prRoleId = database.GetParameter(System.Data.DbType.Guid, "@RoleId", role.Id);
                command.Parameters.Add(prRoleId);
                #endregion

                command.CommandText = "PageAuthentication_Insert";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void RemovePermission(PagePermission Pagepermission, Role role)
        {
            _listAuthentication[Pagepermission].Remove(role);
            Database database = new Database("PSCPortalConnectionString");
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand(connection);
                #region PageId
                DbParameter prPageId = database.GetParameter(System.Data.DbType.Guid, "@PageId", _Page.Id);
                command.Parameters.Add(prPageId);
                #endregion

                #region PagePermissionId
                DbParameter prPagePermissionId = database.GetParameter(System.Data.DbType.Int32, "@PagePermissionId", Pagepermission.Id);
                command.Parameters.Add(prPagePermissionId);
                #endregion

                #region RoleId
                DbParameter prRoleId = database.GetParameter(System.Data.DbType.Guid, "@RoleId", role.Id);
                command.Parameters.Add(prRoleId);
                #endregion

                command.CommandText = "PageAuthentication_Delete";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public  bool IsAllow(PagePermission.PERMISSION permission)
        {
            if (System.Web.HttpContext.Current.User.IsInRole(System.Configuration.ConfigurationManager.AppSettings["GroupAdmin"]))
                return true;
            
            List<Role> listRole = GetRolesForPermission(PagePermission.Parse(permission));
            foreach (Role role in listRole)
                if (System.Web.HttpContext.Current.User.IsInRole(role.Name))
                    return true;
            return false;
        }
       
        public static PageAuthentication GetPageAuthentication(Page Page)
        {
            PageAuthentication result = new PageAuthentication();
            result._Page = Page;
            PagePermissionCollection PagePermissionList = PagePermissionCollection.GetPagePermissionCollection();
            foreach (PagePermission item in PagePermissionList)
            {
              
                result._listAuthentication.Add(item, new List<Role>());
            }
            RoleCollection roleList = RoleCollection.GetRoleCollection();
            Database database = new Database("PSCPortalConnectionString");
            using (DbConnection connection = database.GetConnection())
            {                
                DbCommand command = database.GetCommand(connection);

                #region PageId
                DbParameter prPageId = database.GetParameter(System.Data.DbType.Guid, "@PageId", Page.Id);
                command.Parameters.Add(prPageId);
                #endregion

                command.CommandText = "PageAuthentication_GetAllByPageId";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                int idtptemp;
                Guid idrtemp;
                PagePermission tptemp;
                Role rtemp;
                while (reader.Read())
                {
                    idtptemp = (int)reader["PagePermissionId"];
                    idrtemp = (Guid)reader["RoleId"];
                    tptemp = PagePermissionList.Where(tp => tp.Id == idtptemp).Single();
                   
                    rtemp = roleList.Where(r => r.Id == idrtemp).Single();
                    result._listAuthentication[tptemp].Add(rtemp);
                }
            }
            return result;
        }
    }
}
