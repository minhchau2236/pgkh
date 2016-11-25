using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace PSCPortal.Services
{
    /// <summary>
    /// Summary description for PortletProxy
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class PortletProxy : System.Web.Services.WebService
    {

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public string CallMethod(string fullClassName, string methodName, params object[] objects)
        {
            ProxyResult result = new ProxyResult();
            System.Reflection.MethodInfo method = Type.GetType(fullClassName).GetMethod(methodName, System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
            if (method == null)
            {
                result.Valid = false;
                result.InvalidMessage = "Method not exists!";
            }
            result.Result = (string)method.Invoke(null, objects);
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            return js.Serialize(result);
        }
    }
}
