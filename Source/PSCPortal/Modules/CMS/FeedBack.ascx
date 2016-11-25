<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FeedBack.ascx.cs" Inherits="PSCPortal.Modules.CMS.FeedBack" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div style="background: #fff; float: left; margin-left: 20px;">
    <div class="title">
        <a href="#">Hỏi đáp</a></div>
    <div class="bg_news760">
        <!--content_topic-->
        <div style="padding-left: 15px; padding-right: 15px; color: #333; padding-bottom: 15px;
            padding-top: 35px;">
            <asp:Label ID="Label1" runat="server"><b> Ban giám hiệu trường rất mong nhận đựơc ý kiến. Xin cám ơn!</b></asp:Label>
            <br />
        </div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr>
                <td align="right" style="font-family: Arial; font-size: 11px; color: #333; font-weight: bold;
                    padding: 5px" width="80px">
                    Họ và tên:
                </td>
                <td align="left" style="width: 230px;">
                    <asp:TextBox ID="txtFullName" runat="server" Width="200px"></asp:TextBox>
                    <span style="color: Red">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                        runat="server" ControlToValidate="txtFullName" ErrorMessage="Họ và tên không được trống"
                        ValidationGroup="Feedback" ToolTip="Họ và tên không được trống" Display="None"></asp:RequiredFieldValidator>
                </td>
                <td align="left">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right" style="font-family: Arial; color: #333; font-size: 11px; font-weight: bold;
                    padding: 5px" width="80px">
                    Địa chỉ email:
                </td>
                <td align="left" style="width: 230px;">
                    <asp:TextBox ID="txtEmail" runat="server" Width="200px"></asp:TextBox>
                    <span style="color: Red">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                        runat="server" ControlToValidate="txtEmail" ErrorMessage="Email không được để trống"
                        ValidationGroup="Feedback" ToolTip="Email không được để trống" Display="None">(*)</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtEmail"
                        ErrorMessage="Email không hợp lệ" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        ValidationGroup="Feedback" ToolTip="Email không hợp lệ" Visible="True" Display="None">(*)</asp:RegularExpressionValidator>
                </td>
                <td align="left">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right" style="font-family: Arial; font-size: 11px; color: #333; font-weight: bold;
                    padding: 5px" width="80px">
                    Điện thoại:
                </td>
                <td align="left" style="width: 230px;">
                    <asp:TextBox ID="txtPhone" runat="server" Width="200px"></asp:TextBox>
                    <span style="color: Red">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                        runat="server" ControlToValidate="txtEmail" ErrorMessage="Chưa nhập số điện thoại!"
                        ValidationGroup="Feedback" ToolTip="Chưa nhập số điện thoại!" Display="None">(*)</asp:RequiredFieldValidator>
                </td>
                <td align="left">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right" style="font-family: Arial; color: #333; font-size: 11px; font-weight: bold;
                    padding: 5px">
                    Địa chỉ:
                </td>
                <td align="left" style="width: 230px;">
                    <asp:TextBox ID="txtAddress" runat="server" Width="200px"></asp:TextBox>
                    <span style="color: Red">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtAddress"
                        ErrorMessage="Địa chỉ không được để trống" ValidationGroup="Feedback" ToolTip="Địa chỉ không được để trống"
                        Display="None">(*)</asp:RequiredFieldValidator>
                </td>
                <td align="left">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right" style="font-family: Arial; color: #333; font-size: 11px; font-weight: bold;
                    padding: 5px" width="80px">
                    Tiêu đề:
                </td>
                <td align="left" style="width: 230px;">
                    <asp:TextBox ID="txtTitle" runat="server" Width="200px"></asp:TextBox>
                    <span style="color: Red">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTitle"
                        ErrorMessage="Tiêu đề không được trống" ValidationGroup="Feedback" ToolTip="Tiêu đề không được trống"
                        Display="None">(*)</asp:RequiredFieldValidator>
                </td>
                <td align="left">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right" style="font-family: Arial; font-size: 11px; font-weight: bold;
                    color: #fff; padding: 5px" colspan="3">
                    <telerik:RadEditor ID="fckContent" runat="server" Width="100%" Style="float: left; color:#fff;">
                        <Tools>
                            <telerik:EditorToolGroup>
                                <telerik:EditorTool Name="Bold" />
                                <telerik:EditorTool Name="Italic" />
                                <telerik:EditorTool Name="JustifyLeft" />
                                <telerik:EditorTool Name="JustifyRight" />
                                <telerik:EditorTool Name="JustifyCenter" />
                                <telerik:EditorTool Name="JustifyFull" />
                            </telerik:EditorToolGroup>
                        </Tools>
                        <Content>
                    
                        </Content>
                    </telerik:RadEditor>
                </td>
            </tr>
            <tr>
                <td colspan="3" align="left" style="font-family: Arial; color: #333; font-size: 11px;
                    font-weight: bold; padding-left: 90px">
                    File đính kèm nếu có (dung lượng nhỏ hơn 4 M)
                </td>
            </tr>
            <tr>
                <td align="right" style="font-family: Arial; font-size: 11px; color: #333; font-weight: bold;
                    padding: 5px">
                    File :
                </td>
                <td colspan="2" align="left">
                    <asp:FileUpload runat="server" ID="fileUpLoad" />
                </td>
            </tr>
            <tr>
                <td colspan="3" align="center" height="25px" style="padding-right: 100px">
                    <asp:Button ID="btnSend" runat="server" Text="Gửi" Width="50px" OnClick="btnSend_Click"
                        ValidationGroup="Feedback" Height="25px" />
                    <asp:Button ID="btnCancel" runat="server" Text="Hủy" OnClick="btnCancel_Click" Width="50px" />
                </td>
            </tr>
            <tr>
                <td align="left" style="font-family: Arial; font-size: 11px; color: #333; font-weight: bold;
                    padding: 5px" colspan="3">
                    Các ô có dấu <span style="color: Red; font-weight: bold; font-size: 12px">*</span>
                    bắt buộc phải nhập dữ liệu
                </td>
            </tr>
            <tr>
                <td align="center" colspan="3" style="font-family: Arial; font-size: small; color: #FF0000">
                    <asp:Label runat="server" ID="lbthongbao" Visible="false"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</div>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
    ShowSummary="False" ValidationGroup="Feedback" />
