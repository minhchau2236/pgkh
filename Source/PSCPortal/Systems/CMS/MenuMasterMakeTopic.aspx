<%@ Page Title="<%# Resources.Site.MenuMakeTopic %>" Language="C#" MasterPageFile="~/Systems/DialogMasterPage.Master" AutoEventWireup="true" CodeBehind="MenuMasterMakeTopic.aspx.cs" Inherits="PSCPortal.Systems.CMS.MenuMasterMakeTopic" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/MenuMasterMakeTopic.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">
    function initialize(){
	    rcbPage = $find("<%=rcbPage.ClientID %>");
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table style="width:100%;" align="center" cellpadding="4px;">    
    <tr>
    	<td style="width:40%;" class="textinput" align="right"><%= Resources.Site.PageDisplay %>:</td>
        <td style="width:60%">
            <telerik:RadComboBox Width="200px" DataTextField="Name" DataValueField="Id" ID="rcbPage" runat="server">
            </telerik:RadComboBox>
        </td>
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
