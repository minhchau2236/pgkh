using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using PSCPortal.Security;
using System.Collections.Generic;
using PSCPortal.Framework;
using PSCPortal.Framework.Helpler;
namespace PSCPortal.Systems.Security
{
    public partial class ViewSystemPermission : PSCPortal.Framework.PSCPage
    {
        public static FunctionCategoryCollection FunctionCategoryList
        {
            get
            {
                if (DataStatic["FunctionCategoryList"] == null)
                    DataStatic["FunctionCategoryList"] = FunctionCategoryCollection.GetFunctionCategoryCollection();
                return DataStatic["FunctionCategoryList"] as FunctionCategoryCollection;
            }
        }
        protected static RoleCollection RoleList
        {
            get
            {
                if (DataStatic["RoleList"] == null)
                {
                    Guid subId = SessionHelper.GetSession(SessionKey.SubDomain) == string.Empty ? Guid.Empty : new Guid(SessionHelper.GetSession(SessionKey.SubDomain));
                    if (subId == Guid.Empty)
                        DataStatic["RoleList"] = RoleCollection.GetRoleCollection();
                    else
                    {
                        DataStatic["RoleList"] = RoleCollection.GetRoleCollectionBySubDomain(subId);
                    }                    
                }
                return DataStatic["RoleList"] as RoleCollection;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            if (!IsPostBack)
                LoadData();
        }
        protected void LoadData()
        {
            IEnumerable<Role> iRoleList = RoleList.Where(r => r.Name != System.Configuration.ConfigurationManager.AppSettings["GroupAdmin"]);
            rcbRole.DataSource = iRoleList;
            rcbRole.DataBind();
        }
        [System.Web.Services.WebMethod]
        public static string GetData(string roleId)
        {
            Role role = RoleList.Where(r=>r.Id==(new Guid(roleId))).Single();
            List<List<Function>> FunctionList = new List<List<Function>>();
            List<List<bool>> PermissionList = new List<List<bool>>();
            int i=0;
            foreach (FunctionCategory fca in FunctionCategoryList)
            {
                FunctionList.Add(new List<Function>());
                PermissionList.Add(new List<bool>());
                FunctionList[i] = fca.FunctionList;
                foreach (Function f in FunctionList[i])
                {
                    PermissionList[i].Add(SystemAuthentication.FunctionAuthenticationList[Function.Parse(f.Id)].Contains(role));
                }
                i++;
            }
            var result = new { FunctionCategoryList = FunctionCategoryList , FunctionList=FunctionList, PermissionList = PermissionList};
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            return js.Serialize(result);
        }
        [System.Web.Services.WebMethod]
        public static void Update(string roleId, int functionId, bool value)
        {
            Role role = RoleList.Where(r => r.Id == (new Guid(roleId))).Single();
            if (value)
            {
                SystemAuthentication.AddRoleForFunction(Function.Parse(functionId), role);
            }
            else
            {
                SystemAuthentication.RemoveRoleForFunction(Function.Parse(functionId), role);
            }
        }
        [System.Web.Services.WebMethod]
        public static void RoleNew()
        {
            Role item = new Role();
            item.Id = Guid.NewGuid();
            PSCDialog.DataShare = new RoleArgs(item, false);
        }
        [System.Web.Services.WebMethod]
        public static string RoleAdd()
        {
            RoleList.AddDB(((RoleArgs)PSCDialog.DataShare).Role);
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            return js.Serialize(((RoleArgs)PSCDialog.DataShare).Role);
        }
        [System.Web.Services.WebMethod]
        public static void RoleEdit(string id)
        {
            Guid idRole = new Guid(id);
            PSCDialog.DataShare = new RoleArgs(RoleList.Where(a => a.Id == idRole).Single(), true);
        }
        [System.Web.Services.WebMethod]
        public static string RoleUpdate()
        {
            (PSCDialog.DataShare as RoleArgs).Role.Update();
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            return js.Serialize(((RoleArgs)PSCDialog.DataShare).Role);
        }
        [System.Web.Services.WebMethod]
        public static void RoleDelete(string id)
        {
            Guid idRole = new Guid(id);
            RoleList.RemoveDB(RoleList.Where(a => a.Id == idRole).Single());
        }
    }
}
