<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Display.ascx.cs" Inherits="PSCPortal.Modules.AlbumBookSlider.Display" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%--<script type="text/javascript" src="/Components/turnjs4/jquery.min.1.7.js"></script>--%>
<script type="text/javascript" src="/Components/turnjs4/modernizr.2.5.3.min.js"></script>
<style type="text/css">
    .paging {
        font-family: Verdana;
        font-size: 11px;
        cursor: pointer;
        color: #3f4041;
        text-decoration: none;
    }

        .paging:hover {
            text-decoration: none;
            font-family: Verdana;
            font-size: 11px;
            cursor: pointer;
            color: #fd9b00;
        }

    .imgl_album1 {
        float: left;
    }

    .text_title_show1 {
        clear: both;
        float: left;
    }

        .text_title_show1 a {
            font: bold 12px Arial, Helvetica, sans-serif;
            color: #FF3300;
            padding-left: 7PX;
            text-decoration: none;
        }

            .text_title_show1 a:hover {
                color: #0083C1;
            }

    .line_topic_show1 {
        clear: both;
        padding-top: 3px;
        float: left;
    }

    .content_show1 {
        float: left;
        /*background-color: #f5f4f4;*/
        padding-left: 0px;
    }

    .row_show1 {
        clear: both;
        padding-left: 10px;
    }

    .td_album1 {
        float: left;
        width: 200px;
        margin: 10px 20px 10px 0px;
    }

    .hinhalbum1 {
        clear: both;
        padding: 15px 0px 0px 0px;
        background: url(/Resources/ImagePhoto/khunghinh.jpg) no-repeat;
        height: 252px;
        width: 223px;
    }

        .hinhalbum1 img {
            width: 190px;
            height: 230px;
        }

    .title_album1 {
        width: 220px;
        text-align: center;
    }

        .title_album1 a {
            font: bold 12px Arial, Helvetica, sans-serif;
            color: #0080C0;
            text-decoration: none;
        }

            .title_album1 a:hover {
                color: #FF6633;
            }

        .title_album1 span {
            font: normal 11px Arial, Helvetica, sans-serif;
            color: #999999;
        }

    .content_sp1 {
        padding: 5px 0px 5px 0px;
        float: left;
    }

    .row_sp1 {
        clear: both;
        float: left;
        margin-bottom: 30px;
        padding-left: 15px;
    }

    .part_trang1 {
        clear: both;
    }

    .trang1 {
        float: right;
        padding-top: 10px;
        padding-right: 10px;
    }

        .trang1 a {
            float: left;
            display: block;
            background: #EAEAEA;
            border: 1px #AEAEAE solid;
            padding: 2px 10px 2px 10px;
            margin: 0px 2px 0px 2px;
            color: #333;
        }

            .trang1 a:hover {
                background: #0097DF;
                color: #fff;
            }

    .date_album1 {
        width: 220px;
        text-align: center;
    }

        .date_album1 a {
            font: bold 12px Arial, Helvetica, sans-serif;
            color: #333;
            text-decoration: none;
            font-style: italic;
        }

    .down_album1 {
        width: 220px;
        text-align: center;
        padding-top:2px;
    }

        .down_album1 a {
            font: bold 12px Arial, Helvetica, sans-serif;
            color: #FF6633;
            text-decoration: underline;

        }
</style>
<script type="text/javascript">
    $(function () {
        $("#pnRight").hide();
        $(".flipbook").bind("turning", function (event, page) {
            if (page == 1)
                event.preventDefault();
        });
    });

    function loadApp() {
        $('.flipbook').turn({
            width: 990,
            turnCorners: "tl,tr",
            elevation: 50,
            acceleration: false,
            gradients: true,
            autoCenter: true,
            page: 2,
            display: "double"
        });
    }
    yepnope({
        test: Modernizr.csstransforms,
        yep: ['/Components/turnjs4/turn.js'],
        nope: ['/Components/turnjs4/turn.html4.min.js'],
        both: ['/Components/turnjs4/basic.css'],
        complete: loadApp
    });

</script>
<div style="float:left;width: 940px;padding: 10px 10px;">
<div class="title_topic_show" style="padding-top: 10px;">
    <div class="text_title_show1">
        <img src="/Resources/ImagePhoto/icon_title.png" />
        <asp:HyperLink ID="hplArrivalTypeName"
            CssClass="hplClass" runat="server" Font-Underline="false"></asp:HyperLink>
    </div>
    <div class="line_topic_show1">
        <img src="/Resources/ImagePhoto/arrow_title.png" />
    </div>
</div>
<asp:Panel ID="pnDisplayAlbum" runat="server">
    <div class="content_show1" id="Conten" runat="server" style="width: 995px; text-align: center;">
        <div class="row_show1">
            <asp:DataList ID="Pictype" runat="server" RepeatColumns="4" RepeatDirection="Horizontal"
                Width="995px" CellPadding="0" CellSpacing="0">
                <ItemTemplate>
                    <div class="td_album1">
                        <div class="hinhalbum1">
                            <img style="cursor: hand" src="<%#Eval("PathImgFirst") %>"
                                alt="" onclick="chuyentrang('<%#Eval("Path") %>')" />
                        </div>
                        <div class="title_album1">
                            <a href="#" onclick="chuyentrang('<%#Eval("Path") %>')">
                                <%#Eval("Name") %></a>
                        </div>
                        <div class="date_album1">
                            <a href="#" ">Đăng ngày : <%#string.Format("{0:dd/MM/yyyy}",Eval("CreateDate")) %></a>
                        </div>
                        <div class="down_album1">
                            <a href="<%#Eval("FileDownload") %>" target="_blank" >Tải về</a>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:DataList>
        </div>
    </div>
    <div class="part_trang1 ">
        <div class="trang1">
            <asp:Panel ID="pnAlbum" runat="server">
            </asp:Panel>
        </div>
    </div>
</asp:Panel>

<div class="flipbook-viewport" style="padding-bottom: 20px;">
    <div class="container">
        <div class="flipbook" style="float: left;">
            <div class="hard"></div>
            <asp:Repeater ID="Pic" runat="server">
                <ItemTemplate>

                    <div style="float: left; height: 700px; background-image: url('<%#Eval("Path") %>')"></div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</div>
<script language="javascript" type="text/javascript">
    function chuyentrang(Path) {

        window.location.href = "?ModuleId=<%#ConfigurationManager.AppSettings["ModuleAlbumBookSlider"]%>&AlbumTopic=" + '<%#TopicId%>' + "&Path=" + Path;
    }
</script>
</div>