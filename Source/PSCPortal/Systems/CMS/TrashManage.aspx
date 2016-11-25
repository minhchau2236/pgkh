<%@ Page EnableViewState="false" Language="C#" MasterPageFile="~/Systems/MasterPage.Master" AutoEventWireup="true" CodeBehind="TrashManage.aspx.cs" Inherits="PSCPortal.Systems.CMS.TrashManage" Title="<%# Resources.Site.TrashManage %>" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Systems/CMS/Scripts/TrashManage.js" type="text/javascript"></script>
    <script src="/Scripts/Utility.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function initialize() {
            grid = $find("<%=gvArticleTrash.ClientID%>");
            strArticleNotChoose = "<%= Resources.Site.ArticleNotChoose %>";
            strWarning = "<%= Resources.Site.Warning %>";
            oWnd = $find("<%= rwTrash.ClientID %>");
            strArticleConfirmDelete = "<%= Resources.Site.ArticleConfirmDelete %>";
        }                
    </script>    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindow ID="rwTrash" runat="server" Modal="True" VisibleStatusbar="False" OnClientClose="RadWindowTrashClose">
    </telerik:RadWindow>
    <div style="width:818px; float:left; padding-left:16px; padding-top:25px;">
	<div style="width:815px; float:left; margin-right:12px;">
    	<div class="left_box"></div>
        <div style="width: 795px;" class="box"><div class="title_box" align="left"><%= Resources.Site.TrashManage %></div></div>
        <div class="right_box"></div>
        <div style="background-color:#edeeef; float:left; width:815px; padding-top:19px; padding-bottom:19px;">
            <div>
                 <button type="button" class="btn btn-success btn-xs" onclick="ArticleTrashRestore()"><i class="fa fa-undo"></i> Phục hồi</button>
                 <button type="button" class="btn btn-success btn-xs" onclick="ArticleTrashDelete()"><i class="fa fa-close"></i> Xóa</button>
            </div>
            <hr />
        	<div id="trashLinks" style="display: none;">
        	  <%--  <a id="btnTrashRestore" style="display: none"  href="javascript:void(0)" onclick="ArticleTrashRestore();" class="Header">[<%= Resources.Site.Recover%>]</a>
                <a id="btnTrashDelete" style="display: none" href="javascript:void(0)" onclick="ArticleTrashDelete();" class="Header">[<%= Resources.Site.DeleteItem %>]</a>--%>
        	</div>
        	    <telerik:radgrid  id="gvArticleTrash" runat="server" autogeneratecolumns="False" allowpaging="True" allowsorting="True" gridlines="None" AllowMultiRowSelection="True" Width="812px">    
                    <HeaderContextMenu EnableAutoScroll="True"></HeaderContextMenu>
                    <MasterTableView ClientDataKeyNames="Id" AllowMultiColumnSorting="True">
                        <Columns>
                             <telerik:GridClientSelectColumn>                        
                                <ItemStyle Width="20px" />
                            </telerik:GridClientSelectColumn> 
					        <telerik:GridBoundColumn HeaderText="<%$ Resources:Site,Name %>" DataField="Name">                        							
					        </telerik:GridBoundColumn>
					        <telerik:GridBoundColumn HeaderText="<%$ Resources:Site,Title %>" DataField="Title">                        							
					        </telerik:GridBoundColumn>
					        <telerik:GridBoundColumn HeaderText="<%$ Resources:Site,CreatedDate %>" DataField="CreatedDate" dataformatstring="{0:dd/MM/yyyy}">                        							
					        </telerik:GridBoundColumn>
					        <telerik:GridBoundColumn HeaderText="<%$ Resources:Site,ModifiedDate %>" DataField="ModifiedDate" dataformatstring="{0:dd/MM/yyyy}">                        							
					        </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>  
                    <ClientSettings EnableRowHoverStyle="true">                                          
                        <Selecting AllowRowSelect="True" />
                        <ClientEvents OnCommand="gvArticleTrash_Command" />                
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                    </ClientSettings>                  
                </telerik:radgrid>
        	<div>
        	</div>
        </div>
        <div class="bottombox_left">&nbsp;</div>
        <div style="width: 795px;" class="bottombox_center">&nbsp;</div>
        <div class="bottombox_right">&nbsp;</div>      
    </div>	
</div>
</asp:Content>
