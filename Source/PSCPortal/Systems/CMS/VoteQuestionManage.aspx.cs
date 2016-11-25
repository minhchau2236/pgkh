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
using System.Collections.Generic;
using PSCPortal.Framework.Helpler;
using PSCPortal.Security;
using PSCPortal.Framework;
using PSCPortal.Engine;

namespace PSCPortal.Systems.CMS
{
    public partial class VoteQuestionManage : PSCPortal.Framework.PSCPage
    {
        protected static VoteQuestionCollection VoteQuestionList
        {
            get
            {
                if (DataStatic["VoteQuestionList"] == null)
                {
                    Guid subId = SessionHelper.GetSession(SessionKey.SubDomain) == string.Empty ? Guid.Empty : new Guid(SessionHelper.GetSession(SessionKey.SubDomain));
                     if (subId == Guid.Empty)
                        DataStatic["VoteQuestionList"] = VoteQuestionCollection.GetVoteQuestionCollection();
                    else
                    {
                        DataStatic["VoteQuestionList"] = VoteQuestionCollection.GetVoteQuestionCollectionBySubDomain(subId);
                    }
                    
                }
                return DataStatic["VoteQuestionList"] as VoteQuestionCollection;
            }
            set
            {
                DataStatic["VoteQuestionList"] = value;
            }
        }
        
        [System.Web.Services.WebMethod]
        public static string GetVoteQuestionList(int startIndex, int maximumRows, string sortExpressions)
        {
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            return js.Serialize(VoteQuestionList.GetSegment(startIndex, maximumRows, sortExpressions));
        }
        [System.Web.Services.WebMethod]
        public static int GetVoteQuestionCount()
        {
            return VoteQuestionList.Count;
        }
        [System.Web.Services.WebMethod]
        public static void VoteQuestionNew()
        {
            Guid subId = SessionHelper.GetSession(SessionKey.SubDomain) == string.Empty ? Guid.Empty : new Guid(SessionHelper.GetSession(SessionKey.SubDomain));
            VoteQuestion item = new VoteQuestion();
            item.Id = Guid.NewGuid();
            item.SubDomainId = subId;
            PSCDialog.DataShare = new VoteQuestionArgs(item, false);
        }
        [System.Web.Services.WebMethod]
        public static void VoteQuestionAdd()
        {
            VoteQuestion question = ((VoteQuestionArgs)PSCDialog.DataShare).VoteQuestion;
            VoteQuestionList.AddDB(question);
        }
        [System.Web.Services.WebMethod]
        public static void VoteQuestionEdit(string id)
        {
            Guid idVoteQuestion = new Guid(id);
            PSCDialog.DataShare = new VoteQuestionArgs(VoteQuestionList.Where(a => a.Id == idVoteQuestion).Single(), true);
        }
        [System.Web.Services.WebMethod]
        public static void VoteQuestionUpdate()
        {
            (PSCDialog.DataShare as PSCPortal.CMS.VoteQuestionArgs).VoteQuestion.Update();
            DataStatic["VoteQuestionList"] = null;
        }
        [System.Web.Services.WebMethod]
        public static void VoteQuestionDelete(string[] arrId)
        {
            foreach (string id in arrId)
            {
                Guid idVoteQuestion = new Guid(id);
                VoteQuestionList.RemoveDB(VoteQuestionList.Where(a => a.Id == idVoteQuestion).Single());
            }
        }
        [System.Web.Services.WebMethod]
        public static void VoteQuestionEditAnswer(string id)
        {
            Guid idVoteQuestion = new Guid(id);
            PSCPage.DataShare = VoteQuestionList.Where(item => item.Id == idVoteQuestion).Single();
        }

        [System.Web.Services.WebMethod]
        public static string GetPermission(int[] arr)
        {
            Dictionary<string, bool> result = new Dictionary<string, bool>();
            foreach (int idFun in arr)
            {
                result.Add(idFun.ToString(), PSCPortal.Security.SystemAuthentication.CheckAllowFunction(PSCPortal.Security.Function.Parse(idFun)));
            }
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            return js.Serialize(result);
        }
    }
}
