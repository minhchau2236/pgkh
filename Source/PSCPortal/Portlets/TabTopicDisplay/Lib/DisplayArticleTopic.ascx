<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DisplayArticleTopic.ascx.cs"
    Inherits="PSCPortal.Portlets.TabTopicDisplay.Lib.DisplayArticleTopic" %>
<div class="K_nd_tab">
    <asp:Repeater ID="rptTabTopic" runat="server">
        <ItemTemplate>
            <div class="K_tin_tab"> 
                <img style="cursor: hand; width:25%; height:70px; float:left;margin: 0 5px 0 0;" src="<%# "/Services/GetArticleImage.ashx?Id=" + Eval("Id") %>"
                            alt=""/>             
                <a href="<%#"?ArticleId="+Eval("Id") %>"><%# Eval("Title") %></a>
                <p>
                    <%#string.Format("{0:dd/MM/yyyy}",Eval("CreatedDate")) %></p>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>
