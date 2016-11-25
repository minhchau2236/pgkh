<%@ Page Language="C#" EnableViewState="false" MasterPageFile="~/Systems/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="ArticleManage.aspx.cs" Inherits="PSCPortal.Systems.CMS.ArticleManage"
    Title="<%# Resources.Site.ArticleManage %>" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Systems/CMS/Scripts/ArticleManage.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function initialize() {
            oWnd = $find("<%= rwArticle.ClientID %>");
            strArticleNotChoose = "<%= Resources.Site.ArticleNotChoose %>";
            strWarning = "<%= Resources.Site.Warning %>";
            strInformation = "<%= Resources.Site.Information %>";
            grid = $find("<%=gvArticle.ClientID%>");
            tree = $find("<%= rtvTopic.ClientID %>");
            strArticleChangeTopicPrimaryOnlyOne = "<%= Resources.Site.ArticleChangeTopicPrimaryOnlyOne %>";
            strArticleConfirmDelete = "<%= Resources.Site.ArticleConfirmDelete %>";
            strArticleUpdateTopicSuccess = "<%= Resources.Site.ArticleUpdateTopicSuccess %>";
            strArticleChangeTopicPrimarySuccess = "<%= Resources.Site.ArticleChangeTopicPrimarySuccess %>";
            strArticleSearch = document.getElementById('txtFindArticle');
            strTopic = "<%= Resources.Site.TopicNotChoose %>";
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindow ID="rwArticle" runat="server" Modal="True" VisibleStatusbar="False" OnClientClose="RadWindowArticleClose">
    </telerik:RadWindow>
    <div style="width: 100%; float: left; padding-left: 16px; padding-top: 25px;">
        <div style="width: 208px; float: left; margin-right: 12px;">
            <div class="left_box"></div>
            <div style="width: 188px;" class="box">
                <div class="title_box" align="left"><%= Resources.Site.TopicManage %></div>
            </div>
            <div class="right_box"></div>
            <div style="background-color: #edeeef; float: left; height: 504px; overflow: auto; width: 208px; padding-top: 19px; padding-bottom: 19px;">
                <telerik:RadTreeView ID="rtvTopic" runat="server" DataTextField="Name" DataValueField="Id" EnableDragAndDrop="False" OnClientNodeClicked="TopicSelect" >
                </telerik:RadTreeView>
            </div>
            <div class="bottombox_left">&nbsp;</div>
            <div style="width: 188px;" class="bottombox_center">&nbsp;</div>
            <div class="bottombox_right">&nbsp;</div>
        </div>
        <div style="width: 594px; float: left;">
            <div class="left_box"></div>
            <div style="width: 574px;" class="box">
                <div class="title_box" align="left"><%= Resources.Site.ArticleManage %></div>
            </div>
            <div class="right_box"></div>
            <div style="background-color: #edeeef; float: left; width: 594px; padding-top: 19px; padding-bottom: 19px;">
                <div>
                    <input type="text" id="txtFindArticle" />
                    <telerik:RadButton runat="server" Skin="Office2007"  Text="Tìm kiếm"  OnClientClicked="ArticleSearch" AutoPostBack="false"></telerik:RadButton>
                </div>
                <hr />
                <div>
                    <button type="button" class="btn btn-success btn-xs" onclick="ArticleNew()"><i class="fa fa-plus"></i> Thêm mới</button>
                    <button type="button" class="btn btn-success btn-xs" onclick="ArticleEdit()"><i class="fa fa-pencil"></i> Hiệu chỉnh</button>
                    <button type="button" class="btn btn-success btn-xs" onclick="ArticleEditTopic()"><i class="fa fa-edit"></i> Hiệu chỉnh Chuyên mục</button>
                    <button type="button" class="btn btn-success btn-xs" onclick="ArticlePreview()"><i class="fa fa-binoculars"></i> Xem hiển thị</button>
                    <button type="button" class="btn btn-success btn-xs" onclick="GetCommentList()"><i class="fa fa-list"></i> Danh sách câu hỏi</button><hr />
                    <button type="button" class="btn btn-success btn-xs" onclick="ArticleChuaXB()"><i class="fa fa-file-text"></i> Bài viết chưa xuất bản</button>
                  
                    <button type="button" class="btn btn-success btn-xs" onclick="ArticleCommentNewList()"><i class="fa fa-calendar"></i> Các bài viết có câu hỏi mới</button>
                    <button type="button" class="btn btn-success btn-xs" onclick="ArticleSend()"><i class="fa fa-share"></i>Chuyển bài viết</button>
                    <button type="button" class="btn btn-success btn-xs" onclick="ArticleLoginEdit()"><i class="fa fa-user"></i> Cập nhật đăng nhập</button>
                    <hr />
                    <button type="button" class="btn btn-success btn-xs" onclick="ArticlePublish()"><i class="fa fa-calendar-check-o"></i> Xuất bản</button>
                    <button type="button" class="btn btn-success btn-xs" onclick="ArticleUnPublish()"><i class="fa fa-calendar-times-o"></i> K.xuất bản</button>
                    <button type="button" class="btn btn-success btn-xs" onclick="ArticleDelete()"><i class="fa fa-close"></i> Xóa</button>
                </div>
                <div id="articleLinks" style="display: none;">
                     <%--<a id="btnNew" style="display: none;"  href="javascript:void(0)" onclick="ArticleNew();"   class="Header">[<%= Resources.Site.AddItem %>]</a>
                    <a id="btnEdit" href="javascript:void(0)" style="display: none;" onclick="ArticleEdit();" class="Header">[<%= Resources.Site.EditItem %>]</a>
                    <a id="btnEditTopic" href="javascript:void(0)" style="display: none;" onclick="ArticleEditTopic();" class="Header">[<%= Resources.Site.ArticleEditTopic %>]</a>
                    <a id="btnPublish" href="javascript:void(0)" style="display: none;" onclick="ArticlePublish();" class="Header">[<%= Resources.Site.Publish %>]</a>
                    <a id="btnUnpublish" href="javascript:void(0)" style="display: none;" onclick="ArticleUnPublish();" class="Header">[<%= Resources.Site.Unpublish %>]</a>
                    <a id="btnDelete" href="javascript:void(0)" style="display: none;" onclick="ArticleDelete();" class="Header">[<%= Resources.Site.DeleteItem %>]</a>
                    <a id="btnPreview" href="javascript:void(0)" style="display: none;" onclick="ArticlePreview();" class="Header">[<%= Resources.Site.Preview %>]</a>--%>
                   <%-- <a id="btnChangeTopicPrimary"  href="javascript:void(0)" onclick="ArticleChangeTopicPrimary();" class="Header">[<%= Resources.Site.ArticleChangeTopicPrimary%>]</a>
                    <a id="btnAticleNoBelongTopicPrimary"  href="javascript:void(0)" onclick="GetArticleNoBelongTopicPrimary();" class="Header">[<%= Resources.Site.AticleNoBelongTopicPrimary%>]</a>--%>
                    <%--<a id="btnChuaXB" style="display: none;" href="javascript:void(0)" onclick="ArticleChuaXB();" class="Header">[Bài viết chưa xuất bản]</a>
                    <a id="btnComment" href="javascript:void(0)" style="display: none;" onclick="GetCommentList();" class="Header">[Danh sách câu hỏi]</a>
                    <a id="btnArticleCommentNew" href="javascript:void(0)" style="display: none;" onclick="ArticleCommentNewList();" class="Header">[Các bài viết có câu hỏi mới]</a>
                    <a id="btnSendArticle" href="javascript:void(0)" onclick="ArticleSend();" style="display: none;" class="Header">[Chuyển bài viết]</a>
                    <a id="btnArticleLoginEdit" href="javascript:void(0)" style="display: none;" onclick="ArticleLoginEdit();" class="Header">[Cập nhật đăng nhập]</a>--%>
                </div>
                <hr />
                <div>
                    <telerik:RadGrid ID="gvArticle" runat="server" AutoGenerateColumns="False"
                        AllowPaging="True" AllowSorting="True" GridLines="None"
                        AllowMultiRowSelection="True">
                        <HeaderContextMenu EnableAutoScroll="True"></HeaderContextMenu>
                        <MasterTableView ClientDataKeyNames="Id" AllowMultiColumnSorting="True">
                            <Columns>
                                <telerik:GridClientSelectColumn>
                                    <ItemStyle Width="10px" />
                                </telerik:GridClientSelectColumn>
                                <telerik:GridBoundColumn HeaderText='<%$ Resources:Site,Name %>' DataField="Name">
                                    <ItemStyle Width="120px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText='<%$ Resources:Site,Title %>' DataField="Title">
                                    <ItemStyle Width="120px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText='<%$ Resources:Site,CreatedDate %>' DataField="CreatedDate" DataFormatString="{0:dd/MM/yyyy}">
                                    <ItemStyle Width="50px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText='<%$ Resources:Site,ModifiedDate %>' DataField="ModifiedDate" DataFormatString="{0:dd/MM/yyyy}">
                                    <ItemStyle Width="50px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridImageColumn DataImageUrlFields="PathImage" DataImageUrlFormatString="{0}">
                                    <ItemStyle Width="10px" />
                                </telerik:GridImageColumn>
                                <telerik:GridBoundColumn HeaderText='User' DataField='UserAdd'>
                                    <ItemStyle Width="100px" />
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
            </div>
            <div class="bottombox_left">&nbsp;</div>
            <div style="width: 574px;" class="bottombox_center">&nbsp;</div>
            <div class="bottombox_right">&nbsp;</div>
        </div>
    </div>
</asp:Content>
