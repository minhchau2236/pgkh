<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Display.ascx.cs" Inherits="PSCPortal.Portlets.MenuRad.Display" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<link href="/Portlets/MenuRad/Menu.Green_Khoa.css" rel="stylesheet" type="text/css" />
<telerik:RadMenu Style="z-index: 1; " ID="RadMenu" runat="server" DataFieldID="Id" DataTextField="Name"
    DataNavigateUrlField="NavigationURL" Height="36px" EnableEmbeddedSkins="false"
    Skin="Blue">
</telerik:RadMenu>

