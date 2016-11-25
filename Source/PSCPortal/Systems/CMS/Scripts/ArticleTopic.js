var strWarning ;
var combobox ;
var strTopicNotChoose;
var grid;
var TopicList;
function CallWebMethodSuccess(results, context, methodName) {        
    switch (methodName) {
        case "Save":
            {
                SaveCallback(results, context, methodName);
            }
            break; 
        case "GetTopicBelong":
            {
                GetTopicBelongCallback(results,context,methodName);
            }  
            break;  
        case "AddTopicBelong":
            {
                AddTopicBelongCallback(results,context,methodName);
            }  
            break;
        case "DeleteTopicBelong":
            {
                DeleteTopicBelongCallback(results,context,methodName);
            }
            break;
    }
}
function CallWebMethodFailed(results, context, methodName) {       
    radalert(results._message, 250, 100, strWarning);
}
function Save() {            		
    PageMethods.Save(CallWebMethodSuccess, CallWebMethodFailed);
}
function SaveCallback(results, context, methodName) {               	
    var oWnd = GetRadWindow();
    oWnd.close(true); 
}
function Cancel() {
    var oWnd = GetRadWindow();
    oWnd.close(false); 
}
function pageLoad(){
    initialize();
    PageMethods.GetTopicBelong(CallWebMethodSuccess,CallWebMethodFailed);
}
function GetTopicBelongCallback(results,context,methodName){
    var topics=Sys.Serialization.JavaScriptSerializer.deserialize(results);
    gvTopicBelong_DataBind(topics);
}
function gvTopicBelong_DataBind(topics){
    TopicList = topics;
    var tableView = grid.get_masterTableView();          
    tableView.set_dataSource(topics);
    tableView.dataBind();            
    
    CheckVisibleComboTopic();
}

function AddTopicBelong(){ 
    
    var value = combobox.get_value();
    if (value == '00000000-0000-0000-0000-000000000000') {
        alert(strTopicNotChoose);
        return;
    }  
    PageMethods.AddTopicBelong(value,CallWebMethodSuccess,CallWebMethodFailed);
}
function AddTopicBelongCallback(results,context,methodName){            
    gvTopicBelong_DataBind(Sys.Serialization.JavaScriptSerializer.deserialize(results));   
}
function GetTopicSelect(){
    var items = grid.get_masterTableView().get_selectedItems();            
    var objList = new Array();
    for (var i = 0; i < items.length; i++) {
        Array.add(objList, items[i].get_dataItem().Id);
    }
    return objList;
}
function DeleteTopicBelong(){
    var objList=GetTopicSelect();
    PageMethods.DeleteTopicBelong(objList,CallWebMethodSuccess,CallWebMethodFailed);
}
function DeleteTopicBelongCallback(results,context,methodName){               
    gvTopicBelong_DataBind(Sys.Serialization.JavaScriptSerializer.deserialize(results));
}
function gvTopicBelong_Command(sender,args)
{
    
}
function rcbTopic_SelectedIndexChanged(sender,args)
{
    CheckVisibleComboTopic();
    
}
function CheckVisibleComboTopic ()
{
    for (var i = 0; i<TopicList.length ; i++)
    {
        if(combobox.get_value()==TopicList[i].Id)
        {
            document.getElementById('lnk_Add').style.cssText="visibility:hidden";
            return;
        }
    }
    document.getElementById('lnk_Add').style.cssText="visibility:visible";
}
