<%@ Page Title="" Language="C#" MasterPageFile="~/Systems/MasterPage.Master" AutoEventWireup="true" CodeBehind="Indexing.aspx.cs" Inherits="PSCPortal.Systems.Indexing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">
        function CallWebMethodSuccess(results, context, methodName) {
            switch (methodName) {
                case "IndexingArticle":
                    {
                        radalert("Xây dựng chỉ mục cho tất cả các bài viết trong hệ thống thành công", 250, 100, "<%= Resources.Site.Warning %>");
                    }
                    break;
            }
        }
        function CallWebMethodFailed(results, context, methodName) {
            radalert(results._message, 250, 100, "<%= Resources.Site.Warning %>");
        }
        function IndexingArticle() {
            PageMethods.IndexingArticle(CallWebMethodSuccess, CallWebMethodFailed);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<a id="btnNew" href="javascript:void(0)" onclick="IndexingArticle();" class="Header">[Xây dựng chỉ mục trên Bài viết]</a>
</asp:Content>
