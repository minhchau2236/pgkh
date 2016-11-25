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

namespace PSCPortal.Systems.Security
{
    public partial class SystemPermissionManage : PSCPortal.Framework.PSCPage
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
                    DataStatic["RoleList"] = RoleCollection.GetRoleCollection();
                return DataStatic["RoleList"] as RoleCollection;
            }
        }
        protected static List<List<bool>> AuthenticationList
        {
            get
            {
                return DataStatic["AuthenticationList"] as List<List<bool>>;
            }
            set
            {
                DataStatic["AuthenticationList"] = value;
            }
        }
        protected static FunctionCollection FunctionList
        {
            get
            {
                return DataStatic["FunctionList"] as FunctionCollection;
            }
            set
            {
                DataStatic["FunctionList"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!System.Web.HttpContext.Current.User.IsInRole(System.Configuration.ConfigurationManager.AppSettings["GroupAdmin"]))
            {
                Response.Redirect("~/Systems/Default.aspx");
            }
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            if (!IsPostBack)
                LoadData();
        }
        protected void LoadData()
        {
            rcbFunctionCategory.DataSource = FunctionCategoryList;
            rcbFunctionCategory.DataBind();
        }
        /**********************************/
        #region Security
        [System.Web.Services.WebMethod]
        public static string GetFunctions(int fcatId)
        {
            FunctionList = FunctionCategoryList.Where(fc => fc.Id == fcatId).Single().FunctionList;
            int i = 0;
            AuthenticationList = new List<List<bool>>();
            foreach (Role r in RoleList)
            {
                if (r.Name == System.Configuration.ConfigurationManager.AppSettings["GroupAdmin"])
                    continue;
                AuthenticationList.Add(new List<bool>());
                foreach (Function f in FunctionList)
                {
                    AuthenticationList[i].Add(SystemAuthentication.FunctionAuthenticationList[Function.Parse(f.Id)].Contains(r));
                }
                i++;
            }
            IEnumerable<Role> iRoleList = RoleList.Where(r => r.Name != System.Configuration.ConfigurationManager.AppSettings["GroupAdmin"]);
            var result = new { FunctionList = FunctionList, RoleList = iRoleList, AuthenticationList = AuthenticationList };
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
                        Function function = FunctionList[j];
                        if (subItem)
                        {
                            SystemAuthentication.AddRoleForFunction(Function.Parse(function.Id), role);
                        }
                        else
                        {
                            SystemAuthentication.RemoveRoleForFunction(Function.Parse(function.Id), role);
                        }
                    }
                    j++;
                }
                i++;
            }
        }
        #endregion
        /**********************************/
    }
}
