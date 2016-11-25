using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PSCPortal.Framework;
using PSCPortal.CMS;
using PSCPortal.Framework.Helpler;
using PSCPortal.Security;

namespace PSCPortal.Systems.CMS
{
    public partial class TopicSecurity : PSCDialog
    {
        protected static TopicPermissionCollection TopicPermissionList
        {
            get
            {
                if (DataStatic["TopicPermissionList"] == null)
                    DataStatic["TopicPermissionList"] = TopicPermissionCollection.GetTopicPermissionCollection();
                return DataStatic["TopicPermissionList"] as TopicPermissionCollection;
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
        protected static TopicAuthentication TopicAuthenticationManager
        {
            get
            {
                if (DataStatic["TopicAuthenticationManager"]==null)
                {
                    DataStatic["TopicAuthenticationManager"] = TopicAuthentication.GetTopicAuthentication(Args.Topic);
                }
                return DataStatic["TopicAuthenticationManager"] as TopicAuthentication;
            }
            set
            {
                DataStatic["TopicAuthenticationManager"] = value;
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
        private static TopicArgs Args
        {
            get
            {
                return DataShare as TopicArgs;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DataBind();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
        }
        /*---------------------------------*/
        [System.Web.Services.WebMethod]
        public static string GetData()
        {
            int i = 0;
            foreach (Role r in RoleList)
            {
                if (r.Name == System.Configuration.ConfigurationManager.AppSettings["GroupAdmin"])
                    continue;
                AuthenticationList.Add(new List<bool>());
                foreach (TopicPermission tp in TopicPermissionList)
                {
                    bool contains = TopicAuthenticationManager.GetRolesForPermission(tp).Contains(r);
                    AuthenticationList[i].Add(contains);
                }
                i++;
            }
            IEnumerable<Role> iRoleList = RoleList.Where(r => r.Name != System.Configuration.ConfigurationManager.AppSettings["GroupAdmin"]);
            var result = new { TopicPermissionList= TopicPermissionList, RoleList=iRoleList, AuthenticationList=AuthenticationList };
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
                        TopicPermission topicPermission=TopicPermissionList[j];
                        if (subItem)
                        {
                            TopicAuthenticationManager.AddPermission(topicPermission, role);
                        }
                        else
                        {
                            TopicAuthenticationManager.RemovePermission(topicPermission,role);
                        }
                    }
                    j++;
                }
                i++;
            }
        }
        /*---------------------------------*/
        //[System.Web.Services.WebMethod]
        //public static string GetRolesForPermission(int idPermission)
        //{
        //    System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
        //    List<Role> listRole = TopicAuthenticationManager.GetRolesForPermission(TopicPermissionList.Where(t => t.Id == idPermission).Single());
        //    return js.Serialize(listRole);
        //}
        //[System.Web.Services.WebMethod]
        //public static string GetRolesAvailableForPermission(int idPermission)
        //{
        //    System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
        //    List<Role> listRole = TopicAuthenticationManager.GetRolesForPermission(TopicPermissionList.Where(t => t.Id == idPermission).Single());
        //    List<Role> result = new List<Role>();
        //    foreach (Role item in RoleList)
        //        if (!listRole.Contains(item))
        //            result.Add(item);
        //    return js.Serialize(result);
        //}        
        //[System.Web.Services.WebMethod]
        //public static void AddAuthenticationRole(int idPermission, string idRole)
        //{
        //    Guid roleId = new Guid(idRole);
        //    TopicAuthenticationManager.AddPermission(TopicPermissionList.Where(t => t.Id == idPermission).Single(), RoleList.Where(r => r.Id == roleId).Single());
        //}
        //[System.Web.Services.WebMethod]
        //public static void RemoveAuthenticationRole(int idPermission, string idRole)
        //{
        //    Guid roleId = new Guid(idRole);
        //    TopicAuthenticationManager.RemovePermission(TopicPermissionList.Where(t => t.Id == idPermission).Single(), RoleList.Where(r => r.Id == roleId).Single());
        //}
    }
}
