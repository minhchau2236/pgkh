<%@ Page Title="" Language="C#" MasterPageFile="~/Systems/MasterPage.Master" AutoEventWireup="true" CodeBehind="MailScheduleManager.aspx.cs" Inherits="PSCPortal.Systems.CMS.MailScheduleManager" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Systems/CMS/Scripts/MailScheduleManage.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function initialize() {
            oWnd = $find("<%= rwMailSchedule.ClientID %>");
            strWarning = "<%= Resources.Site.Warning %>";
            strInformation = "<%= Resources.Site.Information %>";
            grid = $find("<%=gvMailSchedule.ClientID%>");
            strMailScheduleNotChoose = "Bạn chưa chọn liên kết.";
            strMenuConfirmDelete = "Bạn có chắc muốn xóa link không?";
        }                               
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindow ID="rwMailSchedule" runat="server" Modal="True" VisibleStatusbar="False" OnClientClose="RadWindowMailScheduleClose">
    </telerik:RadWindow>
    <div style="width:815px; float:left; padding-left:16px; padding-top:25px;">    
	<div style="width:815px; float:left; margin-right:12px;">
    	<div class="left_box"></div>
        <div style="width: 795px;" class="box"><div class="title_box" align="left">Quản lý Mail</div></div>
        <div class="right_box"></div>
        <div style="background-color:#edeeef; float:left; width:815px; padding-top:19px; padding-bottom:19px;">
        	<div>
        	    <%--<a href="javascript:void(0)" id="A1" style=" display: none;"  class="Header">[a]</a>
        	    <a href="javascript:void(0)" id="btnNew" style="display:inline " onclick="MailScheduleNew();" class="Header">[<%= Resources.Site.AddItem %>]</a>
                <a href="javascript:void(0)" id="btnEdit" style="display:inline" onclick="MailScheduleEdit();" class="Header">[<%= Resources.Site.EditItem %>]</a>                
                <a class="Header" href="javascript:void(0)" id="btnDelete" style="display:inline" onclick="MailScheduleDelete();">[<%= Resources.Site.DeleteItem %>]</a>    --%>  
                
                 <button type="button" class="btn btn-success btn-xs" onclick="MailScheduleNew()"><i class="fa fa-plus"></i> Thêm mới</button>
                 <button type="button" class="btn btn-success btn-xs" onclick="MailScheduleEdit()"><i class="fa fa-edit"></i> Hiệu chỉnh</button>
                 <button type="button" class="btn btn-success btn-xs" onclick="MailScheduleDelete()"><i class="fa fa-close"></i> Xóa</button>
        	</div>
            <hr />
        	<div>
        	    <telerik:RadGrid  ID="gvMailSchedule" runat="server" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" GridLines="None" AllowMultiRowSelection="True">                               	        
                    <HeaderContextMenu EnableAutoScroll="True"></HeaderContextMenu>
        	        <MasterTableView ClientDataKeyNames="Id" AllowMultiColumnSorting="True">
                        <Columns>
                            <telerik:GridClientSelectColumn>
                                <ItemStyle Width="20px" />
                            </telerik:GridClientSelectColumn>
                            <telerik:GridBoundColumn HeaderText="<%$ Resources:Site,Name %>" DataField="Name">
                                <ItemStyle Width="120px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Mail" DataField="Mail">
                                <ItemStyle Width="120px" />
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="True" />
                        <ClientEvents OnCommand="gvMailSchedule_Command" />
                        <%--<ClientEvents OnRowSelected="row_select" />--%>
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                    </ClientSettings>
                </telerik:RadGrid>
        	</div>
        </div>
        <div class="bottombox_left">&nbsp;</div>
        <div style="width: 795px;" class="bottombox_center">&nbsp;</div>
        <div class="bottombox_right">&nbsp;</div>      
    </div>	
</div>
</asp:Content>
