using PSCPortal.CMS;
using PSCPortal.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PSCPortal.Systems.CMS
{
    public partial class LayoutManage : PSCPortal.Framework.PSCPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected static LayoutCollection LayoutList
        {
            get
            {
                return DataStatic["LayoutList"] as LayoutCollection;
            }
            set
            {
                DataStatic["LayoutList"] = value;
            }
        }

        [System.Web.Services.WebMethod]
        public static void LayoutNew()
        {
            Layout item = new Layout();
            item.Id = Guid.NewGuid();
            PSCDialog.DataShare = new LayoutArgs(item, false);
        }
        [System.Web.Services.WebMethod]
        public static void LayoutAdd()
        {
            LayoutArgs item = (LayoutArgs)PSCDialog.DataShare;
            if (LayoutList.Where(a => a.Name.Equals(item.Layout.Name)).Count() > 0)
                return;
            LayoutList.AddDB(item.Layout);

        }
        [System.Web.Services.WebMethod]
        public static string GetLayoutList(int startIndex, int maximumRows, string sortExpressions)
        {
            LayoutList = LayoutCollection.GetAllLayout();
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            return js.Serialize(LayoutList.GetSegment(startIndex, maximumRows, sortExpressions).Where(a=>a.Id!=Guid.Empty).ToList());
        }
        [System.Web.Services.WebMethod]
        public static int GetLayoutCount()
        {
            return LayoutList.Count;
        }
        [System.Web.Services.WebMethod]
        public static void LayoutEdit(string id)
        {
            Guid idArticle = new Guid(id);
            Layout layout = LayoutList.Where(a => a.Id == idArticle).Single();
            PSCDialog.DataShare = new LayoutArgs(layout, true);
        }

        [System.Web.Services.WebMethod]
        public static void LayoutUpdate()
        {
            LayoutArgs item = (LayoutArgs)PSCDialog.DataShare;
            item.Layout.Update();
        }

        [System.Web.Services.WebMethod]
        public static void LayoutDelete(string[] arrId)
        {
            foreach (string id in arrId)
            {
                Guid layoutId = new Guid(id);
                Layout layout = LayoutList.Where(a => a.Id == layoutId).Single();
                LayoutList.RemoveDB(LayoutList.Where(a => a.Id == layoutId).Single());
                File.Delete(HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["ArticleDisplay"] + layout.Name + ".ascx"));
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