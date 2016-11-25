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
using PSCPortal.Security;
using System.Collections.Generic;

namespace PSCPortal.Systems.CMS
{
    public partial class TopicManage : PSCPortal.Framework.PSCPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
                //GetPermission1();
            }
        }
        protected void LoadData()
        {
            rtvTopic.DataSource = TopicList.GetBindingSource();
            rtvTopic.DataBind();
        }

        protected static TopicCollection TopicList
        {
            get
            {
                if (DataStatic["TopicList"] == null)
                {
                    DataStatic["TopicList"] = TopicCollection.GetTopicCollection();
                }
                return DataStatic["TopicList"] as TopicCollection;
            }
        }
        public static TopicLoginCollection TopicLoginList
        {
            get
            {
                if (DataStatic["TopicLoginList"] == null)
                    DataStatic["TopicLoginList"] = TopicLoginCollection.GetTopicLoginCollection();
                return DataStatic["TopicLoginList"] as TopicLoginCollection;
            }
            set
            {
                DataStatic["TopicLoginList"] = value;
            }
        }
        [System.Web.Services.WebMethod]
        public static void TopicNew()
        {
            Topic item = new Topic();
            item.Id = Guid.NewGuid();
            PSCDialog.DataShare = new TopicArgs(item, false);
        }
        [System.Web.Services.WebMethod]
        public static string TopicAdd()
        {
            Topic topic = ((TopicArgs)PSCDialog.DataShare).Topic;
            TopicList.AddDB(((TopicArgs)PSCDialog.DataShare).Topic);
            TopicList.UpdateLastPosition(((TopicArgs)PSCDialog.DataShare).Topic);
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            return js.Serialize(((TopicArgs)PSCDialog.DataShare).Topic);
        }
        [System.Web.Services.WebMethod]
        public static void TopicEdit(string id)
        {
            Guid idTopic = new Guid(id);
            PSCDialog.DataShare = new TopicArgs((Topic)TopicList.Search(o => ((Topic)o).Id == idTopic), true);
        }
        [System.Web.Services.WebMethod]
        public static void TopicEditContentTemplate(Guid id)
        {
            PSCDialog.DataShare = new TopicArgs((Topic)TopicList.Search(o => ((Topic)o).Id == id), true);
        }
        [System.Web.Services.WebMethod]
        public static void TopicSecurity(string id)
        {
            Guid idTopic = new Guid(id);
            PSCDialog.DataShare = new TopicArgs((Topic)TopicList.Search(o => ((Topic)o).Id == idTopic), true);
        }
        [System.Web.Services.WebMethod]
        public static string TopicUpdate()
        {
            (PSCDialog.DataShare as PSCPortal.CMS.TopicArgs).Topic.Update();
            DataStatic["TopicList"] = null;
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            return js.Serialize(((TopicArgs)PSCDialog.DataShare).Topic);
        }
        [System.Web.Services.WebMethod]
        public static void TopicDelete(string id)
        {
            Guid idTopic = new Guid(id);
            TopicList.RemoveDB((Topic)TopicList.Search(o => ((Topic)o).Id == idTopic));
        }
        [System.Web.Services.WebMethod]
        public static void ChangeParent(string idChild, string idParent)
        {
            Topic child = (Topic)TopicList.Search(t => ((Topic)t).Id == new Guid(idChild));
            Topic parent = (Topic)TopicList.Search(t => ((Topic)t).Id == new Guid(idParent));
            child.Parent = parent;
            child.Update();
        }
        [System.Web.Services.WebMethod]
        public static void TopicCopy()
        {
            Topic tp = new Topic();
            tp.Id = Guid.NewGuid();
            PSCDialog.DataShare = new TopicArgs(tp, false);
        }
        [System.Web.Services.WebMethod]
        public static void TopicCopyDo(string id)
        {
            Topic topic = (Topic)TopicList.Search(t => ((Topic)t).Id == new Guid(id));
            TopicList.TopicCopy(topic, ((TopicArgs)PSCDialog.DataShare).Topic);
        }
        [System.Web.Services.WebMethod]
        public static void TopicMakeMenu()
        {
            MenuMaster mm = new MenuMaster();
            mm.Id = Guid.NewGuid();
            PSCDialog.DataShare = new MenuMasterArgs(mm, false);
        }
        [System.Web.Services.WebMethod]
        public static void TopicMakeMenuDo(string id)
        {
            Topic topic = (Topic)TopicList.Search(t => ((Topic)t).Id == new Guid(id));
            TopicList.TopicMakeMenu(topic, ((MenuMasterArgs)PSCDialog.DataShare).MenuMaster);
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

        //public void GetPermission1()
        //{
        //    int[] arr = { 20, 21, 22, 23, 24, 25 };
        //    foreach (int idFun in arr)
        //    {
        //        bool result = PSCPortal.Security.SystemAuthentication.CheckAllowFunction(PSCPortal.Security.Function.Parse(idFun));
        //        switch (idFun)
        //        {
        //            case 20:
        //                btnTopicAdd.Visible = result;
        //                break;
        //            case 21:
        //                btnTopicEdit.Visible = result;
        //                btnChangeRoot.Visible = result;
        //                btnTopicLoginEdit.Visible = result;
        //                rtvTopic.EnableDragAndDrop = result;
        //                break;
        //            case 22:
        //                btnTopicCopy.Visible = result;
        //                break;
        //            case 23:
        //                btnTopicMakeMenu.Visible = result;
        //                break;
        //            case 24:
        //                btnTopicDelete.Visible = result;
        //                break;
        //            case 25:
        //                btnTopicPermission.Visible = result;
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //}

        [System.Web.Services.WebMethod]
        public static void ChangeRoot(Guid id)
        {
            Topic topic = (Topic)TopicList.Search(t => ((Topic)t).Id == id);
            topic.ChangeRoot();
        }
        [System.Web.Services.WebMethod]
        public static void TopicLoginEdit(string id)
        {
            Guid idTopic = new Guid(id);
            TopicLogin topicLogin = TopicLoginList.Where(t => t.Id == idTopic).SingleOrDefault();
            if (topicLogin != null)
                PSCDialog.DataShare = new TopicLoginArgs(topicLogin, true);
            else
            {
                topicLogin = new TopicLogin();
                topicLogin.Id = idTopic;
                PSCDialog.DataShare = new TopicLoginArgs(topicLogin, false);
            }
        }
        [System.Web.Services.WebMethod]
        public static void TopicMoveUp(string id)
        {
            Guid topicId = new Guid(id);
            TopicList.MoveUp((PSCPortal.CMS.Topic)TopicList.Search(m => ((PSCPortal.CMS.Topic)m).Id == topicId));
        }
        [System.Web.Services.WebMethod]
        public static void TopicMoveDown(string id)
        {
            Guid topicId = new Guid(id);
            TopicList.MoveDown((PSCPortal.CMS.Topic)TopicList.Search(m => ((PSCPortal.CMS.Topic)m).Id == topicId));
        }
    }
}
