<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DisplayArticleTopic_TuyenSinh.ascx.cs"
    Inherits="PSCPortal.Portlets.TabTopicDisplay.Lib.DisplayArticleTopic_TuyenSinh" %>

    <div class="ct_tabTS">
        <div class="news_tabTS">
            <div class="img_tabTS">
                <a href="/?ArticleId=<%#ArticleId %>">
                    <img style="width: 110px;" src="/Services/GetArticleImage.ashx?Id=<%# ArticleId %>" />
                </a>
            </div>
            <div class="ct_newsTS">
                <a href="/?ArticleId=<%#ArticleId %>"><span><%#ArticleName %></span></a>
                <p><%#ArticleDescription%></p>
            </div>
            <div class="arrow_all_TS  wobble-horizontal">
                <a href="/?TopicId=<%#Topic.Id %>">
                    <img src="/Resources/ImagesPortal/TuyenSinh/arrow_TS.png"></a>
            </div>
        </div>
        <div class="tinkhac_tabTS">
            <ul>
                <asp:Repeater ID="rptTabTopic" runat="server">
                    <ItemTemplate>
                        <li><a href="<%#"?ArticleId="+Eval("Id") %>"><span><%# Eval("Title") %></span></a></li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
    </div>


