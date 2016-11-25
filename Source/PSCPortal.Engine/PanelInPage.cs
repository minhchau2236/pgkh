using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;
using System.Web.UI;

namespace PSCPortal.Engine
{
    [Serializable]
    public class PanelInPage : PSCPortal.Framework.BusinessObject<PanelInPage>
    {
        #region Properties
        private PortletInstanceInPanelCollection _portlets;
        public PortletInstanceInPanelCollection Portlets
        {
            get
            {
                return _portlets;
            }
            internal set
            {
                _portlets = value;
            }
        }
        private Panel _panel;
        public Panel Panel
        {
            get
            {
                return _panel;
            }
            set
            {
                _panel = value;
            }
        }
        private string _style = string.Empty;
        public string Style
        {
            get
            {
                return _style;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                _style = value;
            }
        }

        public Page Page
        {
            get
            {
                return ((PanelInPageCollection)_collection).Page;
            }
        }

        #endregion

        #region Constructions
        public PanelInPage()
            : base()
        {
            _portlets = new PortletInstanceInPanelCollection(this);
        }

        public PanelInPage(DbDataReader reader)
            : base(reader)
        {
        }
        #endregion
        #region Abstract Methods
        protected override void MappingData(System.Data.Common.DbDataReader reader)
        {
            _panel = new Panel(reader);
            _style = (string)reader["Style"];
        }
        protected override System.Data.Common.DbCommand GetInsertCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();

            #region PanelInPagePageId
            DbParameter prPageId = database.GetParameter(System.Data.DbType.Guid, "@PageId", ((PanelInPageCollection)_collection).Page.Id);
            command.Parameters.Add(prPageId);
            #endregion

            #region PanelInPagePanelId
            DbParameter prPanelId = database.GetParameter(System.Data.DbType.Int32, "@PanelId", _panel.Id);
            command.Parameters.Add(prPanelId);
            #endregion

            #region PanelInPageStyle
            DbParameter prStyle = database.GetParameter(System.Data.DbType.String, "@Style", _style);
            command.Parameters.Add(prStyle);
            #endregion

            #region Command Insert Data
            command.CommandText = "PanelInPage_Insert";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            #endregion

            return command;
        }

        protected override System.Data.Common.DbCommand GetUpdateCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();

            #region PanelInPagePageId
            DbParameter prPageId = database.GetParameter(System.Data.DbType.Guid, "@PageId", ((PanelInPageCollection)_collection).Page.Id);
            command.Parameters.Add(prPageId);
            #endregion

            #region PanelInPagePanelId
            DbParameter prPanelId = database.GetParameter(System.Data.DbType.Int32, "@PanelId", _panel.Id);
            command.Parameters.Add(prPanelId);
            #endregion

            #region PanelInPageStyle
            DbParameter prStyle = database.GetParameter(System.Data.DbType.String, "@Style", _style);
            command.Parameters.Add(prStyle);
            #endregion

            #region Command Update Data
            command.CommandText = "PanelInPage_Update";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            #endregion

