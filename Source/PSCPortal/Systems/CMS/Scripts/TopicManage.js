var dialogMethod;
var oWnd;
var tree;
var strTopicNotChoose;
var strWarning;
var strTopicEditTemplateSuccess;
var strInformation;
var strTopicConfirmDelete;
var strTopicDeleteSuccess;
var strTopicCanNotMoveUp;
var strTopicCannotMoveDown;
var btnTopicAdd;
var btnTopicEdit;
var btnTopicCopy;
var btnTopicMakeMenu;
var btnTopicDelete;
var btnTopicSecurity;
var btnTopicChangeRoot;
var btnTopicLoginEdit;
var btnTopicMoveUp;
var btnTopicMoveDown;
function CallWebMethodSuccess(results, context, methodName) {
    switch (methodName) {
        case "TopicNew":
            {
                TopicNewCallback(results, context, methodName);
            }
            break;
        case "TopicAdd":
            {
                TopicAddCallback(results, context, methodName);
            }
            break;
        case "TopicEdit":
            {
                TopicEditCallback(results, context, methodName);
            }
            break;
        case "TopicUpdate":
            {
                TopicUpdateCallback(results, context, methodName);
            }
            break;
        case "TopicDelete":
            {
                TopicDeleteCallback(results, context, methodName);
            }
            break;
        case "LoadTopicAuthentication":
            {
                LoadRoleAuthentication();
            }
            break;
        case "GetRolesForPermission":
            {
                LoadRoleAuthenticationCallback(results, context, methodName);
            }
            break;
        case "AddAuthenticationRole":
            {
                AddAuthenticationRoleCallBack(results, context, methodName);
            }
            break;
        case "GetRolesAvailableForPermission":
            {
                LoadRoleAvailableCallback(results, context, methodName);
            }
            break;
        case "RemoveAuthenticationRole":
            {
                RemoveAuthenticationRoleCallback(results, context, methodName);
            }
            break;
        case "TopicSecurity":
            {
                TopicSecurityCallback(results, context, methodName);
            }
            break;
        case "TopicEditContentTemplate":
            {
                TopicEditContentTemplateCallback(results, context, methodName);
            }
            break;
        case "TopicCopy":
            {
                TopicCopyCallback(results, context, methodName);
            }
            break;
        case "TopicCopyDo":
            {
                TopicCopyDoCallBack();
            }
            break;
        case "TopicMakeMenu":
            {
                TopicMakeMenuCallback(results, context, methodName);
            }
            break;
        case "TopicMakeMenuDo":
            {
                TopicMakeMenuDoCallBack();
            }
            break;
        case "GetPermission":
            {
                SecurityUICallback(results, context, methodName);
            }
            break;
        case "TopicLoginEdit":
            {
                TopicLoginEditCallback(results, context, methodName);
            }
            break;
        case "TopicMoveUp":
            {
                TopicMoveUpCallback(results, context, methodName);
            }
            break;
        case "TopicMoveDown":
            {
                TopicMoveDownCallback(results, context, methodName);
            }
            break;
    }
}

function CallWebMethodFailed(results, context, methodName) {
    // alert(results._message);
}

function TopicNew() {
    PageMethods.TopicNew(CallWebMethodSuccess, CallWebMethodFailed);
}
function ChangeParent(sender, eventArgs) {
    var nodeSrc = eventArgs.get_sourceNode();
    var nodeDest = eventArgs.get_destNode();
    nodeSrc.get_parent().get_nodes().remove(nodeSrc);
    nodeDest.get_nodes().add(nodeSrc);
    PageMethods.ChangeParent(eventArgs.get_sourceNode().get_value(), eventArgs.get_destNode().get_value(), CallWebMethodSuccess, CallWebMethodFailed);
}
function TopicChangeRoot() {
    var curNode = tree.get_selectedNode();
    curNode.get_parent().get_nodes().remove(curNode);
    tree.get_nodes().add(curNode);
    PageMethods.ChangeRoot(curNode.get_value(), CallWebMethodSuccess, CallWebMethodFailed);
}
function TopicNewCallback(results, context, methodName) {
    dialogMethod = "TopicNew";
    oWnd.setSize(815, 280);
    oWnd.setUrl("TopicDetail.aspx");
    oWnd.show();
}
function TopicAddCallback(result, context, methodName) {

    var obj = Sys.Serialization.JavaScriptSerializer.deserialize(result);
    tree.trackChanges();
    var node = new Telerik.Web.UI.RadTreeNode();
    node.set_text(obj.Name);
    node.set_value(obj.Id);
    tree.get_nodes().add(node);
    tree.commitChanges();
}
function TopicEditCallback(results, context, methodName) {
    dialogMethod = "TopicEdit";
    oWnd.setSize(815, 280);
    oWnd.setUrl("TopicDetail.aspx");
    oWnd.show();
}
function TopicEditContentTemplateCallback(results, context, methodName) {
    dialogMethod = "TopicEditContentTemplate";
    oWnd.setSize(800, 600);
    oWnd.setUrl("TopicEditContentTemplate.aspx");
    oWnd.show();
}
function TopicSecurityCallback(results, context, methodName) {
    dialogMethod = "TopicSecurity";
    oWnd.setSize(885, 300);
    oWnd.setUrl("TopicSecurity.aspx");
    oWnd.show();
}

