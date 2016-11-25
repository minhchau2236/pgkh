<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Edit.ascx.cs" Inherits="PSCPortal.Portlets.LinkWebsite.Edit" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<script type="text/javascript" language="javascript">
    function displayResult(event) {
        if (event.keyCode == 13) {
            var x = document.getElementById("thebox");
            var option = document.createElement("option");
            var txtv = document.getElementById("theinput");
            var txtlink = document.getElementById("thelink");
            option.text = txtv.value;
            option.value = txtlink.value
            try {
                // for IE earlier than version 8
                x.add(option, x.options[null]);
                clear();
                document.getElementById("Insert").disabled = false;
            }
            catch (e) {
                x.add(option, null);
            }
        }
    }
    function clear() {
        document.getElementById("theinput").value = '';
        document.getElementById("thelink").value = '';
    }
    function PasteTextInEditor() {
        var x = document.getElementById("thebox");
        var editor = $find("<%=RadEditor1.ClientID%>");
        var oSelElem = editor.get_text(); //get the editor content as plain text        
        var oValue = editor.get_html(true);
        if (x.length > 0) {
            if (oSelElem == '') {
                //alert(oSelElem);                                
                var txt = '<select style="width: 180px; border: 0px; background: url(/Resources/ImagesPortal/bg_web.png) no-repeat;" onchange="if (this.value!=0)window.open(this.value);" size="1">';
                txt = txt + '<option selected="selected">Website</option>';
                var option;
                for (i = 0; i < x.length; i++) {
                    option = '<option value="' + x.options[i].value + '">';
                    option = option + x.options[i].text + '</option>';
                    txt = txt + option;
                }
                txt = txt + '</select>';
                editor.pasteHtml(txt);
            }
            else {


                var oValueend = oValue.search('</select>');
                var oValuestar = oValue.substr(0, oValueend);
                
                var option;
                for (i = 0; i < x.length; i++) {
                    option = '<option value="' + x.options[i].value + '">';
                    option = option + x.options[i].text + '</option>';
                    oValuestar = oValuestar + option;
                }
                var txt = oValuestar + '</select>';
                editor.set_html("");
                editor.pasteHtml(txt);
            }
        }
        else
            alert('Bạn chưa nhập liên kết.');
        x.options.length = 0;
        document.getElementById("Insert").disabled = true;
    }
</script>
<div style=" padding:10px;">
    Nội dung:
    <input type="text" id="theinput" onkeyup="displayResult(event)" />
    Link:
    <input type="text" id="thelink" onkeyup="displayResult(event)" />
   ( Nhập nội dung, link liên kết và Enter )
    </div>
<div style=" padding:10px;"> Liên kết Website:
<select id="thebox" name="thebox" style="width: 180px; border: 0px; background: url(/Resources/ImagesPortal/bg_web.png) no-repeat;"
    onchange="if (this.value!=0)window.open(this.value);" size="1">
</select>
<input id="Insert" disabled="disabled"  type="button" value="Thêm" onclick="PasteTextInEditor();" />
</div>
<telerik:RadEditor runat="server" ID="RadEditor1">
</telerik:RadEditor>

 

<asp:LinkButton ID="lbtAccept" runat="server" OnClick="lbtAccept_Click">[Lưu]</asp:LinkButton>
<asp:LinkButton ID="lbtCancel" runat="server" OnClick="lbtCancel_Click">[Thoát]</asp:LinkButton>
