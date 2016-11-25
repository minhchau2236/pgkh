<%@ Page Title="" Language="C#" MasterPageFile="~/Systems/DialogMasterPage.Master" AutoEventWireup="true" CodeBehind="ArticleSendDetail.aspx.cs" Inherits="PSCPortal.Systems.CMS.ArticleSendDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Systems/CMS/Scripts/ChooseTopicForArticle.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function initialize() {
            strWarning = "<%= Resources.Site.Warning %>";
            strTopicNotChoose = "<%= Resources.Site.TopicNotChoose %>";
            comboboxTopic = $find("<%= rcbTopic.ClientID %>");
            comboboxPage = $find("<%= rcbPage.ClientID %>");
        }
        var strWarning;
        var strTopicNotChoose;
        var comboboxTopic;
        var comboboxPage;
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
            var valueTopic = comboboxTopic.get_value();
            var valuePage = comboboxPage.get_value();
            if (valueTopic == '00000000-0000-0000-0000-000000000000') {
                radalert(strTopicNotChoose, 250, 100, strWarning);
                return;
            }
            PageMethods.Save(valueTopic, valuePage, CallWebMethodSuccess, CallWebMethodFailed);
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table style="width:100%;" align="center" cellpadding="4px;">
	<tr>
    	<td style="width:20%;" class="textinput" align="right"><%= Resources.Site.TopicSelect %>:</td>
        <td style="width:80%">
            <telerik:RadComboBox ID="rcbTopic" runat="server"  Width="400px"></telerik:RadComboBox>
        </td>
    </tr>   
    <tr>
    	<td style="width:20%;" class="textinput" align="right">Trang hiển thị:</td>
        <td style="width:80%">
            <telerik:RadComboBox ID="rcbPage" runat="server"  Width="400px"></telerik:RadComboBox>
        </td>
    </tr>
    <tr align="center">
        <td colspan="2"><img src="/Systems/CMS/Image/line.jpg" width="438" height="1" alt="" /></td>
    </tr>
    <tr>
        <td colspan="2">
            <table style="width:15%" align="center">
                <tr>
                    <td style="width:50%;"><a href="javascript:void(0)" onclick="Save();" class="submit"><%= Resources.Site.Save %></a></td>
                    <td style="width:50%;"><a href="javascript:void(0)" onclick="Cancel();" class="submit"><%= Resources.Site.Cancel %></a></td>
                </tr>
            </table>
        </td>
    </tr>
</table>  
</asp:Content>