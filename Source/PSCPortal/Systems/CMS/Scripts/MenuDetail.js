var strWarning;
var txtName;
var txtDescription;

var rdoArticle;
var txtArticle;
var rdoTopic;
var rcbTopic;

var rdoPage;
var rcbPage;

var rdoModule;
var rcbModule;

var rdoDocument;
var txtDocument;
var rdoDocumentType;
var rcbDocumentType;

var rdoURL;
var txtURL;

var strNameCanNotEmpty;
var strDescriptionCanNotEmpty;

var editWindow;
var dialogMethod;

function CallWebMethodSuccess(results, context, methodName) {
    switch (methodName) {
        case "Save":
            {              
                SaveCallback(results, context, methodName);
            }
            break;
    }
}
function CallWebMethodFailed(results, context, methodName) {
    radalert(results._message, 250, 100, strWarning);
}
function Save() {
    if (txtName.value == '') {
        alert(strNameCanNotEmpty);
        return;
    }
    if (txtDescription.value == '') {
        alert(strDescriptionCanNotEmpty);
        return;
    }
    var link;
    if (rdoArticle.checked == true) {
        link = "~/Default.aspx?ArticleId=" + txtArticle.value;
    }
    else if (rdoTopic.checked == true) {
        link = "~/Default.aspx?TopicId=" + rcbTopic.get_value();;
    }
    else if (rdoPage.checked == true) {
        link = "~/Default.aspx?PageId=" + rcbPage.get_value();;
    }
    else if (rdoModule.checked == true) {
        link = "~/Default.aspx?ModuleId=" + rcbModule.get_value();;
    }

    else if (rdoDocumentType.checked == true) {
        link = "~/Default.aspx?DocumentTypeId=" + rcbDocumentType.get_value();;
    }
    else if (rdoDocument.checked == true) {
        link = "~/Default.aspx?DocumentId=" + txtDocument.value;
    }
    else {
        link = txtURL.value;
    }
    PageMethods.Save(txtName.value, txtDescription.value, link, CallWebMethodSuccess, CallWebMethodFailed);
}
function SaveCallback(results, context, methodName) {
    var oWnd = GetRadWindow();
    oWnd.close(true);
}
function Cancel() {
    var oWnd = GetRadWindow();
    oWnd.close(false);
}
function OnBrowseArticle() {    
    dialogMethod = "editPortlet";
    //editWindow.setSize(700, 450); //Ngọc -16122015
    editWindow.setUrl("ChooseArticle.aspx");
    editWindow.show();
    editWindow.Maximize();
    // var articleChoose = window.showModalDialog("ChooseArticle.aspx", null, "dialogHeight: 400px; dialogWidth: 800px;");
    //if (articleChoose)
    // txtArticle.value = articleChoose;
}
function OnBrowseDocument() {    
    var documentChoose = window.showModalDialog("ChooseDocument.aspx", null, "dialogHeight: 400px; dialogWidth: 800px;");
   
    if (documentChoose)
        txtDocument.value = documentChoose;
}
function pageLoad() {
    initialize();
}

function EditRW_ClientClose(sender, args) {
    if (!args.get_argument())
        return;
    switch (dialogMethod) {
        case "editPortlet":
            {
                var articleChoose = args.get_argument();
                if (articleChoose)
                    txtArticle.value = articleChoose;
            }
            break;
    }
}