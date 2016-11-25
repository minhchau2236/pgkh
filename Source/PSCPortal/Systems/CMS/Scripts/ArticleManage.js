var oWnd;
var strArticleNotChoose;
var strWarning;
var strInformation;
var grid;
var tree;
var strArticleChangeTopicPrimaryOnlyOne;
var strArticleConfirmDelete;
var strArticleUpdateDescriptionSuccess;
var strArticleUpdateContentSuccess;
var strArticleUpdateTopicSuccess;
var strArticleChangeTopicPrimarySuccess;
var dialogMethod;
var flag = 1;
var strArticleSearch;
var strTopic;
var btnArticleNew;
var btnArticleEdit;
var btnArticleEditTopic;
var btnArticlePublish;
var btnArticleUnPublish;
var btnArticleDelete;
var btnArticlePreview;
var btnArticleUnPublishArticles;
var btnArticleComment;
var btnArticleCommentNew;
var btnSendArticle;
var btnArticleLoginEdit;
var btnChangeTopicPrimary;
var btnAticleNoBelongTopicPrimary;
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
        case "ArticleNew":
            {
                ArticleNewCallback(results, context, methodName);
            }
            break;
        case "ArticleAdd":
            {
                ArticleAddCallback(results, context, methodName);
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
        case "ArticleEditDescription":
            {
                ArticleEditDescriptionCallback(results, context, methodName);
            }
            break;
        case "ArticleEditContent":
            {
                ArticleEditContentCallback(results, context, methodName);
            }
            break;
        case "ArticleEditTopic":
            {
                ArticleEditTopicCallback(results, context, methodName);
            }
            break;
        case "SecurityUI":
            {
                SecurityUICallback(results, context, methodName);
            }
            break;
        case "ArticleChangeTopicPrimary":
            {
                ArticleChangeTopicPrimaryCallback(results, context, methodName);
            }
            break;
        case "GetArticleNoBelongTopicPrimary":
            {
                GetArticleNoBelongTopicPrimaryCallback(results, context, methodName);
            }
        case "ArticleChuaXB":
            {
                ArticleChuaXBCallback(results, context, methodName);
            }
            break;
        case "GetArticleListChuaXB":
            {
                GetArticleListChuaXBCallback(results, context, methodName);
            }
            break;
        case "GetArticleCountChuaXB":
            {
                GetArticleCountChuaXBCallback(results, context, methodName);
            }
            break;
        case "GetCommentList":
            {
                GetCommentListCallback(results, context, methodName);
            }
            break;
        case "ArticleCommentNewList":
            {
                ArticleCommentNewListCallback(results, context, methodName);
            }
            break;
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
        case "ArticleLoginEdit":
            {
                TopicLoginEditCallback(results, context, methodName);
            }
            break;
        case "ArticleSearch":
            {
                ArticleSearchCallback(results, context, methodName);
            }
            break;
    }
}

function ArticleSearch() {
    var tableView = grid.get_masterTableView();
    var sortExpressions = tableView.get_sortExpressions();
    PageMethods.ArticleSearch(strArticleSearch.value, 0, tableView.get_pageSize(), sortExpressions.toString(), CallWebMethodSuccess);
    grid.get_masterTableView().clearSelectedItems();
    tree.unselectAllNodes();
}

