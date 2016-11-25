<%@ Page Title="<%# Resources.Site.SubDomainDetail %>" Language="C#" MasterPageFile="~/Systems/DialogMasterPage.Master" AutoEventWireup="true" CodeBehind="SubDomainDetail.aspx.cs" Inherits="PSCPortal.Systems.Engine.SubDomainDetail" %>

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
    <script src="/Systems/Engine/Scripts/SubDomainDetail.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        function initialize() {
            txtName = document.getElementById("<%= txtName.ClientID %>");
            txtDescription = document.getElementById("<%= txtDescription.ClientID %>");
            rcbPage = $find("<%=rcbPage.ClientID %>");
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 100%;" align="center" cellpadding="4px;">
        <tr>
            <td style="width:30%;" class="textinput" align="right">
                <%= Resources.Site.Id %>:
            </td>
            <td style="width: 70%">
                <asp:TextBox ID="txtId" runat="server" Width="400px" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="textinput" align="right">
                <%= Resources.Site.Name %>:
            </td>
            <td>
                <asp:TextBox ID="txtName" runat="server" Width="400px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName"
                    CssClass="failureNotification" ErrorMessage="Vui lòng không bỏ trống!." ToolTip="Vui lòng không bỏ trống!"
                    ValidationGroup="ValidationGroup">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="textinput" align="right">
                <%= Resources.Site.Description %>:
            </td>
            <td>
                <asp:TextBox ID="txtDescription" runat="server" Width="400px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDescription"
                    CssClass="failureNotification" ErrorMessage="Vui lòng không bỏ trống!." ToolTip="Vui lòng không bỏ trống!"
                    ValidationGroup="ValidationGroup">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="textinput" align="right">Page:
            </td>
            <td>
                <telerik:RadComboBox Width="404px" DataTextField="Name" DataValueField="Id" ID="rcbPage"
                    runat="server">
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
                <a href="javascript:void(0)" onclick="Save();" class="submit  submit-luu">
                    <%= Resources.Site.Save %></a>

                <a href="javascript:void(0)" onclick="Cancel();" class="submit">
                    <%= Resources.Site.Cancel %></a>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <p style="color: #698AC0; text-align: left; line-height:25px;">
                    <strong style="text-decoration:underline">Ghi chú:</strong><br/>
                    -<strong>Tên</strong> subdomain: là tên viết tắt và không được đặt trùng.<br />
                    -Khi <strong>chỉnh sửa</strong> thì không cho sửa <strong>Tên</strong> Subdomain.
                </p>
            </td>
        </tr>
    </table>
</asp:Content>
