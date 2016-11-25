<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Display_SVTL.ascx.cs" Inherits="PSCPortal.Portlets.MenuRad.Display_SVTL" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<link href="/Portlets/MenuRad/Menu.Green_SVTL.css" rel="stylesheet" type="text/css" />
<telerik:RadMenu Style="z-index: 1; width: 100%;" ID="RadMenu" runat="server" DataFieldID="Id" DataTextField="Name"
    DataNavigateUrlField="NavigationURL" Height="40px" EnableEmbeddedSkins="false"
    Skin="Green">
</telerik:RadMenu>

