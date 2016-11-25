<%@ Page Language="C#" MasterPageFile="~/Systems/DialogMasterPage.Master" AutoEventWireup="true" CodeBehind="VoteAnswerDetail.aspx.cs" Inherits="PSCPortal.Systems.CMS.VoteAnswerDetail" Title="Untitled Page" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %><asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
            radalert(results._message, 250, 100, "<%= Resources.Site.Warning %>");
        }
        function Save() {
            
			var txtName = document.getElementById("<%= txtName.ClientID %>");
            var txtNumber = document.getElementById("<%= txtNumber.ClientID %>");
            if (txtName.value == "")
                radalert("Bạn vui lòng nhập vào tên của câu trả lời !!", 250, 100, "Chú ý");
            else
                PageMethods.Save(txtName.value, txtNumber.value, CallWebMethodSuccess, CallWebMethodFailed);
        }       
		function SaveCallback(results, context, methodName) {
            var oWnd = GetRadWindow();
            oWnd.close(true); 
        }
        function Cancel() {
            var oWnd = GetRadWindow();
            oWnd.close(false); 
        }       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table style="width:100%;" align="center" cellpadding="4px;">
	<tr>
        <td style="width:40%;" class="textinput" align="right">
            <%= Resources.Site.Id %>:
        </td>
        <td style="width:60%">
				<asp:TextBox ID="txtId" runat="server" Width="200px"></asp:TextBox>
        </td>
        <td>
            &nbsp;
        </td>
    </tr>  
	<tr>
        <td style="width:40%;" class="textinput" align="right">
            <%= Resources.Site.Name %>:
        </td>
        <td style="width:60%">
				<asp:TextBox ID="txtName" runat="server" Width="200px"></asp:TextBox>
        </td>
        <td>
            &nbsp;
        </td>
    </tr>  
	<tr>
        <td style="width:40%;" class="textinput" align="right">
            Số lượng:
        </td>
        <td style="width:60%">
				<asp:TextBox ID="txtNumber" runat="server" Width="200px" Enabled="false"></asp:TextBox>
        </td>
        <td>
            &nbsp;
        </td>
    </tr>  
	<tr align="center">
        <td colspan="2"><img src="/Systems/CMS/Image/line.jpg" width="438" height="1" alt="" /></td>
    </tr>
    <tr>
        <td colspan="2">
            <table style="width:15%" align="center">
                <tr>
                    <td style="width:50%;"><a href="javascript:void(0)" onclick="Save();" class="submit"><%= Resources.Site.Save %></a></td>
                    <td style="width:50%"><a href="javascript:void(0)" onclick="Cancel();" class="submit"><%= Resources.Site.Cancel %></a></td>
                </tr>
            </table>
        </td>
    </tr>
</table>
</asp:Content>
