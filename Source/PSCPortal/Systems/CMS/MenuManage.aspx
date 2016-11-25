<%@ Page EnableViewState="false" Language="C#" MasterPageFile="~/Systems/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="MenuManage.aspx.cs" Inherits="PSCPortal.Systems.CMS.MenuManage" Title="<%# Resources.Site.MenuManage%>" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Systems/CMS/Scripts/MenuManage.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function initialize() {
            strWarning = "<%= Resources.Site.Warning %>";
            strMenuNotChoose = "<%= Resources.Site.MenuNotChoose %>";
            tree = $find("<%= rtvMenu.ClientID %>");
            oWnd = $find("<%= rwMenu.ClientID %>");
            strMenuConfirmDelete = "<%= Resources.Site.MenuConfirmDelete %>";
            strMenuCanNotMoveUp = "<%= Resources.Site.MenuCanNotMoveUp %>";
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindow ID="rwMenu" runat="server" Modal="True" VisibleStatusbar="False" OnClientClose="RadWindowMenuClose">
    </telerik:RadWindow>
    <div style="width: 815px; float: left; padding-left: 16px; padding-top: 25px;">
        <div style="width: 815px; float: left; margin-right: 12px;">
            <div class="left_box"></div>
            <div style="width: 795px;" class="box">
                <div class="title_box" align="left">Menu</div>
            </div>
            <div class="right_box"></div>
            <div style="background-color: #edeeef; float: left; width: 815px; padding-top: 19px; padding-bottom: 19px;">
                <div>
                    <%--  <a href="javascript:void(0)" onclick="MenuNew();" class="Header">[<%= Resources.Site.AddItem %>]</a>
                <a href="javascript:void(0)" onclick="MenuEdit();" class="Header">[<%= Resources.Site.EditItem %>]</a>
                <a href="javascript:void(0)" onclick="MenuDelete();" class="Header">[<%= Resources.Site.DeleteItem %>]</a>
                <a href="javascript:void(0)" onclick="MenuMoveUp();" class="Header">[<%= Resources.Site.MoveUp %>]</a>
                <a href="javascript:void(0)" onclick="MenuMoveDown();" class="Header">[<%= Resources.Site.MoveDown %>]</a>
                <a href="javascript:void(0)" onclick="DisplayMenuMaster();" class="Header">[<%= Resources.Site.ReturnToMenuMaster %>]</a>--%>

                    <button type="button" class="btn btn-success btn-xs" onclick="MenuNew()"><i class="fa fa-plus"></i><%= Resources.Site.AddItem %></button>
                    <button type="button" class="btn btn-success btn-xs" onclick="MenuEdit()"><i class="fa fa-edit"></i><%= Resources.Site.EditItem %></button>
                    <button type="button" class="btn btn-success btn-xs" onclick="MenuMoveUp()"><i class="fa fa-level-up"></i><%= Resources.Site.MoveUp %></button>
                    <button type="button" class="btn btn-success btn-xs" onclick="MenuMoveDown()"><i class="fa fa-level-down"></i><%= Resources.Site.MoveDown %></button>
                    <button type="button" class="btn btn-success btn-xs" onclick="MenuDelete()"><i class="fa fa-close"></i><%= Resources.Site.DeleteItem %></button>
                    <button type="button" class="btn btn-success btn-xs" onclick="DisplayMenuMaster()"><i class="fa fa-reply"></i><%= Resources.Site.ReturnToMenuMaster %></button>

                </div>
                <hr />
                <div style="min-height: 230px; overflow: auto;">
                    <telerik:RadTreeView ID="rtvMenu" runat="server" DataTextField="Name" DataValueField="Id" EnableDragAndDrop="True" OnClientNodeDropping="ChangeParent">
                    </telerik:RadTreeView>
                </div>
            </div>
            <div class="bottombox_left">&nbsp;</div>
            <div style="width: 795px;" class="bottombox_center">&nbsp;</div>
            <div class="bottombox_right">&nbsp;</div>
        </div>
    </div>
</asp:Content>
