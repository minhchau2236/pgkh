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
    public partial class PortletManage : PSCPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) DataBind();
        }
        protected static PortletCollection PortletList
        {
            get
            {
                if (DataStatic["PortletList"] == null)
                    DataStatic["PortletList"] = PortletCollection.GetPortletCollection();
                return DataStatic["PortletList"] as PortletCollection;
            }
        }
        [System.Web.Services.WebMethod]
        public static string GetPortletList(int startIndex, int maximumRows, string sortExpressions)
        {
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            return js.Serialize(PortletList.GetSegment(startIndex, maximumRows, sortExpressions));
        }
        [System.Web.Services.WebMethod]
        public static int GetPortletCount()
        {
            return PortletList.Count;
        }
        [System.Web.Services.WebMethod]
        public static void PortletNew()
        {
            Portlet item = new Portlet();
            item.Id = Guid.NewGuid();
            PSCDialog.DataShare = new PortletArgs(item, false);
        }
        [System.Web.Services.WebMethod]
        public static void PortletAdd()
        {
            PortletList.AddDB(((PortletArgs)PSCDialog.DataShare).Portlet);
        }
        [System.Web.Services.WebMethod]
        public static void PortletEdit(string id)
        {
            Guid idPortlet = new Guid(id);
            PSCDialog.DataShare = new PortletArgs(PortletList.Where(a => a.Id == idPortlet).Single(), true);
        }
        [System.Web.Services.WebMethod]
        public static void PortletUpdate()
        {
            (PSCDialog.DataShare as PSCPortal.Engine.PortletArgs).Portlet.Update();
            DataStatic["PortletList"] = null;
        }
        [System.Web.Services.WebMethod]
        public static void PortletDelete(string[] arrId)
        {
            foreach (string id in arrId)
            {
                Guid idPortlet = new Guid(id);
                PortletList.RemoveDB(PortletList.Where(a => a.Id == idPortlet).Single());
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
