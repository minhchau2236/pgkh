<%@ Page Title="<%# Resources.Site.SubDomainConfig %>" Language="C#" MasterPageFile="~/Systems/DialogMasterPage.Master"
    AutoEventWireup="true" CodeBehind="SubDomainConfig.aspx.cs" Inherits="PSCPortal.Systems.Engine.SubDomainConfig" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <style type="text/css">           
        .submit-luu {
            padding: 5px 10px;
            margin-right: 10px;
            background-color: #698AC0;
            color: #fff;
            border-radius: 4px;
            border: 1px solid #698AC0;
        }

            .submit-luu:hover {
                background-color: #fff;
                text-decoration: none;
            }
        /*chuyên mục*/
        .RadTreeView{padding:10px 0 0 0; min-height:400px;}
    </style>
    <script language="javascript" type="text/javascript">
        function CallWebMethodSuccess(results, context, methodName) {
            switch (methodName) {
                case "GetMenuMasterList":
                    {
                        GetMenuMasterListCallback(results, context, methodName);
                    }
                    break;
                case "GetPageList":
                    {
                        GetPageListCallback(results, context, methodName);
                    }
                    break;
                case "GetRoleList":
                    {
                        GetRoleListCallback(results, context, methodName);
                    }
                    break;
                case "Save":
                    {
                        SaveCallBack(results, context, methodName);
                    }
            }
        }
        function pageLoad() {
            GetMenuMasterList();
            GetPageList();
            GetRoleList();
        }
        function Save() {
            var curNode = $find("<%= rtvTopic.ClientID %>").get_selectedNode();
            var valueNode;
            if (curNode == null)
                valueNode = "00000000-0000-0000-0000-000000000000";
            else
                valueNode = curNode.get_value();
            var objMenuMasterList = GetMenuMasterSelect();
            var objPageList = GetPageSelect();
            var objRoleList = GetRoleSelect();
            PageMethods.Save(valueNode, objMenuMasterList, objPageList,objRoleList, CallWebMethodSuccess, CallWebMethodFailed)
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
    <script language="javascript" type="text/javascript">
        function GetMenuMasterSelect() {
            var items = $find("<%=gvMenuMaster.ClientID%>").get_masterTableView()._dataItems;
            var objList = new Array();
            for (var i = 0; i < items.length; i++) {
                var item = items[i];
                if (item._element.cells[0].children[0].checked == true)
                    Array.add(objList, items[i]._dataItem.Id);
            }
            return objList;
        }
        function CallWebMethodFailed(results, context, methodName) {
            alert(results._message);
        }

        function GetMenuMasterList() {
            var tableView = $find("<%=gvMenuMaster.ClientID%>").get_masterTableView();
            PageMethods.GetMenuMasterList(CallWebMethodSuccess);
        }
        function GetMenuMasterListCallback(results, context, methodName) {
            var grid = $find("<%=gvMenuMaster.ClientID%>");
            var tableView = grid.get_masterTableView();
            tableView.set_dataSource(Sys.Serialization.JavaScriptSerializer.deserialize(results));
            tableView.dataBind();
        }
        function gvMenuMaster_Command(sender, args) {
            args.set_cancel(true);
            sender.get_masterTableView().clearSelectedItems();
            PageMethods.GetMenuMasterList(CallWebMethodSuccess);
        }
        function gvMenuMaster_RowDataBound(sender, args) {
            var item = args.get_item();
            var dataItem = item.get_dataItem();
            var checkbox = item._element.cells[0].children[0];
            if (dataItem.IsCheck) {
                checkbox.checked = "checked";
                item._element.className = "rgRow  rgSelectedRow";
            }
            else {
                item._element.className = "rgRow";
            }
        }
    </script>
    <script language="javascript" type="text/javascript">
        function GetPageSelect() {
            var items = $find("<%=gvPage.ClientID%>").get_masterTableView()._dataItems;
            var objList = new Array();
            for (var i = 0; i < items.length; i++) {
                var item = items[i];
                if (item._element.cells[0].children[0].checked == true)
                    Array.add(objList, items[i]._dataItem.Id);
            }
            return objList;
        }
        function CallWebMethodFailed(results, context, methodName) {
            alert(results._message);
        }

        function GetPageList() {
            var tableView = $find("<%=gvPage.ClientID%>").get_masterTableView();
            PageMethods.GetPageList(CallWebMethodSuccess);
        }
        function GetPageListCallback(results, context, methodName) {
            var grid = $find("<%=gvPage.ClientID%>");
            var tableView = grid.get_masterTableView();
            tableView.set_dataSource(Sys.Serialization.JavaScriptSerializer.deserialize(results));
            tableView.dataBind();
        }
        function gvPage_Command(sender, args) {
            args.set_cancel(true);
            sender.get_masterTableView().clearSelectedItems();
            PageMethods.GetPageList(CallWebMethodSuccess);
        }
        function gvPage_RowDataBound(sender, args) {
            var item = args.get_item();
            var dataItem = item.get_dataItem();
            var checkbox = item._element.cells[0].children[0];
            if (dataItem.IsCheck) {
                checkbox.checked = "checked";
                item._element.className = "rgRow  rgSelectedRow";
            }
            else {
                item._element.className = "rgRow";
            }
        }
    </script>
    <script language="javascript" type="text/javascript">
        function GetRoleSelect() {
            var items = $find("<%=gvRole.ClientID%>").get_masterTableView()._dataItems;
            var objList = new Array();
            for (var i = 0; i < items.length; i++) {
                var item = items[i];
                if (item._element.cells[0].children[0].checked == true)
                    Array.add(objList, items[i]._dataItem.Id);
            }
            return objList;
        }
        function CallWebMethodFailed(results, context, methodName) {
            alert(results._message);
        }

        function GetRoleList() {
            var tableView = $find("<%=gvRole.ClientID%>").get_masterTableView();
            PageMethods.GetRoleList(CallWebMethodSuccess);
        }
        function GetRoleListCallback(results, context, methodName) {
            var grid = $find("<%=gvRole.ClientID%>");
            var tableView = grid.get_masterTableView();
            tableView.set_dataSource(Sys.Serialization.JavaScriptSerializer.deserialize(results));
            tableView.dataBind();
        }
        function gvRole_Command(sender, args) {
            args.set_cancel(true);
            sender.get_masterTableView().clearSelectedItems();
            PageMethods.GetRoleList(CallWebMethodSuccess);
        }
        function gvRole_RowDataBound(sender, args) {
            var item = args.get_item();
            var dataItem = item.get_dataItem();
            var checkbox = item._element.cells[0].children[0];
            if (dataItem.IsCheck) {
                checkbox.checked = "checked";
                item._element.className = "rgRow  rgSelectedRow";
            }
            else {
                item._element.className = "rgRow";
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="padding:10px 0 0 10px">
    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="rmpSubDomain">
        <Tabs>
            <telerik:RadTab runat="server" Selected="true" PageViewID="rpvTopic" Text="Chuyên mục">
            </telerik:RadTab>
            <telerik:RadTab runat="server" PageViewID="rpvMenuMaster" Selected="True" Text="Liên kết">
            </telerik:RadTab>
            <telerik:RadTab runat="server" PageViewID="rpvPage" Text="Trang">
            </telerik:RadTab>
            <telerik:RadTab runat="server" PageViewID="rpvRole" Text="Nhóm quyền">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="rmpSubDomain" runat="server">
        <telerik:RadPageView Selected="true" ID="rpvTopic" runat="server">
            <telerik:RadTreeView ID="rtvTopic" runat="server" DataTextField="Name" DataValueField="Id"
                EnableDragAndDrop="False">
            </telerik:RadTreeView>
        </telerik:RadPageView>
        <telerik:RadPageView ID="rpvMenuMaster" runat="server">
            <div style="clear: both; float: left; padding: 10px 0;">
                <telerik:RadGrid ID="gvMenuMaster" Height="400px" Width="800px" runat="server" AutoGenerateColumns="False"
                    AllowPaging="false" AllowSorting="True" GridLines="None" AllowMultiRowSelection="True">
                    <HeaderContextMenu EnableAutoScroll="True">
                    </HeaderContextMenu>
                    <MasterTableView ClientDataKeyNames="Id" AllowMultiColumnSorting="True" AllowPaging="false">
                        <Columns>
                            <telerik:GridClientSelectColumn HeaderStyle-Width="50px">
                            </telerik:GridClientSelectColumn>
                            <telerik:GridBoundColumn HeaderText="<%$ Resources:Site,Name %>" DataField="Name">
                                <ItemStyle Width="700px" HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="True" />
                        <ClientEvents OnCommand="gvMenuMaster_Command" OnRowDataBound="gvMenuMaster_RowDataBound" />
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </div>
        </telerik:RadPageView>
        <telerik:RadPageView ID="rpvPage" runat="server">
            <div style="clear: both; float: left; padding: 10px 0;">
                <telerik:RadGrid ID="gvPage" Height="400px" Width="800px" runat="server" AutoGenerateColumns="False"
                    AllowPaging="false" AllowSorting="True" GridLines="None" AllowMultiRowSelection="True">
                    <HeaderContextMenu EnableAutoScroll="True">
                    </HeaderContextMenu>
                    <MasterTableView ClientDataKeyNames="Id" AllowMultiColumnSorting="True">
                        <Columns>
                            <telerik:GridClientSelectColumn HeaderStyle-Width="50px">
                            </telerik:GridClientSelectColumn>
                            <telerik:GridBoundColumn HeaderText="<%$ Resources:Site,Name %>" DataField="Name">
                                <ItemStyle Width="700px" HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="True" />
                        <ClientEvents OnCommand="gvPage_Command" OnRowDataBound="gvPage_RowDataBound" />
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </div>
        </telerik:RadPageView>
        <telerik:RadPageView ID="rpvRole" runat="server">
            <div style="clear: both; float: left; padding: 10px 0;">
                <telerik:RadGrid ID="gvRole" Height="400px" Width="800px" runat="server" AutoGenerateColumns="False"
                    AllowPaging="false" AllowSorting="True" GridLines="None" AllowMultiRowSelection="True">
                    <HeaderContextMenu EnableAutoScroll="True">
                    </HeaderContextMenu>
                    <MasterTableView ClientDataKeyNames="Id" AllowMultiColumnSorting="True">
                        <Columns>
                            <telerik:GridClientSelectColumn HeaderStyle-Width="50px">
                            </telerik:GridClientSelectColumn>
                            <telerik:GridBoundColumn HeaderText="<%$ Resources:Site,Name %>" DataField="Name">
                                <ItemStyle Width="700px" HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="True" />
                        <ClientEvents OnCommand="gvRole_Command" OnRowDataBound="gvRole_RowDataBound" />
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </div>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
   
    </div>    
     <table style="width: 100%;" align="center" cellpadding="4px;">
        <tr align="center">
            <td colspan="2">
                 <hr style="width: 600px;" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table style="width: 15%" align="center">
                    <tr>
                        <td style="width: 50%;">
                            <a href="javascript:void(0)" onclick="Save();" class="submit submit-luu">
                                <%= Resources.Site.Save %></a>
                        </td>
                        <td style="width: 50%;">
                            <a href="javascript:void(0)" onclick="Cancel();" class="submit">
                                <%= Resources.Site.Cancel %></a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
