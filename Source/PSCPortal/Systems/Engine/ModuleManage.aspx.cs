using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PSCPortal.Framework;
using PSCPortal.Engine;

namespace PSCPortal.Systems.Engine
{
    public partial class ModuleManage : PSCPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) DataBind();
        }
        protected static ModuleCollection ModuleList
        {
            get
            {
                if (DataStatic["ModuleList"] == null)
                    DataStatic["ModuleList"] = ModuleCollection.GetModuleCollection();
                return DataStatic["ModuleList"] as ModuleCollection;
            }
        }
        [System.Web.Services.WebMethod]
        public static string GetModuleList(int startIndex, int maximumRows, string sortExpressions)
        {
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            return js.Serialize(ModuleList.GetSegment(startIndex, maximumRows, sortExpressions));
        }
        [System.Web.Services.WebMethod]
        public static int GetModuleCount()
        {
            return ModuleList.Count;
        }
        [System.Web.Services.WebMethod]
        public static void ModuleNew()
        {
            Module item = new Module();
            item.Id = Guid.NewGuid();
            PSCDialog.DataShare = new ModuleArgs(item, false);
        }
        [System.Web.Services.WebMethod]
        public static void ModuleAdd()
        {
            ModuleList.AddDB(((ModuleArgs)PSCDialog.DataShare).Module);
        }
        [System.Web.Services.WebMethod]
        public static void ModuleEdit(string id)
        {
            Guid idModule = new Guid(id);
            PSCDialog.DataShare = new ModuleArgs(ModuleList.Where(a => a.Id == idModule).Single(), true);
        }
        [System.Web.Services.WebMethod]
        public static void ModuleUpdate()
        {
            (PSCDialog.DataShare as PSCPortal.Engine.ModuleArgs).Module.Update();
            DataStatic["ModuleList"] = null;
        }
        [System.Web.Services.WebMethod]
        public static void ModuleDelete(string[] arrId)
        {
            foreach (string id in arrId)
            {
                Guid idModule = new Guid(id);
                ModuleList.RemoveDB(ModuleList.Where(a => a.Id == idModule).Single());
            }
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
