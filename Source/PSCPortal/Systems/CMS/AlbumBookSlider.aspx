<%@ Page Language="C#" MasterPageFile="~/Systems/MasterPage.Master"
    AutoEventWireup="true" CodeBehind="AlbumBookSlider.aspx.cs" Inherits="PSCPortal.Systems.CMS.AlbumBookSlider" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width: 815px; float: left; padding-left: 16px; padding-top: 0px;">
        <telerik:RadFileExplorer ID="RadFileExplorer1" runat="server" Width="815px">
            <Configuration MaxUploadFileSize="52428800" SearchPatterns="*.jpeg,*.png,*.jpg,*.pdf" ></Configuration>
        </telerik:RadFileExplorer>
    </div>
</asp:Content>
