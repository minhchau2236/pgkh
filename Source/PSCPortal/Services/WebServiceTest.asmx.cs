using PSCPortal.CMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace PSCPortal.Services
{
    /// <summary>
    /// Summary description for WebServiceTest
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class WebServiceTest : System.Web.Services.WebService
    {

         [System.Web.Services.WebMethod(EnableSession = true)]
        public string GetFormContent()
        {
            Layout layout = LayoutCollection.GetLayOut("4").SingleOrDefault();
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            string s = js.Serialize(layout);
            return s;
        }
    }
}
