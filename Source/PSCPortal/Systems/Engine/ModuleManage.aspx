<%@ Page EnableViewState="false" Title="<%# Resources.Site.ModuleManage %>" Language="C#" MasterPageFile="~/Systems/MasterPage.Master" AutoEventWireup="true" CodeBehind="ModuleManage.aspx.cs" Inherits="PSCPortal.Systems.Engine.ModuleManage" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <script src="../../Scripts/Utility.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        var dialogMethod;
        function CallWebMethodSuccess(results, context, methodName) {
            switch (methodName) {
                case "GetModuleList":
                    {
                        GetModuleListCallback(results, context, methodName);
                    }
                    break;
                case "GetModuleCount":
                    {
                        GetModuleCountCallback(results, context, methodName);
                    }
                    break;
                case "ModuleNew":
                    {
                        ModuleNewCallback(results, context, methodName);
                    }
                    break;
                case "ModuleAdd":
                    {
                        ModuleAddCallback(results, context, methodName);
                    }
                    break;
                case "ModuleEdit":
                    {
                        ModuleEditCallback(results, context, methodName);
                    }
                    break;
                case "ModuleUpdate":
                    {
                        ModuleUpdateCallback(results, context, methodName);
                    }
                    break;                
                case "ModuleDelete":
                    {
                        ModuleDeleteCallback(results, context, methodName);
                    }
                    break;
                  case "GetPermission":                    
                  {
                        SecurityUICallback(results, context, methodName);
                  }
                  break;
            }
        }
        function GetModuleSelect() {
            var items = $find("<%=gvModule.ClientID%>").get_masterTableView().get_selectedItems();            

            var objList = new Array();
            for (var i = 0; i < items.length; i++) {
                Array.add(objList, items[i].get_dataItem().Id);
            }
            return objList;
        }
        function CallWebMethodFailed(results, context, methodName) {
            alert(results._message);
        }

        function GetModuleList() {
            var tableView = $find("<%=gvModule.ClientID%>").get_masterTableView();
            var sortExpressions = tableView.get_sortExpressions();
            PageMethods.GetModuleList(0, tableView.get_pageSize(), sortExpressions.toString(), CallWebMethodSuccess);
            PageMethods.GetModuleCount(CallWebMethodSuccess);
        }
        function GetModuleListCallback(results, context, methodName) {
            var grid = $find("<%=gvModule.ClientID%>");
            var tableView = grid.get_masterTableView();
            tableView.set_dataSource(Sys.Serialization.JavaScriptSerializer.deserialize(results));
            tableView.dataBind();
        }
        
        function GetModuleCountCallback(results, context, methodName) {
            var tableView = $find("<%=gvModule.ClientID%>").get_masterTableView();
            tableView.set_virtualItemCount(results);
        }

        function ModuleNew() {
            PageMethods.ModuleNew(CallWebMethodSuccess, CallWebMethodFailed);
        }
        function ModuleNewCallback(results, context, methodName) {
            dialogMethod = "ModuleNew";
            var oWnd = $find("<%= rwModule.ClientID %>");
            oWnd.setSize(800, 400);//Ngọc -17122015
            oWnd.setUrl("ModuleDetail.aspx");
            oWnd.show();            
        }
        function ModuleAddCallback(result, context, methodName) {
            GetModuleList();
        }

        function ModuleEdit() {
            var items = $find("<%=gvModule.ClientID%>").get_masterTableView().get_selectedItems();
            if (items.length == 0) {
                radalert("<%= Resources.Site.ModuleNotChoose %>", 250, 100, "<%= Resources.Site.Warning %>");
                return;
            }
            var obj = items[0].get_dataItem();
            PageMethods.ModuleEdit(obj.Id, CallWebMethodSuccess, CallWebMethodFailed);
        }
        function ModuleEditCallback(results, context, methodName) {
            dialogMethod = "ModuleEdit";
            var oWnd = $find("<%= rwModule.ClientID %>");
            oWnd.setSize(800, 400);//Ngọc -17122015
            oWnd.setUrl("ModuleDetail.aspx");
            oWnd.show();
        }
        
        function ModuleUpdateCallback(results, context, methodName) {
            GetModuleList();
        }
        
        function ModuleDelete() {			
            var objList = GetModuleSelect();
            if (objList.length == 0) {
                radalert("<%= Resources.Site.ModuleNotChoose %>", 250, 100, "<%= Resources.Site.Warning %>");
                return;
            }
            radconfirm("<%= Resources.Site.ModuleConfirmDelete %>", ModuleDeleteConfirm, 250, 100, null, "<%= Resources.Site.Warning %>");                 
        }
        function ModuleDeleteConfirm(arg) {
            if (!arg)
                return;
            var objList = GetModuleSelect();
            PageMethods.ModuleDelete(objList, CallWebMethodSuccess, CallWebMethodFailed);
        }
        function ModuleDeleteCallback(results, context, methodName) {
            GetModuleList();            
			$find("<%=gvModule.ClientID%>").get_masterTableView().clearSelectedItems();
        }
        var btnModuleAdd;
        var btnModuleEdit;
        var btnModuleDelete;
        function CreateElementModuleWebLinks() {
            btnModuleAdd = document.createElement("a");
            btnModuleAdd.setAttribute("href", "javascript:void(0)");
            btnModuleAdd.setAttribute("onClick", "ModuleNew()");
            btnModuleAdd.setAttribute("class", "Header");
            var addName = document.createTextNode("[Thêm mới]");
            btnModuleAdd.appendChild(addName);

            btnModuleEdit = document.createElement("a");
            btnModuleEdit.setAttribute("href", "javascript:void(0)");
            btnModuleEdit.setAttribute("onClick", "ModuleEdit()");
            btnModuleEdit.setAttribute("class", "Header");
            var editName = document.createTextNode("[Hiệu chỉnh]");
            btnModuleEdit.appendChild(editName);

            btnModuleDelete = document.createElement("a");
            btnModuleDelete.setAttribute("href", "javascript:void(0)");
            btnModuleDelete.setAttribute("onClick", "ModuleDelete()");
            btnModuleDelete.setAttribute("class", "Header");
            var deleteName = document.createTextNode("[Xóa]");
            btnModuleDelete.appendChild(deleteName);
        }
        function pageLoad() {
            GetModuleList();
            CreateElementModuleWebLinks();
            SecurityUI();
        }
        function gvModule_Command(sender, args) {
            args.set_cancel(true);
            sender.get_masterTableView().clearSelectedItems();
            var currentPageIndex = sender.get_masterTableView().get_currentPageIndex();
            var pageSize = sender.get_masterTableView().get_pageSize();
            var sortExpressions = sender.get_masterTableView().get_sortExpressions();

            PageMethods.GetModuleList(currentPageIndex * pageSize, pageSize, sortExpressions.toString(), CallWebMethodSuccess);
            PageMethods.GetModuleCount(CallWebMethodSuccess);
        }
        function RadWindowModuleClose(sender, args) {
            if (!args.get_argument())
                return;
            switch (dialogMethod) {
                case "ModuleNew":
                    {
                        PageMethods.ModuleAdd(CallWebMethodSuccess, CallWebMethodFailed);
                    }
                    break;
                case "ModuleEdit":
                    {
                        PageMethods.ModuleUpdate(CallWebMethodSuccess, CallWebMethodFailed);
                    }
                    break;                
            }
        }
        var Module_Add = 5;
        var Module_Edit = 6;
        var Module_Delete = 7;

   
        function SecurityUI() {
            PageMethods.GetPermission([Module_Add, Module_Edit, Module_Delete], CallWebMethodSuccess, CallWebMethodFailed);
        }
        function SecurityUICallback(results, context, methodName) {
            var arrPermission = Sys.Serialization.JavaScriptSerializer.deserialize(results);
            //if (arrPermission[Module_Add]) {//btnTrashRestore
            //    DisplayElement($get("bntModuleAdd"));
            //}

            //if (arrPermission[Module_Edit]) {
            //    DisplayElement($get("bntModuleEdit"));
            //}
            //if (arrPermission[Module_Delete]) {
            //    DisplayElement($get("bntModuleDelete"));
            //}
            if (arrPermission[Module_Add]) {//btnTrashRestore
                document.getElementById("moduleLinks").appendChild(btnModuleAdd);
            }
            if (arrPermission[Module_Edit]) {
                document.getElementById("moduleLinks").appendChild(btnModuleEdit);
            }
            if (arrPermission[Module_Delete]) {
                document.getElementById("moduleLinks").appendChild(btnModuleDelete);
            }
        }            
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindow ID="rwModule" runat="server" Modal="True" VisibleStatusbar="False" OnClientClose="RadWindowModuleClose">
    </telerik:RadWindow>
    <div style="width:815px; float:left; padding-left:16px; padding-top:25px;">
	    <div style="width:815px; float:left; margin-right:12px;">
    	    <div class="left_box"></div>
            <div style="width: 795px;" class="box"><div class="title_box" align="left"><%= Resources.Site.ModuleManage %></div></div>
            <div class="right_box"></div>
            <div style="background-color:#edeeef; float:left; width:815px; padding-top:19px; padding-bottom:19px;">
                <div>
                    <button type="button" class="btn btn-success btn-xs" onclick="ModuleNew()"><i class="fa fa-plus"></i> Thêm mới</button>
                    <button type="button" class="btn btn-success btn-xs" onclick="ModuleEdit()"><i class="fa fa-pencil"></i> Hiệu chỉnh</button>
                    <button type="button" class="btn btn-success btn-xs" onclick="ModuleDelete()"><i class="fa fa-close"></i> Xóa</button>
                </div>
                <hr />
        	    <div id="moduleLinks" style="display: none;">
