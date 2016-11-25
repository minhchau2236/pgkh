<%@ Page EnableViewState="false" Title="<%# Resources.Site.UserManage %>" Language="C#" MasterPageFile="~/Systems/MasterPage.Master" AutoEventWireup="true" CodeBehind="UserManage.aspx.cs" Inherits="PSCPortal.Systems.Security.UserManage" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">
        var dialogMethod;
        function CallWebMethodSuccess(results, context, methodName) {
            switch (methodName) {
                case "GetUserList":
                    {
                        GetUserListCallback(results, context, methodName);
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
                case "UserAdd":
                    {
                        UserAddCallback(results, context, methodName);
                    }
                    break;
                case "UserAuthenticationEdit":
                    {
                        UserAuthenticationEditCallback(results, context, methodName);
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
                case "UserChangePass":
                    {
                        UserChangePassCallback(results, context, methodName);
                    }
                    break;
                case "UserDelete":
                    {
                        UserDeleteCallback(results, context, methodName);
                    }
                    break;
                case "GetPermission":
                    {
                        SecurityUICallback(results, context, methodName);
                    }
                    break;
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
            //  alert(results._message);
        }

        function GetUserList() {
            var tableView = $find("<%=gvUser.ClientID%>").get_masterTableView();
            var sortExpressions = tableView.get_sortExpressions();
            PageMethods.GetUserList(0, tableView.get_pageSize(), sortExpressions.toString(), CallWebMethodSuccess);
            PageMethods.GetUserCount(CallWebMethodSuccess);
        }
        function GetUserListCallback(results, context, methodName) {
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
            var subdomainId = '<%= PSCPortal.Framework.Helpler.SessionHelper.GetSession(PSCPortal.Framework.Helpler.SessionKey.SubDomain) %>';
            if (subdomainId == '<%=Guid.Empty%>') {
                radalert("Vui lòng chọn SubDomain tương ứng để thêm mới.", 250, 100, "<%= Resources.Site.Warning %>");
                return;
            }
            PageMethods.UserNew(CallWebMethodSuccess, CallWebMethodFailed);
        }
        function UserNewCallback(results, context, methodName) {
            dialogMethod = "UserNew";
            var oWnd = $find("<%= rwUser.ClientID %>");
            oWnd.setSize(600, 350);
            oWnd.setUrl("UserDetail.aspx");
            oWnd.show();
        }
        function UserAddCallback(result, context, methodName) {
            GetUserList();
        }

        function UserEdit() {
            var items = $find("<%=gvUser.ClientID%>").get_masterTableView().get_selectedItems();
            if (items.length == 0) {
                radalert("<%= Resources.Site.UserNotChoose %>", 250, 100, "<%= Resources.Site.Warning %>");
                return;
            }
            var obj = items[0].get_dataItem();
            PageMethods.UserEdit(obj.Id, CallWebMethodSuccess, CallWebMethodFailed);
        }
        function UserEditCallback(results, context, methodName) {
            dialogMethod = "UserEdit";
            var oWnd = $find("<%= rwUser.ClientID %>");
            oWnd.setSize(600, 350);
            oWnd.setUrl("UserDetail.aspx");
            oWnd.show();
        }

        function UserUpdateCallback(results, context, methodName) {
            GetUserList();
        }

        function UserAuthenticationEdit() {
            var items = $find("<%=gvUser.ClientID%>").get_masterTableView().get_selectedItems();
            if (items.length == 0) {
                radalert("<%= Resources.Site.UserNotChoose %>", 250, 100, "<%= Resources.Site.Warning %>");
                return;
            }
            var subdomainId = '<%= PSCPortal.Framework.Helpler.SessionHelper.GetSession(PSCPortal.Framework.Helpler.SessionKey.SubDomain) %>';
            if (subdomainId == '<%=Guid.Empty%>') {
                radalert("Vui lòng chọn SubDomain tương ứng để hiệu chỉnh.", 250, 100, "<%= Resources.Site.Warning %>");
                return;
            }
            var obj = items[0].get_dataItem();
            PageMethods.UserAuthenticationEdit(obj.Id, CallWebMethodSuccess, CallWebMethodFailed);
        }
        function UserAuthenticationEditCallback(results, context, methodName) {
            dialogMethod = "UserEditRole";
            var oWnd = $find("<%= rwUser.ClientID %>");
            oWnd.setSize(800, 450);
            oWnd.setUrl("RolesOfUser.aspx");
            oWnd.show();
        }


        function UserDelete() {
            var objList = GetUserSelect();
            if (objList.length == 0) {
                radalert("<%= Resources.Site.UserNotChoose %>", 250, 100, "<%= Resources.Site.Warning %>");
                return;
            }
            radconfirm("<%= Resources.Site.UserConfirmDelete%>", UserDeleteConfirm, 250, 100, null, "<%= Resources.Site.Warning %>");
        }
        function UserDeleteConfirm(arg) {
            if (!arg)
                return;
            var objList = GetUserSelect();
            PageMethods.UserDelete(objList, CallWebMethodSuccess, CallWebMethodFailed);
        }
        function UserDeleteCallback(results, context, methodName) {
            GetUserList();
            $find("<%=gvUser.ClientID%>").get_masterTableView().clearSelectedItems();
        }

        function UserChangePass() {
            var items = $find("<%=gvUser.ClientID%>").get_masterTableView().get_selectedItems();
            if (items.length == 0) {
                radalert("<%= Resources.Site.UserNotChoose %>", 250, 100, "<%= Resources.Site.Warning %>");
                return;
            }
            var obj = items[0].get_dataItem();
            PageMethods.UserChangePass(obj.Id, CallWebMethodSuccess, CallWebMethodFailed);
        }
        function UserChangePassCallback(results, context, methodName) {
            dialogMethod = "UserChangePassword";
            var oWnd = $find("<%= rwUser.ClientID %>");
            oWnd.setSize(600, 200);
            oWnd.setUrl("ResetPassword.aspx");
            oWnd.show();
        }

        // config subdomain
        function UserConfigSubDomain() {
            var items = $find("<%=gvUser.ClientID%>").get_masterTableView().get_selectedItems();
            if (items.length == 0) {
                radalert("<%= Resources.Site.GroupNotChoose %>", 250, 100, "<%= Resources.Site.Warning %>");
                return;
            }
            if (items[0].get_dataItem().Name == 'Administrators') {
                radalert("Bạn không thể chọn Administrators.", 250, 100, "<%= Resources.Site.Warning %>");
                return;
            }
            var obj = items[0].get_dataItem();
            PageMethods.UserConfigSubDomain(obj.Id, UserConfigSubDomainCallback, CallWebMethodFailed);
        }
        function UserConfigSubDomainCallback(results, context, methodName) {
            dialogMethod = "ConfigSubDomain";
            var oWnd = $find("<%= rwUser.ClientID %>");
            oWnd.setSize(800, 600);
            oWnd.setUrl("UserConfigDomain.aspx");
            oWnd.show();
        }

        var bntUserAdd;
        var bntUserEdit;
        var bntUserEditRole;
        var bntUserDelete;
        var bntUserChangePass;
        var bntUserConfigSub;
        function CreateElementUserLinks() {
            bntUserAdd = document.createElement("a");
            bntUserAdd.setAttribute("href", "javascript:void(0)");
            bntUserAdd.setAttribute("onClick", "UserNew()");
            bntUserAdd.setAttribute("class", "Header");
            var addName = document.createTextNode("[Thêm mới]");
            bntUserAdd.appendChild(addName);

            bntUserEdit = document.createElement("a");
            bntUserEdit.setAttribute("href", "javascript:void(0)");
            bntUserEdit.setAttribute("onClick", "UserEdit()");
            bntUserEdit.setAttribute("class", "Header");
            var editName = document.createTextNode("[Hiệu chỉnh]");
            bntUserEdit.appendChild(editName);

            bntUserEditRole = document.createElement("a");
            bntUserEditRole.setAttribute("href", "javascript:void(0)");
            bntUserEditRole.setAttribute("onClick", "UserAuthenticationEdit()");
            bntUserEditRole.setAttribute("class", "Header");
            var editRoleName = document.createTextNode("[Hiệu chỉnh nhóm]");
            bntUserEditRole.appendChild(editRoleName);

            bntUserDelete = document.createElement("a");
            bntUserDelete.setAttribute("href", "javascript:void(0)");
            bntUserDelete.setAttribute("onClick", "UserDelete()");
            bntUserDelete.setAttribute("class", "Header");
            var deleteName = document.createTextNode("[Xóa]");
            bntUserDelete.appendChild(deleteName);

            bntUserChangePass = document.createElement("a");
            bntUserChangePass.setAttribute("href", "javascript:void(0)");
            bntUserChangePass.setAttribute("onClick", "UserChangePass()");
            bntUserChangePass.setAttribute("class", "Header");
            var changePassName = document.createTextNode("[Đổi mật khẩu]");
            bntUserChangePass.appendChild(changePassName);

            bntUserConfigSub = document.createElement("a");
            bntUserConfigSub.setAttribute("href", "javascript:void(0)");
            bntUserConfigSub.setAttribute("onClick", "UserConfigSubDomain()");
            bntUserConfigSub.setAttribute("class", "Header");
            var changePassName = document.createTextNode("[Cấu hình SubDomain]");
            bntUserConfigSub.appendChild(changePassName);
        }
        function pageLoad() {
            CreateElementUserLinks();
            GetUserList();
            SecurityUI();
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
        function RadWindowUserClose(sender, args) {
            if (!args.get_argument())
                return;
            switch (dialogMethod) {
                case "UserNew":
                    {
                        PageMethods.UserAdd(CallWebMethodSuccess, CallWebMethodFailed);
                    }
                    break;
                case "UserEdit":
                    {
                        PageMethods.UserUpdate(CallWebMethodSuccess, CallWebMethodFailed);
                    }
                    break;
                case "UserEditRole":
                    {
                        PageMethods.UserAuthenticationEdit(CallWebMethodSuccess, CallWebMethodFailed);
                    }
                    break;
            }
        }
        var User_Add = 11;
        var User_Edit = 12;
        var User_EditRole = 13;
        var User_Delete = 14;
        var User_ChangePassword = 15;
        function SecurityUI() {
            PageMethods.GetPermission([User_Add, User_Edit, User_EditRole, User_Delete, User_ChangePassword], CallWebMethodSuccess, CallWebMethodFailed);
        }
        function SecurityUICallback(results, context, methodName) {
            var subdomainId = '<%= PSCPortal.Framework.Helpler.SessionHelper.GetSession(PSCPortal.Framework.Helpler.SessionKey.SubDomain) %>';
            var arrPermission = Sys.Serialization.JavaScriptSerializer.deserialize(results);
            if (arrPermission[User_Add]) {//btnTrashRestore
                document.getElementById("userLinks").appendChild(bntUserAdd);
            }
            if (arrPermission[User_Edit]) {
                document.getElementById("userLinks").appendChild(bntUserEdit);
            }
            if (arrPermission[User_EditRole]) {
                document.getElementById("userLinks").appendChild(bntUserEditRole);
            }
            if (arrPermission[User_Delete]) {
                document.getElementById("userLinks").appendChild(bntUserDelete);
            }
            if (arrPermission[User_ChangePassword]) {
                document.getElementById("userLinks").appendChild(bntUserChangePass);
            }

            if (subdomainId == '<%=Guid.Empty%>') {
                document.getElementById("userLinks").appendChild(bntUserConfigSub);
            }

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindow ID="rwUser" runat="server" Modal="True" VisibleStatusbar="False" OnClientClose="RadWindowUserClose">
    </telerik:RadWindow>
    <div style="width: 815px; float: left; padding-left: 16px; padding-top: 25px;">
        <div style="width: 815px; float: left; margin-right: 12px;">
            <div class="left_box"></div>
            <div style="width: 795px;" class="box">
                <div class="title_box" align="left"><%= Resources.Site.UserManage %></div>
            </div>
            <div class="right_box"></div>
            <div style="background-color: #edeeef; float: left; width: 815px; padding-top: 19px; padding-bottom: 19px;">
               <div>
                    <button type="button" class="btn btn-success btn-xs" onclick="UserNew()"><i class="fa fa-plus"></i> Thêm mới</button>
                    <button type="button" class="btn btn-success btn-xs" onclick="UserEdit()"><i class="fa fa-pencil"></i> Hiệu chỉnh</button>
                    <button type="button" class="btn btn-success btn-xs" onclick="UserAuthenticationEdit()"><i class="fa fa-edit"></i> Hiệu chỉnh nhóm</button>
                    <button type="button" class="btn btn-success btn-xs" onclick="UserConfigSubDomain()"><i class="fa fa-gears"></i> Cấu hình SubDomain</button>
                    <button type="button" class="btn btn-success btn-xs" onclick="UserChangePass()"><i class="fa fa-refresh"></i> Đổi mật khẩu</button>
                    <button type="button" class="btn btn-success btn-xs" onclick="UserDelete()"><i class="fa fa-close"></i> Xóa</button>
                </div>
                <hr />
                <div id="userLinks" style="display: none;">
                    <%-- <a id="bntUserAdd" href="javascript:void(0)" style="display: none" onclick="UserNew();" class="Header">[<%= Resources.Site.AddItem %>]</a>
                    <a id="bntUserEdit" href="javascript:void(0)" style="display: none" onclick="UserEdit();" class="Header" >[<%= Resources.Site.EditItem %>]</a> 
                    <a id="bntUserEditRole" href="javascript:void(0)" style="display: none" onclick="UserAuthenticationEdit();" class="Header">[<%= Resources.Site.ChangeGroup%>]</a>         
                    <a id="bntUserDelete" href="javascript:void(0)" style="display: none" onclick="UserDelete();" class="Header">[<%= Resources.Site.DeleteItem %>]</a>        
                    <a id="bntUserChangePass"  href="javascript:void(0)" style="display: none" onclick="UserChangePass();" class="Header">[<%= Resources.Site.ChangePassword %>]</a>--%>
                </div>
                <div>
                    <telerik:RadGrid ID="gvUser"  runat="server" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" GridLines="None" AllowMultiRowSelection="True">
                        <HeaderContextMenu EnableAutoScroll="True"></HeaderContextMenu>
                        <MasterTableView ClientDataKeyNames="Id" AllowMultiColumnSorting="True">
                            <Columns>
                                <telerik:GridClientSelectColumn>
                                    <ItemStyle Width="20px" />
                                </telerik:GridClientSelectColumn>
                                <telerik:GridBoundColumn HeaderText="<%$ Resources:Site,UserName %>" DataField="Name">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Email" DataField="Email">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="<%$ Resources:Site,IsOnline %>" DataField="IsOnline">
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings EnableRowHoverStyle="true">
                            <Selecting AllowRowSelect="True" />
                            <ClientEvents OnCommand="gvUser_Command" />
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
