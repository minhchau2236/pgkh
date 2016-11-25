<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VideoClipDetailList.ascx.cs" Inherits="PSCPortal.Modules.VideoClip.VideoClipDetailList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<script type="text/javascript">
    var pageObject = {
        gvResult: null
    }

    function pageObject_Initialize() {
        pageObject.gvResult = $find("<%=gvResult.ClientID%>");
    }

    function gvResult_OnCommand(sender, args) {
        args.set_cancel(true);
        GetList();
    }

    function CallWebMethodFailed(results, context, methodName) {
        radalert(results._message);
    }

    function GetList() {
        var tableView = pageObject.gvResult.get_masterTableView();
        PSCPortal.Services.CMS.GetListAllVideoClip(tableView.get_currentPageIndex(), tableView.get_pageSize(), tableView.get_sortExpressions().toString(), CallGetListSuccess, CallWebMethodFailed);
    }

    function CallGetListSuccess(results, context, methodName) {
        var tableView = pageObject.gvResult.get_masterTableView();
        var dataResult = Sys.Serialization.JavaScriptSerializer.deserialize(results);
        tableView.set_dataSource(dataResult.data);
        tableView.set_virtualItemCount(dataResult.total);
        tableView.dataBind();

        var path = tableView._dataItems[0]._dataItem.Path;
        var fileEx = path.substring(path.toString().lastIndexOf(".") + 1).toLowerCase();
        if (fileEx == "flv")
            fileEx = "flv";
        fileEx = "video/" + fileEx;
        var videoPlayer = document.getElementById('videoDiv');
        videoDiv.innerHTML = '<div style="padding: 15px 0 0 0;">' + '<video id="myVideo" width="679" height="450" controls style="background-color: black;"><source src="' + path + '" type="' + fileEx + '">Trình duyệt của bạn không hổ trợ xem video.</video>' + '</div><div style="width: 350px; color: #5C2B03; padding: 15px 0 0 0; font-weight: bold;">' + tableView._dataItems[0]._dataItem.Title + '</div>';
        var myVideo = new MediaElementPlayer('#myVideo', {
            success: function (mediaElement, domObject) { }
        });
    }

    function gvResult_OnRowDataBound(sender, args) {
        args.get_item().get_cell("videoName").style.backgroundColor = "#FFFFFF";
        args.get_item().get_cell("videoName").style.borderColor = "#5D8CC9";
        args.get_item().get_cell("videoName").style.padding = "20px";
        args.get_item().get_cell("videoName").style.fontSize = "13px";
        args.get_item().get_cell("videoName").style.cursor = "pointer";
    }

    function gvResult_OnRowClick(sender, args) {
        ChangeVideo(args._tableView._dataItems[args._itemIndexHierarchical]._dataItem.Path, args._tableView._dataItems[args._itemIndexHierarchical]._dataItem.Title);
    }

    function ChangeVideo(path, title) {
        var fileEx = path.substring(path.toString().lastIndexOf(".") + 1).toLowerCase();
        if (fileEx == "flv")
            fileEx = "flv";
        fileEx = "video/" + fileEx;

        var videoPlayer = document.getElementById('videoDiv');
        videoDiv.innerHTML = '<div style="padding: 15px 0 0 0;">' + '<video id="myVideo" width="679" height="450" controls style="background-color: black;"><source src="' + path + '" type="' + fileEx + '">Trình duyệt của bạn không hổ trợ xem video.</video>' + '</div><div style="width: 350px; color: #5C2B03; padding: 15px 0 0 0; font-weight: bold;">' + title + '</div>';
        myVideo = new MediaElementPlayer('#myVideo', {
            success: function (mediaElement, domObject) { mediaElement.play(); }
        });
    }

    function pageLoad() {
        pageObject_Initialize();
        GetList();
    }
</script>
<div>
    <h3>Pháp thoại</h3>
<div id="videoDiv">
</div>
<div style="margin-top: 10px;">
    <telerik:RadGrid ID="gvResult" runat="server" AllowPaging="True" PageSize="20" ShowHeader="false">
        <MasterTableView HeaderStyle-HorizontalAlign="Center" AllowMultiColumnSorting="true">
            <NoRecordsTemplate>
                <p style="padding: 5px; margin: 5px; color: Black; font-size: 14px;">
                    Không có dữ liệu
                </p>
            </NoRecordsTemplate>
            <HeaderStyle Font-Bold="true" Font-Size="14px" Height="60px" />
            <Columns>
                <telerik:GridBoundColumn UniqueName="videoName" HeaderText="Tên video" DataField="Title">
                    <ItemStyle Width="200px" />
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings Resizing-AllowColumnResize="false" Selecting-AllowRowSelect="false">
            <ClientEvents OnCommand="gvResult_OnCommand" OnRowDataBound="gvResult_OnRowDataBound" OnRowClick="gvResult_OnRowClick" />
            <Scrolling AllowScroll="true" ScrollHeight="700px" />
        </ClientSettings>
    </telerik:RadGrid>
</div>
</div>