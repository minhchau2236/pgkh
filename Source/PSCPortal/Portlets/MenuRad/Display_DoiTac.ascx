<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Display_DoiTac.ascx.cs" Inherits="PSCPortal.Portlets.MenuRad.Display_DoiTac" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<link href="/Portlets/MenuRad/Menu.Green_DoiTac.css" rel="stylesheet" type="text/css" />
<telerik:RadMenu Style="z-index: 1; width: 100%;" ID="RadMenu" runat="server" DataFieldID="Id" DataTextField="Name"
    DataNavigateUrlField="NavigationURL" Height="40px" EnableEmbeddedSkins="false"
    Skin="Yellow">
</telerik:RadMenu>

