var oWnd;
var strArticleNotChoose;
var strWarning;
var strInformation;
var grid;

var strArticleChangeTopicPrimaryOnlyOne;
var strArticleConfirmDelete;
var strArticleUpdateDescriptionSuccess;
var strArticleUpdateContentSuccess;
var strArticleUpdateTopicSuccess;
var strArticleChangeTopicPrimarySuccess;
var dialogMethod;
var flag = 1;
function CallWebMethodSuccess(results, context, methodName) {
    switch (methodName) {
        case "GetArticleCommentNewList":
            {
                GetArticleCommentNewListCallback(results, context, methodName);
            }
            break;
        case "GetArticleCommentNewListCount":
            {
                GetArticleCommentNewListCountCallback(results, context, methodName);
            }
            break;
        case "GetCommentList":
            {
                GetCommentListCallback(results, context, methodName);
            }
            break;
        case "GetPermission":
            {
                SecurityUICallback(results, context, methodName);
            }
            break;
    }
}
function GetArticleSelect() {
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


function GetArticleCommentNewList() {

    var tableView = grid.get_masterTableView();
    var sortExpressions = tableView.get_sortExpressions();
    PageMethods.GetArticleCommentNewList(0, tableView.get_pageSize(), sortExpressions.toString(), CallWebMethodSuccess);
    PageMethods.GetArticleCommentNewListCount(CallWebMethodSuccess);
    grid.get_masterTableView().clearSelectedItems();
}

function GetArticleCommentNewListCallback(results, context, methodName) {
    var tableView = grid.get_masterTableView();
    tableView.set_dataSource(Sys.Serialization.JavaScriptSerializer.deserialize(results));
    tableView.dataBind();
}
function GetArticleCommentNewListCountCallback(results, context, methodName) {
    var tableView = grid.get_masterTableView();
    tableView.set_virtualItemCount(results);
}

function GetCommentList() {
    var items = grid.get_masterTableView().get_selectedItems();
    if (items.length == 0) {
        radalert(strArticleNotChoose, 250, 100, strWarning);
        return;
    }
    var obj = items[0].get_dataItem();
    PageMethods.GetCommentList(obj.Id, CallWebMethodSuccess, CallWebMethodFailed);
}
function GetCommentListCallback(resuls, context, methodName) {
    if (!resuls) {
        radalert("Bài viết chưa có đánh giá", 250, 100, strWarning);
        return;
    }
    dialogMethod = "ArticleComment";
    oWnd.setSize(1000, 500);
    oWnd.setUrl("ArticleComment.aspx");
    oWnd.show();

}
function gvArticle_Command(sender, args) {
    args.set_cancel(true);

    sender.get_masterTableView().clearSelectedItems();
    var currentPageIndex = sender.get_masterTableView().get_currentPageIndex();
    var pageSize = sender.get_masterTableView().get_pageSize();
    var sortExpressions = sender.get_masterTableView().get_sortExpressions();
    PageMethods.GetArticleCommentNewList(currentPageIndex * pageSize, pageSize, sortExpressions.toString(), CallWebMethodSuccess);
    PageMethods.GetArticleCommentNewListCount(CallWebMethodSuccess);
}
var btnComment;
function CreateElementCommentLinks() {
    btnComment = document.createElement("a");
    btnComment.setAttribute("href", "javascript:void(0)");
    btnComment.setAttribute("onClick", "GetCommentList()");
    btnComment.setAttribute("class", "Header");
    var commentName = document.createTextNode("[Quản lý câu hỏi]");
    btnComment.appendChild(commentName);
}

function pageLoad() {
    initialize();
    CreateElementCommentLinks();
    GetArticleCommentNewList();
    SecurityUI();

}

function RadWindowArticleClose(sender, args) {
    //if (!args.get_argument())
    //    return;
    switch (dialogMethod) {
        case "ArticleComment":
            {
                GetArticleCommentNewList();
            }
            break;
    }
}
var Article_comment = 59;

function SecurityUI() {
    PageMethods.GetPermission([Article_comment], CallWebMethodSuccess, CallWebMethodFailed);
}
function SecurityUICallback(results, context, methodName) {
    var arrPermission = Sys.Serialization.JavaScriptSerializer.deserialize(results);
    if (arrPermission[Article_comment]) {//btnTrashRestore
        document.getElementById("commentLinks").appendChild(btnComment);
    }
}