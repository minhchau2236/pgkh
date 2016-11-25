<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopicAnnouncement.ascx.cs"
    Inherits="PSCPortal.Portlets.TopicDisplay.TopicAnnouncement" %>
<%--<div class="tintuc">
    <div class="tieude"><a href="/?TopicId=<%#TopicId %>"><%#TopicName %></a></div>
    <div class="line_tt1">
        <img src="/Resources/ImagesPortal/HomePage/line.jpg">
    </div>
    <div class="nd_news">
        <ul>
            <asp:Repeater ID="rptArticleRelation" runat="server">
                <ItemTemplate>
                    <li><a href="<%#"/?ArticleId="+Eval("Id") %>"><%#Eval("Title") %></a></li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
    <!--End nd_news-->
    <div class="arrow">
        <a class="wobble-horizontal" href="/?TopicId=<%#TopicId %>">
            <img src="/Resources/ImagesPortal/HomePage/arrow.png"></a>
    </div>
</div>--%>

<%--<div class="tintuc">
    <div class="title">
        <a href="/?TopicId=<%#TopicId %>"><%#TopicName %></a>
    </div>
    <!--end tintuc-->
    <div>
       <ul>
            <asp:Repeater ID="rptArticleRelation" runat="server">
                <ItemTemplate>
                    <li>
                        <div class="time"><a><%#string.Format("{0:dd/MM/yyyy}",Eval("CreatedDate")) %></a></div>
                        <div class="chu"><a href="<%#"/?ArticleId="+Eval("Id") %>"><%#Eval("Title") %></a></div>
                        </li>
                </ItemTemplate>
            </asp:Repeater>
           <li><div class="time"></div><div class="chu"><img src="Resources/ImagesPortal/HomePage/imgs/arrow_tin_tuc.png" style="    margin-top: -12px;"></div></li>
        </ul>
    </div>
    <!--end chitiet-->
</div>--%>

<div class="tin_R">
    <div class="topic_tin_R">
        <a href="/?TopicId=<%#TopicId %>"><%#TopicName %></a>
    </div><!--End topic_tin_R-->
    <div class="ct_tin_R">
        <ul data-bind="foreach: $data">
             <asp:Repeater ID="rptArticleRelation" runat="server">
                 <ItemTemplate>
                  <li><img src="/Resources/ImagesPortal/HomePage/Imgs/icon_list_red.jpg"><a href="<%#"/?ArticleId="+Eval("Id") %>"><%#Eval("Title") %></a><span><br><%#string.Format("{0:dd/MM/yyyy}",Eval("CreatedDate")) %></span></li>
             </ItemTemplate>
          </asp:Repeater>
    
        </ul>
    </div><!--End ct_tin_R-->
</div><!--End tin_R-->


<%--<div class="thong-bao" data-animation-target="2">
			<h1><a class="underline_from_center" href="/?TopicId=<%#TopicId %>"><%#TopicName %></a></h1>
			<ul class="container tb-columns">
			      <asp:Repeater ID="rptArticleRelation" runat="server">
                <ItemTemplate>
                    <li><a href="<%#"/?ArticleId="+Eval("Id") %>"><%#Eval("Title") %></a> </li>			
                </ItemTemplate>
            </asp:Repeater>                       
				
			</ul>
</div>--%>