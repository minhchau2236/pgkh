var txtName;
var txtDescription;
var rcbPage;
var cbxRss;

function CallWebMethodSuccess(results, context, methodName) {
    switch (methodName) {
        case "Save":
            {
                if (txtName.value == "") {
                    radalert("Tên chuyên mục chưa nhập", 250, 100, "Chú ý");
                    txtName.focus();
                }
                else {
                    SaveCallback(results, context, methodName);
                }               
            }
            break;
    }
}
function CallWebMethodFailed(results, context, methodName) {
    alert(results._message);
}
function Save() {  
    PageMethods.Save(txtName.value, txtDescription.value, rcbPage.get_value(), cbxRss.checked, CallWebMethodSuccess, CallWebMethodFailed);
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