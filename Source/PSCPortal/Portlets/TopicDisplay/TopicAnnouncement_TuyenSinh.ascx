<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopicAnnouncement_TuyenSinh.ascx.cs"
    Inherits="PSCPortal.Portlets.TopicDisplay.TopicAnnouncement_TuyenSinh" %>
<div class="thongbao_TS">
    <div class="chuyenmuc_TS">
        <div class="left"><a href="/?TopicId=<%#TopicId %>"><%#TopicName %></a></div>
    </div>
    <div class="ndchuyenmuc_TS">
        <asp:Repeater ID="rptArticleRelation" runat="server">
            <ItemTemplate>
                <div class="list_TS">
                    <p><%#string.Format("{0:dd/MM/yyyy}",Eval("CreatedDate")) %></p>
                    <a href="<%#"/?ArticleId="+Eval("Id") %>"><%#Eval("Title") %></a>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>
