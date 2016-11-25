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
    PageMethods.Save(rcbPage.get_value(), CallWebMethodSuccess, CallWebMethodFailed);
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