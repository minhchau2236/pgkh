<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopicAnnouncement_SVTL.ascx.cs"
    Inherits="PSCPortal.Portlets.TopicDisplay.TopicAnnouncement_SVTL" %>
<div class="hocbong_SV">
    <h3><a href="/?TopicId=<%#TopicId %>"><%#TopicName %></a></h3>
    <ul>
        <asp:Repeater ID="rptArticleRelation" runat="server">
            <ItemTemplate>
                <li><a href="<%#"/?ArticleId="+Eval("Id") %>"><%#Eval("Title") %></a></li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
    <div class="arrow_all_SV wobble-horizontal">
        <a href="/?TopicId=<%#TopicId %>">
            <img src="/Resources/ImagesPortal/SinhVienTL/arrow_UEL.png"></a>
    </div>
</div>
