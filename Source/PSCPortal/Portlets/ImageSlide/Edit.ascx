<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Edit.ascx.cs" Inherits="PSCPortal.Portlets.ImageSlide.Edit" %>
<br />
<br />
<br />
<br />

<div>
     <table>
    <tr>
        <td>
            &nbsp;File ảnh:
            <asp:FileUpload ID="fuHinhAnh" runat="server" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnOk" runat="server" Text="OK" onclick="btnOk_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
                onclick="btnCancel_Click" />
        </td>
    </tr>
    <tr>
        <td style="color:Red; font-size:12px; font-family:Arial " align="center">
            <asp:Label ID="lbThongBao" runat="server" Text="Label" Visible="false"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="gvImage" runat="server" AutoGenerateColumns="False" 
                DataKeyNames="Name" CellPadding="4" ForeColor="#333333" GridLines="None" 
                onrowdeleting="gvImage_RowDeleting" Width="535px">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:ImageField DataImageUrlField="Path" HeaderText="Hình ảnh">
                    </asp:ImageField>
                    <asp:BoundField DataField="Name" HeaderText="Tên" />
                    <asp:CommandField DeleteText="Xóa" ShowDeleteButton="True" />
                </Columns>
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" 
                    HorizontalAlign="Center" />
                <EditRowStyle BackColor="#999999" />
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            </asp:GridView>
        </td>
    </tr>
</table>

    </div>