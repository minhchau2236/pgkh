using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PSCPortal.Framework;
using PSCPortal.Engine;
using PSCPortal.CMS;

namespace PSCPortal.Systems.Engine
{
    public partial class PageDetail : PSCDialog
    {
        private static PageArgs Args
        {
            get
            {
                return DataShare as PageArgs;
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
            txtId.Text = Args.Page.Id.ToString();
            txtName.Text = Args.Page.Name;
            txtTitle.Text = Args.Page.Title;
            rcbLayout.DataSource = LayoutCollection.GetAllLayout();
            rcbLayout.DataBind();
            Layout layout = LayoutCollection.GetPageLayOut(Args.Page.Id).SingleOrDefault();
            if (layout != null)
                rcbLayout.Items.FindItemByValue(layout.Id.ToString()).Selected = true;
            else
                rcbLayout.Items.FindItemByValue(Args.Page.LayoutId.ToString()).Selected = true;
            switch (Args.Page.Template)
            {
                case 1:
                    rdoTemplate1.Checked = true;
                    break;
                case 2:
                    rdoTemplate2.Checked = true;
                    break;
                case 3:
                    rdoTemplateMobile.Checked = true;
                    break;
                case 4:
                    rdoTemplate4.Checked = true;
                    break;
                case 5:
                    rdoTemplate5.Checked = true;
                    break;
                case 6:
                    rdoTemplate6.Checked = true;
                    break;
                case 7:
                    rdoTemplate7.Checked = true;
                    break;
                case 8:
                    rdoTemplate8.Checked = true;
                    break;
                case 9:
                    rdoTemplate9.Checked = true;
                    break;
                case 10:
                    rdoTemplate10.Checked = true;
                    break;
                case 11:
                    rdoTemplate11.Checked = true;
                    break;
                case 12:
                    rdoTemplate12.Checked = true;
                    break;
                default:
                    rdoTemplate2.Checked = true;
                    break;
            }
            switch (Args.Page.Language)
            {
                case 1:
                    rdoVietNam.Checked = true;
                    break;
                case 2:
                    rdoEnglish.Checked = true;
                    break;
                default:
                    rdoVietNam.Checked = true;
                    break;
            }

        }
        [System.Web.Services.WebMethod]
        public static void Save(string name, string title, int template, int language, Guid layoutId)
        {
            Args.Page.Name = name;
            Args.Page.Title = title;
            Args.Page.Template = template;
            Args.Page.Language = language;
            Args.Page.LayoutId = layoutId;
        }
    }
}
