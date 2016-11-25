<%@ Page Title="Quản lý nhạc trang chủ" Language="C#" MasterPageFile="~/Systems/MasterPage.Master" AutoEventWireup="true" CodeBehind="MusicManage.aspx.cs" Inherits="PSCPortal.Systems.CMS.MusicManage" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var pageObject = {
            gvResult: null
        }

        function pageObject_Initialize() {
            pageObject.gvResult = $find("<%=gvResult.ClientID%>");
        }

        function gvResult_OnCommand(sender, args) {
            args.set_cancel(true);
            sender.get_masterTableView().clearSelectedItems();
            GetList();
        }

        function CallWebMethodFailed(results, context, methodName) {
            radalert(results._message);
        }

        function GetList() {
            var tableView = pageObject.gvResult.get_masterTableView();
            PageMethods.GetList(tableView.get_currentPageIndex(), tableView.get_pageSize(), tableView.get_sortExpressions().toString(), CallGetListSuccess, CallWebMethodFailed);
        }

        function CallGetListSuccess(results, context, methodName) {
            var tableView = pageObject.gvResult.get_masterTableView();
            var dataResult = Sys.Serialization.JavaScriptSerializer.deserialize(results);
            tableView.set_dataSource(dataResult.data);
            tableView.set_virtualItemCount(dataResult.total);
            tableView.dataBind();

            tableView._columnsInternal[0]._element.width = "10px";
            tableView._columnsInternal[1]._element.width = "300px";
            tableView._columnsInternal[2]._element.width = "75px";
        }

        function Music_Add() {
            PageMethods.Music_Add(CallMusic_AddSuccess, CallWebMethodFailed);
        }

        function CallMusic_AddSuccess(results, context, methodName) {
            dialogMethod = "Music_Add";
            var oWnd = $find("<%= rwMusic.ClientID%>");
            oWnd.setSize(600, 300);
            oWnd.setUrl("MusicDetail.aspx");
            oWnd.set_title("Thêm Music");
            oWnd.show();
        }

        function Music_Update() {
            var items = pageObject.gvResult.get_masterTableView().get_selectedItems();
            if (items.length == 0) {
                radalert("Câu hỏi chưa được chọn.", 250, 100, "Chú ý");
                return;
            }
            PageMethods.Music_Update(items[0].get_dataItem().Id, CallMusic_UpdateSuccess, CallWebMethodFailed);
        }

        function CallMusic_UpdateSuccess(results, context, methodName) {
            dialogMethod = "Music_Update";
            var oWnd = $find("<%= rwMusic.ClientID%>");
            oWnd.setSize(600, 300);
            oWnd.setUrl("MusicDetail.aspx");
            oWnd.set_title("Cập nhật Music");
            oWnd.show();
        }

        function RadWindowVoteQuestionClose(sender, args) {
            var oWnd = $find("<%= rwMusic.ClientID%>");
            oWnd.get_contentFrame().contentWindow.pauseMusic();

            if (!args.get_argument())
                return;
            switch (dialogMethod) {
                case "Music_Add":
                    {
                        PageMethods.AddMusic(function (results, context, methodName) { alert('Đã thêm thành công'); GetList(); }, CallWebMethodFailed);
                        break;
                    }
                case "Music_Update":
                    {
                        PageMethods.UpdateMusic(function (results, context, methodName) { alert('Cập nhật thành công'); GetList(); }, CallWebMethodFailed);
                        break;
                    }
            }
        }

        function GetMusicSelect() {
            var items = pageObject.gvResult.get_masterTableView().get_selectedItems();
            var objList = new Array();
            for (var i = 0; i < items.length; i++) {
                Array.add(objList, items[i].get_dataItem().Id);
            }
            return objList;
        }

        function Music_Delete() {
            var items = pageObject.gvResult.get_masterTableView().get_selectedItems();
            if (items.length == 0) {
                radalert("Câu hỏi chưa được chọn.", 250, 100, "Chú ý");
                return;
            }
            radconfirm("Bạn có chắc muốn xóa không ?", MusicDeleteConfirm, 250, 100, null, "Chú ý");
        }

        function MusicDeleteConfirm(args) {
            if (!args)
                return;
            var objList = GetMusicSelect();
            PageMethods.DeleteMusic(objList, function (results, context, methodName) { alert('Xóa thành công'); GetList(); }, CallWebMethodFailed);
        }

        function gvResult_OnRowDataBound(sender, args) {
            try {
                var item = args.get_item();
                item._element.cells[2].style.textAlign = "center";
            }
            catch (err) {
            }
        }

        function pageLoad() {
            pageObject_Initialize();
            GetList();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindow ID="rwMusic" runat="server" Modal="True" VisibleStatusbar="False"
        OnClientClose="RadWindowVoteQuestionClose">
    </telerik:RadWindow>
    <div style="width: 815px; float: left; padding-left: 16px; padding-top: 25px;">
        <div style="width: 815px; float: left; margin-right: 12px;">
            <div class="left_box">
            </div>
            <div style="width: 795px;" class="box">
                <div class="title_box" align="left">
                    Quản lý Music trang chủ
                </div>
            </div>
            <div class="right_box">
            </div>
            <div style="background-color: #edeeef; float: left; width: 815px; padding-top: 19px; padding-bottom: 19px;">
                <div>
                    <button type="button" class="btn btn-success btn-xs" onclick="Music_Add()"><i class="fa fa-plus"></i>Thêm mới</button>
                    <button type="button" class="btn btn-success btn-xs" onclick="Music_Update()"><i class="fa fa-edit"></i>Hiệu chỉnh</button>
                    <button type="button" class="btn btn-success btn-xs" onclick="Music_Delete()"><i class="fa fa-close"></i>Xóa</button>

                    <%--<button type="button" class="btn btn-success btn-xs" onclick="VoteQuestionNew()"><i class="fa fa-plus"></i>Thêm mới</button>
                    <button type="button" class="btn btn-success btn-xs" onclick="VoteQuestionEdit()"><i class="fa fa-edit"></i>Hiệu chỉnh</button>
                    <button type="button" class="btn btn-success btn-xs" onclick="VoteQuestionDelete()"><i class="fa fa-close"></i>Xóa</button>--%>
                </div>
                <hr />
                <div>
                    <telerik:RadGrid ID="gvResult" runat="server" AllowPaging="True">
                        <MasterTableView HeaderStyle-HorizontalAlign="Center" AllowMultiColumnSorting="true">
                            <NoRecordsTemplate>
                                <p style="padding: 5px; margin: 5px; color: Black; font-size: 14px;">
                                    Không có dữ liệu
                                </p>
                            </NoRecordsTemplate>
                            <HeaderStyle Font-Bold="true" Font-Size="14px" Height="60px" />
                            <Columns>
                                <telerik:GridClientSelectColumn>
                                    <ItemStyle Width="20px" />
                                </telerik:GridClientSelectColumn>
                                <telerik:GridBoundColumn HeaderText="Tên Music" DataField="Title">
                                    <ItemStyle Width="200px" />
                                </telerik:GridBoundColumn>
                                <%--<telerik:GridBoundColumn HeaderText="Đường dẫn file" DataField="Path">
                                    <ItemStyle Width="200px" />
                                </telerik:GridBoundColumn>--%>
                                <telerik:GridBoundColumn HeaderText="Ngày tạo" DataField="CreationDateString">
                                    <ItemStyle Width="50px" />
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings Resizing-AllowColumnResize="true" Selecting-AllowRowSelect="true">
                            <ClientEvents OnCommand="gvResult_OnCommand" OnRowDataBound="gvResult_OnRowDataBound" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
