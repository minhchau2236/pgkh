<%@ Page Language="C#" MasterPageFile="~/Systems/MasterPage.Master" AutoEventWireup="true" CodeBehind="VoteAnswerManage.aspx.cs" Inherits="PSCPortal.Systems.CMS.VoteAnswerManage" Title="Quản lý câu trả lời"  %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script language="javascript" type="text/javascript">
    var dialogMethod;
    function CallWebMethodSuccess(results, context, methodName) {
        switch (methodName) {
            case "GetVoteAnswerList":
                {
                    GetVoteAnswerListCallback(results, context, methodName);
                }
                break;
            case "GetVoteAnswerCount":
                {
                    GetVoteAnswerCountCallback(results, context, methodName);
                }
                break;
            case "VoteAnswerNew":
                {
                    VoteAnswerNewCallback(results, context, methodName);
                }
                break;
            case "VoteAnswerAdd":
                {
                    VoteAnswerAddCallback(results, context, methodName);
                }
                break;
            case "VoteAnswerEdit":
                {
                    VoteAnswerEditCallback(results, context, methodName);
                }
                break;
            case "VoteAnswerUpdate":
                {
                    VoteAnswerUpdateCallback(results, context, methodName);
                }
                break;
            case "VoteAnswerDelete":
                {
                    VoteAnswerDeleteCallback(results, context, methodName);
                }
                break;
            case "VoteAnswerReset":
                {
                    VoteAnswerResetCallback(results, context, methodName);
                }
                break;
            case "VoteAnswerMoveUp":
                {
                    VoteAnswerMoveUpCallback(results, context, methodName);
                }
            case "VoteAnswerMoveDown":
                {
                    VoteAnswerMoveDownCallback(results, context, methodName);
                }
        }
    }
    function GetVoteAnswerSelect() {
        var items = $find("<%=gvVoteAnswer.ClientID%>").get_masterTableView().get_selectedItems();

        var objList = new Array();
        for (var i = 0; i < items.length; i++) {
            Array.add(objList, items[i].get_dataItem().Id);
        }
        return objList;
    }
    function CallWebMethodFailed(results, context, methodName) {
        alert(results._message);
    }

    function GetVoteAnswerList() {
        var tableView = $find("<%=gvVoteAnswer.ClientID%>").get_masterTableView();
        var sortExpressions = tableView.get_sortExpressions();
        PageMethods.GetVoteAnswerList(0, tableView.get_pageSize(), sortExpressions.toString(), CallWebMethodSuccess);
        PageMethods.GetVoteAnswerCount(CallWebMethodSuccess);
    }
    function GetVoteAnswerListCallback(results, context, methodName) {
        var grid = $find("<%=gvVoteAnswer.ClientID%>");
        var tableView = grid.get_masterTableView();
        tableView.set_dataSource(Sys.Serialization.JavaScriptSerializer.deserialize(results));
        tableView.dataBind();
    }

    function GetVoteAnswerCountCallback(results, context, methodName) {
        var tableView = $find("<%=gvVoteAnswer.ClientID%>").get_masterTableView();
        tableView.set_virtualItemCount(results);
    }

    function VoteAnswerNew() {
        PageMethods.VoteAnswerNew(CallWebMethodSuccess, CallWebMethodFailed);
    }
    function VoteAnswerNewCallback(results, context, methodName) {
        dialogMethod = "VoteAnswerNew";
        var oWnd = $find("<%= rwVoteAnswer.ClientID %>");
        oWnd.setSize(600, 280);
        oWnd.setUrl("VoteAnswerDetail.aspx");
        oWnd.show();
    }
    function VoteAnswerAddCallback(result, context, methodName) {
        GetVoteAnswerList();
    }

    function VoteAnswerEdit() {
        var items = $find("<%=gvVoteAnswer.ClientID%>").get_masterTableView().get_selectedItems();
        if (items.length == 0) {
            radalert("Câu trả lời chưa được chọn", 250, 100, "<%= Resources.Site.Warning %>");
            return;
        }
        var obj = items[0].get_dataItem();
        PageMethods.VoteAnswerEdit(obj.Id, CallWebMethodSuccess, CallWebMethodFailed);
    }
    function VoteAnswerEditCallback(results, context, methodName) {
        dialogMethod = "VoteAnswerEdit";
        var oWnd = $find("<%= rwVoteAnswer.ClientID %>");
        oWnd.setSize(600, 280);
        oWnd.setUrl("VoteAnswerDetail.aspx");
        oWnd.show();
    }

    function VoteAnswerUpdateCallback(results, context, methodName) {
        GetVoteAnswerList();
    }

    function VoteAnswerDelete() {
        var objList = GetVoteAnswerSelect();
        if (objList.length == 0) {
            radalert("Câu trả lời chưa được chọn", 250, 100, "<%= Resources.Site.Warning %>");
            return;
        }
        radconfirm("Bạn có chắc muốn xóa không ?", VoteAnswerDeleteConfirm, 250, 100, null, "<%= Resources.Site.Warning %>");
    }
    function VoteAnswerDeleteConfirm(args) {
        if (!args)
            return;
        var objList = GetVoteAnswerSelect();
        PageMethods.VoteAnswerDelete(objList, CallWebMethodSuccess, CallWebMethodFailed);
    }
    function VoteAnswerDeleteCallback(results, context, methodName) {
        GetVoteAnswerList();
        $find("<%=gvVoteAnswer.ClientID%>").get_masterTableView().clearSelectedItems();
    }
    /**--**--**--vote reset--**--**--**/
    function VoteAnswerReset() {
        var objList = GetVoteAnswerSelect();
        if (objList.length == 0) {
            radalert("Câu trả lời chưa được chọn", 250, 100, "<%= Resources.Site.Warning %>");
            return;
        }
        radconfirm("Bạn có chắc muốn reset không ?", VoteAnswerResetConfirm, 250, 100, null, "<%= Resources.Site.Warning %>");
    }
    function VoteAnswerResetConfirm(args) {
        if (!args)
            return;
        var objList = GetVoteAnswerSelect();
        PageMethods.VoteAnswerReset(objList, CallWebMethodSuccess, CallWebMethodFailed);
    }
    function VoteAnswerResetCallback(results, context, methodName) {
        GetVoteAnswerList();
        $find("<%=gvVoteAnswer.ClientID%>").get_masterTableView().clearSelectedItems();
    }
    /**--**--**--end reset--**--**--**/
    function VoteAnswerMoveUp() {
        var items = $find("<%=gvVoteAnswer.ClientID%>").get_masterTableView().get_selectedItems();
        if (items.length == 0) {
            radalert("Câu trả lời chưa được chọn", 250, 100, "<%= Resources.Site.Warning %>");
            return;
        }
        if (items[0]._itemIndexHierarchical == 0) {
            radalert("Không thể di chuyển lên trên", 250, 100, "<%= Resources.Site.Warning %>");
            return;
        }
        var obj = items[0].get_dataItem();
        PageMethods.VoteAnswerMoveUp(obj.Id, CallWebMethodSuccess, CallWebMethodFailed);
    }
    function VoteAnswerMoveUpCallback() {
        GetVoteAnswerList();
        $find("<%=gvVoteAnswer.ClientID%>").get_masterTableView().clearSelectedItems();
    }
    function VoteAnswerMoveDown() {
        var items = $find("<%=gvVoteAnswer.ClientID%>").get_masterTableView().get_selectedItems();
        if (items.length == 0) {
            radalert("Câu trả lời chưa được chọn", 250, 100, "<%= Resources.Site.Warning %>");
            return;
        }

        if (items[0]._itemIndexHierarchical == ($find("<%=gvVoteAnswer.ClientID%>").get_masterTableView()._dataSource.length - 1)) {
            radalert("Không thể di chuyển xuống dưới", 250, 100, "<%= Resources.Site.Warning %>");
            return;
        }
        var obj = items[0].get_dataItem();
        PageMethods.VoteAnswerMoveDown(obj.Id, CallWebMethodSuccess, CallWebMethodFailed);
    }
    function VoteAnswerMoveDownCallback() {
        GetVoteAnswerList();
        $find("<%=gvVoteAnswer.ClientID%>").get_masterTableView().clearSelectedItems();
    }
    function pageLoad() {
        GetVoteAnswerList();
    }
    function gvVoteAnswer_Command(sender, args) {
        args.set_cancel(true);
        sender.get_masterTableView().clearSelectedItems();
        var currentPageIndex = sender.get_masterTableView().get_currentPageIndex();
        var pageSize = sender.get_masterTableView().get_pageSize();
        var sortExpressions = sender.get_masterTableView().get_sortExpressions();

        PageMethods.GetVoteAnswerList(currentPageIndex * pageSize, pageSize, sortExpressions.toString(), CallWebMethodSuccess);
        PageMethods.GetVoteAnswerCount(CallWebMethodSuccess);
    }
    function RadWindowVoteAnswerClose(sender, args) {
        if (!args.get_argument())
            return;
        switch (dialogMethod) {
            case "VoteAnswerNew":
                {
                    PageMethods.VoteAnswerAdd(CallWebMethodSuccess, CallWebMethodFailed);
                }
                break;
            case "VoteAnswerEdit":
                {
                    PageMethods.VoteAnswerUpdate(CallWebMethodSuccess, CallWebMethodFailed);
                }
                break;
        }
    }
    function DisplayVoteQuestion() {
        window.location.assign("http://" + window.location.host + "/Systems/CMS/VoteQuestionManage.aspx");
    }     
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindow ID="rwVoteAnswer" runat="server" Modal="True" VisibleStatusbar="False" OnClientClose="RadWindowVoteAnswerClose">
    </telerik:RadWindow>
    <div style="width:718px; float:left; padding-left:16px; padding-top:25px;">
        <div style="width:608px; float:left; margin-right:12px;">
            <div class="left_box"></div>
            <div style="width: 588px;" class="box"><div class="title_box" align="left">Quản lý câu trả lời</div></div>
            <div class="right_box"></div>
            <div style="background-color:#edeeef; float:left; width:608px; padding-top:19px; padding-bottom:19px;">
                <div>
                    <a href="javascript:void(0)" onclick="VoteAnswerNew();" class="Header">[<%= Resources.Site.AddItem %>]</a>
                    <a href="javascript:void(0)" onclick="VoteAnswerEdit();" class="Header">[<%= Resources.Site.EditItem %>]</a>      
                    <a href="javascript:void(0)" onclick="VoteAnswerDelete();" class="Header">[<%= Resources.Site.DeleteItem %>]</a>
                    <a href="javascript:void(0)" onclick="VoteAnswerMoveUp();" class="Header">[<%= Resources.Site.MoveUp %>]</a>
                    <a href="javascript:void(0)" onclick="VoteAnswerMoveDown();" class="Header">[<%= Resources.Site.MoveDown %>]</a>
                    <%--<a href="javascript:void(0)" onclick="DisplayVoteQuestion();" class="Header">[Quản lý câu hỏi]</a>--%>
                </div>
                <div>
                    <telerik:radgrid id="gvVoteAnswer" runat="server"
            autogeneratecolumns="False" allowpaging="True"
            allowsorting="True" gridlines="None"
            AllowMultiRowSelection="True">    
            <MasterTableView ClientDataKeyNames="Id" AllowMultiColumnSorting="True">
                <Columns>
                     <telerik:GridClientSelectColumn>                        
                        <ItemStyle Width="20px" />
                    </telerik:GridClientSelectColumn> 
					
					<telerik:GridBoundColumn HeaderText="Tên" DataField="Name" >                        							
					</telerik:GridBoundColumn>
					<telerik:GridBoundColumn HeaderText="Số lượng" DataField="Number" >                        							
					</telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>  
            <ClientSettings EnableRowHoverStyle="true">                                          
                <Selecting AllowRowSelect="True" />
                <ClientEvents OnCommand="gvVoteAnswer_Command" />                
            </ClientSettings>                  
        </telerik:radgrid>
                </div>
            </div>
            <div class="bottombox_left">&nbsp;</div>
            <div style="width: 588px;" class="bottombox_center">&nbsp;</div>
            <div class="bottombox_right">&nbsp;</div>   
        </div>
    </div>
</asp:Content>
