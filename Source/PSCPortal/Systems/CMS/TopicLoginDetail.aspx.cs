using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PSCPortal.CMS;

namespace PSCPortal.Systems.CMS
{
    public partial class TopicLoginDetail : PSCPortal.Framework.PSCDialog
    {
        private static TopicLoginArgs Args
        {
            get
            {
                return DataShare as TopicLoginArgs;
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
            txtId.Text = Args.TopicLogin.Id.ToString();
            if (Args.IsEdit)
            {
                txtName.Text = Args.TopicLogin.Name;
                txtPassword.Text = Args.TopicLogin.Password;
                cbIsCheck.Checked = true;
            }
        }
        [System.Web.Services.WebMethod]
        public static void Save(string name, string password, bool check)
        {
            Args.TopicLogin.Name= name;
            Args.TopicLogin.Password = password;
            if (!Args.IsEdit && check)
                TopicManage.TopicLoginList.AddDB(Args.TopicLogin);            
            else
            {
                if (check)
                    Args.TopicLogin.Update();
                else
                    TopicManage.TopicLoginList.RemoveDB(Args.TopicLogin);
            }
        }
    }
}