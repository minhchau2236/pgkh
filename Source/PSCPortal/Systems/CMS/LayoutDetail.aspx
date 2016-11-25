<%@ Page Title="" Language="C#" MasterPageFile="~/Systems/DialogMasterPage.Master" AutoEventWireup="true" CodeBehind="LayoutDetail.aspx.cs" Inherits="PSCPortal.Systems.CMS.LayoutDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Components/codemirror/lib/codemirror.js"></script>
    <link href="/Components/codemirror/lib/codemirror.css" rel="stylesheet" />
    <script src="/Components/codemirror/mode/javascript/javascript.js"></script>
    <script src="/Components/codemirror/mode/xml/xml.js"></script>
    <script src="/Components/codemirror/mode/css/css.js"></script>
    <script src="/Components/codemirror/mode/htmlmixed/htmlmixed.js"></script>
    <style type="text/css">
        .CodeMirror {
            height: 100%;
        }
    </style>
    <script type="text/javascript">
        var txtName;
        var editorForm;
        var editor;
        function init() {
            txtName = document.getElementById("<%= txtName.ClientID %>");
            editorForm = document.getElementById("<%= editorForm.ClientID %>");
        }
        function pageLoad() {
            init();
            var mixedMode = {
                name: "htmlmixed",
                scriptTypes: [{
                    matches: /\/x-handlebars-template|\/x-mustache/i,
                    mode: null
                },
                    {
                        matches: /(text|application)\/(x-)?vb(a|script)/i,
                        mode: "vbscript"
                    }]
            };
            editor = CodeMirror.fromTextArea(editorForm, { mode: mixedMode, lineNumbers: true, height: 245 });
        }
        function Save() {
            PageMethods.Save(txtName.value.trim(), editor.getValue(), SaveCallback);
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
    <table style="width: 100%;" align="center" cellpadding="4px;">
        <tr>
            <td style="width: 40%;" class="textinput" align="right">
                <%= Resources.Site.Id %>:
            </td>
            <td style="width: 60%">
                <asp:TextBox ID="txtId" runat="server" Width="200px" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 40%;" class="textinput" align="right">
                <%= Resources.Site.Name %>:
            </td>
            <td style="width: 60%">
                <asp:TextBox ID="txtName" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 40%; vertical-align: top;" class="textinput" align="right">Editor:
            </td>
            <td style="width: 60%; border: 1px solid gray">
                <textarea id="editorForm" runat="server"></textarea>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <table style="width: 5%" align="left">
                    <tr>
                        <td>
                            <a href="javascript:void(0)" onclick="Save()" class="submit">
                                <%= Resources.Site.Save %></a>
                        </td>
                        <td>
                            <a href="javascript:void(0)" class="submit" onclick="Cancel()">
                                <%= Resources.Site.Cancel %></a>
                        </td>
                    </tr>
                </table>
            </td>

        </tr>
    </table>
</asp:Content>
