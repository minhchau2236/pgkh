using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;

namespace PSCPortal.Portlets.NivoSlider
{
    public partial class DisplayVs2 : Engine.PortletControl
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