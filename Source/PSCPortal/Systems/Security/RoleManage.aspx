<%@ Page EnableViewState="false" Title="<%# Resources.Site.GroupManage %>" Language="C#" MasterPageFile="~/Systems/MasterPage.Master" AutoEventWireup="true" CodeBehind="RoleManage.aspx.cs" Inherits="PSCPortal.Systems.Security.RoleManage" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">
        var dialogMethod;
        function CallWebMethodSuccess(results, context, methodName) {
            switch (methodName) {
                case "GetRoleList":
                    {
                        GetRoleListCallback(results, context, methodName);
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
                case "RoleAdd":
                    {
                        RoleAddCallback(results, context, methodName);
                    }
                    break;
                case "RoleEdit":
                    {
                        RoleEditCallback(results, context, methodName);
                    }
                    break;
                case "RoleAuthenticationEdit":
                    {
                        RoleAuthenticationEditCallback(results, context, methodName);
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
                case "GetPermission":
                    {
                        SecurityUICallback(results, context, methodName)
                    }
                    break;
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
            //  alert(results._message);
        }

        function GetRoleList() {
            var tableView = $find("<%=gvRole.ClientID%>").get_masterTableView();
            var sortExpressions = tableView.get_sortExpressions();
            PageMethods.GetRoleList(0, tableView.get_pageSize(), sortExpressions.toString(), CallWebMethodSuccess);
            PageMethods.GetRoleCount(CallWebMethodSuccess);
        }
        function GetRoleListCallback(results, context, methodName) {
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
            var subdomainId = '<%= PSCPortal.Framework.Helpler.SessionHelper.GetSession(PSCPortal.Framework.Helpler.SessionKey.SubDomain) %>';
            if (subdomainId == '<%=Guid.Empty%>') {
                radalert("Vui lòng chọn SubDomain tương ứng để thêm mới.", 250, 100, "<%= Resources.Site.Warning %>");
                return;
            }
            PageMethods.RoleNew(CallWebMethodSuccess, CallWebMethodFailed);
        }
        function RoleNewCallback(results, context, methodName) {
            dialogMethod = "RoleNew";
            var oWnd = $find("<%= rwRole.ClientID %>");
            oWnd.setSize(600, 200);
            oWnd.setUrl("RoleDetail.aspx");
            oWnd.show();
        }
        function RoleAddCallback(result, context, methodName) {
            GetRoleList();
        }

        function RoleEdit() {
            var items = $find("<%=gvRole.ClientID%>").get_masterTableView().get_selectedItems();
            if (items.length == 0) {
                radalert("<%= Resources.Site.GroupNotChoose %>", 250, 100, "<%= Resources.Site.Warning %>");
                return;
            }
            var obj = items[0].get_dataItem();
            PageMethods.RoleEdit(obj.Id, CallWebMethodSuccess, CallWebMethodFailed);
        }
        function RoleEditCallback(results, context, methodName) {
            dialogMethod = "RoleEdit";
            var oWnd = $find("<%= rwRole.ClientID %>");
            oWnd.setSize(600, 200);
            oWnd.setUrl("RoleDetail.aspx");
            oWnd.show();
        }

        function RoleAuthenticationEdit() {
            var items = $find("<%=gvRole.ClientID%>").get_masterTableView().get_selectedItems();
            if (items.length == 0) {
                radalert("<%= Resources.Site.GroupNotChoose %>", 250, 100, "<%= Resources.Site.Warning %>");
                return;
            }
            var subdomainId = '<%= PSCPortal.Framework.Helpler.SessionHelper.GetSession(PSCPortal.Framework.Helpler.SessionKey.SubDomain) %>';
            if (subdomainId == '<%=Guid.Empty%>') {
                radalert("Vui lòng chọn SubDomain tương ứng để hiệu chỉnh.", 250, 100, "<%= Resources.Site.Warning %>");
                return;
            }
            var obj = items[0].get_dataItem();
            PageMethods.RoleAuthenticationEdit(obj.Id, CallWebMethodSuccess, CallWebMethodFailed);
        }
        function RoleAuthenticationEditCallback(results, context, methodName) {
            dialogMethod = "RoleEditUser";
            var oWnd = $find("<%= rwRole.ClientID %>");
            oWnd.setSize(800, 350);
            oWnd.setUrl("UserInRoleManage.aspx");
            oWnd.show();
        }

        function RoleUpdateCallback(results, context, methodName) {
            GetRoleList();
        }

        function RoleDelete() {
            var objList = GetRoleSelect();
            if (objList.length == 0) {
                radalert("<%= Resources.Site.GroupNotChoose %>", 250, 100, "<%= Resources.Site.Warning %>");
                return;
            }
            radconfirm("<%= Resources.Site.GroupConfirmDelete%>", RoleDeleteConfirm, 250, 100, null, "<%= Resources.Site.Warning %>");
        }
        function RoleDeleteConfirm(arg) {
            if (!arg)
                return;
            var objList = GetRoleSelect();
            PageMethods.RoleDelete(objList, CallWebMethodSuccess, CallWebMethodFailed);
        }
        function RoleDeleteCallback(results, context, methodName) {
            GetRoleList();
            $find("<%=gvRole.ClientID%>").get_masterTableView().clearSelectedItems();
        }

        var bntRoleAdd;
        var bntRoleEdit;
        var bntRoleEditUser;
        var bntRoleDelete;
        var bntSubdomain;
        function CreateElementRoleLinks() {
            bntRoleAdd = document.createElement("a");
            bntRoleAdd.setAttribute("href", "javascript:void(0)");
            bntRoleAdd.setAttribute("onClick", "RoleNew()");
            bntRoleAdd.setAttribute("class", "Header");
            var addName = document.createTextNode("[Thêm mới]");
            bntRoleAdd.appendChild(addName);

            bntRoleEdit = document.createElement("a");
            bntRoleEdit.setAttribute("href", "javascript:void(0)");
            bntRoleEdit.setAttribute("onClick", "RoleEdit()");
            bntRoleEdit.setAttribute("class", "Header");
            var editName = document.createTextNode("[Hiệu chỉnh]");
            bntRoleEdit.appendChild(editName);

            bntRoleEditUser = document.createElement("a");
            bntRoleEditUser.setAttribute("href", "javascript:void(0)");
            bntRoleEditUser.setAttribute("onClick", "RoleAuthenticationEdit()");
            bntRoleEditUser.setAttribute("class", "Header");
            var editUserName = document.createTextNode("[Hiệu chỉnh người dùng]");
            bntRoleEditUser.appendChild(editUserName);

            bntRoleDelete = document.createElement("a");
            bntRoleDelete.setAttribute("href", "javascript:void(0)");
            bntRoleDelete.setAttribute("onClick", "RoleDelete()");
            bntRoleDelete.setAttribute("class", "Header");
            var deleteName = document.createTextNode("[Xóa]");
            bntRoleDelete.appendChild(deleteName);

            bntSubdomain = document.createElement("a");
            bntSubdomain.setAttribute("href", "javascript:void(0)");
            bntSubdomain.setAttribute("onClick", "ConfigSubDomain()");
            bntSubdomain.setAttribute("class", "Header");
            var subdomainName = document.createTextNode("[Cài đặt Subdomain]");
            //bntSubdomain.appendChild(subdomainName);
        }

        function pageLoad() {
            CreateElementRoleLinks();
            GetRoleList();
            SecurityUI();
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
        function RadWindowRoleClose(sender, args) {
            if (!args.get_argument())
                return;
            switch (dialogMethod) {
                case "RoleNew":
                    {
                        PageMethods.RoleAdd(CallWebMethodSuccess, CallWebMethodFailed);
                    }
                    break;
                case "RoleEdit":
                    {
                        PageMethods.RoleUpdate(CallWebMethodSuccess, CallWebMethodFailed);
                    }
                    break;
                case "RoleEditUser":
                    {
                        radalert("<%= Resources.Site.GroupUpdateUserSuccess %>!", 250, 100, "<%= Resources.Site.Information %>");
                    }
                    break;
                case "ConfigSubDomain":
                    {
                        GetRoleList();
                        break;
                    }
            }
        }
        var Role_Add = 16;
        var Role_Edit = 17;
        var Role_EditUser = 18;
        var Role_Delete = 19;
        function SecurityUI() {
            PageMethods.GetPermission([Role_Add, Role_Edit, Role_EditUser, Role_Delete], CallWebMethodSuccess, CallWebMethodFailed);
        }
        function SecurityUICallback(results, context, methodName) {
            var arrPermission = Sys.Serialization.JavaScriptSerializer.deserialize(results);
            if (arrPermission[Role_Add]) {//btnTrashRestore
                document.getElementById("roleLinks").appendChild(bntRoleAdd);
            }
            if (arrPermission[Role_Edit]) {
                document.getElementById("roleLinks").appendChild(bntRoleEdit);
            }
            if (arrPermission[Role_EditUser]) {
                document.getElementById("roleLinks").appendChild(bntRoleEditUser);
            }
            if (arrPermission[Role_Delete]) {
                document.getElementById("roleLinks").appendChild(bntRoleDelete);
            }
            document.getElementById("roleLinks").appendChild(bntSubdomain);
        }
        // config subdomain
        function ConfigSubDomain() {
            var items = $find("<%=gvRole.ClientID%>").get_masterTableView().get_selectedItems();
            if (items.length == 0) {
                radalert("<%= Resources.Site.GroupNotChoose %>", 250, 100, "<%= Resources.Site.Warning %>");
                return;
            }
            if (items[0].get_dataItem().Name == 'Administrators') {
                radalert("Bạn không thể chọn Administrators.", 250, 100, "<%= Resources.Site.Warning %>");
                return;
            }
            var obj = items[0].get_dataItem();
            PageMethods.ConfigSubDomain(obj.Id, ConfigSubDomainCallback, CallWebMethodFailed);
        }
        function ConfigSubDomainCallback(results, context, methodName) {
            dialogMethod = "ConfigSubDomain";
            var oWnd = $find("<%= rwRole.ClientID %>");
            oWnd.setSize(800, 600);
            oWnd.setUrl("RoleConfigDomain.aspx");
            oWnd.show();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindow ID="rwRole" runat="server" Modal="True" VisibleStatusbar="False" OnClientClose="RadWindowRoleClose">
    </telerik:RadWindow>
    <div style="width: 815px; float: left; padding-left: 16px; padding-top: 25px;">
        <div style="width: 815px; float: left; margin-right: 12px;">
            <div class="left_box"></div>
            <div style="width: 795px;" class="box">
                <div class="title_box" align="left"><%= Resources.Site.GroupManage %></div>
            </div>
            <div class="right_box"></div>
            <div style="background-color: #edeeef; float: left; width: 815px; padding-top: 19px; padding-bottom: 19px;">
                <div>
                    <button type="button" class="btn btn-success btn-xs" onclick="RoleNew()"><i class="fa fa-plus"></i> Thêm mới</button>
                    <button type="button" class="btn btn-success btn-xs" onclick="RoleEdit()"><i class="fa fa-pencil"></i> Hiệu chỉnh</button>
                    <button type="button" class="btn btn-success btn-xs" onclick="RoleAuthenticationEdit()"><i class="fa fa-edit"></i> Hiệu chỉnh người dùng</button>
                    <button type="button" class="btn btn-success btn-xs" onclick="RoleDelete()"><i class="fa fa-close"></i> Xóa</button>
                </div>
                <hr />
                <div id="roleLinks" style="display: none;">
                    <%--<a id="bntRoleAdd" href="javascript:void(0)" style="display: none" class="Header" onclick="RoleNew();">[<%= Resources.Site.AddItem%>]</a>
                    <a id="bntRoleEdit" href="javascript:void(0)" style="display: none" class="Header" onclick="RoleEdit();">[<%= Resources.Site.EditItem %>]</a>
                    <a id="bntRoleEditUser" href="javascript:void(0)" style="display: none" class="Header" onclick="RoleAuthenticationEdit();">[<%= Resources.Site.ChangeUser%>]</a>             
                    <a id="bntRoleDelete" href="javascript:void(0)" style="display: none" class="Header" onclick="RoleDelete();">[<%= Resources.Site.DeleteItem %>]</a>--%>
                    <%--<a id="bntSubdomain" href="javascript:void(0)" style="visibility: visible" class="Header" onclick="ConfigSubDomain();">[Cài đặt SubDomain]</a>--%>
                </div>
                <div>
                    <telerik:RadGrid  ID="gvRole" runat="server" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" GridLines="None" AllowMultiRowSelection="True">
                        <HeaderContextMenu EnableAutoScroll="True"></HeaderContextMenu>
                        <MasterTableView ClientDataKeyNames="Id" AllowMultiColumnSorting="True">
                            <Columns>
                                <telerik:GridClientSelectColumn>
                                    <ItemStyle Width="20px" />
                                </telerik:GridClientSelectColumn>
                                <telerik:GridBoundColumn HeaderText="<%$ Resources:Site,Name %>" DataField="Name">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="<%$ Resources:Site,Description %>" DataField="Description">
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings EnableRowHoverStyle="true">
                            <Selecting AllowRowSelect="True" />
                            <ClientEvents OnCommand="gvRole_Command" />
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
