<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Edit.ascx.cs" Inherits="PSCPortal.Portlets.AdvertisementScroller.Edit" %>
<div>
    <table>
        <tr>
            <td>
                <asp:FileUpload ID="fuHinhAnh" runat="server" />
            </td>
        </tr>       
        <tr>
            <td>
                <asp:Button ID="btnOk" runat="server" Text="OK" OnClick="btnOk_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
            </td>
        </tr>
        <tr>
            <td style="color: Red; font-size: 12px; font-family: Arial" align="center">
                <asp:Label ID="lbThongBao" runat="server" Text="Label" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvImage" runat="server" AutoGenerateColumns="False" DataKeyNames="Name"
                    CellPadding="4" ForeColor="#333333" GridLines="Both" OnRowDeleting="gvImage_RowDeleting"
                    Width="500px" CssClass="GridViewStyle" OnRowEditing="gvImage_RowEditing" OnRowUpdating="gvImage_RowUpdating"
                    OnRowCancelingEdit="gvImage_RowCancelingEdit" HorizontalAlign="Center">
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns>                      
                          <asp:ImageField DataImageUrlField="Path" HeaderText="Hình ảnh" ReadOnly="true">
                        </asp:ImageField>
                        <asp:BoundField DataField="Name" HeaderText="Tên" ReadOnly="true" />
                        <asp:BoundField DataField="Link" HeaderText="Link" />
                        <asp:CommandField DeleteText="Xóa" ShowDeleteButton="True" />
                        <asp:CommandField SelectText="Editing" ShowEditButton="true" />
                    </Columns>
                    <EmptyDataRowStyle HorizontalAlign="Center" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                    <EditRowStyle BackColor="#999999" HorizontalAlign="Center" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                </asp:GridView>
            </td>
        </tr>
    </table>
</div>
