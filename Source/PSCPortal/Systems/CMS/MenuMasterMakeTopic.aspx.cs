using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PSCPortal.CMS;

namespace PSCPortal.Systems.CMS
{
    public partial class MenuMasterMakeTopic : PSCPortal.Framework.PSCDialog
    {
        private static MenuMasterArgs Args
        {
            get
            {
                return DataShare as MenuMasterArgs;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DataBind();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            LoadData();
        }
        protected void LoadData()
        {
            
            rcbPage.DataSource = PSCPortal.Engine.PageCollection.GetPageCollection();
            rcbPage.DataBind();
            //rcbPage.Items.FindItemByValue(Args.Topic.PageId.ToString()).Selected = true;

        }
        [System.Web.Services.WebMethod]
        public static void Save(Guid pageId)
        {
            Args.PageId=pageId;
        }
    }
}
