using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using PSCPortal.CMS;
using System.Data.SqlClient;
using PSCPortal.Framework.Helpler;
using PSCPortal.Engine;

namespace PSCPortal.Portlets.Vote
{
    public partial class Display : Engine.PortletControl
    {
        public string VoteQuestionName
        {
            get
            {
                if (ViewState["VoteQuestionName"] == null)
                    ViewState["VoteQuestionName"] = string.Empty;
                return (string)ViewState["VoteQuestionName"];
            }
            set
            {
                ViewState["VoteQuestionName"] = value;
            }
        }
        protected VoteQuestion voteQuestion
        {
            get
            {
                return ViewState["voteQuestion"] as VoteQuestion;
            }
            set
            {
                ViewState["voteQuestion"] = value;
            }
        }
        protected VoteAnswerCollection ListVoteAnswer
        {
            get
            {
                return ViewState["ListVoteAnswer"] as VoteAnswerCollection;
            }
            set
            {
                ViewState["ListVoteAnswer"] = value;
            }
        }
        protected int TotalVoteNumber
        {
            get
            {

                return (int)ViewState["TotalVoteNumber"];
            }
            set
            {
                ViewState["TotalVoteNumber"] = value;
            }
        }
        /// <summary>
             

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
                DataBind();
            }
        }
        private void LoadData()
        {
            
            string subName = Request.Url.Host.Replace(ConfigurationManager.AppSettings["DomainName"], "");
            if (subName.Length > 0)
                subName = subName.Substring(0, subName.Length - 1);
            SubDomain subDomain = subName == string.Empty ? SubDomain.GetSubByName("HomePage") : SubDomain.GetSubByName(subName);
            if (subDomain == null)
                voteQuestion = VoteQuestion.GetVoteQuestionActive();
            else
                voteQuestion = VoteQuestion.GetVoteQuestionBySubDomainActive(subDomain.Id);

                VoteQuestionName = voteQuestion.Name;
                ListVoteAnswer = VoteAnswerCollection.GetVoteAnswerCollectionByVoteQuestion(voteQuestion);
                rblVoteAnswer.DataSource = ListVoteAnswer;
                rblVoteAnswer.DataBind();
                rblVoteAnswer.SelectedIndex = 0;
                TotalVoteNumber = ListVoteAnswer.Sum(item => item.Number);
                /**/
                lb_TongSoPhieu.Text = "Tổng số phiếu: " + TotalVoteNumber;
                lb_XemKetQua_CauHoi.Text = voteQuestion.Name;
                gv_KetQua.DataSource = ListVoteAnswer;
                gv_KetQua.DataBind();
            
                                             
        }
        protected override void DeleteData()
        {
            //using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSCPortalConnectionString"].ConnectionString))
            //{
            //    SqlCommand com = new SqlCommand();
            //    com.Connection = con;
            //    con.Open();
            //    com.CommandType = System.Data.CommandType.Text;
            //    com.Parameters.AddWithValue("@dataId", Portlet.PortletInstance.Id);
            //    com.CommandText = "Delete Vote Where DataId=@dataId";
            //}
        }





        protected string ComputePercent(int SoPhieu)
        {
            if (TotalVoteNumber == 0) return "00.00 %";
            float percent = (float)SoPhieu / TotalVoteNumber * 100;
            return percent.ToString("00.00") + " %";
        }

        protected void ibt_Chon_Click(object sender, ImageClickEventArgs e)
        {
            if (rblVoteAnswer.SelectedIndex != -1)
            {
                if (Request.Cookies["Vote"] != null)
                    return;
                HttpCookie httpCookie = new HttpCookie("Vote", "");
                httpCookie.Expires = DateTime.Now.AddSeconds(60);
                Response.Cookies.Add(httpCookie);
                Guid selectedVote = new Guid((string)rblVoteAnswer.SelectedValue);
                VoteAnswer voteAnswer = ListVoteAnswer.Where(item => ((VoteAnswer)item).Id == selectedVote).Single();
                voteAnswer.Number = voteAnswer.Number + 1;
                voteAnswer.Update();
                TotalVoteNumber = TotalVoteNumber + 1;
                /**/
                lb_TongSoPhieu.Text = "Tổng số phiếu: " + TotalVoteNumber;
                gv_KetQua.DataSource = ListVoteAnswer;
                gv_KetQua.DataBind();
                Response.Redirect(Request.Url.ToString());                
            }
        }
    }
}