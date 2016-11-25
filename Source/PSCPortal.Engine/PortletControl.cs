using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Collections.Specialized;

namespace PSCPortal.Engine
{
    public abstract class PortletControl : System.Web.UI.UserControl
    {
        //public override string ClientID
        //{
        //    get
        //    {
        //        return base.ID;
        //    }
        //}
        //public override string UniqueID
        //{
        //    get
        //    {
        //        return base.ID;
        //    }
        //}
        public PortletInstanceInPanel Portlet
        {
            get
            {
                return ViewState["Portlet"] as PortletInstanceInPanel;
            }
            set
            {
                ViewState["Portlet"] = value;
            }
        }
        public bool EnableEdit
        {
            get
            {
                return (bool)ViewState["EnableEdit"];
            }
            set
            {
                ViewState["EnableEdit"] = value;
            }
        }
        protected System.Web.UI.WebControls.Panel _pnTop = new System.Web.UI.WebControls.Panel();

        protected override void OnInit(EventArgs e)
        {
            string groupAdmin = System.Configuration.ConfigurationManager.AppSettings["GroupAdmin"];
            if (System.Web.HttpContext.Current.User.IsInRole(groupAdmin) && EnableEdit)
                EnableEdit = true;

            if (EnableEdit)
                  {
                        //RenderTitlePorletInTable();
                       // RenderTitlePorletInDiv(); khi load control (portlet) thi kiem tra neu edit thi them phan header de chinh sua
                  }
                  base.OnInit(e);
      
            }

            private void RenderTitlePorletInTable()
            {
                  System.Web.UI.WebControls.Table tblTop = new System.Web.UI.WebControls.Table();
                  tblTop.CellPadding = 0;
                  tblTop.CellSpacing = 0;
                  System.Web.UI.WebControls.TableRow rTop = new System.Web.UI.WebControls.TableRow();
                  tblTop.Rows.Add(rTop);
                  System.Web.UI.WebControls.TableCell cLeft = new System.Web.UI.WebControls.TableCell();
                  cLeft.Width = System.Web.UI.WebControls.Unit.Percentage(100);
                  cLeft.CssClass = "TitlePortlet";

                  rTop.Cells.Add(cLeft);
                  System.Web.UI.WebControls.TableCell cRight = new System.Web.UI.WebControls.TableCell();
                  rTop.Cells.Add(cRight);
                  cRight.Width = System.Web.UI.WebControls.Unit.Pixel(50);
                  cRight.CssClass = "PanelTitlePortlet";

                  System.Web.UI.WebControls.Panel pnTitle = new System.Web.UI.WebControls.Panel();
                  pnTitle.Width = System.Web.UI.WebControls.Unit.Percentage(100);

                  System.Web.UI.WebControls.Label lblPortletInfo = new System.Web.UI.WebControls.Label();
                  lblPortletInfo.Text = string.Format("{0}{1}", Portlet.PortletInstance.Portlet.Name, Portlet.PortletInstance.Name != string.Empty ? "-" + Portlet.PortletInstance.Name : string.Empty);
                  pnTitle.Controls.Add(lblPortletInfo);


                  System.Web.UI.WebControls.Panel pnControlPortlet = new System.Web.UI.WebControls.Panel();
                  pnControlPortlet.Width = System.Web.UI.WebControls.Unit.Pixel(50);

                  LiteralControl btnDelete = new LiteralControl();
                  btnDelete.Text = string.Format("<img src='{0}' onclick=\"{1}\" class='{2}' alt='{3}'/>", "/Systems/Engine/Images/PortletDelete.png", string.Format("PortletRemove('{0}','{1}');", Portlet.PortletInstance.Id, Portlet.PortletInstance.Name), "ButtonImage", "Remove Portlet");

                  LiteralControl btnEdit = new LiteralControl();
                  btnEdit.Text = string.Format("<img src='{0}' onclick=\"{1}\" class='{2}' alt='{3}'/>", "/Systems/Engine/Images/PortletEditData.png", string.Format("PortletEditData('{0}');", Portlet.PortletInstance.Id), "ButtonImage", "Edit Portlet");

                  LiteralControl btnEditApperance = new LiteralControl();
                  btnEditApperance.Text = string.Format("<img src='{0}' onclick=\"{1}\" class='{2}' alt='{3}'/>", "/Systems/Engine/Images/PortletEditApperance.png", string.Format("PortletEditCSS('{0}');", Portlet.PortletInstance.Id), "ButtonImage", "Edit Portlet CSS");

                  if (Portlet.PortletInstance.Portlet.EditURL.Trim() != string.Empty)
                        pnControlPortlet.Controls.Add(btnEdit);
                  pnControlPortlet.Controls.Add(btnEditApperance);
                  pnControlPortlet.Controls.Add(btnDelete);

                  _pnTop.ID = string.Format("{0}", "Title");
                  cLeft.Controls.Add(pnTitle);
                  cRight.Controls.Add(pnControlPortlet);
                  _pnTop.Controls.Add(tblTop);

                  Controls.AddAt(0, _pnTop);
            }

