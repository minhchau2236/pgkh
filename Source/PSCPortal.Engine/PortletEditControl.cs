using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PSCPortal.Framework;

namespace PSCPortal.Engine
{
    [Serializable]
    public class PortletEditControl : System.Web.UI.UserControl
    {
        private static string NameSession = "PortletEditDataSelf";
        public Guid DataId
        {
            get
            {
                return new Guid(Request.QueryString["id"]) ;
            }            
        }
        protected override void OnLoad(EventArgs e)
        {
            if (!IsPostBack)
                Session.Remove(NameSession);
            base.OnLoad(e);
        }
        protected DataContainer DataSelf
        {
            get
            {                
                if (Session[NameSession] == null)
                    Session[NameSession] = new DataContainer();
                return Session[NameSession] as DataContainer;
            }
        }
        protected void Accept()
        {
            if (!Page.ClientScript.IsStartupScriptRegistered("accept"))
            {

                Page.ClientScript.RegisterStartupScript

                    (this.GetType(), "accept", "Accept();", true);

            }            
        }
        protected void Cancel()
        {
            if (!Page.ClientScript.IsStartupScriptRegistered("cancel"))
            {

                Page.ClientScript.RegisterStartupScript

                    (this.GetType(), "cancel", "Cancel();", true);

            }            
        }
    }
}
