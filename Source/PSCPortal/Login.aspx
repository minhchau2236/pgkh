<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PSCPortal.Login" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
        <%#Resources.Site.Login %></title>
    <link href="/Themes/web20/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager2" runat="server">
        </asp:ScriptManager>
        <div align="center">
            <div style="width: 408px;padding-right:15px;" align="right">
                <asp:LinkButton CssClass="textlink" ID="lbtVietnamese" runat="server" OnClick="lbtVietnamese_Click">[Vietnamese]</asp:LinkButton>
                <asp:LinkButton CssClass="textlink" ID="lbtEnglish" runat="server" OnClick="lbtEnglish_Click">[English]</asp:LinkButton>
                <a class="textlink" href="/LoginGoogleApi.aspx">[Đăng nhập Gmail]</a>
            </div>
        </div>
        <div align="center">
            <div style="width: 408px; margin-right: 12px;">
                <div class="left_box">
                </div>
                <div style="width: 388px;" class="box">
                    <div class="title_box" align="center">
                        <%#Resources.Site.Login %>
                    </div>
                </div>
                <div class="right_box">
                </div>
                <div style="background-color: #edeeef; float: left; width: 408px; padding-top: 19px; padding-bottom: 19px;">
                    <asp:Login ID="Login1" runat="server" Width="408px" DestinationPageUrl="~/Systems/Default.aspx">
                        <LayoutTemplate>
                            <table style="width: 100%;" align="center" cellpadding="4px;">
                                <tr>
                                    <td style="width: 40%;" class="textinput" align="right">
                                        <%#Resources.Site.Username%>:
                                    </td>
                                    <td style="width: 60%" align="left">
                                        <asp:TextBox Width="200px" ID="UserName" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                            ErrorMessage="<%$Resources:Site,UsernameRequire %>" ToolTip="<%$Resources:Site,UsernameRequire %>"
                                            ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 40%;" class="textinput" align="right">
                                        <%#Resources.Site.Password %>:
                                    </td>
                                    <td style="width: 60%" align="left">
                                        <asp:TextBox ID="Password" Width="200px" runat="server" TextMode="Password"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                            ErrorMessage="<%$Resources:Site,PasswordRequire %>" ToolTip="<%$Resources:Site,PasswordRequire %>"
                                            ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 40%;" class="textinput" align="right">&nbsp;
                                    </td>
                                    <td style="width: 60%" align="left">
                                        <label>
                                            <asp:CheckBox ID="RememberMe" runat="server" Text="" />
                                        </label>
                                        <span style="font-family: Tahoma; font-size: 11px; font-weight: bold;">
                                            <%#Resources.Site.Remember %></span>
                                    </td>
                                </tr>
                                <tr align="center">
                                    <td colspan="2">
                                        <img src="/Systems/CMS/Image/line.jpg" width="300" height="1" alt="" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="<%$Resources:Site,Login %>"
                                            ValidationGroup="Login1" />
                                    </td>
                                </tr>
                            </table>
                        </LayoutTemplate>
                    </asp:Login>
                    <telerik:RadCaptcha ID="radCaptchaValidate" runat="server" Visible="false" CaptchaTextBoxLabel="Gõ mã an toàn"
                        ValidationGroup="Login1">
                    </telerik:RadCaptcha>
                </div>
                <div class="bottombox_left">
                    &nbsp;
                </div>
                <div style="width: 388px;" class="bottombox_center">
                    &nbsp;
                </div>
                <div class="bottombox_right">
                    &nbsp;
                </div>
            </div>
        </div>
    </form>
</body>
</html>
