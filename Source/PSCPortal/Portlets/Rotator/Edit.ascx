<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Edit.ascx.cs" Inherits="PSCPortal.Portlets.Rotator.Edit" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<style>
    .submit {
        color: #333;
    }
</style>
<script language="javascript" type="text/javascript">
    var grid;
    var oWnd;
    var strWarning;
    var dialogMethod;
    var txtPortletName;
    var dataId;
    function CallWebMethodSuccess(results, context, methodName) {
        switch (methodName) {
            case "GetImagePortletList":
                {
                    GetImagePortletListCallback(results, context, methodName);
                }
                break;
            case "ImagePortletNew":
                {
                    ImagePortletNewCallback(results, context, methodName);
                }
                break;
            case "ImagePortletAdd":
                {
                    ImagePortletAddCallback(results, context, methodName);
                }
                break;
            case "ImagePortletEdit":
                {
                    ImagePortletEditCallback(results, context, methodName);
                }
                break;
            case "ImagePortletUpdate":
                {
                    ImagePortletUpdateCallback(results, context, methodName);
                }
                break;
            case "ImagePortletDelete":
                {
                    ImagePortletDeleteCallback(results, context, methodName);
                }
                break;
        }
    }
    function GetImagePortletSelect() {
        var items = grid.get_masterTableView().get_selectedItems();

        var objList = new Array();
        for (var i = 0; i < items.length; i++) {
            Array.add(objList, items[i].get_dataItem().Id);
        }
        return objList;
    }
    function CallWebMethodFailed(results, context, methodName) {
        alert(results._message);
    }

    function GetImagePortletList() {
        PSCPortal.Portlets.Rotator.Libs.ImageService.GetImagePortletList(CallWebMethodSuccess);
    }
    function GetImagePortletListCallback(results, context, methodName) {
        var tableView = grid.get_masterTableView();
        tableView.set_dataSource(Sys.Serialization.JavaScriptSerializer.deserialize(results));
        tableView.dataBind();
    }
    function ImagePortletNew() {
        PSCPortal.Portlets.Rotator.Libs.ImageService.ImagePortletNew(CallWebMethodSuccess, CallWebMethodFailed);
    }
    function ImagePortletNewCallback(results, context, methodName) {
        dialogMethod = "ImagePortletAdd";
        oWnd.setSize(600, 450);
        oWnd.setUrl("/Portlets/Rotator/Detail.aspx");
        oWnd.show();
    }
    function ImagePortletAddCallback(result, context, methodName) {
        GetImagePortletList();
    }
    function ImagePortletEdit() {
        var items = grid.get_masterTableView().get_selectedItems();
        if (items.length == 0) {
            alert("You not select ImagePortlet!");
            return;
        }
        var obj = items[0].get_dataItem();
        PSCPortal.Portlets.Rotator.Libs.ImageService.ImagePortletEdit(obj.Id, CallWebMethodSuccess, CallWebMethodFailed);
    }
    function ImagePortletEditCallback(results, context, methodName) {
        dialogMethod = "ImagePortletEdit";
        oWnd.setSize(600, 450);
        oWnd.setUrl("/Portlets/Rotator/Detail.aspx");
        oWnd.show();
    }
    function ImagePortletUpdateCallback(results, context, methodName) {
        GetImagePortletList();
    }
    function ImagePortletDelete() {
        var objList = GetImagePortletSelect();
        if (objList.length == 0) {
            alert("You not select ImagePortlet!");
            return;
        }
        //radconfirm("Bạn có chắc muốn xóa?", ImagePortletDeleteConfirm, 250, 100, null, strWarning);
        PSCPortal.Portlets.Rotator.Libs.ImageService.ImagePortletDelete(objList, CallWebMethodSuccess, CallWebMethodFailed);
    }
    function ImagePortletDeleteConfirm(arg) {
        if (!arg)
            return;
        var objList = GetImagePortletSelect();
        PSCPortal.Portlets.Rotator.Libs.ImageService.ImagePortletDelete(objList, CallWebMethodSuccess, CallWebMethodFailed);
    }
    function ImagePortletDeleteCallback(results, context, methodName) {
        GetImagePortletList();
        grid.get_masterTableView().clearSelectedItems();
    }
    function pageLoad() {
        initialize();
        GetImagePortletList();
    }
    function gvImagePortlet_Command(sender, args) {
        args.set_cancel(true);
        sender.get_masterTableView().clearSelectedItems();
        PSCPortal.Portlets.Rotator.Libs.ImageService.GetImagePortletList(CallWebMethodSuccess);
    }
    function RadWindowImagePortletClose(sender, args) {
        if (!args.get_argument())
            return;
        switch (dialogMethod) {
            case "ImagePortletAdd":
                {
                    PSCPortal.Portlets.Rotator.Libs.ImageService.ImagePortletAdd(CallWebMethodSuccess, CallWebMethodFailed);
                }
                break;
            case "ImagePortletEdit":
                {
                    PSCPortal.Portlets.Rotator.Libs.ImageService.ImagePortletUpdate(CallWebMethodSuccess, CallWebMethodFailed);
                }
                break;
        }
    }
    var PortletId;
    function initialize() {
        grid = $find("<%=gvImagePortlet.ClientID%>");
        oWnd = $find("<%= rwImagePortlet.ClientID %>");
        strWarning = "<%= Resources.Site.Warning %>";
        txtPortletName = document.getElementById("<%= txtPortletName.ClientID %>");
    }

