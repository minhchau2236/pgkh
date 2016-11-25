using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PSCPortal.Framework;
using PSCPortal.CMS;

namespace PSCPortal.Systems.CMS
{
    public partial class TopicEditContentTemplate : PSCDialog
    {
        private static TopicArgs Args
        {
            get
            {
                return DataShare as TopicArgs;
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
            RadEditor1.Content = Args.Topic.GetContentTemplate();
        }
        [System.Web.Services.WebMethod]
        public static void Save(string content)
        {
            Args.Topic.SetContentTemplate(content);
        }       
    }
}
