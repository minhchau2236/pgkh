<%@ Page EnableViewState="false" Title="<%# Resources.Site.ChangePassword %>" Language="C#" MasterPageFile="~/Systems/DialogMasterPage.Master" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="PSCPortal.Systems.Security.ResetPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CSS/MasterPage.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
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


            var txtNewPassword = document.getElementById("<%= txtNewPass.ClientID %>");
            var txtConfirmPassword = document.getElementById("<%= txtPassConfirm.ClientID %>");
			if (txtNewPassword.value != txtConfirmPassword.value) {
			    alert("<%= Resources.Site.PasswordNotMatch %>");
			    return;
			}
			PageMethods.Save( txtNewPassword.value, CallWebMethodSuccess, CallWebMethodFailed);
        }
        function SaveCallback(results, context, methodName) {
            if (results)
                alert("<%= Resources.Site.PasswordUpdateSuccess %>");
            else
                alert("<%= Resources.Site.PasswordUpdateFail %>");
            var oWnd = GetRadWindow();
            oWnd.close(true);  
        }
        function Cancel() {
            var oWnd = GetRadWindow();
            oWnd.close(null);  
        }
        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table style="width:100%;" align="center" cellpadding="4px;">
	<tr>
    	<td style="width:40%;" class="textinput" align="right"><%= Resources.Site.NewPassword %>:</td>
        <td style="width:60%"><asp:TextBox ID="txtNewPass" TextMode="Password" runat="server" size="25"></asp:TextBox></td>
    </tr>
    <tr>
    	<td style="width:40%;" class="textinput" align="right"><%= Resources.Site.NewPasswordConfirm%>:</td>
        <td style="width:60%"><asp:TextBox ID="txtPassConfirm" TextMode="Password" runat="server" size="25"></asp:TextBox></td>
    </tr> 
    <tr align="center">
        <td colspan="2"><img src="/Systems/CMS/Image/line.jpg" width="438" height="1" alt="" /></td>
    </tr>
    <tr>
        <td colspan="2">
            <table style="width:15%" align="center">
                <tr>
                    <td style="width:50%;"><a href="javascript:void(0)" onclick="Save();" class="submit"><%= Resources.Site.Save %></a></td>
                    <td style="width:50%;"><a href="javascript:void(0)" onclick="Cancel();" class="submit"><%= Resources.Site.Cancel %></a></td>
                </tr>
            </table>
        </td>
    </tr>
</table>    
</asp:Content>
