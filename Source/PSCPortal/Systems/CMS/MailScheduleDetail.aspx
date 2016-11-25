<%@ Page Title="" Language="C#" MasterPageFile="~/Systems/DialogMasterPage.Master" AutoEventWireup="true" CodeBehind="MailScheduleDetail.aspx.cs" Inherits="PSCPortal.Systems.CMS.MailScheduleDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Systems/CMS/Scripts/MailScheduleDetail.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function initialize() {
            strWarning = "<%= Resources.Site.Warning %>";
            txtName = document.getElementById("<%= txtName.ClientID %>");
            txtMail = document.getElementById("<%= txtMail.ClientID %>");
        }     
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table style="width:100%;" align="center" cellpadding="4px;">
	<tr>
    	<td style="width:40%;" class="textinput" align="right"><%= Resources.Site.Id %>:</td>
        <td style="width:60%"><asp:TextBox Enabled="false" ID="txtId" runat="server" Width="200px"></asp:TextBox></td>
    </tr>
    <tr>
    	<td style="width:40%;" class="textinput" align="right"><%= Resources.Site.Name %>:</td>
        <td style="width:60%"><asp:TextBox ID="txtName" runat="server" Width="200px"></asp:TextBox></td>
    </tr> 
    <tr>
    	<td style="width:40%;" class="textinput" align="right">Mail:</td>
        <td style="width:60%"><asp:TextBox ID="txtMail" runat="server" Width="200px"></asp:TextBox></td>
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
