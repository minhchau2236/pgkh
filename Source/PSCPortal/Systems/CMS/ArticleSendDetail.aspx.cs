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
using PSCPortal.Framework;
using PSCPortal.Engine;
using System.Collections.Generic;
using PSCPortal.Framework.Helpler;

namespace PSCPortal.Systems.CMS
{
    public partial class ArticleSendDetail : PSCPortal.Framework.PSCDialog
    {
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
            rcbTopic.DataSource = TopicList;
            rcbTopic.DataTextField = "Path";
            rcbTopic.DataValueField = "Id";
            rcbTopic.DataBind();

            rcbPage.DataSource = PageList;
            rcbPage.DataTextField = "Name";
            rcbPage.DataValueField = "Id";
            rcbPage.DataBind();

        }
        [System.Web.Services.WebMethod]
        public static void Save(string idTopic, string idPage)
        {
            PSCPage.DataShare = new ArticleSendArgs(new Guid(idTopic), new Guid(idPage));
        }
    }
}
