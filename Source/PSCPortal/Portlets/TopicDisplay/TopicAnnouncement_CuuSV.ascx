<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopicAnnouncement_CuuSV.ascx.cs"
    Inherits="PSCPortal.Portlets.TopicDisplay.TopicAnnouncement_CuuSV" %>
<div class="thongbao_CSV">
    <h3><a href="/?TopicId=<%#TopicId %>"><%#TopicName %></a></h3>
    <ul>
        <asp:Repeater ID="rptArticleRelation" runat="server">
            <ItemTemplate>
                <li><a href="<%#"/?ArticleId="+Eval("Id") %>"><%#Eval("Title") %></a></li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
    <a class="all_TB_CSV wobble-horizontal" href="/?TopicId=<%#TopicId %>">
        <img src="/Resources/ImagesPortal/CuuSinhVien/next_CSV.png" /></a>
</div>
