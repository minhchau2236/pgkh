<%@ Page EnableViewState="false" Title="<%# Resources.Site.GroupDetail %>" Language="C#" MasterPageFile="~/Systems/DialogMasterPage.Master" AutoEventWireup="true" CodeBehind="RoleDetail.aspx.cs" Inherits="PSCPortal.Systems.Security.RoleDetail" %>
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
			var txtDescription = document.getElementById("<%= txtDescription.ClientID %>");
            PageMethods.Save(txtName.value, txtDescription.value, CallWebMethodSuccess, CallWebMethodFailed);
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
    	<td style="width:40%;" class="textinput" align="right"><%= Resources.Site.Description%>:</td>
        <td style="width:60%"><asp:TextBox ID="txtDescription" runat="server" Width="200px"></asp:TextBox></td>
    </tr>                
    <tr align="center">
        <td colspan="2"><img src="/Systems/CMS/Image/line.jpg" width="438" height="1" alt="" /></td>
    </tr>
    <tr>
        <td colspan="2">
            <table style="width:15%" align="center">
                <tr>
                    <td style="width:50%;"><a href="javascript:void(0)" onclick="Save();" class="submit"><%= Resources.Site.Save%></a></td>
                    <td style="width:50%;"><a href="javascript:void(0)" onclick="Cancel();" class="submit"><%=Resources.Site.Cancel%></a></td>
                </tr>
            </table>
        </td>
    </tr>
</table>    
</asp:Content>
