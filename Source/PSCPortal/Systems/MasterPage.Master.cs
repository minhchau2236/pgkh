using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PSCPortal.Engine;
using PSCPortal.Framework.Helpler;
using PSCPortal.Security;
using Telerik.Web.UI;

namespace PSCPortal.Systems
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        public string Skin
        {
            get
            {
                if (Session["Skin"] == null)
                    Session["Skin"] = "Web20";
                return (string)Session["Skin"];
            }
            set
            {
                Session["Skin"] = value;
            }
        }
        public string Username
        {
            get
            {
                return HttpContext.Current.User.Identity.Name;
            }
        }
        public int dem
        {
            get
            {
                if (ViewState["dem"] == null)
                {
                    ViewState["dem"] = 0;
                }
                return (int)ViewState["dem"];
            }
            set
            {
                ViewState["dem"] = value;
            }
        }
        public PSCPortal.CMS.ArticleCommentCollection listCommentUnPublic
        {
            get
            {
                if (ViewState["listCommentUnPublic"] == null)
                {
                    ViewState["listCommentUnPublic"] = PSCPortal.CMS.ArticleCommentCollection.GetArticleCommentUnPublicCollection();
                }
                return ViewState["listCommentUnPublic"] as PSCPortal.CMS.ArticleCommentCollection;
            }
            set
            {
                ViewState["listCommentUnPublic"] = value;
            }
        }

        protected RoleCollection RoleList
        {
            get
            {
                if (ViewState["RoleList"] == null)
                    ViewState["RoleList"] = RoleCollection.GetRoleCollection(HttpContext.Current.User.Identity.Name);
                return ViewState["RoleList"] as RoleCollection;
            }
            set
            {
                ViewState["RoleList"] = value;
            }
        }

        protected SubDomainCollection SubDomain
        {

            get
            {
                if (ViewState["SubDomain"] == null)
                    ViewState["SubDomain"] = SubDomainCollection.GetSubDomainByUser(new User { Name = HttpContext.Current.User.Identity.Name });
                return ViewState["SubDomain"] as SubDomainCollection;
            }
            set
            {
                ViewState["SubDomain"] = value;
            }
        }

        protected string GroupAdmin
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["GroupAdmin"];
            }
        }

        protected List<SubDomain> SubDomainList
        {
            get
            {

                if (ViewState["SubDomainList"] == null)
                {
                    List<SubDomain> subDomains = new List<SubDomain>();
                    if (HttpContext.Current.User.IsInRole(GroupAdmin) && SubDomain.Count == 0)
                        subDomains = SubDomainCollection.GetSubDomainCollection().ToList();
                    else
                        subDomains = SubDomain.Where(a => a.Id != Guid.Empty).ToList();

                    ViewState["SubDomainList"] = subDomains;
                }
                return ViewState["SubDomainList"] as List<SubDomain>;
            }
        }

        protected void ShowMenu()
        {
            Guid subId = SessionHelper.GetSession(SessionKey.SubDomain) == string.Empty ? Guid.Empty : new Guid(SessionHelper.GetSession(SessionKey.SubDomain));
            if (HttpContext.Current.User.IsInRole(GroupAdmin))
            {
                if (!(subId == Guid.Empty))
                {
                    if (subId == new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"))
                        RadPanelBar1.FindItemByValue("bantin").Visible = true;
                    RadPanelBar1.FindItemByValue("administrator").Visible = false;
                }
                else
                    RadPanelBar1.FindItemByValue("bantin").Visible = true;
            }
            else
            {
                RadPanelBar1.FindItemByValue("admin").Visible = false;
                RadPanelBar1.FindItemByValue("administrator").Visible = false;
                RadPanelBar1.FindItemByValue("thongke").Visible = false;
            }
        }

        protected void LoadComment()
        {
            Guid subId = SessionHelper.GetSession(SessionKey.SubDomain) == string.Empty ? Guid.Empty : new Guid(SessionHelper.GetSession(SessionKey.SubDomain));
            if (HttpContext.Current.User.IsInRole(GroupAdmin))
            {
                if (subId == Guid.Empty)
                {
                    RadListView1.DataSource = listCommentUnPublic;
                    dem = listCommentUnPublic.Count;
                }
                else
                {
                    SubDomain subDomain = new SubDomain { Id = subId };
                    PageCollection pageList = subDomain.GetPagesBelongTo();
                    PSCPortal.CMS.ArticleCommentCollection commentListDisplay = new PSCPortal.CMS.ArticleCommentCollection();
                    foreach (Page page in pageList)
                    {
                        PSCPortal.CMS.ArticleCommentCollection commentList = PSCPortal.CMS.ArticleCommentCollection.GetListArticleCommentByPageId(page.Id.ToString());
                        foreach (PSCPortal.CMS.ArticleComment comment in commentList)
                        {
                            commentListDisplay.Add(comment);
                        }
                    }
                    dem = commentListDisplay.Count;
                    RadListView1.DataSource = commentListDisplay;
                }
            }
            else
            {
                SubDomain subDomain = new SubDomain { Id = subId };
                PageCollection pageList = subDomain.GetPagesBelongTo();
                PSCPortal.CMS.ArticleCommentCollection commentListDisplay = new PSCPortal.CMS.ArticleCommentCollection();
                foreach (Page page in pageList)
                {
                    PSCPortal.CMS.ArticleCommentCollection commentList = PSCPortal.CMS.ArticleCommentCollection.GetListArticleCommentByPageId(page.Id.ToString());
                    foreach (PSCPortal.CMS.ArticleComment comment in commentList)
                    {
                        commentListDisplay.Add(comment);
                    }
                }
                dem = commentListDisplay.Count;
                RadListView1.DataSource = commentListDisplay;
            }
            RadListView1.DataBind();
        }

        protected void LoadSubDomain()
        {
            rcbSubDomain.DataSource = SubDomainList;
            rcbSubDomain.DataTextField = "NameAndDescription";// "Name";
            rcbSubDomain.DataValueField = "Id";
            rcbSubDomain.DataBind();
            if (HttpContext.Current.User.IsInRole(GroupAdmin) && SubDomain.Count == 0)
                rcbSubDomain.Items.Insert(0, new RadComboBoxItem("All", Guid.Empty.ToString()));
            RadComboBoxItem rcbItem = rcbSubDomain.Items.FindItemByValue(SessionHelper.GetSession(SessionKey.SubDomain));
            rcbItem = rcbItem ?? rcbSubDomain.Items[0];
            rcbItem.Selected = true;
            SessionHelper.SetSession(SessionKey.SubDomain, rcbItem.Value);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
                System.Web.HttpContext.Current.Response.Redirect("~/Login.aspx");
            if (Session["Count"] != null)
                Session["Count"] = null;
            if (!IsPostBack)
            {
                rcbSkin.FindItemByValue(Skin).Selected = true;
                RadSkinManager1.Skin = rcbSkin.SelectedValue;
                LoadSubDomain();
                ShowMenu();
                LoadComment();
            }
        }

        protected void lbtChangeSkin_Click(object sender, EventArgs e)
        {
            Skin = rcbSkin.SelectedValue;
            RadSkinManager1.Skin = rcbSkin.SelectedValue;
            Response.Redirect("~/Systems");
        }

        protected void lbtLogout_Click(object sender, EventArgs e)
        {
            System.Web.Security.FormsAuthentication.SignOut();
            //System.Web.Security.FormsAuthentication.RedirectToLoginPage();
            Response.Redirect(string.Format("http://{0}/login.aspx", Request.Url.Host));
        }
        protected void lbtSubDomainFilter_Click(object sender, EventArgs e)
        {
            SessionHelper.SetSession(SessionKey.SubDomain, rcbSubDomain.SelectedValue);
            Response.Redirect(Request.Url.ToString());
        }
    }
}
