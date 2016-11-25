var grid;
var tree;
var strWarning;
var strDocumentNotChoose;
function CallWebMethodSuccess(results, context, methodName) {
    switch (methodName) {
        case "GetDocumentList":
            {
                GetDocumentListCallback(results, context, methodName);
            }
            break;
        case "GetDocumentCount":
            {
                GetDocumentCountCallback(results, context, methodName);
            }
            break;
    }
}
function GetDocumentSelect() {
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
function DocumentTypeSelect(sender, eventArgs)
{
    GetDocumentList();
}
function GetDocumentList() {
    var curNode = tree.get_selectedNode();   
    var tableView = grid.get_masterTableView();
    var sortExpressions = tableView.get_sortExpressions();
    PageMethods.GetDocumentList(curNode.get_value(),0, tableView.get_pageSize(), sortExpressions.toString(), CallWebMethodSuccess);
    PageMethods.GetDocumentCount(CallWebMethodSuccess);
}
function GetDocumentListCallback(results, context, methodName) {
    var tableView = grid.get_masterTableView();
    tableView.set_dataSource(Sys.Serialization.JavaScriptSerializer.deserialize(results));
    tableView.dataBind();
}

function GetDocumentCountCallback(results, context, methodName) {
    var tableView = grid.get_masterTableView();
    tableView.set_virtualItemCount(results);
}
function gvDocument_Command(sender, args) {
    args.set_cancel(true);
    sender.get_masterTableView().clearSelectedItems();
    var currentPageIndex = sender.get_masterTableView().get_currentPageIndex();
    var pageSize = sender.get_masterTableView().get_pageSize();
    var sortExpressions = sender.get_masterTableView().get_sortExpressions();

    PageMethods.GetDocumentList(currentPageIndex * pageSize, pageSize, sortExpressions.toString(), CallWebMethodSuccess);
    PageMethods.GetDocumentCount(CallWebMethodSuccess);
}  
function Save()
{
    window.close();
    var items = grid.get_selectedItems();          
     if (items.length == 0) 
     {            
        radalert(strDocumentNotChoose, 250, 100, strWarning);
        return;
        }
    var obj = items[0].get_dataItem();
    window.returnValue = obj.Id ;
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
        GetDocumentList();
    }
    
 }