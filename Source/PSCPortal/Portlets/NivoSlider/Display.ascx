<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Display.ascx.cs" Inherits="PSCPortal.Portlets.NivoSlider.Display" %>
<%--<script src="/Portlets/NivoSlider/Scripts/jquery-1.7.2.min.js"></script>--%>
<link href="/Portlets/NivoSlider/CSS/nivo-slider.css" rel="stylesheet" />
<link href="/Portlets/NivoSlider/CSS/themes/default/default.css" rel="stylesheet" />
<script src="/Scripts/knockout-2.3.0.js"></script>
<script src="/Portlets/NivoSlider/Scripts/jquery.nivo.slider.js"></script>
<script src="/Portlets/NivoSlider/Scripts/System.Utility.NivoSlider.js"></script>
<script type="text/javascript">
    pscjq(function() {
        (new System.Utility.NivoSlider('<%#slider.ClientID%>', '<%#ListImage%>')).CreateSlider();
        var total = pscjq('.nivo-control').length;
      
        pscjq('#nivo-slider-status').show();
        pscjq('#nivo-slider-status > .total-slides').html(total);
        current_slide_no = jQuery('.nivoSlider').data('nivo:vars').currentSlide;
       
        pscjq('#nivo-slider-status > .current-slide').html((current_slide_no + 1)+" / ");
    });
</script>
<div class="slider_outter">
    <div class="slider">
    <div class="slider-wrapper theme-default">
        <div id="slider" class="nivoSlider" runat="server">
            <!-- ko foreach: $data -->
            <a data-bind="attr: {href: Link}" target="_blank">
                <img data-bind="attr: {src: '/'+Url, title: Title }" /></a>
            <!-- /ko -->
        </div>
    </div>
</div>
   <%--  <div id="sliderCount" style="margin:-64px 0px 0px 96%"></div>--%>
    <div id="nivo-slider-status" class="alignright">
    <span class="current-slide"></span>  <span class="total-slides"></span>
</div>
</div>

