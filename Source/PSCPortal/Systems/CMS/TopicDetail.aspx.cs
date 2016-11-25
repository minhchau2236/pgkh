using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using PSCPortal.CMS;
using PSCPortal.Engine;
using PSCPortal.Framework.Helpler;

namespace PSCPortal.Systems.CMS
{
    public partial class TopicDetail : PSCPortal.Framework.PSCDialog
    {
        private static PSCPortal.CMS.TopicArgs Args
        {
            get
            {
                return DataShare as PSCPortal.CMS.TopicArgs;
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
                        SubDomain subDomain = new SubDomain { Id = subId };
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
            txtId.Text = Args.Topic.Id.ToString();
            txtName.Text = Args.Topic.Name;
            txtDescription.Text = Args.Topic.Description;
            rcbPage.DataSource = PageList;
            rcbPage.DataBind();
            cbxRss.Checked = Args.Topic.Rss;
            if (Args.IsEdit)
            {
                PSCPortal.Engine.Page page = PageList.SingleOrDefault(p => p.Id == Args.Topic.PageId);
                if (page != null)
                    rcbPage.Items.FindItemByValue(Args.Topic.PageId.ToString()).Selected = true;
            }
            
        }
        [System.Web.Services.WebMethod]
        public static void Save(string name, string description, Guid pageId,bool rss)
        {
            Args.Topic.Name = name;
            Args.Topic.Description = description;
            Args.Topic.PageId = pageId;
            Args.Topic.Rss = rss;
        }
    }
}
