<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopicNews_DoiTac.ascx.cs"
    Inherits="PSCPortal.Portlets.TopicDisplay.TopicNews_DoiTac" %>

<div class="blogtin_doitac">
    <div class="main_blogtindoitac">
        <h3><a href="/?TopicId=<%#TopicId %>"><%#TopicName %></a></h3>
        <a href="/?ArticleId=<%#ArticleId %>">
            <img style="width: 100px;" src="/Services/GetArticleImage.ashx?Id=<%#ArticleId %>" /></a>
        <a href="/?ArticleId=<%#ArticleId %>"><%#ArticleTitle %><span>(<%#string.Format("{0:dd/MM/yyyy}",ArticleCreateTime) %>)</span></a>
    </div>
    <div class="tinkhac_blogtindoitac">
        <ul>
            <asp:Repeater ID="rptArticleRelation" runat="server">
                <ItemTemplate>
                    <li><a href="<%#"/?ArticleId="+Eval("Id") %>"><%#Eval("Title") %></a></li>

                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
    <div class="arrow_all_doitac">
        <a class="wobble-horizontal" href="/?TopicId=<%#TopicId %>">
            <img src="/Resources/ImagesPortal/DoiTac/next_doitac.png"></a>
    </div>
</div>
