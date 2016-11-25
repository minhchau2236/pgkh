<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PSCPortal.Default" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
     <title><%# PageTitle %></title>
  <%--  <link href="/CSS/search_Article.css" rel="stylesheet" />--%>
     <link href="/Components/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="/Components/AnimateCss/animate.css" rel="stylesheet" />
    <script type="text/javascript" src="/Scripts/jquery-2.1.4.min.js"></script>

    <%--<script type="text/javascript" src="/Scripts/jQuery.scrollSpeed.js"></script>--%>
    <link href="/Portlets/Media/mediaelementplayer.css" rel="stylesheet" />
    <script src="/Portlets/Media/mediaelement-and-player.min.js"></script>

    <script type="text/javascript" src="/Components/Waypoint/jquery.waypoints.min.js"></script>
  <%--  <link href="/CSS/show_img.css" rel="stylesheet" type="text/css" />--%>
    <script type="text/javascript" src="/Components/bootstrap/Js/bootstrap.min.js"></script>
    <script type="text/javascript" src="/Scripts/knockout-2.3.0.js"></script>
    <script src="/Scripts/Common.js" type="text/javascript"></script>
  
    <script src="/Scripts/PagingContent.js" type="text/javascript"></script>
    <script src="/Scripts/zebra_dialog.js" type="text/javascript"></script>
    <link href="/CSS/zebra_dialog.css" rel="stylesheet" />

 

    <script language="javascript" type="text/javascript">

        var pscjq = jQuery.noConflict();

        function SearchProcess(event, query) {
            if (event.keyCode == 13) {
                OnSearch(query);
                return false;
            }
            return true;
        }
        function OnSearch(query) {
            PSCPortal.Services.CMS.SearchAndPaging(query, currentPage, OnSearchSuccess, OnSearchFailed);
        }

        function OnSearchSuccess(results, context, methodName) {
            //var position = results.indexOf('_');
            //totalRecords = results.substring(0, position);
            //results = results.substring(position + 1);
            LoadContentAndPaging(results);
        }
        function OnSearchFailed(results, context, methodName) {
        }

        function retitleUrl(str) {
            str = str.replace(/^\s+|\s+$/g, ''); // trim
            str = str.toLowerCase();
            // remove accents, swap ñ for n, etc
            var from = "àáảãạăằắẳẵặâầấẩẫậđèéẻẽẹêềếểễệìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵ·/_,:;";
            var to = "aaaaaaaaaaaaaaaaadeeeeeeeeeeeiiiiiooooooooooooooooouuuuuuuuuuuyyyyy------";
            for (var i = 0, l = from.length ; i < l ; i++) {
                str = str.replace(new RegExp(from.charAt(i), 'g'), to.charAt(i));
            }
            str = str.replace(/[^a-z0-9 -]/g, '') // remove invalid chars
                .replace(/\s+/g, '-') // collapse whitespace and replace by -
                .replace(/-+/g, '-'); // collapse dashes
            return str;
        };

        function popunder() {
            pscjq('a').not('[href^="http"],[href^="https"],[href^="mailto:"],[href^="#"]').each(function () {
                pscjq(this).attr('href', function (index, value) {
                    if (value != undefined) {
                        var index = value.indexOf("Default.aspx");
                        if (index >= 0)
                            return value = "/" + value.substring(index, value.length);

                    }
                });
            });
            var articleId = '<%#Request.QueryString["ArticleId"]%>' != '' ? '<%#Request.QueryString["ArticleId"]%>' : '<%#RouteData.Values["Id"]%>';
            var topicId = '<%#Request.QueryString["TopicId"]%>' != '' ? '<%#Request.QueryString["TopicId"]%>' : '<%#RouteData.Values["TopicId1"]%>';
            var moduleId = '<%#Request.QueryString["ModuleId"]%>' != '' ? '<%#Request.QueryString["ModuleId"]%>' : '<%#RouteData.Values["ModuleId"]%>';
            var topicId2 = '<%#Request.QueryString["TopicId2"]%>' != '' ? '<%#Request.QueryString["TopicId2"]%>' : '<%#RouteData.Values["TopicId2"]%>';

            if (articleId != '') {
                if (window.history.state == null) {
                    if (articleId.indexOf("/") > -1)
                        articleId = articleId.substr(0, articleId.indexOf("/"));
                    window.history.replaceState({ ArticleId: articleId }, "ArticleId", "/vi/" + retitleUrl('<%#TopicTitle%>') + "/" + retitleUrl('<%#ArticleTitle%>') + "/" + articleId);
                }
            }
<%--            if (topicId != '') {
                if (window.history.state == null) {
                    if (topicId.indexOf("/") > -1)
                        topicId = topicId.substr(0, topicId.indexOf("/"));
                    window.history.replaceState({ TopicId: topicId }, "TopicId", "/vi/" + retitleUrl('<%#TopicTitle%>') + "/" + topicId);
                }
            }--%>
            if (moduleId != '' && topicId2 != '') {
                if (window.history.state == null) {
                    if (moduleId.indexOf("/") > -1)
                        moduleId = moduleId.substr(0, moduleId.indexOf("/"));
                    window.history.replaceState({ ModuleId: moduleId }, "ModuleId", "/module/" + retitleUrl('<%#ModuleTitle%>') + "/" + moduleId + "/" + topicId2);
                }
            }
            if (moduleId != '') {
                if (window.history.state == null) {
                    if (moduleId.indexOf("/") > -1)
                        moduleId = moduleId.substr(0, moduleId.indexOf("/"));
                    window.history.replaceState({ ModuleId: moduleId }, "ModuleId", "/module/" + retitleUrl('<%#ModuleTitle%>') + "/" + moduleId);
                }
            }
        }
        //popup dialog
        function showDialogInfomation(dialogId) {
            PSCPortal.Services.CMS.GetArticleContent(dialogId, GetArticleContentCallback);
        }
        function GetArticleContentCallback(result) {
            //pscjq.Zebra_Dialog({
            //    source: { 'inline': result },
            //    'type': false,
            //    'buttons': false,
            //    'width': 900,
            //    'animation_speed_show': 200,
            //});         

            pscjq('#articleModal').find('.modal-body').html(result);
            pscjq('#articleModal').modal();

            pscjq('#articleModal').find('.modal-body').html(result);
            pscjq('#articleModal').modal();

        }
        pscjq(document).ready(function () {
            var offset = 300,
				 menuOffset = 80,
		    //browser window scroll (in pixels) after which the "back to top" link opacity is reduced
		    offset_opacity = 1200,
		    //duration of the top scrolling animation (in ms)
		    scroll_top_duration = 700,
		    //grab the "back to top" link
		    pscjqback_to_top = pscjq('.cd-top');
            pscjqmenu = pscjq('.menu');
            pscjqhead = pscjq('.header');

            var w768 = 768;
            var screenWidth = pscjq(window).width();
            //kiem tra trinh duyet [chrome: width scroll đứng = 17px] 
            var is_chrome = navigator.userAgent.toLowerCase().indexOf('chrome') > -1;
            if (is_chrome)
                screenWidth = screenWidth + 17;

            //hide or show the "back to top" link
            pscjq(window).scroll(function () {
                (pscjq(this).scrollTop() > offset) ? pscjqback_to_top.addClass('cd-is-visible') : pscjqback_to_top.removeClass('cd-is-visible cd-fade-out');
                if (pscjq(this).scrollTop() > offset_opacity) {
                    //pscjqmenu.addClass('scroll-out');
                    pscjqback_to_top.addClass('cd-fade-out');
                }
                //Ẩn hiện menu top && screen >768

                if (screenWidth >= w768) {
                    if (pscjq(this).scrollTop() > menuOffset) {
                        // hide head
                        pscjqhead.addClass('hidden');
                        // show fix menu
                        pscjqmenu.addClass('menu-scroll-out').fadeIn(450);
                    }
                    else {
                        // show menu
                        pscjqhead.removeClass('hidden');
                        // hide fix menu
                        pscjqmenu.removeClass('menu-scroll-out');
                    }
                }
                //(pscjq(this).scrollTop() > menuOffset) ? pscjqmenu.addClass('menu-scroll-out').fadeIn(450) : pscjqmenu.removeClass('menu-scroll-out');

            });

            //smooth scroll to top
            pscjqback_to_top.on('click', function (event) {
                event.preventDefault();
                pscjq('body,html').animate({
                    scrollTop: 0,
                }, scroll_top_duration);
            });
            //pscjq.scrollSpeed(100, 800);

        });


    </script>
    <script type="text/html" id="search-binding-template">
        <div class="s-tintuc">
            <div class='result_lucene'>
                <a data-bind="html: 'Kết quả tìm thấy: ' + Count" href='#'></a>
            </div>
            <%--      <div class="line_tt" style="clear: both;">
                <img src="/Resources/ImagesPortal/HomePage/line.jpg" style="width: 730px; height: 4px;" />
            </div>--%>
            <div class='nd_news'>
                <!-- ko foreach: Data -->
                <div style="margin: 5px 0px 5px 0px; float: left; width: 100%;">
                    <h4><a class="title_topicdisplay" data-bind="attr: { href: '/?ArticleId=' + Id }, html: Title"></a></h4>
                    <h3 class="h3_content" data-bind="html: Highligth + '...'"></h3>
                </div>
                <!-- /ko -->
                <div align='center' id='pagingContentHolder' class='ct_topic_r1'></div>
            </div>
        </div>
    </script>
   
</head>
<body class="body" onload='popunder();'>
    <form id="form1" runat="server">
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
        </telerik:RadWindowManager>
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
            <Services>
                <asp:ServiceReference Path="~/Services/PortletProxy.asmx" />
                <asp:ServiceReference Path="~/Services/ModuleProxy.asmx" />
                <asp:ServiceReference Path="~/Services/CMS.asmx" />
                <asp:ServiceReference Path="~/Services/WeatherService.asmx" />
            </Services>
        </telerik:RadScriptManager>
        <asp:PlaceHolder ID="phDisplay" runat="server"></asp:PlaceHolder>
       
        <a href="#0" class="cd-top">Top</a>
       
        <div id="articleModal" class="modal fade" role="form">
            <div class="modal-dialog modal-lg vertical-align-center-modal">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>
                    <div class="modal-body">
                        <p>Some text in the modal.</p>
                    </div>

                </div>
            </div>
        </div>

    </form>
       
</body>
</html>
