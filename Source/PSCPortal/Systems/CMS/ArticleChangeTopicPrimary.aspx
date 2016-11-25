<%@ Page Title="<%# Resources.Site.ArticleChangeTopicPrimary %>" Language="C#" MasterPageFile="~/Systems/DialogMasterPage.Master" AutoEventWireup="true" CodeBehind="ArticleChangeTopicPrimary.aspx.cs" Inherits="PSCPortal.Systems.CMS.ArticleChangeTopicPrimary" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Systems/CMS/Scripts/ArticleChangeTopicPrimary.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function initialize() {
            rcbTopic = $find("<%= rcbTopic.ClientID %>");
            strTopicNotChoose = "<%= Resources.Site.TopicNotChoose %>";
            strWarning = "<%= Resources.Site.Warning %>";
        }       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width:100%;" align="center" cellpadding="4px;">	    
        <tr>
    	    <td style="width:40%;" class="textinput" align="right"><%= Resources.Site.ArticleBelongTopicPrimary %>:</td>
            <td style="width:60%">
                <telerik:RadComboBox Width="200px" DataTextField="Name" DataValueField="Id" ID="rcbTopic" runat="server">
                </telerik:RadComboBox>
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
