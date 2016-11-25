using System;
using PSCPortal.Engine;
using PSCPortal.Portlets.Rotator.Libs;

namespace PSCPortal.Portlets.Rotator
{
    public partial class DisplayKhamPha : PortletControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadData();
        }
        private void LoadData()
        {
            RadRotator1.DataSource = ImagePortletCollection.GetImagePortletCollection(Portlet.PortletInstance.Id);
            RadRotator1.DataBind();
        }

        protected override void DeleteData()
        {
            throw new NotImplementedException();
        }
    }
}