function ArticleSearchCallback(results, context, methodName) {
    var tableView = grid.get_masterTableView();
    var obj = Sys.Serialization.JavaScriptSerializer.deserialize(results);
    tableView.set_dataSource(obj.Data);
    tableView.set_virtualItemCount(obj.Count);
    tableView.dataBind();
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
    //radalert(results._message, 250, 100, strWarning);
}
function TopicSelect(sender, eventArgs) {
    document.getElementById("articleLinks").innerHTML = "";
    flag = 1;
    GetArticleList();
    SecurityUI();

}
function GetArticleList() {
    var curNode = tree.get_selectedNode();
    var tableView = grid.get_masterTableView();
    var sortExpressions = tableView.get_sortExpressions();
    PageMethods.GetArticleList(curNode.get_value(), 0, tableView.get_pageSize(), sortExpressions.toString(), CallWebMethodSuccess);
    PageMethods.GetArticleCount(CallWebMethodSuccess);
    grid.get_masterTableView().clearSelectedItems();
}
function GetArticleListChuaXB() {
    var curNode = tree.get_selectedNode();
    var tableView = grid.get_masterTableView();
    var sortExpressions = tableView.get_sortExpressions();
    PageMethods.GetArticleListChuaXB(curNode.get_value(), 0, tableView.get_pageSize(), sortExpressions.toString(), CallWebMethodSuccess);
    PageMethods.GetArticleCountChuaXB(CallWebMethodSuccess);
    grid.get_masterTableView().clearSelectedItems();
}
function GetArticleCommentNewList() {
    var curNode = tree.get_selectedNode();
    var tableView = grid.get_masterTableView();
    var sortExpressions = tableView.get_sortExpressions();
    PageMethods.GetArticleCommentNewList(curNode.get_value(), 0, tableView.get_pageSize(), sortExpressions.toString(), CallWebMethodSuccess);
    PageMethods.GetArticleCommentNewListCount(CallWebMethodSuccess);
    grid.get_masterTableView().clearSelectedItems();
}

function SecurityUI() {
    var curNode = tree.get_selectedNode();
    if (!curNode)
        return;
    PageMethods.SecurityUI(curNode.get_value(), CallWebMethodSuccess, CallWebMethodFailed);
}

