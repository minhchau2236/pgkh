<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadArticleImage.aspx.cs" Inherits="PSCPortal.Systems.CMS.UploadArticleImage" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .infoImg {
            padding-top: 2px;
        }

            .infoImg img {
                float: left;
                margin-right: 5px;
                width: 120px;
                height: 80px;
                border: 1px solid #6788be;
            }
    </style>
    <script type="text/javascript">
        var divImg;
        var btnDelImg;
        function pageLoad() {
            divImg = document.getElementById("<%=divImg.ClientID%>");
            btnDelImg = document.getElementById('btnDelImg');
            if ('<%#PSCPortal.CMS.Article.CheckImage(Args.Article.Id)%>'=="False")
                 btnDelImg.style.display="none";
        }

        function deleteImg() {
            var r = confirm("Bạn có muốn xóa!");
            if (r == false) {
                return;
            }
            PageMethods.DeleteImg();
            divImg.innerHTML = "<img style='float: left; margin-right: 5px; width: 120px; height: 80px; border: 1px solid #6788be' src='/Resources/ImagePhoto/noimage.jpg' />";
            btnDelImg.style.display="none";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager EnablePageMethods="true" ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <div>
            <asp:FileUpload ID="FileUpLoadPic" runat="server" />
            <asp:Button ID="btnUploadPic" runat="server" Text="Tải lên"
                OnClick="btnUploadPic_Click" />
            (jpg, png, gif, jpeg) 
            <div id="divImg" runat="server" class="infoImg">
            </div>
            <a id="btnDelImg" href="#" onclick="deleteImg()">Xóa</a>
        </div>
    </form>
</body>
</html>
