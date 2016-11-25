using System;
using System.IO;
using System.Web.Script.Serialization;

namespace PSCPortal.Portlets.ImageSlide
{
    public partial class Display : Engine.PortletControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string picArr = "";
            PSCPortal.Portlets.Rotator.Libs.ImagePortletCollection imageList = PSCPortal.Portlets.Rotator.Libs.ImagePortletCollection.GetImagePortletCollection(Portlet.PortletInstance.Id);
            var js = new JavaScriptSerializer();
            picArr = js.Serialize(imageList);
            string script = "<script type='text/javascript' language='javascript'> try{ var innerFadeInstance = new System.Utility.ImageInnerFade('" + imgInnerFadeHolder.ClientID + "','" + Portlet.PortletInstance.Id + "'); innerFadeInstance.createImageInnerFade(" + picArr + "); }catch(e){} </script>";
            Page.ClientScript.RegisterStartupScript(Page.GetType(), Guid.NewGuid().ToString(), script);
        }

        protected override void DeleteData()
        {
            throw new NotImplementedException();
        }
    }
}