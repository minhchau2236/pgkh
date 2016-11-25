using PSCPortal.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PSCPortal.Portlets.Rotator
{
    public partial class Edit : Engine.PortletEditControl
    {
        public string PortletInstanceId { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            PortletInstance portlet = PortletInstance.GetPortletInstance(DataId);
            txtPortletName.Value = portlet.Name;
            Session["ImagePortletDataId"] = DataId;
            if (!IsPostBack)
            {
                ScriptManager scriptManager = ScriptManager.GetCurrent(Page);
                var serviceReference = new ServiceReference { Path = "~/Portlets/Rotator/Libs/ImageService.asmx", InlineScript = false };
                if (scriptManager != null) scriptManager.Services.Add(serviceReference);
            }
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            PortletInstance portlet = PortletInstance.GetPortletInstance(DataId);
            portlet.Name = txtPortletName.Value;
            portlet.Update();
            Accept();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Cancel();
        }
    }
}