<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Display.ascx.cs" Inherits="PSCPortal.Portlets.CarouselSlider.Display" %>
<link href="/Components/CarouselSlider/CSS/slick.css" rel="stylesheet" />
<link href="/Components/CarouselSlider/CSS/slick-theme.css" rel="stylesheet" />
<%--<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>--%>
<script src="/Components/CarouselSlider/Scripts/slick.js"></script>
<script src="/Scripts/knockout-2.3.0.js"></script>
<script src="/Components/CarouselFredSel/jquery.carouFredSel-6.2.1.js"></script>
<script src="/Components/CarouselFredSel/System.Utility.CarouselSlider.js"></script>


<script src="/Components/CarouselFredSel/helper-plugins/jquery.ba-throttle-debounce.min.js"></script>
<script src="/Components/CarouselFredSel/helper-plugins/jquery.mousewheel.min.js"></script>
<script src="/Components/CarouselFredSel/helper-plugins/jquery.touchSwipe.min.js"></script>
<script src="/Components/CarouselFredSel/helper-plugins/jquery.transit.min.js"></script>
<script type="text/javascript">
   pscjq(function () {
        (new System.Utility.CarouselSlider('<%#slider.ClientID%>', '<%#ListArticle%>')).CreateSlider();
    });
</script>
<style>
    .variable-width img {
        margin: 5px;
    }

    .list_carousel {
        
        margin: 0 0 30px 0;
            padding-right: 8px;
    }

        .list_carousel ul {
            margin: 0;
            padding: 0;
            list-style: none;
            /*display: block;*/
        }

        .list_carousel li {
            width:220px!important;
            font-size: 40px;
            color: #999;
            text-align: center;
        
            border: 5px solid #999;
            padding: 0;
            /*margin: 6px;*/
            display: block;
            float: left;
        }
</style>
<div id="slider" style="width: 100%" runat="server">
    <div class="list_carousel">
        <div class="kham-pha-v3 container">
            <h1> <a data-bind="attr: { href: '/?TopicId=' + TopicId },html: TopicName" target="_blank">khám phá</a></h1>
            <div class="kp-mot">
                <div class="wrap">
                    <ul id="sliderImgs" data-bind="foreach: resultList">
                        <li>
                            <a href="javascript:void(0)" data-bind="click: function () { showDialogInfomation(Article.Id) } ">
                                <div class="kp-hinh">                                  
                                    <figure><img style="width: 100%;height:160px" data-bind="attr: { src: '/Services/GetArticleImage.ashx?Id=' + Article.Id }" /></figure>
                                </div>
                                <div class="kp-chu" data-bind="html: Article.Title">Giới thiệu Trường đại học quốc tế hồng bàng</div>
                            </a>
                          
                           
                        </li>
                    </ul>                                                        
                </div>
               
            </div>
            <div class="next-KhamPha"><a id="nextCarousel1" class="next" href="#"><img src="/Resources/ImagesPortal/HomePage/imgs/nextKhamPha.png" /></a></div>
        </div>
    </div>
</div>
