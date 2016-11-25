<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopicNews_CuuSV.ascx.cs"
    Inherits="PSCPortal.Portlets.TopicDisplay.TopicNews_CuuSV" %>
<div class="blogtin_CSV">
    <div class="main_blogtinCSV">
        <h3><a href="/?TopicId=<%#TopicId %>"><%#TopicName %></a></h3>
        <a href="/?ArticleId=<%#ArticleId %>">
            <img style="width: 61px;" src="/Services/GetArticleImage.ashx?Id=<%#ArticleId %>" /></a>
        <a href="/?ArticleId=<%#ArticleId %>"><%#ArticleTitle %><span>(<%#string.Format("{0:dd/MM/yyyy}",ArticleCreateTime) %>)</span></a>
    </div>
    <div class="tinkhac_blogtinCSV">
        <ul>
            <asp:Repeater ID="rptArticleRelation" runat="server">
                <ItemTemplate>
                    <li><a href="<%#"/?ArticleId="+Eval("Id") %>"><%#Eval("Title") %></a></li>

                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
    <div class="arrow_all_CSV">
        <a class="wobble-horizontal" href="/?TopicId=<%#TopicId %>">
            <img src="/Resources/ImagesPortal/CuuSinhVien/next_CSV.png"></a>
    </div>
</div>

