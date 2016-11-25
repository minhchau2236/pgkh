<%@ Page EnableViewState="false" Title="<%# Resources.Site.PortletDetail%>" Language="C#" MasterPageFile="~/Systems/DialogMasterPage.Master" AutoEventWireup="true" CodeBehind="PortletDetail.aspx.cs" Inherits="PSCPortal.Systems.Engine.PortletDetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script language="javascript" type="text/javascript">
        function CallWebMethodSuccess(results, context, methodName) {
            switch (methodName) {
                case "Save":
                    {
                        SaveCallback(results, context, methodName);
                    }
                    break;                             
            }
        }
        function CallWebMethodFailed(results, context, methodName) {
            alert(results._message);
        }
        function Save() {
			var txtName = document.getElementById("<%= txtName.ClientID %>");
			var txtDisplayURL = document.getElementById("<%= txtDisplayURL.ClientID %>");
            var txtEditURL = document.getElementById("<%= txtEditURL.ClientID %>");           
            if (!validateToSave(txtName.value, txtDisplayURL.value, txtEditURL.value)) {              
                return;
            }
            PageMethods.Save(txtName.value, txtDisplayURL.value, txtEditURL.value, CallWebMethodSuccess, CallWebMethodFailed);
        }
    function SaveCallback(results, context, methodName) {
        if (results) {
            var oWnd = GetRadWindow();
            oWnd.close(true);
        } else {
            //17122015
            //duong dan k ton tai          
                radalert("<strong style='color:red'>Đường dẫn không tồn tại!</strong>", 250, 100, "Thông báo");
            }
        }
        function Cancel() {
            var oWnd = GetRadWindow();
            oWnd.close(false); 
        }
        function validateToSave(Name,DisplayURL,EditURL) {           
            var valid = true;
            if (Name == '') {              
                radalert("<strong style='color:#FF2424'>Vui lòng nhập tên!</strong>", 250, 100);
                valid = false;
                return;
            }
            if (DisplayURL == '') {                
                radalert("<strong style='color:#FF2424'>Vui lòng nhập đường dẫn hiển thị!</strong>", 250, 100);
                valid = false;
                return;
            }
            if (EditURL == '') {
                radalert("<strong style='color:#FF2424'>Vui lòng nhập đường dẫn hiệu chỉnh!</strong>", 250, 100);
                valid = false;
                return;
            }
            return valid;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table style="width:100%;" align="center" cellpadding="4px;">
	<tr>
    	<td style="width:40%;" class="textinput" align="right"><%= Resources.Site.Id %>:</td>
        <td style="width:60%"><asp:TextBox Enabled="false" ID="txtId" runat="server" Width="200px"></asp:TextBox></td>
    </tr>
    <tr>
    	<td style="width:40%;" class="textinput" align="right"><%= Resources.Site.Name %>:</td>
        <td style="width:60%"><asp:TextBox ID="txtName" runat="server" Width="200px"></asp:TextBox></td>        
    </tr> 
    <tr>
    	<td style="width:40%;" class="textinput" align="right"><%= Resources.Site.PathToFileDisplay %>:</td>
        <td style="width:60%"><asp:TextBox ID="txtDisplayURL" runat="server" Width="200px"></asp:TextBox></td>
    </tr> 
    <tr>
    	<td style="width:40%;" class="textinput" align="right"><%= Resources.Site.PathToFileModify%>:</td>
        <td style="width:60%"><asp:TextBox ID="txtEditURL" runat="server" Width="200px"></asp:TextBox></td>
    </tr>      
    <tr align="center">
        <td colspan="2"><img src="/Systems/CMS/Image/line.jpg" width="438" height="1" alt="" /></td>
    </tr>
    <tr>
        <td colspan="2">
            <table style="width:15% ;" align="center">
                <tr>
                    <td style="width:50%;"><a href="javascript:void(0)" onclick="Save();" class="submit"><%= Resources.Site.Save %></a></td>
                    <td style="width:50%;"><a href="javascript:void(0)" onclick="Cancel();" class="submit"><%= Resources.Site.Cancel %></a></td>
                </tr>
            </table>
        </td>
    </tr>   
</table>    
</asp:Content>
