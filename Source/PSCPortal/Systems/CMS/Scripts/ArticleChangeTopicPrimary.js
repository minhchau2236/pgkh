var strWarning;        
var rcbTopic;
var strTopicNotChoose;

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
    if(rcbTopic.get_value()=="00000000-0000-0000-0000-000000000000")
    {
        radalert(strTopicNotChoose, 250, 100, strWarning);
        return;
    }
    PageMethods.Save(rcbTopic.get_value(), CallWebMethodSuccess, CallWebMethodFailed);
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