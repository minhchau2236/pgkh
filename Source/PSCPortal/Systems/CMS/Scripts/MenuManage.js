var strWarning;
var strMenuNotChoose;
var tree ;
var oWnd ;
var strMenuConfirmDelete;
var strMenuCanNotMoveUp;

var dialogMethod;
function CallWebMethodSuccess(results, context, methodName) {
    switch (methodName) {
        case "MenuNew":
            {
                MenuNewCallback(results, context, methodName);
            }
            break;
        case "MenuAdd":
            {
                MenuAddCallback(results, context, methodName);
            }
            break;
        case "MenuEdit":
            {
                MenuEditCallback(results, context, methodName);
            }
            break;
        case "MenuUpdate":
            {
                MenuUpdateCallback(results, context, methodName);
            }
            break;                
        case "MenuDelete":
            {
                MenuDeleteCallback(results, context, methodName);
            }
            break;
        case "MenuMoveUp":
            {
                MenuMoveUpCallback(results, context, methodName);
            }
            break;
        case "MenuMoveDown":
            {
                MenuMoveDownCallback(results, context, methodName);
            }
            break;                              
    }
}

function CallWebMethodFailed(results, context, methodName) {        
   // radalert(results._message, 250, 100, strWarning);
}        

function MenuNew() {
    PageMethods.MenuNew(CallWebMethodSuccess, CallWebMethodFailed);
}
function MenuNewCallback(results, context, methodName) {
    dialogMethod = "MenuNew";
    oWnd.setSize(800, 550);
    oWnd.setUrl("MenuDetail.aspx");
    oWnd.show();            
}
function MenuAddCallback(result, context, methodName) {
    var obj = Sys.Serialization.JavaScriptSerializer.deserialize(result);
    tree.trackChanges();
    var node = new Telerik.Web.UI.RadTreeNode();
    node.set_text(obj.Name);
    node.set_value(obj.Id);
    tree.get_nodes().add(node);
    tree.commitChanges();
}
function MenuMoveUp() {
    var curNode = tree.get_selectedNode();
    if (!curNode) {
        radalert(strMenuNotChoose, 250, 100, strWarning);
        return;
    }
    if (!curNode.get_previousNode()) {
        radalert(strMenuCanNotMoveUp, 250, 100, strWarning);
        return;
    }
    PageMethods.MenuMoveUp(curNode.get_value(), CallWebMethodSuccess, CallWebMethodFailed);
}
function MenuMoveUpCallback(results, context, methodName) {
    var curNode = tree.get_selectedNode();
    var nodeParent = curNode.get_parent();
    var countNode = nodeParent.get_nodes().get_count();
    var i;
    for (i = 0; i < countNode; i++)
        if (nodeParent.get_nodes().getNode(i).get_value() == curNode.get_value())
            break;
    nodeParent.get_nodes().removeAt(i);
    nodeParent.get_nodes().insert(i - 1, curNode);
}
function MenuMoveDown() {
    var curNode = tree.get_selectedNode();
    if (!curNode) {
        radalert(strMenuNotChoose, 250, 100, strWarning);
        return;
    }
    if (!curNode.get_nextNode()) {
        radalert(strMenuCanNotMoveUp, 250, 100, strWarning);
        return;
    }
    PageMethods.MenuMoveDown(curNode.get_value(), CallWebMethodSuccess, CallWebMethodFailed);
}
function MenuMoveDownCallback(results, context, methodName) {
    var curNode = tree.get_selectedNode();
    var nodeParent = curNode.get_parent();
    var countNode = nodeParent.get_nodes().get_count();
    var i;
    for (i = 0; i < countNode; i++)
        if (nodeParent.get_nodes().getNode(i).get_value() == curNode.get_value())
        break;
    nodeParent.get_nodes().removeAt(i);
    nodeParent.get_nodes().insert(i + 1, curNode);
}

function MenuEdit() {
    var curNode = tree.get_selectedNode();      
    if (!curNode) {
        radalert(strMenuNotChoose, 250, 100, strWarning);
        return;
    }       
    PageMethods.MenuEdit(curNode.get_value(), CallWebMethodSuccess, CallWebMethodFailed);
}
function MenuEditCallback(results, context, methodName) {
    dialogMethod = "MenuEdit";
    //oWnd.setSize(800, 550);//Ngọc - 16122015
    oWnd.setUrl("MenuDetail.aspx");
    oWnd.show();
    oWnd.Maximize();
}

function MenuUpdateCallback(results, context, methodName) {
    var obj = Sys.Serialization.JavaScriptSerializer.deserialize(results);
    var curNode = tree.get_selectedNode();
    tree.trackChanges();
    curNode.set_text(obj.Name);
    tree.commitChanges();
}

function MenuDelete() {
    var curNode = tree.get_selectedNode();
    if (!curNode) {
        radalert(strMenuNotChoose, 250, 100, strWarning);
        return;
    }
    radconfirm(strMenuConfirmDelete, MenuDeleteConfirm, 250, 100, null, strWarning);         			                   
}
function MenuDeleteConfirm(arg) {
    if (!arg)
        return;
    var curNode = tree.get_selectedNode();
    if (!curNode) {
        radalert(strMenuNotChoose, 250, 100, strWarning);
        return;
    }
    PageMethods.MenuDelete(curNode.get_value(), CallWebMethodSuccess, CallWebMethodFailed);
}
function MenuDeleteCallback(results, context, methodName) {
    var curNode = tree.get_selectedNode();
    tree.trackChanges();
    curNode.get_parent().get_nodes().remove(curNode);
    tree.commitChanges();
}		
function ChangeParent(sender, eventArgs) {
    var nodeSrc = eventArgs.get_sourceNode();
    var nodeDest = eventArgs.get_destNode();
    nodeSrc.get_parent().get_nodes().remove(nodeSrc);
    nodeDest.get_nodes().add(nodeSrc);
    PageMethods.ChangeParent(eventArgs.get_sourceNode().get_value(), eventArgs.get_destNode().get_value(), CallWebMethodSuccess, CallWebMethodFailed);
}
function pageLoad() {
    initialize();
}
function RadWindowMenuClose(sender, args) {
    if (!args.get_argument())
        return;
    switch (dialogMethod) {
        case "MenuNew":
            {
                PageMethods.MenuAdd(CallWebMethodSuccess, CallWebMethodFailed);
            }
            break;
        case "MenuEdit":
            {
                PageMethods.MenuUpdate(CallWebMethodSuccess, CallWebMethodFailed);
            }
            break;
    }
}        
function DisplayMenuMaster(){
    window.location.assign("http://"+window.location.host+"/Systems/CMS/MenuMasterManage.aspx");
}       
