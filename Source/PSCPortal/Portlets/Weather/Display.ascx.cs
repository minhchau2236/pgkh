using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net;
using System.Xml;
using System.IO;

namespace PSCPortal.Portlets.Weather
{
    public partial class Display : Engine.PortletControl
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void DeleteData()
        {
            throw new NotImplementedException();
        }
    }
}