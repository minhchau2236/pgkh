var strWarning;
var txtName;
var txtMail;
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
    PageMethods.Save(txtName.value, txtMail.value, CallWebMethodSuccess, CallWebMethodFailed);
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