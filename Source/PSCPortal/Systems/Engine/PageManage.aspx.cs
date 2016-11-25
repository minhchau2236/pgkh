using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PSCPortal.Framework;
using PSCPortal.Engine;
using Page = PSCPortal.Engine.Page;
using PSCPortal.Framework.Helpler;
using PSCPortal.CMS;

namespace PSCPortal.Systems.Engine
{
    public partial class PageManage : PSCPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //PageAuthentication temp = PageAuthentication.GetPageAuthentication(new PSCPortal.Engine.Page() { Id=Guid.Empty });

            //bool kq = temp.IsAllow(PagePermission.PERMISSION.Page_EditStruct);
            if (!IsPostBack) DataBind();
        }
        [System.Web.Services.WebMethod]
        public static string GetPagePermission(int[] arr)
        {
            Dictionary<string, bool> result = new Dictionary<string, bool>();
            foreach (int idFun in arr)
            {
                result.Add(idFun.ToString(), PSCPortal.Security.SystemAuthentication.CheckAllowFunction(PSCPortal.Security.Function.Parse(idFun)));
            }
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            return js.Serialize(result);
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
                        SubDomain subDomain = new SubDomain { Id = subId };
                        DataStatic["PageList"] = subDomain.GetPagesBelongTo();
                    }
                }
                return DataStatic["PageList"] as PageCollection;
            }
        }
        [System.Web.Services.WebMethod]
        public static void PageSecurity(string id)
        {
            Guid idPage = new Guid(id);
            PSCDialog.DataShare = new PageArgs((PSCPortal.Engine.Page)PageList.Where(s => s.Id == idPage).Single(), true);
        }
        [System.Web.Services.WebMethod]
        public static string GetPageList(int startIndex, int maximumRows, string sortExpressions)
        {
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            return js.Serialize(PageList.GetSegment(startIndex, maximumRows, sortExpressions));
        }
        [System.Web.Services.WebMethod]
        public static int GetPageCount()
        {
            return PageList.Count;
        }
        [System.Web.Services.WebMethod]
        public static void PageNew()
        {
            PSCPortal.Engine.Page item = new PSCPortal.Engine.Page();
            item.Id = Guid.NewGuid();
            PSCDialog.DataShare = new PageArgs(item, false);
        }
        [System.Web.Services.WebMethod]
        public static void PageAdd()
        {
            PageArgs pageArgs = (PageArgs)PSCDialog.DataShare;
            Page page = pageArgs.Page;
            PageList.AddDB(page);
            page.UpdatePageLayout(page.LayoutId);
            // check subdomain
            Guid subId = SessionHelper.GetSession(SessionKey.SubDomain) == string.Empty ? Guid.Empty : new Guid(SessionHelper.GetSession(SessionKey.SubDomain));
            if (!(subId == Guid.Empty))
            {
                SubDomainInPage sip = new SubDomainInPage();
                sip.PageId = page.Id;
                sip.SubDomainId = subId;
                sip.AddDB();
            }
        }
        [System.Web.Services.WebMethod]
        public static void PageEdit(string id)
        {
            Guid idPage = new Guid(id);
            PSCDialog.DataShare = new PageArgs(PageList.Where(a => a.Id == idPage).Single(), true);
        }
        [System.Web.Services.WebMethod]
        public static void PageUpdate()
        {
            PageArgs pageArgs = (PageArgs)PSCDialog.DataShare;
            Page page = pageArgs.Page;
            page.Update();
            page.UpdatePageLayout(pageArgs.Page.LayoutId);

            DataStatic["PageList"] = null;
        }
        [System.Web.Services.WebMethod]
        public static void PageCopy(Guid id)
        {
            PSCPortal.Engine.Page page = PageList.Where(p => p.Id == id).Single();
            PSCPortal.Engine.Page item = new PSCPortal.Engine.Page();
            item.Id = Guid.NewGuid();
            item.Template = page.Template;
            item.Language = page.Language;
            item.LayoutId = LayoutCollection.GetPageLayOut(page.Id).Single().Id;
            PSCDialog.DataShare = new PageArgs(item,false);
        }
        [System.Web.Services.WebMethod]
        public static void PageCopyDo(Guid id)
        {
            PSCPortal.Engine.Page page = PageList.Where(p => p.Id == id).Single();
            PageList.PageCopy(page, ((PageArgs)PSCDialog.DataShare));
            //// check subdomain
            Guid subId = SessionHelper.GetSession(SessionKey.SubDomain) == string.Empty ? Guid.Empty : new Guid(SessionHelper.GetSession(SessionKey.SubDomain));
            if (!(subId == Guid.Empty))
            {
                SubDomainInPage sip = new SubDomainInPage();
                sip.PageId = ((PageArgs)PSCDialog.DataShare).Page.Id;
                sip.SubDomainId = subId;
                sip.AddDB();
            }
        }
        [System.Web.Services.WebMethod]
        public static void PageDelete(string[] arrId)
        {
            foreach (string id in arrId)
            {
                Guid idPage = new Guid(id);
                PageList.RemoveDB(PageList.Where(a => a.Id == idPage).Single());
            }
        }
    }
}
