var grid;
var tree;
var strWarning;
var strArticleNotChoose;
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
function TopicSelect(sender, eventArgs)
{
    GetArticleList();
}
function GetArticleList() {
    var curNode = tree.get_selectedNode();   
    var tableView = grid.get_masterTableView();
    var sortExpressions = tableView.get_sortExpressions();
    PageMethods.GetArticleList(curNode.get_value(),0, tableView.get_pageSize(), sortExpressions.toString(), CallWebMethodSuccess);
    PageMethods.GetArticleCount(CallWebMethodSuccess);
}
function GetArticleListCallback(results, context, methodName) {
    var tableView = grid.get_masterTableView();
    tableView.set_dataSource(Sys.Serialization.JavaScriptSerializer.deserialize(results));
    tableView.dataBind();
}

function GetArticleCountCallback(results, context, methodName) {
    var tableView = grid.get_masterTableView();
    tableView.set_virtualItemCount(results);
}
function gvArticle_Command(sender, args) {
    args.set_cancel(true);
    var curNode = tree.get_selectedNode();
    sender.get_masterTableView().clearSelectedItems();
    var currentPageIndex = sender.get_masterTableView().get_currentPageIndex();
    var pageSize = sender.get_masterTableView().get_pageSize();
    var sortExpressions = sender.get_masterTableView().get_sortExpressions();

    PageMethods.GetArticleList(curNode.get_value(), currentPageIndex * pageSize, pageSize, sortExpressions.toString(), CallWebMethodSuccess);
    //PageMethods.GetArticleCount(CallWebMethodSuccess);
}  
function Save()
{
    //window.close();
    var items = grid.get_selectedItems();          
     if (items.length == 0) 
     {            
        radalert(strArticleNotChoose, 250, 100, strWarning);
        return;
        }
    var obj = items[0].get_dataItem();
    //window.returnValue = obj.Id;
    var oWnd = GetRadWindow();
    oWnd.close(obj.Id);
}
function Cancel()
{
    window.close();
}
 function pageLoad()
 {
    initialize();
    var firstNode=tree.get_nodes().getNode(0);
    if(firstNode=tree.get_nodes().getNode(0)){
        firstNode.select();
        GetArticleList();
    }
    
 }