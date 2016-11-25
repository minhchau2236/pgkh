using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PSCPortal.Framework;
using PSCPortal.Security;
using PSCPortal.Engine;
using PSCPortal.Framework.Helpler;
namespace PSCPortal.Systems.Engine
{
    public partial class PageSecurity :   PSCDialog
    {
        protected static PagePermissionCollection PagePermissionList
        {
            get
            {
                if (DataStatic["PagePermissionList"] == null)
                    DataStatic["PagePermissionList"] = PagePermissionCollection.GetPagePermissionCollection();
                return DataStatic["PagePermissionList"] as PagePermissionCollection;
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
        protected static PageAuthentication PageAuthenticationManager
        {
            get
            {
                if (DataStatic["PageAuthenticationManager"] == null)
                {
                    DataStatic["PageAuthenticationManager"] = PageAuthentication.GetPageAuthentication(Args.Page);
                }
                return DataStatic["PageAuthenticationManager"] as PageAuthentication;
            }
            set
            {
                DataStatic["PageAuthenticationManager"] = value;
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
        private static PageArgs Args
        {
            get
            {
                return DataShare as PageArgs;
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
                foreach (PagePermission tp in PagePermissionList)
                {
                    AuthenticationList[i].Add(PageAuthenticationManager.GetRolesForPermission(tp).Contains(r));
                }
                i++;
            }
            IEnumerable<Role> iRoleList = RoleList.Where(r => r.Name != System.Configuration.ConfigurationManager.AppSettings["GroupAdmin"]);
            var result = new { PagePermissionList = PagePermissionList, RoleList = iRoleList, AuthenticationList = AuthenticationList };
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
                        PagePermission pagePermission = PagePermissionList[j];
                        if (subItem)
                        {
                            PageAuthenticationManager.AddPermission(pagePermission, role);
                        }
                        else
                        {
                            PageAuthenticationManager.RemovePermission(pagePermission, role);
                        }
                    }
                    j++;
                }
                i++;
            }
        }
    }
}
