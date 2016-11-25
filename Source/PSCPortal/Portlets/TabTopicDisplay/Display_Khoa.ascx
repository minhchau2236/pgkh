<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Display_Khoa.ascx.cs" Inherits="PSCPortal.Portlets.TabTopicDisplay.Display_Khoa" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<link href="/Portlets/TabTopicDisplay/Skins/Img.Skins_Khoa.css" rel="stylesheet" type="text/css" />
<div class="block_tab_Khoa">
    <div class="tab_Khoa">
        <telerik:RadTabStrip ID="radTabTopicDisplay" MultiPageID="radMultiPageTopic" runat="server"
            SelectedIndex="0" Skin="Skins" EnableEmbeddedSkins="false">
        </telerik:RadTabStrip>
    </div>
    <telerik:RadMultiPage ID="radMultiPageTopic" runat="server" SelectedIndex="0">
    </telerik:RadMultiPage>
</div>