<%@ Page Title="" Language="C#" MasterPageFile="~/Systems/MasterPage.Master" AutoEventWireup="true" CodeBehind="LinkWebsiteManage.aspx.cs" Inherits="PSCPortal.Systems.CMS.LinkWebsiteManage" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">
        var dialogMethod;
        function CallWebMethodSuccess(results, context, methodName) {
            switch (methodName) {
                case "GetLinkWebsiteList":
                    {
                        GetLinkWebsiteListCallback(results, context, methodName);
                    }
                    break;
                case "GetLinkWebsiteCount":
                    {
                        GetLinkWebsiteCountCallback(results, context, methodName);
                    }
                    break;
                case "LinkWebsiteNew":
                    {
                        LinkWebsiteNewCallback(results, context, methodName);
                    }
                    break;
                case "LinkWebsiteAdd":
                    {
                        LinkWebsiteAddCallback(results, context, methodName);
                    }
                    break;
                case "LinkWebsiteEdit":
                    {
                        LinkWebsiteEditCallback(results, context, methodName);
                    }
                    break;
                case "LinkWebsiteUpdate":
                    {
                        LinkWebsiteUpdateCallback(results, context, methodName);
                    }
                    break;
                case "LinkWebsiteDelete":
                    {
                        LinkWebsiteDeleteCallback(results, context, methodName);
                    }
                    break;
                case "GetPermission":
                    {
                        SecurityUICallback(results, context, methodName);
                    }
                    break;
            }
        }
        function GetLinkWebsiteSelect() {
            var items = $find("<%=gvLinkWebsite.ClientID%>").get_masterTableView().get_selectedItems();

            var objList = new Array();
            for (var i = 0; i < items.length; i++) {
                Array.add(objList, items[i].get_dataItem().Id);
            }
            return objList;
        }
        function CallWebMethodFailed(results, context, methodName) {
            alert(results._message);
        }

        function GetLinkWebsiteList() {
            var tableView = $find("<%=gvLinkWebsite.ClientID%>").get_masterTableView();
            var sortExpressions = tableView.get_sortExpressions();
            PageMethods.GetLinkWebsiteList(0, tableView.get_pageSize(), sortExpressions.toString(), CallWebMethodSuccess);
            PageMethods.GetLinkWebsiteCount(CallWebMethodSuccess);
        }
        function GetLinkWebsiteListCallback(results, context, methodName) {
            var grid = $find("<%=gvLinkWebsite.ClientID%>");
            var tableView = grid.get_masterTableView();
            tableView.set_dataSource(Sys.Serialization.JavaScriptSerializer.deserialize(results));
            tableView.dataBind();
        }

        function GetLinkWebsiteCountCallback(results, context, methodName) {
            var tableView = $find("<%=gvLinkWebsite.ClientID%>").get_masterTableView();
            tableView.set_virtualItemCount(results);
        }

        function LinkWebsiteNew() {
            PageMethods.LinkWebsiteNew(CallWebMethodSuccess, CallWebMethodFailed);
        }
        function LinkWebsiteNewCallback(results, context, methodName) {
            dialogMethod = "LinkWebsiteNew";
            var oWnd = $find("<%= rwLinkWebsite.ClientID %>");
            oWnd.setSize(600, 280);
            oWnd.setUrl("LinkWebsiteDetail.aspx");
            oWnd.show();
        }
        function LinkWebsiteAddCallback(result, context, methodName) {
            GetLinkWebsiteList();
        }

        function LinkWebsiteEdit() {
            var items = $find("<%=gvLinkWebsite.ClientID%>").get_masterTableView().get_selectedItems();
            if (items.length == 0) {
                radalert("Câu hỏi chưa được chọn.", 250, 100, "<%= Resources.Site.Warning %>");
                return;
            }
            var obj = items[0].get_dataItem();
            PageMethods.LinkWebsiteEdit(obj.Id, CallWebMethodSuccess, CallWebMethodFailed);
        }
        function LinkWebsiteEditCallback(results, context, methodName) {
            dialogMethod = "LinkWebsiteEdit";
            var oWnd = $find("<%= rwLinkWebsite.ClientID %>");
            oWnd.setSize(600, 280);
            oWnd.setUrl("LinkWebsiteDetail.aspx");
            oWnd.show();
        }

        function LinkWebsiteUpdateCallback(results, context, methodName) {
            GetLinkWebsiteList();
        }

        function LinkWebsiteDelete() {
            var objList = GetLinkWebsiteSelect();
            if (objList.length == 0) {
                radalert("Câu hỏi chưa được chọn", 250, 100, "<%= Resources.Site.Warning %>");
                return;
            }
            radconfirm("Bạn có chắc muốn xóa không ?", LinkWebsiteDeleteConfirm, 250, 100, null, "<%= Resources.Site.Warning %>");
        }
        function LinkWebsiteDeleteConfirm(args) {
            if (!args)
                return;
            var objList = GetLinkWebsiteSelect();
            PageMethods.LinkWebsiteDelete(objList, CallWebMethodSuccess, CallWebMethodFailed);
        }
        function LinkWebsiteDeleteCallback(results, context, methodName) {
            GetLinkWebsiteList();
            $find("<%=gvLinkWebsite.ClientID%>").get_masterTableView().clearSelectedItems();
        }

        var btnLinkAdd;
        var btnLinkEdit;
        var btnLinkDelete;
        function CreateElementLinkWebLinks() {
            btnLinkAdd = document.createElement("a");
            btnLinkAdd.setAttribute("href", "javascript:void(0)");
            btnLinkAdd.setAttribute("onClick", "LinkWebsiteNew()");
            btnLinkAdd.setAttribute("class", "Header");
            var addName = document.createTextNode("[Thêm mới]");
            btnLinkAdd.appendChild(addName);

            btnLinkEdit = document.createElement("a");
            btnLinkEdit.setAttribute("href", "javascript:void(0)");
            btnLinkEdit.setAttribute("onClick", "LinkWebsiteEdit()");
            btnLinkEdit.setAttribute("class", "Header");
            var editName = document.createTextNode("[Hiệu chỉnh]");
            btnLinkEdit.appendChild(editName);

            btnLinkDelete = document.createElement("a");
            btnLinkDelete.setAttribute("href", "javascript:void(0)");
            btnLinkDelete.setAttribute("onClick", "LinkWebsiteDelete()");
            btnLinkDelete.setAttribute("class", "Header");
            var deleteName = document.createTextNode("[Xóa]");
            btnLinkDelete.appendChild(deleteName);
        }
        function pageLoad() {
            CreateElementLinkWebLinks();
            GetLinkWebsiteList();
            SecurityUI();
        }
        function gvLinkWebsite_Command(sender, args) {
            args.set_cancel(true);
            sender.get_masterTableView().clearSelectedItems();
            var currentPageIndex = sender.get_masterTableView().get_currentPageIndex();
            var pageSize = sender.get_masterTableView().get_pageSize();
            var sortExpressions = sender.get_masterTableView().get_sortExpressions();

            PageMethods.GetLinkWebsiteList(currentPageIndex * pageSize, pageSize, sortExpressions.toString(), CallWebMethodSuccess);
            PageMethods.GetLinkWebsiteCount(CallWebMethodSuccess);
        }
        function RadWindowLinkWebsiteClose(sender, args) {
            if (!args.get_argument())
                return;
            switch (dialogMethod) {
                case "LinkWebsiteNew":
                    {
                        PageMethods.LinkWebsiteAdd(CallWebMethodSuccess, CallWebMethodFailed);
                    }
                    break;
                case "LinkWebsiteEdit":
                    {
                        PageMethods.LinkWebsiteUpdate(CallWebMethodSuccess, CallWebMethodFailed);
                    }
                    break;
            }
        }
        var Link_Add = 54;
        var Link_Edit = 55;
        var Link_Delete = 56;

        function SecurityUI() {
            PageMethods.GetPermission([Link_Add, Link_Edit, Link_Delete], CallWebMethodSuccess, CallWebMethodFailed);
        }
        function SecurityUICallback(results, context, methodName) {
            var arrPermission = Sys.Serialization.JavaScriptSerializer.deserialize(results);
            if (arrPermission[Link_Add]) {//btnTrashRestore
                document.getElementById("webLinks").appendChild(btnLinkAdd);
            }
            if (arrPermission[Link_Edit]) {
                document.getElementById("webLinks").appendChild(btnLinkEdit);
            }
            if (arrPermission[Link_Delete]) {
                document.getElementById("webLinks").appendChild(btnLinkDelete);
            }            
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindow ID="rwLinkWebsite" runat="server" Modal="True" VisibleStatusbar="False"
        OnClientClose="RadWindowLinkWebsiteClose">
    </telerik:RadWindow>
    <div style="width: 718px; float: left; padding-left: 16px; padding-top: 25px;">
        <div style="width: 608px; float: left; margin-right: 12px;">
            <div class="left_box">
            </div>
            <div style="width: 588px;" class="box">
                <div class="title_box" align="left">
                    Liên kết website</div>
            </div>
            <div class="right_box">
            </div>
            <div style="background-color: #edeeef; float: left; width: 608px; padding-top: 19px;
                padding-bottom: 19px;">
                <div id="webLinks">
                   <%-- <a href="javascript:void(0)" style="display: none" id="btnNew" onclick="LinkWebsiteNew();" class="Header">[<%= Resources.Site.AddItem %>]</a>
                    <a href="javascript:void(0)" style="display: none" id="btnEdit" onclick="LinkWebsiteEdit();" class="Header">[<%= Resources.Site.EditItem %>]</a>
                    <a href="javascript:void(0)" style="display: none" id="btnDelete" onclick="LinkWebsiteDelete();" class="Header">[<%= Resources.Site.DeleteItem %>]</a>
                    --%>
                </div>
                <div>
                    <telerik:RadGrid ID="gvLinkWebsite" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                        AllowSorting="True" GridLines="None" AllowMultiRowSelection="True">
                        <MasterTableView ClientDataKeyNames="Id" AllowMultiColumnSorting="True">
                            <Columns>
                                <telerik:GridClientSelectColumn>
                                    <ItemStyle Width="20px" />
                                </telerik:GridClientSelectColumn>
                                <telerik:GridBoundColumn HeaderText="<%$ Resources:Site,Name %>" DataField="Name">
                                </telerik:GridBoundColumn>
                                 <telerik:GridBoundColumn HeaderText="Link" DataField="Link">
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings EnableRowHoverStyle="true">
                            <Selecting AllowRowSelect="True" />
                            <ClientEvents OnCommand="gvLinkWebsite_Command" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </div>
            </div>
            <div class="bottombox_left">
                &nbsp;</div>
            <div style="width: 588px;" class="bottombox_center">
                &nbsp;</div>
            <div class="bottombox_right">
                &nbsp;</div>
        </div>
    </div>
</asp:Content>
