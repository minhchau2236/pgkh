<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Display.ascx.cs" Inherits="PSCPortal.Portlets.SliderTopicDisplay.Display" %>
<link rel="stylesheet" type="text/css" href="/Components/Slider_Pro/Css/slider-pro.min.css" media="screen" />
<link rel="stylesheet" type="text/css" href="/Components/Slider_Pro/Css/SliderShow.css" media="screen" />
<%--<script src="/Scripts/jquery-2.1.4.min.js"></script>--%>

<script src="/Scripts/knockout-2.3.0.js"></script>
<script src="/Scripts/moment.js"></script>
<script type="text/javascript" src="/Components/Slider_Pro/Js/jquery.sliderPro.min.js"></script>
<%--<script src="/Components/Slider_Pro/Js/jquery.sliderPro.min.js"></script>--%>
<%--<script type="text/javascript" src="/Portlets/SliderTopicDisplay/System.Utility.SliderTopicDisplay.js"></script>--%>

<script type="text/javascript" src="/Portlets/SliderTopicDisplay/System.Utility.TopicSliderPro.js"></script>

<style type="text/css">
    .tm-sliderpro {
        width: 100%;       
        position: relative;
    }

    .slider-pro {
        position: relative;
    }

        .slider-pro h1 {
            padding: 5px 0 20px 0 !important;
        }

            .slider-pro h1 a {
                  color: #0768D5;
    text-transform: uppercase;
    text-decoration: none;
    font-size: 17px;
    font-weight: bold;
            }

                .slider-pro h1 a:hover {
                    color: #BE1E2D;
                }

        .slider-pro .sp-thumbnail-image-container {
            width: 167px !important;
            height: 92px !important;
        }


    .sp-right-thumbnails.sp-has-pointer .sp-thumbnail {
        width:167px!important;
    }
    .sp-thumbnails-container{
     
    }
    

    /*chinh font chu title*/
    .sp-thumbnail-text {
      float: left;
    width: 165px!important;
    margin-top:5px!important;
    margin-left: 0px!important;
     padding: 0px!important;
     background:none!important;
    }
    /*text ngày tháng năm*/
    .sp-thumbnail-description {
        color: #777;
        font-family: "Open Sans", Arial, Helvetica, sans-serif;
        font-size: 16px;
        font-style: italic;
        line-height: 22px;
    }

    .sp-thumbnail-title  {
        font-family: "Open Sans", Arial, Helvetica, sans-serif;
       
        font-size: 12px;
        font-weight: bold;
        padding-bottom: 10px;
        line-height: 17px;
    }
   
    /*hover list link */
    .sp-thumbnail a {
        color: #000;
        text-decoration: none;
    }

        .sp-thumbnail a:hover .sp-thumbnail-title {
            color: #0768D5;
        }
    /*-----------------------selected - right----------------------*/
    /*tam giac*/
    .sp-right-thumbnails.sp-has-pointer .sp-selected-thumbnail:after {
        content: '';
        position: absolute;
        width: 0;
        height: 0;
        left: 5px;
        top: 50%;
        margin-top: -8px;
        border-right: 8px solid #BE1E2D;
        border-top: 8px solid transparent;
        border-bottom: 8px solid transparent;
    }
    /*border - phải*/
    .sp-right-thumbnails.sp-has-pointer .sp-selected-thumbnail:before {
        content: '';
        position: absolute;
        height: 100%;
        border-left: 5px solid #BE1E2D !important;
        left: 0;
        top: 0;
        margin-left: 13px;
    }
    /*-----------------------selected - bottom----------------------*/
    /*tam giac*/
    .sp-bottom-thumbnails.sp-has-pointer .sp-selected-thumbnail:after {
        content: '';
        position: absolute;
        width: 0;
        height: 0;
        left: 50%;
        top: 5px;
        margin-left: -8px;
        border-bottom: 8px solid #0768D5;
        border-left: 8px solid transparent;
        border-right: 8px solid transparent;
    }
    /*border */
    .sp-bottom-thumbnails.sp-has-pointer .sp-selected-thumbnail:before {
        content: '';
        position: absolute;
        width: 100%;
        border-bottom: 5px solid #0768D5;
        top: 0;
        margin-top: 13px;
    }
    /*---------------------------------------- caption -------------------------------------------------------------*/
    .sp-caption-container div {      
          
    width: 368px;
    height: 45px;
    z-index: 1;
    position: absolute;
    opacity: 0.85;
    background: #2B2C2D;
  padding: 20px 0px 0px 10px;
    }
    .sp-caption-container {
    text-align: left!important;
 
}
    .sp-caption-container div a {
        text-transform: uppercase;
          font-size: 15px;
    color: #fff;
    }

        .sp-caption-container div a:hover {
            text-decoration: none;
            color: #fff;
        }

    .sp-caption-container div p {
        color: #fff;
        font-family: "Open Sans", Arial, Helvetica, sans-serif;
        font-size: 16px;
        font-weight: 400;
        line-height: 22px;
        padding: 0 30px 20px 30px;
        margin: 0;
    }

        .sp-caption-container div p:hover {
            color: #a9a9a9;
        }

        .sp-image-container {
           

            margin-top: 10px;
            margin-left: 10px;

            min-height: 1px;
            padding-right: 15px;
            padding-left: 0px
        }

        .sp-image
        {
             width: 639px !important;
             height: 261px !important;
        }
         .sp-slides-container
         {
             margin-left: -140px;
             width: 655px;
         }
        .banner
        {
            width: 970px; height: 300px;
        }
        /*caption*/
        .sp-caption-container {
                width: 290px;
   clear: both;
    margin-top: -255px;
    margin-left: 515px;
        min-height: 1px;
    padding-right: 15px;
    padding-left: 15px;
            /*position: absolute;
            margin: -70px 0 0 0;
            width: 98%;
            height: 50px;*/
        }

            .sp-caption-container div {
                width: 100%;
                height: 100%;               
            }

                .sp-caption-container div h2 {
                    margin: 0;
                    padding: 40px 30px 15px 30px;
                }
   
.sp-bottom-thumbnails .sp-thumbnail-container:first-child,
 .sp-top-thumbnails .sp-thumbnail-container:first-child{
  
}
       
    
</style>

<div class="bg_banner">
    <div class="row banner">

        <div id="sliderList" class="slider-pro" runat="server">
  	     <div class="sp-slides">
                <!-- ko foreach: SliderArticleList -->
               <div class="sp-slide">
                    <div class="col-md-8 img_banner"><img class="sp-image" data-bind="attr: { src: '/Services/GetArticleImage.ashx?Id=' + Id, retina: '/Services/GetArticleImage.ashx?Id=' + Id }"></div>
  	                <div class="sp-caption col-md-4 tin_banner ">
                        <h5><a data-bind="html: Title, attr: { href: '/?articleId=' + Id }" class="news_banner"></a></h5>
                         
                        <p data-bind="html: Name"></p>
                        <p class="chitiet" style="z-index: 9999"><a data-bind=" attr: { href: '/?articleId=' + Id }">Chi tiết >></a></p>
                  <%--  <div class="sp-arrows sp-fade-arrows">
                        <div class="sp-arrow sp-previous-arrow" style="display: none;"></div>
                        <div class="sp-arrow sp-next-arrow" style="display: block;"></div>

                    </div>--%>
                      
                      </div>
               </div>
               <!-- /ko -->
        </div>
        </div>
    </div>
</div>

<script type="text/javascript">
    pscjq(function () {

        (new System.Utility.SliderTopicDisplay('<%#sliderList.ClientID%>', '<%#ListArticle%>')).CreateSlider();

    });
</script>


