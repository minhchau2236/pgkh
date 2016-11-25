<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Display.ascx.cs" Inherits="PSCPortal.Portlets.Mobile.MenuRad.Display" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<link rel="stylesheet" href="/Components/sidr-package-1.2.1/stylesheets/jquery.sidr.dark.css">
<script src="/Components/sidr-package-1.2.1/jquery.sidr.min.js"></script>
<script>
    $(document).ready(function () {
        $('#simple-menu').sidr();
    });
    $("html").on("click", function (e) {
        var t = $(e.target);
        if (!(t.is("#sidr") || t.parents("#sidr").length > 0)) {
            $.sidr('close', 'sidr');
        }
    });

    function goBack() {
       
            $.sidr('close', 'sidr');
        
    }
</script>
<style>
    #sidr li a {
        font: inherit;
        font-family: Arial;
        font-weight: normal;
        text-shadow: initial;
        color: #fff;
    }

    #sidr input[type="text"]:focus {
        background: #fafafa !important;
        border-color: #999999;
        outline: none;
        color: #333;
    }

    #sidr input[type="text"][disabled] {
        background-color: #dddddd;
    }

    a.button_m {
        -moz-border-radius: 2px;
        -moz-box-shadow: 0 1px 0 rgba(255, 255, 255, 0.3) inset;
        -webkit-box-shadow: 0 1px 0 rgba(255, 255, 255, 0.3) inset;
        -webkit-border-radius: 2px;
        background-color: #f9f9f9; /* Alabaster */
        background-image: -webkit-gradient(linear, left top, left bottom, from(#f9f9f9), to(#f1f1f1));
        background-image: -webkit-linear-gradient(top, #f9f9f9, #f1f1f1);
        background-image: -moz-linear-gradient(top, #f9f9f9, #f1f1f1);
        background-image: -ms-linear-gradient(top, #f9f9f9, #f1f1f1);
        background-image: -o-linear-gradient(top, #f9f9f9, #f1f1f1);
        background-image: linear-gradient(top, #f9f9f9, #f1f1f1);
        box-shadow: 0 1px 0 rgba(255, 255, 255, 0.3) inset;
        border: 1px solid #dddddd;
        border-radius: 2px;
        color: #333333 !important;
        cursor: pointer;
        display: inline-block;
        filter: progid:DXImageTransform.Microsoft.gradient(startColorstr=#f9f9f9, endColorstr=#f1f1f1);
        font-size: 14px;
        font-weight: 700;
        line-height: 20px;
        margin: 0;
        padding: 4px 10px;
        text-decoration: none;
        text-shadow: 0 1px 0 #ffffff;
        vertical-align: middle;
        white-space: nowrap;
    }

        a.button_m:hover {
            -moz-box-shadow: 0 1px 0 rgba(255, 255, 255, 0.3) inset;
            -webkit-box-shadow: 0 1px 0 rgba(255, 255, 255, 0.3) inset;
            background-color: #ffffff;
            background-image: -webkit-gradient(linear, left top, left bottom, from(#ffffff), to(#f1f1f1));
            background-image: -webkit-linear-gradient(top, #ffffff, #f1f1f1);
            background-image: -moz-linear-gradient(top, #ffffff, #f1f1f1);
            background-image: -ms-linear-gradient(top, #ffffff, #f1f1f1);
            background-image: -o-linear-gradient(top, #ffffff, #f1f1f1);
            background-image: linear-gradient(top, #ffffff, #f1f1f1);
            border: 1px solid #dddddd;
            box-shadow: 0 1px 0 rgba(255, 255, 255, 0.3) inset;
            color: #333333;
            filter: progid:DXImageTransform.Microsoft.gradient(startColorstr=#ffffff, endColorstr=#f1f1f1);
        }

        a.button_m:active {
            -moz-box-shadow: 0 1px 0 #ffffff, inset 0 1px 1px rgba(0, 0, 0, 0.1);
            -webkit-box-shadow: 0 1px 0 #ffffff, inset 0 1px 1px rgba(0, 0, 0, 0.1);
            background-color: #f9f9f9;
            background-image: -webkit-gradient(linear, left top, left bottom, from(#f9f9f9), to(#f1f1f1));
            background-image: -webkit-linear-gradient(top, #f9f9f9, #f1f1f1);
            background-image: -moz-linear-gradient(top, #f9f9f9, #f1f1f1);
            background-image: -ms-linear-gradient(top, #f9f9f9, #f1f1f1);
            background-image: -o-linear-gradient(top, #f9f9f9, #f1f1f1);
            background-image: linear-gradient(top, #f9f9f9, #f1f1f1);
            box-shadow: 0 1px 0 #ffffff, inset 0 1px 1px rgba(0, 0, 0, 0.1);
            filter: progid:DXImageTransform.Microsoft.gradient(startColorstr=#f9f9f9, endColorstr=#f1f1f1);
        }
</style>
<div class="mobileDisplay">
    <div id="sidr">
        <div style="margin: 0 15px; padding-top: 10px;">
            <input type="text" style="margin: 0" placeholder="Tìm kiếm..." id="txtSearchArticle_M" onkeyup="return SearchProcess_M(event);">
        </div>
        <div style="margin: 5px 15px;">
            <a href="#" class="button_m" onclick="OnSearch(document.getElementById('txtSearchArticle_M').value);">Tìm kiếm</a>
            <a href="#" id="bntBack" class="button_m" onclick="goBack();">Trở về</a>
        </div>
        <ul>
            <asp:Repeater ID="rptMenu" runat="server">
                <ItemTemplate>
                    <li><a href="<%#"/?ArticleId="+Eval("Id") %>" rel="external"><%#Eval("Name") %></a></li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
</div>