<%--<div class="news_L760">
    <div class="topic_news760">
        <a href='/?TopicId=<%#Topic.Id.ToString() %>'>
            <%#Topic.Name %></a>
    </div>
    <div class="bg_news760">
        <!--content_topic-->
        <telerik:RadListView ID="RadListView1" runat="server" AllowPaging="True">
            <ItemTemplate>
                <div class="ct_news760">
                    <a href="<%#"/?ArticleId="+Eval("Id") %>">
                        <%#Eval("Title")%></a>
                    <p>
                        <%# PSCPortal.CMS.Article.GetDescription((Guid) Eval("Id")) %>
                    </p>
                    
                </div>
            </ItemTemplate>
        </telerik:RadListView>
        <div style="clear: both; padding-top: 5px; float: right; padding-right: 5px;">
            <telerik:RadDataPager PageSize="10" ID="RadDataPager1" runat="server" PagedControlID="RadListView1"
                BackColor="White" BorderStyle="None" Height="30px">
                <Fields>
                    <telerik:RadDataPagerButtonField FieldType="FirstPrev" PageButtonCount="5" />
                    <telerik:RadDataPagerButtonField FieldType="Numeric" PageButtonCount="5" />
                    <telerik:RadDataPagerButtonField FieldType="NextLast" PageButtonCount="5" />
                </Fields>
            </telerik:RadDataPager>
        </div>
    </div>
</div>--%>
