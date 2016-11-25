<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopicNews_ChuaViet.ascx.cs"
    Inherits="PSCPortal.Portlets.TopicDisplay.TopicNews_ChuaViet" %>

<div>
<div class="tin_R_img">
         <div class="topic_tin_R">
                <a href="/?TopicId=<%#TopicId %>"><%#TopicName %></a>
         </div><!--End topic_tin_R-->
         <div class="ct_tin_img">
              <asp:Repeater ID="rptArticleRelation" runat="server">
                <ItemTemplate>
                    <div class="chuaviet"><img src="<%# "/Services/GetArticleImage.ashx?Id=" + Eval("Id") %>" width="90px" height="90px" />
                        <a href="<%#"/?ArticleId="+Eval("Id") %>"><%#Eval("Title") %></a></div>
                </ItemTemplate>
              </asp:Repeater>
           
				<div class="xemthem"><a href="/?TopicId=<%#TopicId %>">Xem thêm >></a></div>                
            </div><!--End ct_tin_img-->
        </div><!--End tin_R_img-->
                
        <div class="face">
            <img src="/Resources/ImagesPortal/HomePage/Imgs/facebook.jpg">
        </div><!--End face-->
</div>
<%--<div class="tintuc_Khoa">
    <div class="topic_tintucKhoa">
        <div class="left"><a href="/?TopicId=<%#TopicId %>" ><%#TopicName %></a></div>
    </div>
    <div class="ct_tinKhoa">
          <asp:Repeater ID="rptArticleRelation" runat="server">
            <ItemTemplate>
                <div class="list_newsKhoa">
                    <a href="<%#"/?ArticleId="+Eval("Id") %>" class="img_tinKhoa">
                        <img style="width: 75px;" src="<%# "/Services/GetArticleImage.ashx?Id=" + Eval("Id") %>"
                            alt="" />
                    </a>
                    <a href="<%#"/?ArticleId="+Eval("Id") %>" class="mota_tinkhoa"><%#Eval("Title") %></a>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>--%>
