var listRole;

function CallWebMethodSuccess(results, context, methodName) {
    switch (methodName) {
        case "GetData":
        {
            LoadDataCallback(results, context, methodName);
        }
        break;
        case "Save":
        {
            SaveCallback(results,context,methodName);
        }
        break;
    }   
}
function CallWebMethodFailed(results, context, methodName) {
    alert(results._message);
}

function LoadData()
{
    PageMethods.GetData(CallWebMethodSuccess, CallWebMethodFailed);
}
function LoadDataCallback(results, context, methodName){    
    var authenList = Sys.Serialization.JavaScriptSerializer.deserialize(results);
    listRole = authenList.RoleList;
    DrawTitle(authenList.MenuMasterPermissionList);    
    DrawData(authenList.MenuMasterPermissionList, authenList.RoleList, authenList.AuthenticationList);
}

function pageLoad(){    
    LoadData();
}
function DrawTitle(pagePermissionList) {
    var $container = $("#container");
    var $tieude = $("<div></div>")
    $tieude.addClass("Authentication_Title");
    $container.append($tieude);

    $firstColumn = $("<div></div>");
    $firstColumn.addClass("Authentication_Title_Item");
    $firstColumn.text("");
    $firstColumn.css({ 'border-left-width': '1px' });
    $tieude.append($firstColumn);

    for (var index in pagePermissionList) {
        var $item = $("<div></div>");
        $item.text(pagePermissionList[index].Name);
        $item.addClass("Authentication_Title_Item");
        $tieude.append($item);
    }

    //Toan quyen
    var $item = $("<div></div>");
    $item.text('Toàn quyền');
    $item.addClass("Authentication_Title_Item");
    $tieude.append($item);
}
function DrawData(pagePermissionList, roleList, authenticationList) {
    for (var i = 0; i < roleList.length; i++) {
        AddRowForRole(roleList[i], pagePermissionList, authenticationList[i]);
    }
}
function AddRowForRole(role, pagePermissionList, arrAuthenticationList) {
    var flag = true;

    var $container = $("#container");
    $row = $("<div></div>");
    $row.addClass("Authentication_Row");
    $container.append($row);

    var $roleName = $("<div></div>");
    $roleName.text(role.Name);
    $roleName.addClass("Authentication_RoleName");
    $row.append($roleName);

    for (var i = 0; i < pagePermissionList.length; i++) {
        var $item = $("<div></div>");
        $item.addClass("Authentication_Row_Item");
        $row.append($item);

        var $checkbox = $(String.format("<input name='{0}' type=checkbox />", role.Name));
        $checkbox.attr("checked", arrAuthenticationList[i]);
        $checkbox.change(function () {
            TrackStatus(role.Name);
        });
        $item.append($checkbox);

        if (!arrAuthenticationList[i])
            flag = false;
    }
    // Toan quyen
    var $itemFullPermission = $("<div></div>");
    $itemFullPermission.addClass("Authentication_Row_Item");
    $row.append($itemFullPermission);

    var $checkboxToanQuyen = $(String.format("<input name='{0}' type=checkbox />", role.Name));
    $checkboxToanQuyen.attr("checked", flag);
    $checkboxToanQuyen.change(function () {
        FullPermissionChangeStatus($(this).attr("Name"), $(this).attr("checked"));
    });
    $itemFullPermission.append($checkboxToanQuyen);
}

function TrackStatus(roleName) {
    var flag = true;
    var ctrlList = document.getElementsByName(roleName);
    var i = 0;
    for (; i < ctrlList.length - 1; i++) {
        $ctrl = $(ctrlList[i]);
        if (!$ctrl.attr("checked")) {
            flag = false;
        }
    }
    $ctrlToanQuyen = $(ctrlList[i]);
    $ctrlToanQuyen.attr("checked", flag);
}

function FullPermissionChangeStatus(roleName, value) {
    var ctrlList = document.getElementsByName(roleName);
    for (var j = 0; j < ctrlList.length - 1; j++) {
        var $ctrl = $(ctrlList[j]);
        $ctrl.attr("checked", value);
    }
}

function Save() {
    var result = new Array();
    for (var i = 0; i < listRole.length; i++) {
        var ctrlList = document.getElementsByName(listRole[i].Name);
        result[i] = new Array();
        for (var j = 0; j < ctrlList.length - 1; j++) {
            var $ctrl = $(ctrlList[j]);
            result[i][j] = $ctrl.attr("checked");
        }
    }
    PageMethods.Save(result, CallWebMethodSuccess, CallWebMethodFailed);
}





function SaveCallback(results, context, methodName){
    var oWnd = GetRadWindow();
    oWnd.close(false);
}
function Cancel() {
    var oWnd = GetRadWindow();
    oWnd.close(false); 
}



