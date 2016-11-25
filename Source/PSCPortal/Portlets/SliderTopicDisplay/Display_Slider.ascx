<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Display_Slider.ascx.cs"
    Inherits="PSCPortal.Portlets.SliderTopicDisplay.Display_Slider" %>

<link href="/Components/PgwSlider/Css/pgwslider.css" rel="stylesheet" />

<script src="/Scripts/knockout-2.3.0.js"></script>
<script src="/Scripts/moment.js"></script>
<link href="/Components/PgwSlider/Css/pgwslider.css" rel="stylesheet" />
<script src="/Portlets/SliderTopicDisplay/System.Utility.SliderPgw.js"></script>

<script type="text/javascript">
    $(function () {
        (new System.Utility.SliderTopicDisplay('<%#SliderPGW.ClientID%>', '<%#ListArticle%>')).CreateSlider();

    });
</script>





<%--<div id="example5" class="slider-pro" runat="server">
		<div class="sp-slides">
             <!-- ko foreach: $data -->
			<div class="sp-slide">
				<img class="sp-image" src="/Components/Slider_Pro/Css/images/blank.gif"
					data-bind="attr: { src: '/Services/GetArticleImage.ashx?Id=' + Id, retina: '/Services/GetArticleImage.ashx?Id=' + Id }"/>

				<div class="sp-caption" data-bind="html: Title">Lorem ipsum dolor sit amet, consectetur adipisicing elit.</div>
			</div>
            <!-- /ko -->
		
		</div>

		<div class="sp-thumbnails">
             <!-- ko foreach: $data -->
			<div class="sp-thumbnail">
				<div class="sp-thumbnail-image-container">
					<img class="sp-thumbnail-image" data-bind="attr: { src: '/Services/GetArticleImage.ashx?Id=' + Id }"/>
				</div>
				<div class="sp-thumbnail-text">
					<div class="sp-thumbnail-title" data-bind="html: Title">Lorem ipsum</div>
					<div class="sp-thumbnail-description">Dolor sit amet, consectetur adipiscing</div>
				</div>
			</div>	
		<!-- /ko -->

			
		</div>
    </div>--%>
<ul class="pgwSlider" id="SliderPGW" runat="server">
      <!-- ko foreach: $data -->
<li><img data-bind="attr: { src: '/Services/GetArticleImage.ashx?Id=' + Id}" alt="Alt 1" data-large-src="http://bqworks.com/slider-pro/images/image1_medium.jpg" data-description="More Description 1"></li>

     <!-- /ko -->
</ul>