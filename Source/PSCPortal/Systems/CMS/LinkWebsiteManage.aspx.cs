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
    public partial class LinkWebsiteManage : PSCPortal.Framework.PSCPage
    {
        protected static LinkWebsiteCollection LinkWebsiteList
        {
            get
            {
                if (DataStatic["LinkWebsiteList"] == null)
                {
                    Guid subId = SessionHelper.GetSession(SessionKey.SubDomain) == string.Empty ? Guid.Empty : new Guid(SessionHelper.GetSession(SessionKey.SubDomain));
                    if (subId == Guid.Empty)
                        DataStatic["LinkWebsiteList"] = LinkWebsiteCollection.GetLinkWebsiteCollection();
                    else
                    {
                        DataStatic["LinkWebsiteList"] = LinkWebsiteCollection.GetLinkWebsiteCollectionBySubDomain(subId);
                    }

                }
                return DataStatic["LinkWebsiteList"] as LinkWebsiteCollection;
            }
            set
            {
                DataStatic["LinkWebsiteList"] = value;
            }
        }

        [System.Web.Services.WebMethod]
        public static string GetLinkWebsiteList(int startIndex, int maximumRows, string sortExpressions)
        {
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            return js.Serialize(LinkWebsiteList.GetSegment(startIndex, maximumRows, sortExpressions));
        }
        [System.Web.Services.WebMethod]
        public static int GetLinkWebsiteCount()
        {
            return LinkWebsiteList.Count;
        }
        [System.Web.Services.WebMethod]
        public static void LinkWebsiteNew()
        {
            Guid subId = SessionHelper.GetSession(SessionKey.SubDomain) == string.Empty ? Guid.Empty : new Guid(SessionHelper.GetSession(SessionKey.SubDomain));
            LinkWebsite item = new LinkWebsite();
            item.Id = Guid.NewGuid();
            item.SubDomainId = subId;
            PSCDialog.DataShare = new LinkWebsiteArgs(item, false);
        }
        [System.Web.Services.WebMethod]
        public static void LinkWebsiteAdd()
        {
            LinkWebsite question = ((LinkWebsiteArgs)PSCDialog.DataShare).LinkWebsite;
            LinkWebsiteList.AddDB(question);
        }
        [System.Web.Services.WebMethod]
        public static void LinkWebsiteEdit(string id)
        {
            Guid idLinkWebsite = new Guid(id);
            PSCDialog.DataShare = new LinkWebsiteArgs(LinkWebsiteList.Where(a => a.Id == idLinkWebsite).Single(), true);
        }
        [System.Web.Services.WebMethod]
        public static void LinkWebsiteUpdate()
        {
            (PSCDialog.DataShare as PSCPortal.CMS.LinkWebsiteArgs).LinkWebsite.Update();
            DataStatic["LinkWebsiteList"] = null;
        }
        [System.Web.Services.WebMethod]
        public static void LinkWebsiteDelete(string[] arrId)
        {
            foreach (string id in arrId)
            {
                Guid idLinkWebsite = new Guid(id);
                LinkWebsiteList.RemoveDB(LinkWebsiteList.Where(a => a.Id == idLinkWebsite).Single());
            }
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