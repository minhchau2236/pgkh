using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PSCPortal.Security;

namespace PSCPortal.Systems.Security
{
    public partial class RoleDetail : PSCPortal.Framework.PSCDialog
    {
        private static RoleArgs Args
        {
            get
            {
                return DataShare as RoleArgs;
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
            if (Args.IsEdit)
                txtId.Enabled = false;

            txtId.Text = Args.Role.Id.ToString();
            txtName.Text = Args.Role.Name;
            txtDescription.Text = Args.Role.Description;
        }
        [System.Web.Services.WebMethod]
        public static void Save(string name, string description)
        {
            Args.Role.Name = name;
            Args.Role.Description = description;
        }
    }
}