function TopicUpdateCallback(results, context, methodName) {
    var obj = Sys.Serialization.JavaScriptSerializer.deserialize(results);
    var curNode = tree.get_selectedNode();
    tree.trackChanges();
    curNode.set_text(obj.Name);
    tree.commitChanges();
}
function TopicDeleteConfirm(arg) {
    if (!arg)
        return;
    var curNode = tree.get_selectedNode();
    PageMethods.TopicDelete(curNode.get_value(), CallWebMethodSuccess, CallWebMethodFailed);
}
function TopicEdit() {
    var curNode = tree.get_selectedNode();
    if (!curNode) {
        radalert(strTopicNotChoose, 250, 100, strWarning);
        return;
    }
    PageMethods.TopicEdit(curNode.get_value(), CallWebMethodSuccess, CallWebMethodFailed);
}

function TopicEditContentTemplate() {
    var curNode = tree.get_selectedNode();
    if (!curNode) {
        radalert(strTopicNotChoose, 250, 100, strWarning);
        return;
    }
    PageMethods.TopicEditContentTemplate(curNode.get_value(), CallWebMethodSuccess, CallWebMethodFailed);
}


function TopicSecurity() {
    var curNode = tree.get_selectedNode();
    if (!curNode) {
        radalert(strTopicNotChoose, 250, 100, strWarning);
        return;
    }
    PageMethods.TopicSecurity(curNode.get_value(), CallWebMethodSuccess, CallWebMethodFailed);
}


function TopicDelete() {
    var curNode = tree.get_selectedNode();
    if (!curNode) {
        radalert(strTopicNotChoose, 250, 100, strWarning);
        return;
    }
    radconfirm(strTopicConfirmDelete, TopicDeleteConfirm, 250, 100, null, strWarning);
}

function TopicDeleteCallback(results, context, methodName) {
    radalert(strTopicDeleteSuccess, 250, 100, strInformation);
    var curNode = tree.get_selectedNode();
    tree.trackChanges();
    curNode.get_parent().get_nodes().remove(curNode);
    tree.commitChanges();
}
function TopicCopy() {

    var curNode = tree.get_selectedNode();
    if (!curNode) {
        radalert(strTopicNotChoose, 250, 100, strWarning);
        return;
    }
    PageMethods.TopicCopy(CallWebMethodSuccess, CallWebMethodFailed);
}
function TopicCopyCallback(results, context, methodName) {
    dialogMethod = "TopicCopy";
    oWnd.setSize(815, 280);
    oWnd.setUrl("TopicDetail.aspx");
    oWnd.show();
}
function TopicCopyDoCallBack() {

    window.location.assign("http://" + window.location.host + "/Systems/CMS/TopicManage.aspx");
}
function TopicMakeMenu() {
    var curNode = tree.get_selectedNode();
    if (!curNode) {
        radalert(strTopicNotChoose, 250, 100, strWarning);
        return;
    }
    PageMethods.TopicMakeMenu(CallWebMethodSuccess, CallWebMethodFailed);
}
function TopicMakeMenuCallback(results, context, methodName) {
    dialogMethod = "TopicMakeMenu";
    oWnd.setSize(815, 230);
    oWnd.setUrl("MenuMasterDetail.aspx");
    oWnd.show();
}
function TopicMakeMenuDoCallBack() {
    radalert("Chuyển chuyên mục thành menu thành công", 250, 100, strWarning);
}
function TopicLoginEdit() {
    var curNode = tree.get_selectedNode();
    if (!curNode) {
        radalert(strTopicNotChoose, 250, 100, strWarning);
        return;
    }
    PageMethods.TopicLoginEdit(curNode.get_value(), CallWebMethodSuccess, CallWebMethodFailed);
}
function TopicLoginEditCallback(results, context, methodName) {
    dialogMethod = "TopicLoginEdit";
    oWnd.setSize(815, 370);
    oWnd.setUrl("TopicLoginDetail.aspx");
    oWnd.show();
}

