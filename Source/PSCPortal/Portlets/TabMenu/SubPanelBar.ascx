<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SubPanelBar.ascx.cs" Inherits="PSCPortal.Portlets.TabMenu.SubPanelBar" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<link href="/Portlets/TabMenu/SubSkin.css" rel="stylesheet" />
<div style="margin-left: 27px; margin-top: 20px;">
    <telerik:RadPanelBar ExpandMode="SingleExpandedItem" Width="280px" ID="radSubMenu" runat="server" DataValueField="Id" DataTextField="Name"
        EnableEmbeddedSkins="false" Skin="SubSkin" DataNavigateUrlField="NavigationURL"
        PersistStateInCookie="true">
        <ExpandAnimation Type="OutSine" Duration="300" />
        <CollapseAnimation Type="InSine" Duration="300" />
        <DataBindings>
            <telerik:RadPanelItemBinding Depth="0" Expanded="false" />
        </DataBindings>
    </telerik:RadPanelBar>
</div>


