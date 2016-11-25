<%@ Page EnableViewState="false" Language="C#" MasterPageFile="~/Systems/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="MenuMasterManage.aspx.cs" Inherits="PSCPortal.Systems.CMS.MenuMasterManage"
    Title="<%# Resources.Site.MenuMasterManage %>" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Systems/CMS/Scripts/MenuMasterManage.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function initialize() {
            oWnd = $find("<%= rwMenuMaster.ClientID %>");
            strWarning = "<%= Resources.Site.Warning %>";
            strInformation = "<%= Resources.Site.Information %>";
            grid = $find("<%=gvMenuMaster.ClientID%>");
            strMenuMasterNotChoose = "<%= Resources.Site.MenuMasterNotChoose %>";
            strMenuConfirmDelete = "<%= Resources.Site.MenuConfirmDelete %>";
        }                               
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindow ID="rwMenuMaster" runat="server" Modal="True" VisibleStatusbar="False" OnClientClose="RadWindowMenuMasterClose">
    </telerik:RadWindow>
    <div style="width:900px; float:left; padding-left:16px;">
	<div style="width:808px; float:left; margin-right:12px;">
    	<div class="left_box"></div>
        <div style="width: 788px;" class="box"><div class="title_box" align="left"><%= Resources.Site.MenuMasterManage %></div></div>
        <div class="right_box"></div>
        <div style="background-color:#edeeef; float:left; width:808px; padding-top:19px; padding-bottom:19px;">
            <div>
                <button type="button" class="btn btn-success btn-xs" onclick="MenuMasterNew()"><i class="fa fa-plus"></i> Thêm mới</button>
                <button type="button" class="btn btn-success btn-xs" onclick="MenuMasterEdit()"><i class="fa fa-edit"></i> Hiệu chỉnh</button>
                <button type="button" class="btn btn-success btn-xs" onclick="MenuMasterCopy()"><i class="fa fa-copy"></i> Sao chép MenuMaster</button>
                <button type="button" class="btn btn-success btn-xs" onclick="EditMenus()"><i class="fa fa-desktop"></i> Quản lý Menu</button>
                <button type="button" class="btn btn-success btn-xs" onclick="MenuMakeTopic()"><i class="fa fa-share-square-o"></i> Chuyển Menu thành CM</button>
                <button type="button" class="btn btn-success btn-xs" onclick="MenuMasterDelete()"><i class="fa fa-close"></i> Xóa</button>
            </div>
            <hr />
        	<div id="menuMasterLinks" style="display:none;">
        	   <%-- <a href="javascript:void(0)" id="A1" style=" visibility:hidden;"  class="Header">[a]</a>
        	    <a href="javascript:void(0)" id="btnNew" style="display:none" onclick="MenuMasterNew();" class="Header">[<%= Resources.Site.AddItem %>]</a>
                <a href="javascript:void(0)" id="btnEdit" style="display:none" onclick="MenuMasterEdit();" class="Header">[<%= Resources.Site.EditItem %>]</a>
                <a href="javascript:void(0)" id="btnCopy" style="display:none" onclick="MenuMasterCopy();" class="Header">[<%= Resources.Site.MenuMasterCopy %>]</a>
                <a class="Header" href="javascript:void(0)" id="btnDelete" style="display:none" onclick="MenuMasterDelete();">[<%= Resources.Site.DeleteItem %>]</a>
                <a class="Header" href="javascript:void(0)" id="btnEditMenu" style="display:none" onclick="EditMenus();">[<%=Resources.Site.MenuManage %>]</a>
                <a class="Header" href="javascript:void(0)" id="btnMakeTopic" style="display:none" onclick="MenuMakeTopic();">[<%=Resources.Site.MenuMakeTopic %>]</a>
                <a href="javascript:void(0)" id="btnPermission" style="display:none" onclick="MenuMasterSecurity();" class="Header">[<%= Resources.Site.GrantItem %>]</a>--%>
        	</div>
        	<div>
        	    <telerik:RadGrid  ID="gvMenuMaster" runat="server" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" GridLines="None" AllowMultiRowSelection="True">                               	        
                    <HeaderContextMenu EnableAutoScroll="True"></HeaderContextMenu>
        	        <MasterTableView ClientDataKeyNames="Id" AllowMultiColumnSorting="True">
                        <Columns>
                            <telerik:GridClientSelectColumn >
                               <HeaderStyle Width="5%" />
                                                           
                            </telerik:GridClientSelectColumn>
                            <telerik:GridBoundColumn HeaderText="<%$ Resources:Site,Name %>" DataField="Name">
                                <HeaderStyle Width="45%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="<%$ Resources:Site,Description %>" DataField="Description">
                                <HeaderStyle Width="50%" />
                               
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="True" />
                        <ClientEvents OnCommand="gvMenuMaster_Command" />
                        <ClientEvents OnRowSelected="row_select" />
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                    </ClientSettings>
                </telerik:RadGrid>
        	</div>
        </div>
        <div class="bottombox_left">&nbsp;</div>
        <div style="width: 788px;" class="bottombox_center">&nbsp;</div>
        <div class="bottombox_right">&nbsp;</div>      
    </div>	
</div>
</asp:Content>
