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
using PSCPortal.CMS;

namespace PSCPortal.Systems.CMS
{
    public partial class MenuMasterDetail : PSCPortal.Framework.PSCDialog
    {
        private static MenuMasterArgs Args
        {
            get
            {
                return DataShare as MenuMasterArgs;
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
            txtId.Text = Args.MenuMaster.Id.ToString();
            txtName.Text = Args.MenuMaster.Name;
            txtDescription.Text = Args.MenuMaster.Description;
        }
        [System.Web.Services.WebMethod]
        public static void Save(string name, string description)
        {
            Args.MenuMaster.Name = name;
            Args.MenuMaster.Description = description;
        }
    }
}
