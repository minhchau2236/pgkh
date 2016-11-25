<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Edit.ascx.cs" Inherits="PSCPortal.Portlets.TabTopicDisplay.Edit" %>
<table border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td>
            <asp:Label ID="Label2" runat="server" Text="Danh sách chuyên mục" 
                style="color: #0066FF"></asp:Label>
            
        </td>
        <td>
            &nbsp;
        </td>
        <td>
            <asp:Label ID="Label3" runat="server" Text="Các chuyên mục hiện thị" 
                style="color: #0066FF"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:ListBox ID="lbxTopicSource" runat="server" Height="250px" Width="250px" 
                SelectionMode="Multiple"></asp:ListBox>
        </td>
        <td>
            <asp:Button ID="btnAdd" runat="server" Text=">>" onclick="btnAdd_Click" />
            <br />
        </td>
        <td>
        <asp:Label ID="Label1" runat="server" Text="Số mục tin:" style="color: #0066FF"></asp:Label> &nbsp;<asp:TextBox ID="txtNumberAirticle" runat="server"></asp:TextBox>
        <br />
            <asp:GridView ID="gvListTopic" runat="server" CellPadding="4" ForeColor="#333333" 
                GridLines="None" AutoGenerateColumns="False" 
                 onrowdeleting="gvListTopic_RowDeleting" 
                onrowediting="gvListTopic_RowEditing" 
                onrowupdating="gvListTopic_RowUpdating" 
                onrowcancelingedit="gvListTopic_RowCancelingEdit">
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:TemplateField HeaderText="Chuyên mục">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" Text='<%#PSCPortal.CMS.Topic.GetTopic(Eval("TopicId").ToString())==null?"":PSCPortal.CMS.Topic.GetTopic(Eval("TopicId").ToString()).Name %>' CommandName="Select"></asp:LinkButton>
                            
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="NumberDisplay" HeaderText="Số mục tin" />
                    <asp:BoundField DataField="TabOrder" HeaderText="Thứ tự" />
                    <asp:CommandField EditText="Sửa" InsertText="Cập nhật" ShowEditButton="True" />
                    <asp:CommandField DeleteText="Xóa" ShowDeleteButton="True" />
                </Columns>
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
                <%--<SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />--%>
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td colspan="3" align="center">
            <asp:Button ID="btnOk" runat="server" Text="Hoàn tất" onclick="btnOk_Click" 
                style="color: #0066FF" />
        </td>
    </tr>
    
</table>