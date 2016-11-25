<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DisplayArticleTopic_TrungTam.ascx.cs"
    Inherits="PSCPortal.Portlets.TabTopicDisplay.Lib.DisplayArticleTopic_TrungTam" %>

<div class="nd_tabCFIS">
    <div class="tinmoi_CFIS">
        <a href="/?ArticleId=<%#ArticleId %>">
            <img style="width: 292px;" src="/Services/GetArticleImage.ashx?Id=<%# ArticleId %>" />
        </a>
        <a href="/?ArticleId=<%#ArticleId %>"><span><%#ArticleName %></span></a>
        <p><%#ArticleDescription%></p>
    </div>
    <div class="line_yellow">
        <img alt="" src="/Resources/ImagesPortal/TrungTam/line_yellow.jpg">
    </div>
    <div class="tinkhac_CFIS">
        <asp:Repeater ID="rptTabTopic" runat="server">
            <ItemTemplate>
                <div class="nd_tinkhacCFIS_No">
                    <a href="#">
                        <img style="width: 88px;" src="<%# "/Services/GetArticleImage.ashx?Id=" + Eval("Id") %>"
                            alt="" />
                        <a href="<%#"?ArticleId="+Eval("Id") %>"><span><%# Eval("Title") %></span></a>
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <div class="all_CFIS">
            <a href="/?TopicId=<%#Topic.Id %>">Tất cả
            <img alt="" src="/Resources/ImagesPortal/TrungTam/arrow_cfis.png"></a>
        </div>
    </div>
    <!--End tinkhac_CFIS-->
</div>
