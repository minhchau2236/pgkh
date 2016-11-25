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

namespace PSCPortal.Systems.CMS
{
    public partial class MailScheduleDetail : PSCPortal.Framework.PSCDialog
    {
        private static MailScheduleArgs Args
        {
            get
            {
                return DataShare as MailScheduleArgs;
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
            txtId.Text = Args.MailSchedule.Id.ToString();
            txtName.Text = Args.MailSchedule.Name;
            txtMail.Text = Args.MailSchedule.Mail;
            
            
        }
        [System.Web.Services.WebMethod]
        public static void Save(string name, string mail)
        {
            Args.MailSchedule.Name = name;
            Args.MailSchedule.Mail = mail;
        }
    }
}