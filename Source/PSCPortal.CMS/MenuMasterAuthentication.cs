using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PSCPortal.Security;
using PSCPortal.Framework;
using System.Data.Common;

namespace PSCPortal.CMS
{
   [Serializable]
    public class MenuMasterAuthentication
    {
        private MenuMaster _MenuMaster;
        public MenuMaster MenuMaster
        {
            get
            {
                return _MenuMaster;
            }
        }
        private Dictionary<MenuMasterPermission, List<Role>> _listAuthentication = new Dictionary<MenuMasterPermission, List<Role>>();
        private MenuMasterAuthentication()
        {
        }
        public List<Role> GetRolesForPermission(MenuMasterPermission permission)
        {
            return _listAuthentication[permission];
        }
        public void AddPermission(MenuMasterPermission MenuMasterpermission, Role role)
        {
            _listAuthentication[MenuMasterpermission].Add(role);
            Database database = new Database("PSCPortalConnectionString");
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand(connection);
                #region MenuMasterId
                DbParameter prMenuMasterId = database.GetParameter(System.Data.DbType.Guid, "@MenuMasterId", _MenuMaster.Id);
                command.Parameters.Add(prMenuMasterId);
                #endregion

                #region MenuMasterPermissionId
                DbParameter prMenuMasterPermissionId = database.GetParameter(System.Data.DbType.Int32, "@MenuMasterPermissionId", MenuMasterpermission.Id);
                command.Parameters.Add(prMenuMasterPermissionId);
                #endregion

                #region RoleId
                DbParameter prRoleId = database.GetParameter(System.Data.DbType.Guid, "@RoleId", role.Id);
                command.Parameters.Add(prRoleId);
                #endregion

                command.CommandText = "MenuMasterAuthentication_Insert";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void RemovePermission(MenuMasterPermission MenuMasterpermission, Role role)
        {
            _listAuthentication[MenuMasterpermission].Remove(role);
            Database database = new Database("PSCPortalConnectionString");
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand(connection);
                #region MenuMasterId
                DbParameter prMenuMasterId = database.GetParameter(System.Data.DbType.Guid, "@MenuMasterId", _MenuMaster.Id);
                command.Parameters.Add(prMenuMasterId);
                #endregion

                #region MenuMasterPermissionId
                DbParameter prMenuMasterPermissionId = database.GetParameter(System.Data.DbType.Int32, "@MenuMasterPermissionId", MenuMasterpermission.Id);
                command.Parameters.Add(prMenuMasterPermissionId);
                #endregion

                #region RoleId
                DbParameter prRoleId = database.GetParameter(System.Data.DbType.Guid, "@RoleId", role.Id);
                command.Parameters.Add(prRoleId);
                #endregion

                command.CommandText = "MenuMasterAuthentication_Delete";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public bool IsAllow(MenuMasterPermission.PERMISSION permission)
        {
            if (System.Web.HttpContext.Current.User.IsInRole(System.Configuration.ConfigurationManager.AppSettings["GroupAdmin"]))
                return true;

            List<Role> listRole = GetRolesForPermission(MenuMasterPermission.Parse(permission));
            foreach (Role role in listRole)
                if (System.Web.HttpContext.Current.User.IsInRole(role.Name))
                    return true;
            return false;
        }

        public static MenuMasterAuthentication GetMenuMasterAuthentication(MenuMaster MenuMaster)
        {
            MenuMasterAuthentication result = new MenuMasterAuthentication();
            result._MenuMaster = MenuMaster;
            MenuMasterPermissionCollection MenuMasterPermissionList = MenuMasterPermissionCollection.GetMenuMasterPermissionCollection();
            foreach (MenuMasterPermission item in MenuMasterPermissionList)
                result._listAuthentication.Add(item, new List<Role>());
            RoleCollection roleList = RoleCollection.GetRoleCollection();
            Database database = new Database("PSCPortalConnectionString");
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand(connection);

                #region MenuMasterId
                DbParameter prMenuMasterId = database.GetParameter(System.Data.DbType.Guid, "@MenuMasterId", MenuMaster.Id);
                command.Parameters.Add(prMenuMasterId);
                #endregion

                command.CommandText = "MenuMasterAuthentication_GetAllByMenuMasterId";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                int idtptemp;
                Guid idrtemp;
                MenuMasterPermission tptemp;
                Role rtemp;
                while (reader.Read())
                {
                    idtptemp = (int)reader["MenuMasterPermissionId"];
                    idrtemp = (Guid)reader["RoleId"];
                    tptemp = MenuMasterPermissionList.Where(tp => tp.Id == idtptemp).Single();
                    rtemp = roleList.Where(r => r.Id == idrtemp).Single();
                    result._listAuthentication[tptemp].Add(rtemp);
                }
            }
            return result;
        }
    }
}
