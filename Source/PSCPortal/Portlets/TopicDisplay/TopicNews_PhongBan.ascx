<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopicNews_PhongBan.ascx.cs"
    Inherits="PSCPortal.Portlets.TopicDisplay.TopicNews_PhongBan" %>
<%-- <div class="tinmoi_PB">
    <div class="ndtin_PB">
        <a href="#">
            <img style="width:414px;" src="/Services/GetArticleImage.ashx?Id=<%#ArticleId %>"></a>
        <a href="/?ArticleId=<%#ArticleId %>" class="tieude_PB"><%#ArticleTitle %></a>
    </div>
    <div class="tinkhac_PB">
        <ul>
            <asp:Repeater ID="rptArticleRelation" runat="server">
                <ItemTemplate>
                    <li><a href="<%#"/?ArticleId="+Eval("Id") %>"><%#Eval("Title") %></a></li>

                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
    <div class="arrow_PB wobble-horizontal">
        <a href="/?TopicId=<%#TopicId %>">
            <img src="/Resources/ImagesPortal/PhongBan/arrow_red.png"></a>
    </div>
</div> --%>


<div class="tintuc-phong">
    <p><a style="color: #8b0b34; text-decoration: none" href="/?TopicId=<%#TopicId %>"><%#TopicName %></a></p>
    <div class="tintuc-hinhanh-phong">
        <div class="hinhanh1-phong" style="margin-right: 20px;">
            <a href="/?ArticleId=<%#ArticleId %>">
                <div class="img-phong">
                    <img src="/Services/GetArticleImage.ashx?Id=<%#ArticleId %>">
                </div>
                <!--end img-phong-->
                <div class="hover-hinh-phong">
                    <div class="chu-hover-phong">
                        <%#ArticleTitle %>
                    </div>
                    <!--end chu-hover-phong-->
                </div>
                <!--end hover-hinh-phong-->
            </a>
        </div>
        <!--end hinhanh1-phong-->
        <div class="hinhanh1-phong">
            <a href="/?ArticleId=<%#ArticleId2 %>">
                <div class="img-phong">
                    <img src="/Services/GetArticleImage.ashx?Id=<%#ArticleId2 %>">
                </div>
                <!--end img-phong-->
                <div class="hover-hinh-phong">
                    <div class="chu-hover-phong">
                        <%#ArticleTitle2 %>
                    </div>
                    <!--end chu-hover-phong-->
                </div>
                <!--end hover-hinh-phong-->
            </a>
        </div>
        <!--end hinhanh1-phong-->
    </div>
    <!--end tintuc-hinhanh-->
    <div class="tintuc-text-phong">
        <div class="tintuc-text">

            <ul>
                <asp:Repeater ID="rptArticleRelation" runat="server">
                    <ItemTemplate>
                        <li><a href="<%#"/?ArticleId="+Eval("Id") %>"><%#Eval("Title") %></a></li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
        <!--thongbao-phong-->
    </div>
    <!--end tintuc-hinhanh-->
</div>
