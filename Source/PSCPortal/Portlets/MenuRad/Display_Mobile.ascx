<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Display_Mobile.ascx.cs" Inherits="PSCPortal.Portlets.MenuRad.Display_Mobile" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<link rel="stylesheet" href="/Scripts/sidr-package-1.2.1/stylesheets/jquery.sidr.dark.css">
<script src="/Scripts/sidr-package-1.2.1/jquery.sidr.min.js"></script>
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
</style>
<div class="mobileDisplay">
    <div id="sidr">
        <div style="margin: 0 15px; padding-top: 10px;">
            <input type="text" placeholder="Tìm kiếm..." id="txtSearchArticle1" onkeyup="return SearchProcess(event);">

        </div>
        <ul>
            <asp:Repeater ID="rptMenu" runat="server">
                <ItemTemplate>
                    <li><a href="<%#"/?ArticleId="+Eval("Id") %>"><%#Eval("Name") %></a></li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
</div>



