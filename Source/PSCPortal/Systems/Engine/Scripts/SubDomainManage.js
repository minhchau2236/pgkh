var grid;
var strDomainNotChoose;
var strWarning;
var strInformation;
var strMenuConfirmDelete;
var flag = 1;
var strDomainDeleteSuccess;
var dialogMethod;
function CallWebMethodSuccess(results, context, methodName) {
    switch (methodName) {
        case "GetSubDomainList":
            {
                GetSubDomainListCallback(results, context, methodName);
            }
            break;
        case "GetSubDomainCount":
            {
                GetSubDomainCountCallback(results, context, methodName);
            }
            break;
        case "SubDomainNew":
            {
                SubDomainNewCallback(results, context, methodName);
            }
            break;
        case "SubDomainAdd":
            {
                SubDomainAddCallback(results, context, methodName);
            }
            break;
        case "SubDomainEdit":
            {
                SubDomainEditCallback(results, context, methodName);
            }
            break;
        case "SubDomainUpdate":
            {
                SubDomainUpdateCallback(results, context, methodName);
            }
            break;
        case "SubDomainDelete":
            {
                SubDomainDeleteCallback(results, context, methodName);
            }
            break;
        case "SubDomainConfig":
            {
                SubDomainConfigCallback(results, context, methodName);
            }
            break;
    }
}
function GetSubDomainSelect() {
    var items = grid.get_masterTableView().get_selectedItems();
    var objList = new Array();
    for (var i = 0; i < items.length; i++) {
        Array.add(objList, items[i].get_dataItem().Id);
    }
    return objList;
}
function SubDomainSelect(sender, eventArgs) {
    flag = 1;
    GetSubDomainList();
}
function GetSubDomainList() {    
    var tableView = grid.get_masterTableView();
    var sortExpressions = tableView.get_sortExpressions();
    PageMethods.GetSubDomainList(0, tableView.get_pageSize(), sortExpressions.toString(), CallWebMethodSuccess);
    PageMethods.GetSubDomainCount(CallWebMethodSuccess);
    grid.get_masterTableView().clearSelectedItems();
}
function GetSubDomainListCallback(results, context, methodName) {
    var tableView = grid.get_masterTableView();
    tableView.set_dataSource(Sys.Serialization.JavaScriptSerializer.deserialize(results));
    tableView.dataBind();
}
function GetSubDomainCountCallback(results, context, methodName) {
    var tableView = grid.get_masterTableView();
    tableView.set_virtualItemCount(results);
}
function gvSubDomain_Command(sender, args) {
    args.set_cancel(true);
    sender.get_masterTableView().clearSelectedItems();
    var currentPageIndex = sender.get_masterTableView().get_currentPageIndex();
    var pageSize = sender.get_masterTableView().get_pageSize();
    var sortExpressions = sender.get_masterTableView().get_sortExpressions();
    PageMethods.GetSubDomainList(currentPageIndex * pageSize, pageSize, sortExpressions.toString(), CallWebMethodSuccess);
    PageMethods.GetSubDomainCount(CallWebMethodSuccess);
}
function CallWebMethodFailed(results, context, methodName) {
    radalert(results._message, 250, 100, strWarning);
}
function SubDomainNew() {
    PageMethods.SubDomainNew(CallWebMethodSuccess, CallWebMethodFailed);
}
function SubDomainNewCallback(results, context, methodName) {
    dialogMethod = "SubDomainNew";
    oWnd.setSize(1050, 500);
    oWnd.setUrl("SubDomainDetail.aspx");
    oWnd.show();
}
function SubDomainAddCallback(result, context, methodName) {

    GetSubDomainList();
}

function SubDomainEdit() {
    var items = grid.get_masterTableView().get_selectedItems();
    if (items.length == 0) {
        radalert(strDomainNotChoose, 250, 100, strWarning);
        return;
    }
    var obj = items[0].get_dataItem();
    PageMethods.SubDomainEdit(obj.Id, CallWebMethodSuccess, CallWebMethodFailed);
}
function SubDomainEditCallback(results, context, methodName) {
    dialogMethod = "SunDomainEdit";
    oWnd.setSize(1050, 500);
    oWnd.setUrl("SubDomainDetail.aspx");
    oWnd.show();
}
function SubDomainUpdateCallback(results, context, methodName) {
    GetSubDomainList();
}
function SubDomainDelete() {
    var objList = GetSubDomainSelect();
    if (objList.length == 0) {
        radalert(strDomainNotChoose, 250, 100, strWarning);
        return;
    }
    radconfirm(strMenuConfirmDelete, SunDomainDeleteConfirm, 250, 100, null, strWarning);
}
function SubDomainConfig() {
    var items = grid.get_masterTableView().get_selectedItems();
    if (items.length == 0) {
        radalert(strDomainNotChoose, 250, 100, strWarning);
        return;
    }
    var obj = items[0].get_dataItem();
    PageMethods.SubDomainConfig(obj.Id, CallWebMethodSuccess, CallWebMethodFailed);
}
function SubDomainConfigCallback(results, context, methodName) {
    dialogMethod = "SunDomainConfig";
    oWnd.setSize(840, 600);
    oWnd.setUrl("SubDomainConfig.aspx");
    oWnd.show();
}
function pageLoad() {
    initialize();
    GetSubDomainList();
}
function SunDomainDeleteConfirm(args) {
    if (!args)
        return;
    var objList = GetSubDomainSelect();
    if (objList.length == 0) {
        radalert(strDomainNotChoose, 250, 100, "Warning");
        return;
    }
    PageMethods.SubDomainDelete(objList, CallWebMethodSuccess, CallWebMethodFailed);
}
function SubDomainDeleteCallback(results, context, methodName) {
    GetSubDomainList();
}
function RadWindowSubDomianClose(sender, args) {
    if (!args.get_argument())
        return;
    switch (dialogMethod) {
        case "SubDomainNew":
            {
                PageMethods.SubDomainAdd(CallWebMethodSuccess, CallWebMethodFailed);
            }
            break;
        case "SunDomainEdit":
            {
                PageMethods.SubDomainUpdate(CallWebMethodSuccess, CallWebMethodFailed);
            }
            break;
    }
} 