using System;
using System.Linq;
using PSCPortal.Framework;
using PSCPortal.Engine;

namespace PSCPortal.Systems.Engine
{
    public partial class SubDomainManage : PSCPage
    {
        protected static SubDomainCollection SubDomainList
        {
            get
            {
                if (DataStatic["SubDomainList"] == null)
                    DataStatic["SubDomainList"] = SubDomainCollection.GetSubDomainCollection();
                return DataStatic["SubDomainList"] as SubDomainCollection;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
        [System.Web.Services.WebMethod]
        public static string GetSubDomainList(int startIndex, int maximumRows, string sortExpressions)
        {
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            return js.Serialize(SubDomainList.GetSegment(startIndex, maximumRows, sortExpressions));
        }
        [System.Web.Services.WebMethod]
        public static int GetSubDomainCount()
        {
            return SubDomainList.Count;
        }
        [System.Web.Services.WebMethod]
        public static void SubDomainNew()
        {
            SubDomain item = new SubDomain {Id = Guid.NewGuid()};
            PSCDialog.DataShare = new SubDomainArgs(item, false);
        }
        [System.Web.Services.WebMethod]
        public static string SubDomainAdd()
        {
            SubDomain item = ((SubDomainArgs)PSCDialog.DataShare).SubDomain;
            SubDomainList.AddDB(item);
            //add folder to new subdomain
            const string rootImages = "~/Resources/Images/SubDomain/";
            const string rootDocs = "~/Resources/Docs/SubDomain/";
            const string rootMedias = "~/Resources/Medias/SubDomain/";
            const string rootFlashs = "~/Resources/Flashs/SubDomain/";
            const string rootAlbums = "~/Resources/Albums/SubDomain/";
            const string rootVideoClips = "~/Resources/VideoClips/SubDomain/";

            if (!System.IO.Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(rootImages + "/" + item.Name)))
                System.IO.Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(rootImages + "/" + item.Name));
            if (!System.IO.Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(rootDocs + "/" + item.Name)))
                System.IO.Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(rootDocs + "/" + item.Name));
            if (!System.IO.Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(rootMedias + "/" + item.Name)))
                System.IO.Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(rootMedias + "/" + item.Name));
            if (!System.IO.Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(rootFlashs + "/" + item.Name)))
                System.IO.Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(rootFlashs + "/" + item.Name));
            if (!System.IO.Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(rootAlbums + "/" + item.Name)))
                System.IO.Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(rootAlbums + "/" + item.Name));
            if (!System.IO.Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(rootVideoClips + "/" + item.Name)))
                System.IO.Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(rootVideoClips + "/" + item.Name));
            //
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            return js.Serialize(item);
        }
        [System.Web.Services.WebMethod]
        public static void SubDomainEdit(string id)
        {
            Guid idSub = new Guid(id);
            SubDomain sub = SubDomainList.Single(a => a.Id == idSub);
            PSCDialog.DataShare = new SubDomainArgs(sub, true);
        }
        [System.Web.Services.WebMethod]
        public static void SubDomainUpdate()
        {
            SubDomainArgs item = (SubDomainArgs)PSCDialog.DataShare;
            item.SubDomain.Update();
            DataStatic["SubDomainList"] = null;
        }
        [System.Web.Services.WebMethod]
        public static void SubDomainDelete(string[] arrId)
        {
            foreach (string id in arrId)
            {
                Guid idsub = new Guid(id);
                SubDomainList.RemoveDB(SubDomainList.Single(a => a.Id == idsub));
            }
        }
        [System.Web.Services.WebMethod]
        public static void SubDomainConfig(string id)
        {
            Guid idSub = new Guid(id);
            SubDomain sub = SubDomainList.Single(a => a.Id == idSub);
            PSCDialog.DataShare = new SubDomainArgs(sub, true);
        }
    }
}