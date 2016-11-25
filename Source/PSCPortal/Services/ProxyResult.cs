using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PSCPortal.Services
{
    public class ProxyResult
    {
        public bool Valid { get; set; }
        public string FullClassName { get; set; }
        public string MethodName { get; set; }
        public string InvalidMessage { get; set; }
        public string Result { get; set; }
    }
}
