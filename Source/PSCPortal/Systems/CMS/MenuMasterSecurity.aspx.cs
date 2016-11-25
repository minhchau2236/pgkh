using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PSCPortal.Framework;
using PSCPortal.CMS;
using PSCPortal.Security;
using PSCPortal.Framework.Helpler;
namespace PSCPortal.Systems.CMS
{
    public partial class MenuMasterSecurity : PSCDialog
    {
        protected static MenuMasterPermissionCollection MenuMasterPermissionList
        {
            get
            {
                if (DataStatic["MenuMasterPermissionList"] == null)
                    DataStatic["MenuMasterPermissionList"] = MenuMasterPermissionCollection.GetMenuMasterPermissionCollection();
                return DataStatic["MenuMasterPermissionList"] as MenuMasterPermissionCollection;
            }
        }
        protected static RoleCollection RoleList
        {
            get
            {
                if (DataStatic["RoleList"] == null)
                {
                    Guid subId = SessionHelper.GetSession(SessionKey.SubDomain) == string.Empty ? Guid.Empty : new Guid(SessionHelper.GetSession(SessionKey.SubDomain));
                    DataStatic["RoleList"] = subId == Guid.Empty
                        ? RoleCollection.GetRoleCollection()
                        : RoleCollection.GetRoleCollectionBySubDomain(subId);
                }
                return DataStatic["RoleList"] as RoleCollection;
            }
        }
        protected static MenuMasterAuthentication MenuMasterAuthenticationManager
        {
            get
            {
                if (DataStatic["MenuMasterAuthenticationManager"] == null)
                {
                    DataStatic["MenuMasterAuthenticationManager"] = MenuMasterAuthentication.GetMenuMasterAuthentication(Args.MenuMaster);
                }
                return DataStatic["MenuMasterAuthenticationManager"] as MenuMasterAuthentication;
            }
            set
            {
                DataStatic["MenuMasterAuthenticationManager"] = value;
            }
        }
        protected static List<List<bool>> AuthenticationList
        {
            get
            {
                if (DataStatic["AuthenticationList"] == null)
                    DataStatic["AuthenticationList"] = new List<List<bool>>();
                return DataStatic["AuthenticationList"] as List<List<bool>>;
            }
            set
            {
                DataStatic["AuthenticationList"] = value;
            }
        }
        private static MenuMasterArgs Args
        {
            get
            {
                return DataShare as MenuMasterArgs;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DataBind();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
        }
        [System.Web.Services.WebMethod]
        public static string GetData()
        {
            int i = 0;
            foreach (Role r in RoleList)
            {
                if (r.Name == System.Configuration.ConfigurationManager.AppSettings["GroupAdmin"])
                    continue;
                AuthenticationList.Add(new List<bool>());
                foreach (MenuMasterPermission tp in MenuMasterPermissionList)
                {
                    AuthenticationList[i].Add(MenuMasterAuthenticationManager.GetRolesForPermission(tp).Contains(r));
                }
                i++;
            }
            IEnumerable<Role> iRoleList = RoleList.Where(r => r.Name != System.Configuration.ConfigurationManager.AppSettings["GroupAdmin"]);
            var result = new { MenuMasterPermissionList = MenuMasterPermissionList, RoleList = iRoleList, AuthenticationList = AuthenticationList };
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            return js.Serialize(result);
        }
        [System.Web.Services.WebMethod]
        public static void Save(object arr)
        {
            int i = 0;
            IEnumerable<Role> iRoleList = RoleList.Where(r => r.Name != System.Configuration.ConfigurationManager.AppSettings["GroupAdmin"]);
            foreach (object[] item in (object[])arr)
            {
                int j = 0;
                foreach (bool subItem in item)
                {
                    if (subItem != AuthenticationList[i][j])
                    {
                        Role role = iRoleList.ElementAt(i);
                        MenuMasterPermission topicPermission = MenuMasterPermissionList[j];
                        if (subItem)
                        {
                            MenuMasterAuthenticationManager.AddPermission(topicPermission, role);
                        }
                        else
                        {
                            MenuMasterAuthenticationManager.RemovePermission(topicPermission, role);
                        }
                    }
                    j++;
                }
                i++;
            }
        }
        
    }
}
