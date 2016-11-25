<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopicAnnouncement_BoMon.ascx.cs"
    Inherits="PSCPortal.Portlets.TopicDisplay.TopicAnnouncement_BoMonToan" %>
<div class="thongbao_BM">
    <div class="chuyenmuc_BM">
        <div class="left"><a href="/?TopicId=<%#TopicId %>"><%#TopicName %></a></div>
    </div>
    <div class="ndchuyenmuc_BM">
        <asp:Repeater ID="rptArticleRelation" runat="server">
            <ItemTemplate>
                <div class="list_BM">
                    <p><%#string.Format("{0:dd/MM/yyyy}",Eval("CreatedDate")) %></p>
                    <a href="<%#"/?ArticleId="+Eval("Id") %>"><%#Eval("Title") %></a>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>
