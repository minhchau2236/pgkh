<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Display.ascx.cs" Inherits="PSCPortal.Portlets.LinkWebsite.Display" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div class="top">
    <div class="link">
        <div class="search">
            <input style="width: 144px;" name="txtSearchArticle" id="txtSearchArticle" onkeydown="return SearchProcess(event);"
                onclick="return txtSearchArticle_onclick()" type="text" /><a href="javascript:void(0)"
                    onclick="OnSearch(document.getElementById('txtSearchArticle').value);"><img alt=""
                        src="/Resources/ImagesPortal/icon_search.png" /></a></div>
        <div class="lienket" style="width: 180px;">
            <select style=" width:180px; border:0px;height:20px; background:url(/Resources/ImagesPortal/bg_web.png) no-repeat;" onchange="if (this.value!=0)window.open(this.value);" size="1">            
                <option selected="selected"><%# Resources.Site.WebsiteSelect %></option>
            <telerik:radlistview id="ListLinkWebsite" runat="server" allowpaging="True">
                <ItemTemplate>
                <option value="<%#Eval("Link")%>"><%#Eval("Name") %></option>
                </ItemTemplate>
            </telerik:radlistview>
        </select>
            <%--<%#GetHTMLBlogContent() %>--%>
        </div>
    </div>
</div>
