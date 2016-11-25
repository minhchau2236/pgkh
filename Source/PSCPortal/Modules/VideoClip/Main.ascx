<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Main.ascx.cs" Inherits="PSCPortal.Modules.VideoClip.Main" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<style type="text/css">
    .videoMain {
        margin-top: 10px;
        margin-left: 10px;
    }

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

    a.SPKT_hotnews {
        font-family: Arial, tahoma, verdana;
        font-size: 11px;
        font-weight: bold;
        color: #737373;
        text-decoration: none;
    }

        a.SPKT_hotnews:visited {
            font-family: Arial, tahoma, verdana;
            font-size: 11px;
            font-weight: bold;
            color: #737373;
            text-decoration: none;
        }

        a.SPKT_hotnews:active {
            font-family: Arial, tahoma, verdana;
            font-size: 11px;
            font-weight: bold;
            color: #737373;
            text-decoration: none;
        }

        a.SPKT_hotnews:hover {
            font-family: Arial, tahoma, verdana;
            font-size: 11px;
            font-weight: bold;
            color: #028cc0;
            text-decoration: none;
        }

    .SPKT_titleleft {
        padding-left: 5px;
        padding-right: 5px;
        font-family: tahoma;
        font-weight: bold;
        font-size: 8.5pt;
        color: #737373;
    }

    .StypeNews {
        padding-left: 5px;
        padding-right: 10px;
        font-family: tahoma, verdana;
        font-weight: bold;
        font-size: 11px;
        color: #f30;
        text-decoration: none;
        height: 24px;
    }

        .StypeNews :hover {
            padding-left: 50px;
            padding-right: 10px;
            font-family: tahoma, verdana;
            font-weight: bold;
            font-size: 11px;
            color: #fd9b00;
            text-decoration: none;
            height: 24px;
        }

    ul.folderList {
        margin-top: -10px;
        list-style: square;
    }

        ul.folderList li {
            border-bottom: 1px #CCCCCC dotted;
            margin: 0px 15px 0px 25px;
            padding: 3px 0px 3px 0px;
            list-style: none;
        }

            ul.folderList li:last-child {
                border: none;
            }
</style>
<telerik:RadWindow ID="rwVideoDetail" OnClientBeforeClose="beforeClose" runat="server" Behaviors="Close, Move" Modal="True" VisibleStatusbar="False">
</telerik:RadWindow>
<div class="videoMain">
    <div class="title_topic_show">
        <div class="text_title_show">
            <img src="/Resources/ImagePhoto/icon_title.png" /><asp:HyperLink ID="hplArrivalTypeName"
                runat="server" Font-Underline="false"></asp:HyperLink>
        </div>
        <div class="line_topic_show">
            <img src="/Resources/ImagePhoto/arrow_title.png" />
        </div>
    </div>
    <br />
    <br />
    <div class="ct_tin">
        <ul class="folderList">
            <asp:Repeater ID="rptDocumentsInCategory" runat="server">
                <ItemTemplate>
                    <li>
                        <img src="/Resources/ImagePhoto/videoListIcon.png" />&nbsp;&nbsp;<a style="cursor: pointer" onclick="ShowDialog('<%#Eval("Path")%>')">
                            <%#Eval("Name")%> (<%#Eval("NumberOfFile")%> Videos) </a></li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
    <div class="part_trang">
        <div class="trang">
            <asp:Panel ID="pnPager" runat="server">
            </asp:Panel>
        </div>
    </div>
</div>
<script language="javascript" type="text/javascript">

    function beforeClose(sender, args) {
        sender.get_contentFrame().contentWindow.document.getElementById("pnVideo").innerHTML = "";
    }
    function ShowDialog(path) {
        var oWnd = $find("<%= rwVideoDetail.ClientID %>");
        oWnd.setSize(430, 590);
        oWnd.setUrl("/Modules/VideoClip/Detail.aspx?Path=" + path);
        oWnd.set_title("Video Clip");
        oWnd.show();
    }
</script>
