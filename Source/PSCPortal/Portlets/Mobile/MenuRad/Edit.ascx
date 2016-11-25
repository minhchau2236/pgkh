<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Edit.ascx.cs" Inherits="PSCPortal.Portlets.Mobile.MenuRad.Edit" %>
<table style="width: 350px;" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td align="right" style="width: 140px;">
            Chọn Menu Master:</td>
        <td>
            <asp:DropDownList ID="ddlListMenuMaster" runat="server" Width="200px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td>
            <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" 
                style="height: 26px" Text="Lưu" Width="80px" />
            <asp:Button ID="btnCancel" runat="server" onclick="btnCancel_Click" Text="Hủy" 
                Width="80px" />
        </td>
    </tr>
</table>