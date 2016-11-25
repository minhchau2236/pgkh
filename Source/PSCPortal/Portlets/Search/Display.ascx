<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Display.ascx.cs" Inherits="PSCPortal.Portlets.Search.Display" %>
<div class="gioithieu">
    <div class="form_timkiem">
        <input name="" type="text" class="form" onkeydown="return SearchProcess(event);" />
        <a href="#">
            <img src="Resources/Images/CaoDangBachViet/icon_timkiem.jpg" onclick="OnSearch(document.getElementById('txtSearchArticle').value);" /></a>
    </div>
</div>
