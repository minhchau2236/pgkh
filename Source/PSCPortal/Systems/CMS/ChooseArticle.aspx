<%@ Page EnableViewState="false" Language="C#" MasterPageFile="~/Systems/DialogMasterPage.Master" AutoEventWireup="true"
    CodeBehind="ChooseArticle.aspx.cs" Inherits="PSCPortal.Systems.CMS.ChooseArticle" Title="Chọn Bài Viết" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Systems/CMS/Scripts/ChooseArticle.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function initialize() {
            grid = $find("<%=gvArticle.ClientID%>");
            tree = $find("<%= rtvTopic.ClientID %>");
            strWarning = "<%= Resources.Site.Warning %>";
            strArticleNotChoose = "<%= Resources.Site.ArticleNotChoose %>";
        }
    </script>
    <style type="text/css">
        /*chuyen muc*/
        .article-chuyenmuc {
            /*width: 208px;*/
            width:28%;
            float: left;
            margin-right: 12px;
        }
            .article-chuyenmuc .box {
                /*width: 188px;*/
                width: 94%;
            }
            .article-chuyenmuc .bottombox_center {
                /*width: 188px;*/
                width: 94%;
            }
        .cm-content {
            background-color: #edeeef;
            float: left;
            min-height: 308px;
            overflow: auto;
            /*width: 208px;*/
            width:100%;
            padding-top: 19px;
            padding-bottom: 19px;
        }
        /*bài viết*/
        .article-baiviet{width:69%; float: left;}
        .article-baiviet .box {
                /*width: 388px;*/
                width: 97%;
            }
        .article-baiviet .bottombox_center {
                /*width: 188px;*/
                width: 97%;
            }
        .grid-baiviet {
            background-color: #edeeef;
            float: left;
            /*width: 408px;*/
            width: 99%;
            padding-top: 19px;
            /*padding-bottom: 19px;*/
        }
        .gridCustom{min-height:230px;}

        /*lưu hủy*/
        .luu-huy{
            margin: 15px 0 0 0;
            float:left;
        }
        .luu-huy a:first-child{padding:5px 10px; background-color:#698AC0; color:#fff; border-radius:4px; border:1px solid #698AC0;margin-right:10px;}
        .luu-huy a:first-child:hover{background-color:#fff; text-decoration:none;color:#000;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td width="100%" colspan="2" align="center">
                <div align="left">
                    <div style="margin:0 auto; padding-left: 16px; padding-top: 25px;width:98%">
                        <div class="article-chuyenmuc">
                            <div class="left_box"></div>
                            <div class="box">
                                <div class="title_box" align="center">Chuyên Mục</div>
                            </div>
                            <div class="right_box"></div>
                            <div class="cm-content">
                                <telerik:RadTreeView ID="rtvTopic" runat="server" DataTextField="Name" DataValueField="Id" EnableDragAndDrop="False" OnClientNodeClicked="TopicSelect">
                                </telerik:RadTreeView>
                            </div>
                            <div class="bottombox_left">&nbsp;</div>
                            <div class="bottombox_center">&nbsp;</div>
                            <div class="bottombox_right">&nbsp;</div>
                        </div>
                        <div class="article-baiviet">
                            <div class="left_box"></div>
                            <div class="box">
                                <div class="title_box" align="center">Bài viết</div>
                            </div>
                            <div class="right_box"></div>
                            <div class="grid-baiviet">
                                <telerik:RadGrid ID="gvArticle" runat="server"  AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" GridLines="None" AllowMultiRowSelection="True" CssClass="gridCustom">
                                    <HeaderContextMenu EnableAutoScroll="True"></HeaderContextMenu>
                                    <MasterTableView ClientDataKeyNames="Id" AllowMultiColumnSorting="True">
                                        <Columns>
                                            <telerik:GridClientSelectColumn>
                                                <ItemStyle Width="20px" />
                                            </telerik:GridClientSelectColumn>
                                            <telerik:GridBoundColumn HeaderText="Tên" DataField="Name">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Tiêu đề" DataField="Title">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Ngày tạo" DataField="CreatedDate" DataFormatString="{0:dd/MM/yyyy}">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderText="Ngày sửa" DataField="ModifiedDate" DataFormatString="{0:dd/MM/yyyy}">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings EnableRowHoverStyle="true">
                                        <Selecting AllowRowSelect="True" />
                                        <ClientEvents OnCommand="gvArticle_Command" />
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </div>
                            <div class="bottombox_left">&nbsp;</div>
                            <div class="bottombox_center">&nbsp;</div>
                            <div class="bottombox_right">&nbsp;</div>

                            <div class="luu-huy">
                                <a href="javascript:void(0)" class="submit" onclick="Save();">Lưu</a>
                                <a href="javascript:void(0)" class="submit" onclick="Cancel();">Hủy</a>
                            </div>
                        </div>
                    </div>
                </div>
            </td>
        </tr>        
        
    </table>
</asp:Content>
