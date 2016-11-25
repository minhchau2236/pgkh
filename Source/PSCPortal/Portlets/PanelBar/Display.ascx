<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Display.ascx.cs" Inherits="PSCPortal.Portlets.PanelBar.Display" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<link href="/Portlets/PanelBar/PanelBar.Green.css" rel="stylesheet" type="text/css" />
<div class="menu_bar">
    <telerik:RadPanelBar ExpandMode="SingleExpandedItem" Width="245px" ID="menu" runat="server" DataValueField="Id" DataTextField="Name"
        EnableEmbeddedSkins="false" Skin="Green" DataNavigateUrlField="NavigationURL"
        PersistStateInCookie="true">
        <ExpandAnimation Type="OutSine" Duration="300" />
        <CollapseAnimation Type="InSine" Duration="300" />
        <DataBindings>
            <telerik:RadPanelItemBinding Depth="0"  Expanded="false" />
        </DataBindings>
    </telerik:RadPanelBar>
</div>