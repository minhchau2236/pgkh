<%@ Page EnableViewState="false" Title="<%# Resources.Site.PortletManage %>" Language="C#" MasterPageFile="~/Systems/MasterPage.Master" AutoEventWireup="true" CodeBehind="PortletManage.aspx.cs" Inherits="PSCPortal.Systems.Engine.PortletManage" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">
        var dialogMethod;
        function CallWebMethodSuccess(results, context, methodName) {
            switch (methodName) {
                case "GetPortletList":
                    {
                        GetPortletListCallback(results, context, methodName);
                    }
                    break;
                case "GetPortletCount":
                    {
                        GetPortletCountCallback(results, context, methodName);
                    }
                    break;
                case "PortletNew": 
                    {
                        PortletNewCallback(results, context, methodName);
                    }
                    break;
                case "PortletAdd":
                    {
                        PortletAddCallback(results, context, methodName);
                    }
                    break;
                case "PortletEdit":
                    {
                        PortletEditCallback(results, context, methodName);
                    }
                    break;
                case "PortletUpdate":
                    {
                        PortletUpdateCallback(results, context, methodName);
                    }
                    break;
                case "PortletDelete":
                    {
                        PortletDeleteCallback(results, context, methodName);
                    }
                    break;
                case "GetPermission":
                    {
                        SecurityUICallback(results, context, methodName);
                    }
                    break;
            }
        }
        function GetPortletSelect() {
            var items = $find("<%=gvPortlet.ClientID%>").get_masterTableView().get_selectedItems();

            var objList = new Array();
            for (var i = 0; i < items.length; i++) {
                Array.add(objList, items[i].get_dataItem().Id);
            }
            return objList;
        }
        function CallWebMethodFailed(results, context, methodName) {
            alert(results._message);
        }

        function GetPortletList() {
            var tableView = $find("<%=gvPortlet.ClientID%>").get_masterTableView();
            var sortExpressions = tableView.get_sortExpressions();
            PageMethods.GetPortletList(0, tableView.get_pageSize(), sortExpressions.toString(), CallWebMethodSuccess);
            PageMethods.GetPortletCount(CallWebMethodSuccess);
        }
        function GetPortletListCallback(results, context, methodName) {
            var grid = $find("<%=gvPortlet.ClientID%>");
            var tableView = grid.get_masterTableView();
            tableView.set_dataSource(Sys.Serialization.JavaScriptSerializer.deserialize(results));
            tableView.dataBind();
        }

        function GetPortletCountCallback(results, context, methodName) {
            var tableView = $find("<%=gvPortlet.ClientID%>").get_masterTableView();
            tableView.set_virtualItemCount(results);
        }

        function PortletNew() {
            PageMethods.PortletNew(CallWebMethodSuccess, CallWebMethodFailed);
        }
        function PortletNewCallback(results, context, methodName) {
            dialogMethod = "PortletNew";
            var oWnd = $find("<%= rwPortlet.ClientID %>");            
            oWnd.setSize(700, 300);
            oWnd.setUrl("PortletDetail.aspx");
            oWnd.show();
        }
        function PortletAddCallback(result, context, methodName) {
            GetPortletList();
        }

        function PortletEdit() {
            var items = $find("<%=gvPortlet.ClientID%>").get_masterTableView().get_selectedItems();
            if (items.length == 0) {
                radalert("<%= Resources.Site.PortletNotChoose %>", 250, 100, "<%= Resources.Site.Warning %>");
                return;
            }
            var obj = items[0].get_dataItem();
            PageMethods.PortletEdit(obj.Id, CallWebMethodSuccess, CallWebMethodFailed);
        }
        function PortletEditCallback(results, context, methodName) {
            dialogMethod = "PortletEdit";
            var oWnd = $find("<%= rwPortlet.ClientID %>");
            oWnd.setSize(700, 300);
            oWnd.setUrl("PortletDetail.aspx");
            oWnd.show();
        }

        function PortletUpdateCallback(results, context, methodName) {
            GetPortletList();
        }

        function PortletDelete() {
            var objList = GetPortletSelect();
            if (objList.length == 0) {
                radalert("<%= Resources.Site.PortletNotChoose %>", 250, 100, "<%= Resources.Site.Warning %>");
                return;
            }
            radconfirm("<%= Resources.Site.PortletConfirmDelete %>", PortletDeleteConfirm, 250, 100, null, "<%= Resources.Site.Warning %>");
        }
        function PortletDeleteConfirm(arg) {
            if (!arg)
                return;
            var objList = GetPortletSelect();
            PageMethods.PortletDelete(objList, CallWebMethodSuccess, CallWebMethodFailed);
        }
        function PortletDeleteCallback(results, context, methodName) {
            GetPortletList();
            $find("<%=gvPortlet.ClientID%>").get_masterTableView().clearSelectedItems();
        }
        var bntPortletAdd;
        var bntPortletEdit;
        var bntPortletDelete;
        function CreateElementPorletLinks() {
            bntPortletAdd = document.createElement("a");
            bntPortletAdd.setAttribute("href", "javascript:void(0)");
            bntPortletAdd.setAttribute("onClick", "PortletNew()");
            bntPortletAdd.setAttribute("class", "Header");
            var addName = document.createTextNode("[Thêm mới]");
            bntPortletAdd.appendChild(addName);

            bntPortletEdit = document.createElement("a");
            bntPortletEdit.setAttribute("href", "javascript:void(0)");
            bntPortletEdit.setAttribute("onClick", "PortletEdit()");
            bntPortletEdit.setAttribute("class", "Header");
            var editName = document.createTextNode("[Hiệu chỉnh]");
            bntPortletEdit.appendChild(editName);

            bntPortletDelete = document.createElement("a");
            bntPortletDelete.setAttribute("href", "javascript:void(0)");
            bntPortletDelete.setAttribute("onClick", "PortletDelete()");
            bntPortletDelete.setAttribute("class", "Header");
            var deleteName = document.createTextNode("[Xóa]");
            bntPortletDelete.appendChild(deleteName);
        }

        function pageLoad() {
            CreateElementPorletLinks();
            GetPortletList();
            SecurityUI();
        }
        function gvPortlet_Command(sender, args) {
            args.set_cancel(true);
            sender.get_masterTableView().clearSelectedItems();
            var currentPageIndex = sender.get_masterTableView().get_currentPageIndex();
            var pageSize = sender.get_masterTableView().get_pageSize();
            var sortExpressions = sender.get_masterTableView().get_sortExpressions();

            PageMethods.GetPortletList(currentPageIndex * pageSize, pageSize, sortExpressions.toString(), CallWebMethodSuccess);
            PageMethods.GetPortletCount(CallWebMethodSuccess);
        }
        function RadWindowPortletClose(sender, args) {
            if (!args.get_argument())
                return;
            switch (dialogMethod) {
                case "PortletNew":
                    {
                        PageMethods.PortletAdd(CallWebMethodSuccess, CallWebMethodFailed);
                    }
                    break;
                case "PortletEdit":
                    {
                        PageMethods.PortletUpdate(CallWebMethodSuccess, CallWebMethodFailed);
                    }
                    break;
            }
        }
        var Portlet_Add = 8;
        var Portlet_Edit = 9;
        var Portlet_Delete = 10;
        function SecurityUI() {
            PageMethods.GetPermission([Portlet_Add, Portlet_Edit, Portlet_Delete], CallWebMethodSuccess, CallWebMethodFailed);
        }
        function SecurityUICallback(results, context, methodName) {
            var arrPermission = Sys.Serialization.JavaScriptSerializer.deserialize(results);
            if (arrPermission[Portlet_Add]) {//btnTrashRestore
                document.getElementById("porletLinks").appendChild(bntPortletAdd);
            }

            if (arrPermission[Portlet_Edit]) {
                document.getElementById("porletLinks").appendChild(bntPortletEdit);
            }
            if (arrPermission[Portlet_Delete]) {
                document.getElementById("porletLinks").appendChild(bntPortletDelete);
            }

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindow ID="rwPortlet" runat="server" Modal="True" VisibleStatusbar="False" OnClientClose="RadWindowPortletClose">
    </telerik:RadWindow>
    <div style="width:815px; float:left; padding-left:16px; padding-top:25px;">
	    <div style="width:815px; float:left; margin-right:12px;">
    	    <div class="left_box"></div>
            <div style="width: 795px;" class="box"><div class="title_box" align="left"><%= Resources.Site.PortletManage %></div></div>
            <div class="right_box"></div>
            <div style="background-color:#edeeef; float:left; width:815px; padding-top:19px; padding-bottom:19px;">
                <div>
                    <button type="button" class="btn btn-success btn-xs" onclick="PortletNew()"><i class="fa fa-plus"></i> Thêm mới</button>
                    <button type="button" class="btn btn-success btn-xs" onclick="PortletEdit()"><i class="fa fa-edit"></i> Hiệu chỉnh</button>
                    <button type="button" class="btn btn-success btn-xs" onclick="PortletDelete()"><i class="fa fa-close"></i> Xóa</button>
                </div>
                <hr />
        	    <div id="porletLinks" style="display: none;">
        	       <%-- <a id="bntPortletAdd" href="javascript:void(0)" style="display: none" class="Header" onclick="PortletNew();">[<%= Resources.Site.AddItem %>]</a>
                    <a id="bntPortletEdit" href="javascript:void(0)" style="display: none" class="Header" onclick="PortletEdit();">[<%= Resources.Site.EditItem %>]</a>      
                    <a id="bntPortletDelete" href="javascript:void(0)" style="display: none" class="Header" onclick="PortletDelete();">[<%= Resources.Site.DeleteItem %>]</a>--%>
        	    </div>
        	    <div>
        	        <telerik:radgrid  id="gvPortlet" runat="server" autogeneratecolumns="False" allowpaging="True" allowsorting="True" gridlines="None" AllowMultiRowSelection="True" HeaderStyle-HorizontalAlign="Center">     
                        <HeaderContextMenu EnableAutoScroll="True"></HeaderContextMenu>
                        <MasterTableView ClientDataKeyNames="Id" AllowMultiColumnSorting="True">
                            <Columns>
                                 <telerik:GridClientSelectColumn  ItemStyle-HorizontalAlign="Center">
                                      <HeaderStyle Width="5%"/>                                                          
                                </telerik:GridClientSelectColumn> 
					            <telerik:GridBoundColumn HeaderText="<%$ Resources:Site,Name %>" DataField="Name" ItemStyle-HorizontalAlign="Center">					                                    							
                                     <HeaderStyle Width="23%"/>
					            </telerik:GridBoundColumn>
					            <telerik:GridBoundColumn HeaderText="<%$ Resources:Site,PathToFileDisplay %>" DataField="DisplayURL" ItemStyle-HorizontalAlign="Center">                        												                
                                     <HeaderStyle Width="45%"/>
					            </telerik:GridBoundColumn>
					            <telerik:GridBoundColumn HeaderText="<%$ Resources:Site,PathToFileModify %>" DataField="EditURL" ItemStyle-HorizontalAlign="Center">                        												              
                                     <HeaderStyle Width="27%"/>
					            </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>  
                        <ClientSettings EnableRowHoverStyle="true">                                          
                            <Selecting AllowRowSelect="True" />
                            <ClientEvents OnCommand="gvPortlet_Command" />                
                            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                        </ClientSettings>                  
                    </telerik:radgrid>
        	    </div>
            </div>
            <div class="bottombox_left">&nbsp;</div>
            <div style="width: 795px;" class="bottombox_center">&nbsp;</div>
            <div class="bottombox_right">&nbsp;</div>      
        </div>
    </div>   
</asp:Content>
