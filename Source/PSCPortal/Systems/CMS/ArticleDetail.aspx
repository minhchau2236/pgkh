<%@ Page EnableViewState="false" Language="C#" MasterPageFile="~/Systems/DialogMasterPage.Master"
    AutoEventWireup="true" CodeBehind="ArticleDetail.aspx.cs" Inherits="PSCPortal.Systems.CMS.ArticleDetail"
    Title="<%# Resources.Site.ArticleDetail %>" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Systems/CMS/Scripts/ArticleDetail.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        var reDescription;
        var reContent;
        var reAvatar;
        var strWarning;
        var rcbPage;
        var txtId;
        var txtName;
        var txtTitle;
        var rdiCreatedDate;
        var rdiModifiedDate;
        var IsVisibleCreateDate;
        var cbxArticleHang;
        var txtArticleHang;
        var rcbArticleTemplate;
        var imgArticle;
        var cbxNews;
        var txtDocument;
        var txtAlbum;
        function initialize() {
            strWarning = "<%= Resources.Site.Warning %>";
            txtId = document.getElementById("<%= txtId.ClientID %>");
            txtName = document.getElementById("<%= txtName.ClientID %>");
            txtTitle = document.getElementById("<%= txtTitle.ClientID %>");
            rdiCreatedDate = $find("<%= rdiCreatedDate.ClientID %>");
            rdiModifiedDate = $find("<%= rdiModifiedDate.ClientID %>");
            rcbPage = $find("<%=rcbPage.ClientID %>");
            reDescription = $find("<%=reDescription.ClientID%>");
            reContent = $find("<%=reContent.ClientID%>");
            IsVisibleCreateDate = document.getElementById("<%=IsVisibleCreateDate.ClientID%>");
            cbxArticleHang = document.getElementById("<%=cbxArticleHang.ClientID%>");
            txtArticleHang = $find("<%=txtArticleHang.ClientID%>");
            rcbArticleTemplate = $find("<%=rcbArticleTemplate.ClientID%>");
            cbxComment = document.getElementById("<%=cbxComment.ClientID%>");
            imgArticle = document.getElementById('imgArticle');
            cbxNews = document.getElementById("<%=cbxNews.ClientID%>");
            txtDocument = $find("<%=txtDocument.ClientID%>");
            txtAlbum = $find("<%=txtAlbum.ClientID%>");
         
        }

        function OnClientPasteHtml(editor, args) {
            var commandName = args.get_commandName();
            if (commandName == "MediaManager") {
                var value = args.get_value();
                if (value.toUpperCase().indexOf(".FLV") >= 0) {
                    var div = document.createElement('div');
                    div.innerHTML = value;
                    var link = div.getElementsByTagName('embed')[0].getAttribute("src");
                    var str = '<object width="300" height="200" type="application/x-shockwave-flash">';
                    str += '<param name="movie" value="http://releases.flowplayer.org/swf/flowplayer-3.2.18.swf">';
                    str += '<param name="allowfullscreen" value="true">';
                    str += '<param name="allowscriptaccess" value="always">';
                    str += '<param name="AutoStart" value="false">';
                    str += '<param name="flashvars"';
                    str += 'value=';
                    str += 'config={"clip":{';
                    str += '"url":"' + link + '"}';
                    str += ',"playlist":[{"url":"' + link + '"}]} /></object>';
                    args.set_value(str);
                }
                else if (value.toUpperCase().indexOf(".MP4") >= 0) {
                    var div = document.createElement('div');
                    div.innerHTML = value;
                    var link = div.getElementsByTagName('embed')[0].getAttribute("src");
                    var width = div.getElementsByTagName('embed')[0].getAttribute("width");
                    var height = div.getElementsByTagName('embed')[0].getAttribute("height");
                    var str = '<div class="videoWrapper">';
                    str += '<iframe width="' + width + '" height="' + height + '" src="' + link + '" frameborder="0"></iframe>';
                    str += '</div>';
                    args.set_value(str);
                }
                else {
                    args.set_value(value);
                }
            }
        }

        function DocumentManagerFunction(sender, args) {
            var txt = $find('<%= txtDocument.ClientID %>');
            var foder = $find("<%=txtAlbum.ClientID%>");
            var path = decodeURI(args.value.pathname);
            var s = path.substring(0, path.lastIndexOf("/"));
            txt.set_value(path);
            foder.set_value(s);
        }
        function OpenFolderManager() {
            var args = new Telerik.Web.UI.EditorCommandEventArgs("DocumentManager", null, document.createElement("a"));
            args.CssClasses = [];
            $find('<%= DialogOpener1.ClientID %>').open('DocumentManager', args);

        }
        function OpenDocManager() {
            var args = new Telerik.Web.UI.EditorCommandEventArgs("DocumentManager", null, document.createElement("a"));
            args.CssClasses = [];
            $find('<%= DialogOpener1.ClientID %>').open('DocumentManager', args);
           
        }
    </script>
    <style type="text/css">
        /*CSS Style Editor*/
        body {
            font-family: Arial !important;
            font-size: 13px !important;
            text-align: justify !important;
            line-height: 150% !important;
        }
        .reWrapper {width:99.8%!important;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:DialogOpener runat="server" ID="DialogOpener1"></telerik:DialogOpener>
    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="rmpAticle">
        <Tabs>
            <telerik:RadTab runat="server" Selected="true" PageViewID="rpvDetail" Text="Thông tin">
            </telerik:RadTab>
            <%-- <telerik:RadTab runat="server" Selected="true" PageViewID="rpvAvatar" Text="Mô tả đặc biệt">
            </telerik:RadTab>--%>
            <telerik:RadTab runat="server" PageViewID="rpvDescription" Selected="True" Text="Mô tả">
            </telerik:RadTab>
            <telerik:RadTab runat="server" PageViewID="rpvContent" Text="Nội dung">
            </telerik:RadTab>
        </Tabs>
    </telerik:RadTabStrip>
    <telerik:RadMultiPage ID="rmpAticle" runat="server">
        <telerik:RadPageView Selected="true" ID="rpvDetail" runat="server">
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
                    <td style="width: 40%;" class="textinput" align="right">
                        <%= Resources.Site.Title %>:
                    </td>
                    <td style="width: 60%">
                        <asp:TextBox ID="txtTitle" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 40%;" class="textinput" align="right">
                        <%= Resources.Site.CreatedDate %>:
                    </td>
                    <td style="width: 40%">
                        <telerik:RadDateInput ID="rdiCreatedDate" runat="server" DisplayDateFormat="dd/MM/yyyy:HH:mm:ss"
                            DateFormat="dd/MM/yyyy HH:mm:ss" Width="200px" MinDate="1900-01-01" Culture="vi-vn">

                        </telerik:RadDateInput>
                        <input type="checkbox" id="IsVisibleCreateDate" runat="server" /><label for="IsVisibleCreateDate">Ẩn ngày tạo</label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 40%;" class="textinput" align="right">
                        <%= Resources.Site.ModifiedDate %>:
                    </td>
                    <td style="width: 60%">
                        <telerik:RadDateInput ID="rdiModifiedDate" runat="server" DisplayDateFormat="dd/MM/yyyy:HH:mm:ss"
                            DateFormat="dd/MM/yyyy HH:mm:ss" Width="200px" MinDate="1900-01-01">
                        </telerik:RadDateInput>
                    </td>
                </tr>
                <tr>
                    <td style="width: 40%;" class="textinput" align="right">Hình ảnh:
                    </td>
                    <td style="width: 60%">
                        <iframe marginheight="0" id="Iframe2" marginwidth="0" style="background-color: #f3f3f3"
                            frameborder="0" src="UploadArticleImage.aspx" width="400px" height="130px"></iframe>
                    </td>
                </tr>
                <%--   <tr>
                    <td style="width: 40%;" class="textinput" align="right">Hình ảnh cho Portlet:
                    </td>
                    <td style="width: 60%">
                        <iframe marginheight="0" id="Iframe1" height="28px" marginwidth="0" style="background-color: #f3f3f3"
                            frameborder="0" src="UploadArticlePortlet.aspx" width="400px"></iframe>
                    </td>
                </tr>--%>
                <tr>
                    <td style="width: 40%;" class="textinput" align="right">
                        <%= Resources.Site.PageDisplay %>:
                    </td>
                    <td style="width: 60%">
                        <telerik:RadComboBox ID="rcbPage" DataTextField="Name" DataValueField="Id" runat="server" Width="300px">
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 40%;" class="textinput" align="right">Treo bài viết:
                    </td>
                    <td style="width: 60%">

                        <input type="checkbox" id="cbxArticleHang" onclick="cbxArticleHang_checked()" runat="server" />
                        <telerik:RadDateInput ID="txtArticleHang" DisplayDateFormat="dd/MM/yyyy:HH:mm:ss"
                            DateFormat="dd/MM/yyyy HH:mm:ss" Width="200px" MinDate="1900-01-01" runat="server">
                        </telerik:RadDateInput>
                    </td>
                </tr>
                <tr>
                    <td style="width: 40%;" class="textinput" align="right">Hiển thị hình ảnh và mô tả:
                    </td>
                    <td style="width: 60%">
                        <telerik:RadComboBox ID="rcbArticleTemplate" runat="server">
                            <Items>
                                <telerik:RadComboBoxItem Value="0" Text="Canh trái" />
                                <telerik:RadComboBoxItem Value="1" Text="Canh phải" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 40%;" class="textinput" align="right">Góp ý:
                    </td>
                    <td style="width: 60%">
                        <input type="checkbox" id="cbxComment" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 40%;" class="textinput" align="right" valign="top">Bản tin:
                    </td>
                    <td style="width: 60%">
                        <div style="padding-bottom: 5px;">
                            <input type="checkbox" id="cbxNews" runat="server" onclick="cbxNews_checked()" />
                        </div>
                        <div id="albumForm">
                              <div style="float: left;padding-top:5px;">
                                <span style="width:120px;float:left">Tập tin tải về:</span>
                                <telerik:RadTextBox ID="txtDocument" runat="server" Width="200px" CssClass="float:left;" disabled="disabled"/>
                            </div>
                            <a title="Document Manager" class="reTool" href="#" onclick="OpenDocManager()" style="float: left;">
                                <span class="DocumentManager" unselectable="on">&nbsp;</span>
                            </a>
                            <div style="float: left">
                                <span style="width:120px;float:left">Thư mục hình ảnh:</span>
                                <telerik:RadTextBox ID="txtAlbum" runat="server" Width="200px" disabled="disabled" />
                            </div>                            
                        </div>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
        <%--<telerik:RadPageView ID="rpvAvatar" runat="server">
            <telerik:RadEditor ID="reAvatar" runat="server">
                <ImageManager DeletePaths="~/Resources/Images" ViewPaths="~/Resources/Images" UploadPaths="~/Resources/Images"
                    MaxUploadFileSize="204800000" />
                <MediaManager DeletePaths="~/Resources/Medias" ViewPaths="~/Resources/Medias" UploadPaths="~/Resources/Medias"
                    MaxUploadFileSize="204800000" />
                <FlashManager DeletePaths="~/Resources/Flashs" ViewPaths="~/Resources/Flashs" UploadPaths="~/Resources/Flashs"
                    MaxUploadFileSize="204800000" SearchPatterns="*.flv,*.swf" />
                <DocumentManager DeletePaths="~/Resources/Docs" ViewPaths="~/Resources/Docs" UploadPaths="~/Resources/Docs"
                    MaxUploadFileSize="204800000" SearchPatterns="*.ppt,*.rar,*.zip,*.doc, *.txt, *.docx, *.xls, *.xlsx, *.pdf" />
            </telerik:RadEditor>
        </telerik:RadPageView>--%>
        <telerik:RadPageView ID="rpvDescription" runat="server">
            <telerik:RadEditor ID="reDescription" AllowScripts="true" runat="server" OnClientPasteHtml="OnClientPasteHtml" MaxTextLength="500">
                <Modules>
                    <telerik:EditorModule Name="RadEditorStatistics" Enabled="true" Visible="true" />
                </Modules>
                <Tools>
                    <telerik:EditorToolGroup>
                        <telerik:EditorTool Name="Print"></telerik:EditorTool>
                        <telerik:EditorTool Name="Cut"></telerik:EditorTool>
                        <telerik:EditorTool Name="Copy"></telerik:EditorTool>
                        <telerik:EditorTool Name="Paste"></telerik:EditorTool>
                        <telerik:EditorTool Name="FindAndReplace"></telerik:EditorTool>
                        <telerik:EditorTool Name="InsertTime"></telerik:EditorTool>
                        <telerik:EditorTool Name="InsertDate"></telerik:EditorTool>
                        <telerik:EditorTool Name="Redo"></telerik:EditorTool>
                        <telerik:EditorTool Name="Undo"></telerik:EditorTool>
                        <telerik:EditorTool Name="Bold"></telerik:EditorTool>
                        <telerik:EditorTool Name="Italic"></telerik:EditorTool>
                        <telerik:EditorTool Name="Underline"></telerik:EditorTool>
                        <telerik:EditorTool Name="ForeColor"></telerik:EditorTool>
                        <telerik:EditorTool Name="FontName"></telerik:EditorTool>
                        <telerik:EditorTool Name="RealFontSize"></telerik:EditorTool>
                    </telerik:EditorToolGroup>
                </Tools>
            </telerik:RadEditor>
        </telerik:RadPageView>
        <telerik:RadPageView ID="rpvContent" runat="server">
            <telerik:RadEditor ID="reContent" runat="server" OnClientPasteHtml="OnClientPasteHtml">
                  <Modules>
                    <telerik:EditorModule Name="RadEditorStatistics" Enabled="true" Visible="true" />
                </Modules>
                <Tools>
                    <telerik:EditorToolGroup>
                        <telerik:EditorTool Name="Print"></telerik:EditorTool>
                        <telerik:EditorTool Name="Cut"></telerik:EditorTool>
                        <telerik:EditorTool Name="Copy"></telerik:EditorTool>
                        <telerik:EditorTool Name="Paste"></telerik:EditorTool>
                        <telerik:EditorToolStrip Name="Paste Options">
                            <telerik:EditorTool Name="PasteFromWord"/>
                            <telerik:EditorTool Name="Paste"/>
                            <telerik:EditorTool Name="PasteFromWordNoFontsNoSizes"/>
                             <telerik:EditorTool Name="PastePlainText"/>
                             <telerik:EditorTool Name="PasteAsHtml"/>
                        </telerik:EditorToolStrip>
                        <telerik:EditorTool Name="FindAndReplace"></telerik:EditorTool>
                         <telerik:EditorTool Name="InsertTable"></telerik:EditorTool>
                        <telerik:EditorSeparator Visible="true"></telerik:EditorSeparator>               
                        <telerik:EditorTool Name="InsertDate"></telerik:EditorTool>
                        <telerik:EditorTool Name="Redo"></telerik:EditorTool>
                        <telerik:EditorTool Name="Undo"></telerik:EditorTool>
                        <telerik:EditorTool Name="Bold"></telerik:EditorTool>
                        <telerik:EditorTool Name="Italic"></telerik:EditorTool>
                        <telerik:EditorTool Name="Underline"></telerik:EditorTool>
                         <telerik:EditorTool Name="JustifyLeft"></telerik:EditorTool>
                        <telerik:EditorTool Name="JustifyCenter"></telerik:EditorTool>
                        <telerik:EditorTool Name="JustifyRight"></telerik:EditorTool>
                        <telerik:EditorTool Name="JustifyFull"></telerik:EditorTool>
                        <telerik:EditorSeparator Visible="true"></telerik:EditorSeparator>
                        <telerik:EditorTool Name="ForeColor"></telerik:EditorTool>
                        <telerik:EditorTool Name="FontName"></telerik:EditorTool>
                        <telerik:EditorTool Name="RealFontSize"></telerik:EditorTool>
                        <telerik:EditorTool Name="ImageManager"></telerik:EditorTool>
                        <telerik:EditorTool Name="MediaManager"></telerik:EditorTool>
                        <telerik:EditorTool Name="FlashManager"></telerik:EditorTool>
                        <telerik:EditorTool Name="DocumentManager"></telerik:EditorTool>
                        <telerik:EditorTool Name="Indent"></telerik:EditorTool>
                        <telerik:EditorTool Name="Outdent"></telerik:EditorTool>

                    </telerik:EditorToolGroup>
                </Tools>
                <%--                <CssClasses>
                    <telerik:EditorCssClass Name="Clear Class1" Value="" />
                    <telerik:EditorCssClass Name="article" Value=".article" />
                </CssClasses>--%>
                <ImageManager ViewPaths="~/Resources/Images" UploadPaths="~/Resources/Images"
                    MaxUploadFileSize="524288 " DeletePaths="~/Resources/Images" SearchPatterns="*.gif,*.png,*.jpg,*.jpeg" />
                <MediaManager ViewPaths="~/Resources/Medias" UploadPaths="~/Resources/Medias"
                    MaxUploadFileSize="52428800 " SearchPatterns="*.flv,*.mp4,*.mkv,*.mp3,*.avi" DeletePaths="~/Resources/Medias" />
                <FlashManager ViewPaths="~/Resources/Flashs" UploadPaths="~/Resources/Flashs"
                    MaxUploadFileSize="52428800 " SearchPatterns="*.swf" DeletePaths="~/Resources/Flashs" />
                <DocumentManager ViewPaths="~/Resources/Docs" UploadPaths="~/Resources/Docs"
                    MaxUploadFileSize="52428800 " SearchPatterns="*.ppt,*.rar,*.zip,*.doc, *.txt, *.docx, *.xls, *.xlsx, *.pdf" DeletePaths="~/Resources/Docs" />
            </telerik:RadEditor>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
    <table style="width: 100%;" align="center" cellpadding="4px;">
        <tr align="center">
            <td colspan="2">
                <img src="/Systems/CMS/Image/line.jpg" width="438" height="1" alt="" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table style="width: 15%" align="center">
                    <tr>
                        <td style="width: 50%;">
                            <a href="javascript:void(0)" onclick="Save();" class="submit">
                                <%= Resources.Site.Save %></a>
                        </td>
                        <td style="width: 50%;">
                            <a href="javascript:void(0)" onclick="Cancel();" class="submit">
                                <%= Resources.Site.Cancel %></a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <script type="text/javascript" language="javascript">
        Telerik.Web.UI.Editor.CommandList["InsertLink"] = function (commandName, editor, args) {
            var elem = editor.getSelectedElement(); //returns the selected element.

            if (elem.tagName == "A") {
                editor.selectElement(elem);
                argument = elem;
            }
            else {
                var content = editor.getSelectionHtml();
                var link = editor.get_document().createElement("A");
                link.innerHTML = content;
                argument = link;
            }

            var myCallbackFunction = function (sender, args) {
                //   editor.pasteHtml(String.format("<img alt=\"\" src=\"/Systems/CMS/ImageFactor.ashx?Id={0}\"/>", args));
                editor.pasteHtml(String.format("{0}", args.outerHTML))
            }

            editor.showExternalDialog('MenuDetailChooseDialog.aspx', argument, 600, 450, myCallbackFunction, 'Link Manage', 'Insert Link', true, Telerik.Web.UI.WindowBehaviors.Close + Telerik.Web.UI.WindowBehaviors.Move, false, false);
        };
    </script>
</asp:Content>
