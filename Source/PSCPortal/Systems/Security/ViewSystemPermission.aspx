<%@ Page Language="C#" MasterPageFile="~/Systems/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="ViewSystemPermission.aspx.cs" Inherits="PSCPortal.Systems.Security.ViewSystemPermission"
    Title="Untitled Page" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .Authentication_Container
        {
            clear: both;
            overflow: auto;
            width: 835px;
            margin-left:10px;
        }
        .Row
        {
            clear: both;
            width: 833px;
            background-color: #404040;
            border: solid #1a1a1a;
            border-width: 0px 1px 1px 1px;
            float: left;
        }
        .FirstRow
        {
            border-top-width: 1px;
        }
        .FunctionCategory
        {
            color: #fff;
            width: 150px;
            float: left;
            font: 12px/23px "Segoe UI" , Arial, sans-serif;
            padding-left: 3px;
        }
        .FunctionContainer
        {
            float: left;
            width: 678px;
            border-left: solid 1px #1a1a1a;
            background-color: White;
        }
        .NormalFunction
        {
            float: left;
            width: 530px;
        }
        .Function
        {
            color: #333;
            width: 130px;
            float: left;
            font: 12px/23px "Segoe UI" , Arial, sans-serif;
        }
        .FullPermission
        {
            float: left;
            width: 130px;
            display: table-cell;
            font: 12px/23px "Segoe UI" , Arial, sans-serif;
        }
    </style>
    <script src="/Scripts/jquery-1.4.3.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function CallWebMethodSuccess(results, context, methodName) {
            switch (methodName) {
                case "GetData":
                    {
                        LoadDataCallback(results, context, methodName);
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
            }
        }

        function CallWebMethodFailed(results, context, methodName) {
        }

        function rcbRoleSelectedIndexChanged(sender, eventArgs) {
            LoadData();
        }
        function LoadData() {
            var rcbRole = $find("<%= rcbRole.ClientID %>");
            var idRole = rcbRole.get_selectedItem().get_value();
            PageMethods.GetData(idRole, CallWebMethodSuccess, CallWebMethodFailed);
        }
        function LoadDataCallback(results, context, methodName) {
            var _data = Sys.Serialization.JavaScriptSerializer.deserialize(results);
            DrawData(_data.FunctionCategoryList, _data.FunctionList, _data.PermissionList);
        }
        function DrawData(functionCategoryList, functionList, permissionList) {
            Refresh();
            var $container = $("#container");
            for (var index in functionCategoryList) {
                var $row = $("<div></div>");
                $row.addClass("Row");
                if (index == 0) {
                    $row.addClass("FirstRow");
                }
                $container.append($row);

                var $functionCategory = $("<div></div>");
                $functionCategory.text(functionCategoryList[index].Name);
                $functionCategory.addClass("FunctionCategory");
                $row.append($functionCategory);
                DrawFunction(functionCategoryList[index].Id, functionList[index], permissionList[index], $row);
            }
        }
        function DrawFunction(functionCategoryId, functions, permissions, $row) {
            var flag = true;
            var $functionContainer = $("<div></div>");
            $functionContainer.addClass("FunctionContainer");
            $row.append($functionContainer);

            var $normalFunction = $("<div></div>");
            $normalFunction.addClass("NormalFunction");
            $functionContainer.append($normalFunction);
            for (var index in functions) {
                var $function = $("<div></div>");
                $function.addClass("Function");
                $normalFunction.append($function);

                var $checkbox = $(String.format("<input  id='{0}' type=checkbox name='{1}'/>", functions[index].Id, 'FunctionCategory_' + functionCategoryId));
                $checkbox.attr("checked", permissions[index]);
                $checkbox.change(function () {
                    TrackStatus(functionCategoryId);
                    UpdatePermission($(this).attr("Id"), $(this).attr("checked"));
                });
                $function.append($checkbox);

                var $span = $("<span></span>");
                $span.text(functions[index].Name);
                $function.append($span);
                if (!permissions[index])
                    flag = false;
            }
            /*Toan quyen*/
            var $toanquyen = $("<div></div>");
            $toanquyen.addClass("FullPermission");
            $functionContainer.append($toanquyen); /*$row*/

            var $checkbox = $(String.format("<input  name='{0}' type=checkbox />", 'FunctionCategory_' + functionCategoryId));
            $checkbox.attr("checked", flag);
            $checkbox.change(function () {
                FullPermissionChangeStatus($(this).attr("Name"), $(this).attr("checked"));
            });
            $toanquyen.append($checkbox);

            var $span = $("<span></span>");
            $span.text('Toàn quyền');
            $toanquyen.append($span);

        }
        function TrackStatus(functionCategoryId) {
            var flag = true;
            var ctrlList = document.getElementsByName('FunctionCategory_' + functionCategoryId);
            var i = 0;
            for (; i < ctrlList.length - 1; i++) {
                $ctrl = $(ctrlList[i]);
                if (!$ctrl.attr("checked")) {
                    flag = false;
                }
            }
            $ctrlToanQuyen = $(ctrlList[i]);
            $ctrlToanQuyen.attr("checked", flag);
        }
        function UpdatePermission(functionId, value) {
            var rcbRole = $find("<%= rcbRole.ClientID %>");
            PageMethods.Update(rcbRole.get_selectedItem().get_value(), functionId, value, CallWebMethodSuccess, CallWebMethodFailed);
        }
        function FullPermissionChangeStatus(name, value) {
            var ctrlList = document.getElementsByName(name);
            for (var j = 0; j < ctrlList.length - 1; j++) {
                var $ctrl = $(ctrlList[j]);
                $ctrl.attr("checked", value);
                UpdatePermission($ctrl.attr("Id"), value);
            }
        }
        function Refresh() {
            var $container = $("#container");
            $container.empty();
        }
        function pageLoad() {
            Initialize();
            LoadData();
        }
        function RoleNew() {
            PageMethods.RoleNew(CallWebMethodSuccess, CallWebMethodFailed);
        }
        function RoleNewCallback(results, context, methodName) {
            dialogMethod = "RoleNew";
            rwRole.setSize(600, 280);
            rwRole.setUrl("Role/RoleDetail.aspx");
            rwRole.show();
        }
        function RoleAddCallback(results, context, methodName) {
            var role = Sys.Serialization.JavaScriptSerializer.deserialize(results);
            var rcbRole = $find("<%= rcbRole.ClientID %>");
            var comboItem = new Telerik.Web.UI.RadComboBoxItem();
            comboItem.set_text(role.Name);
            comboItem.set_value(role.Id)
            rcbRole.trackChanges();
            rcbRole.get_items().add(comboItem);
            rcbRole.commitChanges();
        }
        function RoleEdit() {
            var rcbRole = $find("<%= rcbRole.ClientID %>");
            var idRole = rcbRole.get_selectedItem().get_value();
            PageMethods.RoleEdit(idRole, CallWebMethodSuccess, CallWebMethodFailed);
        }
        function RoleEditCallback(results, context, methodName) {
            dialogMethod = "RoleEdit";
            rwRole.setSize(600, 280);
            rwRole.setUrl("Role/RoleDetail.aspx");
            rwRole.show();
        }

        function RoleUpdateCallback(results, context, methodName) {
            var role = Sys.Serialization.JavaScriptSerializer.deserialize(results);
            var rcbRole = $find("<%= rcbRole.ClientID %>");
            var curNode = rcbRole.get_selectedItem();
            rcbRole.trackChanges();
            curNode.set_text(role.Name);
            curNode.select();
            rcbRole.commitChanges();
        }
        function RoleDelete() {

            radconfirm("Are you sure!", RoleDeleteConfirm, 250, 100, null, "Warning");
        }
        function RoleDeleteConfirm(args) {
            if (!args)
                return;
            var rcbRole = $find("<%= rcbRole.ClientID %>");
            var idRole = rcbRole.get_selectedItem().get_value();
            PageMethods.RoleDelete(idRole, CallWebMethodSuccess, CallWebMethodFailed);
        }
        function RoleDeleteCallback(results, context, methodName) {
            var rcbRole = $find("<%= rcbRole.ClientID %>");
            var curNode = rcbRole.get_selectedItem();
            rcbRole.trackChanges();
            rcbRole.get_items().remove(curNode);
            rcbRole.get_items()._array[0].select();
            rcbRole.commitChanges();
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
            }
        }
        var rwRole;
        function Initialize() {
            rwRole = $find("<%=rwRole.ClientID%>");
        }     
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindow ID="rwRole" runat="server" Modal="True" VisibleStatusbar="False"
        OnClientClose="RadWindowRoleClose">
    </telerik:RadWindow>
    <div style="width: 835px;">
        <div style="clear: both; float: left; width: 100%; padding-top: 30px; padding-bottom: 5px;
            height: 25px; padding-left: 30px; text-align: center; font: 12px/23px 'Segoe UI', Arial, sans-serif;">
            <div style="float: left">
                Nhóm người dùng:<telerik:RadComboBox OnClientSelectedIndexChanged="rcbRoleSelectedIndexChanged"
                    ID="rcbRole" DataValueField="Id" DataTextField="Name" runat="server" Width="200px">
                </telerik:RadComboBox>
            </div>
        </div>
        <div id="container" class="Authentication_Container">
        </div>
    </div>
</asp:Content>
