var strWarning;
var txtName;
var txtDescription;
function CallWebMethodSuccess(results, context, methodName) {
    switch (methodName) {
        case "Save":
            {
                if (txtName.value == "") {
                    radalert("Bạn vui lòng nhập vào tên của menu !!!", 250, 100, "Chú ý");
                }
                else {
                    SaveCallback(results, context, methodName);
                }
            }
            break;                            
    }
}
function CallWebMethodFailed(results, context, methodName) {          
    radalert(results._message, 250, 100, strWarning);
}
function Save() {
    PageMethods.Save(txtName.value, txtDescription.value, CallWebMethodSuccess, CallWebMethodFailed);
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