<%--                    <a id="bntModuleAdd" href="javascript:void(0)"  class="Header" onclick="ModuleNew();">[<%= Resources.Site.AddItem %>]</a>
                    <a id="bntModuleEdit" href="javascript:void(0)" class="Header" onclick="ModuleEdit();">[<%= Resources.Site.EditItem %>]</a>      
                    <a id="bntModuleDelete" href="javascript:void(0)" class="Header" onclick="ModuleDelete();">[<%= Resources.Site.DeleteItem %>]</a>--%>
                </div>
	            <div>
                    <telerik:radgrid id="gvModule" Height="350px" runat="server" autogeneratecolumns="False" allowpaging="True" allowsorting="True" gridlines="None" AllowMultiRowSelection="True">    
                        <HeaderContextMenu EnableAutoScroll="True"></HeaderContextMenu>
                        <MasterTableView ClientDataKeyNames="Id" AllowMultiColumnSorting="True">
                            <Columns>
                                 <telerik:GridClientSelectColumn>                        
                                    <HeaderStyle Width="3%"/>
                                </telerik:GridClientSelectColumn> 
					            <telerik:GridBoundColumn  HeaderText="<%$ Resources:Site,Name %>" DataField="Name" >                        							
					                <HeaderStyle Width="15%"/>
					            </telerik:GridBoundColumn>
					            <telerik:GridBoundColumn HeaderText="<%$ Resources:Site,PathToFileDisplay %>" DataField="DisplayURL" >                        							
					                <HeaderStyle Width="50%"/>
					            </telerik:GridBoundColumn>
					            <telerik:GridBoundColumn HeaderText="<%$ Resources:Site,PathToFileModify %>" DataField="EditURL" Visible="false" >                        							
					               <HeaderStyle Width="30%"/>
					            </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>  
                        <ClientSettings EnableRowHoverStyle="true">                                          
                            <Selecting AllowRowSelect="True" />
                            <ClientEvents OnCommand="gvModule_Command" />                
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
