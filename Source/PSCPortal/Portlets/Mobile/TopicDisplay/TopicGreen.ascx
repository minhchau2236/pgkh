<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Display.ascx.cs" Inherits="PSCPortal.Portlets.Mobile.TopicDisplay.Display" %>

<article class="mobileDisplay">
   
    <h1 class="thongbao_m">
        <img src="/Resources/ImagesPortal/HomePage/Mobile/i_thongbao.png"><a href="/?TopicId=<%#TopicId %>"><%#TopicName %></a></h1>
    <div class="new_m">
        <img src="<%# "/Services/GetArticleImage.ashx?Id=" + ArticleId %>">
        <a href="/?ArticleId=<%#ArticleId %>">
            <%#ArticleName%>
            <%--<%#(DateTime.Now.Date - (DateTime)ArticleCreatedDate).Days <=3?"<img src='/Resources/ImagePhoto/new.gif' style='width:20px;height:10px;float:inherit;margin-bottom:0px'></img>":"" %>--%>
        </a>
    </div>
    <ul>
        <asp:Repeater ID="rptArticleRelation" runat="server">
            <ItemTemplate>
                <li>
                    <a href="<%#"/?ArticleId="+Eval("Id") %>">
                        <%#Eval("Title")%>
                        <span><%#string.Format("{0:dd/MM/yyyy}",Eval("CreatedDate")) %><%#(DateTime.Now.Date - (DateTime)Eval("CreatedDate")).Days <=3?"<img src='/Resources/ImagePhoto/new.gif' style='width:20px;height:10px;float:inherit;margin-bottom:0px'></img>":"" %></span>
                    </a>
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
      
</article>
