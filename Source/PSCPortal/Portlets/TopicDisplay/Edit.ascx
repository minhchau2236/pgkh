<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Edit.ascx.cs" Inherits="PSCPortal.Portlets.TopicDisplay.Edit" %>
<table cellpadding="0" cellspacing="0" width = "600px" align="center" border="0">
    <tr>
        <td>
            <fieldset border="1">
                <legend align="top" style="text-align: center">Thông tin cần nhập</legend>
                <table cellspacing="0" cellpadding="0" width="100%" style="height: 137px">
                    <tr>
                        <td align="right">
                        Số mục tin :&nbsp;&nbsp;&nbsp;
                        </td>                   
                        <td>
                            &nbsp;&nbsp;
                            <asp:TextBox ID="txtDisplayNumber" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                        Topic :&nbsp;&nbsp;&nbsp;
                        </td>                   
                        <td>
                            &nbsp;&nbsp;
                            <asp:DropDownList DataTextField="Path" DataValueField="Id" ID="ddlTopic" runat="server" Height="100px" Width="200px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            &nbsp;</td>                   
                        <td align="left">
                            <asp:Button ID="btnOk" runat="server" Text="Ok" Width="70px" 
                                onclick="btnSave_Click" />
                            &nbsp;<asp:Button ID="bntCancel" runat="server" Text="Cancel" 
                                onclick="btnCancel_Click" Width="70px" />
                        </td>
                    </tr>
                </table>
            </fieldset>        
        </td>
    </tr>
</table>