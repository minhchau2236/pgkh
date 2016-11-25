<%@ Page Title="" Language="C#" MasterPageFile="~/Systems/DialogMasterPage.Master" AutoEventWireup="true" CodeBehind="VideoClipDetail.aspx.cs" Inherits="PSCPortal.Systems.CMS.VideoClipDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Cancel() {
            pauseVideo();
            var oWnd = GetRadWindow();
            oWnd.close(false);
        }

        function Save() {
            pauseVideo();
            var txtTitle = document.getElementById("<%= txtTitle.ClientID %>");
            if (txtTitle.value == "")
                radalert("Bạn vui lòng nhập vào tiêu đề của video !!", 250, 100, "Chú ý");
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

        function linkClick() {
            document.getElementById('<%= trLink.ClientID %>').style.display = "table-row";
            document.getElementById('<%= trFile.ClientID %>').style.display = "none";
        }

        function fileClick() {
            document.getElementById('<%= trFile.ClientID %>').style.display = "table-row";
            document.getElementById('<%= trLink.ClientID %>').style.display = "none";
        }

        function pauseVideo() {
            _V_('myVideo').pause();
        }
    </script>
    <link href="http://vjs.zencdn.net/c/video-js.css" rel="stylesheet">
    <script src="http://vjs.zencdn.net/c/video.js"></script>
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
            <td style="width: 40%;" class="textinput" align="right">Tùy chọn up video</td>
            <td>
                <input id="rdLink" type="radio" runat="server" name="uploadOpp" value="link" onclick="linkClick()" />Đường dẫn
                <input id="rdFile" type="radio" runat="server" name="uploadOpp" value="file" onclick="fileClick()" />Up file
            </td>
        </tr>
        <tr id="trLink" runat="server">
            <td style="width: 40%;" class="textinput" align="right">Đường dẫn</td>
            <td>
                <asp:TextBox ID="txtLink" runat="server"></asp:TextBox>
                <asp:Button runat="server" ID="UploadLink" Text="Upload" OnClick="UploadLink_Click" />
            </td>
        </tr>
        <tr id="trFile" runat="server" style="display: none;">
            <td style="width: 40%;" class="textinput" align="right">File (mpg/mp4/avi)</td>
            <td>
                <asp:FileUpload ID="FileUploadControl" runat="server" />
                <asp:Button runat="server" ID="UploadButton" Text="Upload" OnClick="UploadButton_Click" />
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Label runat="server" ID="StatusLabel" Text="Tình trạng: " Visible="false" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center;">
                <div id="videoPreview" runat="server" style="margin-left: 60px;">
                    <div style="width: 450px; height: 300px; background-color: black;"></div>
                </div>
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
