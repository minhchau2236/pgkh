<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Display_SinhVien.ascx.cs" Inherits="PSCPortal.Portlets.ImageAutoRotator.Display_SinhVien" %>
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
<div class="infoTS_SV">
    <h3><%#Portlet.PortletInstance.Name %></h3>
    <div style="width: 730px; padding: 0px 0px 0px 0px;">
        <div class="microfiche_Slide" id="rotatorSlider" runat="server" >
            <ul data-bind="foreach: $data" >
                <li><a data-bind="attr: {href: Link}" target="_blank" class="float-shadow">
                    <img data-bind="attr: {src: '/'+Url}" width="230" height="80" /></a></li>
            </ul>
        </div>
    </div>
</div>
