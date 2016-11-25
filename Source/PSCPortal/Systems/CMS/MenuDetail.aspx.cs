using System;
using System.Web;
using PSCPortal.CMS;
using PSCPortal.Engine;
using PSCPortal.Framework.Helpler;

namespace PSCPortal.Systems.CMS
{
    public partial class MenuDetail : Framework.PSCDialog
    {
        private static MenuArgs Args
        {
            get
            {
                return DataShare as MenuArgs;
            }
        }
        protected static TopicCollection TopicList
        {
            get
            {
                if (DataStatic["TopicList"] == null)
                {
                    DataStatic["TopicList"] = TopicCollection.GetTopicCollection();
                }
                return DataStatic["TopicList"] as TopicCollection;
            }
        }
        private static ModuleCollection ModuleList
        {
            get
            {
                if (DataStatic["ModuleList"] == null)
                    DataStatic["ModuleList"] = ModuleCollection.GetModuleCollection();
                return DataStatic["ModuleList"] as ModuleCollection;
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
            txtId.Text = Args.Menu.Id.ToString();
            txtName.Text = Args.Menu.Name;
            txtDescription.Text = Args.Menu.Description;           
            rcbTopic.DataSource = TopicList;
            rcbTopic.DataBind();
            rcbPage.DataSource = PageList;
            rcbPage.DataBind();
            rcbModule.DataSource = ModuleList;
            rcbModule.DataBind();
            string link = Args.Menu.NavigationURL;
            if(link.IndexOf("~/Default.aspx?ArticleId=")!=-1)
            {
                rdoArticle.Checked=true;
                txtArticle.Text = link.Replace("~/Default.aspx?ArticleId=","");
            }
            else if(link.IndexOf("~/Default.aspx?TopicId=")!=-1)
            {
                rdoTopic.Checked=true;
                rcbTopic.Items.FindItemByValue(link.Replace("~/Default.aspx?TopicId=", "")).Selected = true;                
            }
            else if (link.IndexOf("~/Default.aspx?PageId=") != -1)
            {
                rdoPage.Checked = true;
                rcbPage.Items.FindItemByValue(link.Replace("~/Default.aspx?PageId=", "")).Selected = true;                
            }
            else if (link.IndexOf("~/Default.aspx?ModuleId=") != -1)
            {
                rdoModule.Checked = true;
                rcbModule.Items.FindItemByValue(link.Replace("~/Default.aspx?ModuleId=", "")).Selected = true;
            }
            else if (link.IndexOf("~/Default.aspx?DocumentTypeId=") != -1)
            {
                rdoDocumentType.Checked = true;
                rcbDocumentType.Items.FindItemByValue(link.Replace("~/Default.aspx?DocumentTypeId=", "")).Selected = true;
            }
            else if (link.IndexOf("~/Default.aspx?DocumentId=") != -1)
            {
                rdoDocument.Checked = true;
                txtDocument.Text = link.Replace("~/Default.aspx?DocumentId=", "");
            }
            else
            {
                rdoURL.Checked = true;
                txtURL.Text = link == string.Empty ? "#" : link;
            }
        }
        [System.Web.Services.WebMethod]
        public static void Save(string name, string description, string navigationURL)
        {
            Args.Menu.Name = name;
            Args.Menu.Description = description;
            Args.Menu.NavigationURL = navigationURL;           
        }
    }
}
