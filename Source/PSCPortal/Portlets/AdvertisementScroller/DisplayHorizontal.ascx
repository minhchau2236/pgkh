<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DisplayHorizontal.ascx.cs" Inherits="PSCPortal.Portlets.AdvertisementScroller.DisplayHorizontal" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<style type="text/css">
    .slide_item
    {
        margin: 0;
        padding: 0;
        margin-top: 3px;
        float: left;
        margin-left: 4px;
        margin-right: 4px;
    }
    .slide_item a img
    {
        border: 1px #e2dfdf solid;
        width: 118px;
        height: 80px;
    }
    .slide_itemi a
    {
        text-decoration: none;
        color: #504f4f;
        line-height: 22px;
    }
    .slide_item a:hover
    {
        color: #f88b00;
    }
    .scroll
    {
        width: 140px; height: 80px;
        }
</style>
<div style="clear: both; float: left; padding-left: 0px; width: 1002px">
    <telerik:RadRotator ID="radRotator" ItemWidth="142px" Width="1002px" Height="80px"
        ItemHeight="80px" runat="server" ScrollDuration="500" FrameDuration="1000">
        <ItemTemplate>           
            <div style="float: left; margin-right: 2px;">
            <%--<a id="link" href=''>
             <img src=' <%# Container.DataItem %>' style="width: 140px; height: 80px;" />
             </a>--%>
             <%# Container.DataItem %>'
            </div>
           <%-- <div style="float: left; margin-right: 2px;">
            <a id="link" href=''>
             <asp:Label ID="lblShortDesc" Text='<%# DataBinder.Eval(Container.DataItem, "Link")%>' runat="server"></asp:Label>
     </div>
             </a>
            </div>--%>
        </ItemTemplate>
    </telerik:RadRotator>
</div>
