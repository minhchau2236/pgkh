var oWndComment;
var strArticleNotChoose;
var strWarning;
var strInformation;
var gridComment;
var strArticleConfirmDelete;
var dialogMethod;
var flag = 1;
function CallWebMethodSuccess(results, context, methodName) {
    switch (methodName) {
        case "GetArticleList":
            {
                GetArticleListCallback(results, context, methodName);
            }
            break;
        case "GetArticleCount":
            {
                GetArticleCountCallback(results, context, methodName);
            }
           break;
        case "ArticleEdit":
            {
                ArticleEditCallback(results, context, methodName);
            }
            break;
        case "ArticleUpdate":
            {
                ArticleUpdateCallback(results, context, methodName);
            }
            break;
        case "ArticleDelete":
            {
                ArticleDeleteCallback(results, context, methodName);
            }
            break;
        case "ArticlePublish":
            {
                ArticlePublishCallback(results, context, methodName);
            }
            break;
        case "ArticleUnPublish":
            {
                ArticleUnPublishCallback(results, context, methodName);
            }
            break;             
    }
}


function CallWebMethodFailed(results, context, methodName) {
    radalert(results._message, 250, 100, strWarning);
}
function GetArticleList() {
    var tableView = gridComment.get_masterTableView();
    var sortExpressions = tableView.get_sortExpressions();
    PageMethods.GetArticleList(0,tableView.get_pageSize(), sortExpressions.toString(), CallWebMethodSuccess);
    PageMethods.GetArticleCount(CallWebMethodSuccess);
    gridComment.get_masterTableView().clearSelectedItems();
}
function GetArticleListCallback(results, context, methodName) {
    var tableView = gridComment.get_masterTableView();
    tableView.set_dataSource(Sys.Serialization.JavaScriptSerializer.deserialize(results));
    tableView.dataBind();
}
function GetArticleCountCallback(results, context, methodName) {
    var tableView = gridComment.get_masterTableView();
    tableView.set_virtualItemCount(results);
}
function ArticleEdit() {
    var items = gridComment.get_masterTableView().get_selectedItems();
    if (items.length == 0) {
        radalert(strArticleNotChoose, 250, 100, strWarning);
        return;
    }
    var obj = items[0].get_dataItem();
    PageMethods.ArticleEdit(obj.ID, CallWebMethodSuccess, CallWebMethodFailed);
}
function ArticleEditCallback(results, context, methodName) {
    dialogMethod = "ArticleEdit";
    oWndComment.setSize(500, 350);
    oWndComment.setUrl("ArticleCommentDetail.aspx");
    oWndComment.show();
    
}

function ArticleUpdateCallback(results, context, methodName) {
    GetArticleList();
}

function ArticleDelete() {
    var objList = GetArticleSelect();
    if (objList.length == 0) {
        radalert(strArticleNotChoose, 250, 100, strWarning);
        return;
    }
    radconfirm(strArticleConfirmDelete, ArticleDeleteConfirm, 250, 100, null, strWarning);
}
function ArticleDeleteConfirm(args) {
    if (!args)
        return;
    var objList = GetArticleSelect();
    if (objList.length == 0) {
        radalert(strArticleNotChoose, 250, 100, "Warning");
        return;
    }
    PageMethods.ArticleDelete(objList, CallWebMethodSuccess, CallWebMethodFailed);
}
function ArticleDeleteCallback(results, context, methodName) {
    GetArticleList();
}
function GetArticleSelect() {
    var items = gridComment.get_masterTableView().get_selectedItems();
    var objList = new Array();
    for (var i = 0; i < items.length; i++) {
        Array.add(objList, items[i].get_dataItem().ID);
    }
    return objList;
}
function ArticlePublish() {
    var objList = GetArticleSelect();
    if (objList.length == 0) {
        radalert(strArticleNotChoose, 250, 100, strWarning);
        return;
    }
    PageMethods.ArticlePublish(objList, CallWebMethodSuccess, CallWebMethodFailed);
}
function ArticlePublishCallback(results, context, methodName) {
    GetArticleList();
}
function ArticleUnPublish() {
    var objList = GetArticleSelect();
    if (objList.length == 0) {
        radalert(strArticleNotChoose, 250, 100, strWarning);
        return;
    }
    PageMethods.ArticleUnPublish(objList, CallWebMethodSuccess, CallWebMethodFailed);
}
function ArticleUnPublishCallback() {
    GetArticleList();
}
function gvArticleComment_Command(sender, args) {
    args.set_cancel(true);
    sender.get_masterTableView().clearSelectedItems();
    var currentPageIndex = sender.get_masterTableView().get_currentPageIndex();
    var pageSize = sender.get_masterTableView().get_pageSize();
    var sortExpressions = sender.get_masterTableView().get_sortExpressions();
    PageMethods.GetArticleList(currentPageIndex * pageSize, pageSize, sortExpressions.toString(), CallWebMethodSuccess);
    PageMethods.GetArticleCount(CallWebMethodSuccess);
}

function pageLoad() {
    initialize();
    GetArticleList();
}


function RadWindowArticleCommentClose(sender, args) {
    if (!args.get_argument())
        return;
    switch (dialogMethod) {
        case "ArticleEdit":
            {
                PageMethods.ArticleUpdate(CallWebMethodSuccess, CallWebMethodFailed);
            }
            break;        
    }
}         