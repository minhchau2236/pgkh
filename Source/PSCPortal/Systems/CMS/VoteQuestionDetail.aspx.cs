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
    public partial class VoteQuestionDetail : PSCPortal.Framework.PSCDialog
    {
        private static VoteQuestionArgs Args
        {
            get
            {
                return DataShare as VoteQuestionArgs;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            LoadData();
        }
        protected void LoadData()
        {
            txtId.Text = Args.VoteQuestion.Id.ToString();
           
            if (Args.IsEdit)
            {
                txtName.Text = Args.VoteQuestion.Name;
                cbIsCheck.Checked = Args.VoteQuestion.IsActive;
            }
        }
        [System.Web.Services.WebMethod]
        public static void Save(string name, bool check)
        {
            Args.VoteQuestion.Name = name;
            Args.VoteQuestion.IsActive = check;            
        }
    }
}
