<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Display.ascx.cs" Inherits="PSCPortal.Portlets.TopicDisplay.Display" %>
<link href="/Portlets/TopicDisplay/Scripts/blueberry.css" rel="stylesheet" type="text/css" />
<script src="/Scripts/Pager.js" type="text/javascript"></script>
<script src="/Scripts/TPSlideShow.js" type="text/javascript"></script>
<script type="text/javascript">
    $(window).load(function () {
        $('.blueberry').blueberry();
    });
</script>
<style type="text/css">    
    .pager
    {
        float: left;
        /*padding-left:200px;*/
        width:100%;
    }
</style>
<div class="sukien">
    <div class="title">
        <a href="/?TopicId=<%#TopicId %>">

            <%#TopicName %></a></div>
    <div class="ct_sukien">
        <%--<div id="blueberry" runat="server" class="blueberry">
            <ul class="slides">
                <asp:Repeater ID="rptArticleRelation" runat="server">
                    <ItemTemplate>
                        <li>
                            <div class="ndtin">
                                <div class="img_tin">
                                    <%# PSCPortal.CMS.Article.GetAvatar((Guid)Eval("Id")) %></div>
                                <div class="ct_tin">
                                    <a href="<%#"/?ArticleId="+Eval("Id") %>">
                                        <%#Eval("Title")%></a>
                                    <p>
                                        <%# PSCPortal.CMS.Article.GetDescription((Guid)Eval("Id")) %></p>
                                </div>
                            </div>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>--%>
        <asp:Panel ID="TPNiceSlideShowMenuWrp" runat="server" CssClass="TPNiceSlideShowMenuWrp">
            <asp:Panel ID="SlideShowMenuId" runat="server" CssClass="TPNiceSlideShowMenu">
            </asp:Panel>
        </asp:Panel>
        <asp:Panel ID="tpniceslideshow_scrollbar" runat="server" CssClass="tpniceslideshow_scrollbar-vert">
            <asp:Panel ID="tpniceslideshow_handle" runat="server" CssClass="tpniceslideshow_handle-vert">
            </asp:Panel>
        </asp:Panel>
        <asp:Panel ID="paging" CssClass="pager" runat="server" Visible="true">
        </asp:Panel>
    </div>
</div>
<script type="text/javascript">
        var currentPage = 1;
        var sizeOfBlock = 5;
        var recordInPage =3;   
    function createPager()
    { 
        var pager = new Pager('<%# paging.ClientID %>',currentPage,sizeOfBlock,recordInPage,<%# ListArticle %>,'DisplayPagerContent');
        pager.Paging(1);
        
    }   
        
    function DisplayPagerContent(pager)
    {

        var menuHolder = document.getElementById('<%#SlideShowMenuId.ClientID %>');
        menuHolder.innerHTML = "";
        
        var datasource = pager.datasource;

        //khoi tao paging
        var start = pager.GetStartOfRecord();
        var end = pager.GetEndOfRecord();
        
        //Tao mang chua cac phan tu con cua datasource
        var arr = new Array();
        for(j=start ; j<end ;j++)
            {  
               arr.push(datasource[j]);
            }
       
       var slideShow = new TPSlideShow('<%#TPNiceSlideShowMenuWrp.ClientID %>', '<%#SlideShowMenuId.ClientID %>', '<%#tpniceslideshow_scrollbar.ClientID %>', '<%#tpniceslideshow_handle.ClientID %>',currentPage,sizeOfBlock,recordInPage,arr,<%# ListArticle %>);
       slideShow.createTPSlideShowMenu(); 
    }
    
    createPager();
      
</script>
