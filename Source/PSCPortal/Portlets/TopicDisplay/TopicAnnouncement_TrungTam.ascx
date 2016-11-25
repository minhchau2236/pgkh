<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopicAnnouncement_TrungTam.ascx.cs"
    Inherits="PSCPortal.Portlets.TopicDisplay.TopicAnnouncement_TrungTam" %>
<div class="thongbao_CFIS">
    <div class="chuyenmuc_CFIS">
        <div class="left"><a href="/?TopicId=<%#TopicId %>"><%#TopicName %></a></div>
    </div>
    <div class="nd_chuyenmucCFIS">
        <asp:Repeater ID="rptArticleRelation" runat="server">
            <ItemTemplate>
                <div class="list_CFIS">
                    <p><%#string.Format("{0:dd/MM/yyyy}",Eval("CreatedDate")) %></p>
                    <a href="<%#"/?ArticleId="+Eval("Id") %>"><%#Eval("Title") %></a>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>
