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

namespace PSCPortal.Systems.CMS
{
    public partial class MenuManage : PSCPortal.Framework.PSCPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (MenuMaster == null)
                Response.Redirect("~/Systems/CMS/MenuMasterMange.aspx");
            if (!IsPostBack)
            {
                LoadData();
                DataBind();
            }
        }

        protected void LoadData()
        {            
            rtvMenu.DataSource = MenuList.GetBindingSource();
            rtvMenu.DataBind();
        }
        private static MenuMaster MenuMaster
        {
            get
            {
                return DataShare as MenuMaster;
            }
        }
        protected static MenuCollection MenuList
        {
            get
            {
                if (DataStatic["MenuList"] == null)
                    DataStatic["MenuList"] = MenuCollection.GetMenuCollection(MenuMaster);
                return DataStatic["MenuList"] as MenuCollection;
            }
        }
        [System.Web.Services.WebMethod]
        public static void MenuNew()
        {
            PSCPortal.CMS.Menu item = new PSCPortal.CMS.Menu();
            item.Id = Guid.NewGuid();     
            PSCDialog.DataShare = new MenuArgs(item, false);
        }
        [System.Web.Services.WebMethod]
        public static string MenuAdd()
        {
            MenuList.AddDB(((MenuArgs)PSCDialog.DataShare).Menu);
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            return js.Serialize(((MenuArgs)PSCDialog.DataShare).Menu);
        }
        [System.Web.Services.WebMethod]
        public static void MenuEdit(string id)
        {
            Guid idMenu = new Guid(id);
            PSCDialog.DataShare = new MenuArgs((PSCPortal.CMS.Menu)MenuList.Search(o => ((PSCPortal.CMS.Menu)o).Id == idMenu), true);
        }
        [System.Web.Services.WebMethod]
        public static string MenuUpdate()
        {
            (PSCDialog.DataShare as PSCPortal.CMS.MenuArgs).Menu.Update();
            DataStatic["MenuList"] = null;
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            return js.Serialize(((MenuArgs)PSCDialog.DataShare).Menu);
        }
        [System.Web.Services.WebMethod]
        public static void MenuDelete(string id)
        {
            Guid idMenu = new Guid(id);
            MenuList.RemoveDB((PSCPortal.CMS.Menu)MenuList.Search(o => ((PSCPortal.CMS.Menu)o).Id == idMenu));
        }
        [System.Web.Services.WebMethod]
        public static void ChangeParent(string idChild, string idParent)
        {
            PSCPortal.CMS.Menu child = (PSCPortal.CMS.Menu)MenuList.Search(t => ((PSCPortal.CMS.Menu)t).Id == new Guid(idChild));
            PSCPortal.CMS.Menu parent = (PSCPortal.CMS.Menu)MenuList.Search(t => ((PSCPortal.CMS.Menu)t).Id == new Guid(idParent));
            child.Parent = parent;
            child.Update();
            MenuList.UpdatePostionChilds(parent);
        }
        [System.Web.Services.WebMethod]
        public static void MenuMoveUp(string id)
        {
            Guid menuId = new Guid(id);
            MenuList.MoveUp((PSCPortal.CMS.Menu)MenuList.Search(m => ((PSCPortal.CMS.Menu)m).Id == menuId));
        }
        [System.Web.Services.WebMethod]
        public static void MenuMoveDown(string id)
        {
            Guid menuId = new Guid(id);
            MenuList.MoveDown((PSCPortal.CMS.Menu)MenuList.Search(m => ((PSCPortal.CMS.Menu)m).Id == menuId));
        }
    }
}
