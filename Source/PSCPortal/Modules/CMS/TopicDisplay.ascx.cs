using System;
using System.Collections.Generic;
using System.Linq;
using PSCPortal.CMS;
using System.Web.UI.WebControls;

namespace PSCPortal.Modules.CMS
{
    public partial class TopicDisplay : System.Web.UI.UserControl
    {
        public string TopicName
        {
            get
            {
                if (ViewState["TopicName"] == null)
                {
                    string topicId = Request.QueryString["TopicId"];
                    string topicId2 = Page.RouteData.Values["TopicId2"] != null ? Page.RouteData.Values["TopicId2"].ToString() : Request.QueryString["TopicId2"];
                    if (topicId2 != null)
                    {
                        topicId = topicId2;
                    }
                    Topic topic = TopicCollection.GetTopic(topicId);
                    TopicName = topic.Name;
                }
                return (string)ViewState["TopicName"];
            }
            set
            {
                ViewState["TopicName"] = value;
            }
        }
        public string TopicId
        {
            get
            {
                if (ViewState["TopicId"] == null)
                    ViewState["TopicId"] = string.Empty;
                return (string)ViewState["TopicId"];
            }
            set
            {
                ViewState["TopicId"] = value;
            }
        }



        public IEnumerable<Article> ListArticle
        {
            get
            {
                if (ViewState["ListArticle"] == null)
                {
                    string topicId = Page.RouteData.Values["TopicId1"] != null ? Page.RouteData.Values["TopicId1"].ToString() : Request.QueryString["TopicId"];
                    string topicId2 = Page.RouteData.Values["TopicId2"] != null ? Page.RouteData.Values["TopicId2"].ToString() : Request.QueryString["TopicId2"];
                    if (topicId2 != null)
                    {
                        topicId = topicId2;
                    }
                    int index = topicId.IndexOf("/");
                    if (index > 0)
                        topicId = topicId.Substring(0, index);
                    Topic topic = TopicCollection.GetTopic(topicId);
                    TopicName = topic.Name;
                    ArticleCollection arts = ArticleCollection.GetArticleCollectionPublish(topic);
                    ViewState["ListArticle"] = arts;
                }
                return ViewState["ListArticle"] as IEnumerable<Article>;
            }
            set
            {
                ViewState["ListArticle"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                CurrentPage = 0;
                LoadData();
            }
            DataBind();
            RenderPager();
            Session.Remove("CurrentPage");

        }
        protected void LoadData()
        {
            Topic topic = new Topic();
            string topicId = Page.RouteData.Values["TopicId1"] != null ? Page.RouteData.Values["TopicId1"].ToString() : Request.QueryString["TopicId"];
            string topicId2 = Page.RouteData.Values["TopicId2"] != null ? Page.RouteData.Values["TopicId2"].ToString() : Request.QueryString["TopicId2"];        
            if (topicId2 != null)
            {
                topicId = topicId2;
            }
            if (topicId == null)
                return;
            int index = topicId.IndexOf("/");
            if (index > 0)
                topicId = topicId.Substring(0, index);
            topic = TopicCollection.GetTopic(topicId);
            TopicName = topic.Name;
            TopicId = topic.Id.ToString();
            if (ListArticle.Count() <= 10)
                pnPager.Visible = false;
            IEnumerable<Article> ilist = ListArticle.Skip(CurrentPage * RPP);
            ilist = ilist.Take(CurrentPage * RPP + RPP > TotalRecord ? TotalRecord % RPP : RPP);
            RadListView1.DataSource = ilist;
            RadListView1.DataBind();
            UserLogin.TopicId = topicId;

        }

        /************** Phan Trang**************/
        protected void Page_PreRender(object sender, EventArgs e)
        {
            LinkButton lbtFirst = (LinkButton)pnPager.FindControl("lbtFirst");
            LinkButton lbtPrev = (LinkButton)pnPager.FindControl("lbtPrev");
            if (CurrentPage == 0)
            {
                lbtFirst.Enabled = false;
                lbtPrev.Enabled = false;
                lbtFirst.CssClass = "Link_Selected";
                lbtPrev.CssClass = "Link_Selected";
            }
            else
            {
                lbtFirst.Enabled = true;
                lbtPrev.Enabled = true;
                lbtFirst.CssClass = "Link_Unselected";
                lbtPrev.CssClass = "Link_Unselected";
            }

            LinkButton lbtNext = (LinkButton)pnPager.FindControl("lbtNext");
            LinkButton lbtLast = (LinkButton)pnPager.FindControl("lbtLast");
            if (CurrentPage == TotalPage - 1)
            {
                lbtLast.Enabled = false;
                lbtNext.Enabled = false;
                lbtLast.CssClass = "Link_Selected";
                lbtNext.CssClass = "Link_Selected";
            }
            else
            {
                lbtLast.Enabled = true;
                lbtNext.Enabled = true;
                lbtLast.CssClass = "Link_Unselected";
                lbtNext.CssClass = "Link_Unselected";
            }

            LinkButton lbtNumber = (LinkButton)pnPager.FindControl("lbt" + CurrentPage);
            lbtNumber.Enabled = true;
            lbtNumber.CssClass = "Link_Selected";
            pnPager.Visible = TotalPage > 1;
        }
        private void RenderPager()
        {
            pnPager.Controls.Clear();
            LinkButton lbtFirst = new LinkButton();
            lbtFirst.ID = "lbtFirst";
            lbtFirst.Text = "";
            lbtFirst.CommandArgument = 0 + "";
            lbtFirst.CssClass = "Link_Unselected";
            lbtFirst.Click += new EventHandler(Page_Changing);
            pnPager.Controls.Add(lbtFirst);

            LinkButton lbtPrev = new LinkButton();
            lbtPrev.Text = "«";
            lbtPrev.CommandArgument = CurrentPage - 1 + "";
            lbtPrev.ID = "lbtPrev";
            lbtPrev.Click += new EventHandler(Page_Changing);
            lbtPrev.CssClass = "Link_Unselected";
            pnPager.Controls.Add(lbtPrev);
            Label lb_seperator = new Label();
            lb_seperator.Text = " | ";
            lb_seperator.ForeColor = System.Drawing.Color.White;
            pnPager.Controls.Add(lb_seperator);
            /*----phan trang 5 record--- */
            int index_Begin = CurrentPage - 2;
            int index_End = CurrentPage + 3;
            if (CurrentPage == 0 || CurrentPage == 1 || CurrentPage == 2)
            {
                index_Begin = 0;
                index_End = 5;
            }
            for (int i = index_Begin; i < index_End && i < TotalPage; i++)
            {
                LinkButton lbtNumber = new LinkButton();
                lbtNumber.CssClass = "Link_Unselected";
                lbtNumber.CommandArgument = i + "";
                lbtNumber.Text = (i + 1).ToString();
                lbtNumber.ID = "lbt" + i;
                lbtNumber.Click += new EventHandler(Page_Changing);
                pnPager.Controls.Add(lbtNumber);
                lbtNumber.Enabled = true;
                lb_seperator = new Label();
                lb_seperator.Text = " | ";
                lb_seperator.ForeColor = System.Drawing.Color.White;
                pnPager.Controls.Add(lb_seperator);
            }
            /*-----------*/
            LinkButton lbtNext = new LinkButton();
            lbtNext.CssClass = "Link_Unselected";
            lbtNext.Text = " » ";
            lbtNext.ID = "lbtNext";
            lbtNext.CommandArgument = CurrentPage + 1 + "";
            lbtNext.Click += new EventHandler(Page_Changing);
            pnPager.Controls.Add(lbtNext);
            LinkButton lbtLast = new LinkButton();
            lbtLast.Text = "";
            lbtLast.ID = "lbtLast";
            lbtLast.CssClass = "Link_Unselected";
            lbtLast.CommandArgument = TotalPage - 1 + "";
            lbtLast.Click += new EventHandler(Page_Changing);
            pnPager.Controls.Add(lbtLast);
        }
        protected void Page_Changing(object sender, EventArgs e)
        {
            CurrentPage = int.Parse(((LinkButton)sender).CommandArgument);
            LoadData();
            RenderPager();
        }

        private int TotalRecord
        {
            get
            {
                int total = ListArticle == null ? 0 : ListArticle.Count();
                return total;
            }
        }
        private int RPP
        {
            get
            {
                return int.Parse(System.Configuration.ConfigurationManager.AppSettings["ProductDisplayDefault"]);
            }
        }
        private int TotalPage
        {
            get
            {
                return (int)((TotalRecord - 1) / RPP) + 1;
            }
        }
        private int CurrentPage
        {
            get
            {
                if (Session["CurrentPage"] == null)
                    Session["CurrentPage"] = 0;
                return (int)Session["CurrentPage"];
            }
            set
            {
                Session["CurrentPage"] = value;
            }
        }
    }
}