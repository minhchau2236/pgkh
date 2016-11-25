<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Display_TuyenSinh.ascx.cs" Inherits="PSCPortal.Portlets.TabTopicDisplay.Display_TuyenSinh" %>


<script src="/Scripts/knockout-2.3.0.js"></script>
<script src="/Scripts/moment.js"></script>


<script src="/Portlets/TabTopicDisplay/Skins/System.Utility.TabTopicDisplay.js"></script>
<script type="text/javascript">
    pscjq(function () {
        (new System.Utility.CarouselSlider1('<%#sliderTopicDispaly.ClientID%>', '<%#sliderArticle.ClientID%>', '<%#ListArticle%>'));
    });
</script>
<div class="thongbaohoatdong" id="sliderTopicDispaly" runat="server">
<div class="container">
             	<div class="row thongbao-hoatdong" id="TabTopicDispaly" runat="server">
                	               	
                        <div class="col-xs-12 col-sm-6 col-md-6" style="padding: 0px; perspective: 1000px;">
                        <div class="block" data-move-y="200px" data-move-x="-200px" style="opacity: 1; transition: all 1s ease, opacity 1.5s ease;">
                        <p class="title"><a data-bind="attr: { href: '/?TopicId=' + TopicId1 }" target="_blank">Thông báo</a></p> 
                            <div  id="sliderArticle" runat="server" >
                                <div data-bind="foreach: SliderArticleList">
                        	<div class="tb-1">
                            	<div class="ngth">
                                     <span class="date" data-bind="html: $root.convertDate($data.CreatedDate)">05</span>
                                      <span class="month" data-bind="html: $root.convertMonth($data.CreatedDate)">tháng 11</span>
                            	</div>
                            	<div class="tb-text">
                                	<div class="tb-chude">
                                        <a href="#" style="color:#454545!important"  data-bind="attr: { href: '/?ArticleId=' + Id},html: Title" target="_blank"></a></div><!--end tb-title-->
                                    <div class="tb-noidung" data-bind="html: Name"></div><!--end tb-title-->
                                </div><!--tb-text-->
                            </div><!--en thông bao 1-->
                               
                                </div>
                            </div><!--en block-->
                        </div>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6" style="padding: 0px; overflow: hidden; perspective: 1000px;">
                        <div  class="block" data-move-y="200px" data-move-x="200px" style="opacity: 1; transition: all 1s ease, opacity 1.5s ease;">
                        <p class="title"><a data-bind="attr: { href: '/?TopicId=' + TopicId2 }" target="_blank">hoạt động sinh viên</a></p> 
                            <div data-bind="foreach: SecondArticleList">
                        	<div class="tb-2">
                            	<div class="hinhanh"><a data-bind="attr: { href: '/?ArticleId=' + Id }" target="_blank"><img data-bind="    attr: { src: '/Services/GetArticleImage.ashx?Id=' + Id }" class="img-responsive"></a></div>
                            	<div class="tb-text">
                                	<div class="tb-chude">
                                        <a  style="color:#454545!important" data-bind="attr: { href: '/?ArticleId=' + Id }, html: Title" target="_blank"> </a></div><!--end tb-title-->
                                    <div class="tb-noidung-2" data-bind="html: Name"></div><!--end tb-title-->
                                </div><!--tb-text-->
                            </div><!--en thông bao 2-->
                       </div>
                          </div><!--end block-->  
                        </div>
                </div>
             
             </div>
    </div>