function TopicMoveUp() {
    var curNode = tree.get_selectedNode();
    if (!curNode) {
        radalert(strTopicNotChoose, 250, 100, strWarning);
        return;
    }
    if (!curNode.get_previousNode()) {
        radalert(strTopicCanNotMoveUp, 250, 100, strWarning);
        return;
    }
    PageMethods.TopicMoveUp(curNode.get_value(), CallWebMethodSuccess, CallWebMethodFailed);
}

function TopicMoveUpCallback(results, context, methodName) {
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

function TopicMoveDown() {
    var curNode = tree.get_selectedNode();
    if (!curNode) {
        radalert(strTopicNotChoose, 250, 100, strWarning);
        return;
    }
    if (!curNode.get_nextNode()) {
        radalert(strTopicCannotMoveDown, 250, 100, strWarning);
        return;
    }
    PageMethods.TopicMoveDown(curNode.get_value(), CallWebMethodSuccess, CallWebMethodFailed);
}

function TopicMoveDownCallback(results, context, methodName) {
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

function RadWindowTopicClose(sender, args) {
    if (!args.get_argument())
        return;
    switch (dialogMethod) {
        case "TopicNew":
            {
                PageMethods.TopicAdd(CallWebMethodSuccess, CallWebMethodFailed);
            }
            break;
        case "TopicEdit":
            {
                PageMethods.TopicUpdate(CallWebMethodSuccess, CallWebMethodFailed);
            }
            break;
        case "TopicSecurity":
            {
            }
            break;
        case "TopicEditContentTemplate":
            {
                radalert(strTopicEditTemplateSuccess, 250, 100, strInformation);
            }
            break;
        case "TopicCopy":
            {
                var curNode = tree.get_selectedNode();
                if (!curNode) {
                    radalert(strTopicNotChoose, 250, 100, strWarning);
                    return;
                }
                PageMethods.TopicCopyDo(curNode.get_value(), CallWebMethodSuccess, CallWebMethodFailed);
            }
            break;
        case "TopicMakeMenu":
            {
                var curNode = tree.get_selectedNode();
                if (!curNode) {
                    radalert(strTopicNotChoose, 250, 100, strWarning);
                    return;
                }
                PageMethods.TopicMakeMenuDo(curNode.get_value(), CallWebMethodSuccess, CallWebMethodFailed);
            }
            break;
    }
}
var Topic_Add = 20;
var Topic_Edit = 21;
var Topic_Copy = 22;
var Topic_MakeMenu = 23;
var Topic_Delete = 24;
var Topic_Permission = 25;


function CreateElementTopicLinks() {
    btnTopicAdd = document.createElement("a");
    btnTopicAdd.setAttribute("href", "javascript:void(0)");
    btnTopicAdd.setAttribute("onClick", "TopicNew()");
    btnTopicAdd.setAttribute("class", "Header");
    btnTopicAdd.setAttribute("hidden", "true");
    var addName = document.createTextNode("[Thêm mới]");
    btnTopicAdd.appendChild(addName);

    btnTopicEdit = document.createElement("a");
    btnTopicEdit.setAttribute("href", "javascript:void(0)");
    btnTopicEdit.setAttribute("onClick", "TopicEdit()");
    btnTopicEdit.setAttribute("class", "Header");
    btnTopicEdit.setAttribute("hidden", "true");
    var editName = document.createTextNode("[Hiệu chỉnh]");
    btnTopicEdit.appendChild(editName);

    btnTopicCopy = document.createElement("a");
    btnTopicCopy.setAttribute("href", "javascript:void(0)");
    btnTopicCopy.setAttribute("onClick", "TopicCopy()");
    btnTopicCopy.setAttribute("class", "Header");
    btnTopicCopy.setAttribute("hidden", "true");
    var copyName = document.createTextNode("[Sao chép Chuyên mục]");
    btnTopicCopy.appendChild(copyName);

    btnTopicMakeMenu = document.createElement("a");
    btnTopicMakeMenu.setAttribute("href", "javascript:void(0)");
    btnTopicMakeMenu.setAttribute("onClick", "TopicMakeMenu()");
    btnTopicMakeMenu.setAttribute("class", "Header");
    btnTopicMakeMenu.setAttribute("hidden", "true");
    var makeMenuName = document.createTextNode("[Chuyển chuyên mục thành menu]");
    btnTopicMakeMenu.appendChild(makeMenuName);

    btnTopicDelete = document.createElement("a");
    btnTopicDelete.setAttribute("href", "javascript:void(0)");
    btnTopicDelete.setAttribute("onClick", "TopicDelete()");
    btnTopicDelete.setAttribute("class", "Header");
    btnTopicDelete.setAttribute("hidden", "true");
    var deleteName = document.createTextNode("[Xóa]");
    btnTopicDelete.appendChild(deleteName);

    btnTopicSecurity = document.createElement("a");
    btnTopicSecurity.setAttribute("href", "javascript:void(0)");
    btnTopicSecurity.setAttribute("onClick", "TopicSecurity()");
    btnTopicSecurity.setAttribute("class", "Header");
    btnTopicSecurity.setAttribute("hidden", "true");
    var securityName = document.createTextNode("[Phân quyền]");
    btnTopicSecurity.appendChild(securityName);

    btnTopicChangeRoot = document.createElement("a");
    btnTopicChangeRoot.setAttribute("href", "javascript:void(0)");
    btnTopicChangeRoot.setAttribute("onClick", "TopicChangeRoot()");
    btnTopicChangeRoot.setAttribute("class", "Header");
    btnTopicChangeRoot.setAttribute("hidden", "true");
    var changeRootName = document.createTextNode("[Chuyển lên cùng]");
    btnTopicChangeRoot.appendChild(changeRootName);

    btnTopicLoginEdit = document.createElement("a");
    btnTopicLoginEdit.setAttribute("href", "javascript:void(0)");
    btnTopicLoginEdit.setAttribute("onClick", "TopicLoginEdit()");
    btnTopicLoginEdit.setAttribute("class", "Header");
    btnTopicLoginEdit.setAttribute("hidden", "true");
    var loginEditName = document.createTextNode("[Cập nhật đăng nhập]");
    btnTopicLoginEdit.appendChild(loginEditName);

    btnTopicMoveUp = document.createElement("a");
    btnTopicMoveUp.setAttribute("href", "javascript:void(0)");
    btnTopicMoveUp.setAttribute("onClick", "TopicMoveUp()");
    btnTopicMoveUp.setAttribute("class", "Header");
    btnTopicMoveUp.setAttribute("hidden", "true");
    var moveUpName = document.createTextNode("[Di chuyển lên]");
    btnTopicMoveUp.appendChild(moveUpName);

    btnTopicMoveDown = document.createElement("a");
    btnTopicMoveDown.setAttribute("href", "javascript:void(0)");
    btnTopicMoveDown.setAttribute("onClick", "TopicMoveDown()");
    btnTopicMoveDown.setAttribute("class", "Header");
    btnTopicMoveDown.setAttribute("hidden", "true");
    var moveDownName = document.createTextNode("[Di chuyển xuống]");
    btnTopicMoveDown.appendChild(moveDownName);

}
function SecurityUI() {
    PageMethods.GetPermission([Topic_Add, Topic_Edit, Topic_Copy, Topic_MakeMenu, Topic_Delete, Topic_Permission], CallWebMethodSuccess, CallWebMethodFailed);
}
function SecurityUICallback(results, context, methodName) {
    var arrPermission = Sys.Serialization.JavaScriptSerializer.deserialize(results);
    if (arrPermission[Topic_Add]) {
        document.getElementById("topicLinks").appendChild(btnTopicAdd);
    }
    if (arrPermission[Topic_Edit]) {
        document.getElementById("topicLinks").appendChild(btnTopicEdit);
        document.getElementById("topicLinks").appendChild(btnTopicChangeRoot);
        document.getElementById("topicLinks").appendChild(btnTopicLoginEdit);
        document.getElementById("topicLinks").appendChild(btnTopicMoveUp);
        document.getElementById("topicLinks").appendChild(btnTopicMoveDown);
        tree._enableDragAndDrop = true;
    }
    if (arrPermission[Topic_MakeMenu]) {
        document.getElementById("topicLinks").appendChild(btnTopicMakeMenu);
    }
    if (arrPermission[Topic_Delete]) {
        document.getElementById("topicLinks").appendChild(btnTopicDelete);
    }
    if (arrPermission[Topic_Copy]) {
        document.getElementById("topicLinks").appendChild(btnTopicCopy);
    }
    if (arrPermission[Topic_Permission]) {
        document.getElementById("topicLinks").appendChild(btnTopicSecurity);
    }
}

function pageLoad() {
    initialize();
    CreateElementTopicLinks();
    SecurityUI();
}
