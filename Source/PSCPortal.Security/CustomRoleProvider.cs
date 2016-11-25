using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Configuration.Provider;
using System.Collections.Specialized;
using System.Web.Security;

namespace PSCPortal.Security
{
    public class CustomRoleProvider : RoleProvider
    {
        /*
        private RoleStore _CurrentStore = null;
        private RoleStore CurrentStore
        {
            get
            {
                if (_CurrentStore == null)
                    _CurrentStore = new RoleStore();
                return _CurrentStore;
            }
        }*/
        /*
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            try
            {
                // Get the roles to be modified
                foreach (string roleName in roleNames)
                {
                    Role Role = CurrentStore.GetRole(roleName);
                    if (Role != null)
                    {
                        foreach (string userName in usernames)
                        {
                            if (!Role.AssignedUsers.Contains(userName))
                            {
                                Role.AssignedUsers.Add(userName);
                            }
                        }
                    }
                }
                CurrentStore.Save();
            }
            catch
            {
                // If an exception is raised while saving the storage 
                // or while serializing contents we just forward it to the
                // caller. It would be cleaner to work with custom exception
                // classes here and pass more detailed information to the caller
                // but we leave as is for simplicity.
                throw;
            }
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

       public void CustomCreateRole(string roleName, string roleDesciption)
       {
            
       }
        
       public override void CreateRole(string roleName)
        {
            try
            {
                Role NewRole = new Role();
                NewRole.Id = Guid.NewGuid();
                NewRole.Name = roleName;
                NewRole.AssignedUsers = new StringCollection();
                CurrentStore.Roles.Add(NewRole);
                CurrentStore.Save();
            }
            catch
            {
                // If an exception is raised while saving the storage 
                // or while serializing contents we just forward it to the
                // caller. It would be cleaner to work with custom exception
                // classes here and pass more detailed information to the caller
                // but we leave as is for simplicity.
                throw;
            }
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            try
            {
                List<string> Results = new List<string>();
                Regex Expression = new Regex(usernameToMatch.Replace("%", @"\w*"));
                Role Role = CurrentStore.GetRole(roleName);
                if (Role != null)
                {
                    foreach (string userName in Role.AssignedUsers)
                    {
                        if (Expression.IsMatch(userName))
                            Results.Add(userName);
                    }
                }
                else
                {
                    throw new ProviderException("Role does not exist!");
                }
                return Results.ToArray();
            }
            catch
            {
                // If an exception is raised while saving the storage 
                // or while serializing contents we just forward it to the
                // caller. It would be cleaner to work with custom exception
                // classes here and pass more detailed information to the caller
                // but we leave as is for simplicity.
                throw;
            }
        }
        
        public override string[] GetAllRoles()
        {
            RoleCollection rolCol = RoleCollection.GetRoleCollection();
            string[] rolNames = new string[rolCol.Count];
            for(int i=0;i<rolCol.Count;i++)
            {
                rolNames[i] = rolCol.ElementAt(i).Name;
            }
            return rolNames;
        }

        public override string[] GetRolesForUser(string username)
        {
            try
            {
                RoleCollection RolesForUser = CurrentStore.GetRolesForUser(username);
                string[] Results = new string[RolesForUser.Count];
                for (int i = 0; i < Results.Length; i++)
                    Results[i] = RolesForUser.ElementAt(i).Name;
                return Results;
            }
            catch
            {
                // If an exception is raised while saving the storage 
                // or while serializing contents we just forward it to the
                // caller. It would be cleaner to work with custom exception
                // classes here and pass more detailed information to the caller
                // but we leave as is for simplicity.
                throw;
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            try
            {
                return CurrentStore.GetUsersInRole(roleName);
            }
            catch
            {
                // If an exception is raised while saving the storage 
                // or while serializing contents we just forward it to the
                // caller. It would be cleaner to work with custom exception
                // classes here and pass more detailed information to the caller
                // but we leave as is for simplicity.
                throw;
            }
        }

        public override bool IsUserInRole(string username, string roleName)
        {        
            try
            {
                Role Role = CurrentStore.GetRole(roleName);
                if (Role != null)
                {                    
                    return Role.AssignedUsers.Contains(username);
                }
                else
                {
                    // Requires import of System.Configuration.Provider
                    throw new ProviderException("Role does not exist!");
                }
            }
            catch
            {
                // If an exception is raised while saving the storage 
                // or while serializing contents we just forward it to the
                // caller. It would be cleaner to work with custom exception
                // classes here and pass more detailed information to the caller
                // but we leave as is for simplicity.
                throw;
            }
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            try
            {
                // Get the roles to be modified
     
                foreach (string roleName in roleNames)
                {
                    Role Role = CurrentStore.GetRole(roleName);
                    if (Role != null)
                    {
                        foreach (string userName in usernames)
                        {
                            if (Role.AssignedUsers.Contains(userName))
                            {
                                Role.AssignedUsers.Remove(userName);
                            }
                        }
                    }
                }
                CurrentStore.Save();
            }
            catch
            {
                // If an exception is raised while saving the storage 
                // or while serializing contents we just forward it to the
                // caller. It would be cleaner to work with custom exception
                // classes here and pass more detailed information to the caller
                // but we leave as is for simplicity.
                throw;
            }
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }*/

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            List<string> roles = new List<string>();
            if (username.ToLower() != "psc")
            {
                PSCPortal.Security.User user = PSCPortal.Security.UserCollection.GetUserCollection().Single<PSCPortal.Security.User>(u => u.Name == username);
                RoleCollection rolesOfUser = user.GetRolesBelongTo();

                foreach (Role role in rolesOfUser)
                {
                    roles.Add(role.Name);
                }
            }
            if (username.ToLower() == "psc")
            {
                roles.Add(System.Configuration.ConfigurationManager.AppSettings["GroupAdmin"].ToString());
            }

            return roles.ToArray();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}
