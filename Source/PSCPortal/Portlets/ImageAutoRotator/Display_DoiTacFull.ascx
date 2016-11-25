<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Display_DoiTac.ascx.cs" Inherits="PSCPortal.Portlets.ImageAutoRotator.Display_DoiTac" %>
<link href="/Portlets/ImageAutoRotator/CSS/microfiche.css" rel="stylesheet" />
<link href="/Portlets/ImageAutoRotator/CSS/microficheDefault.css" rel="stylesheet" />
<script src="/Scripts/knockout-2.3.0.js"></script>
<script src="/Portlets/ImageAutoRotator/Scripts/microfiche.js"></script>
<script src="/Portlets/ImageAutoRotator/Scripts/System.Utility.ImageAutoRotator.js"></script>
<script type="text/javascript">
    $(function () {
        (new System.Utility.ImageAutoRotator('<%#rotatorSlider.ClientID%>', '<%#ListImage%>')).CreateSlider(true);
    });
</script>
<div class="tuyendung_doitac" style="padding: 0px 0px 0px 0px;">
    <h3><%#Portlet.PortletInstance.Name %></h3>
    <div style="width: 450px;padding:0px 0px 0px 10px;">
        <div class="microfiche_Slide" id="rotatorSlider" runat="server">
            <ul data-bind="foreach: $data" class="logo_doitac">
                <li><a data-bind="attr: {href: Link}" target="_blank" class="float-shadow">
                    <img data-bind="attr: {src: '/'+Url}" width="79" height="64" /></a></li>
            </ul>
        </div>
    </div>
</div>
