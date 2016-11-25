<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DisplayArticleTopic_Khoa.ascx.cs"
    Inherits="PSCPortal.Portlets.TabTopicDisplay.Lib.DisplayArticleTopic_Khoa" %>
<div class="ct_tabKhoa">
    <div class="news_tabKhoa">
        <div class="img_tabkhoa">
            <a href="/?ArticleId=<%#ArticleId %>">
                <img style="width: 110px;" src="/Services/GetArticleImage.ashx?Id=<%# ArticleId %>" />
            </a>
        </div>
        <div class="ct_newsKhoa">
            <a href="/?ArticleId=<%#ArticleId %>"><span><%#ArticleName %></span></a>
            <p><%#ArticleDescription%></p>
        </div>
        <div class="arrow_all_Khoa  wobble-horizontal">
            <a href="/?TopicId=<%#Topic.Id %>">
                <img src="/Resources/ImagesPortal/Khoa/next_khoa.png"></a>
        </div>
    </div>
    <div class="tinkhac_tabKhoa">
        <ul>
            <asp:Repeater ID="rptTabTopic" runat="server">
                <ItemTemplate>
                    <li><a href="<%#"?ArticleId="+Eval("Id") %>"><span><%# Eval("Title") %></span></a></li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
</div>



