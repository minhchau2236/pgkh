<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopicAnnouncement_Khoa.ascx.cs"
    Inherits="PSCPortal.Portlets.TopicDisplay.TopicAnnouncement_Khoa" %>
<%--<div class="thongbao_Khoa">
    <div class="chuyenmuc_Khoa">
        <div class="left"><a href="/?TopicId=<%#TopicId %>" "><%#TopicName %></a></div>
    </div>
    <div class="ndchuyenmuc_Khoa">--%>


<div class="right">
            	<p><a href="/?TopicId=<%#TopicId %>" "><%#TopicName %></a></p>
        <asp:Repeater ID="rptArticleRelation" runat="server">
            <ItemTemplate>
               <%-- <div class="list_Khoa">
                    <p><%#string.Format("{0:dd/MM/yyyy}",Eval("CreatedDate")) %></p>
                    <a href="<%#"/?ArticleId="+Eval("Id") %>"><%#Eval("Title") %></a>
                </div>--%>
                  <div class="icon">
                      <a href="<%#"/?ArticleId="+Eval("Id") %>">
                        <img style="width: 310px;" src="<%# "/Services/GetArticleImage.ashx?Id=" + Eval("Id") %>"
                            alt="" /> </a>
                
                    <div class="title"><a  href="<%#"/?ArticleId="+Eval("Id") %>"><%#Eval("Title") %></a></div>
                   
                </div><!--end icon--->
            </ItemTemplate>
        </asp:Repeater>                       
</div>