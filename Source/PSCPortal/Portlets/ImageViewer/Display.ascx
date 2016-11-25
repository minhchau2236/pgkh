<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Display.ascx.cs" Inherits="PSCPortal.Portlets.ImageViewer.Display" %>
<script type="text/javascript" language="javascript">
    //jQuery.noConflict();
    pscjq(function () {
        (new System.Utility.ImageViewer('<%#slider.ClientID%>', '<%#ListImage%>'));
        var total = pscjq('.nivo-control').length;
    });
</script>
<script src="/Scripts/knockout-2.3.0.js"></script>
<script src="/Portlets/ImageViewer/Scripts/jquery.timers-1.1.2.js" type="text/javascript"></script>
<script src="/Portlets/ImageViewer/Scripts/jquery.galleryview-1.1.js" type="text/javascript"></script>
<script src="/Portlets/ImageViewer/Scripts/System.Utility.GalleryView.js" type="text/javascript"></script>
<script src="/Portlets/ImageViewer/Scripts/System.Utility.ImageViewer.js"></script>


<link href="../../Components/FancyBox/jquery.fancybox.css" rel="stylesheet" />
<script type="text/javascript" src="http://code.jquery.com/jquery-1.11.0.min.js"></script>
<script type="text/javascript" src="http://code.jquery.com/jquery-migrate-1.2.1.min.js"></script>
<script src="../../Components/FancyBox/jquery.fancybox.js"></script>
<script type="text/javascript">
    $(function ($) {
        var addToAll = false;
        var gallery = true;
        var titlePosition = 'inside';
        $(addToAll ? 'img' : 'img.fancybox').each(function () {
            var $this = $(this);
            var title = $this.attr('title');
            var src = $this.attr('data-big') || $this.attr('src');
            var a = $('<a href="#" class="fancybox"></a>').attr('href', src);
            $this.wrap(a);
        });
        if (gallery)
            $('a.fancybox').attr('rel', 'fancyboxgallery');
        $('a.fancybox').fancybox({
            titlePosition: titlePosition
        });
    });
    $.noConflict();
</script>
<style type="text/css">
    a.fancybox img {
        border: none;
        /*box-shadow: 0 1px 7px rgba(0,0,0,0.6);*/
        -o-transform: scale(1,1);
        -ms-transform: scale(1,1);
        -moz-transform: scale(1,1);
        -webkit-transform: scale(1,1);
        transform: scale(1,1);
        -o-transition: all 0.2s ease-in-out;
        -ms-transition: all 0.2s ease-in-out;
        -moz-transition: all 0.2s ease-in-out;
        -webkit-transition: all 0.2s ease-in-out;
        transition: all 0.2s ease-in-out;
    }

    a.fancybox:hover img {
        position: relative;
        z-index: 999;
        -o-transform: scale(1.1,1.1);
        -ms-transform: scale(1.1,1.1);
        -moz-transform: scale(1.1,1.1);
        -webkit-transform: scale(1.1,1.1);
        transform: scale(1.1,1.1);
    }
</style>

<div id="slider" runat="server" class="phongsuanh">
    <div class="topic">
        <div class="L_topic">
            <img src="/Resources/ImagesPortal/HomePage/imgs/arrow_yellow.jpg">
            <a href="#">Phóng sự ảnh</a>
        </div>
        <!--L_topic-->
    </div>
    <!--topic-->

    <div class="ct_anh">
        <ul data-bind="foreach: $data">

            <li><a href="#">
                <img data-bind="attr: { src: '/' + Url, title: Title }" width= "155px" height="85px" class="fancybox"></a></li>
            <%--<li><a href="#">
                <img src="/Resources/ImagesPortal/HomePage/imgs/a2.jpg"></a></li>
            <li><a href="#">
                <img src="/Resources/ImagesPortal/HomePage/imgs/a3.jpg"></a></li>
            <li><a href="#">
                <img src="/Resources/ImagesPortal/HomePage/imgs/a4.jpg"></a></li>
            <li><a href="#">
                <img src="/Resources/ImagesPortal/HomePage/imgs/a5.jpg"></a></li>
            <li><a href="#">
                <img src="/Resources/ImagesPortal/HomePage/imgs/a6.jpg"></a></li>
            <li><a href="#">
                <img src="/Resources/ImagesPortal/HomePage/imgs/a7.jpg"></a></li>
            <li><a href="#">
                <img src="/Resources/ImagesPortal/HomePage/imgs/a8.jpg"></a></li>
            <li><a href="#">
                <img src="/Resources/ImagesPortal/HomePage/imgs/a9.jpg"></a></li>
            <li><a href="#">
                <img src="/Resources/ImagesPortal/HomePage/imgs/a10.jpg"></a></li>
            <li><a href="#">
                <img src="/Resources/ImagesPortal/HomePage/imgs/a11.jpg"></a></li>
            <li><a href="#">
                <img src="/Resources/ImagesPortal/HomePage/imgs/a12.jpg"></a></li>--%>

        </ul>
    </div>
    <!--End ct_anh-->
</div>
<!--End phongsuanh-->
