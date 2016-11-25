<%@ Page Title="" Language="C#" MasterPageFile="~/Systems/MasterPage.Master" AutoEventWireup="true" CodeBehind="ArticleSendManage.aspx.cs" Inherits="PSCPortal.Systems.CMS.ArticleSendManage" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Systems/CMS/Scripts/TrashManage.js" type="text/javascript"></script>
    <script src="/Scripts/Utility.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function initialize() {
            grid = $find("<%=gvArticle.ClientID%>");
            strArticleNotChoose = "<%= Resources.Site.ArticleNotChoose %>";
            strWarning = "<%= Resources.Site.Warning %>";
            oWnd = $find("<%= rwTrash.ClientID %>");
            strArticleConfirmDelete = "<%= Resources.Site.ArticleConfirmDelete %>";
        }
        var grid;
        var strArticleNotChoose;
        var strWarning;
        var oWnd;
        var strArticleConfirmDelete;

        var dialogMethod;
        function CallWebMethodSuccess(results, context, methodName) {
            switch (methodName) {
                case "GetArticleList":
                    {
                        GetArticleListCallback(results, context, methodName);
                    }
                    break;
                case "GetArticleCount":
                    {
                        GetArticleCountCallback(results, context, methodName);
                    }
                    break;
                case "ArticleRestore":
                    {
                        ArticleRestoreCallback(results, context, methodName);
                    }
                    break;
                case "ArticleDelete":
                    {
                        ArticleDeleteCallback(results, context, methodName);
                    }
                    break;
                case "GetPermission":
                    {
                        SecurityUICallback(results, context, methodName);
                    }
                    break;
            }
        }

        function CallWebMethodFailed(results, context, methodName) {
            alert(results._message);
        }

        function GetArticleSelect() {
            var items = grid.get_masterTableView().get_selectedItems();
            var objList = new Array();
            for (var i = 0; i < items.length; i++) {
                if (items[i].get_dataItem().IsCheck == true)
                    alert("Tiêu đề: " + items[i].get_dataItem().Title + " không được xử lý");
                else {
                    Array.add(objList, items[i].get_dataItem().Id);
                }
            }
            return objList;
        }

        function GetArticleList() {
            var tableView = grid.get_masterTableView();
            var sortExpressions = tableView.get_sortExpressions();
            PageMethods.GetArticleList(0, tableView.get_pageSize(), sortExpressions.toString(), CallWebMethodSuccess);
            PageMethods.GetArticleCount(CallWebMethodSuccess);
            grid.get_masterTableView().clearSelectedItems();
        }
        function GetArticleListCallback(results, context, methodName) {
            var tableView = grid.get_masterTableView();
            tableView.set_dataSource(Sys.Serialization.JavaScriptSerializer.deserialize(results));
            tableView.dataBind();
        }

        function GetArticleCountCallback(results, context, methodName) {
            var tableView = grid.get_masterTableView();
            tableView.set_virtualItemCount(results);
        }

        var btnSendArResote;
        var btnSendArDelete;
        function CreateElementArticleSendLinks() {
            btnSendArResote = document.createElement("a");
            btnSendArResote.setAttribute("href", "javascript:void(0)");
            btnSendArResote.setAttribute("onClick", "ArticleRestore()");
            btnSendArResote.setAttribute("class", "Header");
            var restoreName = document.createTextNode("[Xử lý]");
            btnSendArResote.appendChild(restoreName);

            btnSendArDelete = document.createElement("a");
            btnSendArDelete.setAttribute("href", "javascript:void(0)");
            btnSendArDelete.setAttribute("onClick", "ArticleDelete()");
            btnSendArDelete.setAttribute("class", "Header");
            var deleteName = document.createTextNode("[Xóa]");
            btnSendArDelete.appendChild(deleteName);
        }

        function pageLoad() {
            initialize();
            CreateElementArticleSendLinks();
            GetArticleList();
            SecurityUI();
        }
        function gvArticle_Command(sender, args) {
            args.set_cancel(true);
            sender.get_masterTableView().clearSelectedItems();
            var currentPageIndex = sender.get_masterTableView().get_currentPageIndex();
            var pageSize = sender.get_masterTableView().get_pageSize();
            var sortExpressions = sender.get_masterTableView().get_sortExpressions();
            PageMethods.GetArticleList(currentPageIndex * pageSize, pageSize, sortExpressions.toString(), CallWebMethodSuccess);
            PageMethods.GetArticleCount(CallWebMethodSuccess);
        }

        function ArticleRestore() {
            var objList = GetArticleSelect();
            if (objList.length == 0) {
                radalert(strArticleNotChoose, 250, 100, strWarning);
                return;
            }
            dialogMethod = "ArticleRestore";
            oWnd.setSize(600, 150);
            oWnd.setUrl("ArticleSendDetail.aspx");
            oWnd.show();
        }

        function ArticleRestoreCallback() {
            GetArticleList();
        }

        function ArticleDelete() {
            var objList = GetArticleSelect();
            if (objList.length == 0) {
                radalert(strArticleNotChoose, 250, 100, strWarning);
                return;
            }
            radconfirm(strArticleConfirmDelete, ArticleDeleteConfirm, 250, 100, null, strWarning);
        }
        function ArticleDeleteConfirm(arg) {
            if (!arg)
                return;
            var objList = GetArticleSelect();
            PageMethods.ArticleDelete(objList, CallWebMethodSuccess, CallWebMethodFailed);
        }
        function ArticleDeleteCallback() {
            GetArticleList();
        }
        function RadWindowTrashClose(sender, args) {
            if (!args.get_argument())
                return;
            switch (dialogMethod) {
                case "ArticleRestore":
                    {
                        var objList = GetArticleSelect();
                        if (objList.length == 0) {
                            radalert(strArticleNotChoose, 250, 100, strWarning);
                            return;
                        }
                        PageMethods.ArticleRestore(objList, CallWebMethodSuccess, CallWebMethodFailed);
                    }
                    break;
            }
        }
        var Article_Xuly = 57;
        var Article_Delete = 58;

        function SecurityUI() {
            PageMethods.GetPermission([Article_Xuly, Article_Delete], CallWebMethodSuccess, CallWebMethodFailed);
        }
        function SecurityUICallback(results, context, methodName) {
            var arrPermission = Sys.Serialization.JavaScriptSerializer.deserialize(results);
            if (arrPermission[Article_Xuly]) {//btnTrashRestore
                document.getElementById("sendArLinks").appendChild(btnSendArResote);
            }

            if (arrPermission[Article_Delete]) {
                document.getElementById("sendArLinks").appendChild(btnSendArDelete);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindow ID="rwTrash" runat="server" Modal="True" VisibleStatusbar="False" OnClientClose="RadWindowTrashClose">
    </telerik:RadWindow>
    <div style="width: 818px; float: left; padding-left: 16px; padding-top: 25px;">
        <div style="width: 815px; float: left; margin-right: 12px;">
            <div class="left_box"></div>
            <div style="width: 795px;" class="box">
                <div class="title_box" align="left">Bài viết được gởi đến</div>
            </div>
            <div class="right_box"></div>
            <div style="background-color: #edeeef; float: left; width: 815px; padding-top: 19px; padding-bottom: 19px;">
                <div>
                    <button type="button" class="btn btn-success btn-xs" onclick="ArticleRestore()"><i class="fa fa-spinner"></i> Xử lý</button>
                    <button type="button" class="btn btn-success btn-xs" onclick="ArticleDelete()"><i class="fa fa-close"></i> Xóa</button>
                </div>
                <hr />
                <div id="sendArLinks" style="display: none;">
                    <%--<a id="btnTrashRestore" style="display: none" href="javascript:void(0)" onclick="ArticleRestore();" class="Header">[Xử lý]</a>
                    <a id="btnTrashDelete" style="display: none" href="javascript:void(0)" onclick="ArticleDelete();" class="Header">[<%= Resources.Site.DeleteItem %>]</a>--%>
                </div>
                <telerik:RadGrid ID="gvArticle" runat="server" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" GridLines="None" AllowMultiRowSelection="True" Width="812px">
                    <HeaderContextMenu EnableAutoScroll="True"></HeaderContextMenu>
                    <MasterTableView ClientDataKeyNames="Id" AllowMultiColumnSorting="True">
                        <Columns>
                            <telerik:GridClientSelectColumn>
                                <ItemStyle Width="50px" />
                                <HeaderStyle Width="50px" />
                            </telerik:GridClientSelectColumn>
                            <telerik:GridBoundColumn HeaderText="<%$ Resources:Site,Name %>" DataField="Name">
                                <HeaderStyle Width="500px" />
                                <ItemStyle Width="500px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Nơi gởi" DataField="SubDomainFromName">
                                <HeaderStyle Width="250px" />
                                <ItemStyle Width="250px" />
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="True" />
                        <ClientEvents OnCommand="gvArticle_Command" />
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                    </ClientSettings>
                </telerik:RadGrid>
                <div>
                </div>
            </div>
            <div class="bottombox_left">&nbsp;</div>
            <div style="width: 795px;" class="bottombox_center">&nbsp;</div>
            <div class="bottombox_right">&nbsp;</div>
        </div>
    </div>
</asp:Content>

