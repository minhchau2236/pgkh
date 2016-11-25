using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PSCPortal.Framework;
using System.Data.Common;
using System.Web.UI.WebControls;
namespace PSCPortal.Engine
{
    [Serializable]
    public class PanelInPageCollection : PSCPortal.Framework.BusinessObjectCollection<PanelInPageCollection, PanelInPage>
    {
        private Page _page;
        public Page Page
        {
            get
            {
                return _page;
            }
        }
        private PanelInPageCollection()
            : base()
        {
        }

        public void PortletRemoveDB(Guid idPortlet)
        { 
            foreach (PanelInPage item in this)
            {
                foreach (PortletInstanceInPanel pi in item.Portlets)
                {
                    if (pi.PortletInstance.Id == idPortlet)
                    {
                        item.Portlets.RemoveDB(pi);
                        return;
                    }
                }
            }
        }

        protected override DbCommand GetSelectAllCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region PageId
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@PageId", _page.Id);
            command.Parameters.Add(prId);
            #endregion
            command.CommandText = "PanelInPage_GetAllByPageId";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            return command;
        }

        public PortletInstanceInPanel Search(Guid idPortlet)
        {            
            foreach (PanelInPage pip in this)
            {
                foreach (PortletInstanceInPanel item in pip.Portlets)
                    if (item.PortletInstance.Id == idPortlet)
                        return item;
            }
            return null;
        }

