<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HotNewsSlider.ascx.cs" Inherits="PSCPortal.Portlets.TopicDisplay.HotNewsSlider" %>
<link rel="stylesheet" type="text/css" href="/Portlets/TopicDisplay/Scripts/styles.css">
<script type="text/javascript" src="/Portlets/TopicDisplay/Scripts/jquery.scroller.js"></script>
<script src="/Portlets/TopicDisplay/Scripts/System.Utility.ImageThumbSlider.js" type="text/javascript"></script>
<%--<script type="text/javascript" src="/Portlets/PanelBar/js/jquery-1.7.2.min.js"></script>--%>
<script type="text/javascript">   
    //$.noConflict();
    $(function() {
        var autoplaySlider = new System.Utility.ImageThumbSlider('<%#gallery.ClientID%>',<%#ArticleList%>);
        autoplaySlider.CreateSlider();
    });
</script>
<div class="container">
    <asp:Panel ID="gallery" runat="server"></asp:Panel>
</div>
