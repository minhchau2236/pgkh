
//                                     Declare variable
//===================================================================================================//
var dialogMethod = '';
var arrayDivConnectWith = ["#psc-divPanelTop-Content", "#psc-divPanelLeft-Content", "#psc-divPanelCenter-Content", "#psc-divPanelRight-Content", "#psc-divPanelBottom-Content"];
var editWindow = null;
//                                  End Declare variable
//===================================================================================================//


//                              Sortable Portlet in jQueryUI
//===================================================================================================//

function ParsePosition(strListPosition) {
    var arrPanel = strListPosition.split("|");
    var messagePositions = "[";
    for (var i = 0; i < arrPanel.length; i++) {
        if (i != 0)
            messagePositions += ", ";
        var arrElement = arrPanel[i].split("&");
        messagePositions += "[";
        if (arrElement[0].length > 0)
            for (var j = 0; j < arrElement.length; j++) {
                if (j != 0)
                    messagePositions += ", ";
                if (arrElement[j].split("=").length > 1)
                    messagePositions += "\"" + arrElement[j].split("=")[1] + "\"";
            }
        messagePositions += "]";
    }
    messagePositions += "]";
    return messagePositions;
}
function OnChange() {
    var temp = Serialize();
    if (temp != strStatus) {
        OnUpdatePosition(temp);
        strStatus = temp;
    }
}
function OnUpdatePosition(strListNew) {
    //alert(ParsePosition(strStatus));
    //alert(ParsePosition(strListNew));
    var arrListOrg = Sys.Serialization.JavaScriptSerializer.deserialize(ParsePosition(strStatus));
    var arrListNew = Sys.Serialization.JavaScriptSerializer.deserialize(ParsePosition(strListNew));
    var i = 0;
    var index1 = 0;
    var index2 = 0;
    if (arrListOrg[0].length == arrListNew[0].length &&
                        arrListOrg[1].length == arrListNew[1].length &&
                        arrListOrg[2].length == arrListNew[2].length &&
                        arrListOrg[3].length == arrListNew[3].length &&
                        arrListOrg[4].length == arrListNew[4].length) {

        while ((index1 = CompareArray(arrListOrg[i], arrListNew[i++])) == -1);
        i = 0;
        while ((index2 = CompareArrayLast(arrListOrg[i], arrListNew[i++])) == -1);
        i--;
        var temp = arrListOrg[i][index1];
        for (var j = index1; j < index2; j++)
            arrListOrg[i][j] = arrListOrg[i][j + 1];
        arrListOrg[i][index2] = temp;
        if (CompareArray(arrListOrg[i], arrListNew[i]) == -1) {
            PageMethods.PortletChangePosition(i, index1, i, index2);
        }
        else {
            PageMethods.PortletChangePosition(i, index2, i, index1);
        }
    }
    else {
        var arrPanel = new Array();
        for (j = 0; j < arrListOrg.length; j++)
            if (arrListNew[j].length != arrListOrg[j].length)
                arrPanel[i++] = j;
        index1 = CompareArrayAsync(arrListOrg[arrPanel[0]], arrListNew[arrPanel[0]]);
        index2 = CompareArrayAsync(arrListOrg[arrPanel[1]], arrListNew[arrPanel[1]]);
        if (arrListOrg[arrPanel[0]].length > arrListNew[arrPanel[0]].length) {
            PageMethods.PortletChangePosition(arrPanel[0], index1, arrPanel[1], index2);
        }
        else {
            PageMethods.PortletChangePosition(arrPanel[1], index2, arrPanel[0], index1);
        }
    }
}

function CompareArrayAsync(arrSrc, arrDest) {
    var arrLarge = arrSrc.length > arrDest.length ? arrSrc : arrDest;
    var arrSmall = arrSrc.length > arrDest.length ? arrDest : arrSrc;
    for (var i = 0; i < arrLarge.length; i++)
        if (Search(arrSmall, arrLarge[i]) == -1)
            return i;
    return -1;
}
function CompareArray(arrSrc, arrDest) {
    for (var i = 0; i < arrSrc.length; i++)
        if (arrSrc[i] != arrDest[i])
            return i;
    return -1;
}
function Search(arrSrc, element) {
    for (var i = 0; i < arrSrc.length; i++)
        if (arrSrc[i] == element)
            return i;
    return -1;
}
function CompareArrayLast(arrSrc, arrDest) {
    for (var i = arrSrc.length - 1; i >= 0; i--)
        if (arrSrc[i] != arrDest[i])
            return i;
    return -1;
}

