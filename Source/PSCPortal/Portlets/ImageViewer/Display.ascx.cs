using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using PSCPortal.Portlets.Rotator.Libs;

namespace PSCPortal.Portlets.ImageViewer
{
    public partial class Display : Engine.PortletControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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
            List<ImagePortlet> imageList = PSCPortal.Portlets.Rotator.Libs.ImagePortletCollection.GetImagePortletCollection(Portlet.PortletInstance.Id).Take(16).ToList();
            var js = new JavaScriptSerializer();
            ListImage = js.Serialize(imageList);
        }
        protected override void DeleteData()
        {
            throw new NotImplementedException();
        }
    }
}