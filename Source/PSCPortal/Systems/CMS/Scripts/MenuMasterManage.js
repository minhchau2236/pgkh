var dialogMethod;
var oWnd;
var strWarning;
var strInformation;
var grid;
var strMenuMasterNotChoose;
var strMenuConfirmDelete;
var btnMMasterNew;
var btnMMasterEdit;
var btnMMasterCopy;
var btnMMasterDelete;
var btnMMasterEditMenu;
var btnMakeTopic;
var btnMMasterSecurity;
function CallWebMethodSuccess(results, context, methodName) {
    switch (methodName) {
        case "GetMenuMasterList":
            {
                GetMenuMasterListCallback(results, context, methodName);
            }
            break;
        case "GetMenuMasterCount":
            {
                GetMenuMasterCountCallback(results, context, methodName);
            }
            break;
        case "MenuMasterNew":
            {
                MenuMasterNewCallback(results, context, methodName);
            }
            break;
        case "MenuMasterAdd":
            {
                MenuMasterAddCallback(results, context, methodName);
            }
            break;
        case "MenuMasterEdit":
            {
                MenuMasterEditCallback(results, context, methodName);
            }
            break;
        case "MenuMasterUpdate":
            {
                MenuMasterUpdateCallback(results, context, methodName);
            }
            break;
        case "MenuMasterDelete":
            {
                MenuMasterDeleteCallback(results, context, methodName);
            }
            break;
        case "GetMenuMaster_Menu":
            {
                DisplayMenus();
            }
            break;
        case "MenuMasterCopy":
            {
                MenuMasterCopyCallback(results, context, methodName);
            }
            break;
        case "MenuMasterCopyDo":
            {
                MenuMasterCopyDoCallback(results, context, methodName);
            }
            break;
        case "MenuMakeTopic":
            {
                MenuMakeTopicCallback(results, context, methodName);
            }
            break;
        case "MenuMakeTopicDo":
            {
                MenuMakeTopicDoCallback(results, context, methodName);
            }
            break;
        case "MenuMasterSecurity":
            {
                MenuMasterSecurityCallback(results, context, methodName);
            }
            break;
        case "GetMenuMasterPermission":
            {
                MenuMasterPermissionCallback(results, context, methodName);
            }
            break;
    }
}
function GetMenuMasterSelect() {
    var items = grid.get_masterTableView().get_selectedItems();

    var objList = new Array();
    for (var i = 0; i < items.length; i++) {
        Array.add(objList, items[i].get_dataItem().Id);
    }
    return objList;
}
function CallWebMethodFailed(results, context, methodName) {
    //   radalert(results._message, 250, 100, strWarning);
}

function GetMenuMasterList() {
    var tableView = grid.get_masterTableView();
    var sortExpressions = tableView.get_sortExpressions();
    PageMethods.GetMenuMasterList(0, tableView.get_pageSize(), sortExpressions.toString(), CallWebMethodSuccess);
    PageMethods.GetMenuMasterCount(CallWebMethodSuccess);
}
function GetMenuMasterListCallback(results, context, methodName) {
    var tableView = grid.get_masterTableView();
    tableView.set_dataSource(Sys.Serialization.JavaScriptSerializer.deserialize(results));
    tableView.dataBind();
}

function GetMenuMasterCountCallback(results, context, methodName) {
    var tableView = grid.get_masterTableView();
    tableView.set_virtualItemCount(results);
}

function MenuMasterNew() {
    PageMethods.MenuMasterNew(CallWebMethodSuccess, CallWebMethodFailed);
}
function MenuMasterNewCallback(results, context, methodName) {
    dialogMethod = "MenuMasterNew";
    oWnd.setSize(800, 250);
    oWnd.setUrl("MenuMasterDetail.aspx");
    oWnd.show();
}
function MenuMasterAddCallback(result, context, methodName) {
    GetMenuMasterList();
}

function MenuMasterEdit() {
    var items = grid.get_masterTableView().get_selectedItems();
    if (items.length == 0) {
        radalert(strMenuMasterNotChoose, 250, 100, strWarning);
        return;
    }
    var obj = items[0].get_dataItem();
    PageMethods.MenuMasterEdit(obj.Id, CallWebMethodSuccess, CallWebMethodFailed);
}
function MenuMasterEditCallback(results, context, methodName) {
    dialogMethod = "MenuMasterEdit";
    oWnd.setSize(800, 250);
    oWnd.setUrl("MenuMasterDetail.aspx");
    oWnd.show();
}

function MenuMasterUpdateCallback(results, context, methodName) {
    GetMenuMasterList();
}

