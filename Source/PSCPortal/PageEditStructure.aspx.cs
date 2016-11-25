using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PSCPortal.Framework;
using PSCPortal.Engine;
using PSCPortal.Framework.Helpler;
using PSCPortal.Libs;

namespace PSCPortal
{
      public partial class PageEditStructure : PSCPage
      {
            /// <summary>
            /// Load pageTemplate
            /// 26/11/2015
            /// </summary>
            //==============================================================================//
            // Luan
            private Guid portalPageId { get; set; }
           
            // End Luan
            //===============================================================================//
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
            protected override PageStatePersister PageStatePersister
            {
                  get
                  {
                        return new SessionPageStatePersister(this);
                  }
            }
            public string PageTitle
            {
                  get
                  {
                        return PagePortal.Title;
                  }
            }
            public string PageId
            {
                  get
                  {
                        return PagePortal.Id.ToString();
                  }
            }
            protected static PSCPortal.Engine.Page PagePortal
            {
                  get
                  {
                        if (DataStatic["PagePortal"] == null)
                              DataStatic["PagePortal"] = PSCPortal.Engine.Page.GetPage(new Guid(System.Web.HttpContext.Current.Request.QueryString["PageId"]));
                        return DataStatic["PagePortal"] as PSCPortal.Engine.Page;
                  }
            }
            protected static PanelInPageCollection PanelInPageList
            {
                  get
                  {
                        if (DataStatic["PanelInPageList"] == null && PagePortal != null)
                              DataStatic["PanelInPageList"] = PanelInPageCollection.GetPanelInPageCollection(PagePortal);
                        return DataStatic["PanelInPageList"] as PanelInPageCollection;
                  }
            }
            protected static PanelCollection PanelList
            {
                  get
                  {
                        if (DataStatic["PanelList"] == null)
                              DataStatic["PanelList"] = PanelCollection.GetPanelCollection();
                        return DataStatic["PanelList"] as PanelCollection;
                  }
            }
            protected static List<PSCPortal.Engine.Panel> PanelListExists
            {
                  get
                  {
                        List<PSCPortal.Engine.Panel> result = new List<PSCPortal.Engine.Panel>();
                        foreach (PSCPortal.Engine.Panel panel in PanelList)
                        {
                              if (PanelInPageList.Search(PSCPortal.Engine.Panel.Parse(panel.Id)) != null)
                                    result.Add(panel);
                        }
                        return result;
                  }
            }
            protected static List<PSCPortal.Engine.Panel> PanelListNonExists
            {
                  get
                  {
                        List<PSCPortal.Engine.Panel> result = new List<PSCPortal.Engine.Panel>();
                        foreach (PSCPortal.Engine.Panel panel in PanelList)
                        {
                              if (PanelInPageList.Search(PSCPortal.Engine.Panel.Parse(panel.Id)) == null)
                                    result.Add(panel);
                        }
                        return result;
                  }
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
            protected void Page_Load(object sender, EventArgs e)
            {
                  //PageAuthentication temp = PageAuthentication.GetPageAuthentication(PagePortal);
                  //bool kq = temp.IsAllow(PagePermission.PERMISSION.Page_EditStruct);
                  //if (!kq)
                  //    System.Web.HttpContext.Current.Response.Redirect("~/Login.aspx");
                  if (!HttpContext.Current.User.Identity.IsAuthenticated)
                        System.Web.HttpContext.Current.Response.Redirect("~/Login.aspx");
                  if (Request.QueryString["PageId"] == null)
                        Response.Redirect("~/Systems/Engine/PageManage.aspx");
                  else
                        portalPageId = new Guid(Request.QueryString["PageId"]);
                  DataBind();
            }
            protected void Page_PreRender(object sender, EventArgs e)
            {
                  LoadDataPanelFunction();
                  LoadDataPortletFunction();
                  LoadDataPortletReferenceFunction();
                  LoadPagePortal();
            }

            protected void LoadDataPanelFunction()
            {
                  ddlPanel.DataSource = PanelListNonExists;
                  ddlPanel.DataBind();
                  pnPanelFunction.Visible = PanelListNonExists.Count > 0;
            }
            protected void LoadDataPortletFunction()
            {
                  ddlPortlet.DataSource = PortletList.OrderBy(p => p.Name);
                  ddlPortlet.DataBind();
                  ddlPanelAddPortlet.DataSource = PanelListExists;
                  ddlPanelAddPortlet.DataBind();
                  pnPortletFunction.Visible = PortletList.Count > 0;
            }
            protected void LoadDataPortletReferenceFunction()
            {
                  ddlPage.DataSource = PageList;
                  ddlPage.DataBind();

                  ddlPagePanelAdd.DataSource = PanelListExists;
                  ddlPagePanelAdd.DataBind();
            }
            protected void LoadPagePortal()
            {
                  //phDisplay.Controls.Add(PanelInPageList.RenderTable(null, true));
                  //PanelInPageList are set of Panle in a page
                  //phDisplay.Controls.Add(PanelInPageList.RenderDIV(null, true));

                  //=====================================================================//
                  // Get Page By Id
                  Engine.Page page = Engine.Page.GetPage(portalPageId);
                

                  // Get template from Page 
                  PageTemplateCollection pageTemplateCollection = new PageTemplateCollection();
                  PageTemplate pageTemplate = pageTemplateCollection[(Template)page.Template];

                  if (pageTemplate == null)
                  {
                      Response.Write(@"<script language='javascript'> alert('Trang này chưa có Template trong hệ thống - bạn vui lòng hiệu chỉnh lại trang hoặc xóa trang đi');</script>");
                      
                  }
                  else
                  {
                      UICulture = page.Language == 1 ? "vi-vn" : "en-us";
                      var pageEngine = (PageEngine)LoadControl(pageTemplate.FileASCXPath);
                      pageEngine.PagePortal = page;

                      //Cho phép hiệu chỉnh cấu trúc
                      pageEngine.Edit = true;
                      phDisplay.Controls.Add(pageEngine);

                      // phDisplay.Controls.Add(PanelInPageList.RenderDIV(null, true));
                  }

            }

            protected void lbtAddPanel_Click(object sender, EventArgs e)
            {
                  int idPanel = int.Parse(ddlPanel.SelectedValue);
                  PanelInPage pip = new PanelInPage();
                  pip.Panel = PanelList.Search(PSCPortal.Engine.Panel.Parse(idPanel));
                  PanelInPageList.AddDB(pip);
            }
            [System.Web.Services.WebMethod]
            public static void PanelRemove(int idPanel)
            {
                  PanelInPageList.RemoveDB(PanelInPageList.Search(PSCPortal.Engine.Panel.Parse(idPanel)));
            }
            [System.Web.Services.WebMethod]
            public static string PanelStyleGet(int idPanel)
            {
                  string result = string.Empty;
                  PanelInPage pip = PanelInPageList.Search(PSCPortal.Engine.Panel.Parse(idPanel));
                  result = pip.Style;
                  return result;
            }
            [System.Web.Services.WebMethod]
            public static string PanelStyleUpdate(int idPanel, string style)
            {
                  PanelInPage pip = PanelInPageList.Search(PSCPortal.Engine.Panel.Parse(idPanel));
                  pip.Style = style;
                  pip.Update();
                  return pip.Panel.ToString();
            }
            [System.Web.Services.WebMethod]
            public static string PortletInstanceAdd(string idPortlet, string name, int idPanel)
            {
                  Guid id = new Guid(idPortlet);
                  PortletInstance pi = new PortletInstance(PortletList.Where(p => p.Id == id).Single(), name);
                  string path = pi.Portlet.DisplayURL;
                    if (System.IO.File.Exists( HttpContext.Current.Server.MapPath(path)))
                    {
                        PortletInstanceInPanel pip = new PortletInstanceInPanel();
                        pip.PortletInstance = pi;
                        PanelInPageList.Search(PSCPortal.Engine.Panel.Parse(idPanel)).Portlets.AddDB(pip);
                        return "";
                    }
                    else
                    {
                        return string.Format("Đường dẫn '{0}' Portlet '{1}' không tồn tại", pi.Portlet.DisplayURL, pi.Portlet.Name);
                    }
            }
            [System.Web.Services.WebMethod]
            public static void PortletRemove(string idPortlet)
            {
                  PanelInPageList.PortletRemoveDB(new Guid(idPortlet));
            }
            [System.Web.Services.WebMethod]
            public static string PortletEditCSSGet(string idPortlet)
            {
                  string result = string.Empty;
                  Guid id = new Guid(idPortlet);
                  result = PanelInPageList.Search(id).Style;
                  return result;
            }
            [System.Web.Services.WebMethod]
            public static void PortletEditCSSUpdate(string idPortlet, string css)
            {
                  Guid id = new Guid(idPortlet);
                  PortletInstanceInPanel pip = PanelInPageList.Search(id);
                  pip.Style = css;
                  pip.Update();
            }
            [System.Web.Services.WebMethod]
            public static void PortletChangePosition(int panelId1, int index1, int panelId2, int index2)
            {
                  PanelInPage panel1 = PanelInPageList.Where(p => p.Panel.Id == panelId1).Single();
                  PanelInPage panel2 = PanelInPageList.Where(p => p.Panel.Id == panelId2).Single();
                  if (panelId1 == panelId2)
                  {
                        PanelInPageList.PortletMove(panelId1, index1, index2);
                  }
                  else
                  {
                        PanelInPageList.PortletMove(panelId1, index1, panelId2, index2);
                  }
            }
            [System.Web.Services.WebMethod]
            public static string PortletInstanceCollectionGet(string id)
            {
                  string result = string.Empty;
                  Guid pageId = new Guid(id);
                  PortletInstanceCollection listPortlet = PortletInstanceCollection.GetPortletInstanceCollection(PSCPortal.Engine.Page.GetPage(pageId));
                  System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
                  result = js.Serialize(listPortlet);
                  return result;
            }
            [System.Web.Services.WebMethod]
            public static void PortletInstanceReferenceAdd(string portletId, int idPanel)
            {
                  Guid idPortletInstance = new Guid(portletId);
                  PortletInstance pi = PortletInstance.GetPortletInstance(idPortletInstance);
                  PortletInstanceInPanel pip = new PortletInstanceInPanel();
                  pip.PortletInstance = pi;
                  pip.Style = string.Empty;
                  PanelInPageList.Search(PSCPortal.Engine.Panel.Parse(idPanel)).Portlets.AddDB(pip);
            }

      }
}
