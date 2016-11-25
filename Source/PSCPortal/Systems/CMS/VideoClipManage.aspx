<%@ Page Title="Quản lý video trang chủ" Language="C#" MasterPageFile="~/Systems/MasterPage.Master" AutoEventWireup="true" CodeBehind="VideoClipManage.aspx.cs" Inherits="PSCPortal.Systems.CMS.VideoClipManage" %>

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

        function VideoClip_Add() {
            PageMethods.VideoClip_Add(CallVideoClip_AddSuccess, CallWebMethodFailed);
        }

        function CallVideoClip_AddSuccess(results, context, methodName) {
            dialogMethod = "VideoClip_Add";
            var oWnd = $find("<%= rwVideoClip.ClientID%>");
            oWnd.setSize(600, 600);
            oWnd.setUrl("VideoClipDetail.aspx");
            oWnd.set_title("Thêm Video Clip");
            oWnd.show();
        }

        function VideoClip_Update() {
            var items = pageObject.gvResult.get_masterTableView().get_selectedItems();
            if (items.length == 0) {
                radalert("Câu hỏi chưa được chọn.", 250, 100, "Chú ý");
                return;
            }
            PageMethods.Video_Update(items[0].get_dataItem().Id, CallVideo_UpdateSuccess, CallWebMethodFailed);
        }

        function CallVideo_UpdateSuccess(results, context, methodName) {
            dialogMethod = "VideoClip_Update";
            var oWnd = $find("<%= rwVideoClip.ClientID%>");
            oWnd.setSize(600, 600);
            oWnd.setUrl("VideoClipDetail.aspx");
            oWnd.set_title("Cập nhật Video Clip");
            oWnd.show();
        }

        function RadWindowVoteQuestionClose(sender, args) {
            var oWnd = $find("<%= rwVideoClip.ClientID%>");
            oWnd.get_contentFrame().contentWindow.pauseVideo();

            if (!args.get_argument())
                return;
            switch (dialogMethod) {
                case "VideoClip_Add":
                    {
                        PageMethods.AddVideoClip(function (results, context, methodName) { alert('Đã thêm thành công'); GetList(); }, CallWebMethodFailed);
                        break;
                    }
                case "VideoClip_Update":
                    {
                        PageMethods.UpdateVideoClip(function (results, context, methodName) { alert('Cập nhật thành công'); GetList(); }, CallWebMethodFailed);
                        break;
                    }
            }
        }

        function GetVideoClipSelect() {
            var items = pageObject.gvResult.get_masterTableView().get_selectedItems();
            var objList = new Array();
            for (var i = 0; i < items.length; i++) {
                Array.add(objList, items[i].get_dataItem().Id);
            }
            return objList;
        }

        function VideoClip_Delete() {
            var items = pageObject.gvResult.get_masterTableView().get_selectedItems();
            if (items.length == 0) {
                radalert("Câu hỏi chưa được chọn.", 250, 100, "Chú ý");
                return;
            }
            radconfirm("Bạn có chắc muốn xóa không ?", VideoClipDeleteConfirm, 250, 100, null, "Chú ý");
        }

        function VideoClipDeleteConfirm(args) {
            if (!args)
                return;
            var objList = GetVideoClipSelect();
            PageMethods.DeleteVideoClip(objList, function (results, context, methodName) { alert('Xóa thành công'); GetList(); }, CallWebMethodFailed);
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
    <telerik:RadWindow ID="rwVideoClip" runat="server" Modal="True" VisibleStatusbar="False"
        OnClientClose="RadWindowVoteQuestionClose">
    </telerik:RadWindow>
    <div style="width: 815px; float: left; padding-left: 16px; padding-top: 25px;">
        <div style="width: 815px; float: left; margin-right: 12px;">
            <div class="left_box">
            </div>
            <div style="width: 795px;" class="box">
                <div class="title_box" align="left">
                    Quản lý video trang chủ
                </div>
            </div>
            <div class="right_box">
            </div>
            <div style="background-color: #edeeef; float: left; width: 815px; padding-top: 19px; padding-bottom: 19px;">
                <div>
                    <button type="button" class="btn btn-success btn-xs" onclick="VideoClip_Add()"><i class="fa fa-plus"></i>Thêm mới</button>
                    <button type="button" class="btn btn-success btn-xs" onclick="VideoClip_Update()"><i class="fa fa-edit"></i>Hiệu chỉnh</button>
                    <button type="button" class="btn btn-success btn-xs" onclick="VideoClip_Delete()"><i class="fa fa-close"></i>Xóa</button>

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
                                <telerik:GridBoundColumn HeaderText="Tên video" DataField="Title">
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
