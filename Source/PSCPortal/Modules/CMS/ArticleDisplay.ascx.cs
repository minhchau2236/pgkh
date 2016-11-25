using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using System.Web;
using System.Net;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using PSCPortal.CMS;
using PSCPortal.Framework;
namespace PSCPortal.Modules.CMS
{
    public partial class ArticleDisplay : System.Web.UI.UserControl
    {
        public PSCPortal.CMS.Topic TopicComment
        {
            get { return ViewState["TopicComment"] as PSCPortal.CMS.Topic; }
            set { ViewState["TopicComment"] = value; }
        }
        public string StringCapcha
        {
            get
            {
                return (string)ViewState["StringCapcha"];
            }
            set
            {
                ViewState["StringCapcha"] = value;
            }
        }
        public string ArticleDescription
        {
            get
            {
                if (ArticleId == string.Empty)
                    ViewState["ArticleDescription"] = "";
                else
                    ViewState["ArticleDescription"] = PSCPortal.CMS.Article.GetDescription(new Guid(ArticleId));
                return (string)ViewState["ArticleDescription"];
            }
        }
        //public string ArticleContent
        //{
        //    get
        //    {
        //        if (ArticleId == string.Empty)
        //            ViewState["ArticleContent"] = "";
        //        else
        //            ViewState["ArticleContent"] = PSCPortal.CMS.Article.GetContent(new Guid(ArticleId));
        //        return (string)ViewState["ArticleContent"];
        //    }
        //}

