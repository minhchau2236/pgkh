<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PortletEdit.aspx.cs" Inherits="PSCPortal.PortletEdit" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Portlet Edit</title>
    <base target="_self">
     <script language="javascript" type="text/javascript">
         function Accept() {
             var oArg = {};
             oArg.IsOK = true;
             var oWnd = GetRadWindow();
             oWnd.close(oArg);
         }
         function Cancel() {
             var oArg = {};
             oArg.IsOK = false;
             var oWnd = GetRadWindow();
             oWnd.close(oArg);
         }

         function GetRadWindow() {
             var oWindow = null;
             if (window.radWindow) oWindow = window.radWindow;
             else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
             return oWindow;
         }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <asp:PlaceHolder ID="phPortlet" runat="server"></asp:PlaceHolder>
    </form>
</body>
</html>
