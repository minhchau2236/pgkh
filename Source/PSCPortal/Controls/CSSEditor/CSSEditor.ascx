<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CSSEditor.ascx.cs" Inherits="CssEditor.CSSEditor" %>
<style type="text/css">
    .ABCDEFGHIJKL{height:30px; padding:15px 0 0 0; color:#fff; background:#3A94C4; border: 1px solid #fff; text-align:center;}
    .txt-readonly{background-color:#bfbfbf; height:40px; margin-top:10px; border:0; padding: 5px;}
</style>
<script language="javascript" type="text/javascript">
    function OnLoadCSSEditor() {
        window.dialogArguments = sessionStorage.getItem("editResults");
        var hdfSign = document.getElementById('<%=hdfSign.ClientID %>');
        if (hdfSign.value == 0) {
            var hdf = document.getElementById('<%=hdfCssValue.ClientID %>');
            hdf.value = window.dialogArguments;
            var txt = document.getElementById('<%=txt.ClientID %>');
                    txt.value = window.dialogArguments;
                    hdfSign.value = 1;
                    javascript: __doPostBack('<%=hdfCssValue.ClientID%>', "");
        }
    }
    function OnAccept() {
        var oArg = {};
        oArg.IsOK = true;
        oArg.Style = document.getElementById('<%=txtCss.ClientID %>').value;
                var oWnd = GetRadWindow();
                oWnd.close(oArg);
            }
            function OnCancel() {
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
<table style="width: 50%;" border="0" cellpadding="0" cellspacing="0">
    <tr style="display:none;">
        <td style="width: 150px;" valign="top">
            <asp:ListBox ID="lstCssStyle" runat="server" Height="315px" Width="100px"
                AutoPostBack="True"
                OnSelectedIndexChanged="lstCssStyle_SelectedIndexChanged">
                <asp:ListItem>Font</asp:ListItem>
                <asp:ListItem>Block</asp:ListItem>
                <asp:ListItem>Background</asp:ListItem>
                <asp:ListItem>Border</asp:ListItem>
                <asp:ListItem>Box</asp:ListItem>
                <asp:ListItem>Position</asp:ListItem>
                <asp:ListItem>Layout</asp:ListItem>
                <asp:ListItem>List</asp:ListItem>
                <asp:ListItem>Table</asp:ListItem>
            </asp:ListBox>
        </td>
        <td valign="top">
            <asp:Panel ID="Panel1" runat="server">
                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                    <asp:View ID="View1" runat="server">
                        <table style="width: 100%;">
                            <tr>
                                <td width="100px">font-family:</td>
                                <td colspan="2">
                                    <asp:DropDownList ID="ddlFont" runat="server" Width="200px" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlFont_SelectedIndexChanged">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>Arial, Helvetica, sans-serif</asp:ListItem>
                                        <asp:ListItem Value="'Times New Roman', Times, serif">Times New Roman, Times, 
                                        serif</asp:ListItem>
                                        <asp:ListItem Value="'Courier New', Courier, monospace">Courier New, Courier, 
                                        monospace</asp:ListItem>
                                        <asp:ListItem>Arial</asp:ListItem>
                                        <asp:ListItem Value="'Arial Black'">Arial Black</asp:ListItem>
                                        <asp:ListItem Value="'Arial Narrow'">Arial Narrow</asp:ListItem>
                                        <asp:ListItem Value="Courier"></asp:ListItem>
                                        <asp:ListItem Value="'Courier New'">Courier New</asp:ListItem>
                                        <asp:ListItem Value="'Times New Roman'">Times New Roman</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>font-size:</td>
                                <td width="250px">
                                    <asp:DropDownList ID="ddlFontSize" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlFontSize_SelectedIndexChanged" Width="150px">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>xx-small</asp:ListItem>
                                        <asp:ListItem>small</asp:ListItem>
                                        <asp:ListItem>medium</asp:ListItem>
                                        <asp:ListItem>large</asp:ListItem>
                                        <asp:ListItem>x-large</asp:ListItem>
                                        <asp:ListItem>xx-large</asp:ListItem>
                                        <asp:ListItem>smaller</asp:ListItem>
                                        <asp:ListItem>larger</asp:ListItem>
                                        <asp:ListItem>inherit</asp:ListItem>
                                        <asp:ListItem>(value)</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtFontsize" runat="server" AutoPostBack="True"
                                        OnTextChanged="txtFontsize_TextChanged" Visible="False" Width="50px"></asp:TextBox>
                                </td>
                                <td>text-decoration:</td>
                            </tr>
                            <tr>
                                <td>font-weight:</td>
                                <td>
                                    <asp:DropDownList ID="ddlFontWeight" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlFontWeight_SelectedIndexChanged" Width="150px">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>normal</asp:ListItem>
                                        <asp:ListItem>bold</asp:ListItem>
                                        <asp:ListItem>lighter</asp:ListItem>
                                        <asp:ListItem>bolder</asp:ListItem>
                                        <asp:ListItem>100</asp:ListItem>
                                        <asp:ListItem>200</asp:ListItem>
                                        <asp:ListItem>300</asp:ListItem>
                                        <asp:ListItem>400</asp:ListItem>
                                        <asp:ListItem>500</asp:ListItem>
                                        <asp:ListItem>600</asp:ListItem>
                                        <asp:ListItem>700</asp:ListItem>
                                        <asp:ListItem>800</asp:ListItem>
                                        <asp:ListItem>900</asp:ListItem>
                                        <asp:ListItem>inherit</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkUnderline" runat="server" AutoPostBack="True"
                                        OnCheckedChanged="chkUnderline_CheckedChanged" Text="UnderLine" />
                                </td>
                            </tr>
                            <tr>
                                <td>font-style:</td>
                                <td>
                                    <asp:DropDownList ID="ddlFontStyle" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlFontStyle_SelectedIndexChanged" Width="150px">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>italic</asp:ListItem>
                                        <asp:ListItem>normal</asp:ListItem>
                                        <asp:ListItem>inherit</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkOverline" runat="server" AutoPostBack="True"
                                        OnCheckedChanged="chkOverline_CheckedChanged" Text="OverLine" />
                                </td>
                            </tr>
                            <tr>
                                <td>font-variant:</td>
                                <td>
                                    <asp:DropDownList ID="ddlFontVariant" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlFontVariant_SelectedIndexChanged" Width="150px">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>normal</asp:ListItem>
                                        <asp:ListItem>inherit</asp:ListItem>
                                        <asp:ListItem>small-caps</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkLineThrough" runat="server" AutoPostBack="True"
                                        OnCheckedChanged="chkLineThrough_CheckedChanged" Text="Line-Through" />
                                </td>
                            </tr>
                            <tr>
                                <td>text-transform:</td>
                                <td>
                                    <asp:DropDownList ID="ddlTextTransform" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlTextTransform_SelectedIndexChanged"
                                        Width="150px">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>capitalize</asp:ListItem>
                                        <asp:ListItem>lowercase</asp:ListItem>
                                        <asp:ListItem>none</asp:ListItem>
                                        <asp:ListItem>uppercase</asp:ListItem>
                                        <asp:ListItem>inherit</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkBlink" runat="server" AutoPostBack="True"
                                        OnCheckedChanged="chkBlink_CheckedChanged" Text="Blink" />
                                </td>
                            </tr>
                            <tr>
                                <td>color</td>
                                <td>
                                    <asp:TextBox ID="txtFontColor" runat="server" Style="width: 128px"
                                        OnTextChanged="txtFontColor_TextChanged" Width="150px"></asp:TextBox>
                                    <img id="fontColor" alt="" src="Images/FColor.jpg"
                                        onclick="return fontColor_onclick()" /></td>
                                <td>
                                    <asp:CheckBox ID="chkNone" runat="server" AutoPostBack="True"
                                        OnCheckedChanged="chkNone_CheckedChanged" Text="None" />
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="View2" runat="server">
                        <table style="width: 60%;">
                            <tr>
                                <td width="100px">line-height:</td>
                                <td>
                                    <asp:DropDownList ID="ddlLineHeight" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlLineHeight_SelectedIndexChanged" Width="100px">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>normal</asp:ListItem>
                                        <asp:ListItem>inherit</asp:ListItem>
                                        <asp:ListItem>(value)</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtLineHeight" runat="server" AutoPostBack="True"
                                        OnTextChanged="txtLineHeight_TextChanged" Visible="False" Width="40px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>vertical-align:</td>
                                <td width="150px">
                                    <asp:DropDownList ID="ddlVerticalAlign" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlVerticalAlign_SelectedIndexChanged"
                                        Width="150px">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>baseline</asp:ListItem>
                                        <asp:ListItem>bottom</asp:ListItem>
                                        <asp:ListItem>middle</asp:ListItem>
                                        <asp:ListItem>sub</asp:ListItem>
                                        <asp:ListItem>supper</asp:ListItem>
                                        <asp:ListItem>text-bottom</asp:ListItem>
                                        <asp:ListItem>text-top</asp:ListItem>
                                        <asp:ListItem>top</asp:ListItem>
                                        <asp:ListItem>inherit</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>text-align:</td>
                                <td>
                                    <asp:DropDownList ID="ddlTextAlign" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlTextAlign_SelectedIndexChanged" Width="150px">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>center</asp:ListItem>
                                        <asp:ListItem>justify</asp:ListItem>
                                        <asp:ListItem>left</asp:ListItem>
                                        <asp:ListItem>right</asp:ListItem>
                                        <asp:ListItem>inherit</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>text-indent:</td>
                                <td>
                                    <asp:DropDownList ID="ddlTextIndent" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlTextIndent_SelectedIndexChanged" Width="150px">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>inherit</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>white-space:</td>
                                <td>
                                    <asp:DropDownList ID="ddlWhiteSpace" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlWhiteSpace_SelectedIndexChanged" Width="150px">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>normal</asp:ListItem>
                                        <asp:ListItem>nowrap</asp:ListItem>
                                        <asp:ListItem>pre</asp:ListItem>
                                        <asp:ListItem>pre-line</asp:ListItem>
                                        <asp:ListItem>pre-wrap</asp:ListItem>
                                        <asp:ListItem>inherit</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>word-spacing:</td>
                                <td>
                                    <asp:DropDownList ID="ddlWordSpacing" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlWordSpacing_SelectedIndexChanged" Width="150px">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>normal</asp:ListItem>
                                        <asp:ListItem>inherit</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>letter-spacing:</td>
                                <td>
                                    <asp:DropDownList ID="ddlLetterSpacing" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlLetterSpacing_SelectedIndexChanged"
                                        Width="150px">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>normal</asp:ListItem>
                                        <asp:ListItem>inherit</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="View3" runat="server">
                        <table style="width: 100%;">
                            <tr>
                                <td width="150px">background-color:</td>
                                <td>
                                    <asp:TextBox ID="txtBackgroundColor" runat="server" AutoPostBack="True"
                                        OnTextChanged="txtBackgroundColor_TextChanged" Width="150px"></asp:TextBox>
                                    <img id="colorBrowse" alt="" src="Images/FColor.jpg"
                                        onclick="return colorBrowse_onclick()" /></td>
                            </tr>
                            <tr>
                                <td>background-image:</td>
                                <td>
                                    <asp:TextBox ID="txtBackgroundImage" runat="server"
                                        OnTextChanged="txtBackgroundImage_TextChanged" Width="150px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>background-repeat:</td>
                                <td>
                                    <asp:DropDownList ID="ddlBackgroundRepeat" runat="server"
                                        OnSelectedIndexChanged="ddlBacKgroundRepeat_SelectedIndexChanged"
                                        AutoPostBack="True" Width="150px">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>repeat</asp:ListItem>
                                        <asp:ListItem>no-repeat</asp:ListItem>
                                        <asp:ListItem>repeat-x</asp:ListItem>
                                        <asp:ListItem>repeat-y</asp:ListItem>
                                        <asp:ListItem>inherit</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>background-attachment:</td>
                                <td width="150-">
                                    <asp:DropDownList ID="ddlBackgroundAttachment" runat="server"
                                        OnSelectedIndexChanged="ddlBackgroundAttachment_SelectedIndexChanged"
                                        AutoPostBack="True" Width="150px">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>fixed</asp:ListItem>
                                        <asp:ListItem>scroll</asp:ListItem>
                                        <asp:ListItem>inherit</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>(x) background-position:</td>
                                <td>
                                    <asp:DropDownList ID="ddlBackgroundPositionX" runat="server"
                                        OnSelectedIndexChanged="ddlBackgroundPositionX_SelectedIndexChanged"
                                        AutoPostBack="True" Width="100px">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>center</asp:ListItem>
                                        <asp:ListItem>left</asp:ListItem>
                                        <asp:ListItem>right</asp:ListItem>
                                        <asp:ListItem>inherit</asp:ListItem>
                                        <asp:ListItem>(value)</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtBackgroundPositionX" runat="server" AutoPostBack="True"
                                        OnTextChanged="txtBackgroundPositionX_TextChanged" Visible="False"
                                        Width="50px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>(y) backgroundposition</td>
                                <td>
                                    <asp:DropDownList ID="ddlBackgroundPositionY" runat="server"
                                        OnSelectedIndexChanged="ddlBackgroundPositionY_SelectedIndexChanged"
                                        AutoPostBack="True" Width="100px">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>bottom</asp:ListItem>
                                        <asp:ListItem>center</asp:ListItem>
                                        <asp:ListItem>top</asp:ListItem>
                                        <asp:ListItem>(value)</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtBackgroundPositionY" runat="server" AutoPostBack="True"
                                        OnTextChanged="txtBackgroundPositionY_TextChanged" Visible="False"
                                        Width="50px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="View4" runat="server">
                        <table style="width: 50%;">
                            <tr>
                                <td>border-style:</td>
                                <td>
                                    <asp:DropDownList ID="ddlBorderStyle" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlBorderStyle_SelectedIndexChanged" Width="150px">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>dashed</asp:ListItem>
                                        <asp:ListItem>dotted</asp:ListItem>
                                        <asp:ListItem>double</asp:ListItem>
                                        <asp:ListItem>groove</asp:ListItem>
                                        <asp:ListItem>hidden</asp:ListItem>
                                        <asp:ListItem>inset</asp:ListItem>
                                        <asp:ListItem>none</asp:ListItem>
                                        <asp:ListItem>outset</asp:ListItem>
                                        <asp:ListItem>ridge</asp:ListItem>
                                        <asp:ListItem>solid</asp:ListItem>
                                        <asp:ListItem>inherit</asp:ListItem>
                                    </asp:DropDownList>
                                </td>

                            </tr>
                            <tr>
                                <td>border-width:</td>
                                <td>
                                    <asp:DropDownList ID="ddlBorderWidth" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlBorderWidth_SelectedIndexChanged" Width="100px">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>think</asp:ListItem>
                                        <asp:ListItem>medium</asp:ListItem>
                                        <asp:ListItem>thick</asp:ListItem>
                                        <asp:ListItem>inherit</asp:ListItem>
                                        <asp:ListItem>(value)</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtBorderWidth" runat="server" AutoPostBack="True"
                                        OnTextChanged="txtBorderWidth_TextChanged" Visible="False" Width="50px"></asp:TextBox>
                                </td>

                            </tr>
                            <tr>
                                <td>border-color:</td>
                                <td>
                                    <asp:TextBox ID="txtBorderColor" runat="server" AutoPostBack="True"
                                        OnTextChanged="txtBorderColor_TextChanged" Width="150px"></asp:TextBox>
                                    <img id="borderColorBrowse" alt="" src="Images/FColor.jpg" style="height: 16px"
                                        onclick="BorderColorBrowse();" /></td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="View5" runat="server">
                        <table style="width: 50%;">
                            <tr>
                                <td width="100px">padding:</td>
                                <td>
                                    <asp:TextBox ID="txtPadding" runat="server" AutoPostBack="True"
                                        OnTextChanged="txtPadding_TextChanged"></asp:TextBox>
                                </td>

                            </tr>
                            <tr>
                                <td class="style1">margin:</td>
                                <td class="style1">
                                    <asp:TextBox ID="txtMargin" runat="server" AutoPostBack="True"
                                        OnTextChanged="txtMargin_TextChanged"></asp:TextBox>
                                </td>

                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="View6" runat="server">
                        <table style="width: 50%;">
                            <tr>
                                <td width="100px">position:</td>
                                <td>
                                    <asp:DropDownList ID="ddlPosition" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlPosition_SelectedIndexChanged">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>absolute</asp:ListItem>
                                        <asp:ListItem>fixed</asp:ListItem>
                                        <asp:ListItem>relative</asp:ListItem>
                                        <asp:ListItem>static</asp:ListItem>
                                        <asp:ListItem>inherit</asp:ListItem>
                                    </asp:DropDownList>
                                </td>

                            </tr>
                            <tr>
                                <td>z-index:</td>
                                <td>
                                    <asp:TextBox ID="txtPositionZIndex" runat="server" AutoPostBack="True"
                                        OnTextChanged="txtPositionZIndex_TextChanged"></asp:TextBox>
                                </td>

                            </tr>
                            <tr>
                                <td>width:</td>
                                <td>
                                    <asp:TextBox ID="txtPositionWidth" runat="server" AutoPostBack="True"
                                        OnTextChanged="txtPositionWidth_TextChanged"></asp:TextBox>
                                </td>

                            </tr>
                            <tr>
                                <td>height:</td>
                                <td>
                                    <asp:TextBox ID="txtPositionHeight" runat="server" AutoPostBack="True"
                                        OnTextChanged="txtPositionHeight_TextChanged"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>top:</td>
                                <td>
                                    <asp:TextBox ID="txtPositionTop" runat="server" AutoPostBack="True"
                                        OnTextChanged="txtPositionTop_TextChanged"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>right:</td>
                                <td>
                                    <asp:TextBox ID="txtPositionRight" runat="server" AutoPostBack="True"
                                        OnTextChanged="txtPositionRight_TextChanged"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>bottom:</td>
                                <td>
                                    <asp:TextBox ID="txtPositionBottom" runat="server" AutoPostBack="True"
                                        OnTextChanged="txtPositionBottom_TextChanged"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>left:</td>
                                <td>
                                    <asp:TextBox ID="txtPositionLeft" runat="server" AutoPostBack="True"
                                        OnTextChanged="txtPositionLeft_TextChanged"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="View7" runat="server">
                        <table style="width: 60%;">
                            <tr>
                                <td width="100px">visibility:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlvisibility" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlvisibility_SelectedIndexChanged" Width="100px">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>collapse</asp:ListItem>
                                        <asp:ListItem>hidden</asp:ListItem>
                                        <asp:ListItem>visible</asp:ListItem>
                                        <asp:ListItem>inherit</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td width="100px">overflow:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddloverflow" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddloverflow_SelectedIndexChanged">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>auto</asp:ListItem>
                                        <asp:ListItem>hidden</asp:ListItem>
                                        <asp:ListItem>scroll</asp:ListItem>
                                        <asp:ListItem>visible</asp:ListItem>
                                        <asp:ListItem>inherit</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>display :
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddldisplay" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddldisplay_SelectedIndexChanged" Width="100px">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>inline</asp:ListItem>
                                        <asp:ListItem>inline-block</asp:ListItem>
                                        <asp:ListItem>inline-table</asp:ListItem>
                                        <asp:ListItem>list-item</asp:ListItem>
                                        <asp:ListItem>none</asp:ListItem>
                                        <asp:ListItem>run-in</asp:ListItem>
                                        <asp:ListItem>table</asp:ListItem>
                                        <asp:ListItem>table-caption</asp:ListItem>
                                        <asp:ListItem>table-cell</asp:ListItem>
                                        <asp:ListItem>table-column</asp:ListItem>
                                        <asp:ListItem>table-column-group</asp:ListItem>
                                        <asp:ListItem>table-footer-group</asp:ListItem>
                                        <asp:ListItem>table-header-group</asp:ListItem>
                                        <asp:ListItem>table-row</asp:ListItem>
                                        <asp:ListItem>table-row-group</asp:ListItem>
                                        <asp:ListItem>inherit</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>clip :
                                </td>
                                <td>rect(...)
                                </td>
                            </tr>
                            <tr>
                                <td>float:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlfloat" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlfloat_SelectedIndexChanged" Width="100px">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>left</asp:ListItem>
                                        <asp:ListItem>none</asp:ListItem>
                                        <asp:ListItem>right</asp:ListItem>
                                        <asp:ListItem>inherit</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>top:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddltop" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddltop_SelectedIndexChanged">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>auto</asp:ListItem>
                                        <asp:ListItem>inherit</asp:ListItem>
                                        <asp:ListItem>(value)</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtLayoutTop" runat="server" AutoPostBack="True" Width="50px"
                                        Visible="False" OnTextChanged="txtLayoutTop_TextChanged"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>clear:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlclear" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlclear_SelectedIndexChanged" Width="100px">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>both</asp:ListItem>
                                        <asp:ListItem>left</asp:ListItem>
                                        <asp:ListItem>none</asp:ListItem>
                                        <asp:ListItem>right</asp:ListItem>
                                        <asp:ListItem>inherit</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>right:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlright" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlright_SelectedIndexChanged">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>auto</asp:ListItem>
                                        <asp:ListItem>inherit</asp:ListItem>
                                        <asp:ListItem>(value)</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtLayoutRight" runat="server" AutoPostBack="True"
                                        Width="50px" Visible="False" OnTextChanged="txtLayoutRight_TextChanged"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>cursor:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlcursor" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlcursor_SelectedIndexChanged" Width="100px">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>default</asp:ListItem>
                                        <asp:ListItem>e-resize</asp:ListItem>
                                        <asp:ListItem>help</asp:ListItem>
                                        <asp:ListItem>move</asp:ListItem>
                                        <asp:ListItem>n-resize</asp:ListItem>
                                        <asp:ListItem>ne-resize</asp:ListItem>
                                        <asp:ListItem>nw-resize</asp:ListItem>
                                        <asp:ListItem>pointer</asp:ListItem>
                                        <asp:ListItem>progress</asp:ListItem>
                                        <asp:ListItem>s-resize</asp:ListItem>
                                        <asp:ListItem>se-resize</asp:ListItem>
                                        <asp:ListItem>sw-resize</asp:ListItem>
                                        <asp:ListItem>text</asp:ListItem>
                                        <asp:ListItem>w-resize</asp:ListItem>
                                        <asp:ListItem>wait</asp:ListItem>
                                        <asp:ListItem>inherit</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>bottom:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlbottom" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlbottom_SelectedIndexChanged">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>auto</asp:ListItem>
                                        <asp:ListItem>inherit</asp:ListItem>
                                        <asp:ListItem>(value)</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtLayoutBottom" runat="server" Width="50px"
                                        AutoPostBack="True" Visible="False"
                                        OnTextChanged="txtLayoutBottom_TextChanged"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td>left:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlleft" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlleft_SelectedIndexChanged">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>auto</asp:ListItem>
                                        <asp:ListItem>inherit</asp:ListItem>
                                        <asp:ListItem>(value)</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtLayoutLeft" runat="server" Width="51px" AutoPostBack="True"
                                        Visible="False" OnTextChanged="txtLayoutLeft_TextChanged"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="View8" runat="server">
                        <table>
                            <tr>
                                <td>list-style-type :
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlliststyletype" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlliststyletype_SelectedIndexChanged"
                                        Width="100px">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>dics</asp:ListItem>
                                        <asp:ListItem>circle</asp:ListItem>
                                        <asp:ListItem>square</asp:ListItem>
                                        <asp:ListItem>decimal</asp:ListItem>
                                        <asp:ListItem>decimal-leading-zero</asp:ListItem>
                                        <asp:ListItem>lower-roman</asp:ListItem>
                                        <asp:ListItem>upper-roman</asp:ListItem>
                                        <asp:ListItem>lower-greek</asp:ListItem>
                                        <asp:ListItem>lower-latin</asp:ListItem>
                                        <asp:ListItem>upper-latin</asp:ListItem>
                                        <asp:ListItem>armenian</asp:ListItem>
                                        <asp:ListItem>gieorgian</asp:ListItem>
                                        <asp:ListItem>lower-alpha</asp:ListItem>
                                        <asp:ListItem>lower-alpha</asp:ListItem>
                                        <asp:ListItem>none</asp:ListItem>
                                        <asp:ListItem>inherit</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>list-style-image :
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtliststyleimage" AutoPostBack="true"
                                        OnTextChanged="txtliststyleimage_TextChanged" Width="100px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>list-style-position :
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlliststyleposition" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlliststyleposition_SelectedIndexChanged"
                                        Width="100px">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>inside</asp:ListItem>
                                        <asp:ListItem>outside</asp:ListItem>
                                        <asp:ListItem>inherit</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="View9" runat="server">
                        <table style="width: 50%">
                            <tr>
                                <td width="100px">table-layout :
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddltablelayout" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddltablelayout_SelectedIndexChanged"
                                        Width="100px">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>auto</asp:ListItem>
                                        <asp:ListItem>fixed</asp:ListItem>
                                        <asp:ListItem>inherit</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>border-colapse :
                                </td>
                                <td width="100">
                                    <asp:DropDownList ID="ddlbordercolapse" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlbordercolapse_SelectedIndexChanged"
                                        Width="100px">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>colapse</asp:ListItem>
                                        <asp:ListItem>separate</asp:ListItem>
                                        <asp:ListItem>inherit</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>border-spacing :
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlborderspacing" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlborderspacing_SelectedIndexChanged"
                                        Width="50px">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>inherit</asp:ListItem>
                                        <asp:ListItem>(value)</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtTableBorderSpacing" runat="server" AutoPostBack="True"
                                        Width="50px" Visible="False"
                                        OnTextChanged="txtTableBorderSpacing_TextChanged"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>empty-cells :
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlemptycells" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlemptycells_SelectedIndexChanged" Width="100px">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>hide</asp:ListItem>
                                        <asp:ListItem>show</asp:ListItem>
                                        <asp:ListItem>inherit</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>caption-side :
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlcaptionside" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlcaptionside_SelectedIndexChanged"
                                        Width="100px">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>bottom</asp:ListItem>
                                        <asp:ListItem>top</asp:ListItem>
                                        <asp:ListItem>inherit</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td>
            <asp:Panel ID="Panel2" runat="server" CssClass="ABCDEFGHIJKL">
                Ví dụ: width:100%; float:left;
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td>
            <asp:TextBox ID="txtCss" runat="server" Height="100px" TextMode="MultiLine"
                Width="654px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td>
            <asp:TextBox ID="txt" runat="server" Width="650px" CssClass="txt-readonly" ></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td></td>
        <td style="padding-top:10px">
            <input id="btnAccept" type="button" value="Accept" onclick="OnAccept();" />
            <input id="btnCancel" type="button" value="Cancel" onclick="OnCancel();" />
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <p style="padding-top:20px; color:#3A94C4">Ghi chú: chỉ hỗ trợ các thuộc tính của CSS!</p>
        </td>
    </tr>
</table>

<asp:HiddenField ID="HiddenField1" runat="server" />

<asp:HiddenField ID="hdfCssValue" runat="server"
    OnValueChanged="hdfCssValue_ValueChanged" />


<asp:HiddenField ID="hdfSign" runat="server" Value="0" />



<script language="javascript" type="text/javascript">

    function colorBrowse_onclick() {
        var color = window.showModalDialog('ColorDialog.htm', '', 'dialogHeight:180px,dialodWidth : 400px');

        var colorTxt = document.getElementById('<%=txtBackgroundColor.ClientID%>');
        colorTxt.focus();
        if (color != undefined)
            colorTxt.value = color;
        __doPostBack('<%=HiddenField1.ClientID%>', '');
    }

    function fontColor_onclick() {
        var color = window.showModalDialog('ColorDialog.htm', '', 'dialogHeight:180px,dialodWidth : 400px');

        var colorTxt = document.getElementById('<%=txtFontColor.ClientID%>');
    colorTxt.focus();
    if (color != undefined)
        colorTxt.value = color;
    __doPostBack('<%=HiddenField1.ClientID%>', '');
}
function BorderColorBrowse() {
    var color = window.showModalDialog('ColorDialog.htm', '', 'dialogHeight:180px,dialodWidth : 400px');

    var colorTxt = document.getElementById('<%=txtBorderColor.ClientID%>');
    colorTxt.focus();
    if (color != undefined)
        colorTxt.value = color;
    __doPostBack('<%=HiddenField1.ClientID%>', '');
}
OnLoadCSSEditor();
</script>
