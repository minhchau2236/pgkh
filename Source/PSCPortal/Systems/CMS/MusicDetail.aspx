<%@ Page Title="" Language="C#" MasterPageFile="~/Systems/DialogMasterPage.Master" AutoEventWireup="true" CodeBehind="MusicDetail.aspx.cs" Inherits="PSCPortal.Systems.CMS.MusicDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Cancel() {
            var oWnd = GetRadWindow();
            oWnd.close(false);
        }

        function Save() {
            var txtTitle = document.getElementById("<%= txtTitle.ClientID %>");
            if (txtTitle.value == "")
                radalert("Bạn vui lòng nhập vào tiêu đề của music !!", 250, 100, "Chú ý");
            else
                PageMethods.Save(txtTitle.value, CallSaveSuccess, CallWebMethodFailed);
        }

        function CallSaveSuccess(results, context, methodName) {
            var oWnd = GetRadWindow();
            oWnd.close(true);
        }

        function CallWebMethodFailed(results, context, methodName) {
            radalert(results._message, 250, 100, "<%= Resources.Site.Warning %>");
        }

        function pauseMusic() {
            document.getElementById('myAudio').pause();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 100%;" align="center" cellpadding="4px;">
        <tr>
            <td style="width: 40%;" class="textinput" align="right">Mã</td>
            <td style="width: 60%">
                <asp:TextBox ID="txtId" runat="server" Width="200px" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 40%;" class="textinput" align="right">Tiêu đề</td>
            <td style="width: 60%">
                <asp:TextBox ID="txtTitle" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 40%;" class="textinput" align="right">File (mp3)</td>
            <td>
                <asp:FileUpload ID="FileUploadControl" runat="server" />
                <asp:Button runat="server" ID="UploadButton" Text="Upload" OnClick="UploadButton_Click" />
                <br />
                <asp:Label runat="server" ID="StatusLabel" Text="Tình trạng: " Visible="false" />
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <div id="musicPreview" runat="server" style="width: 250px; height: 80px"></div>
            </td>
        </tr>
        <tr align="center">
            <td colspan="2">
                <img src="/Systems/CMS/Image/line.jpg" width="438" height="1" alt="" /></td>
        </tr>
        <tr>
            <td colspan="2">
                <table style="width: 15%" align="center">
                    <tr>
                        <td style="width: 50%;"><a href="javascript:void(0)" onclick="Save();" class="submit"><%= Resources.Site.Save %></a></td>
                        <td style="width: 50%"><a href="javascript:void(0)" onclick="Cancel();" class="submit"><%= Resources.Site.Cancel %></a></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
