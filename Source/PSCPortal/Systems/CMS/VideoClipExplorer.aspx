<%@ Page Language="C#" MasterPageFile="~/Systems/MasterPage.Master" AutoEventWireup="true" CodeBehind="VideoClipExplorer.aspx.cs" Inherits="PSCPortal.Systems.CMS.VideoClipExplorer" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div style="width: 815px; float: left; padding-left: 16px; padding-top: 0px;">
        <telerik:RadFileExplorer ID="RadFileExplorer1" runat="server" Visible="false" Width="815px">
            <Configuration  MaxUploadFileSize="100000000"
                SearchPatterns="*.flv"></Configuration>
        </telerik:RadFileExplorer>
    </div>
</asp:Content>
