function CallWebMethodSuccess(results, context, methodName) {
    switch (methodName) {
        case "Save":
            {
                if (txtName.value == "")
                {
                    radalert("vui lòng nhập vào tên bài viết !!", 250, 100, "Chú ý");                
                }
                else
                {
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
    if (reDescription.get_html(true).length > 350) {
        alert("Mô tả không được dài quá 500 ký tự !!");
        return;
    }
   
    PageMethods.Save(txtName.value, txtTitle.value, rdiCreatedDate.get_selectedDate().format("MM/dd/yyyy HH:mm:ss"), rdiModifiedDate.get_selectedDate().format("MM/dd/yyyy HH:mm:ss"), rcbPage.get_value(), reDescription.get_html(true), reContent.get_html(true), IsVisibleCreateDate.checked, txtArticleHang.get_selectedDate().format("MM/dd/yyyy HH:mm:ss"), cbxArticleHang.checked, rcbArticleTemplate.get_value(), cbxComment.checked, txtDocument.get_value(), txtAlbum.get_value(), CallWebMethodSuccess, CallWebMethodFailed);
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
    txtArticleHang.set_visible(cbxArticleHang.checked ? true : false);
    cbxNews_checked();
}
function cbxArticleHang_checked() {
    txtArticleHang.set_visible(cbxArticleHang.checked ? true : false);
}
function cbxNews_checked() {
    if (cbxNews.checked)
        document.getElementById('albumForm').style.display = "inline";
    else {
        document.getElementById('albumForm').style.display = "none";
        txtAlbum.set_value("");
        txtDocument.set_value("");
    }
}