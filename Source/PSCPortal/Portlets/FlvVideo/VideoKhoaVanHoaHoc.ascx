<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VideoKhoaVanHoaHoc.ascx.cs"
    Inherits="PSCPortal.Portlets.FlvVideo.VideoKhoaVanHoaHoc" %>
<link href="/Portlets/FlvVideo/Scripts/Copytpniceslideshow.css" rel="stylesheet"
    type="text/css" />
<script src="/Portlets/FlvVideo/Scripts/CopyTpniceslideshow.js" type="text/javascript"></script>
<script src="/Portlets/FlvVideo/Scripts/swfobject.js" type="text/javascript"></script>
<script src="/Portlets/FlvVideo/Scripts/TPSlideShow.js" type="text/javascript"></script>
<script src="/Portlets/FlvVideo/Scripts/Pager.js" type="text/javascript"></script>

<div class="K_truyenhinh">
    <div class="K_title_th">
        <span>Bản tin truyền hình</span><a href="/?moduleid=12d0585b-adc4-485a-86ba-6b4a37884f28">Tất cả <img src="/Resources/ImagesPortal/arrow_all.png" /></a></div>
    <div class="K_clip">
       
            <span id="videoHolder" runat="server" align="left" style="width: 20px">Bạn cần tải <a
                href="http://www.macromedia.com/go/getflashplayer">Flash Player</a> để xem được
                clip này.</span>
        
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
