<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SendArticle.aspx.cs" Inherits="PSCPortal.CMS.SendArticle" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <base target="_self"/>
    <title>Untitled Page</title>
    <script type="text/javascript">
        function OnLoad()
        {
            document.getElementById("<%=HiddenField1.ClientID %>").value=window.dialogArguments;
        }
       
        function OnCancel()
        {
            window.close();
        }
    </script>
</head>
<body onload="OnLoad();">
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="width:450px;height:300px">
       
        <tr>
            <td  style="font-family: tahoma, verdana;font-weight: bold;font-size: 12px;	COLOR: #028cc0; ">
                &nbsp;&nbsp;&nbsp;
                Gửi bài viết cho bạn bè
            </td>
        
    </tr>
    <tr>
        <td align="right" style="padding-right:15px; color: #737373; font-family:Arial, tahoma, verdana;font-size:11px;font-weight:bold;" >
            Tên người gửi:</td>
        <td align="left">
            <asp:TextBox ID="txtSenderName" runat="server" Width="200px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="txtSenderName" 
                ErrorMessage="Tên người gửi không được rỗng" 
                ValidationGroup="SendArticle" ToolTip="Họ và tên không được trống">(*)</asp:RequiredFieldValidator>           
        </td>
        
    </tr>
    <tr>
        <td align="right" style="color: #737373; padding-right:15px; font-family:Arial, tahoma, verdana;font-size:11px;font-weight:bold;">
            Email người gửi:</td>
        <td align="left">
            <asp:TextBox ID="txtSenderEmail" runat="server" Width="200px"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                ControlToValidate="txtSenderEmail" ErrorMessage="Email người gửi không hợp lệ" 
                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="SendArticle" 
                ToolTip="Email người gửi không hợp lệ">(*)</asp:RegularExpressionValidator>            
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ControlToValidate="txtSenderEmail" 
                ErrorMessage="Email người gửi không được để trống" ValidationGroup="SendArticle" 
                ToolTip="Email người gửi không được để trống">(*)</asp:RequiredFieldValidator>
        </td>
       
    </tr>
     
   
    <tr>
        <td align="right" style="color: #737373; padding-right:15px; font-family:Arial, tahoma, verdana;font-size:11px;font-weight:bold;">
            Email người nhận:</td>
        <td align="left">
            <asp:TextBox ID="txtReceiptEmail" runat="server" Width="200px"></asp:TextBox>
             <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                ControlToValidate="txtReceiptEmail" ErrorMessage="Email người nhận không hợp lệ" 
                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="SendArticle" 
                ToolTip="Email người nhận không hợp lệ">(*)</asp:RegularExpressionValidator>            
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ControlToValidate="txtReceiptEmail" 
                ErrorMessage="Email người nhận không được để trống" ValidationGroup="SendArticle" 
                ToolTip="Email người nhận không được để trống">(*)</asp:RequiredFieldValidator>
        </td>
      
    </tr>
    <tr >
        <td align="right" style="color: #737373; padding-right:15px; font-family:Arial, tahoma, verdana;font-size:11px;font-weight:bold;">
            Tiêu đề:
          </td>
           <td align="left">
            <asp:TextBox ID="txtTitle" runat="server" Width="200px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                ControlToValidate="txtTitle" 
                ErrorMessage="Tiêu đề không được rỗng" 
                ValidationGroup="SendArticle" ToolTip="Họ và tên không được trống">(*)</asp:RequiredFieldValidator>           
        </td>
    </tr>
    <tr>
        <td align="right" style="color: #737373; padding-right:15px;padding-top:3px;padding-bottom:0px; font-family:Arial, tahoma, verdana;font-size:11px;font-weight:bold;vertical-align:top">
            Lời nhắn:
          </td>
          <td align="left" style="vertical-align:top;padding-top:3px">
            <asp:TextBox ID="txtMessage"  runat="server" Width="200px" Height="100px" TextMode="MultiLine"></asp:TextBox></td>
    </tr>
    <tr>
        <td colspan="2" align="center" height="35px" style="padding-right:15%">            
            <asp:Button ID="btnSend" runat="server" Text="Gửi" Width="70px" 
                    onclick="btnSend_Click" ValidationGroup="SendArticle" Height="25px" />
           
            <input id="btnCancel" onclick="OnCancel();" type="button" value="Hủy Bỏ" style="height:25px;width:70px"  />
        </td>
    </tr>
    <tr>
        <td align="center" colspan="3" style="font-family: Arial; font-size: small; color: #FF0000">
            <asp:Label runat="server" ID="lbthongbao" Visible="false"></asp:Label>
        </td>
    </tr>
</table>
<asp:HiddenField ID="HiddenField1" runat="server" />
   <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
    ShowMessageBox="True" ShowSummary="False" ValidationGroup="SendArticle" />
    </div>
    </form>
</body>
</html>