function CreateElementArticleLinks() {
    btnArticleNew = document.createElement("a");
    btnArticleNew.setAttribute("href", "javascript:void(0)");
    btnArticleNew.setAttribute("onClick", "ArticleNew()");
    btnArticleNew.setAttribute("class", "Header");
    btnArticleNew.setAttribute("hidden", "true");
    var addName = document.createTextNode("[Thêm mới]");
    btnArticleNew.appendChild(addName);

    btnArticleEdit = document.createElement("a");
    btnArticleEdit.setAttribute("href", "javascript:void(0)");
    btnArticleEdit.setAttribute("onClick", "ArticleEdit()");
    btnArticleEdit.setAttribute("class", "Header");
    btnArticleEdit.setAttribute("hidden", "true");
    var editName = document.createTextNode("[Hiệu chỉnh]");
    btnArticleEdit.appendChild(editName);

    btnArticleEditTopic = document.createElement("a");
    btnArticleEditTopic.setAttribute("href", "javascript:void(0)");
    btnArticleEditTopic.setAttribute("onClick", "ArticleEditTopic()");
    btnArticleEditTopic.setAttribute("class", "Header");
    btnArticleEditTopic.setAttribute("hidden", "true");
    var editTopicName = document.createTextNode("[Hiệu chỉnh Chuyên mục]");
    btnArticleEditTopic.appendChild(editTopicName);

    btnArticlePublish = document.createElement("a");
    btnArticlePublish.setAttribute("href", "javascript:void(0)");
    btnArticlePublish.setAttribute("onClick", "ArticlePublish()");
    btnArticlePublish.setAttribute("class", "Header");
    btnArticlePublish.setAttribute("hidden", "true");
    var publishName = document.createTextNode("[Xuất bản]");
    btnArticlePublish.appendChild(publishName);

    btnArticleUnPublish = document.createElement("a");
    btnArticleUnPublish.setAttribute("href", "javascript:void(0)");
    btnArticleUnPublish.setAttribute("onClick", "ArticleUnPublish()");
    btnArticleUnPublish.setAttribute("class", "Header");
    btnArticleUnPublish.setAttribute("hidden", "true");
    var unpublishName = document.createTextNode("[K.xuất bản]");
    btnArticleUnPublish.appendChild(unpublishName);

    btnArticleDelete = document.createElement("a");
    btnArticleDelete.setAttribute("href", "javascript:void(0)");
    btnArticleDelete.setAttribute("onClick", "ArticleDelete()");
    btnArticleDelete.setAttribute("class", "Header");
    btnArticleDelete.setAttribute("hidden", "true");
    var deleteName = document.createTextNode("[Xóa]");
    btnArticleDelete.appendChild(deleteName);

    btnArticlePreview = document.createElement("a");
    btnArticlePreview.setAttribute("href", "javascript:void(0)");
    btnArticlePreview.setAttribute("onClick", "ArticlePreview()");
    btnArticlePreview.setAttribute("class", "Header");
    btnArticlePreview.setAttribute("hidden", "true");
    var previewName = document.createTextNode("[Xem hiển thị]");
    btnArticlePreview.appendChild(previewName);

    btnArticleUnPublishArticles = document.createElement("a");
    btnArticleUnPublishArticles.setAttribute("href", "javascript:void(0)");
    btnArticleUnPublishArticles.setAttribute("onClick", "ArticleChuaXB()");
    btnArticleUnPublishArticles.setAttribute("class", "Header");
    btnArticleUnPublishArticles.setAttribute("hidden", "true");
    var ubaName = document.createTextNode("[Bài viết chưa xuất bản]");
    btnArticleUnPublishArticles.appendChild(ubaName);

    btnArticleComment = document.createElement("a");
    btnArticleComment.setAttribute("href", "javascript:void(0)");
    btnArticleComment.setAttribute("onClick", "GetCommentList()");
    btnArticleComment.setAttribute("class", "Header");
    btnArticleComment.setAttribute("hidden", "true");
    var commentName = document.createTextNode("[Danh sách câu hỏi]");
    btnArticleComment.appendChild(commentName);

    btnArticleCommentNew = document.createElement("a");
    btnArticleCommentNew.setAttribute("href", "javascript:void(0)");
    btnArticleCommentNew.setAttribute("onClick", "ArticleCommentNewList()");
    btnArticleCommentNew.setAttribute("class", "Header");
    btnArticleCommentNew.setAttribute("hidden", "true");
    var accName = document.createTextNode("[Các bài viết có câu hỏi mới]");
    btnArticleCommentNew.appendChild(accName);

    btnSendArticle = document.createElement("a");
    btnSendArticle.setAttribute("href", "javascript:void(0)");
    btnSendArticle.setAttribute("onClick", "ArticleSend()");
    btnSendArticle.setAttribute("class", "Header");
    btnSendArticle.setAttribute("hidden", "true");
    var sArticleName = document.createTextNode("[Chuyển bài viết]");
    btnSendArticle.appendChild(sArticleName);

    btnArticleLoginEdit = document.createElement("a");
    btnArticleLoginEdit.setAttribute("href", "javascript:void(0)");
    btnArticleLoginEdit.setAttribute("onClick", "ArticleLoginEdit()");
    btnArticleLoginEdit.setAttribute("class", "Header");
    btnArticleLoginEdit.setAttribute("hidden", "true");
    var aldName = document.createTextNode("[Cập nhật đăng nhập]");
    btnArticleLoginEdit.appendChild(aldName);

    btnChangeTopicPrimary = document.createElement("a");
    btnChangeTopicPrimary.setAttribute("href", "javascript:void(0)");
    btnChangeTopicPrimary.setAttribute("onClick", "ArticleChangeTopicPrimary()");
    btnChangeTopicPrimary.setAttribute("class", "Header");
    btnChangeTopicPrimary.setAttribute("hidden", "true");
    var ctpName = document.createTextNode("[Chuyển Chuyên mục chính]");
    btnChangeTopicPrimary.appendChild(ctpName);

    btnAticleNoBelongTopicPrimary = document.createElement("a");
    btnAticleNoBelongTopicPrimary.setAttribute("href", "javascript:void(0)");
    btnAticleNoBelongTopicPrimary.setAttribute("onClick", "GetArticleNoBelongTopicPrimary()");
    btnAticleNoBelongTopicPrimary.setAttribute("class", "Header");
    btnAticleNoBelongTopicPrimary.setAttribute("hidden", "true");
    var anbtpName = document.createTextNode("[Bài viết chưa chọn chuyên mục chính]");
    btnAticleNoBelongTopicPrimary.appendChild(anbtpName);

}
function SecurityUICallback(results, context, methodName) {
    var data = Sys.Serialization.JavaScriptSerializer.deserialize(results);
    if (data.AllowNew) {
        document.getElementById("articleLinks").appendChild(btnArticleNew);
    }
    if (data.AllowEdit) {
        document.getElementById("articleLinks").appendChild(btnArticleEdit);
        document.getElementById("articleLinks").appendChild(btnArticleEditTopic);
        document.getElementById("articleLinks").appendChild(btnArticlePreview);
        document.getElementById("articleLinks").appendChild(btnArticleComment);
        document.getElementById("articleLinks").appendChild(btnArticleUnPublishArticles);
        document.getElementById("articleLinks").appendChild(btnArticleCommentNew);
        document.getElementById("articleLinks").appendChild(btnSendArticle);
        document.getElementById("articleLinks").appendChild(btnArticleLoginEdit);
    }
    if (data.AllowDelete) {
        document.getElementById("articleLinks").appendChild(btnArticleDelete);
    }
    if (data.AllowAprove) {
        document.getElementById("articleLinks").appendChild(btnArticlePublish);
        document.getElementById("articleLinks").appendChild(btnArticleUnPublish);
    }
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
function GetArticleListChuaXBCallback(results, context, methodName) {
    var tableView = grid.get_masterTableView();
    tableView.set_dataSource(Sys.Serialization.JavaScriptSerializer.deserialize(results));
    tableView.dataBind();
}
function GetArticleCountChuaXBCallback(results, context, methodName) {
    var tableView = grid.get_masterTableView();
    tableView.set_virtualItemCount(results);
}
function ArticleChuaXB() {
    if (tree.get_selectedNode() == null) {
        radalert(strTopic, 250, 100, strWarning);
        return;
    }
    GetArticleListChuaXB();
}
function ArticleCommentNewList() {
    if (tree.get_selectedNode() == null) {
        radalert(strTopic, 250, 100, strWarning);
        return;
    }
    GetArticleCommentNewList();
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


function ArticleNew() {
    if (tree.get_selectedNode() == null) {
        radalert(strTopic, 250, 100, strWarning);
        return;
    }
    PageMethods.ArticleNew(tree.get_selectedNode().get_value(), CallWebMethodSuccess, CallWebMethodFailed);
}
function ArticleNewCallback(results, context, methodName) {

    dialogMethod = "ArticleNew";
    oWnd.setSize(830, 600);
    oWnd.setUrl("ArticleDetail.aspx");
    oWnd.show();
}
function ArticleAddCallback(result, context, methodName) {
    GetArticleList();
}

function ArticleEdit() {
    var items = grid.get_masterTableView().get_selectedItems();
    if (items.length == 0) {
        radalert(strArticleNotChoose, 250, 100, strWarning);
        return;
    }
    var obj = items[0].get_dataItem();
    PageMethods.ArticleEdit(obj.Id, CallWebMethodSuccess, CallWebMethodFailed);
}
function ArticleEditCallback(results, context, methodName) {
    dialogMethod = "ArticleEdit";
    oWnd.setSize(830, 600);
    oWnd.setUrl("ArticleDetail.aspx");
    oWnd.show();
}
function ArticleEditContent() {
    var items = grid.get_masterTableView().get_selectedItems();
    if (items.length == 0) {
        radalert(strArticleNotChoose, 250, 100, strWarning);
        return;
    }
    var obj = items[0].get_dataItem();
    PageMethods.ArticleEditContent(obj.Id, CallWebMethodSuccess, CallWebMethodFailed);
}
function ArticleEditContentCallback() {
    dialogMethod = "ArticleEditContent";
    oWnd.setSize(800, 600);
    oWnd.setUrl("ArticleContent.aspx");
    oWnd.show();
}
function ArticleEditDescription() {
    var items = grid.get_masterTableView().get_selectedItems();
    if (items.length == 0) {
        radalert(strArticleNotChoose, 250, 100, strWarning);
        return;
    }
    var obj = items[0].get_dataItem();
    PageMethods.ArticleEditDescription(obj.Id, CallWebMethodSuccess, CallWebMethodFailed);
}
function ArticleEditDescriptionCallback() {
    dialogMethod = "ArticleEditDescription";
    oWnd.setSize(800, 600);
    oWnd.setUrl("ArticleDescription.aspx");
    oWnd.show();
}
function ArticleUpdateCallback(results, context, methodName) {
    var curNode = tree.get_selectedNode();
    if (curNode != null)
        GetArticleList();
    else
        ArticleSearch();
}
function ArticleEditTopic() {
    var items = grid.get_masterTableView().get_selectedItems();
    if (items.length == 0) {
        radalert(strArticleNotChoose, 250, 100, strWarning);
        return;
    }
    var obj = items[0].get_dataItem();
    PageMethods.ArticleEditTopic(obj.Id, CallWebMethodSuccess, CallWebMethodFailed);
}
function ArticleEditTopicCallback(results, context, methodName) {
    dialogMethod = "ArticleEditTopic";
    oWnd.setSize(600, 400);
    oWnd.setUrl("ArticleTopic.aspx");
    oWnd.show();
}
function ArticleChangeTopicPrimary() {
    var items = grid.get_masterTableView().get_selectedItems();
    if (items.length == 0) {
        radalert(strArticleNotChoose, 250, 100, strWarning);
        return;
    }

    var obj = items[0].get_dataItem();
    PageMethods.ArticleChangeTopicPrimary(obj.Id, CallWebMethodSuccess, CallWebMethodFailed);
}
function ArticleChangeTopicPrimaryCallback(resuls, context, methodName) {
    if (!resuls) {
        radalert(strArticleChangeTopicPrimaryOnlyOne, 250, 100, strWarning);
        return;
    }
    dialogMethod = "ArticleChangeTopicPrimary";
    oWnd.setSize(600, 250);
    oWnd.setUrl("ArticleChangeTopicPrimary.aspx");
    oWnd.show();

}
function ArticleLoginEdit() {
    var items = grid.get_masterTableView().get_selectedItems();
    if (items.length == 0) {
        radalert(strArticleNotChoose, 250, 100, strWarning);
        return;
    }
    var obj = items[0].get_dataItem();
    PageMethods.ArticleLoginEdit(obj.Id, CallWebMethodSuccess, CallWebMethodFailed);
}
function TopicLoginEditCallback(results, context, methodName) {
    dialogMethod = "ArticleLoginEdit";
    oWnd.setSize(600, 370);
    oWnd.setUrl("ArticleLoginDetail.aspx");
    oWnd.show();
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
    var curNode = tree.get_selectedNode();
    if (curNode != null)
        GetArticleList();
    else
        ArticleSearch();
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
    var curNode = tree.get_selectedNode();
    if (curNode != null)
        GetArticleList();
    else
        ArticleSearch();
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
    var curNode = tree.get_selectedNode();
    if (curNode != null)
        GetArticleList();
    else
        ArticleSearch();
}
function gvArticle_Command(sender, args) {
    args.set_cancel(true);
    var curNode = tree.get_selectedNode();
    sender.get_masterTableView().clearSelectedItems();
    var currentPageIndex = sender.get_masterTableView().get_currentPageIndex();
    var pageSize = sender.get_masterTableView().get_pageSize();
    var sortExpressions = sender.get_masterTableView().get_sortExpressions();
    if (curNode != null)
        PageMethods.GetArticleList(curNode.get_value(), currentPageIndex * pageSize, pageSize, sortExpressions.toString(), CallWebMethodSuccess);
    else
        PageMethods.ArticleSearch(strArticleSearch.value, currentPageIndex * pageSize, pageSize, sortExpressions.toString(), CallWebMethodSuccess);
    //PageMethods.GetArticleCount(CallWebMethodSuccess);
}

function pageLoad() {
    initialize();
    CreateElementArticleLinks();
    var firstNode = tree.get_nodes().getNode(0);
    if (firstNode = tree.get_nodes().getNode(0)) {
        firstNode.select();
        GetArticleList();
        SecurityUI();
    }
}
function ArticlePreview() {
    var curNode = tree.get_selectedNode();
    var items = grid.get_masterTableView().get_selectedItems();
    if (items.length == 0) {
        radalert(strArticleNotChoose, 250, 100, strWarning);
        return;
    }
    var obj = items[0].get_dataItem();
    dialogMethod = "ArticlePreview";
    oWnd.setSize(600, 250);
    oWnd.setUrl(String.format("/ArticleId/{0}/IsPreview={1}", obj.Id, "IsPreview"));
    //oWnd.setUrl(String.format("/{0}/{1}/{2}",curNode.get_value(), obj.Id, "Preview"));
    oWnd.show();
}
function GetArticleNoBelongTopicPrimary() {
    flag = 0;
    var tableView = grid.get_masterTableView();
    var sortExpressions = tableView.get_sortExpressions();
    grid.get_masterTableView().clearSelectedItems();
    PageMethods.GetArticleNoBelongTopicPrimary(0, tableView.get_pageSize(), sortExpressions.toString(), CallWebMethodSuccess)
}
function GetArticleNoBelongTopicPrimaryCallback(results, context, methodName) {
    tree.unselectAllNodes();
    var tableView = grid.get_masterTableView();
    tableView.set_dataSource(Sys.Serialization.JavaScriptSerializer.deserialize(results));
    tableView.dataBind();

}
// goi bai viet ve truong

function ArticleSend() {
    var items = grid.get_masterTableView().get_selectedItems();
    if (items.length == 0) {
        radalert(strArticleNotChoose, 250, 100, strWarning);
        return;
    }
    var obj = items[0].get_dataItem();
    PageMethods.ArticleSend(obj.Id, ArticleSendCallback, CallWebMethodFailed);
}
function ArticleSendCallback(results, context, methodName) {
    dialogMethod = "ArticleSendSchool";
    oWnd.setSize(800, 600);
    oWnd.setUrl("ArticleSubDomain.aspx");
    oWnd.show();
}
//
function RadWindowArticleClose(sender, args) {
    var curNode = tree.get_selectedNode();
    if (!args.get_argument())
        return;
    switch (dialogMethod) {
        case "ArticleNew":
            {
                PageMethods.ArticleAdd(CallWebMethodSuccess, CallWebMethodFailed);
            }
            break;
        case "ArticleEdit":
            {
                PageMethods.ArticleUpdate(CallWebMethodSuccess, CallWebMethodFailed);
            }
            break;
        case "ArticleEditDescription":
            {
                radalert(strArticleUpdateDescriptionSuccess, 250, 100, strInformation);
            }
            break;
        case "ArticleEditContent":
            {
                radalert(strArticleUpdateContentSuccess, 250, 100, strInformation);
            }
            break;
        case "ArticleEditTopic":
            {
                radalert(strArticleUpdateTopicSuccess, 250, 100, strInformation);
                if (curNode != null)
                    GetArticleList();
                else
                    ArticleSearch();
            }
            break;
        case "ArticleChangeTopicPrimary":
            {
                radalert(strArticleChangeTopicPrimarySuccess, 250, 100, strInformation);
                if (flag == 0)
                    GetArticleNoBelongTopicPrimary();
            }
            break;
        case "GetCommentList":
            {

            }
            break;
    }
}
