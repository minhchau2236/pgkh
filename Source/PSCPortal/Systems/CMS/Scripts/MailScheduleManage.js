var dialogMethod;
var oWnd;
var strWarning;
var strInformation;
var grid;
var strMailScheduleNotChoose;
var strMenuConfirmDelete;
function CallWebMethodSuccess(results, context, methodName) {
    switch (methodName) {
        case "GetMailScheduleList":
            {
                GetMailScheduleListCallback(results, context, methodName);
            }
            break;
        case "GetMailScheduleCount":
            {
                GetMailScheduleCountCallback(results, context, methodName);
            }
            break;
        case "MailScheduleNew":
            {
                MailScheduleNewCallback(results, context, methodName);
            }
            break;
        case "MailScheduleAdd":
            {
                MailScheduleAddCallback(results, context, methodName);
            }
            break;
        case "MailScheduleEdit":
            {
                MailScheduleEditCallback(results, context, methodName);
            }
            break;
        case "MailScheduleUpdate":
            {
                MailScheduleUpdateCallback(results, context, methodName);
            }
            break;
        case "MailScheduleDelete":
            {
                MailScheduleDeleteCallback(results, context, methodName);
            }
            break;        
    }
}
function GetMailScheduleSelect() {
    var items = grid.get_masterTableView().get_selectedItems();

    var objList = new Array();
    for (var i = 0; i < items.length; i++) {
        Array.add(objList, items[i].get_dataItem().Id);
    }
    return objList;
}
function CallWebMethodFailed(results, context, methodName) {
    radalert(results._message, 250, 100, strWarning);
}

function GetMailScheduleList() {
    var tableView = grid.get_masterTableView();
    var sortExpressions = tableView.get_sortExpressions();
    PageMethods.GetMailScheduleList(0, tableView.get_pageSize(), sortExpressions.toString(), CallWebMethodSuccess);
    PageMethods.GetMailScheduleCount(CallWebMethodSuccess);
}
function GetMailScheduleListCallback(results, context, methodName) {
    var tableView = grid.get_masterTableView();
    tableView.set_dataSource(Sys.Serialization.JavaScriptSerializer.deserialize(results));
    tableView.dataBind();
}

function GetMailScheduleCountCallback(results, context, methodName) {
    var tableView = grid.get_masterTableView();
    tableView.set_virtualItemCount(results);
}

function MailScheduleNew() {
    PageMethods.MailScheduleNew(CallWebMethodSuccess, CallWebMethodFailed);
}
function MailScheduleNewCallback(results, context, methodName) {
    dialogMethod = "MailScheduleNew";
    oWnd.setSize(600, 200);
    oWnd.setUrl("MailScheduleDetail.aspx");
    oWnd.show();
}
function MailScheduleAddCallback(result, context, methodName) {
    GetMailScheduleList();
}

function MailScheduleEdit() {
    var items = grid.get_masterTableView().get_selectedItems();
    if (items.length == 0) {
        radalert(strMailScheduleNotChoose, 250, 100, strWarning);
        return;
    }
    var obj = items[0].get_dataItem();
    PageMethods.MailScheduleEdit(obj.Id, CallWebMethodSuccess, CallWebMethodFailed);
}
function MailScheduleEditCallback(results, context, methodName) {
    dialogMethod = "MailScheduleEdit";
    oWnd.setSize(600, 200);
    oWnd.setUrl("MailScheduleDetail.aspx");
    oWnd.show();
}

function MailScheduleUpdateCallback(results, context, methodName) {
    GetMailScheduleList();
}

