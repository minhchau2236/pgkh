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
using PSCPortal.Framework;
using PSCPortal.Framework.Helpler;
using PSCPortal.CMS;
using System.Data.SqlClient;
namespace PSCPortal.Portlets.Vote
{
    public partial class Edit : Engine.PortletEditControl
    {
        protected Guid subId
        {
            get
            {
                return SessionHelper.GetSession(SessionKey.SubDomain) == string.Empty ? Guid.Empty : new Guid(SessionHelper.GetSession(SessionKey.SubDomain));
            }
        }
        protected VoteQuestionCollection ListVoteQuestion
        {
            get
            {
                if (ViewState["ListVoteQuestion"] == null)
                {
                    Guid subId = SessionHelper.GetSession(SessionKey.SubDomain) == string.Empty ? Guid.Empty : new Guid(SessionHelper.GetSession(SessionKey.SubDomain));
                    if (subId == Guid.Empty)
                        ViewState["ListVoteQuestion"] = VoteQuestionCollection.GetVoteQuestionCollection();
                    else
                    {
                        ViewState["ListVoteQuestion"] = VoteQuestionCollection.GetVoteQuestionCollectionBySubDomain(subId);
                    }

                }
                return ViewState["ListVoteQuestion"] as VoteQuestionCollection;
            }            
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            LoadData();
        }

        private void LoadData()
        {
           
            
            ddlVoteQuestion.DataSource = ListVoteQuestion;
            ddlVoteQuestion.DataBind();
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSCPortalConnectionString"].ConnectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                connection.Open();
                command.Parameters.AddWithValue("@DataId", DataId);
                command.CommandText = "SELECT VoteQuestionId FROM Vote WHERE DataId=@DataId";
                Object id = command.ExecuteScalar();
                if (id != null)
                {
                    Guid voteQuestionId = (Guid)id;
                    int index = 0;
                    foreach (VoteQuestion voteQuestion in ListVoteQuestion)
                    {
                        if (voteQuestion.Id == voteQuestionId)
                            {
                                break;
                            }
                            index++;
                    }
                    ddlVoteQuestion.SelectedIndex = index;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSCPortalConnectionString"].ConnectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.Parameters.AddWithValue("@DataId", DataId);
                command.CommandText = "SELECT VoteQuestionId FROM Vote WHERE DataId=@DataId";

                Object voteQuestionId = command.ExecuteScalar();

                if (voteQuestionId == null)
                {
                    //neu khong ton tai record nao chua dataId
                    SqlCommand com = new SqlCommand();
                    com.Connection = connection;

                    com.Parameters.AddWithValue("@DataId", DataId);
                    com.Parameters.AddWithValue("@VoteQuestionId", new Guid(ddlVoteQuestion.SelectedValue));
                    com.CommandText = "INSERT INTO Vote(DataId,VoteQuestionId) VALUES (@DataId,@VoteQuestionId)";
                    com.ExecuteNonQuery();
                }
                else
                {
                    //nguoc lai ta update thong tin ve TopicId cho portlet
                    SqlCommand com = new SqlCommand();
                    com.Connection = connection;

                    com.Parameters.AddWithValue("@DataId", DataId);
                    command.Parameters.AddWithValue("@SubDomainId", subId);
                    com.Parameters.AddWithValue("@VoteQuestionId", new Guid(ddlVoteQuestion.SelectedValue));
                    com.CommandText = "UPDATE Vote SET VoteQuestionId=@VoteQuestionId WHERE DataId=@DataId";
                    com.ExecuteNonQuery();
                }
            }
            Accept();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Cancel();
        }        
    }
}