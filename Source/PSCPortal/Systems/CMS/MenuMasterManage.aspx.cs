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
using PSCPortal.CMS;
using PSCPortal.Framework;
using System.Collections.Generic;
using PSCPortal.Engine;
using PSCPortal.Framework.Helpler;

namespace PSCPortal.Systems.CMS
{
    public partial class MenuMasterManage : PSCPortal.Framework.PSCPage
    {
        protected static MenuMasterCollection MenuMasterList
        {
            get
            {
                if (DataStatic["MenuMasterList"] == null)
                {
                    Guid subId = SessionHelper.GetSession(SessionKey.SubDomain) == string.Empty ? Guid.Empty : new Guid(SessionHelper.GetSession(SessionKey.SubDomain));
                    if (subId == Guid.Empty)
                        DataStatic["MenuMasterList"] = MenuMasterCollection.GetMenuMasterCollection();
                    else
                    {
                        SubDomain subDomain = new SubDomain { Id = subId };
                        DataStatic["MenuMasterList"] = subDomain.GetMenuMastersBelongTo();
                    }
                }
                return DataStatic["MenuMasterList"] as MenuMasterCollection;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                DataBind();
        }
        [System.Web.Services.WebMethod]
        public static string GetMenuMasterPermission(int[] arr)
        {
            Dictionary<string, bool> result = new Dictionary<string, bool>();
            foreach (int idFun in arr)
            {
                result.Add(idFun.ToString(), PSCPortal.Security.SystemAuthentication.CheckAllowFunction(PSCPortal.Security.Function.Parse(idFun)));
            }
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            return js.Serialize(result);

        }
        [System.Web.Services.WebMethod]
        public static void MenuMasterSecurity(string MenuMasterId)
        {
            MenuMaster menuMaster = MenuMasterList.Where(s => s.Id.ToString() == MenuMasterId).Single();
            PSCDialog.DataShare = new MenuMasterArgs(menuMaster, true);
        }
        [System.Web.Services.WebMethod]
        public static string GetMenuMasterList(int startIndex, int maximumRows, string sortExpressions)
        {
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            return js.Serialize(MenuMasterList.GetSegment(startIndex, maximumRows, sortExpressions));
        }
        [System.Web.Services.WebMethod]
        public static int GetMenuMasterCount()
        {
            return MenuMasterList.Count;
        }
        [System.Web.Services.WebMethod]
        public static void MenuMasterNew()
        {
            MenuMaster item = new MenuMaster();
            item.Id = Guid.NewGuid();
            PSCDialog.DataShare = new MenuMasterArgs(item, false);
        }
        [System.Web.Services.WebMethod]
        public static void MenuMasterAdd()
        {
            MenuMaster menuMaster = ((MenuMasterArgs)PSCDialog.DataShare).MenuMaster;
            MenuMasterList.AddDB(menuMaster);
            // check subdomain
            Guid subId = SessionHelper.GetSession(SessionKey.SubDomain) == string.Empty ? Guid.Empty : new Guid(SessionHelper.GetSession(SessionKey.SubDomain));
            if (!(subId==Guid.Empty))
            {
                SubDomainInMenuMaster sim = new SubDomainInMenuMaster();
                sim.MenuMasterId = menuMaster.Id;
                sim.SubDomainId = subId;
                sim.AddDB();
            }
        }
        [System.Web.Services.WebMethod]
        public static void MenuMasterEdit(string id)
        {
            Guid idMenuMaster = new Guid(id);
            PSCDialog.DataShare = new MenuMasterArgs(MenuMasterList.Where(a => a.Id == idMenuMaster).Single(), true);
        }
        [System.Web.Services.WebMethod]
        public static void MenuMasterUpdate()
        {
            (PSCDialog.DataShare as PSCPortal.CMS.MenuMasterArgs).MenuMaster.Update();
            DataStatic["MenuMasterList"] = null;
        }
        [System.Web.Services.WebMethod]
        public static void MenuMasterDelete(string[] arrId)
        {
            foreach (string id in arrId)
            {
                Guid idMenuMaster = new Guid(id);
                MenuMasterList.RemoveDB(MenuMasterList.Where(a => a.Id == idMenuMaster).Single());
            }
        }
        [System.Web.Services.WebMethod]
        public static void GetMenuMaster_Menu(string id)
        {
            Guid idMenuMaster = new Guid(id);
            PSCPage.DataShare = MenuMasterList.Where(a => a.Id == idMenuMaster).Single();
        }
        [System.Web.Services.WebMethod]
        public static void MenuMasterCopyDo(Guid id)
        {
            MenuMaster menuMaster = MenuMasterList.Where(m => m.Id == id).Single();
            MenuMasterList.MenuMasterCopy(menuMaster, ((MenuMasterArgs)PSCDialog.DataShare).MenuMaster);
        }
        [System.Web.Services.WebMethod]
        public static void MenuMasterCopy()
        {
            MenuMaster item = new MenuMaster();
            item.Id = Guid.NewGuid();
            PSCDialog.DataShare = new MenuMasterArgs(item, false);
        }
        [System.Web.Services.WebMethod]
        public static void MenuMakeTopic(Guid id)
        {
            MenuMaster menuMaster = MenuMasterList.Where(m => m.Id == id).Single();
            PSCDialog.DataShare = new MenuMasterArgs(menuMaster, false);
        }
        [System.Web.Services.WebMethod]
        public static void MenuMakeTopicDo(Guid id)
        {
            MenuMaster menuMaster = MenuMasterList.Where(m => m.Id == id).Single();
            MenuMasterList.MenuMakeTopic(menuMaster, ((MenuMasterArgs)PSCDialog.DataShare).PageId);
        }
    }
}
