<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="PSCPortal.Portlets.Media.Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width,initial-scale=1.0">
    <title>Responsive Tabs</title>
    <meta name="description" content="Responsive tabbed layout component built with some CSS3 and JavaScript">
    <link rel="stylesheet" href="css/font-awesome.min.css">
    <link rel="stylesheet" href="css/style.min.css">
    <%--<link rel="apple-touch-icon" href="/apple-touch-icon.png">--%>
    <%--<link rel="icon" type="image/png" href="/favicon.png">--%>
</head>
<body>

    <%--<header class="o-header">
        <nav class="o-header-nav">
            <a href="http://callmenick.com/_development/angular/intro-to-angular/" class="o-header-nav__link"><i class="fa fa-arrow-left"></i>Previous Demo</a>
            <a href="http://callmenick.com/post/simple-responsive-tabs-javascript-css" class="o-header-nav__link">Back To Article <i class="fa fa-star"></i></a>
        </nav>
        <div class="o-container">
            <h1 class="o-header__title">Responsive Tabs</h1>
        </div>
    </header>

    <main class="o-main">
  <div class="o-container">--%>

    <div class="o-section">
    <div id="tabs" class="c-tabs no-js">
        <div class="c-tabs-nav">
            <a href="#" class="c-tabs-nav__link is-active">
                <i class="fa fa-home"></i>
                <span>Pháp thoại</span>
            </a>
            <a href="#" class="c-tabs-nav__link">
                <i class="fa fa-book"></i>
                <span>Âm nhạc</span>
            </a>
        </div>
        <div class="c-tab is-active">
            <div class="c-tab__content">
                <h2>Pháp thoại</h2>
            </div>
        </div>
        <div class="c-tab">
            <div class="c-tab__content">
                <h2>Âm nhạc</h2>
            </div>
        </div>
    </div>
</div>

    <%--<div class="o-section">
        <div id="github-icons"></div>
    </div>
    </div>--%>
    <%--</main>--%>

    <%--<footer class="o-footer">
        <div class="o-container">
            <small>&copy; 2015, callmenick.com</small>
        </div>
    </footer>--%>

    <script src="js/src/tabs.js"></script>
    <script>
        var myTabs = tabs({
            el: '#tabs',
            tabNavigationLinks: '.c-tabs-nav__link',
            tabContentContainers: '.c-tab'
        });

        myTabs.init();
</script>

    <!-- EXTERNAL SCRIPTS FOR CALLMENICK.COM, PLEASE DO NOT INCLUDE -->
    <%--<script src="js/lib/githubicons.js"></script>
    <script src="js/lib/carbonad.js"></script>
    <script>
        (function (i, s, o, g, r, a, m) {
            i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
                (i[r].q = i[r].q || []).push(arguments)
            }, i[r].l = 1 * new Date(); a = s.createElement(o),
            m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
        })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');
        ga('create', 'UA-34160351-1', 'auto');
        ga('send', 'pageview');
    </script>--%>
    <!-- /EXTERNAL SCRIPTS -->

</body>
</html>
