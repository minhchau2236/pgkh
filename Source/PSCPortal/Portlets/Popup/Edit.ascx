<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Edit.ascx.cs" Inherits="PSCPortal.Portlets.Popup.Edit" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<telerik:RadEditor runat="server" ID="RadEditor1">
</telerik:RadEditor> 
<asp:LinkButton ID="lbtAccept" runat="server" OnClick="lbtAccept_Click">[Lưu]</asp:LinkButton>
<asp:LinkButton ID="lbtCancel" runat="server" OnClick="lbtCancel_Click">[Thoát]</asp:LinkButton>
