var txtName;
var txtDescription;
//var txtLink;
var rcbPage;
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
    alert(results._message);
}

function Save() {
    //Ngọc - 17122015
    var required = false;
    Page_ClientValidate("ValidationGroup");
    if (Page_IsValid) {
        required = true;
    }

    if (required) {
        var nameSubDomainCustom = txtName.value.replace(" ", "");//Remove khoản trắng
        //PageMethods.Save(txtName.value, txtDescription.value, rcbPage.get_value(), CallWebMethodSuccess, CallWebMethodFailed);
        PageMethods.Save(nameSubDomainCustom, txtDescription.value, rcbPage.get_value(), CallWebMethodSuccess, CallWebMethodFailed);
    }
}
function SaveCallback(results, context, methodName) {
    var oWnd = GetRadWindow();
    oWnd.close(true);
}
function Cancel() {
    var oWnd = GetRadWindow();
    oWnd.close(false);
}
function pageLoad() {
    initialize();
}