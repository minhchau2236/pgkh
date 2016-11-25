<%@ Page Language="C#" MasterPageFile="~/Systems/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="SystemPermissionManage.aspx.cs" Inherits="PSCPortal.Systems.Security.SystemPermissionManage"
    Title="Untitled Page" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .Authentication_Container
        {
            clear: both;
            width: 100%;
            overflow: auto;
            padding-top: 10px;
        }
        .Authentication_Title
        {
            margin: 0 auto;
            padding: 0;
            width: 100%;
        }
        .Authentication_Title_Item
        {
            margin: 0 auto;
            padding: 0 5px 0 5px;
            float: left;
            border-color: #004D71;
            border-style: solid;
            border-width: 1px 1px 1px 0;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 11px;
            color: #FFF;
            font-size: bold;
            font-weight: bold;
            width: 90px;
            height: 40px;
            text-align: center;
            background-color: #004D71;
        }
        .Authentication_Row
        {
            clear: both;
            width: 100%;
        }
        .Authentication_RoleName
        {
            float: left;
            width: 90px;
            border-color: #004D71;
            border-style: solid;
            border-width: 0 1px 1px 1px;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 11px;
            color: #333;
            font-size: 11px;
            font-weight: normal;
            text-align: left;
            height: 40px;
            padding-left: 10px;
        }
        .Authentication_Row_Item
        {
            margin: 0 auto;
            padding: 0 5px 0 5px;
            float: left;
            border-color: #004D71;
            border-style: solid;
            border-width: 0 1px 1px 0;
            width: 90px;
            text-align: center;
            height: 40px;
        }
    </style>

    <script src="/Scripts/jquery-1.4.3.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        function CallWebMethodSuccess(results, context, methodName) {
            switch (methodName) {
                case "GetFunctions":
                    {
                        LoadFunctionCallback(results, context, methodName);
                    }
                    break;
                case "Save":
                    {
                        SaveCallback(results,context,methodName);
                    }
                    break;    
            }
        }
        function CallWebMethodFailed(results, context, methodName) {
        }
        function rcbFunctionCategorySelectedIndexChanged(sender, eventArgs) {
            LoadFunction();
        }
        function LoadFunction() {
            var rcbFunctionCategory = $find("<%= rcbFunctionCategory.ClientID %>");
            var idCat = rcbFunctionCategory.get_selectedItem().get_value();
            PageMethods.GetFunctions(idCat, CallWebMethodSuccess, CallWebMethodFailed);
        }
        function LoadFunctionCallback(results, context, methodName){    
            var authenList = Sys.Serialization.JavaScriptSerializer.deserialize(results);
            listRole = authenList.RoleList;
            DrawTitle(authenList.FunctionList);    
            DrawData(authenList.FunctionList, authenList.RoleList, authenList.AuthenticationList);
        }
        function DrawTitle(functionList)
        {
            Refesh();
            var $container=$("#container");
            var $tieude=$("<div></div>")
            $tieude.addClass("Authentication_Title");
            $container.append($tieude);
            
            $firstColumn=$("<div></div>");
            $firstColumn.addClass("Authentication_Title_Item");
            $firstColumn.text("");
            $firstColumn.css({'border-left-width':'1px'});
            $tieude.append($firstColumn);
            
            for( var index in functionList)
            {
                var $item=$("<div></div>");
                $item.text(functionList[index].Name);
                $item.addClass("Authentication_Title_Item");
                $tieude.append($item);
            }
            
            //Toan quyen
            var $item=$("<div></div>");
            $item.text('Toàn quyền');
            $item.addClass("Authentication_Title_Item");
            $tieude.append($item);        
        }
        function DrawData(functionList, roleList, authenticationList)
        {
            for ( var i = 0 ; i < roleList.length ; i++)
            {
                AddRowForRole(roleList[i], functionList, authenticationList[i]);
            }
        }
        function AddRowForRole(role, functionList, arrAuthenticationList){
            var flag=true;
            
            var $container=$("#container");
            $row = $("<div></div>");
            $row.addClass("Authentication_Row");
            $container.append($row);
            
            var $roleName=$("<div></div>");
            $roleName.text(role.Name);
            $roleName.addClass("Authentication_RoleName");
            $row.append($roleName);
            
            for( var i = 0 ; i < functionList.length ; i++)
            {
                var $item=$("<div></div>");
                $item.addClass("Authentication_Row_Item");
                $row.append($item);
                
                var $checkbox = $(String.format("<input name='{0}' type=checkbox />", role.Name));
                $checkbox.attr("checked", arrAuthenticationList[i]);
                $checkbox.change(function(){
                    TrackStatus(role.Name);
                });
                $item.append($checkbox);
                
                if(!arrAuthenticationList[i])
                    flag=false;
            }
            // Toan quyen
            var $itemFullPermission=$("<div></div>");
            $itemFullPermission.addClass("Authentication_Row_Item");
            $row.append($itemFullPermission);
            
            var $checkboxToanQuyen = $(String.format("<input name='{0}' type=checkbox />", role.Name));
            $checkboxToanQuyen.attr("checked",flag);
            $checkboxToanQuyen.change(function(){
                FullPermissionChangeStatus($(this).attr("Name"),$(this).attr("checked"));
            });
            $itemFullPermission.append($checkboxToanQuyen);
        }

        function TrackStatus(roleName){
            var flag=true;
            var ctrlList = document.getElementsByName(roleName);
            var i=0;
            for ( ; i < ctrlList.length -1 ; i++)
            {
                $ctrl=$(ctrlList[i]);
                if(!$ctrl.attr("checked"))
                {
                    flag=false;
                }
            }
            $ctrlToanQuyen=$(ctrlList[i]);
            $ctrlToanQuyen.attr("checked",flag);
        }

        function FullPermissionChangeStatus(roleName,value){
            var ctrlList = document.getElementsByName(roleName);
            for(var j = 0; j < ctrlList.length -1 ; j++)
            {
                var $ctrl = $(ctrlList[j]);
                $ctrl.attr("checked",value);            
            }
        }
        function Refesh()
        {
             var $container=$("#container");
             $container.empty();
        }
        function Save()
        {    
            var result = new Array();    
            for(var i = 0 ; i < listRole.length ; i++)
            {                
                var ctrlList = document.getElementsByName(listRole[i].Name);
                result[i] = new Array();
                for(var j = 0; j < ctrlList.length -1 ; j++)
                {
                    var $ctrl = $(ctrlList[j]);
                    result[i][j] = $ctrl.attr("checked");            
                }
            }
            PageMethods.Save(result, CallWebMethodSuccess, CallWebMethodFailed);    
        }
        function SaveCallback(results, context, methodName){
            alert("Cập nhật thành công");
        }
        function Cancel() {
            var oWnd = GetRadWindow();
            oWnd.close(false); 
        }
        function pageLoad() {
            LoadFunction();
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div style="width: 400px;">
            <div style="float: left; width: 150px;">
                Loại chức năng:</div>
            <div style="float: left; width: 250px;">
                <telerik:RadComboBox OnClientSelectedIndexChanged="rcbFunctionCategorySelectedIndexChanged"
                    ID="rcbFunctionCategory" DataValueField="Id" DataTextField="Name" runat="server"
                    Width="200px">
                </telerik:RadComboBox>
            </div>
        </div>
        <div id="container" class="Authentication_Container">
        </div>
        <div style="clear: both; width: 100%; padding-top: 10px" align="center">
            <a href="javascript:void(0)" onclick="Save();" class="submit">Lưu</a>
        </div>
    </div>
</asp:Content>
