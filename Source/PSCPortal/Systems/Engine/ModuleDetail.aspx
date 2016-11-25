<%@ Page EnableViewState="false" Title="<%# Resources.Site.ModuleDetail %>" Language="C#" MasterPageFile="~/Systems/DialogMasterPage.Master" AutoEventWireup="true" CodeBehind="ModuleDetail.aspx.cs" Inherits="PSCPortal.Systems.Engine.ModuleDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        input[type='text'] {
            line-height: 25px;
        }

        .submit-luu {
            padding: 5px 10px;
            margin-right: 10px;
            background-color: #698AC0;
            color: #fff;
            border-radius: 4px;
            border: 1px solid #698AC0;
        }

            .submit-luu:hover {
                background-color: #fff;
                text-decoration: none;
            }
        
    </style>
    <script language="javascript" type="text/javascript">
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
            //Ngọc - 18122015
            var required = false;
            Page_ClientValidate("ModuleValidationGroup");
            if (Page_IsValid) {
                required = true;
            }

            if (required) {
                var txtName = document.getElementById("<%= txtName.ClientID %>");
                var txtDisplayURL = document.getElementById("<%= txtDisplayURL.ClientID %>");
                var txtEditURL = document.getElementById("<%= txtEditURL.ClientID %>");
                var rcbPage = $find("<%=rcbPage.ClientID %>");
                PageMethods.Save(txtName.value, txtDisplayURL.value, txtEditURL.value, rcbPage.get_value(), CallWebMethodSuccess, CallWebMethodFailed);
            }
        }
        function SaveCallback(results, context, methodName) {
            if (results) {
                var oWnd = GetRadWindow();
                oWnd.close(true);
            }
            else {
                radalert("<strong style='color:#f00'>Đường dẫn file hiển thị không tồn tại!</strong>", 350, 100, "Thông báo");

                document.getElementById("<%= txtDisplayURL.ClientID %>").style.color = "#f00";// highlight cảnh báo
            }

        }
        function Cancel() {
            var oWnd = GetRadWindow();
            oWnd.close(false);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 100%;" align="center" cellpadding="4px;">
        <tr>
            <td style="width: 30%;" class="textinput" align="right"><%= Resources.Site.Id %>:</td>
            <td style="width: 70%">
                <asp:TextBox Enabled="false" ID="txtId" runat="server" Width="400px"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="textinput" align="right"><%= Resources.Site.Name %> :</td>
            <td>
                <asp:TextBox ID="txtName" runat="server" Width="400px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="NameRequired" runat="server" ControlToValidate="txtName"
                    CssClass="failureNotification" ErrorMessage="Vui lòng không bỏ trống!." ToolTip="Vui lòng không bỏ trống!"
                    ValidationGroup="ModuleValidationGroup">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="textinput" align="right"><%= Resources.Site.PathToFileDisplay %> :</td>
            <td>
                <asp:TextBox ID="txtDisplayURL" runat="server" Width="400px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDisplayURL"
                    CssClass="failureNotification" ErrorMessage="Vui lòng không bỏ trống!." ToolTip="Vui lòng không bỏ trống!"
                    ValidationGroup="ModuleValidationGroup">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="textinput" align="right"><%= Resources.Site.PathToFileModify %>:</td>
            <td>
                <asp:TextBox ID="txtEditURL" runat="server" Width="400px"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="textinput" align="right"><%= Resources.Site.PageDisplay %>:</td>
            <td>
                <telerik:RadComboBox ID="rcbPage" DataTextField="Name" DataValueField="Id" runat="server" Width="403px">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr align="center">
            <td colspan="2">
                <hr style="width: 600px;" />
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <a href="javascript:void(0)" onclick="Save();" class="submit submit-luu"><%= Resources.Site.Save %></a>
                <a href="javascript:void(0)" onclick="Cancel();" class="submit"><%= Resources.Site.Cancel %></a>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <p style="color: #698AC0; text-align: center"><strong>Ví dụ:</strong> ~/Modules/CMS/TopicDisplay1.ascx (Đường dẫn đến file hiển thị)</p>
            </td>
        </tr>
    </table>
</asp:Content>