            private void RenderTitlePorletInDiv()
            {

                  // Create div portlet header 
                  PSCPanel divHeader = new PSCPanel();
                  divHeader.CssClass = "psc-divPortlet-Header";
                  Controls.AddAt(0, divHeader);

                  //Title Porlet
                  System.Web.UI.WebControls.Literal lblTitle = new System.Web.UI.WebControls.Literal();
                  lblTitle.Text = string.Format("<span>{0}</span>", Portlet.PortletInstance.Portlet.Name);
                  divHeader.Controls.Add(lblTitle);

                  //Button Edit
                  LiteralControl btnEdit = new LiteralControl();
                  btnEdit.Text = string.Format("<img src='{0}' onclick=\"{1}\" title='Hiệu chỉnh Dữ liệu' class='{2}' alt='{3}'/>", "/Systems/Engine/Images/PortletEditData.png", string.Format("PortletEditData('{0}');", Portlet.PortletInstance.Id), "ButtonImage", "Edit Portlet");
                  divHeader.Controls.Add(btnEdit);

                  //Button giao dien
                  LiteralControl btnEditApperance = new LiteralControl();
                  btnEditApperance.Text = string.Format("<img src='{0}' onclick=\"{1}\" title='Hiệu chỉnh CSS' class='{2}' alt='{3}'/>", "/Systems/Engine/Images/PortletEditApperance.png", string.Format("PortletEditCSS('{0}');", Portlet.PortletInstance.Id), "ButtonImage", "Edit Portlet CSS");
                  divHeader.Controls.Add(btnEditApperance);


                  // Button Delete
                  LiteralControl btnDelete = new LiteralControl();
                  btnDelete.Text = string.Format("<img src='{0}' onclick=\"{1}\" title='Xóa Porlet' class='{2}' alt='{3}'/>", "/Systems/Engine/Images/PortletDelete.png", string.Format("PortletRemove('{0}','{1}');", Portlet.PortletInstance.Id, Portlet.PortletInstance.Name), "ButtonImage", "Remove Portlet");
                  divHeader.Controls.Add(btnDelete);


            }

            //protected override void Render(System.Web.UI.HtmlTextWriter writer)
            //{
            //      writer.Write(string.Format("<div id='{0}' style=\"{1}\" class='portletCss'>", ClientID, Portlet.Style));
            //      writer.RenderBeginTag(System.Web.UI.HtmlTextWriterTag.Div);
            //      base.Render(writer);
            //      writer.RenderEndTag();
            //      writer.Write("</div>");
            //}
            protected void btnEdit_Click(object sender, ImageClickEventArgs e)
        {
            //Type type = Page.GetType().GetInterface(typeof(IHostPortlet).ToString());
            //if (type != null)
            //{
            //    ((IHostPortlet)Page).EditPortlet(Portlet);
            //}
        }
        protected void btnEditAppearance_Click(object sender, ImageClickEventArgs e)
        {
            //Type type = Page.GetType().GetInterface(typeof(IHostPortlet).ToString());
            //if (type != null)
            //{
            //    ((IHostPortlet)Page).EditAppearancePortlet(Portlet);
            //}
        }
        protected abstract void DeleteData();
    }
}
