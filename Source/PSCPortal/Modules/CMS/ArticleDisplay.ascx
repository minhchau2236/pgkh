<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ArticleDisplay.ascx.cs"
    Inherits="PSCPortal.Modules.CMS.ArticleDisplay" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<style type="text/css">
    /*Article Display*/
    .content_detail {
        float: left;
        margin-top: 10px;
        width: 1000px;
    }



        .content_detail .tieude a {
            float: left;
            font: 14px Arial, Helvetica, sans-serif;
            text-transform: uppercase;
            color: #1c78b4;
            font-weight: bold;
            padding-left: 20px;
        }

        .content_detail .line_tt {
            float: left;
            padding: 5px 0px 5px 0px;
        }

            .content_detail .line_tt img {
                width: 1000px;
                height: 4px;
            }

        .content_detail .nd_detail {
            float: left;
            width: 960px;
            padding: 0px 20px 0px 20px;
        }

    /*.content_detail .nd_detail ul {
            clear: both;
            padding-left: 25px;
        }

        .content_detail .nd_detail li {
            list-style-image: url(/Resources/ImagesPortal/HomePage/icon.jpg);
        }*/

    /*.content_detail .arrow {
        float: right;
        margin-right: 20px;
    }*/


    .tieude {
        margin: 50px 0px 0px 0px;
        clear: both;
    }

        .tieude a {
            text-decoration: none;
            width: 165px;
            height: 23px;
            color: #BE1E2D;
            font-family: "Open Sans", Arial, Helvetica, sans-serif;
            font-size: 18px;
            font-weight: 600;
            line-height: 47px;
        }
    /*11/11/2015  HBU*/
    #Article_Display .row {
        margin-left: 0px !important;
    }

    .Article_Display_right {
        padding-left: 0px !important;
    }

    @media (max-width: 768px) {
        .tieude {
            margin: 50px 0px 0px 0px;
            padding: 15px;
            float: left;
        }

        .other_news ul {
            width: 100%;
            clear: both;
            padding: 0px 10px 0px 15px !important;
        }

        #tintuc-right {
            padding-bottom: 20px;
        }
    }

    ul.share-buttons {
        list-style: none;
        padding: 0;
    }
</style>

<script type="text/javascript">
    var dialogMethod;
    function CallWebMethodSuccess(results, context, methodName) {
        switch (methodName) {
            case "AllowWatchArticle":
                {
                    CheckUserWatchCallback(results, context, methodName);
                }
                break;
        }
    }

    function CallWebMethodFailed(results, context, methodName) {
        alert(results._message);
    }
    // check user login
    function getQuerystringNameValue(name) {
        // For example... passing a name parameter of "name1" will return a value of "100", etc.
        // page.htm?name1=100 or page.htm/name1/100
        var articleId;
        var winURL = window.location.href;
        var index = winURL.indexOf(name);
        if (index > -1) {
            winURL = winURL.substr(index, winURL.length);
            var arr = winURL.indexOf("/") > -1 ? winURL.split("/") : winURL.split("=");;
            articleId = arr[1];
        }
        return articleId;
    }
    function CheckUserWatchArticle() {
        PSCPortal.Services.CMS.AllowWatchArticle("<%#ArticleId%>", CallWebMethodSuccess);
    }
    var dialogMethod;
    function CheckUserWatchCallback(results, context, methodName) {
        if (!results) {
            dialogMethod = "CheckUserWatchArticle";
            var oWnd = $find("<%= rwUserLogin.ClientID %>");
            oWnd.setSize(380, 180);
            oWnd.setUrl("/Modules/CMS/UserLogin.aspx");
            oWnd.set_title("Login");
            oWnd.show();
        }
    }
    function RadCheckUserWatchClose(sender, args) {
        switch (dialogMethod) {
            case "CheckUserWatchArticle":
                {
                    PSCPortal.Services.CMS.CheckUserWatchArticle(CallCheckUserWatchWebMethodSuccess);
                }
                break;
        }
    }
    function CallCheckUserWatchWebMethodSuccess(results, context, methodName) {
        if (!results) {

            if (history.length === 1) {
                window.close();
            } else {
                window.history.back();
            }
        }
    }
    //end check user login

    window.onload = function (e) {
        CheckUserWatchArticle();
    }


