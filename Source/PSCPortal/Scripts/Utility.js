function BindDataListBox(list, data) {
    var items = list.get_items();
    items.clear();

    for (var i = 0; i < data.length; i++) {        
        var item = new Telerik.Web.UI.RadListBoxItem();
        item.set_text(data[i].Name);
        item.set_value(data[i].Id);
        items.add(item);        
    }
}

function BindDataComboBox(list, data) {
    var items = list.get_items();
    items.clear();    
    for (var i = 0; i < data.length; i++) {
        var item = new Telerik.Web.UI.RadComboBoxItem();
        item.set_text(data[i].Name);
        item.set_value(data[i].Id);
        items.add(item);
    }
    if (data.length > 0)
        items.getItem(0).select();
}

function GetRadWindow() {
    var oWindow = null;
    if (window.radWindow)
        oWindow = window.radWindow;
    else if (window.frameElement.radWindow)
        oWindow = window.frameElement.radWindow;
    return oWindow;
}
function DisplayElement(element) {
    element.style.display="inline";
}

function RemoveElement(element) {
    element.style.display = "none";
}         