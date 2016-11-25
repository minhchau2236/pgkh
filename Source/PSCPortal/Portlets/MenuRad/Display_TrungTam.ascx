<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Display_TrungTam.ascx.cs" Inherits="PSCPortal.Portlets.MenuRad.Display_TrungTam" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<link href="/Portlets/MenuRad/Menu.Green_TrungTam.css" rel="stylesheet" type="text/css" />

<%--    <div class="left">
        <img alt="" src="/Resources/ImagesPortal/TrungTam/L_mnCFIS.png">
    </div>--%>
    <telerik:RadMenu Style="z-index: 1; width: 100%;" ID="RadMenu" runat="server" DataFieldID="Id" DataTextField="Name"
        DataNavigateUrlField="NavigationURL" Height="40px" EnableEmbeddedSkins="false"
        Skin="White">
    </telerik:RadMenu>
<%--    <div class="right">
        <img alt="" src="/Resources/ImagesPortal/TrungTam/R_mnCFIS.png">
    </div>--%>

