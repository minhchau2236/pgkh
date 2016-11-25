using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PSCPortal.Framework.Helpler;
using PSCPortal.Security;
using PSCPortal.Framework;
using PSCPortal.Engine;

namespace PSCPortal.Systems.Security
{
    public partial class RoleManage : PSCPortal.Framework.PSCDialog
    {
        protected static RoleCollection RoleList
        {
            get
            {
                if (DataStatic["RoleList"] == null)
                {
                    Guid subId = new Guid(SessionHelper.GetSession(SessionKey.SubDomain));
                    DataStatic["RoleList"] = subId == Guid.Empty ? RoleCollection.GetRoleCollection() : RoleCollection.GetRoleCollectionBySubDomain(subId);
                }
                return DataStatic["RoleList"] as RoleCollection;
            }
        }

        [System.Web.Services.WebMethod]
        public static string GetRoleList(int startIndex, int maximumRows, string sortExpressions)
        {
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            return js.Serialize(RoleList.GetSegment(startIndex, maximumRows, sortExpressions));
        }
        [System.Web.Services.WebMethod]
        public static int GetRoleCount()
        {
            return RoleList.Count;
        }
        [System.Web.Services.WebMethod]
        public static void RoleNew()
        {
            Role item = new Role();
            item.Id = Guid.NewGuid();
            PSCDialog.DataShare = new RoleArgs(item, false);
        }
        [System.Web.Services.WebMethod]
        public static void RoleAdd()
        {
            Role role = ((RoleArgs)PSCDialog.DataShare).Role;
            RoleList.AddDB(role);
            // check subdomain
            Guid subId = SessionHelper.GetSession(SessionKey.SubDomain) == string.Empty ? Guid.Empty : new Guid(SessionHelper.GetSession(SessionKey.SubDomain));
            if (!(subId == Guid.Empty))
            {
                SubDomainInRole sir = new SubDomainInRole();
                sir.RoleId = role.Id;
                sir.SubDomainId = subId;
                sir.AddDB();
            }
        }
        [System.Web.Services.WebMethod]
        public static void RoleEdit(string id)
        {
            Guid idRole = new Guid(id);
            PSCDialog.DataShare = new RoleArgs(RoleList.Where(a => a.Id == idRole).Single(), true);
        }
        [System.Web.Services.WebMethod]
        public static void RoleAuthenticationEdit(string id)
        {
            Guid idRole = new Guid(id);
            PSCDialog.DataShare = new RoleArgs(RoleList.Where(a => a.Id == idRole).Single(), true);
        }
        [System.Web.Services.WebMethod]
        public static void RoleUpdate()
        {
            (PSCDialog.DataShare as PSCPortal.Security.RoleArgs).Role.Update();
            DataStatic["RoleList"] = null;
        }
        [System.Web.Services.WebMethod]
        public static void RoleDelete(string[] arrId)
        {
            foreach (string id in arrId)
            {
                Guid idRole = new Guid(id);
                Role role = RoleList.Where(a => a.Id == idRole).Single();
                RoleList.RemoveDB(role);
                SystemAuthentication.RemoveRole(role.Name);
            }
        }
        [System.Web.Services.WebMethod]
        public static void ConfigSubDomain(string id)
        {
            Guid idRole = new Guid(id);
            PSCDialog.DataShare = new RoleArgs(RoleList.Where(a => a.Id == idRole).Single(), true);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            // DataBind();
        }
        [System.Web.Services.WebMethod]
        public static string GetPermission(int[] arr)
        {
            Dictionary<string, bool> result = new Dictionary<string, bool>();
            foreach (int idFun in arr)
            {
                result.Add(idFun.ToString(), PSCPortal.Security.SystemAuthentication.CheckAllowFunction(PSCPortal.Security.Function.Parse(idFun)));
            }
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            return js.Serialize(result);
        }
    }
}