function MenuMasterDelete() {
    var objList = GetMenuMasterSelect();
    if (objList.length == 0) {
        radalert(strMenuMasterNotChoose, 250, 100, strWarning);
        return;
    }
    radconfirm(strMenuConfirmDelete, MenuMasterDeleteConfirm, 250, 100, null, strWarning);
}
function MenuMasterDeleteConfirm(arg) {
    if (!arg)
        return;
    var objList = GetMenuMasterSelect();
    if (objList.length == 0) {
        radalert(strMenuMasterNotChoose, 250, 100, strWarning);
        return;
    }
    PageMethods.MenuMasterDelete(objList, CallWebMethodSuccess, CallWebMethodFailed);
}
function EditMenus() {
    var items = grid.get_selectedItems();
    if (items.length == 0) {
        radalert(strMenuMasterNotChoose, 250, 100, strWarning);
        return;
    }
    var obj = items[0].get_dataItem();

    PageMethods.GetMenuMaster_Menu(obj.Id, CallWebMethodSuccess, CallWebMethodFailed);
}
function DisplayMenus() {
    window.location.assign("http://" + window.location.host + "/Systems/CMS/MenuManage.aspx");
}
function MenuMasterDeleteCallback(results, context, methodName) {
    GetMenuMasterList();
    grid.get_masterTableView().clearSelectedItems();
}

function MenuMasterCopyCallback(results, context, methodName) {
    dialogMethod = "MenuMasterCopy";
    oWnd.setSize(800, 250);
    oWnd.setUrl("MenuMasterDetail.aspx");
    oWnd.show();
}
function MenuMasterCopyDoCallback(results, context, methodName) {
    GetMenuMasterList();
}
function MenuMasterCopy() {
    var items = grid.get_selectedItems();
    if (items.length == 0) {
        radalert(strMenuMasterNotChoose, 250, 100, strWarning);
        return;
    }
    var obj = items[0].get_dataItem();
    PageMethods.MenuMasterCopy(CallWebMethodSuccess, CallWebMethodFailed);
}
function MenuMakeTopic() {
    var items = grid.get_selectedItems();
    if (items.length == 0) {
        radalert(strMenuMasterNotChoose, 250, 100, strWarning);
        return;
    }
    var obj = items[0].get_dataItem();
    PageMethods.MenuMakeTopic(obj.Id, CallWebMethodSuccess, CallWebMethodFailed);
}
function MenuMakeTopicCallback(results, context, methodName) {
    dialogMethod = "MenuMakeTopic";
    oWnd.setSize(800, 250);
    oWnd.setUrl("MenuMasterMakeTopic.aspx");
    oWnd.show();
}
function MenuMakeTopicDoCallback(results, context, methodName) {
    alert("thanh cong");
}

function CreateElementMenuMasterLinks() {
    btnMMasterNew = document.createElement("a");
    btnMMasterNew.setAttribute("href", "javascript:void(0)");
    btnMMasterNew.setAttribute("onClick", "MenuMasterNew()");
    btnMMasterNew.setAttribute("class", "Header");
    var addName = document.createTextNode("[Thêm mới]");
    btnMMasterNew.appendChild(addName);

    btnMMasterEdit = document.createElement("a");
    btnMMasterEdit.setAttribute("href", "javascript:void(0)");
    btnMMasterEdit.setAttribute("onClick", "MenuMasterEdit()");
    btnMMasterEdit.setAttribute("class", "Header");
    var editName = document.createTextNode("[Hiệu chỉnh]");
    btnMMasterEdit.appendChild(editName);

    btnMMasterCopy = document.createElement("a");
    btnMMasterCopy.setAttribute("href", "javascript:void(0)");
    btnMMasterCopy.setAttribute("onClick", "MenuMasterCopy()");
    btnMMasterCopy.setAttribute("class", "Header");
    var copyName = document.createTextNode("[Sao chép MenuMaster]");
    btnMMasterCopy.appendChild(copyName);

    btnMMasterDelete = document.createElement("a");
    btnMMasterDelete.setAttribute("href", "javascript:void(0)");
    btnMMasterDelete.setAttribute("onClick", "MenuMasterDelete()");
    btnMMasterDelete.setAttribute("class", "Header");
    var deletehName = document.createTextNode("[Xóa]");
    btnMMasterDelete.appendChild(deletehName);

    btnMMasterEditMenu = document.createElement("a");
    btnMMasterEditMenu.setAttribute("href", "javascript:void(0)");
    btnMMasterEditMenu.setAttribute("onClick", "EditMenus()");
    btnMMasterEditMenu.setAttribute("class", "Header");
    var editMenuName = document.createTextNode("[Quản lý Menu]");
    btnMMasterEditMenu.appendChild(editMenuName);

    btnMakeTopic = document.createElement("a");
    btnMakeTopic.setAttribute("href", "javascript:void(0)");
    btnMakeTopic.setAttribute("onClick", "MenuMakeTopic()");
    btnMakeTopic.setAttribute("class", "Header");
    var makeTopicName = document.createTextNode("[Chuyển Menu thành CM]");
    btnMakeTopic.appendChild(makeTopicName);

    //btnMMasterSecurity = document.createElement("a");
    //btnMMasterSecurity.setAttribute("href", "javascript:void(0)");
    //btnMMasterSecurity.setAttribute("onClick", "MenuMasterSecurity()");
    //btnMMasterSecurity.setAttribute("class", "Header");
    //var securityName = document.createTextNode("[Phân quyền]");
    //btnMMasterSecurity.appendChild(securityName);
}

