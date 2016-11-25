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
using PSCPortal.Framework;
namespace PSCPortal.Systems.CMS
{
    public partial class VoteAnswerManage : PSCPortal.Framework.PSCPage
    {
        protected static VoteQuestion VoteQuestion
        {
            get
            {
                return DataShare as VoteQuestion;
            }
        }
        protected static VoteAnswerCollection VoteAnswerList
        {
            get
            {
                if (DataStatic["VoteAnswerList"] == null)
                    DataStatic["VoteAnswerList"] = VoteAnswerCollection.GetVoteAnswerCollectionByVoteQuestion(VoteQuestion);
                return DataStatic["VoteAnswerList"] as VoteAnswerCollection;
            }
        }
        [System.Web.Services.WebMethod]
        public static string GetVoteAnswerList(int startIndex, int maximumRows, string sortExpressions)
        {

            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            return js.Serialize(VoteAnswerList.GetSegment(startIndex, maximumRows, (sortExpressions == String.Empty) ? "Order ASC" : "Order ASC," +sortExpressions));
        }
        [System.Web.Services.WebMethod]
        public static int GetVoteAnswerCount()
        {
            return VoteAnswerList.Count;
        }
        [System.Web.Services.WebMethod]
        public static void VoteAnswerNew()
        {
            VoteAnswer item = new VoteAnswer();
            item.Id = Guid.NewGuid();
            PSCDialog.DataShare = new VoteAnswerArgs(item, false);
        }
        [System.Web.Services.WebMethod]
        public static void VoteAnswerAdd()
        {
            VoteAnswerList.AddDB(((VoteAnswerArgs)PSCDialog.DataShare).VoteAnswer);
        }
        [System.Web.Services.WebMethod]
        public static void VoteAnswerEdit(string id)
        {
            Guid idVoteAnswer = new Guid(id);
            PSCDialog.DataShare = new VoteAnswerArgs(VoteAnswerList.Where(a => a.Id == idVoteAnswer).Single(), true);
        }
        [System.Web.Services.WebMethod]
        public static void VoteAnswerUpdate()
        {
            (PSCDialog.DataShare as PSCPortal.CMS.VoteAnswerArgs).VoteAnswer.Update();
            DataStatic["VoteAnswerList"] = null;
        }
        [System.Web.Services.WebMethod]
        public static void VoteAnswerDelete(string[] arrId)
        {
            foreach (string id in arrId)
            {
                Guid idVoteAnswer = new Guid(id);
                VoteAnswerList.RemoveDB(VoteAnswerList.Where(a => a.Id == idVoteAnswer).Single());
            }
        }
        [System.Web.Services.WebMethod]
        public static void VoteAnswerMoveUp(string id)
        {
            Guid idVoteAnswer = new Guid(id);
            VoteAnswerList.MoveUp(VoteAnswerList.Where(a => a.Id == idVoteAnswer).Single());
        }
        [System.Web.Services.WebMethod]
        public static void VoteAnswerMoveDown(string id)
        {
            Guid idVoteAnswer = new Guid(id);
            VoteAnswerList.MoveDown(VoteAnswerList.Where(a => a.Id == idVoteAnswer).Single());
        }
    }
}
