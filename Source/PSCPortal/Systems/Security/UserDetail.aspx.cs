using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PSCPortal.CMS;
using PSCPortal.Security;

namespace PSCPortal.Systems.Security
{
    public partial class UserDetail : PSCPortal.Framework.PSCDialog
    {
        private static UserArgs Args
        {
            get
            {
                return DataShare as UserArgs;
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
            if (Args != null)
            {
                if (Args.IsEdit)
                {
                    txtId.Enabled = false;
                    txtName.Enabled = false;
                    txtPassword.Enabled = false;
                    txtPasswordConfirm.Enabled = false;
                }

                txtId.Text = Args.User.Id.ToString();
                txtName.Text = Args.User.Name;
                txtPassword.Text = Args.User.Password;
                txtEmail.Text = Args.User.Email;
                txtPasswordQuestion.Text = Args.User.PasswordQuestion;
                txtPasswordAnswer.Text = Args.User.PasswordAnswer;
                chkIsAdministrator.Checked = Args.IsAdministrator;
            }
        }
        [System.Web.Services.WebMethod]
        public static void Save(string name, string password, string email, string passwordQuestion, string passwordAnswer, bool isAdministrator)
        {
            Args.User.Name = name;
            Args.User.Password = password;
            Args.User.Email = email;
            Args.User.PasswordQuestion = passwordQuestion;
            Args.User.PasswordAnswer = passwordAnswer;
            Args.User.CreationDate = DateTime.Now; ;
            Args.User.LastActivityDate = DateTime.Now;
            Args.User.LastLoginDate = DateTime.Now;
            Args.User.LastPasswordChangeDate = DateTime.Now;
            Args.User.IsApproved = true;
            Args.User.IsOnline = false;
            Args.IsAdministrator = isAdministrator;
        }
    }
}
