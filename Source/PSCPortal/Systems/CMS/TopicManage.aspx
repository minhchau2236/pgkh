<%@ Page EnableViewState="false" Language="C#" MasterPageFile="~/Systems/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="TopicManage.aspx.cs" Inherits="PSCPortal.Systems.CMS.TopicManage"
    Title="<%# Resources.Site.TopicManage %>" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Systems/CMS/Scripts/TopicManage.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function initialize() {
            oWnd = $find("<%= rwTopic.ClientID %>");
            tree = $find("<%= rtvTopic.ClientID %>");
            strTopicNotChoose = "<%= Resources.Site.TopicNotChoose %>";
            strWarning = "<%= Resources.Site.Warning %>";
            strTopicEditTemplateSuccess = "<%=Resources.Site.TopicEditTemplateSuccess %>";
            strInformation = "<%= Resources.Site.Information %>";
            strTopicConfirmDelete = "<%= Resources.Site.TopicConfirmDelete %>";
            strTopicDeleteSuccess = "<%= Resources.Site.TopicDeleteSuccess %>";
            strTopicCanNotMoveUp = "<%= Resources.Site.TopicCannotMoveUp %>";
            strTopicCannotMoveDown = "<%= Resources.Site.TopicCannotMoveDown %>"; 
        }
    </script>
   
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:radwindow id="rwTopic" runat="server" modal="True" visiblestatusbar="False" onclientclose="RadWindowTopicClose">
    </telerik:radwindow>
    <div style="width: 815px; float: left; padding-left: 16px; padding-top: 25px;">
        <div style="width: 815px; float: left; margin-right: 12px;">
            <div class="left_box"></div>
            <div class="box" style="width: 795px;">
                <div class="title_box" align="left">
                    <%= Resources.Site.TopicManage %>
                </div>
            </div>
            <div class="right_box"></div>
            <div style="background-color: #edeeef; float: left; width: 815px; padding-left: 10px; padding-top: 19px; padding-bottom: 19px;">
                <div id="topicLinks">
                    <%--<a id="btnTopicAdd" runat="server" href="javascript:void(0)"  onclick="TopicNew();" class="Header">[<%= Resources.Site.AddItem %>]</a>
                    <a id="btnTopicEdit"  href="javascript:void(0)"  onclick="TopicEdit();" class="Header">[<%= Resources.Site.EditItem %>]</a>                    
                    <a id="btnTopicCopy"  href="javascript:void(0)"  onclick="TopicCopy();" class="Header">[<%= Resources.Site.TopicCopy %>]</a>
                    <a id="btnTopicMakeMenu"  href="javascript:void(0)"  onclick="TopicMakeMenu();" class="Header">[<%= Resources.Site.TopicMakeMenu %>]</a>
                    <a id="btnTopicDelete" href="javascript:void(0)"  onclick="TopicDelete();" class="Header">[<%= Resources.Site.DeleteItem %>]</a>                    
                    <a id="btnTopicPermission"  href="javascript:void(0)"  onclick="TopicSecurity();" class="Header">[<%= Resources.Site.GrantItem %>]</a>
                    <a id="btnChangeRoot" href="javascript:void(0)"  onclick="TopicChangeRoot();" class="Header">[Chuyển lên cùng]</a>
                    <a id="btnTopicLoginEdit"  href="javascript:void(0)"  onclick="TopicLoginEdit();" class="Header">[Cập nhật đăng nhập]</a>--%>
                    <button type="button" class="btn btn-success btn-xs" onclick="TopicNew()"><i class="fa fa-plus"></i> Thêm mới</button>
                    <button type="button" class="btn btn-success btn-xs" onclick="TopicEdit()"><i class="fa fa-edit"></i> Hiệu chỉnh</button>
                    <button type="button" class="btn btn-success btn-xs" onclick="TopicChangeRoot()"><i class="fa fa-upload"></i> Chuyển lên cùng</button>
                    <button type="button" class="btn btn-success btn-xs" onclick="TopicLoginEdit()"><i class="fa fa-user"></i> Cập nhật đăng nhập</button>
                    <button type="button" class="btn btn-success btn-xs" onclick="TopicMoveUp()"><i class="fa fa-level-up"></i> Di chuyển lên</button>
                    <button type="button" class="btn btn-success btn-xs" onclick="TopicMoveDown()"><i class="fa fa-level-down"></i> Di chuyển xuống</button>
                    <hr />
                    <button type="button" class="btn btn-success btn-xs" onclick="TopicMakeMenu()"><i class="fa fa-calendar"></i> Chuyển chuyên mục thành menu</button>
                    <button type="button" class="btn btn-success btn-xs" onclick="TopicCopy()"><i class="fa fa-copy"></i> Sao chép Chuyên mục</button>
                    <button type="button" class="btn btn-success btn-xs" onclick="TopicSecurity()"><i class="fa fa-group"></i>Phân quyền</button>
                    <button type="button" class="btn btn-success btn-xs" onclick="TopicDelete()"><i class="fa fa-close"></i> Xóa</button>
                </div>
                <hr />
                <div>
                    <telerik:radtreeview id="rtvTopic" runat="server" datatextfield="Name" datavaluefield="Id" enabledraganddrop="False" onclientnodedropping="ChangeParent">
                    </telerik:radtreeview>
                </div>
            </div>
            <div class="bottombox_left">&nbsp;</div>
            <div style="width: 795px;" class="bottombox_center">&nbsp;</div>
            <div class="bottombox_right">&nbsp;</div>
        </div>
    </div>
</asp:Content>
