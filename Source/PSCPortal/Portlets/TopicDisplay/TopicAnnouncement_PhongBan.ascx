<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopicAnnouncement_PhongBan.ascx.cs"
    Inherits="PSCPortal.Portlets.TopicDisplay.TopicAnnouncement_PhongBan" %>
<%-- <div class="thongbao_PB">
    <div class="chuyenmuc_PB">
        <div class="left"><a href="/?TopicId=<%#TopicId %>"><%#TopicName %></a></div>
    </div>
    <div class="ndchuyenmuc_PB">
        <asp:Repeater ID="rptArticleRelation" runat="server">
            <ItemTemplate>
                <div class="list_PB">
                    <p><%#string.Format("{0:dd/MM/yyyy}",Eval("CreatedDate")) %></p>
                    <a href="<%#"/?ArticleId="+Eval("Id") %>"><%#Eval("Title") %></a>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div> --%>




<div class="thongbao-phong">
            	<a href="/?TopicId=<%#TopicId %>"><%#TopicName %></a>		
                <ul>
				 <asp:Repeater ID="rptArticleRelation" runat="server">
            <ItemTemplate>
			<li> <a href="<%#"/?ArticleId="+Eval("Id") %>"><%#Eval("Title") %></a></li>
            </ItemTemplate>
        </asp:Repeater>                	
                </ul>
            </div>