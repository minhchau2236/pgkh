using System;
using System.Linq;
using System.Web.Services;

namespace PSCPortal.Portlets.Rotator.Libs
{

    /// <summary>
    /// Summary description for VideoClipService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ImageService : WebService
    {

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public string GetImagePortletList()
        {
            ImagePortletCollection imagePortletList = ImagePortletCollection.GetImagePortletCollection(new Guid(System.Web.HttpContext.Current.Session["ImagePortletDataId"].ToString()));
            var js = new System.Web.Script.Serialization.JavaScriptSerializer();
            return js.Serialize(imagePortletList);
        }
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public void ImagePortletNew()
        {
            var item = new ImagePortlet
                {
                    Id = Guid.NewGuid(),
                    DataId = new Guid(System.Web.HttpContext.Current.Session["ImagePortletDataId"].ToString())
                };
            item.Order = ImagePortlet.GetImageOrderMax(item.DataId) + 1;
            System.Web.HttpContext.Current.Session["ImagePortletArgs"] = new ImagePortletArgs(item, false);
        }
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public void ImagePortletAdd()
        {
            ((ImagePortletArgs) System.Web.HttpContext.Current.Session["ImagePortletArgs"]).ImagePortlet.Insert();
            System.Web.HttpContext.Current.Session.Remove("ImagePortletArgs");
        }
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public void ImagePortletEdit(string id)
        {
            var idImagePortlet = new Guid(id);
            System.Web.HttpContext.Current.Session["ImagePortletArgs"] = new ImagePortletArgs(ImagePortletCollection.GetImagePortletCollection(new Guid(System.Web.HttpContext.Current.Session["ImagePortletDataId"].ToString())).Single(a => a.Id == idImagePortlet), true);
        }
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public void ImagePortletUpdate()
        {
            ((ImagePortletArgs) System.Web.HttpContext.Current.Session["ImagePortletArgs"]).ImagePortlet.Update();
            System.Web.HttpContext.Current.Session.Remove("ImagePortletArgs");
        }
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod]
        public void ImagePortletDelete(string[] arrId)
        {
            foreach (string id in arrId)
            {
                var idImagePortlet = new Guid(id);
                ImagePortletCollection.GetImagePortletCollection(new Guid(System.Web.HttpContext.Current.Session["ImagePortletDataId"].ToString())).RemoveDB(ImagePortletCollection.GetImagePortletCollection(new Guid(System.Web.HttpContext.Current.Session["ImagePortletDataId"].ToString())).Single(a => a.Id == idImagePortlet));
            }
        }
    }
}
