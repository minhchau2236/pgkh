<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Display.ascx.cs" Inherits="PSCPortal.Portlets.Counting.Display" %>
<div class="truycap">
    <p>
        <%# Resources.Site.VisitTotal %>:<span>
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></span>
    </p>
    <div style="display: none;">
        <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label></div>
</div>
