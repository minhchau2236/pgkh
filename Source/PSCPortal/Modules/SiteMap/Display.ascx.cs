using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PSCPortal.CMS;
using System.Web.Script.Serialization;

namespace PSCPortal.Modules.SiteMap
{
    public partial class Display : System.Web.UI.UserControl
    {
        protected static MenuMasterCollection MenuMasterList
        {
            get
            {
                return MenuMasterCollection.GetMenuMasterCollection(); ;
            }
        }

        public struct SiteMapNode
        {
            public Guid Id;
            public string Name;
            public string NavigationUrl;
            public List<SiteMapNode> Children;
        }
        public string MenuMasterName { get; set; }
        public string TreeNodesLinkList { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

            LoadData();
            DataBind();
        }


        protected SiteMapNode GetSiteNodeFromMenu(PSCPortal.CMS.Menu menu)
        {
            SiteMapNode siteNode = new SiteMapNode();
            siteNode.Id = menu.Id;
            siteNode.Name = menu.Name;
            siteNode.NavigationUrl = menu.NavigationURL;
            siteNode.Children = new List<SiteMapNode>();
            if (menu.HasChildren)
            {
                var children = menu.GetChildren();
                foreach (PSCPortal.Framework.Core.BusinessObjectHierarchical mc in children)
                {
                    siteNode.Children.Add(GetSiteNodeFromMenu((PSCPortal.CMS.Menu)mc.Item));
                }
            }
            return siteNode;
        }

        protected void LoadData()
        {
            try
            {
                string rootId = Request.QueryString["menuId"].ToString();
                MenuMaster menuMaster = MenuMasterList.Where(s => s.Id.ToString() == rootId).Single();
                MenuMasterName = menuMaster.Name;
                MenuCollection collection = MenuCollection.GetMenuCollection(menuMaster);
                List<SiteMapNode> result = new List<SiteMapNode>();
                PSCPortal.Framework.Core.BusinessObjectTreeBindingSource bindingResource = collection.GetBindingSource();
                foreach (PSCPortal.Framework.Core.BusinessObjectHierarchical mc in bindingResource)
                {
                    result.Add(GetSiteNodeFromMenu((PSCPortal.CMS.Menu)mc.Item));
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                TreeNodesLinkList = js.Serialize(result);
                DataBind();
            }
            catch (Exception e)
            {
            }
        }
    }
}