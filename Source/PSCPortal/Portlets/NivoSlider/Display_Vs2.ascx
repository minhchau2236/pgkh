<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Display.ascx.cs" Inherits="PSCPortal.Portlets.NivoSlider.Display" %>

<%--<script src="/Portlets/NivoSlider/Scripts/jquery-1.7.2.min.js"></script>--%>
<script src="/Portlets/NivoSlider/Scripts/jquery.nivo.slider.pack.3.2.js"></script>
<script src="/Portlets/NivoSlider/Scripts/nivo-timelined-captions.js"></script>
<link href="/Portlets/NivoSlider/CSS/demo33-nivo-slider.css" rel="stylesheet" />
<script src="/Scripts/knockout-2.3.0.js"></script>
<script src="/Portlets/NivoSlider/Scripts/System.Utility.NivoSlider2.js"></script>
<link href="/Portlets/NivoSlider/CSS/custom5-S.css" rel="stylesheet" />
<script type="text/javascript">
    pscjq(function () {
        (new System.Utility.NivoSlider('<%#slider.ClientID%>', '<%#ListImage%>')).CreateSlider();

    });
</script>
<style type="text/css"> 


</style>
<%--<div class="slider">
    <div class="slider-wrapper theme-default">
        <div id="slider" class="nivoSlider" runat="server">
            <!-- ko foreach: $data -->
            <a data-bind="attr: { href: Link }" target="_blank">
                <img data-bind="attr: { src: '/' + Url, title: Title }" /></a>
            <!-- /ko -->
        </div>
    </div>
</div>--%>
<div class="container">

<!-- Title -->
<!-- Nivo slider Demo 33 -->
<div id="NivosliderD33oo">
<div id="slider" class="nivoSlider" runat="server">
  <!-- ko foreach: $data -->
            <a data-bind="attr: { href: Link }" target="_blank">
                <img class="img-responsive" data-bind="attr: { src: '/' + Url, title: Title }" />
            </a>
            <!-- /ko -->
</div></div>
</div>

