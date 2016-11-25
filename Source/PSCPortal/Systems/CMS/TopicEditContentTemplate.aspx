<%@ Page Title="<%#Resources.Site.TopicEditContentTemplate %>" Language="C#" MasterPageFile="~/Systems/DialogMasterPage.Master" AutoEventWireup="true" CodeBehind="TopicEditContentTemplate.aspx.cs" Inherits="PSCPortal.Systems.CMS.TopicEditContentTemplate" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Systems/CMS/Scripts/TopicEditContentTemplate.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function initialize(){
            strWarning="<%= Resources.Site.Warning %>";
            editor = $find("<%=RadEditor1.ClientID%>");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
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
        <tr>
            <td colspan="2" align="center">
                <telerik:RadEditor ID="RadEditor1" runat="server" ToolbarMode="Default">                            
                    <ImageManager ViewPaths="~/Resources/Images" UploadPaths="~/Resources/Images" MaxUploadFileSize="20480000" />
                    <MediaManager ViewPaths="~/Resources/Medias" UploadPaths="~/Resources/Medias" MaxUploadFileSize="204800000" />
                    <FlashManager ViewPaths="~/Resources/Flashs" UploadPaths="~/Resources/Flashs" MaxUploadFileSize="204800000" />
                    <Content>
                    </Content>
                </telerik:RadEditor>
            </td>
        </tr>    
    </table>
</asp:Content>
