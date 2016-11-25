<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Display.ascx.cs" Inherits="PSCPortal.Portlets.TabMenu.Display" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<link href="/Portlets/TabMenu/TabSkin.css" rel="stylesheet" />
<div class="donvi">
    <div class="top_dv">
        <img src="/Resources/ImagesPortal/HomePage/top.png">
    </div>
    <div class="bg_donvi">
        <h2>Đơn vị trực thuộc</h2>
        <div class="line1_dv">
            <img src="/Resources/ImagesPortal/HomePage/line_blue.jpg">
        </div>
        <div class="tab" style="margin-left: 20px;">
            <telerik:RadTabStrip ID="radTabMenu" MultiPageID="radMultiPageMenu" runat="server"
                SelectedIndex="0" Skin="TabSkin" EnableEmbeddedSkins="false">
            </telerik:RadTabStrip>
        </div>
        <div class="line1_dv">
            <img src="/Resources/ImagesPortal/HomePage/line_blue.jpg">
        </div>

        <div>
            <telerik:RadMultiPage ID="radMultiPageMenu" runat="server" SelectedIndex="0">
            </telerik:RadMultiPage>
        </div>
        <!--End list_tab-->
    </div>
    <!--End bg_donvi-->
    <div class="bt_dv">
        <img src="/Resources/ImagesPortal/HomePage/bottom.png">
    </div>
</div>