function StartSort(event, ui) {
    ui.placeholder.width(ui.item.width());
    ui.placeholder.height(ui.item.height());
}

function LoadSortables() {
    if (pscjq("#psc-divPanelTop-Content") != null) {
        pscjq("#psc-divPanelTop-Content").sortable({
            connectWith: arrayDivConnectWith,
            handle: ".psc-divPortlet-Header",
            stop: OnChange,
            placeholder: "portlet-placeholder ui-corner-all",
            start: StartSort
        });
    }
    if (pscjq("#psc-divPanelLeft-Content") != null) {
        pscjq("#psc-divPanelLeft-Content").sortable({
            connectWith: arrayDivConnectWith,
            handle: ".psc-divPortlet-Header",
            stop: OnChange,
            placeholder: "portlet-placeholder ui-corner-all",
            start: StartSort
        });
    }
    pscjq("#psc-divPanelCenter-Content").sortable({
        connectWith: arrayDivConnectWith,
        handle: ".psc-divPortlet-Header",
        stop: OnChange,
        placeholder: "portlet-placeholder ui-corner-all",
        start: StartSort
    });
    if (pscjq("#psc-divPanelRight-Content") != null) {
        pscjq("#psc-divPanelRight-Content").sortable({
            connectWith: arrayDivConnectWith,
            handle: ".psc-divPortlet-Header",
            stop: OnChange,
            placeholder: "portlet-placeholder ui-corner-all",
            start: StartSort
        });
    }
    if (pscjq("#psc-divPanelBottom-Content") != null) {
        pscjq("#psc-divPanelBottom-Content").sortable({
            connectWith: arrayDivConnectWith,
            handle: ".psc-divPortlet-Header",
            stop: OnChange,
            placeholder: "portlet-placeholder ui-corner-all",
            start: StartSort
        });
    }

   


    strStatus = Serialize();
}

function Serialize() {
    var top = "";
    if (pscjq("#psc-divPanelTop-Content")) {
        top = pscjq("#psc-divPanelTop-Content").sortable("serialize", { key: "portlet" });
        //top = Sortable.serialize("psc-divPanelTop", { name: "portlet" });
    }
    var left = "";
    if (pscjq("#psc-divPanelLeft-Content")) {
        left = pscjq("#psc-divPanelLeft-Content").sortable("serialize", { key: "portlet" });
        //left = Sortable.serialize("pnLeftDisplay", { name: "portlet" });
    }
    var center = "";
    if (pscjq("#psc-divPanelCenter-Content")) {
        center = pscjq("#psc-divPanelCenter-Content").sortable("serialize", { key: "portlet" });
        //center = Sortable.serialize("pnCenterDisplay", { name: "portlet" });
    }
    var right = "";
    if (pscjq("#psc-divPanelRight-Content")) {
        right = pscjq("#psc-divPanelRight-Content").sortable("serialize", { key: "portlet" });
        //right = Sortable.serialize("psc-divPanelRight", { name: "portlet" });
    }
    var bottom = "";
    if (pscjq("#psc-divPanelBottom-Content")) {
        bottom = pscjq("#psc-divPanelBottom-Content").sortable("serialize", { key: "portlet" });
        //bottom = Sortable.serialize("psc-divPanelBottom", { name: "portlet" });
    }



    var temp = top + "|" + left + "|" + center + "|" + right + "|" + bottom;
    return temp;
}


//                                 End Sortable Portlet
//===================================================================================================//


//                                            Form
//===================================================================================================//

function pageLoad() {
   
     LoadSortables();

    // Init toggle collapse
    // pscjq(".psc-divPortlet-Content").hide();

    // Add toggle into .psc-divPortlet-Header
    // Ánh Ngọc -15122015- thêm đóng mở cho phần thêm mới panel - portlet
    pscjq(".quantri")
       .addClass("ui-widget ui-widget-content ui-helper-clearfix ui-corner-all")
     .find(".qt-header")
       .addClass("ui-widget-header ui-corner-all")
       .append("<span class='ui-icon ui-icon-minusthick portlet-toggle'></span>");

    // end
    pscjq(".psc-divPortlet-Wrapper")
     .addClass("ui-widget ui-widget-content ui-helper-clearfix ui-corner-all")
     .find(".psc-divPortlet-Header")
       .addClass("ui-widget-header ui-corner-all")
       .prepend("<span class='ui-icon ui-icon-minusthick portlet-toggle'></span>");

    pscjq(".portlet-toggle").click(function () {
        var icon = pscjq(this);
        icon.toggleClass("ui-icon-minusthick ui-icon-plusthick");
        icon.closest(".psc-divPortlet-Wrapper").find(".psc-divPortlet-Content").toggle();
        icon.closest(".quantri").find(".qt-content").toggle();
    });

   
}


