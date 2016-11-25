<%@ Page EnableViewState="false" Title="<%#Resources.Site.ChangePassword %>" Language="C#" MasterPageFile="~/Systems/MasterPage.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="PSCPortal.Systems.Security.ChangePassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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

            var txtPassword = document.getElementById("<%= txtOldPass.ClientID %>");
            var txtNewPassword = document.getElementById("<%= txtNewPass.ClientID %>");
            var txtConfirmPassword = document.getElementById("<%= txtPassConfirm.ClientID %>");
			if (txtNewPassword.value != txtConfirmPassword.value) {
			    alert("password confirm không đúng");
			    return;
			}
			PageMethods.Save(txtPassword.value, txtNewPassword.value, CallWebMethodSuccess, CallWebMethodFailed);
        }
        function SaveCallback(results, context, methodName) {
            window.returnValue = '';
            if (results == true) {
                window.returnValue = "<%=Resources.Site.PasswordUpdateSuccess %>";
                alert(window.returnValue);              
                window.location.href = '/Login.aspx';
            }
            else {
                window.returnValue = "<%=Resources.Site.PasswordUpdateFail %>";
                alert(window.returnValue);
            }
          
        }
        function Cancel() {
            window.returnValue = false;
            window.location.href = '/Login.aspx';
        }        
        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div style="width:718px; float:left; padding-left:16px; padding-top:25px;">
	<div style="width:408px; float:left; margin-right:12px;">
    	<div class="left_box"></div>
        <div style="width: 388px;" class="box"><div class="title_box" align="center"><%=Resources.Site.ChangePassword %></div></div>
        <div class="right_box"></div>
        <div style="background-color:#edeeef; float:left; width:408px; padding-top:19px; padding-bottom:19px;">
            <table style="width:100%;" align="center" cellpadding="4px;">
	            <tr>
    	            <td style="width:30%;padding: 4px 4px" class="textinput" align="right"><%=Resources.Site.Password %>:</td>
                    <td style="width:70%;padding: 4px 0px"><asp:TextBox ID="txtOldPass" runat="server" TextMode="Password" Width="200px"></asp:TextBox></td>
                </tr>
                <tr>
    	            <td style="width:30%;padding: 4px 4px" class="textinput" align="right"><%=Resources.Site.NewPassword%>:</td>
                    <td style="width:70%;padding: 4px 0px"><asp:TextBox ID="txtNewPass" TextMode="Password" runat="server" Width="200px"></asp:TextBox></td>
                </tr> 
                <tr>
    	            <td style="width:30%;padding: 4px 4px" class="textinput" align="right"><%=Resources.Site.ConfirmNewPassword%>:</td>
                    <td style="width:70%;padding: 4px 0px"><asp:TextBox ID="txtPassConfirm" TextMode="Password" runat="server" Width="200px"></asp:TextBox></td>
                </tr>     
                <tr align="center">
                    <td colspan="2"><img src="/Systems/CMS/Image/line.jpg" width="300" height="1" alt="" /></td>
                </tr>
                <tr>
                    <td style="width:50%;padding: 4px 4px" align="right">   
                        <a href="javascript:void(0)" onclick="Save();" class="submit"><%=Resources.Site.Save%></a>                 
                    </td>
                    <td style="width:50%;padding: 4px 0px">
                        <a href="javascript:void(0)" onclick="Cancel();" class="submit"><%=Resources.Site.Cancel%></a>
                    </td>                    
                </tr>
            </table>                  	
        </div>
        <div class="bottombox_left">&nbsp;</div>
        <div style="width: 388px;" class="bottombox_center">&nbsp;</div>
        <div class="bottombox_right">&nbsp;</div>      
    </div>	
</div>
</asp:Content>