            return command;
        }

        protected override System.Data.Common.DbCommand GetDeleteCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();

            #region PanelInPagePageId
            DbParameter prPageId = database.GetParameter(System.Data.DbType.Guid, "@PageId", ((PanelInPageCollection)_collection).Page.Id);
            command.Parameters.Add(prPageId);
            #endregion

            #region PanelInPagePanelId
            DbParameter prPanelId = database.GetParameter(System.Data.DbType.Int32, "@PanelId", _panel.Id);
            command.Parameters.Add(prPanelId);
            #endregion

            #region Command Delete Data
            command.CommandText = "PanelInPage_Delete";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            #endregion

            return command;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(PanelInPage) && Panel.Id == ((PanelInPage)obj).Panel.Id
               )
                return true;
            return false;
        }
        public override int GetHashCode()
        {
            int hashCode = 0;
            return hashCode;
        }
        protected static new string ConnectionStringName
        {
            get
            {
                return "PSCPortalConnectionString";
            }
        }
        #endregion

        public System.Web.UI.Control LoadControlInTable(IPanelArgs args, bool enableEdit)
        {
            PSCPanel result = new PSCPanel();
            result.ID = string.Format("pn{0}", _panel.ToString());
            PSCPanel pnTitle = new PSCPanel();
            pnTitle.ID = string.Format("pn{0}Title", _panel.ToString());

            System.Web.UI.WebControls.Table tblTitle = new System.Web.UI.WebControls.Table();
            tblTitle.CellPadding = 0;
            tblTitle.CellSpacing = 0;
            tblTitle.Width = System.Web.UI.WebControls.Unit.Percentage(100);
            System.Web.UI.WebControls.TableRow rowTitle = new System.Web.UI.WebControls.TableRow();
            tblTitle.Rows.Add(rowTitle);
            System.Web.UI.WebControls.TableCell cellLeft = new System.Web.UI.WebControls.TableCell();
            System.Web.UI.WebControls.Image imgLeft = new System.Web.UI.WebControls.Image();
            imgLeft.ImageUrl = "~/Systems/Engine/Images/PanelTitleLeft.png";
            cellLeft.Controls.Add(imgLeft);
            cellLeft.Width = System.Web.UI.WebControls.Unit.Pixel(18);
            cellLeft.Height = System.Web.UI.WebControls.Unit.Pixel(18);
            rowTitle.Cells.Add(cellLeft);

            System.Web.UI.WebControls.TableCell cellTitle = new System.Web.UI.WebControls.TableCell();
            cellTitle.Width = System.Web.UI.WebControls.Unit.Percentage(100);
            System.Web.UI.WebControls.Label lblTitle = new System.Web.UI.WebControls.Label();
            lblTitle.Text = _panel.Name;
            cellTitle.Controls.Add(lblTitle);
            cellTitle.Style.Add(System.Web.UI.HtmlTextWriterStyle.BackgroundImage, "/Systems/Engine/Images/PanelTitleCenter.png");
            rowTitle.Cells.Add(cellTitle);


            System.Web.UI.WebControls.TableCell cellControls = new System.Web.UI.WebControls.TableCell();
            cellControls.Wrap = false;
            cellControls.Style.Add(System.Web.UI.HtmlTextWriterStyle.BackgroundImage, "/Systems/Engine/Images/PanelTitleCenter.png");

            System.Web.UI.WebControls.Literal btnEditApperance = new System.Web.UI.WebControls.Literal();
            btnEditApperance.Text = string.Format("<img id='{0}_btnEditApperance' src='/Systems/Engine/Images/PanelEditApperance.png' alt='Edit Apperance' onclick='PanelStyleGet(\"{0}\");'/>", _panel.Id, _panel.Id);
            cellControls.Controls.Add(btnEditApperance);

            if (_panel.Id != (int)Panel.PANEL.Center)
            {
                System.Web.UI.WebControls.Literal btnRemove = new System.Web.UI.WebControls.Literal();
                btnRemove.Text = string.Format("<img id='{0}_btnRemove' src='/Systems/Engine/Images/PanelDelete.png' alt='Remove Panel' onclick='PanelRemove(\"{1}\");'/>", _panel.Id, _panel.Id);
                cellControls.Controls.Add(btnRemove);
            }
            rowTitle.Cells.Add(cellControls);

            System.Web.UI.WebControls.TableCell cellRight = new System.Web.UI.WebControls.TableCell();
            System.Web.UI.WebControls.Image imgRight = new System.Web.UI.WebControls.Image();
            imgRight.ImageUrl = "~/Systems/Engine/Images/PanelTitleRight.png";
            cellRight.Controls.Add(imgRight);
            rowTitle.Cells.Add(cellRight);

            //bool enableEdit = true;                        
            if (enableEdit)
                result.Controls.Add(pnTitle);

            pnTitle.Controls.Add(tblTitle);

            PSCPanel pnMain = new PSCPanel();
            pnMain.ID = string.Format("pn{0}Display", _panel.ToString());
            pnMain.Style.Value = _style;
            //add panel class for mobile
            pnMain.CssClass = "panelCss";
            if (pnMain.Style["width"] != null)
                pnTitle.Width = System.Web.UI.WebControls.Unit.Parse(pnMain.Style["width"]);

            if (args != null)
                pnMain.Height = System.Web.UI.WebControls.Unit.Empty;

            result.Controls.Add(pnMain);
            if (enableEdit)
            {
                pnMain.BorderStyle = System.Web.UI.WebControls.BorderStyle.Dotted;
                pnMain.BorderWidth = 1;
            }

            if (args != null)
            {
                System.Web.UI.UserControl uc = new System.Web.UI.UserControl();
                pnMain.Controls.Add(uc.LoadControl(args.Path));
            }
            else
            {
                foreach (PortletInstanceInPanel item in _portlets)
                {
                    pnMain.Controls.Add(item.RenderPortletTable(enableEdit));
                }
            }

            return result;
        }

         public void LoadControlInDiv(IPanelArgs args, bool enableEdit, PSCPanel pannel)
            {
                  //Create Div Edit

                  if (enableEdit)
                  {
                        PSCPanel divEdit = new PSCPanel();
                        divEdit.ID = string.Format("psc-divPanel{0}-edit", _panel.ToString());
                        divEdit.CssClass="psc-divPanelEdit";
                        pannel.Controls.Add(divEdit);

                        //Title Pannel
                        System.Web.UI.WebControls.Literal lblTitle = new System.Web.UI.WebControls.Literal();
                        lblTitle.Text = string.Format("<span>{0}</span>", _panel.Name);
                        divEdit.Controls.Add(lblTitle);

                        //Button Edit
                        System.Web.UI.WebControls.Literal btnEditApperance = new System.Web.UI.WebControls.Literal();
                        btnEditApperance.Text = string.Format("<img id='{0}_btnEditApperance' title='Hiệu chỉnh Panel' src='/Systems/Engine/Images/PortletEditApperance.png' alt='Edit Apperance' onclick='PanelStyleGet(\"{0}\");'/>", _panel.Id, _panel.Id);
                        divEdit.Controls.Add(btnEditApperance);

                        //Button Remove
                        System.Web.UI.WebControls.Literal btnRemove = new System.Web.UI.WebControls.Literal();
                        btnRemove.Text = string.Format("<img id='{0}_btnRemove'  title='Xóa Panel' src='/Systems/Engine/Images/PortletDelete.png' alt='Remove Panel' onclick='PanelRemove(\"{1}\");'/>", _panel.Id, _panel.Id);
                        divEdit.Controls.Add(btnRemove);

                      

                  }

                  PSCPanel divContent = new PSCPanel();
                  divContent.ID = string.Format("psc-divPanel{0}-Content", _panel.ToString());
                  divContent.Style.Value= "min-height: 30px;"; 
                  pannel.Style.Value=_style;
                  pannel.Controls.Add(divContent);

                  //Load Porlets into divContent
                  if (args != null)
                  {
                        System.Web.UI.UserControl uc = new System.Web.UI.UserControl();
                        divContent.Controls.Add(uc.LoadControl(args.Path));
                  }
                  else
                  {
                        foreach (PortletInstanceInPanel item in _portlets)
                        {
                              //divContent.Controls.Add(item.RenderTable(enableEdit));


                              //Create div Portlet Wrapper
                              PSCPanel divPortletWrapper = new PSCPanel();
                              divPortletWrapper.ID = string.Format("psc-divPortlet-{0}",          item.PortletInstance.Id.ToString());
                              divPortletWrapper.Style.Value=item.Style;
                              divPortletWrapper.CssClass = "psc-divPortlet-Wrapper";
                              divContent.Controls.Add(divPortletWrapper);

                              // Create div Portlet Header
                              if(enableEdit)
                              CreateDivPortletHeader(divPortletWrapper, item);

                              // Create div Portlet Content
                              PSCPanel divPortletContent = new PSCPanel();
                              //divPortletContent.ID = string.Format("psc-divPortlet-{0}", item.PortletInstance.Id.ToString());
                              divPortletContent.CssClass = "psc-divPortlet-Content";
                              divPortletWrapper.Controls.Add(divPortletContent);

                              divPortletContent.Controls.Add(item.RenderPortletDiv(enableEdit));
                        }
                  }

            }

            private void CreateDivPortletHeader(PSCPanel panel, PortletInstanceInPanel portletInstanceInPanel)
            {

                  // Create div portlet header 
                  PSCPanel divHeader = new PSCPanel();
                  divHeader.CssClass = "psc-divPortlet-Header";
                  panel.Controls.Add( divHeader);

                  //Title Porlet
                  System.Web.UI.WebControls.Literal lblTitle = new System.Web.UI.WebControls.Literal();
                  lblTitle.Text = string.Format("<span>{0}</span>", portletInstanceInPanel.PortletInstance.Name);
                  divHeader.Controls.Add(lblTitle);

                  //Button Edit
                  LiteralControl btnEdit = new LiteralControl();
                  btnEdit.Text = string.Format("<img src='{0}' onclick=\"{1}\" title='Hiệu chỉnh Dữ liệu' class='{2}' alt='{3}'/>", "/Systems/Engine/Images/PortletEditData.png", string.Format("PortletEditData('{0}');", portletInstanceInPanel.PortletInstance.Id), "ButtonImage", "Edit Portlet");
                  divHeader.Controls.Add(btnEdit);



                  //Button giao dien
                  LiteralControl btnEditApperance = new LiteralControl();
                  btnEditApperance.Text = string.Format("<img src='{0}' onclick=\"{1}\" title='Hiệu chỉnh CSS' class='{2}' alt='{3}'/>", "/Systems/Engine/Images/PortletEditApperance.png", string.Format("PortletEditCSS('{0}');", portletInstanceInPanel.PortletInstance.Id), "ButtonImage", "Edit Portlet CSS");
                  divHeader.Controls.Add(btnEditApperance);


                  // Button Delete
                  LiteralControl btnDelete = new LiteralControl();
                  btnDelete.Text = string.Format("<img src='{0}' onclick=\"{1}\" title='Xóa Porlet' class='{2}' alt='{3}'/>", "/Systems/Engine/Images/PortletDelete.png", string.Format("PortletRemove('{0}','{1}');", portletInstanceInPanel.PortletInstance.Id,portletInstanceInPanel.PortletInstance.Name), "ButtonImage", "Remove Portlet");
                  divHeader.Controls.Add(btnDelete);


            }

      }
}