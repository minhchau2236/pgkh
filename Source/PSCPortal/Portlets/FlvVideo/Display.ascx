<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Display.ascx.cs" Inherits="PSCPortal.Portlets.FlvVideo.Display" %>

<link href="/Portlets/FlvVideo/Scripts/Copytpniceslideshow.css" rel="stylesheet"
    type="text/css" />
<script src="/Portlets/FlvVideo/Scripts/CopyTpniceslideshow.js" type="text/javascript"></script>
<script src="/Portlets/FlvVideo/Scripts/swfobject.js" type="text/javascript"></script>
<script src="/Portlets/FlvVideo/Scripts/TPSlideShow.js" type="text/javascript"></script>
<script src="/Portlets/FlvVideo/Scripts/Pager.js" type="text/javascript"></script>
<style type="text/css">
    .pager
    {
        font-size: .9em;
        font-weight: bolder;
        display: none;
    }
    .pager a
    {
        color: #0093dd;
        text-decoration: none;
    }
    .TPNSSwrapper
    {
        width: 218px;
    }
    .TPNiceSlideShowMenu div img
    {
        float: left;
        margin: 8px;
    }
    .TPNiceSlideShowMenu div
    {
        /*
        height: 60px;
        width: 185px;*/
        height: 35px;
        width: 218px;
    }
    .TPNiceSlideShowMenuContent
    {
        text-align: left;
        float: left;
    }
    .TPNiceSlideShow
    {
        /*
        height: 180px;
        width: 0px;
        float: left;*/
        height: 35px;
        width: 0px;
        float: left;
    }
    .TPNiceSlideShowMenuWrp
    {
        /*
        height: 180px;
        width: 185px;
        overflow: hidden;
          background: #CCCCCC;
          */
        height: 35px;
        width: 218px;
        overflow: hidden;
        background: #CCCCCC;
    }
    .tpniceslideshow_scrollbar-vert
    {
        /*
        height: 180px;
        width: 22px;
        float: right;*/
        height: 0px;
        width: 0px;
    }
    .tpniceslideshow_handle-vert
    {
        /* width: 22px;*/
        width: 0px;
    }
    .TPNiceSlideShowGallery .slideInfoZone
    {
        /*
        height: 200px;
        width: 200px;
        background: #CCCCCC;
        position: absolute;
        left: 50%;
        top: 50%;
        margin-top: -150px;
        margin-left: -100px;*/
        height: 600px;
        width: 218px;
        background: #CCCCCC;
        position: absolute;
        left: 50%;
        top: 50%;
        margin-top: -150px;
        margin-left: -100px;
    }
</style>
<div class="blog_topic_an2">
    <div class="topic_an_title_tienich">
        <a href="/?moduleid=39f51434-e27d-437f-a3b4-9a10c6f9b536">Video Clip</a></div>
    <div style="width: 208px; border: #135bac 1px solid;">
        <div style="width: 208px">
            <span id="videoHolder" runat="server" align="left" style="width: 20px">Bạn cần tải <a
                href="http://www.macromedia.com/go/getflashplayer">Flash Player</a> để xem được
                clip này.</span>
        </div>
        <div class="TPNSSwrapper">
            <div id="TPNiceSlideShow" class="TPNiceSlideShow">
            </div>
            <asp:Panel ID="TPNiceSlideShowMenuWrp" runat="server" CssClass="TPNiceSlideShowMenuWrp">
                <asp:Panel ID="SlideShowMenuId" runat="server" CssClass="TPNiceSlideShowMenu">
                </asp:Panel>
            </asp:Panel>
            <asp:Panel ID="tpniceslideshow_scrollbar" runat="server" CssClass="tpniceslideshow_scrollbar-vert">
                <asp:Panel ID="tpniceslideshow_handle" runat="server" CssClass="tpniceslideshow_handle-vert">
                </asp:Panel>
            </asp:Panel>
            <div style="clear: both;">
            </div>
        </div>
        <asp:Panel ID="paging" CssClass="pager" runat="server">
        </asp:Panel>
    </div>
</div>
<script type="text/javascript">
        var currentPage = 1;
        var sizeOfBlock = 5;
        var recordInPage =1;
    

    
    function createPager()
    { 
        var pager = new Pager('<%# paging.ClientID %>',currentPage,sizeOfBlock,recordInPage,<%# FlvVideo %>,'DisplayPagerContent');
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
       
            
       var slideShow = new TPSlideShow('<%#TPNiceSlideShowMenuWrp.ClientID %>', '<%#SlideShowMenuId.ClientID %>', '<%#tpniceslideshow_scrollbar.ClientID %>', '<%#tpniceslideshow_handle.ClientID %>', '<%#videoHolder.ClientID %>',arr);
       //slideShow.setMenuItems(arr);
       slideShow.createTPSlideShowMenu(); 
 
    }
    
    createPager();
       // startTPNiceSlideShow();
       //  window.onDomReady(startTPNiceSlideShow);
</script>
