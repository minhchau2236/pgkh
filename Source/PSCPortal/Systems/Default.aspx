<%@ Page Title="Quản Trị Portal" Language="C#" MasterPageFile="~/Systems/MasterPage.Master"
    AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PSCPortal.Systems.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width: 100%; float: left; padding-left: 16px; padding-top: 25px;">
        <div style="width: 308px; float: left; margin-right: 12px;">
            <div class="left_box">
            </div>
            <div class="box">
                <div class="title_box" align="center">
                    <%=Resources.Site.ContentManagement %></div>
            </div>
            <div class="right_box">
            </div>
            <div style="background-color: #edeeef; float: left; height: 120px; width: 308px; padding-top: 19px;
                padding-bottom: 19px;">
                <div class="link">
                    <a href="/Systems/CMS/TopicManage.aspx">
                        <%=Resources.Site.TopicManage %></a></div>
                <div class="link">
                    <a href="/Systems/CMS/ArticleManage.aspx">
                        <%=Resources.Site.ArticleManage %></a></div>
                <div class="link">
                    <a href="/Systems/CMS/MenuMasterManage.aspx">
                        <%=Resources.Site.MenuMasterManage %></a></div>
                <div class="link">
                    <a href="/Systems/Engine/PageManage.aspx">
                        <%=Resources.Site.PageManage %></a></div>
                <%--<div class="link"><a onclick="window.open('/Systems/Security/Science.aspx',null,'height=800,width=1024,status=yes,toolbar=no,menubar=no,location=no');" href=""><%=Resources.Site.ScientificHistory%></a></div>--%>
            </div>
            <div class="bottombox_left">
                &nbsp;</div>
            <div style="width: 288px;" class="bottombox_center">
                &nbsp;</div>
            <div class="bottombox_right">
                &nbsp;</div>
        </div>
        <asp:Panel ID="Panel1" runat="server">
            <div style="width: 308px; float: left;">
                <div class="left_box">
                </div>
                <div class="box">
                    <div class="title_box" align="center">
                        Quản lý</div>
                </div>
                <div class="right_box">
                </div>
                <div style="background-color: #edeeef; height: 120px; float: left; width: 308px; padding-top: 19px;
                    padding-bottom: 19px;">
                   <div class="link">
                        <a href="/Systems/CMS/ArticleSendManage.aspx">Bài viết được gửi đến</a></div>
                    <div class="link">
                        <a href="/Systems/CMS/TrashManage.aspx">
                            <%=Resources.Site.TrashManage %></a></div>
                    <div class="link">
                        <a href="/Systems/Security/RoleManage.aspx">Quản lý Nhóm</a></div>
                    <div class="link">
                        <a href="/Systems/Security/UserManage.aspx">
                            <%=Resources.Site.UserManage %></a></div>
                </div>
                <div class="bottombox_left">
                    &nbsp;</div>
                <div style="width: 288px;" class="bottombox_center">
                    &nbsp;</div>
                <div class="bottombox_right">
                    &nbsp;</div>
            </div>
        </asp:Panel>
    </div>
    <div style="width: 100%; float: left; padding-left: 16px; padding-top: 25px;">
        <asp:Panel ID="Panel2" runat="server">
            <div style="width: 308px; float: left; margin-right: 12px;">
                <div class="left_box">
                </div>
                <div class="box">
                    <div class="title_box" align="center">
                        Thống kê</div>
                </div>
                <div class="right_box">
                </div>
                <div style="background-color: #edeeef; height: 120px; float: left; width: 308px; padding-top: 19px;
                    padding-bottom: 19px;">                    
                    <div class="link">
                        <a href="/Systems/CMS/ArticleViewTimeReport.aspx">Thông kê lượt truy cập bài viết</a></div>
                    <div class="link">
                        <a href="/Systems/CMS/ArticleInPageViewTimeReport.aspx">Thông kê số bài viết được đăng</a></div>
                    <div class="link">
                        <a href="/Systems/CMS/VisitorViewTimeReport.aspx">Thông kê số lượng truy cập trang</a></div>
                   
                </div>
                <div class="bottombox_left">
                    &nbsp;</div>
                <div style="width: 288px;" class="bottombox_center">
                    &nbsp;</div>
                <div class="bottombox_right">
                    &nbsp;</div>
            </div>
        </asp:Panel>
        <asp:Panel ID="Panel3" runat="server">
            <div style="width: 308px; float: left;">
                <div class="left_box">
                </div>
                <div class="box">
                    <div class="title_box" align="center">
                       Album, Video</div>
                </div>
                <div class="right_box">
                </div>
                <div style="background-color: #edeeef; height: 120px; float: left; width: 308px; padding-top: 19px;
                    padding-bottom: 19px;">
                     <div class="link">
                        <a href="/Systems/CMS/AlbumExplorer.aspx">Quản lý Album</a></div>
                    <div class="link">
                        <a href="/Systems/CMS/VideoClipExplorer.aspx">Quản lý Video</a></div>
                         <div class="link">
                        <a href="/Systems/CMS/ArticleCommentManage.aspx">Bài viết có câu hỏi</a></div>
                </div>
                <div class="bottombox_left">
                    &nbsp;</div>
                <div style="width: 288px;" class="bottombox_center">
                    &nbsp;</div>
                <div class="bottombox_right">
                    &nbsp;</div>
            </div>
        </asp:Panel>
       
    </div>
</asp:Content>
