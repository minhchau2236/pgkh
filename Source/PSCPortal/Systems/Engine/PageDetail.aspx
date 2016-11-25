<%@ Page EnableViewState="false" Title="<%# Resources.Site.PageDetail %>" Language="C#"
    MasterPageFile="~/Systems/DialogMasterPage.Master" AutoEventWireup="true" CodeBehind="PageDetail.aspx.cs"
    Inherits="PSCPortal.Systems.Engine.PageDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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

            var txtName = document.getElementById("<%= txtName.ClientID %>");
            var txtTitle = document.getElementById("<%= txtTitle.ClientID %>");
            var template;
            var rdoTemplate1 = $get("<% =rdoTemplate1.ClientID %>");
            var rdoTemplate2 = $get("<% =rdoTemplate2.ClientID %>")
            var rdoTemplateMobile = $get("<% =rdoTemplateMobile.ClientID %>");
            var rdoTemplate4 = $get("<% =rdoTemplate4.ClientID %>")
            var rdoTemplate5 = $get("<% =rdoTemplate5.ClientID %>")
            var rdoTemplate6 = $get("<% =rdoTemplate6.ClientID %>")
            var rdoTemplate7 = $get("<% =rdoTemplate7.ClientID %>")
            var rdoTemplate8 = $get("<% =rdoTemplate8.ClientID %>")
            var rdoTemplate9 = $get("<% =rdoTemplate9.ClientID %>")
            var rdoTemplate10 = $get("<% =rdoTemplate10.ClientID %>")
            var rdoTemplate11 = $get("<% =rdoTemplate11.ClientID %>")
            var rdoTemplate12 = $get("<% =rdoTemplate12.ClientID %>")
            if (rdoTemplate1.checked)
                template = 1;
            if (rdoTemplate2.checked)
                template = 2;
            if (rdoTemplateMobile.checked)
                template = 3;
            if (rdoTemplate4.checked)
                template = 4;
            if (rdoTemplate5.checked)
                template = 5;
            if (rdoTemplate6.checked)
                template = 6;
            if (rdoTemplate7.checked)
                template = 7;
            if (rdoTemplate8.checked)
                template = 8;
            if (rdoTemplate9.checked)
                template = 9;
            if (rdoTemplate10.checked)
                template = 10;
            if (rdoTemplate11.checked)
                template = 11;
            if (rdoTemplate12.checked)
                template = 12;
            var language;
            var rdoVietNam = $get("<% =rdoVietNam.ClientID %>");
            var rdoEnglish = $get("<% =rdoEnglish.ClientID %>");
            if (rdoVietNam.checked)
                language = 1;
            if (rdoEnglish.checked)
                language = 2;
            var rcbLayout = $find("<%=rcbLayout.ClientID%>");
            PageMethods.Save(txtName.value, txtTitle.value, template, language, rcbLayout.get_value(), CallWebMethodSuccess, CallWebMethodFailed);
        }
        function SaveCallback(results, context, methodName) {
            var oWnd = GetRadWindow();
            oWnd.close(true);
        }
        function Cancel() {
            var oWnd = GetRadWindow();
            oWnd.close(false);
        }
    </script>
    <style type="text/css">
        .templatePage {
            width: 115px;
            float: left;
            padding-right: 5px;
            padding-top: 5px;
        }

        .imgTemplate {
            width: 95px;
            height: 65px;
            float: left;
            text-align: center;
        }

        .textTemplate {
            float: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 100%;" align="center" cellpadding="4px;">
        <tr>
            <td style="width: 30%;" class="textinput" align="right">
                <%= Resources.Site.Id %>:
            </td>
            <td style="width: 70%">
                <asp:TextBox Enabled="false" ID="txtId" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 30%;" class="textinput" align="right">
                <%= Resources.Site.Name %>:
            </td>
            <td style="width: 60%">
                <asp:TextBox ID="txtName" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 30%;" class="textinput" align="right">
                <%= Resources.Site.Title %>:
            </td>
            <td style="width: 60%">
                <asp:TextBox ID="txtTitle" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 30%;" class="textinput" align="right">Template:
            </td>
            <td style="width: 60%">
                <div class="templatePage">
                    <div class="imgTemplate">
                        <img src="/Systems/TemplateImage/NoTemplate.jpg" />
                    </div>
                    <div class="textTemplate">
                        <asp:RadioButton runat="server" GroupName="Template" ID="rdoTemplate1" Text="No Template" />
                    </div>
                </div>
                <div class="templatePage">
                    <div class="imgTemplate">
                        <img src="/Systems/TemplateImage/HomePage.png" />
                    </div>
                    <div class="textTemplate">
                        <asp:RadioButton runat="server" GroupName="Template" ID="rdoTemplate2" Text="Homepage"
                            Checked="true" />
                    </div>
                </div>                
                 <div class="templatePage">
                    <div class="imgTemplate">
                        <img src="/Systems/TemplateImage/NoTemplate.jpg" />
                    </div>
                    <div class="textTemplate">
                        <asp:RadioButton runat="server" GroupName="Template" ID="rdoTemplateMobile" Text="Mobile" />
                    </div>
                </div>
                 <div class="templatePage">
                    <div class="imgTemplate">
                        <img src="/Systems/TemplateImage/SinhVien.png" />
                    </div>
                    <div class="textTemplate">
                        <asp:RadioButton runat="server" GroupName="Template" ID="rdoTemplate4" Text="Sinh Viên" />
                    </div>
                </div>
                  <div class="templatePage">
                    <div class="imgTemplate">
                        <img src="/Systems/TemplateImage/SinhVienTL.png" />
                    </div>
                    <div class="textTemplate">
                        <asp:RadioButton runat="server" GroupName="Template" ID="rdoTemplate5" Text="Tân SV" />
                    </div>
                </div>
                 <div class="templatePage">
                    <div class="imgTemplate">
                        <img src="/Systems/TemplateImage/DoiTac.png" />
                    </div>
                    <div class="textTemplate">
                        <asp:RadioButton runat="server" GroupName="Template" ID="rdoTemplate6" Text="Đối Tác" />
                    </div>
                </div>
                <div class="templatePage">
                    <div class="imgTemplate">
                        <img src="/Systems/TemplateImage/CuuSinhVien.png" />
                    </div>
                    <div class="textTemplate">
                        <asp:RadioButton runat="server" GroupName="Template" ID="rdoTemplate7" Text="Cựu SV" />
                    </div>
                </div>
                 <div class="templatePage">
                    <div class="imgTemplate">
                        <img src="/Systems/TemplateImage/BoMon.png" />
                    </div>
                    <div class="textTemplate">
                        <asp:RadioButton runat="server" GroupName="Template" ID="rdoTemplate8" Text="Cán Bộ" />
                    </div>
                </div>
                 <div class="templatePage">
                    <div class="imgTemplate">
                        <img src="/Systems/TemplateImage/PhongBan.png" />
                    </div>
                    <div class="textTemplate">
                        <asp:RadioButton runat="server" GroupName="Template" ID="rdoTemplate12" Text="Phòng TS" />
                    </div>
                </div>
                <div class="templatePage">
                    <div class="imgTemplate">
                        <img src="/Systems/TemplateImage/Khoa.png" />
                    </div>
                    <div class="textTemplate">
                        <asp:RadioButton runat="server" GroupName="Template" ID="rdoTemplate9" Text="Khoa" />
                    </div>
                </div>
                 <div class="templatePage">
                    <div class="imgTemplate">
                        <img src="/Systems/TemplateImage/TrungTam.png" />
                    </div>
                    <div class="textTemplate">
                        <asp:RadioButton runat="server" GroupName="Template" ID="rdoTemplate10" Text="Trung Tâm" />
                    </div>
                </div>
                 <div class="templatePage">
                    <div class="imgTemplate">
                        <img src="/Systems/TemplateImage/PhongBan.png" />
                    </div>
                    <div class="textTemplate">
                        <asp:RadioButton runat="server" GroupName="Template" ID="rdoTemplate11" Text="Phòng Ban" />
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td style="width: 30%;" class="textinput" align="right">Ngôn ngữ:
            </td>
            <td style="width: 60%">
                <asp:RadioButton runat="server" GroupName="Language" ID="rdoVietNam" Text="Tiếng việt"
                    Checked="true" />
                <asp:RadioButton runat="server" GroupName="Language" ID="rdoEnglish" Text="Tiếng anh" />
            </td>
        </tr>
        <tr>
            <td style="width: 30%;" class="textinput" align="right">Layout:
            </td>
            <td style="width: 60%">
                <telerik:radcombobox id="rcbLayout" datatextfield="Name" datavaluefield="Id" runat="server" width="300px">
                </telerik:radcombobox>
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
    </table>
</asp:Content>
