<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="CSSEditor.aspx.cs" Inherits="PSCPortal.Controls.CSSEditor" %>

<%@ Register src="CSSEditor/CSSEditor.ascx" tagname="CSSEditor" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>    
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>     
            <uc1:CSSEditor ID="CSSEditor1" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