function pageLoad() {
    initialize();
    CreateElementMenuMasterLinks();
    GetMenuMasterList();
    MenuMasterPermission();
}

var MenuMaster_Add = 70;
var MenuMaster_Edit = 71;
var MenuMaster_Copy = 72;
var MenuMaster_Delete = 73;
var MenuMaster_ManageMenu = 74;
var MenuMaster_MakeTopic = 75;
function MenuMasterPermission() {
    PageMethods.GetMenuMasterPermission([MenuMaster_Add, MenuMaster_Edit, MenuMaster_Copy, MenuMaster_Delete, MenuMaster_ManageMenu, MenuMaster_MakeTopic], CallWebMethodSuccess, CallWebMethodFailed);
}
function MenuMasterPermissionCallback(results, context, methodName) {
    document.getElementById("menuMasterLinks").innerHTML = "";
    var arrPermission = Sys.Serialization.JavaScriptSerializer.deserialize(results);
    if (arrPermission[MenuMaster_Add]) {
        document.getElementById("menuMasterLinks").appendChild(btnMMasterNew);
    }
    if (arrPermission[MenuMaster_Edit]) {
        document.getElementById("menuMasterLinks").appendChild(btnMMasterEdit);
    }
    if (arrPermission[MenuMaster_Copy]) {
        document.getElementById("menuMasterLinks").appendChild(btnMMasterCopy);
    }
    if (arrPermission[MenuMaster_Delete]) {
        document.getElementById("menuMasterLinks").appendChild(btnMMasterDelete);
    }
    if (arrPermission[MenuMaster_ManageMenu]) {
        document.getElementById("menuMasterLinks").appendChild(btnMMasterEditMenu);
    }
    if (arrPermission[MenuMaster_MakeTopic]) {
        document.getElementById("menuMasterLinks").appendChild(btnMakeTopic);
    }
}
function row_select(sender, args) {
    MenuMasterPermission();
}
function gvMenuMaster_Command(sender, args) {
    args.set_cancel(true);
    sender.get_masterTableView().clearSelectedItems();
    var currentPageIndex = sender.get_masterTableView().get_currentPageIndex();
    var pageSize = sender.get_masterTableView().get_pageSize();
    var sortExpressions = sender.get_masterTableView().get_sortExpressions();

    PageMethods.GetMenuMasterList(currentPageIndex * pageSize, pageSize, sortExpressions.toString(), CallWebMethodSuccess);
    PageMethods.GetMenuMasterCount(CallWebMethodSuccess);
}
function MenuMasterSecurity() {

    var items = grid.get_selectedItems();
    if (items.length == 0) {
        radalert(strMenuMasterNotChoose, 250, 100, strWarning);
        return;
    }
    var obj = items[0].get_dataItem();


    PageMethods.MenuMasterSecurity(obj.Id, CallWebMethodSuccess, CallWebMethodFailed);
}
function MenuMasterSecurityCallback(results, context, methodName) {
    dialogMethod = "MenuMasterPermission";
    oWnd.setSize(380, 230);
    oWnd.setUrl("MenuMasterSecurity.aspx");
    oWnd.show();
}
function RadWindowMenuMasterClose(sender, args) {
    if (!args.get_argument())
        return;
    switch (dialogMethod) {
        case "MenuMasterNew":
            {
                PageMethods.MenuMasterAdd(CallWebMethodSuccess, CallWebMethodFailed);
            }
            break;
        case "MenuMasterEdit":
            {
                PageMethods.MenuMasterUpdate(CallWebMethodSuccess, CallWebMethodFailed);
            }
            break;
        case "MenuMasterCopy":
            {
                var items = grid.get_selectedItems();
                var obj = items[0].get_dataItem();
                PageMethods.MenuMasterCopyDo(obj.Id, CallWebMethodSuccess, CallWebMethodFailed);
            }
            break;
        case "MenuMakeTopic":
            {
                var items = grid.get_selectedItems();
                var obj = items[0].get_dataItem();
                PageMethods.MenuMakeTopicDo(obj.Id, CallWebMethodSuccess, CallWebMethodFailed);
            }
            break;

    }
}
