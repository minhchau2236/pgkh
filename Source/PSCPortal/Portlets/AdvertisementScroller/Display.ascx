<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Display.ascx.cs" Inherits="PSCPortal.Portlets.AdvertisementScroller.Display" %>
<script src="/Porlets/affects.js" type="text/javascript"></script>
<style type="text/css">
    .NoBorder
    {
        text-decoration: none;
    }
</style>
<%--
<script src="crawler.js" type="text/javascript"></script>--%>
<%--
<script src="crawler_vertical.js" type="text/javascript"></script>--%>
<%--<div class="marquee" id="mycrawler">
    <span class="style1">Those confounded friars</span> <span class="style4">dully buzz that faltering jay</span>.
    <span class="style2">An appraising tongue acutely causes our courageous hogs. Their fitting submarines deftly break your approving improvisations</span>. <a href="htpp://www.google.com"> Her downcast</a> taxono<span 
        class="style3">mies actually box up</span> those disgusted turtles.
</div>

<script type="text/javascript">
marqueeInit({
	uniqueid: 'mycrawler',
	style: {
		'padding': '5px',
		'width': '100%',
		'background': 'lightyellow',
		'border': '1px solid #CC3300'
	},
	inc: 2, //speed - pixel increment for each iteration of this marquee's movement
	mouse: 'cursor driven', //mouseover behavior ('pause' 'cursor driven' or false)
	moveatleast: 2,
	neutral: 150,
	savedirection: true
});
</script>--%>
<%--<div class="marquee" id="mycrawler2">

<a href="coco.jpg" rel="lightbox[sushi]"> <img alt="" src="coco.jpg" /></a> <a href="beach.jpg" rel="lightbox[sushi]"><img alt="" src="beach.jpg" /></a> <a href="bonsai.jpg" rel="lightbox[sushi]"><img alt="" src="bonsai.jpg" /></a> <a href="mountain.jpg" rel="lightbox[sushi]"><img alt="" src="mountain.jpg" /></a> <a href="water.jpg" rel="lightbox[sushi]"> <img alt="" src="water.jpg" /></a>
</div>--%>
<div class="root">
    <div class="icon_left">
        <img alt="" src="/Resources/images/icon_left.jpg" /></div>
    <div class="tile_root">
        Thông tin - Quảng cáo</div>
</div>
<div style="width: 210px; border: 0px;">
    <asp:Panel ID="vMarquee" runat="server" Width="100%">
        <%--<a href="coco.jpg" rel="lightbox[sushi]"> <img alt="" src="coco.jpg" /></a><a href="#">test</a> <a href="beach.jpg" rel="lightbox[sushi]"><img alt="" src="beach.jpg" /></a> <a href="bonsai.jpg" rel="lightbox[sushi]"><img alt="" src="bonsai.jpg" /></a> <a href="mountain.jpg" rel="lightbox[sushi]"><img alt="" src="mountain.jpg" /></a> <a href="water.jpg" rel="lightbox[sushi]"> <img alt="" src="water.jpg" /></a>
<a href="#">test</a>--%>
    </asp:Panel>
</div>
<script type="text/javascript">
    function LoadImages(panelId, images, topicLinks) {
        var panelHolder = document.getElementById(panelId);
        panelHolder.innerHTML = "";
        var index = 0;
        for (index = 0; index < images.length; index++) {
            var link = document.createElement('a');
            link.setAttribute('href', topicLinks[index]);
            link.setAttribute("class", "NoBorder");
            link.setAttribute("className", "NoBorder");
            link.setAttribute("target", "_blank");
            panelHolder.appendChild(link);

            var imgElement = document.createElement('img');
            imgElement.setAttribute('src', images[index]);
            imgElement.setAttribute('width', '210px');
            imgElement.setAttribute('height', '100px');
            imgElement.setAttribute("Border", 0);

         imgElement.setAttribute("overflow", 'hidden');
            link.appendChild(imgElement);
        }

    }

</script>
