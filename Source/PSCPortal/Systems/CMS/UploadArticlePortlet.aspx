<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadArticlePortlet.aspx.cs" Inherits="PSCPortal.Systems.CMS.UploadArticlePortlet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
   <div>
     <asp:FileUpload ID="FileUpLoadPic" runat="server" /> 
        <asp:Button ID="btnUploadPic" runat="server" Text="Tải lên" 
             onclick="btnUploadPic_Click" /> (.jpg, .gif)
    </div>
    </form>
</body>
</html>
