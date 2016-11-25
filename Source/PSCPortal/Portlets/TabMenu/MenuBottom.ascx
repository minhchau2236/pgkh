<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuBottom.ascx.cs" Inherits="PSCPortal.Portlets.TabMenu.MenuBottom" %>
<div class="block_BM">
    <asp:Repeater ID="rptMenuList" runat="server" OnItemDataBound="rptMenuList_ItemDataBound">
        <ItemTemplate>
            <div class="block<%# Container.ItemIndex + 1 %>_BM">
                <h3><a href="<%# Eval("NavigationURL").ToString().Replace("~/","")%>"><%# Eval("Name") %></a></h3>
                <ul>
                    <asp:Repeater ID="rptChild" runat="server">
                        <ItemTemplate>
                            <li><a href="<%# Eval("NavigationURL").ToString().Replace("~/","")%>">
                                <%# Eval("Name") %></a></li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>

