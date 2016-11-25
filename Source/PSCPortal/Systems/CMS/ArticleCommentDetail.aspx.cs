using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq.Expressions;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Collections.Generic;
using PSCPortal.CMS;
namespace PSCPortal.Systems.CMS
{
    public partial class ArticleCommentDetail : PSCPortal.Framework.PSCDialog
    {
        private static PSCPortal.CMS.ArticleCommentArgs Args
        {
            get
            {
                return DataShare as PSCPortal.CMS.ArticleCommentArgs;
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
            txtId.Text = Args.FeedBack.ID.ToString();
            txtIdArticle.Text = Args.FeedBack.IDArticle.ToString();
            txtTitle.Text = Args.FeedBack.Title;

            txtName.Text = Args.FeedBack.NameSender;
            txtEmail.Text = Args.FeedBack.EmailSender;

            rdiCreatedDate.SelectedDate = Args.FeedBack.FeedBackDate;
            txtQuestion.Text = Server.HtmlDecode(Args.FeedBack.Content).ToString();
            txtAns.Text = Args.FeedBack.ContentReply;
        }
        [System.Web.Services.WebMethod]
        public static void Save(string title, string name, string email, DateTime createdDate, string ques, string ans)
        {
            Args.FeedBack.Title = title;
            Args.FeedBack.NameSender = name;
            Args.FeedBack.EmailSender = email;
            Args.FeedBack.FeedBackDate = createdDate;
            Args.FeedBack.Content = ques;
            Args.FeedBack.ContentReply = ans;
        }
    }
}