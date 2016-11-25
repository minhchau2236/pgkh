<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserLogin.aspx.cs" Inherits="PSCPortal.Modules.CMS.UserLogin" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="/Scripts/Utility.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function CallWebMethodSuccess(results, context, methodName) {
            switch (methodName) {
                case "Save":
                    {
                        SaveCallback(results, context, methodName);
                    }
                    break;
            }
        }
        function CallWebMethodFailed(results, context, methodName) {
            alert(results._message);
        }
        function Save() {

            var UserName = document.getElementById("<%= UserName.ClientID %>");
            var Password = document.getElementById("<%= Password.ClientID %>");
            PageMethods.Save(UserName.value, Password.value, CallWebMethodSuccess, CallWebMethodFailed);
        }
        function SaveCallback(results, context, methodName) {
            if (results) {
                var oWnd = GetRadWindow();
                oWnd.close(true);
            }
            else {
                alert("Đăng nhập không thành công.");
            }
        }
        function Cancel() {
            var oWnd = GetRadWindow();
            oWnd.close(false);
        }
    </script>
    <style type="text/css">
        .form
        {
            width: 350px;
            font: 12px Arial;
            color: Black;
            float: left;
        }
        .form p
        {
            padding: 5px;
        }
        .form p label
        {
            width: 100px;
            float: left;
            text-align:right;
            margin-top:4px;
        }
        .form p input
        {
            width: 170px;
            float: left;
            margin-left: 5px;
        }
        .form a
        {
            color: #000;
            text-decoration: none;
            font-weight: bold;
            padding-right:5px;
        }
        .form a:hover
        {
            color: orange;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager EnablePageMethods="true" ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <div class="form">
         <p>
            <label>
                UserName:</label>
            <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                ErrorMessage="UserName require" ToolTip="UserName require" ValidationGroup="Login1">*</asp:RequiredFieldValidator>
        </p>
        <p>
            <label>
                Password:</label>
            <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                ErrorMessage="Password require" ToolTip="Password require" ValidationGroup="Login1">*</asp:RequiredFieldValidator>
        </p>
        <p style="text-align:center">
            <a href="javascript:void(0)" onclick="Save();" class="submit">Login</a> <a href="javascript:void(0)"
                onclick="Cancel();" class="submit">Cancel</a>
        </p>
    </div>
    </form>
</body>
</html>
