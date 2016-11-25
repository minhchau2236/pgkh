using System;
using System.Configuration;
using System.Web;
using System.Web.UI;
using PSCPortal.Engine;
using PSCPortal.Libs;
using Page = PSCPortal.Engine.Page;
using PSCPortal.CMS;

namespace PSCPortal
{
    public partial class Default : System.Web.UI.Page
    {
        public string PageTitle
        {
            get
            {
                //return PagePortal.Title;
                return ViewState["PageTitle"] as string;
            }
            set
            {
                ViewState["PageTitle"] = value;
            }
        }
        protected override PageStatePersister PageStatePersister
        {
            get
            {
                return new SessionPageStatePersister(this);
            }
        }
        public string ArticleTitle
        {
            get
            {
                if (ViewState["ArticleTitle"] == null)
                {
                    string articleId = Request.QueryString["ArticleId"] ?? "";
                    string articleIdRoute = Page.RouteData.Values["Id"] != null ? Page.RouteData.Values["Id"].ToString() : "";
                    if (articleId != "" || articleIdRoute != "")
                    {
                        if (articleIdRoute.IndexOf("/") > 0)
                        {
                            var o = Page.RouteData.Values["Id"];
                            if (o != null)
                                articleIdRoute = o.ToString().Substring(0, articleIdRoute.IndexOf("/"));
                        }
                        Article article = Article.GetArticle(articleId != "" ? articleId : articleIdRoute);
                        ArticleTitle = article.Title.Replace("\"", "").Replace("'", "");
                    }
                }
                return (string)ViewState["ArticleTitle"];
            }
            set
            {
                ViewState["ArticleTitle"] = value;
            }
        }


        public string TopicTitle
        {
            get
            {
                if (ViewState["TopicTitle"] == null)
                {
                    string topicId = Request.QueryString["TopicId"] ?? "";
                    string topicIdRoute = Page.RouteData.Values["TopicId1"] != null ? Page.RouteData.Values["TopicId1"].ToString() : "";
                    string topicIdRoute2 = Page.RouteData.Values["TopicId2"] != null ? Page.RouteData.Values["TopicId2"].ToString() : "";
                    string articleId = Request.QueryString["ArticleId"] ?? "";
                    string articleIdRoute = Page.RouteData.Values["Id"] != null ? Page.RouteData.Values["Id"].ToString() : "";
                    if (topicId != "" || topicIdRoute != "" || topicIdRoute2 !="")
                    {
                        if(topicIdRoute2!=""){
                            topicId = topicIdRoute2;
                        }
                        if (topicIdRoute.IndexOf("/") > 0)
                        {
                            var o = Page.RouteData.Values["TopicId1"];
                            if (o != null)
                                topicIdRoute = o.ToString().Substring(0, topicIdRoute.IndexOf("/"));
                        }
                        Topic topic = TopicCollection.GetTopic(topicId != "" ? topicId : topicIdRoute);
                        TopicTitle = topic.Name;
                    }
                    else if (articleId != "" || articleIdRoute != "")
                    {
                        if (topicId == "" || topicIdRoute == "")
                        {
                            if (articleIdRoute.IndexOf("/") > 0)
                            {
                                var o = Page.RouteData.Values["Id"];
                                if (o != null)
                                    articleIdRoute = o.ToString().Substring(0, articleIdRoute.IndexOf("/"));
                            }

                            Topic primaryTopic = Topic.GetTopicPrimary(articleId != "" ? articleId : articleIdRoute);


                            //var topic = TopicCollection.GetTopicCollectionByArticleId(articleId != "" ? articleId : articleIdRoute);
                            TopicTitle = primaryTopic.Name;
                        }
                    }
                }
                return (string)ViewState["TopicTitle"];
            }
            set
            {
                ViewState["TopicTitle"] = value;
            }
        }

