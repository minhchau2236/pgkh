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
    public partial class VoteAnswerDetail : PSCPortal.Framework.PSCDialog
    {
        private static VoteAnswerArgs Args
        {
            get
            {
                return DataShare as VoteAnswerArgs;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            LoadData();
        }
        protected void LoadData()
        {
            txtId.Text = Args.VoteAnswer.Id.ToString();
            txtName.Text = Args.VoteAnswer.Name;
            txtNumber.Text = Args.VoteAnswer.Number.ToString();
        }
        [System.Web.Services.WebMethod]
        public static void Save(string name, string number)
        {
            Args.VoteAnswer.Name = name;
            Args.VoteAnswer.Number = Int32.Parse(number);
        }
    }
}
