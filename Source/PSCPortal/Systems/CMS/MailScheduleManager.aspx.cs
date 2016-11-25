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
using System.Collections.Generic;

namespace PSCPortal.Systems.CMS
{
    public partial class MailScheduleManager : PSCPortal.Framework.PSCPage
    {
        protected static MailScheduleCollection ListMailSchedule
        {
            get
            {
                if (DataStatic["ListMailSchedule"] == null)
                    DataStatic["ListMailSchedule"] = MailScheduleCollection.GetMailScheduleCollection();
                return DataStatic["ListMailSchedule"] as MailScheduleCollection;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DataBind();
        }
        [System.Web.Services.WebMethod]
        public static string GetMailScheduleList(int startIndex, int maximumRows, string sortExpressions)
        {
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            return js.Serialize(ListMailSchedule.GetSegment(startIndex, maximumRows, sortExpressions));
        }
        [System.Web.Services.WebMethod]
        public static int GetMailScheduleCount()
        {
            return ListMailSchedule.Count;
        }
        [System.Web.Services.WebMethod]
        public static void MailScheduleNew()
        {
            MailSchedule item = new MailSchedule();
            item.Id = Guid.NewGuid();
            PSCDialog.DataShare = new MailScheduleArgs(item, false);
        }
        [System.Web.Services.WebMethod]
        public static void MailScheduleAdd()
        {
            ListMailSchedule.AddDB(((MailScheduleArgs)PSCDialog.DataShare).MailSchedule);
        }
        [System.Web.Services.WebMethod]
        public static void MailScheduleEdit(string id)
        {
            Guid idMailSchedule = new Guid(id);
            PSCDialog.DataShare = new MailScheduleArgs(ListMailSchedule.Where(a => a.Id == idMailSchedule).Single(), true);
        }
        [System.Web.Services.WebMethod]
        public static void MailScheduleUpdate()
        {
            (PSCDialog.DataShare as PSCPortal.CMS.MailScheduleArgs).MailSchedule.Update();
            DataStatic["ListMailSchedule"] = null;
        }
        [System.Web.Services.WebMethod]
        public static void MailScheduleDelete(string[] arrId)
        {
            foreach (string id in arrId)
            {
                Guid idMailSchedule = new Guid(id);
                ListMailSchedule.RemoveDB(ListMailSchedule.Where(a => a.Id == idMailSchedule).Single());
            }
        }
    }
}