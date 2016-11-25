<%@ Page Title="<%# Resources.Site.MenuMasterGrant %>" Language="C#" MasterPageFile="~/Systems/DialogMasterPage.Master" AutoEventWireup="true" CodeBehind="MenuMasterSecurity.aspx.cs" Inherits="PSCPortal.Systems.CMS.MenuMasterSecurity" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/Utility.js" type="text/javascript"></script>
    <script src="/Systems/CMS/Scripts/MenuMasterSecurity.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-1.4.3.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="container" class="Authentication_Container">
    </div>
    <div style="clear: both; width: 100%;" align="center">
        <a href="javascript:void(0)" onclick="Save();" class="submit">
            <%= Resources.Site.Save %></a>&nbsp;&nbsp;<a href="javascript:void(0)" onclick="Cancel();" class="submit">
                <%= Resources.Site.Cancel %></a>
    </div>
</asp:Content>
