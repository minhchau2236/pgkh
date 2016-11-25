<%@ Page EnableViewState="false" Title="<%# Resources.Site.ArticleEditTopic %>" Language="C#" MasterPageFile="~/Systems/DialogMasterPage.Master" AutoEventWireup="true" CodeBehind="ArticleTopic.aspx.cs" Inherits="PSCPortal.Systems.CMS.ArticleTopic" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script src="/Systems/CMS/Scripts/ArticleTopic.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">
        function initialize() {
            strWarning = "<%= Resources.Site.Warning %>";
            combobox = $find("<%= rcbTopic.ClientID %>");
            strTopicNotChoose="<%= Resources.Site.TopicNotChoose %>";
            grid=$find("<%=gvTopicBelong.ClientID%>");
        }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table style="width:100%;" align="center" cellpadding="4px;">
	<tr>
    	<td style="width:40%;" class="textinput" align="right"><%= Resources.Site.TopicSelect %>:</td>
        <td style="width:60%">
            <telerik:radcombobox id="rcbTopic" DataTextField="Path" DataValueField="Id" runat="server" onclientselectedindexchanged="rcbTopic_SelectedIndexChanged" width="400px"></telerik:radcombobox>
            <a href="javascript:void(0)" onclick="AddTopicBelong();" id="lnk_Add" class="submit">[<%= Resources.Site.AddItem %>]</a>
        </td>
    </tr>
    <tr>
    	<td style="width:40%;" class="textinput" align="right"><%= Resources.Site.ArticleBelongTopic %>:</td>
        <td style="width:60%">
            <telerik:radgrid id="gvTopicBelong" runat="server" autogeneratecolumns="False" Height="130px" gridlines="None" allowmultirowselection="True" width="250px">    
                <HeaderContextMenu EnableAutoScroll="True"></HeaderContextMenu>
                <MasterTableView ClientDataKeyNames="Id" AllowMultiColumnSorting="True" >
                    <Columns>
                        <telerik:GridClientSelectColumn>                        
                            <ItemStyle Width="20px" />
                        </telerik:GridClientSelectColumn> 
				        <telerik:GridBoundColumn HeaderText="<%$ Resources:Site,Name %>" DataField="Name">                        							
				        <ItemStyle Width="150px" />
				        </telerik:GridBoundColumn>
			        </Columns>
                </MasterTableView>  
                <ClientSettings EnableRowHoverStyle="true" >                                          
                    <Selecting AllowRowSelect="True" />
                    <ClientEvents OnCommand="gvTopicBelong_Command" />   
                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                </ClientSettings>                  
            </telerik:radgrid>
            <a href="javascript:void(0)" onclick="DeleteTopicBelong();" class="submit">[<%= Resources.Site.DeleteItem %>]</a>
        </td>
    </tr>     
    <tr align="center">
        <td colspan="2"><img src="/Systems/CMS/Image/line.jpg" width="438" height="1" alt="" /></td>
    </tr>
    <tr>
        <td colspan="2">
            <table style="width:15%" align="center">
                <tr>
                    <td style="width:50%;"><a href="javascript:void(0)" onclick="Save();" class="submit">[<%= Resources.Site.Save %>]</a></td>
                    <td style="width:50%;"><a href="javascript:void(0)" onclick="Cancel();" class="submit">[<%= Resources.Site.Cancel %>]</a></td>
                </tr>
            </table>
        </td>
    </tr>
</table>          
</asp:Content>    
