<%@ Page Language="C#" MasterPageFile="~/Systems/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="VoteQuestionManage.aspx.cs" Inherits="PSCPortal.Systems.CMS.VoteQuestionManage"
    Title="Quản lý câu hỏi" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">
        var dialogMethod;
        function CallWebMethodSuccess(results, context, methodName) {
            switch (methodName) {
                case "GetVoteQuestionList":
                    {
                        GetVoteQuestionListCallback(results, context, methodName);
                    }
                    break;
                case "GetVoteQuestionCount":
                    {
                        GetVoteQuestionCountCallback(results, context, methodName);
                    }
                    break;
                case "VoteQuestionNew":
                    {
                        VoteQuestionNewCallback(results, context, methodName);
                    }
                    break;
                case "VoteQuestionAdd":
                    {
                        VoteQuestionAddCallback(results, context, methodName);
                    }
                    break;
                case "VoteQuestionEdit":
                    {
                        VoteQuestionEditCallback(results, context, methodName);
                    }
                    break;
                case "VoteQuestionUpdate":
                    {
                        VoteQuestionUpdateCallback(results, context, methodName);
                    }
                    break;
                case "VoteQuestionDelete":
                    {
                        VoteQuestionDeleteCallback(results, context, methodName);
                    }
                    break;
                case "VoteQuestionEditAnswer":
                    {
                        VoteQuestionEditAnswerCallback(results, context, methodName);
                    }
                    break;
                case "GetPermission":
                    {
                        SecurityUICallback(results, context, methodName);
                    }
                    break;
            }
        }
        function GetVoteQuestionSelect() {
            var items = $find("<%=gvVoteQuestion.ClientID%>").get_masterTableView().get_selectedItems();

            var objList = new Array();
            for (var i = 0; i < items.length; i++) {
                Array.add(objList, items[i].get_dataItem().Id);
            }
            return objList;
        }
        function CallWebMethodFailed(results, context, methodName) {
            alert(results._message);
        }

        function GetVoteQuestionList() {
            var tableView = $find("<%=gvVoteQuestion.ClientID%>").get_masterTableView();
            var sortExpressions = tableView.get_sortExpressions();
            PageMethods.GetVoteQuestionList(0, tableView.get_pageSize(), sortExpressions.toString(), CallWebMethodSuccess);
            PageMethods.GetVoteQuestionCount(CallWebMethodSuccess);
        }
        function GetVoteQuestionListCallback(results, context, methodName) {
            var grid = $find("<%=gvVoteQuestion.ClientID%>");
            var tableView = grid.get_masterTableView();
            tableView.set_dataSource(Sys.Serialization.JavaScriptSerializer.deserialize(results));
            tableView.dataBind();
        }

        function GetVoteQuestionCountCallback(results, context, methodName) {
            var tableView = $find("<%=gvVoteQuestion.ClientID%>").get_masterTableView();
            tableView.set_virtualItemCount(results);
        }

        function VoteQuestionNew() {
            PageMethods.VoteQuestionNew(CallWebMethodSuccess, CallWebMethodFailed);
        }
        function VoteQuestionNewCallback(results, context, methodName) {
            dialogMethod = "VoteQuestionNew";
            var oWnd = $find("<%= rwVoteQuestion.ClientID %>");
            oWnd.setSize(600, 280);
            oWnd.setUrl("VoteQuestionDetail.aspx");
            oWnd.show();
        }
        function VoteQuestionAddCallback(result, context, methodName) {
            GetVoteQuestionList();
        }

        function VoteQuestionEdit() {
            var items = $find("<%=gvVoteQuestion.ClientID%>").get_masterTableView().get_selectedItems();
            if (items.length == 0) {
                radalert("Câu hỏi chưa được chọn.", 250, 100, "<%= Resources.Site.Warning %>");
                return;
            }
            var obj = items[0].get_dataItem();
            PageMethods.VoteQuestionEdit(obj.Id, CallWebMethodSuccess, CallWebMethodFailed);
        }
        function VoteQuestionEditCallback(results, context, methodName) {
            dialogMethod = "VoteQuestionEdit";
            var oWnd = $find("<%= rwVoteQuestion.ClientID %>");
            oWnd.setSize(600, 280);
            oWnd.setUrl("VoteQuestionDetail.aspx");
            oWnd.show();
        }

        function VoteQuestionUpdateCallback(results, context, methodName) {
            GetVoteQuestionList();            
        }

        function VoteQuestionDelete() {
            var objList = GetVoteQuestionSelect();
            if (objList.length == 0) {
                radalert("Câu hỏi chưa được chọn", 250, 100, "<%= Resources.Site.Warning %>");
                return;
            }
            radconfirm("Bạn có chắc muốn xóa không ?", VoteQuestionDeleteConfirm, 250, 100, null, "<%= Resources.Site.Warning %>");
        }
        function VoteQuestionDeleteConfirm(args) {
            if (!args)
                return;
            var objList = GetVoteQuestionSelect();
            PageMethods.VoteQuestionDelete(objList, CallWebMethodSuccess, CallWebMethodFailed);
        }
        function VoteQuestionDeleteCallback(results, context, methodName) {
            GetVoteQuestionList();
            $find("<%=gvVoteQuestion.ClientID%>").get_masterTableView().clearSelectedItems();
        }
        function VoteQuestionEditAnswer() {
            var items = $find("<%=gvVoteQuestion.ClientID%>").get_masterTableView().get_selectedItems();
            if (items.length == 0) {
                radalert("Câu hỏi chưa được chọn.", 250, 100, "<%= Resources.Site.Warning %>");
                return;
            }
            var obj = items[0].get_dataItem();
            PageMethods.VoteQuestionEditAnswer(obj.Id, CallWebMethodSuccess, CallWebMethodFailed);
        }
        function VoteQuestionEditAnswerCallback() {
            window.location.assign("http://" + window.location.host + "/Systems/CMS/VoteAnswerManage.aspx");
        }
        function pageLoad() {
            CreateElementVoteQuestionLinks();
            GetVoteQuestionList();
            SecurityUI();
        }
        function gvVoteQuestion_Command(sender, args) {
            args.set_cancel(true);
            sender.get_masterTableView().clearSelectedItems();
            var currentPageIndex = sender.get_masterTableView().get_currentPageIndex();
            var pageSize = sender.get_masterTableView().get_pageSize();
            var sortExpressions = sender.get_masterTableView().get_sortExpressions();

            PageMethods.GetVoteQuestionList(currentPageIndex * pageSize, pageSize, sortExpressions.toString(), CallWebMethodSuccess);
            PageMethods.GetVoteQuestionCount(CallWebMethodSuccess);
        }

        
        function RadWindowVoteQuestionClose(sender, args) {
            if (!args.get_argument())
                return;
            switch (dialogMethod) {
                case "VoteQuestionNew":
                    {
                        PageMethods.VoteQuestionAdd(CallWebMethodSuccess, CallWebMethodFailed);
                    }
                    break;
                case "VoteQuestionEdit":
                    {
                        PageMethods.VoteQuestionUpdate(CallWebMethodSuccess, CallWebMethodFailed);
                    }
                    break;
            }
        }
        var Vote_Add = 50;
        var Vote_Edit = 51;
        var Vote_Delete = 52;
        var Vote_Answer = 53;
        var btnVoteAdd;
        var btnVoteEdit;
        var btnVoteDelete;
        var btnVoteAnswer;
        function CreateElementVoteQuestionLinks() {
            btnVoteAdd = document.createElement("a");
            btnVoteAdd.setAttribute("href", "javascript:void(0)");
            btnVoteAdd.setAttribute("onClick", "VoteQuestionNew()");
            btnVoteAdd.setAttribute("class", "Header");
            var addName = document.createTextNode("[Thêm mới]");
            btnVoteAdd.appendChild(addName);

            btnVoteEdit = document.createElement("a");
            btnVoteEdit.setAttribute("href", "javascript:void(0)");
            btnVoteEdit.setAttribute("onClick", "VoteQuestionEdit()");
            btnVoteEdit.setAttribute("class", "Header");
            var editName = document.createTextNode("[Hiệu chỉnh]");
            btnVoteEdit.appendChild(editName);

            btnVoteDelete = document.createElement("a");
            btnVoteDelete.setAttribute("href", "javascript:void(0)");
            btnVoteDelete.setAttribute("onClick", "VoteQuestionDelete()");
            btnVoteDelete.setAttribute("class", "Header");
            var deleteName = document.createTextNode("[Xóa]");
            btnVoteDelete.appendChild(deleteName);

            btnVoteAnswer = document.createElement("a");
            btnVoteAnswer.setAttribute("href", "javascript:void(0)");
            btnVoteAnswer.setAttribute("onClick", "VoteQuestionEditAnswer()");
            btnVoteAnswer.setAttribute("class", "Header");
            var voteAnswerName = document.createTextNode("[Quản lý câu trả lời]");
            btnVoteAnswer.appendChild(voteAnswerName);
        }

        function SecurityUI() {
            PageMethods.GetPermission([Vote_Add, Vote_Edit, Vote_Delete, Vote_Answer], CallWebMethodSuccess, CallWebMethodFailed);
        }

        function SecurityUICallback(results, context, methodName) {
            var arrPermission = Sys.Serialization.JavaScriptSerializer.deserialize(results);
            if (arrPermission[Vote_Add]) {
                document.getElementById("voteQuestionLinks").appendChild(btnVoteAdd);
            }
            if (arrPermission[Vote_Edit]) {
                document.getElementById("voteQuestionLinks").appendChild(btnVoteEdit);
            }
            if (arrPermission[Vote_Delete]) {
                document.getElementById("voteQuestionLinks").appendChild(btnVoteDelete);
            }
            if (arrPermission[Vote_Answer]) {
                document.getElementById("voteQuestionLinks").appendChild(btnVoteAnswer);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindow ID="rwVoteQuestion" runat="server" Modal="True" VisibleStatusbar="False"
        OnClientClose="RadWindowVoteQuestionClose">
    </telerik:RadWindow>
    <div style="width: 815px; float: left; padding-left: 16px; padding-top: 25px;">
        <div style="width: 815px; float: left; margin-right: 12px;">
            <div class="left_box">
            </div>
            <div style="width: 795px;" class="box">
                <div class="title_box" align="left">
                    Quản lý câu hỏi</div>
            </div>
            <div class="right_box">
            </div>
            <div style="background-color: #edeeef; float: left; width: 815px; padding-top: 19px;
                padding-bottom: 19px;">
                <div>
                    <button type="button" class="btn btn-success btn-xs" onclick="VoteQuestionNew()"><i class="fa fa-plus"></i> Thêm mới</button>
                    <button type="button" class="btn btn-success btn-xs" onclick="VoteQuestionEdit()"><i class="fa fa-edit"></i> Hiệu chỉnh</button>
                    <button type="button" class="btn btn-success btn-xs" onclick="VoteQuestionEditAnswer()"><i class="fa fa-desktop"></i> Quản lý câu trả lời</button>
                    <button type="button" class="btn btn-success btn-xs" onclick="VoteQuestionDelete()"><i class="fa fa-close"></i> Xóa</button>
                </div>
                <hr />
                <div id="voteQuestionLinks" style="display:none;">
                   <%-- <a href="javascript:void(0)" id="btnNew" style="display: none" onclick="VoteQuestionNew();" class="Header">[<%= Resources.Site.AddItem %>]</a>
                    <a href="javascript:void(0)" id="btnEdit" style="display: none" onclick="VoteQuestionEdit();" class="Header">[<%= Resources.Site.EditItem %>]</a>
                    <a href="javascript:void(0)" id="btnDelete" style="display: none" onclick="VoteQuestionDelete();" class="Header">[<%= Resources.Site.DeleteItem %>]</a>
                    <a href="javascript:void(0)" id="btnAnswer" style="display: none" onclick="VoteQuestionEditAnswer();" class="Header">[Quản lý câu trả lời]</a>--%>
                    
                </div>
                <div>
                    <telerik:RadGrid ID="gvVoteQuestion" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                        AllowSorting="True" GridLines="None" AllowMultiRowSelection="True">
                        <MasterTableView ClientDataKeyNames="Id" AllowMultiColumnSorting="True">
                            <Columns>
                                <telerik:GridClientSelectColumn>
                                    <ItemStyle Width="20px" />
                                </telerik:GridClientSelectColumn>
                                <telerik:GridBoundColumn HeaderText="<%$ Resources:Site,Name %>" DataField="Name">
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings EnableRowHoverStyle="true">
                            <Selecting AllowRowSelect="True" />
                            <ClientEvents OnCommand="gvVoteQuestion_Command" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </div>
            </div>
            <div class="bottombox_left">
                &nbsp;</div>
            <div style="width: 795px;" class="bottombox_center">
                &nbsp;</div>
            <div class="bottombox_right">
                &nbsp;</div>
        </div>
    </div>
</asp:Content>
