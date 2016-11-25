using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using PSCPortal.Engine;

namespace PSCPortal.Systems.Engine
{
    public partial class SubDomainDetail : PSCPortal.Framework.PSCDialog
    {        
        private static SubDomainArgs Args
        {
            get
            {
                return DataShare as SubDomainArgs;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            DataBind();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            LoadData();
        }
        protected void LoadData()
        {            
            txtId.Text = Args.SubDomain.Id.ToString();
            txtName.Text = Args.SubDomain.Name;
            if (Args.IsEdit)
                txtName.Enabled = false; //khi chỉnh sửa thì không cho phép sửa "Tên subdomain"
            txtDescription.Text = Args.SubDomain.Description;
            rcbPage.DataSource = PSCPortal.Engine.PageCollection.GetPageCollection();
            rcbPage.DataBind();
            rcbPage.Items.FindItemByValue(Args.SubDomain.PageId.ToString()).Selected = true;            
        }
        [System.Web.Services.WebMethod]
        public static void Save(string name, string description, Guid pageId)
        {
            Args.SubDomain.Name = name;
            Args.SubDomain.Description = description;
            Args.SubDomain.PageId = pageId;
        }        
    }
}