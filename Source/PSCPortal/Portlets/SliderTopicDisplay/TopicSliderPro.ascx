<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopicSliderPro.ascx.cs" Inherits="PSCPortal.Portlets.SliderTopicDisplay.TopicSliderPro" %>
<link rel="stylesheet" type="text/css" href="/Components/Slider_Pro/Css/slider-pro.min.css" media="screen" />
<link rel="stylesheet" type="text/css" href="/Components/Slider_Pro/Css/SliderShow.css" media="screen" />

<script src="/Scripts/moment.js"></script>
<script type="text/javascript" src="/Components/Slider_Pro/Js/jquery.sliderPro.min.js"></script>
<%--<script src="/Components/Slider_Pro/Js/jquery.sliderPro.min.js"></script>--%>
<%--<script type="text/javascript" src="/Portlets/SliderTopicDisplay/System.Utility.SliderTopicDisplay.js"></script>--%>
<script type="text/javascript" src="/Portlets/SliderTopicDisplay/System.Utility.TopicSliderPro.js"></script>
<script type="text/javascript">
    pscjq(function () {
        (new System.Utility.SliderTopicDisplay('<%#example5.ClientID%>', '<%#ListArticle%>')).CreateSlider();

    });
</script>
<style type="text/css">
    .tm-sliderpro {
        width: 100%;
        background-color: #f1f1f1;
        padding-top: 30px;
        position: relative;
    }

    .slider-pro {
        position: relative;
    }

        .slider-pro h1 {
            padding: 5px 0 20px 0 !important;
        }

            .slider-pro h1 a {
                color: #454545;
                font-family: 'Roboto Condensed Bold';
                font-size: 35px;
                text-transform: uppercase;
                text-decoration: none;
            }

                .slider-pro h1 a:hover {
                    color: #BE1E2D;
                }

        /*list img bên phải*/
        /*fix 140x117*/
        .slider-pro .sp-thumbnail-image {                   
            -webkit-transform: translate(-29%,0%);
            -ms-transform: translate(-29%,0%);
            transform: translate(-29%,0%);
        }

        .slider-pro .sp-thumbnail-image-container {
            width: 140px !important;
            height: 117px !important;
        }

    .sp-right-thumbnails .sp-thumbnail-container {
        height: 117px !important;
        margin-bottom: 21px;
    }

    .sp-right-thumbnails.sp-has-pointer .sp-thumbnail {
        margin-left: 18px !important;
        position: static !important;
    }

    .sp-right-thumbnails.sp-has-pointer {
        margin-top: 63px !important;
    }
    /*chinh font chu title*/
    .sp-thumbnail-text {
        width: 327px !important;
        padding-top: 0px !important;
        margin-left: 150px !important;
        display: block;
    }
    /*text ngày tháng năm*/
    .sp-thumbnail-description {
        color: #777;
        font-family: "Open Sans", Arial, Helvetica, sans-serif;
        font-size: 16px;
        font-style: italic;
        line-height: 22px;
    }

    .sp-thumbnail-title {
        font-family: "Open Sans", Arial, Helvetica, sans-serif;
        color: #454545;
        font-size: 16px;
        font-weight: 700;
        padding-bottom: 10px;
        line-height: 22px;
    }
    /*hover list link */
    .sp-thumbnail a {
        color: #454545;
        text-decoration: none;
    }

        .sp-thumbnail a:hover .sp-thumbnail-title {
            color: #BE1E2D;
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
        border-bottom: 8px solid #BE1E2D;
        border-left: 8px solid transparent;
        border-right: 8px solid transparent;
    }
    /*border */
    .sp-bottom-thumbnails.sp-has-pointer .sp-selected-thumbnail:before {
        content: '';
        position: absolute;
        width: 100%;
        border-bottom: 5px solid #BE1E2D;
        top: 0;
        margin-top: 13px;
    }
    /*---------------------------------------- caption -------------------------------------------------------------*/
    .sp-caption-container div {      
        background: #BE1E2D;
        opacity: 0.9 !important;
        color: #fff !important;
        text-align: left;
        margin: 0;
        padding: 0;
    }
    .sp-caption-container div a {
        color: #fff;
        font-family: 'Roboto Condensed Bold';
        font-size: 18px;
        font-weight: 500;
        line-height: 28px;
        text-transform: uppercase;
    }

        .sp-caption-container div a:hover {
            text-decoration: none;
            color: #a9a9a9;
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
    /*-----------------------------------------desktop >=1200px ----------------------------------------------------*/
    @media (min-width:1200px) {
        .tm-sliderpro {
            min-height: 580px;
        }

        .slider-pro {
            padding-right: 38% !important;
        }

        .sp-slides-container {
            position: absolute;
        }
        /*khung ảnh lớn bên trái*/
        .sp-mask, .sp-slide, .sp-image-container {
            height: 400px !important;
        }

        /*caption*/
        .sp-caption-container {
            position: absolute;
            margin: 0px;
            width: 25%;
            height: 400px;
        }

            .sp-caption-container div {
                width: 100%;
                height: 100%;                
            }

                .sp-caption-container div h2 {
                    margin: 0;
                    padding: 40px 30px 15px 30px;
                }
    }
    /*-----------------------------------------851px <= ... <=1199px ----------------------------------------------------*/
    @media (min-width:851px) and (max-width:1199px) {
        /*khung bao*/
        .tm-sliderpro {
            padding: 20px 15px 10px 15px;
            min-height: 550px;
        }

        .slider-pro {
            margin: 0 !important;
        }

        /*caption*/
        .sp-caption-container {
            position: absolute;
            margin: -400px 0 0 0;
            width: 240px;
            height: 400px;
        }

            .sp-caption-container div {
                width: 100%;
                height: 100%;               
            }

                .sp-caption-container div h2 {
                    margin: 0;
                    padding: 40px 30px 15px 30px;
                }
    }

    /*------------------------------------------ tablet 768px<= ... <= 800px ---------------------------------------*/
    @media (min-width:768px) and (max-width:850px) {
        /*khung bao*/
        .tm-sliderpro {
            padding: 20px 15px 10px 15px;
            min-height: 690px;
        }

        .slider-pro {
            width: 100%;
            margin: 0 !important;
            max-width: 100% !important;
        }
        /*img lớn*/
        .sp-mask, .sp-slide, .sp-image-container, .sp-image {
            height: 400px !important;
        }

        .sp-thumbnails-container {
            width: 100% !important;
            height: auto !important;
            margin-top: 400px !important;
            position: absolute;
        }

        .sp-thumbnails > .sp-thumbnail-container {
            min-height: 160px;
            margin: 0 5px 0 0px !important;
            width: 360px !important;
            height: auto !important;
        }
        /*chỉnh text cho thumbnail*/
        .sp-bottom-thumbnails.sp-has-pointer .sp-thumbnail {
            width: 100% !important;
            height: auto;
        }

        .slider-pro .sp-thumbnail-text {
            width: 60% !important;
        }
        /*caption*/
        .sp-slides-container {
            position: absolute;
        }

        .sp-caption-container {
            position: absolute;
        }

        .sp-caption-container {
            margin-top: 0 !important;
            width: 43%;
            height: 400px;
        }

            .sp-caption-container div {
                width: 100%;
                height: 100%;               
            }

                .sp-caption-container div h2 {
                    margin: 0 !important;
                    padding: 40px 30px 15px 30px;
                }
    }
    /*------------------------------------------ mobile <=767px  ---------------------------------------------------*/
    @media (max-width: 767px) {
        .tm-sliderpro {
            padding: 20px 15px;
            min-height:820px;
        }

        .slider-pro {
            width: 100% !important;
            margin: 0 !important;
        }

        .sp-thumbnails > .sp-thumbnail-container {
            min-height: 160px;
            margin-top: 155px;
        }

        .sp-thumbnails-container {
            width: 100% !important;
            position: absolute;
        }

        .sp-mask {
            width: 100% !important;
        }

        .sp-slide {
            width: 100% !important;
        }

        .sp-image-container {
            width: 100% !important;
        }

        .sp-caption-container {
            margin-top: 0px !important;
            width: 100%;
            min-height: 160px;
            position: absolute;
        }

            .sp-caption-container div {
                width: 100%;
                min-height: 140px;               
            }

                .sp-caption-container div h2 {
                    margin: 0 !important;
                    padding: 15px;
                }

                .sp-caption-container div p {
                    display: none;
                }
    }
     /*------------------------------------------ mobile <=700px  ---------------------------------------------------*/
    @media (max-width: 700px) {
        .tm-sliderpro {
            padding: 20px 15px;
            min-height:780px !important;
        }
    }
    /*------------------------------------------ mobile <=600px  ---------------------------------------------------*/
    @media (max-width: 600px) {
        .tm-sliderpro {
            padding: 20px 15px;
            min-height:755px !important;
        }
    }
    /*------------------------------------------ mobile <=500px  ---------------------------------------------------*/
    @media (max-width: 500px) {
        .tm-sliderpro {
            padding: 20px 15px;
            min-height: 665px !important;
        }
    }
    /*------------------------------------------ mobile <=400px  ---------------------------------------------------*/
    @media (max-width: 400px) {
        .tm-sliderpro {
            padding: 20px 15px;
            min-height: 625px !important;
        }
    }
    /*------------------------------------------ mobile <=320px  ---------------------------------------------------*/
    @media (max-width: 319px) {
        .tm-sliderpro {
            padding: 20px 15px;
            min-height: 585px !important;
        }
    }
</style>
<div class="tm-sliderpro">
    <div id="example5" class="slider-pro" runat="server">
        <h1><a data-bind="attr: { href: '/?topicId=' + TopicId },html: TopicName" target="_blank"></a></h1>
        <div class="sp-slides">
            <!-- ko foreach: ResultList -->
            <div class="sp-slide">
                <img class="sp-image" data-bind="attr: { src: '/Services/GetArticleImage.ashx?Id=' + Id, retina: '/Services/GetArticleImage.ashx?Id=' + Id }" />
                <div class="sp-caption">
                    <div>
                        <h2><a data-bind="html: Title, attr: {href: '/?articleId='+Id}" target="_blank"></a></h2>
                        <p data-bind="html: Name"></p>
                    </div>
                </div>
            </div>
            <!-- /ko -->
        </div>

        <div class="sp-thumbnails">
            <!-- ko foreach: ResultList -->
            <div class="sp-thumbnail">
                <div class="sp-thumbnail-image-container">
                    <img class="sp-thumbnail-image" data-bind="attr: { src: '/Services/GetArticleImage.ashx?Id=' + Id}" />
                </div>
                <div class="sp-thumbnail-text">
                    <a data-bind="attr: { href: '/?articleId=' + Id }" target="_blank">
                        <div class="sp-thumbnail-title" data-bind="html: Title"></div>
                    </a>
                    <div class="sp-thumbnail-description" data-bind="html: $root.convertDateTime($data.CreatedDate)"></div>
                </div>
            </div>
            <!-- /ko -->
        </div>
    </div>
</div>