function MailScheduleDelete() {
    var objList = GetMailScheduleSelect();
    if (objList.length == 0) {
        radalert(strMailScheduleNotChoose, 250, 100, strWarning);
        return;
    }
    radconfirm(strMenuConfirmDelete, MailScheduleDeleteConfirm, 250, 100, null, strWarning);
}
function MailScheduleDeleteConfirm(arg) {
    if (!arg)
        return;
    var objList = GetMailScheduleSelect();
    if (objList.length == 0) {
        radalert(strMailScheduleNotChoose, 250, 100, strWarning);
        return;
    }
    PageMethods.MailScheduleDelete(objList, CallWebMethodSuccess, CallWebMethodFailed);
}

function MailScheduleDeleteCallback(results, context, methodName) {
    GetMailScheduleList();
    grid.get_masterTableView().clearSelectedItems();
}


function pageLoad() {
    initialize();
    GetMailScheduleList();
}
//function MailSchedulePermission() {
//    var items = grid.get_masterTableView().get_selectedItems();
//    if (items.length == 0) {
//        PageMethods.GetMailSchedulePermission("-1", CallWebMethodSuccess, CallWebMethodFailed);
//        return;
//    }
//    var obj = items[0].get_dataItem();
//    PageMethods.GetMailSchedulePermission(obj.Id, CallWebMethodSuccess, CallWebMethodFailed);

//}
//function MailSchedulePermissionCallback(results, context, methodName) {
//    var ArrPermission = Sys.Serialization.JavaScriptSerializer.deserialize(results)
//    if (ArrPermission[0] == true) { // Có quyền administrator
//        $get("btnNew").style.display = "";
//        $get("btnEdit").style.display = "";
//        $get("btnCopy").style.display = "";
//        $get("btnDelete").style.display = "";
//        $get("btnEditMenu").style.display = "";
//        $get("btnMakeTopic").style.display = "";

//        $get("btnPermission").style.display = "";
//    }
//    else { // khong co quyen administrator
//        $get("btnNew").style.display = "none";
//        $get("btnEdit").style.display = "none";
//        $get("btnCopy").style.display = "none";
//        $get("btnDelete").style.display = "none";
//        if (ArrPermission[1] == false)
//            $get("btnEditMenu").style.display = "none";
//        else
//            $get("btnEditMenu").style.display = "";
//        $get("btnMakeTopic").style.display = "none";
//        $get("btnPermission").style.display = "none";
//    }
//}
//function row_select(sender, args) {
//    MailSchedulePermission();
//}
function gvMailSchedule_Command(sender, args) {
    args.set_cancel(true);
    sender.get_masterTableView().clearSelectedItems();
    var currentPageIndex = sender.get_masterTableView().get_currentPageIndex();
    var pageSize = sender.get_masterTableView().get_pageSize();
    var sortExpressions = sender.get_masterTableView().get_sortExpressions();

    PageMethods.GetMailScheduleList(currentPageIndex * pageSize, pageSize, sortExpressions.toString(), CallWebMethodSuccess);
    PageMethods.GetMailScheduleCount(CallWebMethodSuccess);
}
//function MeuMasterSecurity() {

//    var items = grid.get_selectedItems();
//    if (items.length == 0) {
//        radalert(strMailScheduleNotChoose, 250, 100, strWarning);
//        return;
//    }
//    var obj = items[0].get_dataItem();


//    PageMethods.MailScheduleSecurity(obj.Id, CallWebMethodSuccess, CallWebMethodFailed);
//}
//function MailScheduleSecurityCallback(results, context, methodName) {
//    dialogMethod = "MailSchedulePermission";
//    oWnd.setSize(600, 230);
//    oWnd.setUrl("MailScheduleSecurity.aspx");
//    oWnd.show();
//}
function RadWindowMailScheduleClose(sender, args) {
    if (!args.get_argument())
        return;
    switch (dialogMethod) {
        case "MailScheduleNew":
            {
                PageMethods.MailScheduleAdd(CallWebMethodSuccess, CallWebMethodFailed);
            }
            break;
        case "MailScheduleEdit":
            {
                PageMethods.MailScheduleUpdate(CallWebMethodSuccess, CallWebMethodFailed);
            }
            break;        
    }
}         