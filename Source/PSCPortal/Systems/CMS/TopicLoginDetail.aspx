<%@ Page Title="" Language="C#" MasterPageFile="~/Systems/DialogMasterPage.Master"
    AutoEventWireup="true" CodeBehind="TopicLoginDetail.aspx.cs" Inherits="PSCPortal.Systems.CMS.TopicLoginDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">    
    <script src="Scripts/TopicLoginDetail.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function initialize() {
            txtName = document.getElementById("<%= txtName.ClientID %>");
            txtPassword = document.getElementById("<%= txtPassword.ClientID %>");
            cbIsCheck = document.getElementById("<%= cbIsCheck.ClientID %>");
        }
             
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 100%;" align="center" cellpadding="4px;">
        <tr>
            <td style="width: 40%;" class="textinput" align="right"><%= Resources.Site.Id %>:</td>
            <td style="width: 60%"><asp:TextBox ID="txtId" runat="server" Width="200px" Enabled="false"/></td>
        </tr>
        <tr>
            <td style="width: 40%;" class="textinput" align="right"><%= Resources.Site.Name %>:</td>
            <td style="width: 60%"><asp:TextBox ID="txtName" runat="server" Width="200px"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 40%;" class="textinput" align="right"><%= Resources.Site.Password %>:</td>
            <td style="width: 60%"><asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="200px"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 40%;" class="textinput" align="right">Cho phép đăng nhập:</td>
            <td style="width: 60%"> <asp:CheckBox ID="cbIsCheck" runat="server" /></td>
        </tr>
        <tr>
            <td colspan="2">
                <table style="width: 15%" align="center">
                    <tr>
                        <td style="width: 50%;">
                            <a href="javascript:void(0)" onclick="Save();" class="submit">
                                <%= Resources.Site.Save %></a>
                        </td>
                        <td style="width: 50%;">
                            <a href="javascript:void(0)" onclick="Cancel();" class="submit">
                                <%= Resources.Site.Cancel %></a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr align="center">
            <td colspan="2">
                <img src="/Systems/CMS/Images/line.jpg" width="438" height="1" alt="" />
            </td>
        </tr>
    </table>
    <div style="margin-left: 15px; font-family: Arial, Helvetica, sans-serif; font-weight: bold;
        margin-bottom: 15px">
        Chú ý:
        <span style="font-weight:normal; font-size:11px">Chức năng "Cập nhật đăng nhập" quản lý "Tên, Mật khẩu" của một chuyên mục. Khi người dùng muốn
       xem chuyên mục thì phải đăng nhập đúng "Tên, Mật khẩu" này.
        </span>
    </div>    
    <div style="margin-left: 25px; font-family: Arial,Helvetica, sans-serif; font-size: 12px;
        margin-bottom: 7px; margin-right: 20px;">
        - Thêm "Tên, Mật khẩu" (Nếu chưa tạo): Nhập "Tên, Mật khẩu" và chọn
        ô CheckBox. Sau đó nhấn nút Lưu.
    </div>
    <div style="margin-left: 25px; font-family: Arial,Helvetica, sans-serif; font-size: 12px;
        margin-bottom: 7px; margin-right: 20px;">
        - Cập nhật "Tên, Mật khẩu" (Nếu đã tạo nhưng muốn sửa lại): Nhập
        "Tên, Mật khẩu" và chọn ô CheckBox. Sau đó nhấn nút Lưu.
    </div>
    <div style="margin-left: 25px; font-family: Arial,Helvetica, sans-serif; font-size: 12px;
        margin-bottom: 7px; margin-right: 20px;">
        - Xóa "Tên, Mật khẩu" (Nếu đã tạo nhưng không muốn xét đăng nhập): Bỏ chọn ô CheckBox. Sau đó nhấn nút Lưu.
    </div>
</asp:Content>
