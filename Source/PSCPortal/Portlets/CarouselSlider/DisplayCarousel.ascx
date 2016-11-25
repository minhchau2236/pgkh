<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Display.ascx.cs" Inherits="PSCPortal.Portlets.CarouselSlider.Display" %>
<link href="/Components/CarouselSlider/CSS/slick.css" rel="stylesheet" />
<link href="/Components/CarouselSlider/CSS/slick-theme.css" rel="stylesheet" />
<%--<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>--%>
<script src="/Components/CarouselSlider/Scripts/slick.js"></script>
<script src="/Scripts/knockout-2.3.0.js"></script>
<script src="/Components/CarouselFredSel/jquery.carouFredSel-6.2.1.js"></script>
<script src="/Components/CarouselSlider/Scripts/System.Utility.CarouselSlider.js"></script>


<script src="/Components/CarouselFredSel/helper-plugins/jquery.ba-throttle-debounce.min.js"></script>
<script src="/Components/CarouselFredSel/helper-plugins/jquery.mousewheel.min.js"></script>
<script src="/Components/CarouselFredSel/helper-plugins/jquery.touchSwipe.min.js"></script>
<script src="/Components/CarouselFredSel/helper-plugins/jquery.transit.min.js"></script>
<script type="text/javascript">
    $(function () {
        (new System.Utility.CarouselSlider('<%#slider.ClientID%>', '<%#ListArticle%>')).CreateSlider();
    });
</script>
<style>
    .variable-width img {
        margin: 5px;
    }

    .list_carousel {
        background-color: #f1f1f1;;
        margin: 0 0 30px 0;
    }

        .list_carousel ul {
            margin: 0;
            padding: 0;
            list-style: none;
            display: block;
        }

        .list_carousel li {
            font-size: 40px;
            color: #999;
            text-align: center;
            background-color: #eee;
            border: 5px solid #999;
            padding: 0;
            margin: 6px;
            display: block;
            float: left;
        }
</style>
<div id="slider" style="width: 100%" runat="server">
    <div class="list_carousel">
        <div class="kham-pha-v3">
            <h2>khám phá</h2>
            <div class="kp-mot">
                <div class="wrap">
                    <ul id="sliderImgs" data-bind="foreach: $data">
                        <li>
                            <a href="javascript:void(0)" data-bind="click: function () { showDialogInfomation(Article.Id) } ">
                                <div class="kp-hinh">
                                    <img style="height: 150px;" data-bind="attr: { src: '/Services/GetArticleImage.ashx?Id=' + Article.Id }" />
                                </div>
                                <div class="kp-chu" data-bind="html: Article.Title">Giới thiệu Trường đại học quốc tế hồng bàng</div>
                            </a>

                            <%--<a href="#"><img style="width: 200px; height: 200px;" data-bind="attr: { src: '/Services/GetArticleImage.ashx?Id=' + Article.Id }, click: function () { showDialogInfomation(Article.Id) }" /></a>--%>
                        </li>
                    </ul>
                  
                <%--    <a id="prev2" class="prev" href="#">&lt;</a>--%>
                    
                  
                </div>
               
            </div>
            <div class="next-KhamPha"><a id="next2" class="next" href="#"><img src="/Resources/ImagesPortal/HomePage/imgs/nextKhamPha.png" /></a></div>
        </div>
    </div>
</div>
<input type="button" onclick="showDialogInfomation('5801047b-9128-47ad-b52d-c40094b3b733')" value="showDialog" />
<button type="button" class="btn btn-info btn-lg" data-toggle="modal" data-target="#myModal">Open Modal</button>
<div id="myModal" class="modal fade" role="dialog">
    <div class="modal-dialog">

        <!-- Modal content-->
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal">&times;</button>
                <h4 class="modal-title">Modal Header</h4>
            </div>
            <div class="modal-body">
                <p>Some text in the modal.</p>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
            </div>
        </div>

    </div>
</div>
<%--
<div id="slider" runat="server">
    <div class="variable-width">
        <!-- ko foreach: $data -->
        <div>
            <a data-bind="attr: { href: Link }" target="_blank">
                <img data-bind="attr: { src: '/' + Url, title: Title }" />
            </a>
        </div>
        <!-- /ko -->
    </div>
</div>--%>


