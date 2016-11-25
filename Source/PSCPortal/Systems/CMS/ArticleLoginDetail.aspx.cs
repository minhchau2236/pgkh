using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PSCPortal.CMS;

namespace PSCPortal.Systems.CMS
{
    public partial class ArticleLoginDetail : PSCPortal.Framework.PSCDialog
    {
        private static ArticleLoginArgs Args
        {
            get { return DataShare as ArticleLoginArgs; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DataBind();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            LoadData();
        }
        protected void LoadData()
        {
            txtId.Text = Args.ArticleLogin.Id.ToString();
            if (Args.IsEdit)
            {
                txtName.Text = Args.ArticleLogin.Name;
                txtPassword.Text = Args.ArticleLogin.Password;
                cbIsCheck.Checked = true;
            }
        }
        [System.Web.Services.WebMethod]
        public static void Save(string name, string password, bool check)
        {
            Args.ArticleLogin.Name = name;
            Args.ArticleLogin.Password = password;
            if (!Args.IsEdit && check)
                ArticleManage.ArticleLoginList.AddDB(Args.ArticleLogin);
            else
            {
                if (check)
                    Args.ArticleLogin.Update();
                else
                    ArticleManage.ArticleLoginList.RemoveDB(Args.ArticleLogin);
            }
        }
    }
}