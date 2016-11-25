<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Display.ascx.cs" Inherits="PSCPortal.Portlets.Popup.Display" %>
<script language="javascript" type="text/javascript">
    var nv = 0;
    function revealModal(divID) {
        if (!document.getElementById(divID))
            return;
        window.onscroll = function () { document.getElementById(divID).style.top = document.body.scrollTop; };
        document.getElementById(divID).style.display = "block";
        document.getElementById(divID).style.top = document.body.scrollTop;
    }

    function hideModal(divID) {
        document.getElementById(divID).style.display = "none";
    }
    function Set_Cookie(name, value, days) {
        if (typeof days != "undefined") {
            var today = new Date();
            today.setTime(today.getTime());
            var expires_date = new Date(today.getTime() + days);
            document.cookie = name + "=" + decodeURIComponent(value) + "; expires=" + expires_date.toGMTString()
        }
        else document.cookie = name + "=" + decodeURIComponent(value);
    }

    function Get_Cookie(name) {
        var re = new RegExp(name + "=[^;]+", "i");
        if (document.cookie.match(re))
            return decodeURIComponent(document.cookie.match(re)[0].split("=")[1]);
        return ""
    }

    function popunder() {

        var cookie_popup_ads = Get_Cookie('nhanvan_popup');
        if (cookie_popup_ads == '') {
            if (nv == 0) {
                nv = 1;
                var minutes = 1000 * 60;
                var hours = minutes * 60;
                var days = hours * 24;
                var years = days * 365;
                Set_Cookie('nhanvan_popup', 'true', 3 * minutes);   // thời gian tiemout 3 phút                 
                revealModal('modalPage');
            }
        }
    }
    </script>
<div id="modalPage">
<div class="modalBackground"></div>
<div class="modalContainer">
<div class="modal">
<div class="modalTop"><a href="#" onclick="hideModal('modalPage');">[X]</a></div>
<div class="modalBody">
<%#GetHTMLBlogContent() %>
</div>
</div>
</div>
</div>