        public string ModuleTitle
        {
            get
            {
                if (ViewState["ModuleTitle"] == null)
                {
                    string moduleId = Request.QueryString["ModuleId"] ?? "";
                    string moduleIdRoute = Page.RouteData.Values["ModuleId"] != null ? Page.RouteData.Values["ModuleId"].ToString() : "";
                    if (moduleId != "" || moduleIdRoute != "")
                    {
                        if (moduleIdRoute.IndexOf("/") > 0)
                        {
                            var o = Page.RouteData.Values["ModuleId"];
                            if (o != null)
                                moduleIdRoute = o.ToString().Substring(0, moduleIdRoute.IndexOf("/"));
                        }
                        Module module = Module.GetModule(moduleId != "" ? moduleId : moduleIdRoute);
                        ModuleTitle = module.Name;
                    }
                }
                return (string)ViewState["TopicTitle"];
            }
            set
            {
                ViewState["TopicTitle"] = value;
            }
        }

        protected Page PagePortal
        {
            get
            {
                if (ViewState["PagePortal"] == null)
                {
                    Guid pageid = HttpContext.Current.Request.QueryString["PageId"] == null ? Guid.Empty : new Guid(HttpContext.Current.Request.QueryString["PageId"]);
                    ViewState["PagePortal"] = Engine.Page.GetPage(pageid);
                }
                return ViewState["PagePortal"] as Page;
            }
            set
            {
                ViewState["PagePortal"] = value;
            }
        }
        protected PanelInPageCollection PanelInPageList
        {
            get
            {
                if (ViewState["PanelInPageList"] == null && PagePortal != null)
                    ViewState["PanelInPageList"] = PanelInPageCollection.GetPanelInPageCollection(PagePortal);
                return ViewState["PanelInPageList"] as PanelInPageCollection;
            }
            set
            {
                ViewState["PanelInPageList"] = value;
            }
        }
        protected override void InitializeCulture()
        {
            //string language = String.Empty;
            //if (Session["UICulture"] != null)
            //    language = (string)Session["UICulture"];
            //UICulture = language;
        }

