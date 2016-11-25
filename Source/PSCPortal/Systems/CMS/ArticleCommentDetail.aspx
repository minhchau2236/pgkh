<%@ Page Title="" Language="C#" MasterPageFile="~/Systems/DialogMasterPage.Master"
    AutoEventWireup="true" CodeBehind="ArticleCommentDetail.aspx.cs" Inherits="PSCPortal.Systems.CMS.ArticleCommentDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
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
                var txtTitle = document.getElementById("<%= txtTitle.ClientID %>");
                var txtName = document.getElementById("<%= txtName.ClientID %>");
                var txtEmail = document.getElementById("<%= txtEmail.ClientID %>");
                var rdiCreatedDate = $find("<%= rdiCreatedDate.ClientID %>");
                var txtQuestion = document.getElementById("<%= txtQuestion.ClientID %>");
                var txtAns = document.getElementById("<%= txtAns.ClientID %>");
                PageMethods.Save(txtTitle.value, txtName.value, txtEmail.value, rdiCreatedDate.get_selectedDate(), txtQuestion.value, txtAns.value, CallWebMethodSuccess, CallWebMethodFailed);
            }
            function SaveCallback(results, context, methodName) {
                var oWnd = GetRadWindow();
                oWnd.close(true);
            }
            function Cancel() {
                var oWnd = GetRadWindow();
                oWnd.close(false);
            }
            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow)
                    oWindow = window.radWindow;
                else if (window.frameElement.radWindow)
                    oWindow = window.frameElement.radWindow;
                return oWindow;
            }  
        </script>
       
    </telerik:RadCodeBlock>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table align="center" cellpadding="4px;">
        <tr>
            <td style="width: 40%;" class="textinput" align="right">
                Id:
            </td>
            <td style="width: 60%">
                <asp:TextBox ID="txtId" runat="server" Width="200px" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 40%;" class="textinput" align="right">
                Bài viết:
            </td>
            <td style="width: 60%">
                <asp:TextBox ID="txtIdArticle" runat="server" Width="200px" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 40%;" class="textinput" align="right">
                Tiêu đề:
            </td>
            <td style="width: 60%">
                <asp:TextBox ID="txtTitle" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 40%;" class="textinput" align="right">
                Tên người gửi:
            </td>
            <td style="width: 60%">
                <asp:TextBox ID="txtName" runat="server" Width="200px" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 40%;" class="textinput" align="right">
                Email người gửi:
            </td>
            <td style="width: 60%">
                <asp:TextBox ID="txtEmail" runat="server" Width="200px" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 40%;" class="textinput" align="right">
                Ngày gửi:
            </td>
            <td style="width: 60%">
                <telerik:RadDateInput ID="rdiCreatedDate" runat="server" DisplayDateFormat="dd/MM/yyyy:HH:mm:ss"
                    DateFormat="dd/MM/yyyy HH:mm:ss" Width="200px" MinDate="1900-01-01">
                </telerik:RadDateInput>
            </td>
        </tr>
        <tr>
            <td style="width: 40%;" class="textinput" align="right">
                Câu hỏi:
            </td>
            <td style="width: 60%">
               <%-- <textarea name="txtQuestion" id="txtQuestion" rows="10" cols="20" style="width: 300px"></textarea>--%>
               <asp:TextBox ID="txtQuestion" TextMode="MultiLine" Rows="10" runat="server" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 40%;" class="textinput" align="right">
                Trả lời:
            </td>
            <td style="width: 60%">
                <%--<textarea name="txtAns" id="txtAns" rows="10" cols="20" style="width: 300px"></textarea>--%>
                <asp:TextBox ID="txtAns" TextMode="MultiLine" Rows="10" runat="server" Width="300px"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table style="width: 100%;" align="center" cellpadding="4px;">
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
