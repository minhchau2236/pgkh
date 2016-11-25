var strWarning;
var strTopicNotChoose;
var combobox;
function CallWebMethodSuccess(results, context, methodName) {
    switch (methodName) {
     case "TopicSelect":
         {
             SaveCallback(results, context, methodName);
         }
         break;
    }
}
function CallWebMethodFailed(results, context, methodName) {
    alert(results._message);
}
function Save()
{
    var value = combobox.get_value();
    if (value == '00000000-0000-0000-0000-000000000000') {
        radalert(strTopicNotChoose, 250, 100, strWarning);
        return;
    }
    PageMethods.TopicSelect(value, CallWebMethodSuccess, CallWebMethodFailed);            
}
function SaveCallback(results, context, methodName) {
    var oWnd = GetRadWindow();
    oWnd.close(true);  
}
function Cancel()
{
    var oWnd = GetRadWindow();
    oWnd.close(false);  
}
function pageLoad() {
    initialize();
}