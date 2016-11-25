<%@ Page EnableViewState="false" Title="<%# Resources.Site.ChangeUser %>" Language="C#" MasterPageFile="~/Systems/DialogMasterPage.Master" AutoEventWireup="true" CodeBehind="UserInRoleManage.aspx.cs" Inherits="PSCPortal.Systems.Security.UserInRoleManage" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="../CSS/MasterPage.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
    //nam giu role hien hanh
    var currentRoleId;
    
    function CallWebMethodSuccess(results, context, methodName) {
        switch (methodName) {
            case "GetUserList":
                {
                    GetUserListCallback(results, context, methodName);
                }
                break;
            case "GetUserNotInRoleList":
                {
                    GetUserNotInRoleListCallback(results, context, methodName);
                }
                break;
            case "GetUserCount":
                {
                    GetUserCountCallback(results, context, methodName);
                }
                break;
            case "UserNew":
                {
                    UserNewCallback(results, context, methodName);
                }
                break;
            case "AddUser":
                {
                    UserAddCallback(results, context, methodName);
                }
                break;
            case "UserEdit":
                {
                    UserEditCallback(results, context, methodName);
                }
                break;
            case "UserUpdate":
                {
                    UserUpdateCallback(results, context, methodName);
                }
                break;
            case "UserDelete":
                {
                    UserDeleteCallback(results, context, methodName);
                }
                break;
            case "Save":
                {
                    SaveCallBack(results, context, methodName);
                }
        }
    }
    function GetUserSelect() {
        var items = $find("<%=gvUser.ClientID%>").get_masterTableView().get_selectedItems();

        var objList = new Array();
        for (var i = 0; i < items.length; i++) {
            Array.add(objList, items[i].get_dataItem().Id);
        }
        return objList;
    }
    function CallWebMethodFailed(results, context, methodName) {
        alert(results._message);
    }

    function GetUserNotInRoleList() {
        PageMethods.GetUserNotInRoleList(CallWebMethodSuccess,CallWebMethodFailed);
    }

    function GetUserNotInRoleListCallback(results, context, methodName) {
        var radListBox = $find("<%=radUsersNotInRole.ClientID%>");
        var users = Sys.Serialization.JavaScriptSerializer.deserialize(results);
        radListBox.get_items().clear();
        
        for (var i = 0; i < users.length; i++) {
            var item = new Telerik.Web.UI.RadListBoxItem();
            item.set_text(users[i].Name);
            item.set_value(users[i].Id);

            radListBox.get_items().add(item);
        }
    }


    function GetUserList() {
        var tableView = $find("<%=gvUser.ClientID%>").get_masterTableView();
        var sortExpressions = tableView.get_sortExpressions();
        PageMethods.GetUserList(0, tableView.get_pageSize(), sortExpressions.toString(), CallWebMethodSuccess);
        PageMethods.GetUserCount(CallWebMethodSuccess);
    }
    
    function GetUserListCallback(results, context, methodName) {
        var lastIndex = results.lastIndexOf('_');
        currentRoleId = results.substring(lastIndex + 1);
        results = results.substring(0, lastIndex);
        
        var grid = $find("<%=gvUser.ClientID%>");
        var tableView = grid.get_masterTableView();
        tableView.set_dataSource(Sys.Serialization.JavaScriptSerializer.deserialize(results));
        tableView.dataBind();
    }

    function GetUserCountCallback(results, context, methodName) {
        var tableView = $find("<%=gvUser.ClientID%>").get_masterTableView();
        tableView.set_virtualItemCount(results);
    }

    function UserNew() {
        PageMethods.UserNew(CallWebMethodSuccess, CallWebMethodFailed);
    }
    function UserNewCallback(results, context, methodName) {     
        GetUserList();
    }

    function AddUser() {
        var objList = new Array();
        var radListBox = $find("<%=radUsersNotInRole.ClientID%>");
        var items = radListBox.get_checkedItems();
        for (var i = 0; i < items.length; i++) {
            Array.add(objList, items[i].get_value());
            items[i].set_checked(false);
        }
        PageMethods.AddUser(objList, CallWebMethodSuccess, CallWebMethodFailed);
    }
    
    function UserAddCallback(result, context, methodName) {
        GetUserList();
        GetUserNotInRoleList();
    }

    

    function UserDelete() {       
        var objList = GetUserSelect();
        if (objList.length == 0) {
            alert("<%= Resources.Site.UserNotChoose %>");
            return;
        }
        PageMethods.UserDelete(objList, CallWebMethodSuccess, CallWebMethodFailed);
    }
    function UserDeleteCallback(results, context, methodName) {
        GetUserList();
        GetUserNotInRoleList();
        $find("<%=gvUser.ClientID%>").get_masterTableView().clearSelectedItems();
    }
    function pageLoad() {
        GetUserNotInRoleList();
        GetUserList();
    }
    function gvUser_Command(sender, args) {
        args.set_cancel(true);
        sender.get_masterTableView().clearSelectedItems();
        var currentPageIndex = sender.get_masterTableView().get_currentPageIndex();
        var pageSize = sender.get_masterTableView().get_pageSize();
        var sortExpressions = sender.get_masterTableView().get_sortExpressions();

        PageMethods.GetUserList(currentPageIndex * pageSize, pageSize, sortExpressions.toString(), CallWebMethodSuccess);
        PageMethods.GetUserCount(CallWebMethodSuccess);
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
        oWnd.close(true); 
    }

    ////////////////////////

    function SetSettingsOfTransfer() {
        var radListBox = $find("<%=radUsersNotInRole.ClientID%>");
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
        var radListBox = $find("<%=radUsersNotInRole.ClientID%>");

        var items = radListBox.get_checkedItems();
        if (items.length > 0) {
            document.getElementById('nextImg').setAttribute('src', 'Images/NextBlue.JPG');
        }
    }

    function SetSettingsOfTransferOnMouseOut() {
        var radListBox = $find("<%=radUsersNotInRole.ClientID%>");
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
        var radGrid = $find("<%=gvUser.ClientID%>");
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
        var radGrid = $find("<%=gvUser.ClientID%>");

        var items = radGrid.get_masterTableView().get_selectedItems();
        if (items.length > 0) {
            document.getElementById('prevImg').setAttribute('src', 'Images/bluePrev.JPG');
        }
    }

    function SetSettingsOfTransferOnMouseOutPrev() {
        var radGrid = $find("<%=gvUser.ClientID%>");
        var items = radGrid.get_masterTableView().get_selectedItems();
        if (items.length > 0) {
            document.getElementById('prevImg').setAttribute('src', 'Images/Prevblack.JPG');
        }
        else {
            document.getElementById('prevImg').setAttribute('src', 'Images/PrevOut.JPG');
        }
    }
    
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table style="width:100%;" align="center" cellpadding="4px;">
	<tr>
    	<td style="width: 40%;" align="right">
            <div style="width:208px; float: right; margin-right:12px;">
	            <div class="left_box"></div>
                <div style="width: 188px;" class="box"><div class="title_box" align="left"><%# Resources.Site.UserNotBelongGroup %></div></div>
                <div class="right_box"></div>
                <div style="background-color:#edeeef; float:left; width:208px; padding-top:19px; padding-bottom:19px;">
                    <div align="left">
    	            <telerik:RadListBox ID="radUsersNotInRole" Width="100%" runat="server" CheckBoxes="true" OnClientItemChecked="SetSettingsOfTransfer" Height="200px">       
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
                <img id="nextImg" alt="" onclick="AddUser();" onmouseout="SetSettingsOfTransferOnMouseOut();" onmouseover="SetSettingsOfTransferOnMouseOver();" src="Images/NextOut.JPG" />
                <img id="prevImg" alt="" onclick="UserDelete();" onmouseout="SetSettingsOfTransferOnMouseOutPrev();" onmouseover="SetSettingsOfTransferOnMouseOverPrev();" src="Images/PrevOut.JPG" />
            </div>
    	</td>
    	<td style="width: 50%;" align="left">
    	    <div style="width:408px; float:left;">
	            <div class="left_box"></div>
                <div style="width: 388px;" class="box"><div class="title_box" align="left"><%# Resources.Site.UserBelongGroup %></div></div>
                <div class="right_box"></div>
                <div style="background-color:#edeeef; float:left; width:408px; padding-top:19px; padding-bottom:19px;">
                    <div align="left">
    	                <telerik:radgrid id="gvUser" Height="200px" runat="server" autogeneratecolumns="False" allowpaging="True" allowsorting="True" gridlines="None" AllowMultiRowSelection="True">    
                            <HeaderContextMenu EnableAutoScroll="True"></HeaderContextMenu>
                            <MasterTableView ClientDataKeyNames="Id" AllowMultiColumnSorting="True">
                                <Columns>
                                     <telerik:GridClientSelectColumn>                     
                                        <ItemStyle Width="20px" />
                                    </telerik:GridClientSelectColumn> 
				                    <telerik:GridBoundColumn HeaderText="<%$ Resources:Site,Name %>" DataField="Name">                        							
				                        <ItemStyle Width="380px" />
				                    </telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>  
                            <ClientSettings EnableRowHoverStyle="true">                                          
                                <Selecting AllowRowSelect="True" />
                                <ClientEvents OnCommand="gvUser_Command" OnRowSelected="SetSettingsOfTransferPrev" OnRowDeselected="SetSettingsOfTransferPrev" />                
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
                    <td style="width:50%;"><a href="javascript:void(0)" onclick="Save();" class="submit"><%# Resources.Site.Save %></a></td>
                    <td style="width:50%;"><a href="javascript:void(0)" onclick="Cancel();" class="submit"><%# Resources.Site.Cancel %></a></td>
                </tr>
            </table>
        </td>
    </tr>
</table>       
</asp:Content>
