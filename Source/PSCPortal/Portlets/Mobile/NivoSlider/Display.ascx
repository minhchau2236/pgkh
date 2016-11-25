<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Display.ascx.cs" Inherits="PSCPortal.Portlets.Mobile.NivoSlider.Display" %>
<link href="/Portlets/NivoSlider/CSS/nivo-slider.css" rel="stylesheet" />
<link href="/Portlets/NivoSlider/CSS/themes/default/default.css" rel="stylesheet" />
<script src="/Scripts/knockout-2.3.0.js"></script>
<script src="/Portlets/NivoSlider/Scripts/jquery.nivo.slider.js"></script>
<script src="/Portlets/NivoSlider/Scripts/System.Utility.NivoSlider.js"></script>
<script type="text/javascript">
    $(function () {
        (new System.Utility.NivoSlider('<%#slider.ClientID%>', '<%#ListImage%>')).CreateSlider();
    });
</script>
<div class="mobileDisplay" style="max-width: 96%;padding: 5px 2%;">
    <div class="container">
        <div class="slider-wrapper theme-default">
            <div id="slider" class="nivoSlider" runat="server">
                <!-- ko foreach: $data -->
                <a data-bind="attr: {href: Link}" target="_blank">
                    <img data-bind="attr: {src: '/'+Url, title: Title }" /></a>
                <!-- /ko -->
            </div>
        </div>
    </div>
</div>
