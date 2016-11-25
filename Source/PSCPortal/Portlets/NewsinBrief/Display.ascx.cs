using System;
using System.Web;
using System.Web.UI;
using System.Text;
using System.IO;
using System.Data.SqlClient;

namespace PSCPortal.Portlets.NewsinBrief
{
    public partial class Display : Engine.PortletControl
    {
        public string TopicName
        {
            get
            {
                if (ViewState["TopicName"] == null)
                    ViewState["TopicName"] = string.Empty;
                return (string)ViewState["TopicName"];
            }
            set
            {
                ViewState["TopicName"] = value;
            }
        }
        protected string News
        {
            get
            {
                if (ViewState["News"] == null)
                {
                    StringBuilder marqueecontent = new StringBuilder();
                    using (StringWriter sw = new StringWriter(marqueecontent))
                    {
                        using (HtmlTextWriter textWriter = new HtmlTextWriter(sw))
                        {
                            RPDisplay.RenderControl(textWriter);
                        }
                    }
                    RPDisplay.Visible = false;
                    string content = marqueecontent.ToString().Replace("\r\n", string.Empty).Replace("\n",string.Empty);
                    ViewState["News"] = content;
                }
                return (string)ViewState["News"];
            }
        }

        protected void LoadNewsinBrief()
        {
            using (
                SqlConnection con =
                    new SqlConnection(
                        System.Configuration.ConfigurationManager.ConnectionStrings["PSCPortalConnectionString"]
                            .ConnectionString))
            {
                SqlCommand com = new SqlCommand {Connection = con};
                con.Open();
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.AddWithValue("@dataId", Portlet.PortletInstance.Id);
                com.CommandText = "select TopicId From PortletNewsInBrief Where DataId=@dataId";

                Object topicGuid = com.ExecuteScalar();
                if (topicGuid != null)
                {
                    CMS.Topic topic = CMS.Topic.GetTopic(topicGuid.ToString());
                    if (topic != null)
                    {
                        TopicName = topic.Name;
                        CMS.ArticleCollection artCol = CMS.ArticleCollection.GetArticleCollectionPublish(topic);
                        RPDisplay.DataSource = artCol;
                        RPDisplay.DataBind();
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            LoadNewsinBrief();
            DataBind();

            string content = News;
            string key = Guid.NewGuid().ToString();
            if (!Page.ClientScript.IsStartupScriptRegistered("ScrollText"))
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "ScrollText", "<script src='/Portlets/NewsinBrief/Scripts/ScrollTextObject.js' type='text/javascript' language='javascript'></script>");

            Page.ClientScript.RegisterStartupScript(Page.GetType(), key, "<script type='text/javascript'>" + " var scrObj = new Utility.ScrollText('" + content + "',\"" + "__" + ClientID + "\"); " + "scrObj.LoadMarquee(\"" + "_" + ClientID + "\");" + "</script>");
        }
        protected void Page_Prerender(object sender, EventArgs e)
        {            
        }

        protected override void DeleteData()
        {
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSCPortalConnectionString"].ConnectionString))
            {
                SqlCommand com = new SqlCommand {Connection = con};
                con.Open();
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.AddWithValue("@dataId", Portlet.PortletInstance.Id);
                com.CommandText = "Delete PortletNewsInBrief Where DataId=@dataId";
            }
        }
    }
}