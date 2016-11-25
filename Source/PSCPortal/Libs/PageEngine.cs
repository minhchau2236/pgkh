using System;
using System.Linq;
using System.Web.UI.WebControls;
using PSCPortal.Engine;
using System.Configuration;
using PSCPortal.CMS;


namespace PSCPortal.Libs
{
    public class PageEngine : System.Web.UI.UserControl
    {

       public bool Edit { get; set; }
       public Page PagePortal
        {
            get
            {
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

        protected void Page_Load(object sender, EventArgs eventArgs)
        {
            IPanelArgs args = null;
            string articleId = Page.RouteData.Values["Id"] != null ? Page.RouteData.Values["Id"].ToString() : Request.QueryString["ArticleId"];
            string topicId = Page.RouteData.Values["TopicId1"] != null ? Page.RouteData.Values["TopicId1"].ToString() : Request.QueryString["TopicId"];           
            string moduleId = Page.RouteData.Values["ModuleId"] != null ? Page.RouteData.Values["ModuleId"].ToString() : Request.QueryString["ModuleId"];           
            if (topicId != null)
            {
                int index = topicId.IndexOf("/");
                if (index > 0)
                    topicId = topicId.Substring(0, index);
                var topic = Topic.GetTopic(topicId);
                if (topic.PageId != PagePortal.Id)
                    ChangePage(topic.PageId);
                var topicArgs = new Engine.TopicArgs(topic.PageId);
                if (topic.Rss)
                    Response.Redirect("~/Services/RssHandler.ashx?TopicId=" + topic.Id);
                args = topicArgs;
            }
            else if (articleId != null)
            {
                int index = articleId.IndexOf("/");
                int review = articleId.IndexOf("Preview");
                if (index > 0)
                    articleId = articleId.Substring(0, index);
                Article article = review > -1 ? Article.GetArticleUnPublish(articleId) : Article.GetArticle(articleId);
                if (article.PageId != PagePortal.Id)
                    ChangePage(article.PageId);
                var articleArgs = new Engine.ArticleArgs();
                Layout layout = LayoutCollection.GetPageLayOut(article.PageId).SingleOrDefault();
                if (layout != null)
                {
                    if (layout.Id != Guid.Empty)
                        articleArgs.Path = layout.NavigationUrl ; // ConfigurationManager.AppSettings["ArticleDisplay"] + layout.Name + ".ascx";
                }
                args = articleArgs;
            }
            else if (moduleId != null)
            {
                int index = moduleId.IndexOf("/");
                if (index > 0)
                    moduleId = moduleId.Substring(0, index);
                var moduleArgs = new ModuleDipslayArgs { moduleId = moduleId };
                var moudule = Module.GetModule(moduleId);
                if (moudule.Id.ToString() == ConfigurationManager.AppSettings["ModuleAlbum"] ||
                    moudule.Id.ToString() == ConfigurationManager.AppSettings["ModuleVideoClip"] || moduleId == ConfigurationManager.AppSettings["ModuleSiteMap"])
                    ChangePage(new Guid(SubDomain.GetPage(Libs.Ultility.GetSubDomain())));
                else if (moudule.PageId != PagePortal.Id)
                    ChangePage(moudule.PageId);
                args = moduleArgs;
            }
            foreach (var control in Controls)
            {
                var phDisplay = control as PlaceHolder;
                if (phDisplay != null)
                {
                              //phDisplay.Controls.Add(PanelInPageList.RenderTable(args, false));
                              phDisplay.Controls.Add(PanelInPageList.RenderDIV(args, Edit));
                              break;
                }
            }
        }

        protected void ChangePage(Guid pageId)
        {
            PagePortal = Engine.Page.GetPage(pageId);
            PanelInPageList = PanelInPageCollection.GetPanelInPageCollection(PagePortal);
            int language = Engine.Page.GetPage(pageId).Language;
            //language = 1 => vi-vn, language = 2 => en-us
            Page.UICulture = language == 1 ? "vi-vn" : "en-us";
        }
    }
}