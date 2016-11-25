using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PSCPortal.Portlets.ImageAutoRotator
{
    public partial class Display_DoiTac : Engine.PortletControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadData();
            DataBind();
        }
        public string ListImage
        {
            get;
            set;
        }
        protected void LoadData()
        {
            PSCPortal.Portlets.Rotator.Libs.ImagePortletCollection imageList = PSCPortal.Portlets.Rotator.Libs.ImagePortletCollection.GetImagePortletCollection(Portlet.PortletInstance.Id);
            var js = new JavaScriptSerializer();
            ListImage = js.Serialize(imageList);
        }
        protected override void DeleteData()
        {
            throw new NotImplementedException();
        }
    }
}