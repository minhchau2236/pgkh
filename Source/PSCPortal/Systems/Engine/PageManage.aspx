<%@ Page EnableViewState="false" Title="<%# Resources.Site.PageManage %>" Language="C#" MasterPageFile="~/Systems/MasterPage.Master" AutoEventWireup="true" CodeBehind="PageManage.aspx.cs" Inherits="PSCPortal.Systems.Engine.PageManage" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/Common.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        var dialogMethod;
        function CallWebMethodSuccess(results, context, methodName) {
            switch (methodName) {
                case "GetPageList":
                    {
                        GetPageListCallback(results, context, methodName);
                    }
                    break;
                case "GetPageCount":
                    {
                        GetPageCountCallback(results, context, methodName);
                    }
                    break;
                case "PageNew":
                    {
                        PageNewCallback(results, context, methodName);
                    }
                    break;
                case "PageAdd":
                    {
                        PageAddCallback(results, context, methodName);
                    }
                    break;
                case "PageEdit":
                    {
                        PageEditCallback(results, context, methodName);
                    }
                    break;
                case "PageUpdate":
                    {
                        PageUpdateCallback(results, context, methodName);
                    }
                    break;
                case "PageDelete":
                    {
                        PageDeleteCallback(results, context, methodName);
                    }
                    break;
                case "PageCopy":
                    {
                        PageCopyCallback(results, context, methodName);
                    }
                    break;
                case "PageCopyDo":
                    {
                        PageCopyDoCallback(results, context, methodName);
                    }
                    break;
                case "PageSecurity":
                    {
                        PageSecurityCallback(results, context, methodName);
                    }
                    break;
                case "GetPagePermission":
                    {
                        PagePermissionCallback(results, context, methodName);
                    }
                    break;
            }
        }
        function PageSecurityCallback(results, context, methodName) {
            dialogMethod = "PageSecurity";
            var oWnd = $find("<%= rwPage.ClientID %>");
            oWnd.setSize(380, 300);
            oWnd.setUrl("PageSecurity.aspx");
            oWnd.show();
        }
        function GetPageSelect() {
            var items = $find("<%=gvPage.ClientID%>").get_masterTableView().get_selectedItems();

            var objList = new Array();
            for (var i = 0; i < items.length; i++) {
                Array.add(objList, items[i].get_dataItem().Id);
            }
            return objList;
        }
        function CallWebMethodFailed(results, context, methodName) {
            //  alert(results._message);
        }

        function GetPageList() {
            var tableView = $find("<%=gvPage.ClientID%>").get_masterTableView();
            var sortExpressions = tableView.get_sortExpressions();
            PageMethods.GetPageList(0, tableView.get_pageSize(), sortExpressions.toString(), CallWebMethodSuccess);
            PageMethods.GetPageCount(CallWebMethodSuccess);
        }
        function GetPageListCallback(results, context, methodName) {
            var grid = $find("<%=gvPage.ClientID%>");
            var tableView = grid.get_masterTableView();
            tableView.set_dataSource(Sys.Serialization.JavaScriptSerializer.deserialize(results));
            tableView.dataBind();
        }

        function GetPageCountCallback(results, context, methodName) {
            var tableView = $find("<%=gvPage.ClientID%>").get_masterTableView();
            tableView.set_virtualItemCount(results);
        }

        function PageNew() {
            PageMethods.PageNew(CallWebMethodSuccess, CallWebMethodFailed);
        }
        function PageNewCallback(results, context, methodName) {
            dialogMethod = "PageNew";
            var oWnd = $find("<%= rwPage.ClientID %>");
            oWnd.setSize(680, 615);
            oWnd.setUrl("PageDetail.aspx");
            oWnd.show();
        }
        function PageAddCallback(result, context, methodName) {
            GetPageList();
        }

        function PageEdit() {
            var items = $find("<%=gvPage.ClientID%>").get_masterTableView().get_selectedItems();
            if (items.length == 0) {
                radalert("<%= Resources.Site.PageNotChoose %>", 250, 100, "<%= Resources.Site.Warning %>");
                return;
            }
            var obj = items[0].get_dataItem();
            PageMethods.PageEdit(obj.Id, CallWebMethodSuccess, CallWebMethodFailed);
        }
        function PageEditCallback(results, context, methodName) {
            dialogMethod = "PageEdit";
            var oWnd = $find("<%= rwPage.ClientID %>");
            oWnd.setSize(680, 615);
            oWnd.setUrl("PageDetail.aspx");
            oWnd.show();
        }

        function PageUpdateCallback(results, context, methodName) {
            GetPageList();
        }
        function PageCopy() {
            var items = $find("<%=gvPage.ClientID%>").get_masterTableView().get_selectedItems();
            if (items.length == 0) {
                radalert("<%= Resources.Site.PageNotChoose %>", 250, 100, "<%= Resources.Site.Warning %>");
                return;
            }
            var obj = items[0].get_dataItem();
            PageMethods.PageCopy(obj.Id,CallWebMethodSuccess, CallWebMethodFailed);
        }
        function PageCopyCallback(results, context, methodName) {
            dialogMethod = "PageCopy";
            var oWnd = $find("<%= rwPage.ClientID %>");
            oWnd.setSize(680, 550);
            oWnd.setUrl("PageDetail.aspx");
            oWnd.show();
        }
        function PageCopyDoCallback(results, context, methodName) {
            GetPageList();
        }
        function PageDelete() {
            var objList = GetPageSelect();
            if (objList.length == 0) {
                radalert("<%= Resources.Site.PageNotChoose %>", 250, 100, "<%= Resources.Site.Warning %>");
                return;
            }
            radconfirm("<%= Resources.Site.PageConfirmDelete %>", PageDeleteConfirm, 250, 100, null, "<%= Resources.Site.Warning %>");
        }
        function PageDeleteConfirm(arg) {
            if (!arg)
                return;
            var objList = GetPageSelect();
            PageMethods.PageDelete(objList, CallWebMethodSuccess, CallWebMethodFailed);
        }
        function PageDeleteCallback(results, context, methodName) {
            GetPageList();
            $find("<%=gvPage.ClientID%>").get_masterTableView().clearSelectedItems();
        }
        var btnPageNew;
        var btnPageEdit;
        var btnPageCopy;
        var btnChangeStruct;
        var btnPageDelete;
        var btnPagePermission;
        function CreateElementPageLinks() {
            btnPageNew = document.createElement("a");
            btnPageNew.setAttribute("href", "javascript:void(0)");
            btnPageNew.setAttribute("onClick", "PageNew()");
            btnPageNew.setAttribute("class", "Header");
            var addName = document.createTextNode("[Thêm mới]");
            btnPageNew.appendChild(addName);

            btnPageEdit = document.createElement("a");
            btnPageEdit.setAttribute("href", "javascript:void(0)");
            btnPageEdit.setAttribute("onClick", "PageEdit()");
            btnPageEdit.setAttribute("class", "Header");
            var editName = document.createTextNode("[Hiệu chỉnh]");
            btnPageEdit.appendChild(editName);

            btnPageCopy = document.createElement("a");
            btnPageCopy.setAttribute("href", "javascript:void(0)");
            btnPageCopy.setAttribute("onClick", "PageCopy()");
            btnPageCopy.setAttribute("class", "Header");
            var copyName = document.createTextNode("[Sao chép Page]");
            btnPageCopy.appendChild(copyName);

            btnChangeStruct = document.createElement("a");
            btnChangeStruct.setAttribute("href", "javascript:void(0)");
            btnChangeStruct.setAttribute("onClick", "PageEditStructure()");
            btnChangeStruct.setAttribute("class", "Header");
            var changeStructName = document.createTextNode("[Hiệu chỉnh cấu trúc]");
            btnChangeStruct.appendChild(changeStructName);

            btnPageDelete = document.createElement("a");
            btnPageDelete.setAttribute("href", "javascript:void(0)");
            btnPageDelete.setAttribute("onClick", "PageDelete()");
            btnPageDelete.setAttribute("class", "Header");
            var deleteName = document.createTextNode("[Xóa]");
            btnPageDelete.appendChild(deleteName);

            //btnPagePermission = document.createElement("a");
            //btnPagePermission.setAttribute("href", "javascript:void(0)");
            //btnPagePermission.setAttribute("onClick", "PageSecurity()");
            //btnPagePermission.setAttribute("class", "Header");
            //var permissionName = document.createTextNode("[Phân quyền]");
            //btnPagePermission.appendChild(permissionName);
        }

        function pageLoad() {
            CreateElementPageLinks();
            GetPageList();
            PagePermission();
        }
        var Page_Add = 65;
        var Page_Edit = 66;
        var Page_Copy = 67;
        var Page_ChangeStruct = 68;
        var Page_Delete = 69;
        function PagePermission() {
            PageMethods.GetPagePermission([Page_Add, Page_Edit, Page_Copy, Page_ChangeStruct, Page_Delete], CallWebMethodSuccess, CallWebMethodFailed);

        }
        function PagePermissionCallback(results, context, methodName) {
            var arrPermission = Sys.Serialization.JavaScriptSerializer.deserialize(results);
            if (arrPermission[Page_Add]) {
                document.getElementById("pageLinks").appendChild(btnPageNew);
            }
            if (arrPermission[Page_Edit]) {
                document.getElementById("pageLinks").appendChild(btnPageEdit);
            }
            if (arrPermission[Page_Copy]) {
                document.getElementById("pageLinks").appendChild(btnPageCopy);
            }
            if (arrPermission[Page_ChangeStruct]) {
                document.getElementById("pageLinks").appendChild(btnChangeStruct);
            }
            if (arrPermission[Page_Delete]) {
                document.getElementById("pageLinks").appendChild(btnPageDelete);
            }
        }
        function row_select(sender, args) {
          //  var items = $find("<=gvPage.ClientID%>").get_masterTableView().get_selectedItems();
          //  var obj = items[0].get_dataItem();
          //  PageMethods.GetPagePermission(obj.Id, CallWebMethodSuccess, CallWebMethodFailed);
        }
        function gvPage_Command(sender, args) {
            args.set_cancel(true);
            sender.get_masterTableView().clearSelectedItems();
            var currentPageIndex = sender.get_masterTableView().get_currentPageIndex();
            var pageSize = sender.get_masterTableView().get_pageSize();
            var sortExpressions = sender.get_masterTableView().get_sortExpressions();

            PageMethods.GetPageList(currentPageIndex * pageSize, pageSize, sortExpressions.toString(), CallWebMethodSuccess);
            PageMethods.GetPageCount(CallWebMethodSuccess);
        }
        function PageEditStructure() {
            var items = $find("<%=gvPage.ClientID%>").get_masterTableView().get_selectedItems();
            if (items.length == 0) {
                radalert("<%= Resources.Site.PageNotChoose %>", 250, 100, "<%= Resources.Site.Warning %>");
                return;
            }
            var obj = items[0].get_dataItem();
            Redirect(String.format("~/PageEditStructure.aspx?PageId={0}", obj.Id));
        }
        function RadWindowPageClose(sender, args) {
            if (!args.get_argument())
                return;
            switch (dialogMethod) {
                case "PageNew":
                    {
                        PageMethods.PageAdd(CallWebMethodSuccess, CallWebMethodFailed);
                    }
                    break;
                case "PageEdit":
                    {
                        PageMethods.PageUpdate(CallWebMethodSuccess, CallWebMethodFailed);
                    }
                    break;
                case "PageCopy":
                    {
                        var items = $find("<%=gvPage.ClientID%>").get_masterTableView().get_selectedItems();
                        var obj = items[0].get_dataItem();
                        PageMethods.PageCopyDo(obj.Id, CallWebMethodSuccess, CallWebMethodFailed);
                    }
                    break;
            }
        }
        function PageSecurity() {
            var items = $find("<%=gvPage.ClientID%>").get_masterTableView().get_selectedItems();
            if (items.length == 0) {
                radalert("<%= Resources.Site.PageNotChoose %>", 250, 100, "<%= Resources.Site.Warning %>");
                return;
            }
            var obj = items[0].get_dataItem();

            PageMethods.PageSecurity(obj.Id, CallWebMethodSuccess, CallWebMethodFailed);
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:radwindow id="rwPage" runat="server" modal="True" visiblestatusbar="False" onclientclose="RadWindowPageClose">
    </telerik:radwindow>
    <div style="width: 815px; float: left; padding-left: 16px; padding-top: 25px;">
        <div style="width: 815px; float: left; margin-right: 12px;">
            <div class="left_box"></div>
            <div style="width: 795px;" class="box">
                <div class="title_box" align="left"><%= Resources.Site.PageManage %></div>
            </div>
            <div class="right_box"></div>
            <div style="background-color: #edeeef; float: left; width: 815px; padding-top: 19px; padding-bottom: 19px;">
                <div>
                    <button type="button" class="btn btn-success btn-xs" onclick="PageNew()"><i class="fa fa-plus"></i> Thêm mới</button>
                    <button type="button" class="btn btn-success btn-xs" onclick="PageEdit()"><i class="fa fa-pencil"></i> Hiệu chỉnh</button>
                    <button type="button" class="btn btn-success btn-xs" onclick="PageCopy()"><i class="fa fa-copy"></i> Sao chép Page</button>
                    <button type="button" class="btn btn-success btn-xs" onclick="PageEditStructure()"><i class="fa fa-edit"></i> Hiệu chỉnh cấu trúc</button>
                    <button type="button" class="btn btn-success btn-xs" onclick="PageDelete()"><i class="fa fa-close"></i> Xóa</button>
                </div>
                <hr />
                <div id="pageLinks" style="display: none;">
                    <%-- <a href="javascript:void(0)" id="A1" style="  visibility:hidden;" class="Header" > [a]</a>
                    <a href="javascript:void(0)" id="btnPageNew" style=" display:none;" class="Header" onclick="PageNew();">[<%= Resources.Site.AddItem %>]</a>
                    <a href="javascript:void(0)" id="btnPageEdit" style=" display:none;" class="Header" onclick="PageEdit();">[<%= Resources.Site.EditItem %>]</a>    
                    <a href="javascript:void(0)" id="btnPageCopy" style=" display:none;" class="Header" onclick="PageCopy();">[<%= Resources.Site.PageCopy %>]</a> 
                    <a href="javascript:void(0)" id="btnChangeStruct" style=" display:none;" class="Header" onclick="PageEditStructure();">[<%= Resources.Site.PageChangeStructure%>]</a>  
                    <a href="javascript:void(0)" id="btnPageDelete" style=" display:none;" class="Header" onclick="PageDelete();">[<%= Resources.Site.DeleteItem %>]</a>   
                    <a href="javascript:void(0)" id="btnPagePermission" style=" display:none;" onclick="PageSecurity();" class="Header">[<%= Resources.Site.GrantItem %>]</a>--%>
                </div>
                <div>
                    <telerik:radgrid  Width="100%" id="gvPage" runat="server" autogeneratecolumns="False" allowpaging="True" allowsorting="True" gridlines="None" allowmultirowselection="True">    
                        <HeaderContextMenu EnableAutoScroll="True"></HeaderContextMenu>
                        <MasterTableView ClientDataKeyNames="Id" AllowMultiColumnSorting="True">
                            <Columns>
                                 <telerik:GridClientSelectColumn>                        
                                   <ItemStyle Width="5px" />	
                                </telerik:GridClientSelectColumn> 			
					            <telerik:GridBoundColumn HeaderText="<%$ Resources:Site,Name %>" DataField="Name" >                        						
					            </telerik:GridBoundColumn>
					            <telerik:GridBoundColumn HeaderText="<%$ Resources:Site,Title %>" DataField="Title" >                        						
					            </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>  
                        <ClientSettings EnableRowHoverStyle="true">                                          
                            <Selecting AllowRowSelect="True" />
                            <ClientEvents OnCommand="gvPage_Command" /> 
                            <ClientEvents OnRowSelected="row_select" />               
                            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                        </ClientSettings>                  
                    </telerik:radgrid>
                </div>
            </div>
            <div class="bottombox_left">&nbsp;</div>
            <div style="width: 795px;" class="bottombox_center">&nbsp;</div>
            <div class="bottombox_right">&nbsp;</div>
        </div>
    </div>
</asp:Content>
