using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PSCPortal.CMS;
using PSCPortal.Engine;
using PSCPortal.Framework;
using PSCPortal.Security;

namespace PSCPortal.Systems.Engine
{
    public partial class SubDomainConfig : PSCPortal.Framework.PSCDialog
    {
        private static SubDomainArgs Args
        {
            get
            {
                return DataShare as SubDomainArgs;
            }
        }
        # region MenuMaster
        protected static MenuMasterCollection MenuMasterList
        {
            get
            {
                if (DataStatic["MenuMasterList"] == null)
                    DataStatic["MenuMasterList"] = MenuMasterCollection.GetMenuMasterCollection();
                return DataStatic["MenuMasterList"] as MenuMasterCollection;
            }
            set
            {
                DataStatic["MenuMasterList"] = value;
            }
        }
        protected static MenuMasterCollection BeforeMenuMasterList
        {
            get
            {
                if (DataStatic["BeforeMenuMasterList"] == null)
                {
                    DataStatic["BeforeMenuMasterList"] = Args.SubDomain.GetMenuMastersBelongTo();
                }
                return DataStatic["BeforeMenuMasterList"] as MenuMasterCollection;
            }
            set
            {
                DataStatic["BeforeMenuMasterList"] = value;
            }
        }
        protected static MenuMasterCollection AfterMenuMasterList
        {
            get
            {
                return DataStatic["AfterMenuMasterList"] as MenuMasterCollection;
            }
            set
            {
                DataStatic["AfterMenuMasterList"] = value;
            }
        }
        protected static MenuMasterCollection DisplayMenuMasterList
        {
            get
            {
                MenuMasterCollection result = MenuMasterList;
                if (DataStatic["DisplayMenuMasterList"] == null)
                {
                    foreach (var item in BeforeMenuMasterList)
                        result.Single(i => i.Id == item.Id).IsCheck = true;
                    DataStatic["DisplayMenuMasterList"] = result;
                }
                return DataStatic["DisplayMenuMasterList"] as MenuMasterCollection;
            }
            set
            {
                DataStatic["DisplayMenuMasterList"] = value;
            }
        }
        [System.Web.Services.WebMethod]
        public static string GetMenuMasterList()
        {
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            string results = js.Serialize(DisplayMenuMasterList);
            return results;
        }
        public static MenuMasterCollection MenuMasterListChoice(string[] arrId)
        {
            MenuMasterCollection result = new MenuMasterCollection();
            foreach (string id in arrId)
            {
                Guid idMenuMaster = new Guid(id);
                MenuMaster menuMaster = MenuMasterList.Where(u => u.Id == idMenuMaster).Single();
                result.Add(menuMaster);
            }
            AfterMenuMasterList = result;
            return result;
        }
        #endregion
        # region Page
        protected static PageCollection PageList
        {
            get
            {
                //if (DataStatic["PageList"] == null)
                DataStatic["PageList"] = PageCollection.GetPageCollection();
                return DataStatic["PageList"] as PageCollection;
            }
            set
            {
                DataStatic["PageList"] = value;
            }
        }
        protected static PageCollection BeforePageList
        {
            get
            {
                //if (DataStatic["BeforePageList"] == null)
                //{
                DataStatic["BeforePageList"] = Args.SubDomain.GetPagesBelongTo();
                //}
                return DataStatic["BeforePageList"] as PageCollection;
            }
            set
            {
                DataStatic["BeforePageList"] = value;
            }
        }
        protected static PageCollection AfterPageList
        {
            get
            {
                return DataStatic["AfterPageList"] as PageCollection;
            }
            set
            {
                DataStatic["AfterPageList"] = value;
            }
        }
        protected static PageCollection DisplayPageList
        {
            get
            {
                PageCollection result = PageList;
                //if (DataStatic["DisplayPageList"] == null)
                //{
                foreach (var item in BeforePageList)
                    result.Single(i => i.Id == item.Id).IsCheck = true;
                DataStatic["DisplayPageList"] = result;
                //}
                return DataStatic["DisplayPageList"] as PageCollection;
            }
            set
            {
                DataStatic["DisplayPageList"] = value;
            }
        }
        [System.Web.Services.WebMethod]
        public static string GetPageList()
        {
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            string results = js.Serialize(DisplayPageList);
            return results;
        }
        public static PageCollection PageListChoice(string[] arrId)
        {
            PageCollection result = new PageCollection();
            foreach (string id in arrId)
            {
                Guid idPage = new Guid(id);
                PSCPortal.Engine.Page page = PageList.Where(u => u.Id == idPage).Single();
                result.Add(page);
            }
            AfterPageList = result;
            return result;
        }
        #endregion

        #region Role
        protected static RoleCollection AfterRoleList
        {
            get
            {
                return DataStatic["AfterRoleList"] as RoleCollection;
            }
            set
            {
                DataStatic["AfterRoleList"] = value;
            }
        }
        public static RoleCollection RoleListChoice(string[] arrId)
        {
            RoleCollection result = new RoleCollection();
            foreach (string id in arrId)
            {
                Guid idRole = new Guid(id);
                Role role = RoleCollection.GetRoleCollection().Where(u => u.Id == idRole).Single();
                result.Add(role);
            }
            AfterRoleList = result;
            return result;
        }