</script>
<script type="text/javascript">
    function OnSendArticle() {
        var result = window.showModalDialog("/Modules/CMS/SendArticle.aspx", document.URL, "dialogHeight: 320px; dialogWidth: 450px;");
    }
    function OnBack() {
        window.history.back();
    }
    function OnGoToHead() {
        window.moveTo(0, 0);
    }

</script>
<script type="text/javascript">

    pscjq(document).ready(function () {
        pscjq(".bg_banner").hide();
        pscjq(".slidingDiv").hide();
        pscjq(".show_hide").show();

        pscjq('.show_hide').click(function () {
            pscjq(".slidingDiv").slideToggle();
        });
        pscjq('#print').click(function () {
            window.print();
        });
        var a = ('<%#TopicId %>')
        if (a == '235abc79-aad7-4e16-b249-7a9d4616b6c4') {
            pscjq(".home ul li a:nth-child(2)").attr("href", "/?moduleid=00000000-0000-0000-0000-000000000001&topicid2=<%#TopicId %>");
        }

       
    });

</script>





<%--<div class="article_display">--%>
<div id="Article_Display">
    <div class="home">
        <div class="topic">
            <div class="L_topic">
                <img alt="" src="/Resources/ImagesPortal/HomePage/Imgs/arrow_yellow.jpg" />
                <a href="/?TopicId=<%#TopicId %>" style="text-transform: capitalize;"><%# ListTopic%></a>
            </div>
            <!--L_topic-->
        </div>
    </div>
    <!--end home-->
    <div class="container">
        <div class="row">
            <div class="col-xs-12 col-sm-12 col-md-12">
                <div class="tuade">
                    <h3>
                        <%#ArticleTitle%></h3>
                </div>
                <!--end tua de-->
            </div>
        </div>

        <div class="row">
            <div class="col-xs-12 Article_Display_left ">
                <div id="content-tintuc">
                    <div style="color: #6F6F6F; text-align:right; font-style:italic; font-size:small"><%# ArticleCreatedDate.Date.ToString("dd/MM/yyyy")%></div>

                  <%--  <div class="tomtat"><%#ArticleDescription%> </div>--%>
                    <!--end tomtat-->
                    <div class="chitietbaiviet"><%#ArticleContent %></div>
                    <div style="clear: both; padding-top: 15px; float: right; padding-right: 5px; padding-left: 0px;">
                        <asp:Panel ID="pnPager" runat="server" ForeColor="Black">
                        </asp:Panel>
                    </div>                
                </div>
            </div>

            <asp:Panel ID="pnCactinkhac" runat="server" CssClass="cacTinKhac">

                <div class="tieude">
                    <a href="#" style="padding-bottom: 5px;">Tin Khác :</a>
                </div>
                <div class="orthersArticle">
                    <div class="other_news">
                        <ul>
                            <asp:Repeater ID="rptArticleOld" runat="server">
                                <ItemTemplate>
                                    <li><a href="<%#"/?ArticleId="+Eval("Id") %>">
                                        <%#Eval("Title")%></a>
                                        <br />
                                        <span><%# Convert.ToBoolean(Eval("IsVisibleCreateDate")) ==true ? "" :string.Format("{0:dd/MM/yyyy}",Eval("CreatedDate")) %></span>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>

                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
</div>


<telerik:RadWindow ID="rwUserLogin" runat="server" Behaviors="Close, Move" Modal="True" VisibleStatusbar="False"
    OnClientClose="RadCheckUserWatchClose">
</telerik:RadWindow>

<style type="text/css">
    .TelerikModalOverlay {
        filter: alpha(opacity=95) !important; /*for IE 5.5+*/
        opacity: 1 !important; /*for FF 2x, Opera 9x*/
        -moz-opacity: 1 !important; /*for FF 1x*/
        background-color: #BE1E2D !important;
    }
</style>
