<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Display.ascx.cs" Inherits="PSCPortal.Modules.SiteMap.Display" %>
<link href="/Components/quickTree/quickTree.css" rel="stylesheet" />
<script src="/Components/quickTree/jquery.quickTree.js"></script>
<style type="text/css">
    .quickTree a {
        color: black;
        font-size: 100%;
        font: inherit;
    }

    .quickTree .tree {
        padding: 0px 10px 0px 10px;
    }

    .quickTree, .tree, .expand {
        margin-top: 5px;
    }

    .sitemap {
        padding: 0px 10px 0px 10px;
    }

    .sitemap_title {
        margin-top: 10px;
        font-size: 14px;
        font: Arial;
        color: #1c78b4;
        font-weight: bold;
        padding-left: 10px;
        text-transform: uppercase;
    }
</style>
<script type="text/javascript">
    $(function () {
        var treeNodesList = $.parseJSON('<%#TreeNodesLinkList%>');
        ko.applyBindings(treeNodesList, $("#siteMapMain")[0]);
        $('ul.quickTree').quickTree();
    });
</script>
<div id="siteMapMain">
    <div class="sitemap_title"><%#MenuMasterName %></div>
    <img src="/Resources/ImagesPortal/HomePage/line.jpg" style="width: 100%; height: 4px;">
    <script type="text/html" id="treeNode">
        <li>
            <a data-bind="html: Name, attr: {href: NavigationUrl}"></a>
            <!-- ko if: Children.length>0 -->
            <ul>
                <!-- ko template: { name: 'treeNode', foreach: Children } -->
                <!-- /ko -->
            </ul>
            <!-- /ko -->
        </li>
    </script>
    <div class="sitemap">
        <ul id="quickTreeList" class="quickTree tree" data-bind="template: {name: 'treeNode',foreach: $data}"></ul>
    </div>
</div>

