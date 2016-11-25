<%@ Page Title="" Language="C#" MasterPageFile="~/Systems/DialogMasterPage.Master"
    AutoEventWireup="true" CodeBehind="ArticleComment.aspx.cs" Inherits="PSCPortal.Systems.CMS.ArticleComment" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Systems/CMS/Scripts/ArticleComment.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function initialize() {
            oWndComment = $find("<%= rwArticleComment.ClientID %>");
            strArticleNotChoose = "<%= Resources.Site.ArticleNotChoose %>";
            strWarning = "<%= Resources.Site.Warning %>";
            strInformation = "<%= Resources.Site.Information %>";
            gridComment = $find("<%=gvArticleComment.ClientID%>");           
            strArticleConfirmDelete = "<%= Resources.Site.ArticleConfirmDelete %>";            
        }          
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindow ID="rwArticleComment" runat="server" Modal="True" VisibleStatusbar="False"
        OnClientClose="RadWindowArticleCommentClose" Width="100%">
    </telerik:RadWindow>   
    <div>
        <a id="btnEdit" href="javascript:void(0)" style="display: inline;" onclick="ArticleEdit();"
            class="Header">[Hiệu chỉnh]</a> <a id="btnDelete" href="javascript:void(0)"
                style="display: inline;" onclick="ArticleDelete();" class="Header">[<%= Resources.Site.DeleteItem %>]</a>
        <a id="btnPublish" href="javascript:void(0)" style="display: inline;" onclick="ArticlePublish();"
            class="Header">[<%= Resources.Site.Publish %>]</a> <a id="btnUnpublish" href="javascript:void(0)"
                style="display: inline;" onclick="ArticleUnPublish();" class="Header">[<%= Resources.Site.Unpublish %>]</a>
    </div>
    <div>
         <telerik:RadGrid ID="gvArticleComment" runat="server" AutoGenerateColumns="False" 
                    AllowPaging="True" AllowSorting="True" GridLines="None" 
                    AllowMultiRowSelection="True"  Height="350px">
                    <HeaderContextMenu EnableAutoScroll="True"></HeaderContextMenu>
                    <MasterTableView ClientDataKeyNames="ID" AllowMultiColumnSorting="True">
                <Columns>
                    <telerik:GridClientSelectColumn>
                        <ItemStyle Width="10px" />
                    </telerik:GridClientSelectColumn>
                    <telerik:GridBoundColumn HeaderText='<%$ Resources:Site,Title %>' DataField="Title">
                        <ItemStyle Width="220px" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText='<%$ Resources:Site,CreatedDate %>' DataField="FeedBackDate"
                        DataFormatString="{0:dd/MM/yyyy}">
                        <ItemStyle Width="50px" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText='Câu hỏi' DataField="Content">
                        <ItemStyle Width="320px" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText='Trả lời' DataField="ContentReply">
                        <ItemStyle Width="320px" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText='Người gửi' DataField="NameSender">
                        <ItemStyle Width="80px" />
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn HeaderText='Email người gửi' DataField="EmailSender">
                        <ItemStyle Width="200px" />
                    </telerik:GridBoundColumn>
                    <telerik:GridImageColumn DataImageUrlFields="PathImage" DataImageUrlFormatString="{0}">
                        <ItemStyle Width="10px" />
                    </telerik:GridImageColumn>
                </Columns>
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="True" />
                <ClientEvents OnCommand="gvArticleComment_Command" />
                <Scrolling AllowScroll="True" UseStaticHeaders="True" />
            </ClientSettings>
        </telerik:RadGrid>
    </div>
</asp:Content>
