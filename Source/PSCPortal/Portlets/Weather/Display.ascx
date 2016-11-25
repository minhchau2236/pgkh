<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Display.ascx.cs" Inherits="PSCPortal.Portlets.Weather.Display" %>
<div class="blog_wather">
    <div class="title_wather">
        THỜI TIẾT</div>
    <div class="input_wather">
        <select id="ddlWeather" style="width: 100px; font-size: 12px;" onchange="GetWeatherInfo();">
                    <option selected="selected" value="http://weather.msn.com/RSS.aspx?wealocations=wc:VMXX0029&amp;weadegreetype=C">
                        Nha Trang</option>
                    <option value="http://weather.msn.com/RSS.aspx?wealocations=wc:VMXX0007&amp;weadegreetype=C">
                        Hồ Chí Minh</option>
                    <option value="http://weather.msn.com/RSS.aspx?wealocations=wc:VMXX0006&amp;weadegreetype=C">
                        Hà Nội</option>
                    <option value="http://weather.msn.com/RSS.aspx?wealocations=wc:8456&amp;weadegreetype=C">
                        Đà Lạt</option>
                    <option value="http://weather.msn.com/RSS.aspx?wealocations=wc:VMXX0028&amp;weadegreetype=C">
                        Đà Nẵng</option>
                    <option value="http://weather.msn.com/RSS.aspx?wealocations=wc:VMXX0009&amp;weadegreetype=C">
                        Thừa Thiên Huế</option>
                </select>
    <div class="text_nd">
        <div id="HinhAnh"></div><span id="NhietDo"></span></div>
    <div class="text_da">
        <div class="text_wather1">
            <span id="Info"></span></div>
        <div class="text_wather1">
            Độ ẩm :<span id="humidity"></span></div>
        <div class="text_wather1">
            Gió:<span id="windSpeed"></span></div>
       <%-- <div class="text_wather1">
            Tốc độ :<span>10km/h</span></div>--%>
    </div>
</div>
</div>
<script type="text/javascript" language="javascript">
    function GetWeatherInfo() {
        //dia chi cua tỉnh thành
        var placeLink = document.getElementById('ddlWeather').value;
        try {
            PSCPortal.Services.WeatherService.GetWeatherInfo(placeLink, OnSucessGetWeatherInfo, OnFailGetWeatherInfo);
        }
        catch (e) {
        }
    }

    function OnSucessGetWeatherInfo(result, context, methodName) {
        LoadWeather(result);
    }

    function OnFailGetWeatherInfo(result, context, methodName) {
        alert("Load Data Fail");
    }

    function LoadWeather(wetherHTML) {

        document.getElementById('HinhAnh').innerHTML = wetherHTML[0];
        document.getElementById('NhietDo').innerHTML = wetherHTML[1];
        document.getElementById('Info').innerHTML = wetherHTML[2];  //+ "<br/>" + "Gió :" + wetherHTML[3] + "<br/>" + "Độ ẩm :" + wetherHTML[4];
        document.getElementById('windSpeed').innerHTML = wetherHTML[3];
        document.getElementById('humidity').innerHTML = wetherHTML[4];
    }

    GetWeatherInfo();
</script>
