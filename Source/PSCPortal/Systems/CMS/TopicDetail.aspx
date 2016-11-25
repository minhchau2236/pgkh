<%@ Page EnableViewState="false" Language="C#" MasterPageFile="~/Systems/DialogMasterPage.Master"
    AutoEventWireup="true" CodeBehind="TopicDetail.aspx.cs" Inherits="PSCPortal.Systems.CMS.TopicDetail"
    Title='<%# Resources.Site.TopicDetail %>' %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/Systems/CMS/Scripts/TopicDetail.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        function initialize() {
            txtName = document.getElementById("<%= txtName.ClientID %>");
            txtDescription = document.getElementById("<%= txtDescription.ClientID %>");
            rcbPage = $find("<%=rcbPage.ClientID %>");
            cbxRss = document.getElementById("<%= cbxRss.ClientID %>");
        }
    
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 100%;" align="center" cellpadding="4px;">
        <tr>
            <td style="width: 40%;" class="textinput" align="right">
                <%= Resources.Site.Id %>:
            </td>
            <td style="width: 60%">
                <asp:TextBox ID="txtId" runat="server" Width="200px" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 40%;" class="textinput" align="right">
                <%= Resources.Site.Name %>:
            </td>
            <td style="width: 60%">
                <asp:TextBox ID="txtName" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 40%;" class="textinput" align="right">
                <%= Resources.Site.Description %>:
            </td>
            <td style="width: 60%">
                <asp:TextBox ID="txtDescription" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 40%;" class="textinput" align="right">
                <%= Resources.Site.PageDisplay %>:
            </td>
            <td style="width: 60%">
                <telerik:RadComboBox Width="300px" DataTextField="Name" DataValueField="Id" ID="rcbPage"
                    runat="server">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td style="width: 40%;" class="textinput" align="right">Rss:
            </td>
            <td style="width: 60%">
                <input type="checkbox" id="cbxRss" runat="server" />
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