// This fuction is called when User click on save, cancel or close button in form Edit.
function EditRW_ClientClose(sender, args) {
    if (args.get_argument() == null || !args.get_argument().IsOK)
        return;
    switch (dialogMethod) {
        case "PortletEditCss":
            {
                PortletEditCSSUpdate(sessionStorage.getItem("idPortlet"), args.get_argument().Style);
            }
            break;
        case "editPortlet":
            {
                PostBack();
            }
            break;
        case "PanelEditCss":
            {
                PanelStyleUpdate(sessionStorage.getItem("idPortlet"), args.get_argument().Style);
            }
            break;
    }

}


function CallWebMethodSuccess(results, context, methodName) {
    switch (methodName) {
        case "PanelStyleGet":
            {
                PanelStyleGetCallback(results, context, methodName);
            }
            break;
        case "PanelStyleUpdate":
            {
                PanelStyleUpdateCallback(results, context, methodName);
            }
            break;
        case "PanelRemove":
            {
                PanelRemoveCallback(results, context, methodName);
            }
            break;
        case "PortletInstanceAdd":
            {
                PortletInstanceAddCallback(results, context, methodName);
            }
            break;
        case "PortletRemove":
            {
                PortletRemoveCallback(results, context, methodName);
            }
            break;
        case "PortletEditCSSGet":
            {
                PortletEditCSSGetCallback(results, context, methodName);
            }
            break;
        case "PortletEditCSSUpdate":
            {
                PortletEditCSSUpdateCallback(results, context, methodName);
            }
            break;
        case "PortletInstanceCollectionGet":
            {
                LoadPortletReferenceCallback(results, context, methodName);
            }
            break;
        case "PortletInstanceReferenceAdd":
            {
                PortletInstanceReferenceAddCallback(results, context, methodName);
            }
            break;
        case "PanelEditCss":
            {
                PostBack();
            }
            break;
            
    }
}


function CallWebMethodFailed(results, context, methodName) {
    radalert(results._message, 250, 100, "Cảnh báo");
}

// function PagePreview() {
    
    // dialogMethod = "PagePreview";
    // var oWnd = document.getElementById("rwPageStructure");
    // //            oWnd.setSize(400, 400);
    // //            oWnd.maximize();
    // console.log(oWnd);
    // oWnd.setUrl(String.format("/{0}", window.location.search));
    // oWnd.show();
// }

//Binding data into combobox
function DropDownListBind(listNameValuePair, dropdownlistName) {
    var dropdownlist = document.getElementById(dropdownlistName);
    dropdownlist.innerHTML = "";

    for (var i = 0; i < listNameValuePair.length; i++) {
        var item = document.createElement("option");
        item.innerHTML = listNameValuePair[i].Name;
        item.value = listNameValuePair[i].Id;
        dropdownlist.appendChild(item);
    }
}

//                                        EndForm
//===================================================================================================//

//                                        Portlet
//===================================================================================================//

//Load
function LoadPortletReference(id) {
    PageMethods.PortletInstanceCollectionGet(id, CallWebMethodSuccess, CallWebMethodFailed);
}

function LoadPortletReferenceCallback(results, context, methodName) {
    var portlets = Sys.Serialization.JavaScriptSerializer.deserialize(results);
    DropDownListBind(portlets, "ddlPagePortlet");
}

//Add

function PortletInstanceAdd() {
    var ddlPortlet = document.getElementById("ddlPortlet");
    var ddlPanelAddPortlet = document.getElementById("ddlPanelAddPortlet");
    var txtPortletInstanceName = document.getElementById("txtPortletInstanceName");
    PageMethods.PortletInstanceAdd(ddlPortlet.value, txtPortletInstanceName.value, ddlPanelAddPortlet.value, CallWebMethodSuccess, CallWebMethodFailed);

}

