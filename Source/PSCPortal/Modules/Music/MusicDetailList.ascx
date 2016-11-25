<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MusicDetailList.ascx.cs" Inherits="PSCPortal.Modules.Music.MusicDetailList" %>

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
        PSCPortal.Services.CMS.GetListAllMusic(tableView.get_currentPageIndex(), tableView.get_pageSize(), tableView.get_sortExpressions().toString(), CallGetListSuccess, CallWebMethodFailed);
    }

    function CallGetListSuccess(results, context, methodName) {
        var tableView = pageObject.gvResult.get_masterTableView();
        var dataResult = Sys.Serialization.JavaScriptSerializer.deserialize(results);
        tableView.set_dataSource(dataResult.data);
        tableView.set_virtualItemCount(dataResult.total);
        tableView.dataBind();

        var path = tableView._dataItems[0]._dataItem.Path;
        var musicPlayer = document.getElementById('musicDiv');
        musicDiv.innerHTML = '<div style="color: #5C2B03; font-weight: bold; margin-top: 10px;"><div style="padding-bottom: 10px;">' + tableView._dataItems[0]._dataItem.Title + '</div><audio id="myMusic" controls style="width: 100%; margin-top: 10px;"><source src="' + path + '" type="audio/mpeg">Trình duyệt của bạn không hổ trợ nghe nhạc.</audio></div>';
        var myMusic = new MediaElementPlayer('#myMusic', {
            success: function (mediaElement, domObject) { }
        });
    }

    function gvResult_OnRowDataBound(sender, args) {
        args.get_item().get_cell("musicName").style.backgroundColor = "#FFFFFF";
        args.get_item().get_cell("musicName").style.borderColor = "#5D8CC9";
        args.get_item().get_cell("musicName").style.padding = "20px";
        args.get_item().get_cell("musicName").style.fontSize = "13px";
        args.get_item().get_cell("musicName").style.cursor = "pointer";
    }

    function gvResult_OnRowClick(sender, args) {
        ChangeMusic(args._tableView._dataItems[args._itemIndexHierarchical]._dataItem.Path, args._tableView._dataItems[args._itemIndexHierarchical]._dataItem.Title);
    }

    function ChangeMusic(path, title) {
        var musicPlayer = document.getElementById('musicDiv');
        musicDiv.innerHTML = '<div style="color: #5C2B03; font-weight: bold; margin-top: 10px;"><div style="padding-bottom: 10px;">' + title + '</div><audio id="myMusic" controls style="width: 100%; margin-top: 10px;"><source src="' + path + '" type="audio/mpeg">Trình duyệt của bạn không hổ trợ nghe nhạc.</audio></div>';
        var myMusic = new MediaElementPlayer('#myMusic', {
            success: function (mediaElement, domObject) { mediaElement.play(); }
        });
    }

    function pageLoad() {
        pageObject_Initialize();
        GetList();
    }
</script>
<div>
    <h3>Âm nhạc</h3>
<div id="musicDiv">
</div>
<div style="margin-top: 15px;">
    <telerik:RadGrid ID="gvResult" runat="server" AllowPaging="True" PageSize="20" ShowHeader="false">
        <MasterTableView HeaderStyle-HorizontalAlign="Center" AllowMultiColumnSorting="true">
            <NoRecordsTemplate>
                <p style="padding: 5px; margin: 5px; color: Black; font-size: 14px;">
                    Không có dữ liệu
                </p>
            </NoRecordsTemplate>
            <HeaderStyle Font-Bold="true" Font-Size="14px" Height="60px" />
            <Columns>
                <telerik:GridBoundColumn UniqueName="musicName" HeaderText="Tên music" DataField="Title">
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