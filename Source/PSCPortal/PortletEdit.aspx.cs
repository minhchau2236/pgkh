using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PSCPortal.Engine;

namespace PSCPortal
{
    public partial class PortletEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            Guid id = new Guid(Request.QueryString["id"]);
            PortletInstance pi = PortletInstance.GetPortletInstance(id);
            phPortlet.Controls.Add(LoadControl(pi.Portlet.EditURL));
        }
    }
}