function PortletInstanceReferenceAdd() {
    ddlPagePortlet = document.getElementById("ddlPagePortlet");
    ddlPagePanelAdd = document.getElementById("ddlPagePanelAdd");
    PageMethods.PortletInstanceReferenceAdd(ddlPagePortlet.value, ddlPagePanelAdd.value, CallWebMethodSuccess, CallWebMethodFailed);
}

function PortletInstanceReferenceAddCallback(results, context, methodName) {
    PostBack();
}
function PortletInstanceAddCallback(results, context, methodName) {
    if (results != "")
    {
        radalert(results, 300, 100, "Chú ý");
        return;
    }
    else
    PostBack();
}

//Edit Data
//Called by PSCPortal.Engine.PortletControl.cs
function PortletEditData(id) {
    dialogMethod = "editPortlet";
    editWindow.setSize(800, 600);
    editWindow.setUrl(String.format("PortletEdit.aspx?id={0}", id));
    editWindow.show();

}

function PortletEditCSSUpdateCallback(results, context, methodName) {
    PostBack();
    //document.getElementById("portlet_" + context.Id).style.cssText = context.Style;
}

//Edit Style
function PortletEditCSSUpdate(idPortlet, css) {
    PageMethods.set_defaultUserContext({ Id: idPortlet, Style: css });
    PageMethods.PortletEditCSSUpdate(idPortlet, css, CallWebMethodSuccess, CallWebMethodFailed);
}

//Called by PSCPortal.Engine.PortletControl.cs
function PortletEditCSS(idPortlet) {
    PortletEditCSSGet(idPortlet);
}

function PortletEditCSSGet(idPortlet) {
    PageMethods.set_defaultUserContext(idPortlet);
    PageMethods.PortletEditCSSGet(idPortlet, CallWebMethodSuccess, CallWebMethodFailed);
}

function PortletEditCSSGetCallback(results, context, methodName) {
    sessionStorage.setItem('editResults', results);
    sessionStorage.setItem('idPortlet', context);
    dialogMethod = "PortletEditCss";
    editWindow.setSize(700, 550);
    editWindow.setUrl("/Controls/CSSEditor.aspx");
    editWindow.show();
}


//Delete
//Called by PSCPortal.Engine.PortletControl.cs

function PortletRemove(idPortlet, name) {

    function PortletRemoveConfirmCallback(arg, obj) {
        if (arg)
        PageMethods.PortletRemove(idPortlet, CallWebMethodSuccess, CallWebMethodFailed);
    }
    radconfirm("Bạn có muốn xóa portlet " + name + " ?", PortletRemoveConfirmCallback, 330, 180, null, "Confirm");
}

function PortletRemoveCallback(results, context, methodName) {
    PostBack();
}

//                                         EndPortlet
//===================================================================================================//

//                                          Pannel
//===================================================================================================//

//Add

//Edit

//Called by PSCPortal.Engine.PanelInPage.cs
//Edit css pannel
function PanelStyleGet(idPanel) {
    //dialogMethod = "P";
    PageMethods.set_defaultUserContext(idPanel);
    PageMethods.PanelStyleGet(idPanel, CallWebMethodSuccess, CallWebMethodFailed);
}

function PanelStyleGetCallback(results, context, methodName) {
    sessionStorage.setItem('editResults', results);
    sessionStorage.setItem('idPortlet', context);
    dialogMethod = "PanelEditCss";
    editWindow.setSize(700, 550);
    editWindow.setUrl("/Controls/CSSEditor.aspx");
    editWindow.show();

}

function PanelStyleUpdate(idPanel, style) {
    dialogMethod = "PanelEditCss";
    PageMethods.set_defaultUserContext(style);
    PageMethods.PanelStyleUpdate(idPanel, style, CallWebMethodSuccess, CallWebMethodFailed);
}
function PanelStyleUpdateCallback(results, context, methodName) {
    //var panel = document.getElementById("pn" + results + "Display");
    //panel.style.cssText = "border-width:1px;border-style:Solid;" + context;
    //document.getElementById("pn" + results + "Title").style.width = panel.style.width;
    PostBack();
}


//Delete

//Called by PSCPortal.Engine.PanelInPage.cs
function PanelRemove(idPanel) {
    if (confirm("Bạn có muốn xóa ?"))
        PageMethods.PanelRemove(idPanel, CallWebMethodSuccess, CallWebMethodFailed);
}

function PanelRemoveCallback(results, context, methodName) {
    PostBack();
}

//EndPannel
//===================================================================================================//