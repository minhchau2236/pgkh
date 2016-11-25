<%@ Page EnableViewState="false" Title="<%# Resources.Site.ChangeGroup %>" Language="C#" MasterPageFile="~/Systems/DialogMasterPage.Master" AutoEventWireup="true" CodeBehind="RolesOfUser.aspx.cs" Inherits="PSCPortal.Systems.Security.RolesOfUser" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Systems/CSS/MasterPage.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
    //nam giu role hien hanh
    var currentUserId;
    
    function CallWebMethodSuccess(results, context, methodName) {
        switch (methodName) {
            case "GetRoleList":
                {
                    GetRoleListCallback(results, context, methodName);
                }
                break;
            case "GetRoleNotOfUserList":
                {
                    GetRoleNotOfUserListCallback(results, context, methodName);
                }
                break;
            case "GetRoleCount":
                {
                    GetRoleCountCallback(results, context, methodName);
                }
                break;
            case "RoleNew":
                {
                    RoleNewCallback(results, context, methodName);
                }
                break;
            case "AddRole":
                {
                    RoleAddCallback(results, context, methodName);
                }
                break;
            case "RoleEdit":
                {
                    RoleEditCallback(results, context, methodName);
                }
                break;
            case "RoleUpdate":
                {
                    RoleUpdateCallback(results, context, methodName);
                }
                break;
            case "RoleDelete":
                {
                    RoleDeleteCallback(results, context, methodName);
                }
                break;
            case "Save":
                {
                    SaveCallBack(results, context, methodName);
                }
        }
    }
    function GetRoleSelect() {
        var items = $find("<%=gvRole.ClientID%>").get_masterTableView().get_selectedItems();

        var objList = new Array();
        for (var i = 0; i < items.length; i++) {
            Array.add(objList, items[i].get_dataItem().Id);
        }
        return objList;
    }
    function CallWebMethodFailed(results, context, methodName) {
        alert(results._message);
    }

    function GetRoleNotOfUserList() {

        PageMethods.GetRoleNotOfUserList(CallWebMethodSuccess, CallWebMethodFailed);
    }

    function GetRoleNotOfUserListCallback(results, context, methodName) {
        var radListBox = $find("<%=radRolesNotInRole.ClientID%>");
        var Roles = Sys.Serialization.JavaScriptSerializer.deserialize(results);
        radListBox.get_items().clear();
        
        for (var i = 0; i < Roles.length; i++) {
            var item = new Telerik.Web.UI.RadListBoxItem();
            item.set_text(Roles[i].Name);
            item.set_value(Roles[i].Id);

            radListBox.get_items().add(item);
        }
    }


    function GetRoleList() {
        var tableView = $find("<%=gvRole.ClientID%>").get_masterTableView();
        var sortExpressions = tableView.get_sortExpressions();
        PageMethods.GetRoleList(0, tableView.get_pageSize(), sortExpressions.toString(), CallWebMethodSuccess);
        PageMethods.GetRoleCount(CallWebMethodSuccess);
    }
    
    function GetRoleListCallback(results, context, methodName) {
        var lastIndex = results.lastIndexOf('_');
        currentUserId = results.substring(lastIndex + 1);
        results = results.substring(0, lastIndex);
        
        var grid = $find("<%=gvRole.ClientID%>");
        var tableView = grid.get_masterTableView();
        tableView.set_dataSource(Sys.Serialization.JavaScriptSerializer.deserialize(results));
        tableView.dataBind();
    }

    function GetRoleCountCallback(results, context, methodName) {
        var tableView = $find("<%=gvRole.ClientID%>").get_masterTableView();
        tableView.set_virtualItemCount(results);
    }

    function RoleNew() {
        PageMethods.RoleNew(CallWebMethodSuccess, CallWebMethodFailed);
    }
    function RoleNewCallback(results, context, methodName) {     
        GetRoleList();
    }

    function AddRole() {
        var objList = new Array();
        var radListBox = $find("<%=radRolesNotInRole.ClientID%>");
        var items = radListBox.get_checkedItems();
        for (var i = 0; i < items.length; i++) {
            Array.add(objList, items[i].get_value());
            items[i].set_checked(false);
        }
        PageMethods.AddRole(objList, CallWebMethodSuccess, CallWebMethodFailed);
    }
    
    function RoleAddCallback(result, context, methodName) {
        GetRoleList();
        GetRoleNotOfUserList();
    }

    function SetSettingsOfTransfer() {
        var radListBox = $find("<%=radRolesNotInRole.ClientID%>");
        if (radListBox == null)
            return;
        var items = radListBox.get_checkedItems();
        if (items.length > 0) {
            document.getElementById('nextImg').setAttribute('src', 'Images/Nextblack.JPG');
        }
        else {
            document.getElementById('nextImg').setAttribute('src', 'Images/NextOut.JPG');
        }
    }

    function SetSettingsOfTransferOnMouseOver() {
        var radListBox = $find("<%=radRolesNotInRole.ClientID%>");

        var items = radListBox.get_checkedItems();
        if (items.length > 0) {
            document.getElementById('nextImg').setAttribute('src', 'Images/NextBlue.JPG');
        }
    }

    function SetSettingsOfTransferOnMouseOut() {
        var radListBox = $find("<%=radRolesNotInRole.ClientID%>");
        var items = radListBox.get_checkedItems();

        if (items.length > 0) {
            document.getElementById('nextImg').setAttribute('src', 'Images/Nextblack.JPG');
        }
        else {
            document.getElementById('nextImg').setAttribute('src', 'Images/NextOut.JPG');
        }
    }

    /////////////////////////

    function SetSettingsOfTransferPrev() {
        var radGrid = $find("<%=gvRole.ClientID%>");
        if (radGrid == null)
            return;
        var items = radGrid.get_masterTableView().get_selectedItems();
        if (items.length > 0) {
            document.getElementById('prevImg').setAttribute('src', 'Images/Prevblack.JPG');
        }
        else {
            document.getElementById('prevImg').setAttribute('src', 'Images/PrevOut.JPG');
        }
    }

    function SetSettingsOfTransferOnMouseOverPrev() {
        var radGrid = $find("<%=gvRole.ClientID%>");

        var items = radGrid.get_masterTableView().get_selectedItems();
        if (items.length > 0) {
            document.getElementById('prevImg').setAttribute('src', 'Images/bluePrev.JPG');
        }
    }

    function SetSettingsOfTransferOnMouseOutPrev() {
        var radGrid = $find("<%=gvRole.ClientID%>");
        var items = radGrid.get_masterTableView().get_selectedItems();
        if (items.length > 0) {
            document.getElementById('prevImg').setAttribute('src', 'Images/Prevblack.JPG');
        }
        else {
            document.getElementById('prevImg').setAttribute('src', 'Images/PrevOut.JPG');
        }
    }
    
    /////////////////////////////////////////////////
    function RoleDelete() {        
        var objList = GetRoleSelect();
        if (objList.length == 0) {
            alert("<%= Resources.Site.GroupNotChoose %>");
            return;
        }
        PageMethods.RoleDelete(objList, CallWebMethodSuccess, CallWebMethodFailed);
    }
    function RoleDeleteCallback(results, context, methodName) {
        GetRoleList();
        GetRoleNotOfUserList();
        $find("<%=gvRole.ClientID%>").get_masterTableView().clearSelectedItems();
    }
    function pageLoad() {
        GetRoleNotOfUserList();
        GetRoleList();
    }
    function gvRole_Command(sender, args) {
        args.set_cancel(true);
        sender.get_masterTableView().clearSelectedItems();
        var currentPageIndex = sender.get_masterTableView().get_currentPageIndex();
        var pageSize = sender.get_masterTableView().get_pageSize();
        var sortExpressions = sender.get_masterTableView().get_sortExpressions();

        PageMethods.GetRoleList(currentPageIndex * pageSize, pageSize, sortExpressions.toString(), CallWebMethodSuccess);
        PageMethods.GetRoleCount(CallWebMethodSuccess);
    }

    function Save() {
        PageMethods.Save(CallWebMethodSuccess, CallWebMethodFailed)
    }
    
    function SaveCallBack(results, context, methodName) {
        var oWnd = GetRadWindow();
        oWnd.close(true);  
    }
    function Cancel() {
        var oWnd = GetRadWindow();
        oWnd.close(false);  
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table style="width:100%;" align="center" cellpadding="4px;">
	<tr>
    	<td style="width: 40%;" align="right">
            <div style="width:208px; float: right; margin-right:12px;">
	            <div class="left_box"></div>
                <div style="width: 188px;" class="box"><div class="title_box" align="left"><%= Resources.Site.GroupNotBelongUser %></div></div>
                <div class="right_box"></div>
                <div style="background-color:#edeeef; float:left; width:208px; padding-top:19px; padding-bottom:19px;">
                    <div align="left">
    	                <telerik:RadListBox ID="radRolesNotInRole" Width="100%" runat="server" CheckBoxes="true" Height="200px" OnClientItemChecked="SetSettingsOfTransfer">
                            <ButtonSettings ReorderButtons="Common"></ButtonSettings>
                        </telerik:RadListBox>
                    </div>
                </div>
                <div class="bottombox_left">&nbsp;</div>
                <div style="width: 188px;" class="bottombox_center">&nbsp;</div>
                <div class="bottombox_right">&nbsp;</div>      
            </div>     	    
    	</td>    	
    	<td style="width: 20px;">
    	    <div style="float: left;">
                <img id="nextImg" alt="" onclick="AddRole();" onmouseout="SetSettingsOfTransferOnMouseOut();"  onmouseover="SetSettingsOfTransferOnMouseOver();" src="Images/NextOut.JPG" />
                <img id="prevImg" alt="" onclick="RoleDelete();" onmouseout="SetSettingsOfTransferOnMouseOutPrev();" onmouseover="SetSettingsOfTransferOnMouseOverPrev();" src="Images/PrevOut.JPG" />
            </div>
    	</td>
    	<td style="width: 50%;" align="left">
    	    <div style="width:408px; float:left;">
	            <div class="left_box"></div>
                <div style="width: 388px;" class="box"><div class="title_box" align="left"><%= Resources.Site.GroupBelongUser %></div></div>
                <div class="right_box"></div>
                <div style="background-color:#edeeef; float:left; width:408px; padding-top:19px; padding-bottom:19px;">
                    <div align="left">
    	                <telerik:radgrid id="gvRole" Height="250px" runat="server" autogeneratecolumns="False" allowpaging="True" allowsorting="True" gridlines="None" AllowMultiRowSelection="True">    
                            <HeaderContextMenu EnableAutoScroll="True"></HeaderContextMenu>
                            <MasterTableView ClientDataKeyNames="Id" AllowMultiColumnSorting="True">
                                <Columns>
                                     <telerik:GridClientSelectColumn>                                                                                            
                                    </telerik:GridClientSelectColumn> 
					                <telerik:GridBoundColumn HeaderText="<%$ Resources:Site,Name %>" DataField="Name">
					                    <ItemStyle Width="380px" />                        							
					                </telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>  
                            <ClientSettings EnableRowHoverStyle="true">                                          
                                <Selecting AllowRowSelect="True" />
                                <ClientEvents OnCommand="gvRole_Command" OnRowSelected="SetSettingsOfTransferPrev" OnRowDeselected="SetSettingsOfTransferPrev" />                                                
                                <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                            </ClientSettings>                  
                        </telerik:radgrid>
                    </div>
                </div>
                <div class="bottombox_left">&nbsp;</div>
                <div style="width: 388px;" class="bottombox_center">&nbsp;</div>
                <div class="bottombox_right">&nbsp;</div>      
            </div>  
    	</td>
    </tr>    
    <tr align="center">
        <td colspan="3"><img src="/Systems/CMS/Image/line.jpg" width="438" height="1" alt="" /></td>
    </tr>
    <tr>
        <td colspan="3">
            <table style="width:15%" align="center">
                <tr>
                    <td style="width:50%;"><a href="javascript:void(0)" onclick="Save();" class="submit"><%= Resources.Site.Save %></a></td>
                    <td style="width:50%;"><a href="javascript:void(0)" onclick="Cancel();" class="submit"><%= Resources.Site.Cancel %></a></td>
                </tr>
            </table>
        </td>
    </tr>
</table>        
</asp:Content>
