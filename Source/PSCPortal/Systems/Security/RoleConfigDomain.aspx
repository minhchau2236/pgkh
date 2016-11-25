<%@ Page Title="" Language="C#" MasterPageFile="~/Systems/DialogMasterPage.Master"
    AutoEventWireup="true" CodeBehind="RoleConfigDomain.aspx.cs" Inherits="PSCPortal.Systems.Security.RoleConfigDomain" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">
        function CallWebMethodSuccess(results, context, methodName) {
            switch (methodName) {
                case "GetSubDomainList":
                    {
                        GetSubDomainListCallback(results, context, methodName);
                    }
                    break;
                case "Save":
                    {
                        SaveCallBack(results, context, methodName);
                    }
            }
        }
        function pageLoad() {
            GetSubDomainList();
        }
        function Save() {
            var objSubDomainList = GetSubDomainSelect();
            PageMethods.Save(objSubDomainList, CallWebMethodSuccess, CallWebMethodFailed)
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
        function GetSubDomainSelect() {
            var items = $find("<%=gvSubDomain.ClientID%>").get_masterTableView()._dataItems;
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

        function GetSubDomainList() {
            PageMethods.GetSubDomainList(CallWebMethodSuccess);
        }
        function GetSubDomainListCallback(results, context, methodName) {
            var grid = $find("<%=gvSubDomain.ClientID%>");
            var tableView = grid.get_masterTableView();
            tableView.set_dataSource(Sys.Serialization.JavaScriptSerializer.deserialize(results));
            tableView.dataBind();
        }
        function gvSubDomain_Command(sender, args) {
            args.set_cancel(true);
            sender.get_masterTableView().clearSelectedItems();
            PageMethods.GetSubDomainList(CallWebMethodSuccess);
        }
        function gvSubDomain_RowDataBound(sender, args) {
            var item = args.get_item();
            var dataItem = item.get_dataItem();
            var checkbox = item._element.cells[0].children[0];
            if (dataItem.IsCheck)
                item.set_selected(true);
            else
                item.set_selected(false);
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="clear: both; float: left; padding: 15px;">
        <telerik:RadGrid ID="gvSubDomain" Height="450px" Width="750px" runat="server" AutoGenerateColumns="False"
            AllowPaging="false" AllowSorting="True" GridLines="None">
            <HeaderContextMenu EnableAutoScroll="True">
            </HeaderContextMenu>
            <MasterTableView ClientDataKeyNames="Id" AllowMultiColumnSorting="True" AllowPaging="false">
                <Columns>
                    <telerik:GridClientSelectColumn HeaderStyle-Width="50px">
                    </telerik:GridClientSelectColumn>
                    <telerik:GridBoundColumn HeaderText="<%$ Resources:Site,Name %>" DataField="Name">
                        <ItemStyle Width="650px" HorizontalAlign="Left" />
                    </telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
                <ClientEvents OnCommand="gvSubDomain_Command" OnRowDataBound="gvSubDomain_RowDataBound" />
                <Scrolling AllowScroll="True" UseStaticHeaders="True" />
            </ClientSettings>
        </telerik:RadGrid>
    </div>
    <table style="width: 100%;" align="center" cellpadding="4px;">
        <tr align="center">
            <td colspan="2">
                <img src="/Systems/CMS/Image/line.jpg" width="438" height="1" alt="" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table style="width: 15%" align="center">
                    <tr>
                        <td style="width: 50%;">
                            <a href="javascript:void(0)" onclick="Save();" class="submit">
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
