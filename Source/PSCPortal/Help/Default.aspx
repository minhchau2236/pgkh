<%@ Page Language="C#" MasterPageFile="~/Systems/MasterPage.Master"
    AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PSCPortal.Systems.Default" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="float:left;width: 840px;padding: 0 10px;">
    <object classid='clsid:D27CDB6E-AE6D-11cf-96B8-444553540000' codebase='http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0' width='100%' height='650'>
        <param name='movie' value='/Help/adminportal.swf' />
        <param name='quality' value='high' />
        <embed src='/Help/adminportal.swf' quality='high' pluginspage='http://www.adobe.com/shockwave/download/download.cgi?P1_Prod_Version=ShockwaveFlash' type='application/x-shockwave-flash' width='100%' height='800'></embed>
    </object>
        </div>
</asp:Content>