</script>
<telerik:radwindow id="rwImagePortlet" runat="server" modal="True" visiblestatusbar="False"
    onclientclose="RadWindowImagePortletClose">
</telerik:radwindow>
<div style="float: left; padding: 5px 5px 5px 5px;">
    Tên Portlet:
    <input type="text" id="txtPortletName" runat="server" />
</div>
<div style="clear: both; font-weight: bold; font-family: Tahoma; font-size: medium; text-align: center; color: #333;">
    Quản Trị Hình ảnh
</div>
<div style="clear: both; float: left">
    <div>
        <a href="javascript:void(0)" class="Header" onclick="ImagePortletNew();">[Thêm mới]</a>
        <a href="javascript:void(0)" class="Header" onclick="ImagePortletEdit();">[Hiệu Chỉnh]</a>
        <a href="javascript:void(0)" class="Header" onclick="ImagePortletDelete();">[Xóa]</a>
    </div>
    <div>
        <telerik:radgrid id="gvImagePortlet" runat="server" autogeneratecolumns="False" allowpaging="True"
            allowsorting="True" gridlines="None" allowmultirowselection="True" width="750px">
            <MasterTableView ClientDataKeyNames="Id" AllowMultiColumnSorting="True">
                <Columns>
                    <telerik:GridClientSelectColumn>
                        <ItemStyle Width="20px" />
                    </telerik:GridClientSelectColumn>
                    <telerik:GridBoundColumn HeaderText="Title" DataField="Title">
                    </telerik:GridBoundColumn>
                    <telerik:GridImageColumn DataImageUrlFields="Url" DataImageUrlFormatString="{0}" ImageWidth="150px" ImageHeight="100px" />
                    <telerik:GridBoundColumn HeaderText="Link" DataField="Link" />
                    <telerik:GridBoundColumn HeaderText="Order" DataField="Order" />
                </Columns>
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="True" />
                <ClientEvents OnCommand="gvImagePortlet_Command" />
            </ClientSettings>
        </telerik:radgrid>
    </div>
    <div style="text-align: center; padding: 5px 5px 5px 5px;">
        <asp:Button ID="btnOk" runat="server" Text="Lưu" Width="70px"
            OnClick="btnSave_Click" />
        <asp:Button ID="btnCancel" runat="server" Text="Hủy" Width="70px"
            OnClick="btnCancel_Click" />
    </div>
</div>
