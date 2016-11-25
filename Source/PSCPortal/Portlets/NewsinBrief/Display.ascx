<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Display.ascx.cs" Inherits="PSCPortal.Portlets.NewsinBrief.Display" %>
<div class="T_RSS">
    <div class="T_title_rss">
        <a href="#">
            <%#TopicName %></a></div>
    <div id='__<%# ClientID %>'>
        <asp:Repeater ID="RPDisplay" runat="server">
            <ItemTemplate>
                <div class="T_rss1">
                    <div class="date_rss">
                        <%#string.Format("{0:dd/MM/yyyy}",Eval("CreatedDate")) %></div>
                    <div class="ct_rss">
                        <a href="<%# "/?ArticleId=" + Eval("Id")%>">
                    <%#Eval("Name")%>
                </a></div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>