        public string ArticleContent
        {
            get
            {
                string html = string.Empty;
                if (ArticleId == string.Empty)
                {
                    ViewState["ArticleContent"] = "";
                }
                else
                {
                    string html2 = string.Empty;
                    ViewState["ArticleContent"] = PSCPortal.CMS.Article.GetContent(new Guid(ArticleId));
                }
                return (string)ViewState["ArticleContent"];
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
        public string ListTopic
        {
            get
            {
                if (ViewState["ListTopic"] == null)
                    ViewState["ListTopic"] = string.Empty;
                return (string)ViewState["ListTopic"];
            }
            set
            {
                ViewState["ListTopic"] = value;
            }
        }
        public string ArticleTitle
        {
            get
            {
                if (ViewState["ArticleTitle"] == null)
                    ViewState["ArticleTitle"] = string.Empty;
                return (string)ViewState["ArticleTitle"];
            }
            set
            {
                ViewState["ArticleTitle"] = value;
            }
        }
        public string ArticleId
        {
            get
            {
                if (ViewState["ArticleId"] == null)
                    ViewState["ArticleId"] = string.Empty;
                return (string)ViewState["ArticleId"];
            }
            set
            {
                ViewState["ArticleId"] = value;
            }
        }
        public DateTime ArticleCreatedDate
        {
            get
            {
                if (ViewState["ArticleCreatedDate"] == null)
                    ViewState["ArticleCreatedDate"] = DateTime.Now;
                return (DateTime)ViewState["ArticleCreatedDate"];
            }
            set
            {
                ViewState["ArticleCreatedDate"] = value;
            }
        }

        public bool? IsVisibleComment
        {
            get
            {
                if (ViewState["IsVisibleComment"] == null)
                    ViewState["IsVisibleComment"] = false;
                return (bool)ViewState["IsVisibleComment"];
            }
            set
            {
                ViewState["IsVisibleComment"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //StringCapcha = RadCaptcha1.CaptchaImage.Text;
            }
            //RadCaptcha1.CaptchaImage.RenderImageOnly = true;
            //RadCaptcha1.CaptchaImage.Height = 50;
            //RadCaptcha1.CaptchaImage.Width = 150;
            LoadData();
            DataBind();
        }
        protected void LoadData()
        {
            string articleid = Page.RouteData.Values["Id"] != null ? Page.RouteData.Values["Id"].ToString() : Request.QueryString["ArticleId"];
            int index1 = articleid.IndexOf("/");
            int review = articleid.IndexOf("Preview");
            if (index1 > 0)
                articleid = articleid.Substring(0, index1);
            PSCPortal.CMS.Article article = review > -1 ? PSCPortal.CMS.Article.GetArticleUnPublish(articleid) : PSCPortal.CMS.Article.GetArticle(articleid);
            if (articleid == null)
                return;
            if (article.Id == Guid.Empty)
                return;
            PSCPortal.CMS.ArticleCollection articleCollection;
            PSCPortal.CMS.Topic topic = PSCPortal.CMS.Topic.GetTopicPrimary(article.Id.ToString());
            if (topic != null)
            {
                string chuoi = "";
                TopicId = topic.Id.ToString();
                chuoi = chuoi + " <a style='color:#666;' href=/?TopicId=" + TopicId + ">" + topic.Name + " </a> ";
                ListTopic = chuoi;
                ArticleCreatedDate = article.CreatedDate;
                ArticleTitle = article.Title;
                ArticleId = article.Id.ToString();
                IsVisibleComment = PSCPortal.CMS.Article.CheckVisibleComment(article.Id);
                articleCollection = PSCPortal.CMS.ArticleCollection.GetArticleCollectionPublish(topic);
                int index = 0;
                for (; index < articleCollection.Count; index++)
                {
                    if (articleCollection[index].Id.ToString() == articleid)
                        break;
                }
                int OrtherArticleNumber = int.Parse(System.Configuration.ConfigurationManager.AppSettings["OrtherArticleNumber"]);
                IEnumerable<PSCPortal.CMS.Article> it = articleCollection.Skip(index + 1).Take(OrtherArticleNumber);
                rptArticleOld.DataSource = it;
                rptArticleOld.DataBind();
                if (articleCollection.Count == 1)
                    pnCactinkhac.Visible = false;
            }
            else
            {
                PSCPortal.CMS.TopicCollection TopicCollection = PSCPortal.CMS.TopicCollection.GetTopicCollectionByArticleId(article.Id.ToString());
                string chuoi = "";
                List<string> listobj = new List<string>();
                foreach (PSCPortal.CMS.Topic item in TopicCollection)
                {
                    if (item.Id != Guid.Empty)
                    {
                        chuoi = chuoi + " <a style='color:#666;' href='/?TopicId=" + item.Id + "'> " + item.Name + " </a> ";
                        listobj.Add(item.Id.ToString());
                    }
                }

                ListTopic = chuoi;
                ArticleCreatedDate = article.CreatedDate;
                ArticleTitle = article.Title;
                ArticleId = article.Id.ToString();
                IsVisibleComment = article.IsVisibleComment.HasValue ? article.IsVisibleComment : false;
                articleCollection = PSCPortal.CMS.ArticleCollection.GetListArticleOld(article.CreatedDate, listobj);
                rptArticleOld.DataSource = articleCollection;
                rptArticleOld.DataBind();
                if (articleCollection.Count == 0)
                    pnCactinkhac.Visible = false;
            }
            /////////Commment//////////////                
            PSCPortal.CMS.ArticleCommentCollection commentList = PSCPortal.CMS.ArticleCommentCollection.GetArticleCommentPublicCollection(article.Id);
            //if (commentList.Count == 0)
            //   pnCommnet.Visible = false;
            //IEnumerable<PSCPortal.CMS.ArticleComment> commentDB = commentList.Take(5);
            //RadListView1.DataSource = commentDB;
            //RadListView1.DataBind();
            article.IncViewTime();
            UserLogin.ArticleId = article.Id.ToString();
        }
        protected void btnSend_Click(object sender, EventArgs e)
        {
            string articleid = Page.RouteData.Values["Id"] != null ? Page.RouteData.Values["Id"].ToString() : Request.QueryString["ArticleId"];
            int index1 = articleid.IndexOf("/");
            if (index1 > 0)
                articleid = articleid.Substring(0, index1);
            //if (fckContent.Content == string.Empty)
            //{
            //    lbthongbao.Text = "Nội dung không được rỗng.";
            //    lbthongbao.Visible = true;
            //    return;
            //}
            //if (txtCapCha.Text.ToUpper() != StringCapcha.ToUpper())
            //{
            //    RadCaptcha1.ErrorMessage = "Đoạn mã không hợp lệ.";
            //    StringCapcha = RadCaptcha1.CaptchaImage.Text;
            //    return;
            //}
            //RadCaptcha1.ErrorMessage = string.Empty;
            //StringCapcha = RadCaptcha1.CaptchaImage.Text;
            //try
            //{
            //    //PSCPortal.Modules.FeedBack.Libs.FeedBack feedBack1 = new PSCPortal.Modules.FeedBack.Libs.FeedBack();
            //    PSCPortal.CMS.ArticleComment feedBack = new PSCPortal.CMS.ArticleComment
            //    {
            //        IDArticle = new Guid(articleid),
            //        NameSender = txtFullName.Text,
            //        EmailSender = txtEmail.Text,
            //        Title = txtTitle.Text,
            //        Content = fckContent.Content,
            //        FeedBackDate = DateTime.Now.Date
            //    };
            //    //feedBack.Send();
            //    feedBack.Insert();
            //    ClearEditor();
            //    lbthongbao.Text = "Gởi thành công.";
            //}
            //catch (Exception)
            //{
            //    lbthongbao.Text = "Gởi không thành công.";
            //}
            //lbthongbao.Visible = true;
        }
        private void ClearEditor()
        {
            //txtFullName.Text = string.Empty;
            //txtTitle.Text = string.Empty;
            //txtEmail.Text = string.Empty;
            //fckContent.Content = string.Empty;
            //lbthongbao.Visible = false;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
    }
}
