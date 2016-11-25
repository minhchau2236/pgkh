using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PSCPortal.CMS;

namespace PSCPortal.Portlets.FlvVideo
{
    public partial class VideoToChucHanhChinh : PSCPortal.Engine.PortletControl
    {
        protected PSCPortal.Engine.Page PagePortal
        {
            get
            {
                if (ViewState["PagePortal"] == null)
                {
                    Guid pageid = System.Web.HttpContext.Current.Request.QueryString["PageId"] == null ? new Guid("6bbaf34c-3713-4ad5-b0d7-fcfd285e80a3") : new Guid(System.Web.HttpContext.Current.Request.QueryString["PageId"]);
                    ViewState["PagePortal"] = PSCPortal.Engine.Page.GetPage(pageid);
                }
                return ViewState["PagePortal"] as PSCPortal.Engine.Page;
            }
            set
            {
                ViewState["PagePortal"] = value;
            }
        }
        public string FlvVideo
        {
            get
            {
                string results = string.Empty;
                ClipNewCollection collection = ClipNewCollection.GetClipNewCollectionIsPublish("8262ba7c-0224-493f-ba4e-3f9c82f9484e");
                System.Web.Script.Serialization.JavaScriptSerializer serialize = new System.Web.Script.Serialization.JavaScriptSerializer();
                results = serialize.Serialize(collection);
                return results;
            }
        }
        public string ClipNewId
        {
            get
            {
                if (ViewState["ClipNewId"] == null)
                    ViewState["ClipNewId"] = string.Empty;
                return (string)ViewState["ClipNewId"];
            }
            set
            {
                ViewState["ClipNewId"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DataBind();
            if (!Request.RawUrl.Contains("PageEditStructure.aspx"))
            {
                Page.ClientScript.RegisterClientScriptInclude("mootool", "/Portlets/FlvVideo/Scripts/mootools-111-uncompressed.js");
                Page.ClientScript.RegisterClientScriptInclude("slideshow", "/Portlets/FlvVideo/Scripts/CopyTpniceslideshow.js");
                string key = Guid.NewGuid().ToString();
                Page.ClientScript.RegisterStartupScript(Page.GetType(), key, "<script type='text/javascript'> " + "createPager();" + "</script>");
            }
        }
        private void LoadData()
        {
            //ClipNew clip=FlvVideo.Where(a=>a.I);
            //    ClipNewsList.Where(a => a.Id == idClipNews).Single()
        }
        protected override void DeleteData()
        {
            throw new NotImplementedException();
        }
    }
}