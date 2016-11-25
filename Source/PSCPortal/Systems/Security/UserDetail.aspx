<%@ Page EnableViewState="false" Title="<%# Resources.Site.UserDetail %>" Language="C#" MasterPageFile="~/Systems/DialogMasterPage.Master" AutoEventWireup="true" CodeBehind="UserDetail.aspx.cs" Inherits="PSCPortal.Systems.Security.UserDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Systems/CSS/DialogMasterPage.css" rel="stylesheet" type="text/css" />
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
			var txtName = document.getElementById("<%= txtName.ClientID %>");
			var txtPassword = document.getElementById("<%= txtPassword.ClientID %>");
			var txtPasswordConfirm = document.getElementById("<%= txtPasswordConfirm.ClientID %>");
			var txtEmail = document.getElementById("<%= txtEmail.ClientID %>");
			var txtPasswordQuestion = document.getElementById("<%= txtPasswordQuestion.ClientID %>");
			var txtPasswordAnswer = document.getElementById("<%= txtPasswordAnswer.ClientID %>");
			var chkIsAdministrator = document.getElementById("<%= chkIsAdministrator.ClientID %>");
			if (txtPassword.value != txtPasswordConfirm.value) {
			    alert("Không đúng mật khẩu!");
			    return;
			}

			PageMethods.Save(txtName.value, txtPassword.value, txtEmail.value, txtPasswordQuestion.value, txtPasswordAnswer.value, chkIsAdministrator.checked, CallWebMethodSuccess, CallWebMethodFailed);
        }
        function SaveCallback(results, context, methodName) {
            var oWnd = GetRadWindow();
            oWnd.close(true);  
        }
        function Cancel() {
            var oWnd = GetRadWindow();
            oWnd.close(false);  
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table style="width:100%;" align="center" cellpadding="4px;">
	<tr>
    	<td style="width:40%;" class="textinput" align="right"><%= Resources.Site.Id %>:</td>
        <td style="width:60%"><asp:TextBox ID="txtId" Enabled="false" runat="server" Width="200px"></asp:TextBox></td>
    </tr>
    <tr>
    	<td style="width:40%;" class="textinput" align="right"><%= Resources.Site.Name %>:</td>
        <td style="width:60%"><asp:TextBox ID="txtName" runat="server" Width="200px"></asp:TextBox></td>
    </tr> 
    <tr>
    	<td style="width:40%;" class="textinput" align="right"><%= Resources.Site.Password %>:</td>
        <td style="width:60%"><asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="200px"></asp:TextBox></td>
    </tr> 
    <tr>
    	<td style="width:40%;" class="textinput" align="right"><%= Resources.Site.ConfirmPassword %>:</td>
        <td style="width:60%"><asp:TextBox ID="txtPasswordConfirm" runat="server" TextMode="Password" Width="200px"></asp:TextBox></td>
    </tr> 
    <tr>
    	<td style="width:40%;" class="textinput" align="right">Email:</td>
        <td style="width:60%"><asp:TextBox ID="txtEmail" runat="server" Width="200px"></asp:TextBox></td>
    </tr>  
    <tr>
    	<td style="width:40%;" class="textinput" align="right"><%= Resources.Site.SecretQuestion %>:</td>
        <td style="width:60%"><asp:TextBox ID="txtPasswordQuestion" runat="server" Width="200px"></asp:TextBox></td>
    </tr>            
    <tr>
    	<td style="width:40%;" class="textinput" align="right"><%= Resources.Site.SecretAnswer %>:</td>
        <td style="width:60%"><asp:TextBox ID="txtPasswordAnswer" runat="server" Width="200px"></asp:TextBox></td>
    </tr>  
    <tr>
    	<td style="width:40%;" class="textinput" align="right">Is Administrator</td>
        <td style="width:60%"><asp:CheckBox ID="chkIsAdministrator" runat="server" Width="200px"></asp:CheckBox></td>
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