        public static PanelInPageCollection GetPanelInPageCollection(Page page)
        {
            Database database = new Database("PSCPortalConnectionString");
            PanelInPageCollection result = new PanelInPageCollection();
            result._page = page;
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = result.GetSelectAllCommand();
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    PanelInPage item = new PanelInPage(reader);
                    result.Add(item);
                    item.Portlets = PortletInstanceInPanelCollection.GetPortletInstanceInPanelCollection(item);
                }
            }            
            return result;
        }
        public void PortletMove(int panel, int index1, int index2)
        {
            PanelInPage pip = this.Where(p=>p.Panel.Id==panel).Single();
            PortletInstanceInPanel portlet = pip.Portlets[index1];
            pip.Portlets.RemoveAt(index1);            
            pip.Portlets.Insert(index2, portlet);
            pip.Portlets.UpdatePosition();          
        }
        public void PortletMove(int panel1, int index1, int panel2, int index2)
        {
            PanelInPage pip1 = this.Where(p => p.Panel.Id == panel1).Single();
            PanelInPage pip2 = this.Where(p => p.Panel.Id == panel2).Single();

            PortletInstanceInPanel portlet = pip1.Portlets[index1];
            pip1.Portlets.RemoveAt(index1);
            pip2.Portlets.Insert(index2, portlet);

            pip1.Portlets.UpdatePosition();
            pip2.Portlets.UpdatePosition();
        }
        protected static new string ConnectionStringName
        {
            get
            {
                return "PSCPortalConnectionString";
            }
        }
        public PanelInPage Search(Panel.PANEL panel)
        {
            foreach (PanelInPage item in this)
                if (item.Panel.Id == (int)panel)
                    return item;
            return null;
        }
        public WebControl RenderTable(IPanelArgs args, bool enableEdit)
        {
            PanelInPage pnTop = Search(Panel.PANEL.Top);
            PanelInPage pnLeft = Search(Panel.PANEL.Left);
            PanelInPage pnCenter = Search(Panel.PANEL.Center);
            PanelInPage pnRight = Search(Panel.PANEL.Right);
            PanelInPage pnBottom = Search(Panel.PANEL.Bottom);
            int colSpan = 1;
            Table result = new Table();
            result.CellPadding = 0;
            result.CellSpacing = 0;
            result.Width = new Unit("100%");
            TableRow rowTop = new TableRow();
            TableCell cellTop = new TableCell();
            rowTop.Cells.Add(cellTop);
            if (pnTop != null)
            {
                result.Rows.Add(rowTop);
                cellTop.Controls.Add(pnTop.LoadControlInTable(null, enableEdit));
            }

            TableRow rowCenter = new TableRow();
            rowCenter.ID = "rowCenter";
            rowCenter.VerticalAlign = VerticalAlign.Top;
            if (pnLeft != null)
            {
                TableCell cellLeft = new TableCell();
                rowCenter.Cells.Add(cellLeft);
                cellLeft.Controls.Add(pnLeft.LoadControlInTable(null, enableEdit));
                colSpan++;
            }
            TableCell cellCenter = new TableCell();
            rowCenter.Cells.Add(cellCenter);
            cellCenter.Controls.Add(pnCenter.LoadControlInTable(args, enableEdit));

            if (pnRight != null)
            {
                TableCell cellRight = new TableCell();
                rowCenter.Cells.Add(cellRight);
                cellRight.Controls.Add(pnRight.LoadControlInTable(null, enableEdit));
                colSpan++;
            }
            result.Rows.Add(rowCenter);

            TableRow rowBottom = new TableRow();
            TableCell cellBottom = new TableCell();
            rowBottom.Cells.Add(cellBottom);
            if (pnBottom != null)
            {
                result.Rows.Add(rowBottom);
                cellBottom.Controls.Add(pnBottom.LoadControlInTable(null, enableEdit));
            }

            cellTop.ColumnSpan = colSpan;
            cellBottom.ColumnSpan = colSpan;
            return result; 
            }
            /// <summary>
            /// 
            /// 
            /// <div Id="psc-divWrapper">
		///	<div Id="psc-divTop">TOP</div>
		///	<div Id="psc-divMain">
		///		<div Id="psc-divLeft">Left-sidebar</div>
		///		<div Id="psc-divCenter">Main Content</div>
		///		<div Id="psc-divRight">Right-sidebar</div>
		///	</div>
		///	<div Id="psc-divBottom">Bottom</div>
		/// </div>
            /// 
            /// 
            /// </summary>
            /// <param name="args"></param>
            /// <param name="enableEdit"></param>
            /// <returns></returns>
         public WebControl RenderDIV(IPanelArgs args, bool enableEdit)
            {

                  PanelInPage pnTop = Search(Panel.PANEL.Top);
                  PanelInPage pnLeft = Search(Panel.PANEL.Left);
                  PanelInPage pnCenter = Search(Panel.PANEL.Center);
                  PanelInPage pnRight = Search(Panel.PANEL.Right);
                  PanelInPage pnBottom = Search(Panel.PANEL.Bottom);
                  
                  //Create DIV Wrapper
                  PSCPanel divWrapper = new PSCPanel();
                  divWrapper.ID = string.Format("psc-divPanelWrapper");

                 
                  if (pnTop != null)
                  {
                        //Create DIV TOP
                        PSCPanel divTop = new PSCPanel();
                        divTop.ID = string.Format("psc-divPanelTop");
                        //add divTop to wrapper
                        divWrapper.Controls.Add(divTop);
                        
                        //Loading controls in divTop and add to divTop
                        pnTop.LoadControlInDiv(null, enableEdit, divTop);
                  }

                  //Create DIV Main
                  PSCPanel divMain = new PSCPanel();
                  divMain.ID = string.Format("psc-divPanelMid");
                  //add divMain to wrapper
                  divWrapper.Controls.Add(divMain);


                  
                  if (pnLeft != null)
                  {
                        //Create DIV Left
                        PSCPanel divLeft = new PSCPanel();
                        divLeft.ID = string.Format("psc-divPanelLeft");

                        //add divTop to DIV Main
                        divMain.Controls.Add(divLeft);

                        //Loading controls in divLeft
                        pnLeft.LoadControlInDiv(null, enableEdit, divLeft);
                  }


                  //if (pnCenter != null)
                  //{
                        //Create divCenter
                        PSCPanel divCenter = new PSCPanel();
                        divCenter.ID = string.Format("psc-divPanelCenter");

                        //add divCenter to DIV Main
                        divMain.Controls.Add(divCenter);

                        //Loading controls in divLeft
                        pnCenter.LoadControlInDiv(args, enableEdit, divCenter);
                 // }

                  if (pnRight != null)
                  {
                        //Create divRight
                        PSCPanel divRight = new PSCPanel();
                        divRight.ID = string.Format("psc-divPanelRight");

                        //add divRight to DIV Main
                        divMain.Controls.Add(divRight);

                        //Loading controls in divRight
                        pnRight.LoadControlInDiv(null, enableEdit,divRight);
                  }

                  if (pnBottom != null)
                  {
                        //Create DIV Bottom
                        PSCPanel divBottom = new PSCPanel();
                        divBottom.ID = string.Format("psc-divPanelBottom");
                        //add divBottom to wrapper
                        divWrapper.Controls.Add(divBottom);

                        //Loading controls in divBottom
                        pnBottom.LoadControlInDiv(null, enableEdit,divBottom);
                  }

                  return divWrapper;
            }
      }
}