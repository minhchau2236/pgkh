<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Display_SliderShow.ascx.cs"
    Inherits="PSCPortal.Portlets.SliderTopicDisplay.Display_SliderShow" %>

<link href="/Portlets/SliderTopicDisplay/style.css" rel="stylesheet" />
<link href="/Portlets/SliderTopicDisplay/an_style.css" rel="stylesheet" />
<%--<script src="/Scripts/knockout-2.3.0.js"></script>--%>
<script src="/Scripts/moment.js"></script>

<%--<script src="/Portlets/SliderTopicDisplay/jquery-ui-1.9.0.custom.min.js"></script>--%>



<script src="/Portlets/SliderTopicDisplay/jquery-ui-tabs-rotate.js"></script>
<script type="text/javascript" src="/Portlets/SliderTopicDisplay/System.Utility.SliderTopicDisplay.js"></script>

<script type="text/javascript">
    pscjq(function () {
        (new System.Utility.SliderTopicDisplay12('<%#Sliderfea.ClientID%>', '<%#ListArticle%>')).CreateSlider();
       

            pscjq("#featured").tabs({

                fx: {
                    opacity: "toggle"
                }

            }).tabs("rotate", 5000, true);
       });
  
</script>



<%--<div id="example5" class="slider-pro" runat="server">

       
        <div class="sp-slides">
             <!-- ko foreach: $data -->
            <div class="sp-slide">
                <a data-bind="attr: { href: '/?ArticleId=' + Id }" target="_blank">
                    <img class="sp-image" src="/Components/Slider_Pro/Css/images/blank.gif" data-bind="attr: { src: '/Services/GetArticleImage.ashx?Id=' + Id, retina: '/Services/GetArticleImage.ashx?Id=' + Id }" />
                    <div class="sp-caption" data-bind="html: Title">Lorem ipsum dolor sit amet, consectetur adipisicing elit.</div>
                </a>
            </div>
               <!-- /ko -->
        </div>
     

      
        <div class="sp-thumbnails">
              <!-- ko foreach: $data -->
            <div class="sp-thumbnail">
                <div class="sp-thumbnail-image-container">
                    <img class="sp-thumbnail-image" data-bind="attr: { src: '/Services/GetArticleImage.ashx?Id=' + Id }" />
                </div>
                <div class="sp-thumbnail-text">
                    <div class="sp-thumbnail-title" data-bind="html: Title">Laudantium</div>
                    <div class="sp-thumbnail-description">20/11/2015</div>
                </div>
            </div>
             <!-- /ko -->
        </div>
       
     
</div>--%>



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
<div class="tinmoi-ui" id="Sliderfea" runat="server">
    <div class="container">
         <h1 ><a data-bind="attr: {href: '/?topicId='+TopicId}">tin mới</a></h1>
    </div>   
    <div class="container">
        <div id="featured">
            <!-- ko foreach: ResultList -->
            <div data-bind="attr: { id: 'fragment-' + parseInt($index()) }" class="ui-tabs-panel" style="">
                <img alt="" data-bind="attr: { src: '/Services/GetArticleImage.ashx?Id=' + Id }" />
                <div class="info">
                    <h2><a  data-bind="html: Title, attr: {href: '/?articleId='+Id}"></a></h2>
                    <p data-bind="html: Name">
                     
                    </p>
                </div>
            </div>
            <!-- /ko -->



            <ul class="ui-tabs-nav">
                <!-- ko foreach: ResultList -->
                <li class="ui-tabs-nav-item" data-bind="attr: { id: 'nav-fragment-' + Id }">
                    <a data-bind="attr: { href: '#fragment-' + parseInt($index()) }">
                        <img data-bind="    attr: { src: '/Services/GetArticleImage.ashx?Id=' + Id, href: '#fragment-' + parseInt($index()) }" />
                        <span class="tm-title" data-bind="    html: Title"></span>
                        <span class="tm-date" data-bind="html: $root.convertDateTime($data.CreatedDate)">01/01/2015</span>
                    </a>
                </li>
                <!-- /ko -->


            </ul>

        </div>
    </div>
</div>