        protected static List<Role> DisplayRoleList
        {
            get
            {
                string groupAdmin = System.Configuration.ConfigurationManager.AppSettings["GroupAdmin"];
                RoleCollection result = RoleCollection.GetRoleCollection();

                foreach (var item in Args.SubDomain.GetRolesBelongTo())
                {
                    result.Single(i => i.Id == item.Id).IsCheck = true;
                }
                IEnumerable<Role> list = Args.SubDomain.GetRolesNotBelongTo();
                DataStatic["DisplayRoleList"] = result.Where(r => r.Name != groupAdmin).Except(list).ToList();
                return DataStatic["DisplayRoleList"] as List<Role>;
            }
            set
            {
                DataStatic["DisplayRoleList"] = value;
            }
        }

        [System.Web.Services.WebMethod]
        public static string GetRoleList()
        {
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            string results = js.Serialize(DisplayRoleList);
            return results;
        }
        #endregion

        protected static TopicCollection TopicList
        {
            get
            {
                //if (DataStatic["TopicList"] == null)
                DataStatic["TopicList"] = TopicCollection.GetTopicCollection();
                return DataStatic["TopicList"] as TopicCollection;
            }
        }
        protected void LoadTree()
        {
            Guid topicIdSelect = Args.SubDomain.GetTopic().Id;
            rtvTopic.DataSource = TopicList.GetBindingSource();
            rtvTopic.DataBind();
            Telerik.Web.UI.RadTreeNode nodeSelected = rtvTopic.FindNodeByValue(topicIdSelect.ToString());
            if (nodeSelected != null)
            {
                Telerik.Web.UI.RadTreeNode nodeCurrent = nodeSelected;
                while (nodeCurrent.ParentNode != null)
                {
                    nodeCurrent.ParentNode.Expanded = true;
                    nodeCurrent = nodeCurrent.ParentNode;
                }
                nodeSelected.Selected = true;
            }
        }
        [System.Web.Services.WebMethod]
        public static void Save(string topicId, string[] arrMenuMasterId, string[] arrPageId, string[] arrRoleId)
        {
            #region Topic
            Guid topicSelected = new Guid(topicId);
            if (topicSelected != Guid.Empty)
            {
                SubDomainInTopic sit = new SubDomainInTopic();
                sit.SubDomainId = Args.SubDomain.Id;
                sit.TopicId = new Guid(topicId);
                if (Args.SubDomain.GetTopic().Id == Guid.Empty)
                {
                    sit.AddDB();
                }
                else
                {
                    sit.Update();
                }
            }
            #endregion
            #region MenuMaster
            MenuMasterCollection listMenuMasterBefore = BeforeMenuMasterList;
            MenuMasterCollection listMenuMasterAfter = MenuMasterListChoice(arrMenuMasterId);
            IEnumerable<MenuMaster> addMenuMasterMaster = listMenuMasterAfter.Except(listMenuMasterBefore);
            IEnumerable<MenuMaster> removeMenuMasterMaster = listMenuMasterBefore.Except(listMenuMasterAfter);
            // add list
            foreach (var item in addMenuMasterMaster)
            {
                SubDomainInMenuMaster sdim = new SubDomainInMenuMaster();
                sdim.MenuMasterId = item.Id;
                sdim.SubDomainId = Args.SubDomain.Id;
                sdim.AddDB();
            }
            //remove list
            foreach (var item in removeMenuMasterMaster)
            {
                SubDomainInMenuMaster sdim = new SubDomainInMenuMaster();
                sdim.MenuMasterId = item.Id;
                sdim.SubDomainId = Args.SubDomain.Id;
                sdim.RemoveDB();
            }
            #endregion
            #region Page
            PageCollection listPageBefore = BeforePageList;
            PageCollection listPageAfter = PageListChoice(arrPageId);
            IEnumerable<PSCPortal.Engine.Page> addPageMaster = listPageAfter.Except(listPageBefore);
            IEnumerable<PSCPortal.Engine.Page> removePageMaster = listPageBefore.Except(listPageAfter);
            // add list
            foreach (var item in addPageMaster)
            {
                SubDomainInPage sdim = new SubDomainInPage();
                sdim.PageId = item.Id;
                sdim.SubDomainId = Args.SubDomain.Id;
                sdim.AddDB();
            }
            //remove list
            foreach (var item in removePageMaster)
            {
                SubDomainInPage sdim = new SubDomainInPage();
                sdim.PageId = item.Id;
                sdim.SubDomainId = Args.SubDomain.Id;
                sdim.RemoveDB();
            }
            #endregion
            #region Role
            RoleCollection listRoleBefore = Args.SubDomain.GetRolesBelongTo();
            RoleCollection listRoleAfter = RoleListChoice(arrRoleId);
            IEnumerable<Role> addRoleMaster = listRoleAfter.Except(listRoleBefore);
            IEnumerable<Role> removeRoleMaster = listRoleBefore.Except(listRoleAfter);
            // add list
            foreach (var item in addRoleMaster)
            {
                SubDomainInRole sdim = new SubDomainInRole();
                sdim.RoleId = item.Id;
                sdim.SubDomainId = Args.SubDomain.Id;
                sdim.AddDB();
            }
            //remove list
            foreach (var item in removeRoleMaster)
            {
                SubDomainInRole sdim = new SubDomainInRole();
                sdim.RoleId = item.Id;
                sdim.SubDomainId = Args.SubDomain.Id;
                sdim.RemoveDB();
            }
            #endregion
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DataBind();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            if (!IsPostBack)
            {
                LoadTree();
                gvMenuMaster.MasterTableView.PageSize = DisplayMenuMasterList.Count();
                gvPage.MasterTableView.PageSize = DisplayPageList.Count();
            }
        }
    }
}