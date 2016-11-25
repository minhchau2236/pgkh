<%@ Page Title="" Language="C#" MasterPageFile="~/Systems/MasterPage.Master" AutoEventWireup="true" CodeBehind="ArticleCommentManage.aspx.cs" Inherits="PSCPortal.Systems.CMS.ArticleCommentManage" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Systems/CMS/Scripts/ArticleCommentManage.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function initialize() {
            oWnd = $find("<%= rwArticle.ClientID %>");
            strArticleNotChoose = "<%= Resources.Site.ArticleNotChoose %>";
            strWarning = "<%= Resources.Site.Warning %>";
            strInformation = "<%= Resources.Site.Information %>";
            grid = $find("<%=gvArticle.ClientID%>");
            strArticleChangeTopicPrimaryOnlyOne = "<%= Resources.Site.ArticleChangeTopicPrimaryOnlyOne %>";
            strArticleConfirmDelete = "<%= Resources.Site.ArticleConfirmDelete %>";
            strArticleUpdateTopicSuccess = "<%= Resources.Site.ArticleUpdateTopicSuccess %>";
            strArticleChangeTopicPrimarySuccess = "<%= Resources.Site.ArticleChangeTopicPrimarySuccess %>";
        }          
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindow ID="rwArticle" runat="server" Modal="True" VisibleStatusbar="False" OnClientClose="RadWindowArticleClose">
    </telerik:RadWindow>
    <div style="width:918px; float:left; padding-left:16px; padding-top:25px;">
	
	<div style="width:815px; float:left;">
    	<div class="left_box"></div>
        <div style="width: 795px;" class="box"><div class="title_box" align="left"><%= Resources.Site.ArticleManage %></div></div>
        <div class="right_box"></div>
        <div style="background-color:#edeeef; float:left; width:815px; padding-top:19px; padding-bottom:19px;">
            <div>
                  <button type="button" class="btn btn-success btn-xs" onclick="GetCommentList()"><i class="fa fa-desktop"></i> Quản lý câu hỏi</button>
            </div>
            <hr />
        	<div id="commentLinks" style="display: none;">                
                <%--<a id="btnComment" href="javascript:void(0)" style="display: none" onclick="GetCommentList();" class="Header">[Quản lý câu hỏi]</a>--%>                                                
            </div>
            <div>
                <telerik:RadGrid ID="gvArticle" runat="server" AutoGenerateColumns="False" 
                    AllowPaging="True" AllowSorting="True" GridLines="None" 
                    AllowMultiRowSelection="True" >
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
                            <telerik:GridBoundColumn HeaderText='User' DataField='UserData'>
                                <ItemStyle Width="50px" />
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
        <div style="width: 795px;" class="bottombox_center">&nbsp;</div>
        <div class="bottombox_right">&nbsp;</div>      
    </div>
</div> 
</asp:Content>

