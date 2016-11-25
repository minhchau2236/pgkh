<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Display.ascx.cs" Inherits="PSCPortal.Portlets.ImageAutoRotator.Display" %>
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
<div class="khampha">
<div style="width: 736px; height: 116px;float:left;">
    <img class="kp" src="/Resources/ImagesPortal/HomePage/khampha.png">
    <div style="float: left; padding-left: 10px;">
        <div style="width: 685px; height: 100px;float:left">
            <div class="microfiche_Slide" id="rotatorSlider" runat="server" style="bottom:10px">
                <div >
                    <ul data-bind="foreach: $data" >
                        <li><a data-bind="attr: {href: Link}" target="_blank">
                            <img data-bind="attr: {src: '/'+Url}"  width="158" height="90" /></a></li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</div>
</div>