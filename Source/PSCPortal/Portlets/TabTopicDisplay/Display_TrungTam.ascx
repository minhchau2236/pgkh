<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Display_TrungTam.ascx.cs" Inherits="PSCPortal.Portlets.TabTopicDisplay.Display_TrungTam" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<link href="/Portlets/TabTopicDisplay/Skins/Img.Skins_TrungTam.css" rel="stylesheet" type="text/css" />
<div class="K_tab_tb">
    <div class="K_tab">
        <telerik:RadTabStrip ID="radTabTopicDisplay" MultiPageID="radMultiPageTopic" runat="server"
            SelectedIndex="0" Skin="Skins" EnableEmbeddedSkins="false">
        </telerik:RadTabStrip>
    </div>
    <telerik:RadMultiPage ID="radMultiPageTopic" runat="server" SelectedIndex="0">
    </telerik:RadMultiPage>
</div>