using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PSCPortal.Framework;
using System.Data.Common;
using PSCPortal.Engine;
using PSCPortal.Framework.Helpler;
namespace PSCPortal.Security
{
    public static class SystemAuthentication
    {
        public static Dictionary<FUNCTIONS, List<Role>> FunctionAuthenticationList
        {
            get
            {
                System.Web.HttpContext.Current.Application.Lock();
                if (System.Web.HttpContext.Current.Application["FunctionAuthenticationList"] == null)
                {
                    Dictionary<FUNCTIONS, List<Role>> fal = new Dictionary<FUNCTIONS, List<Role>>();
                    FunctionCollection functionList = FunctionCollection.GetFunctionCollection();
                    foreach (Function function in functionList)
                        fal.Add(Function.Parse(function.Id), new List<Role>());
                    System.Web.HttpContext.Current.Application["FunctionAuthenticationList"] = fal;
                    LoadDataSystemAuthentication();
                }
                System.Web.HttpContext.Current.Application.UnLock();
                return System.Web.HttpContext.Current.Application["FunctionAuthenticationList"] as Dictionary<FUNCTIONS, List<Role>>;
            }
        }
        public static List<Role> GetRolesForFunction(FUNCTIONS function)
        {
            List<Role> listRoles = FunctionAuthenticationList[function].ToList();
            //Role roleAdmin = RoleCollection.GetRoleCollection().Where(r => r.Name == System.Configuration.ConfigurationManager.AppSettings["GroupAdmin"]).Single();
            //listRoles.Add(roleAdmin);
            return listRoles;
        }
        public static void RemoveRole(string roleName)
        {
            foreach (KeyValuePair<FUNCTIONS, List<Role>> item in FunctionAuthenticationList)
            {
                Role role = null;
                try
                {
                    role = item.Value.Where(r => r.Name == roleName).Single();
                }
                catch { }
                if (role != null)
                    item.Value.Remove(role);
            }
        }
        public static bool CheckAllowFunction(FUNCTIONS function)
        {
            if (System.Web.HttpContext.Current.User.IsInRole(System.Configuration.ConfigurationManager.AppSettings["GroupAdmin"]))
                return true;
            string subId = SessionHelper.GetSession(SessionKey.SubDomain);
            List<Role> listRoles = GetRolesForFunction(function);
            foreach (Role role in listRoles)
            {
                if (System.Web.HttpContext.Current.User.IsInRole(role.Name))
                {
                    SubDomainCollection sub = SubDomainCollection.GetSubDomainCollection(role);
                    if (sub.Count(a => a.Id == new Guid(subId)) > 0)
                        return true;
                }
            }
            return false;
        }
        public static void AddRoleForFunction(FUNCTIONS function, Role role)
        {
            FunctionAuthenticationList[function].Add(role);
            AddRoleForFunctionDB(function, role);
        }
        public static void RemoveRoleForFunction(FUNCTIONS function, Role role)
        {
            FunctionAuthenticationList[function].Remove(role);
            RemoveRoleForFunctionDB(function, role);
        }
        public static void LoadDataSystemAuthentication()
        {
            Database database = new Database(ConnectionStringName);
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = GetSelectAllCommand();
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                int funId = 0;
                FUNCTIONS function;
                Role role = null;
                while (reader.Read())
                {
                    funId = (int)reader["FunctionId"];
                    role = new Role { Id = new Guid(reader["RoleId"].ToString()), Name = reader["RoleName"].ToString() };
                    function = Function.Parse(funId);
                    FunctionAuthenticationList[function].Add(role);
                }
            }
        }

        private static DbCommand GetSelectAllCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            command.CommandText = "select s.RoleId, RoleName, RoleDescription, FunctionId from SystemAuthentication s inner join [Role] r on s.RoleId=r.RoleId";
            return command;
        }
        public static void AddRoleForFunctionDB(FUNCTIONS function, Role role)
        {
            Database database = new Database(ConnectionStringName);
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();
                command.Connection = connection;
                #region RoleId
                DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@RoleId", role.Id);
                command.Parameters.Add(prId);
                #endregion
                #region FunctionId
                DbParameter prFunctionId = database.GetParameter(System.Data.DbType.Int32, "@FunctionId", (int)function);
                command.Parameters.Add(prFunctionId);
                #endregion
                command.CommandText = "insert into SystemAuthentication(RoleId, FunctionId) values (@RoleId, @FunctionId)";
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public static void RemoveRoleForFunctionDB(FUNCTIONS function, Role role)
        {
            Database database = new Database(ConnectionStringName);
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();
                command.Connection = connection;
                #region RoleId
                DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@RoleId", role.Id);
                command.Parameters.Add(prId);
                #endregion
                #region FunctionId
                DbParameter prFunctionId = database.GetParameter(System.Data.DbType.Int32, "@FunctionId", (int)function);
                command.Parameters.Add(prFunctionId);
                #endregion
                command.CommandText = "delete SystemAuthentication where RoleId=@RoleId and FunctionId=@FunctionId";
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private static string ConnectionStringName
        {
            get
            {
                return "PSCPortalConnectionString";
            }
        }
    }
}