        protected string portalPageId { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {


                  if (!IsPostBack)
                ChangeSubDomain();
            portalPageId = portalPageId == null ? Request["PageId"] : portalPageId;
            if (string.IsNullOrEmpty(portalPageId))
            {
                portalPageId = SubDomain.GetPage(Ultility.GetSubDomain());
                if (portalPageId == null)
                    HttpContext.Current.Response.Redirect(string.Format("http://{0}",
                        ConfigurationManager.AppSettings["DomainName"]));
            }
            if (portalPageId != null) ChangePage(new Guid(portalPageId));
            DataBind();
        }
        protected string GetQueryRemove(string queryString)
        {
            string queryRemove = String.Empty;
            if (queryString.ToLower().Contains("pageid"))
            {
                string[] vars = queryString.ToLower().Split(new[] { '&' });
                foreach (var item in vars)
                {
                    string[] pair = item.Split(new[] { '=' });
                    if (pair[0].ToLower().Contains("pageid"))
                        queryRemove = "PageId=" + pair[1];
                }
            }
            return queryRemove;
        }

        protected void ChangeSubDomain()
        {
            string pageId = Page.RouteData.Values["PageId"] != null ? Page.RouteData.Values["PageId"].ToString() : Request.QueryString["PageId"];
            if (!String.IsNullOrEmpty(pageId))
            {
                CheckSubDomain(pageId);
            }
            string articleId = Page.RouteData.Values["Id"] != null ? Page.RouteData.Values["Id"].ToString() : Request.QueryString["ArticleId"];
            string topicId = Page.RouteData.Values["TopicId1"] != null ? Page.RouteData.Values["TopicId1"].ToString() : Request.QueryString["TopicId"];
            if (!String.IsNullOrEmpty(articleId))
            {
                int index = articleId.IndexOf("/");
                int review = articleId.IndexOf("Preview");
                if (index > 0)
                    articleId = articleId.Substring(0, index);
                pageId = review > -1 ? Ultility.GetPageIdByArticleIdUnPublish(articleId).ToString(): Ultility.GetPageIdByArticleId(articleId).ToString();
                portalPageId = pageId;
                CheckSubDomain(pageId);
            }

            if (!String.IsNullOrEmpty(topicId))
            {
                int index = topicId.IndexOf("/");
                if (index > 0)
                    topicId = topicId.Substring(0, index);
                //topicId => pageId
                pageId = Ultility.GetPageIdByTopicId(topicId).ToString();
                portalPageId = pageId;
                CheckSubDomain(pageId);
            }
            string moduleId = Page.RouteData.Values["ModuleId"] != null ? Page.RouteData.Values["ModuleId"].ToString() : Request.QueryString["ModuleId"];
            if (!String.IsNullOrEmpty(moduleId))
            {
                //moduleId => pageId
                int index = moduleId.IndexOf("/");
                if (index > 0)
                    moduleId = moduleId.Substring(0, index);
                if (moduleId == ConfigurationManager.AppSettings["ModuleAlbum"] || moduleId == ConfigurationManager.AppSettings["ModuleVideoClip"] || moduleId == ConfigurationManager.AppSettings["ModuleSiteMap"])
                    pageId = SubDomain.GetPage(Ultility.GetSubDomain());
                else
                    pageId = Ultility.GetPageIdByModuleleId(moduleId).ToString();
                portalPageId = pageId;
                CheckSubDomain(pageId);
            }
        }

        public void CheckSubDomain(string pageId)
        {
            string subDomain = Ultility.GetSubDomain();
            string subCurrent = SubDomainInPage.GetSub(new Guid(pageId)) ?? "HomePage";
            if (subCurrent == subDomain || (subCurrent == "HomePage" && subDomain == string.Empty))
            {
                return;
            }
            string rawUrl = HttpContext.Current.Request.RawUrl;
            int index = rawUrl.IndexOf('?');
            int index1 = rawUrl.IndexOf('=');
            if (index > 0)
                rawUrl = "/" + rawUrl.Substring(index + 1, index1 - (index + 1)) + "/" + rawUrl.Substring(index1 + 1, rawUrl.Length - (index1 + 1));
            HttpContext.Current.Response.Redirect(string.Format("http://{0}{1}{2}",
                subCurrent == "HomePage" ? "www." : subCurrent + ".",
                ConfigurationManager.AppSettings["DomainName"], rawUrl));

        }

        protected void ChangePage(Guid pageId)
        {
            // Get Page By Id
            Page page = Engine.Page.GetPage(pageId);

            // Get template from Page 

            PageTemplateCollection pageTemplateCollection = new PageTemplateCollection();
            PageTemplate pageTemplate =  pageTemplateCollection[(Template)page.Template];
            if(pageTemplate==null)
            {
                Response.Write(@"<script language='javascript'> alert('Trang này chưa có Template trong hệ thống - bạn vui lòng hiệu chỉnh lại trang hoặc xóa trang đi');</script>");
            }
            else
            {

                UICulture = page.Language == 1 ? "vi-vn" : "en-us";
                var pageEngine = (PageEngine)LoadControl(pageTemplate.FileASCXPath);
                pageEngine.PagePortal = page;
                //Không cho phép hiệu chỉnh cấu trúc
                pageEngine.Edit = false;
                PageTitle = page.Title;
                phDisplay.Controls.Add(pageEngine);

            }
            
            //string url;
            //switch (page.Template)
            //{
            //    case 1:
            //        url = "~/PageTemplate/NoTemplate.ascx";
            //        break;
            //    case 2:
            //        url = "~/PageTemplate/HomePage.ascx";
            //        break;
            //    case 3:
            //        url = "~/PageTemplate/Mobile.ascx";
            //        break;
            //    case 4:
            //        url = "~/PageTemplate/SinhVien.ascx";
            //        break;
            //    case 5:
            //        url = "~/PageTemplate/TanSinhVien.ascx";
            //        break;
            //    case 6:
            //        url = "~/PageTemplate/DoiTac.ascx";
            //        break;
            //    case 7:
            //        url = "~/PageTemplate/CuuSinhVien.ascx";
            //        break;
            //    case 8:
            //        url = "~/PageTemplate/CanBo.ascx";
            //        break;
            //    case 9:
            //        url = "~/PageTemplate/Khoa.ascx";
            //        break;
            //    case 10:
            //        url = "~/PageTemplate/TrungTam.ascx";
            //        break;
            //    case 11:
            //        url = "~/PageTemplate/PhongBan.ascx";
            //        break;
            //    default:
            //        url = "~/PageTemplate/HomePage.ascx";
            //        break;
            //}

        }
    }
}
