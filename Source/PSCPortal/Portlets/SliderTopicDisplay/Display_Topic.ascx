<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Display_Topic.ascx.cs" Inherits="PSCPortal.Portlets.SliderTopicDisplay.Display_Topic" %>


<script src="/Scripts/knockout-2.3.0.js"></script>
<script src="/Scripts/moment.js"></script>
<script src="/Components/CarouselFredSel/jquery.carouFredSel-6.2.1.js"></script>

<script src="/Components/CarouselFredSel/helper-plugins/jquery.ba-throttle-debounce.min.js"></script>
<script src="/Components/CarouselFredSel/helper-plugins/jquery.mousewheel.min.js"></script>
<script src="/Components/CarouselFredSel/helper-plugins/jquery.touchSwipe.min.js"></script>
<script src="/Components/CarouselFredSel/helper-plugins/jquery.transit.min.js"></script>

<script src="/Portlets/SliderTopicDisplay/System.Utility.CarouselTopic_Slider.js"></script>
<script type="text/javascript">
    pscjq(function () {
        (new System.Utility.CarouselSlider1('<%#sliderTopicDispaly.ClientID%>', '<%#sliderArticle.ClientID%>', '<%#ListArticle%>')).CreateSlider();
    });
</script>
<style>
    .variable-width img {
        margin: 5px;
    }

    .col-sukien .list_carousel {
        margin: 35px 0px 30px 0px;
        background-color:#fff;
    }

        .col-sukien .list_carousel ul {
           padding:0;
             margin:0;
             list-style:none;
        }

        .col-sukien .list_carousel li {
             background-color:#fff;
           float: left;
        width: 220px!important;
        margin-right: 20px;
        border: none;
        }
          .col-sukien .list_carousel li a{
              text-decoration:none;
          }
          .col-sukien .list_carousel li a:hover{
              color: #7a1214;
              text-decoration:none;
          }
    .caroufredsel_wrapper{
        width:100%!important;
    }
</style>


<div class="sukien-thongbao" id="sliderTopicDispaly" runat="server">
    <div class="container">
        <div class="col-sukien">
            <h1><a data-bind="attr: { href: '/?TopicId=' + TopicId1 },html: TopicName1" target="_blank"></a></h1>
            <div id="sliderArticle" style="width: 100%" runat="server">
                <div class="list_carousel">
                    <div class="sk">

                        <div class="sk-mot">
                            <div class="wrap_sk">
                                <ul id="sliderImgs" data-bind="foreach: SliderArticleList">
                                    <li>
                                        <a data-bind="attr: { href: '/?ArticleId=' + Id }" target="_blank">
                                            <div class="sk-hinh">
                                                <figure><img style="width: 100%;height:160px" data-bind="attr: { src: '/Services/GetArticleImage.ashx?Id=' + Id, title: '#' + Id }" /></figure>
                                            </div>
                                            <div class="sk-chu"><span data-bind="html: Title">Giới thiệu Trường đại học quốc tế hồng bàng</span></div>
                                             <div class="sk-date"><span data-bind="html: $root.convertDateTime($data.CreatedDate)">10/10/2015</span></div>
                                        </a>
                                    </li>

                                </ul>

                            </div>

                        </div>
                        <div class="next-sk">
                            <a id="next234" class="next" href="#">
                                <img src="/Resources/ImagesPortal/HomePage/imgs/nextKhamPha.png" /></a>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <div class="col-thongbao">
            <h1><a data-bind="attr: { href: '/?moduleid=00000000-0000-0000-0000-000000000001&topicid2=' + TopicId2 },html: TopicName2" target="_blank"></a></h1>
            <div data-bind="foreach: SecondArticleList" style="width: 100%; float: left;">
                <div class="tb-item">
                   
                        <a data-bind="attr: { href: '/?ArticleId=' + Id }" class="tb-date" target="_blank">
                            <span data-bind="html: $root.convertDate($data.CreatedDate)">05</span>
                            <span data-bind="html: $root.convertMonth($data.CreatedDate)">tháng 11</span>
                        </a>
                   
                    <!-- / tb-date -->
                    <div class="tb-content">
                        <a data-bind="attr: { href: '/?ArticleId=' + Id }, html: Title" target="_blank"></a>
                    </div>
                    <!-- / tb-content -->
                </div>
            </div>

        </div>
    </div>
</div>
