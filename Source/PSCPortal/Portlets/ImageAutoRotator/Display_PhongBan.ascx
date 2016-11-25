<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Display_PhongBan.ascx.cs" Inherits="PSCPortal.Portlets.ImageAutoRotator.Display_PhongBan" %>
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

<div class="hoatdong_PB">
    <div class="topic_hdPB">
        <h3><%#Portlet.PortletInstance.Name %></h3>
    </div>
    <div class="banner_hdPB">
        <div class="khampha">
            <div style="float: left; margin-top: -10px;">
                <div style="width: 670px;">
                    <div class="microfiche_Slide" id="rotatorSlider" runat="server">
                        <div>
                            <ul data-bind="foreach: $data">
                                <li><a data-bind="attr: {href: Link}" target="_blank">
                                    <img data-bind="attr: {src: '/'+Url}" width="156" height="79" /></a></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
</div>

