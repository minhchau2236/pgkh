<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImageUpload.aspx.cs" Inherits="PSCPortal.Portlets.Rotator.ImageUpload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
