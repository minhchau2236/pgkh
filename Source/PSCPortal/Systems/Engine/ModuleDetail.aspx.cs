using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PSCPortal.Framework;
using PSCPortal.Engine;
using PSCPortal.Framework.Helpler;

namespace PSCPortal.Systems.Engine
{
    public partial class ModuleDetail : PSCDialog
    {
        private static ModuleArgs Args
        {
            get
            {
                return DataShare as ModuleArgs;
            }
        }
        protected static PageCollection PageList
        {
            get
            {
                if (DataStatic["PageList"] == null)
                {
                    Guid subId = SessionHelper.GetSession(SessionKey.SubDomain) == string.Empty ? Guid.Empty : new Guid(SessionHelper.GetSession(SessionKey.SubDomain));
                    if (subId == Guid.Empty)
                        DataStatic["PageList"] = PageCollection.GetPageCollection();
                    else
                    {
                        PSCPortal.Engine.SubDomain subDomain = new PSCPortal.Engine.SubDomain { Id = subId };
                        DataStatic["PageList"] = subDomain.GetPagesBelongTo();
                    }
                }

                return DataStatic["PageList"] as PageCollection;
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
            txtId.Text = Args.Module.Id.ToString();
            txtName.Text = Args.Module.Name;
            txtDisplayURL.Text = Args.Module.DisplayURL;
            txtEditURL.Text = Args.Module.EditURL;
            rcbPage.DataSource = PageList;
            rcbPage.DataBind();
            PSCPortal.Engine.Page page = PageList.SingleOrDefault(p => p.Id == Args.Module.PageId);
            if (page != null)
                rcbPage.Items.FindItemByValue(Args.Module.PageId.ToString()).Selected = true;
        }
        [System.Web.Services.WebMethod]
        public static bool Save(string name, string displayURL, string editURL, string pageId)
        {
            bool result = false;
           
            //Ngọc - 17122015: kiểm tra tồn tại "đường dẫn file hiển thị" 
            if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(displayURL)))
            {
                Args.Module.Name = name;
                Args.Module.DisplayURL = displayURL;
                Args.Module.EditURL = editURL;
                Args.Module.PageId = new Guid(pageId);
                result = true; 
            }
           
            return result;
        }
    }
}
