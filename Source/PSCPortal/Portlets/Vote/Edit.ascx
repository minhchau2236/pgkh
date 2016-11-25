<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Edit.ascx.cs" Inherits="PSCPortal.Portlets.Vote.Edit" %>
<div style="text-align:center;padding-top:10px">
<asp:DropDownList ID="ddlVoteQuestion" DataTextField="Name" DataValueField="Id" runat="server" Width="300px">
</asp:DropDownList>
        <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" />
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
            onclick="btnCancel_Click" />
</div>