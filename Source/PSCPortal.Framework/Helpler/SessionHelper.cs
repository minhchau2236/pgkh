using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace PSCPortal.Framework.Helpler
{
    public enum SessionKey
    {
        SubDomain
    }

    public static class SessionHelper
    {
        public static string GetSession(SessionKey key)
        {
            return HttpContext.Current.Session[key.ToString()]==null?string.Empty:HttpContext.Current.Session[key.ToString()].ToString();
            
        }

        public static void SetSession(SessionKey key, string value)
        {
            HttpContext.Current.Session[key.ToString()] = value;
        }
    }
}
