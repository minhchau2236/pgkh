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
<div class="lichhoatdong">
    <p><a style="color: #8b0b34; text-decoration: none;" href="/?TopicId=<%#TopicId %>"><%#TopicName %></a></p>
    <asp:Repeater ID="rptArticleRelation" runat="server">
        <ItemTemplate>
            <div class="lich">
                <img src="/Resources/ImagesPortal/PhongBan/imgs/phong/Layer 6.png">
                <div class="lich-text">
                    <span style="color: #454545; font-size: 20px; font-size: 20px; font-weight: 400;"><%#string.Format("{0:dd/MM/yyyy}",Eval("CreatedDate")) %></span><br>
                    <br>
                    <a href="<%#"/?ArticleId="+Eval("Id") %>"><%#Eval("Title") %></a>
                </div>
                <!--end lich-text-->
            </div>
            <!--end lich-->
        </ItemTemplate>
    </asp:Repeater>
</div>
