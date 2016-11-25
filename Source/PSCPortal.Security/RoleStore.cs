using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Xml.Linq;
using System.Collections.Generic;
using PSCPortal.Security;

namespace PSCPortal.Security
{
    public class RoleStore
    {
        private RoleCollection _Roles;

        public RoleStore()
        {
            _Roles = new RoleCollection();
            LoadStore();
        }

        public static RoleStore GetStore()
        {
            return null;
        }


        #region "Private Helper Methods"
        private void LoadStore()
        {
            try
            {
                _Roles = RoleCollection.GetRoleCollection();
                
            }
            catch (Exception ex)
            {
                string err = ex.ToString();
            }
        }
        private void SaveStore()
        {
            try
            {
             //   _Roles.Save();
            }
            catch (Exception ex)
            {
                string err = ex.ToString();
            }
        }
        #endregion
        public RoleCollection Roles
        {
            get { return _Roles; }
        }
        public void Save()
        {
            SaveStore();
        }
        public RoleCollection GetRolesForUser(string userName)
        {
            //RoleCollection Results = new RoleCollection();

            //foreach (Role r in _Roles)
            //{
            //    if (r.AssignedUsers.Contains(userName))
            //        Results.Add(r);
            //}
            //return Results;
            return RoleCollection.GetRoleCollection(userName);
        }
        public string[] GetUsersInRole(string roleName)
        {
            Role Role = GetRole(roleName);
            if (Role != null)
            {
                string[] Results = new string[Role.AssignedUsers.Count];
                Role.AssignedUsers.CopyTo(Results, 0);
                return Results;
            }
            else
            {
                throw new Exception(string.Format(
                      "Role with name {0} does not exist!", roleName));
            }
        }
        public Role GetRole(string roleName)
        {
            return Roles.Where(delegate(Role role)
                {
                    return role.Name.Equals(
                        roleName, StringComparison.OrdinalIgnoreCase);
                }).Single<Role>();
        }
    }
}
