var grid;
var strArticleNotChoose;
var strWarning;
var oWnd;
var strArticleConfirmDelete;
var TRASH_RESTORE = 1;
var TRASH_DELETE = 2;

var dialogMethod;
function CallWebMethodSuccess(results, context, methodName) {
    switch (methodName) {
        case "GetArticleTrashList":
            {
                GetArticleTrashListCallback(results, context, methodName);
            }
            break;
        case "GetArticleTrashCount":
            {
                GetArticleTrashCountCallback(results, context, methodName);
            }
            break;
        case "ArticleTrashRestore":
            {
                ArticleTrashRestoreCallback(results, context, methodName);
            }
            break;
        case "ArticleTrashDelete":
            {
                ArticleTrashDeleteCallback(results, context, methodName);
            }
            break;
        case "GetPermission":
            {
                SecurityUICallback(results, context, methodName);
            }
            break;                   
    }
}

function CallWebMethodFailed(results, context, methodName) {
   // alert(results._message);
}

function GetArticleTrashSelect() {
    var items = grid.get_masterTableView().get_selectedItems();            
    var objList = new Array();
    for (var i = 0; i < items.length; i++) {
        Array.add(objList, items[i].get_dataItem().Id);
    }
    return objList;
}

function GetArticleTrashList() {
    var tableView =grid.get_masterTableView();
    var sortExpressions = tableView.get_sortExpressions();
    PageMethods.GetArticleTrashList(0, tableView.get_pageSize(), sortExpressions.toString(), CallWebMethodSuccess);
    PageMethods.GetArticleTrashCount(CallWebMethodSuccess);
    grid.get_masterTableView().clearSelectedItems();
}
function GetArticleTrashListCallback(results, context, methodName) {
    var tableView = grid.get_masterTableView();
    tableView.set_dataSource(Sys.Serialization.JavaScriptSerializer.deserialize(results));
    tableView.dataBind();
}

function GetArticleTrashCountCallback(results, context, methodName) {
    var tableView = grid.get_masterTableView();
    tableView.set_virtualItemCount(results);
}

var btnTrashResote;
var btnTrashDelete;
function CreateElementTrashLinks() {
    btnTrashResote = document.createElement("a");
    btnTrashResote.setAttribute("href", "javascript:void(0)");
    btnTrashResote.setAttribute("onClick", "ArticleTrashRestore()");
    btnTrashResote.setAttribute("class", "Header");
    var restoreName = document.createTextNode("[Phục hồi]");
    btnTrashResote.appendChild(restoreName);

    btnTrashDelete = document.createElement("a");
    btnTrashDelete.setAttribute("href", "javascript:void(0)");
    btnTrashDelete.setAttribute("onClick", "ArticleTrashDelete()");
    btnTrashDelete.setAttribute("class", "Header");
    var deleteName = document.createTextNode("[Xóa]");
    btnTrashDelete.appendChild(deleteName);
}

function pageLoad() {    
    initialize();
    CreateElementTrashLinks();
    SecurityUI();
    GetArticleTrashList();    
}
function gvArticleTrash_Command(sender, args) {
    args.set_cancel(true);
    sender.get_masterTableView().clearSelectedItems();
    var currentPageIndex = sender.get_masterTableView().get_currentPageIndex();
    var pageSize = sender.get_masterTableView().get_pageSize();
    var sortExpressions = sender.get_masterTableView().get_sortExpressions();
    PageMethods.GetArticleTrashList(currentPageIndex * pageSize, pageSize, sortExpressions.toString(), CallWebMethodSuccess);
    PageMethods.GetArticleTrashCount(CallWebMethodSuccess);
}
  
function ArticleTrashRestore(){
    var objList = GetArticleTrashSelect();
    if (objList.length == 0) {
        radalert(strArticleNotChoose, 250, 100, strWarning);
        return;
    }  
    dialogMethod = "ArticleTrashRestore";
    oWnd.setSize(600, 150);
    oWnd.setUrl("ChooseTopicForArticle.aspx");
    oWnd.show();   
}
 
function ArticleTrashRestoreCallback(){
    GetArticleTrashList();
}

function ArticleTrashDelete(){
    var objList = GetArticleTrashSelect();
    if (objList.length == 0) {
        radalert(strArticleNotChoose, 250, 100, strWarning);
        return;
    }
    radconfirm(strArticleConfirmDelete, ArticleTrashDeleteConfirm, 250, 100, null, strWarning);         
}
function ArticleTrashDeleteConfirm(arg) {
    if (!arg)
        return;
    var objList = GetArticleTrashSelect();
    PageMethods.ArticleTrashDelete(objList, CallWebMethodSuccess, CallWebMethodFailed); 
}
function ArticleTrashDeleteCallback(){
    GetArticleTrashList();
}
function RadWindowTrashClose(sender, args) {
    if (!args.get_argument())
        return;
    switch (dialogMethod) {
        case "ArticleTrashRestore":
            {
                var objList = GetArticleTrashSelect();
                if (objList.length == 0) {
                    radalert(strArticleNotChoose, 250, 100, strWarning);
                    return;
                }  
                PageMethods.ArticleTrashRestore(objList, CallWebMethodSuccess, CallWebMethodFailed);
            }
            break;                
    }
}
function SecurityUI() {
    PageMethods.GetPermission([TRASH_RESTORE, TRASH_DELETE], CallWebMethodSuccess, CallWebMethodFailed);
}
function SecurityUICallback(results, context, methodName) {
    var arrPermission = Sys.Serialization.JavaScriptSerializer.deserialize(results);
    if (arrPermission[TRASH_RESTORE]) {
        document.getElementById("trashLinks").appendChild(btnTrashResote);
    }

    if (arrPermission[TRASH_DELETE]) {
        document.getElementById("trashLinks").appendChild(btnTrashDelete);
    }
}
