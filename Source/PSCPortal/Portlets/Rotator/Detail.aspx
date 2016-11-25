<%@ Page Title="" Language="C#" MasterPageFile="~/Systems/DialogMasterPage.Master" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="PSCPortal.Portlets.Rotator.Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">
        var txtTitle;
        var txtLink;
        var txtOrder;
        function CallWebMethodSuccess(results, context, methodName) {
            switch (methodName) {
                case "Save":
                    {
                        SaveCallback(results, context, methodName);
                    }
                    break;
            }
        }
        function CallWebMethodFailed(results, context, methodName) {
            alert(results._message);
        }
        function Save() {
            PageMethods.Save(txtTitle.value, txtLink.value, txtOrder.value, CallWebMethodSuccess, CallWebMethodFailed);
        }
        function SaveCallback(results, context, methodName) {
            var oWnd = GetRadWindow();
            oWnd.close(true);
        }
        function Cancel() {
            var oWnd = GetRadWindow();
            oWnd.close(false);
        }
        function pageLoad() {
            initialize();
        }

        function initialize() {
            txtTitle = document.getElementById("<%= txtTitle.ClientID %>");
            txtLink = document.getElementById("<%= txtLink.ClientID %>");
            txtOrder = document.getElementById("<%= txtOrder.ClientID %>");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 100%;" align="center" cellpadding="4px;">
        <tr>
            <td style="width: 40%;" class="textinput" align="right">Mã:
            </td>
            <td style="width: 60%">
                <asp:TextBox ID="txtId" Enabled="false" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 40%;" class="textinput" align="right">Tiêu đề:
            </td>
            <td style="width: 60%">
                <asp:TextBox ID="txtTitle" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 40%;" class="textinput" align="right">Hình:
            </td>
            <td style="width: 60%">
                <iframe marginheight="0" id="Iframe2" height="28px" marginwidth="0" style="background-color: #f3f3f3; border: 0px;" src="ImageUpload.aspx" width="400px"></iframe>
            </td>
        </tr>
        <tr>
            <td style="width: 40%;" class="textinput" align="right">Đường dẫn:
            </td>
            <td style="width: 60%">
                <asp:TextBox ID="txtLink" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 40%;" class="textinput" align="right">Thứ tự:
            </td>
            <td style="width: 60%">
                <asp:TextBox ID="txtOrder" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr align="center">
            <td colspan="2">
                <img src="/Systems/CMS/Image/line.jpg" width="438" height="1" alt="" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table style="width: 15%" align="center">
                    <tr>
                        <td style="width: 50%;">
                            <a href="javascript:void(0)" onclick="Save();" class="submit">Lưu</a>
                        </td>
                        <td style="width: 50%;">
                            <a href="javascript:void(0)" onclick="Cancel();" class="submit">Hủy</a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

