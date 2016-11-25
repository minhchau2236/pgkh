<%@ Page Title="" Language="C#" MasterPageFile="~/Systems/MasterPage.Master" AutoEventWireup="true" CodeBehind="LayoutManage.aspx.cs" Inherits="PSCPortal.Systems.CMS.LayoutManage" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var oWnd, grid, dialogMethod;

        function init() {
            oWnd = $find("<%= rwLayout.ClientID %>");
            grid = $find("<%= gvLayout.ClientID %>");
        }
        var btnLinkAdd;
        var btnLinkEdit;
        var btnLinkDelete;
        function CreateElementLinkWebLinks() {
            btnLinkAdd = document.createElement("a");
            btnLinkAdd.setAttribute("href", "javascript:void(0)");
            btnLinkAdd.setAttribute("onClick", "LayoutNew()");
            btnLinkAdd.setAttribute("class", "Header");
            var addName = document.createTextNode("[Thêm mới]");
            btnLinkAdd.appendChild(addName);

            btnLinkEdit = document.createElement("a");
            btnLinkEdit.setAttribute("href", "javascript:void(0)");
            btnLinkEdit.setAttribute("onClick", "LayoutEdit()");
            btnLinkEdit.setAttribute("class", "Header");
            var editName = document.createTextNode("[Hiệu chỉnh]");
            btnLinkEdit.appendChild(editName);

            btnLinkDelete = document.createElement("a");
            btnLinkDelete.setAttribute("href", "javascript:void(0)");
            btnLinkDelete.setAttribute("onClick", "LayoutDelete()");
            btnLinkDelete.setAttribute("class", "Header");
            var deleteName = document.createTextNode("[Xóa]");
            btnLinkDelete.appendChild(deleteName);
        }
        function pageLoad() {
            init();
            CreateElementLinkWebLinks();
            SecurityUI();
            layoutList();
         
        }

        function layoutList() {
            var tableView = grid.get_masterTableView();
            var sortExpressions = tableView.get_sortExpressions();
            PageMethods.GetLayoutList(0, tableView.get_pageSize(), sortExpressions.toString(), layoutListCallBack);
            PageMethods.GetLayoutCount(layoutCountCallBack);
        }

        function layoutListCallBack(result) {
            var tableView = grid.get_masterTableView();
            tableView.set_dataSource(Sys.Serialization.JavaScriptSerializer.deserialize(result));
            tableView.dataBind();
        }
        function layoutCountCallBack(results) {
            var tableView = grid.get_masterTableView();
            tableView.set_virtualItemCount(results);
        }
        function LayoutNew() {
            PageMethods.LayoutNew(LayoutNewCallback);
        }

        function LayoutNewCallback(results) {
            dialogMethod = "LayoutNew";
            oWnd.setSize(700, 500);
            oWnd.setUrl("LayoutDetail.aspx");
            oWnd.show();
        }

        function LayoutEdit() {
            var items = grid.get_masterTableView().get_selectedItems();
            if (items.length == 0) {
                radalert("Bạn chưa chọn Layout", 250, 100, "Chú ý");
                return;
            }
            var obj = items[0].get_dataItem();
            PageMethods.LayoutEdit(obj.Id, LayoutEditCallBack);
        }

        function LayoutEditCallBack() {
            dialogMethod = "LayoutEdit";
            oWnd.setSize(700, 500);
            oWnd.setUrl("LayoutDetail.aspx");
            oWnd.show();
        }
        function GetLayoutSelect() {
            var items = grid.get_masterTableView().get_selectedItems();
            var objList = new Array();
            for (var i = 0; i < items.length; i++) {
                Array.add(objList, items[i].get_dataItem().Id);
            }
            return objList;
        }
        function LayoutDelete() {
            var objList = GetLayoutSelect();
            if (objList.length == 0) {
                radalert("Bạn chưa chọn Layout", 250, 100, "Chú ý");
                return;
            }
            radconfirm("Bạn có muốn xóa không?", LayoutDeleteConfirm, 250, 100, null, "Chú ý");
        }
        function LayoutDeleteConfirm(args) {
            var objList = GetLayoutSelect();
            if (!args)
                return;
            PageMethods.LayoutDelete(objList, layoutList);
            grid.get_masterTableView().clearSelectedItems();
        }

        function gvLayout_Command(sender, args) {
            args.set_cancel(true);
            sender.get_masterTableView().clearSelectedItems();
            var currentPageIndex = sender.get_masterTableView().get_currentPageIndex();
            var pageSize = sender.get_masterTableView().get_pageSize();
            var sortExpressions = sender.get_masterTableView().get_sortExpressions();
            PageMethods.GetLayoutList(currentPageIndex * pageSize, pageSize, sortExpressions.toString(), layoutListCallBack);
        }

        function RadWindowLayoutClose(sender, args) {
            if (!args.get_argument())
                return;
            switch (dialogMethod) {
                case "LayoutNew":
                    {
                        PageMethods.LayoutAdd(layoutList);
                    }
                    break;
                case "LayoutEdit":
                    {
                        PageMethods.LayoutUpdate(layoutList);
                    }
                    break;
            }
        }
        var Layout_Add = 62;
        var Layout_Edit = 63;
        var Layout_Delete = 64;
        function SecurityUI() {
            PageMethods.GetPermission([Layout_Add, Layout_Edit, Layout_Delete], SecurityUICallback);
        }
        function SecurityUICallback(results, context, methodName) {
            var arrPermission = Sys.Serialization.JavaScriptSerializer.deserialize(results);
            if (arrPermission[Layout_Add]) {
                document.getElementById("layoutLinks").appendChild(btnLinkAdd);
            }
            if (arrPermission[Layout_Edit]) {
                document.getElementById("layoutLinks").appendChild(btnLinkEdit);
            }
            if (arrPermission[Layout_Delete]) {
                document.getElementById("layoutLinks").appendChild(btnLinkDelete);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindow ID="rwLayout" runat="server" Modal="True" VisibleStatusbar="False" OnClientClose="RadWindowLayoutClose">
    </telerik:RadWindow>
    <div style="width: 918px; float: left; padding-left: 16px; padding-top: 25px;">
        <div style="width: 815px; float: left;">
            <div class="left_box"></div>
            <div style="width: 795px;" class="box">
                <div class="title_box" align="left">Layout</div>
            </div>
            <div class="right_box"></div>
            <div style="background-color: #edeeef; float: left; width: 815px; padding-top: 19px; padding-bottom: 19px;">
                <div>
                    <button type="button" class="btn btn-success btn-xs" onclick="LayoutNew()"><i class="fa fa-plus"></i> Thêm mới</button>
                    <button type="button" class="btn btn-success btn-xs" onclick="LayoutEdit()"><i class="fa fa-pencil"></i> Hiệu chỉnh</button>
                    <button type="button" class="btn btn-success btn-xs" onclick="LayoutDelete()"><i class="fa fa-close"></i> Xóa</button>
                </div>
                <hr />
                <div id="layoutLinks" style="display: none;">
                    <%--  <a id="btnNew" href="javascript:void(0)" onclick="LayoutNew();" class="Header">New</a>
                    <a id="btnEdit" href="javascript:void(0)" onclick="LayoutEdit();" class="Header">Edit</a>
                    <a id="A1" href="javascript:void(0)" onclick="LayoutDelete();" class="Header">Delete</a>--%>
                </div>
                <div>
                    <telerik:RadGrid ID="gvLayout" runat="server" AutoGenerateColumns="False"
                        AllowPaging="True" AllowSorting="True" GridLines="None"
                        AllowMultiRowSelection="True" >
                        <HeaderContextMenu EnableAutoScroll="True"></HeaderContextMenu>
                        <MasterTableView ClientDataKeyNames="Id" AllowMultiColumnSorting="True">
                            <Columns>
                                <telerik:GridClientSelectColumn>
                                    <ItemStyle Width="5px" />
                                </telerik:GridClientSelectColumn>
                                <telerik:GridBoundColumn HeaderText='<%$ Resources:Site,Name %>' DataField="Name">
                                    
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings EnableRowHoverStyle="true">
                            <Selecting AllowRowSelect="True" />
                            <ClientEvents OnCommand="gvLayout_Command" />
                            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </div>
            </div>
            <div class="bottombox_left">&nbsp;</div>
            <div style="width: 795px;" class="bottombox_center">&nbsp;</div>
            <div class="bottombox_right">&nbsp;</div>
        </div>
    </div>
</asp:Content>
