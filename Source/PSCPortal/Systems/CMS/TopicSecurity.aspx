<%@ Page EnableViewState="false" Title="<%# Resources.Site.TopicGrant %>" Language="C#"
    MasterPageFile="~/Systems/DialogMasterPage.Master" AutoEventWireup="true" CodeBehind="TopicSecurity.aspx.cs"
    Inherits="PSCPortal.Systems.CMS.TopicSecurity" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/Utility.js" type="text/javascript"></script>
    <script src="/Systems/CMS/Scripts/TopicSecurity.js" type="text/javascript"></script>
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
