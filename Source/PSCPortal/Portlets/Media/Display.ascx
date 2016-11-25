<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Display.ascx.cs" Inherits="PSCPortal.Portlets.Media.Display" %>

<style>
    .Media_tabs {
        position: relative;
        height: 450px;
        clear: both;
        margin: 25px 0;
    }

    .Media_tab {
        float: left;
        display: inline-block;
    }

        .Media_tab [type=radio] {
            display: none;
        }

        .Media_tab label {
            font-weight: normal;
            background-color: #E1E1E1;
            padding: 13px 35px 13px 35px;
            border: 1px solid #ccc;
            margin-right: 5px;
            position: relative;
            cursor: pointer;
            z-index: 0;
            border-radius: 3px 3px 3px 3px;
        }

    [type=radio]:checked ~ label {
        background: #F2F0F1;
        border-bottom: 1px solid #F2F0F1;
        z-index: 2;
    }

    .Media_content {
        position: absolute;
        left: 0;
        background: #F2F0F1;
        right: 0;
        bottom: 0;
        padding: 20px;
        border: 1px solid #ccc;
        display: block;
        z-index: -1;
        border-radius: 0 3px 3px 3px;
        top: 47px;
    }

    [type=radio]:checked ~ label ~ .Media_content {
        display: block;
        z-index: 1;
    }
</style>

<%--<link href="http://vjs.zencdn.net/c/video-js.css" rel="stylesheet">
<script src="http://vjs.zencdn.net/c/video.js"></script>--%>

<script src="/Portlets/Media/System.Utility.Media.js"></script>

<script type="text/javascript">
    var curTab = 'phapthoai';
    var myVideo, videoDiv;
    var myAudio;

    function phapthoai_Click() {
        curTab = 'phapthoai';
        myAudio.pause();
    }

    function amnhac_Click() {
        curTab = 'amnhac';
        myVideo.pause();
    }

    function pageLoad() {

        myVideo = new MediaElementPlayer('#myVideo');
        videoDiv = document.getElementById('videoDiv');
        myAudio = document.getElementById('myAudio');
    }

    function changVideo(title, path) {
        var fileEx = path.substring(path.toString().lastIndexOf(".") + 1).toLowerCase();
        if (fileEx == "flv")
            fileEx = "flv";
        fileEx = "video/" + fileEx;

        videoDiv.innerHTML = '<div style="padding: 15px 0 0 0;">' + '<video id="myVideo" width="400" height="315" controls style="background-color: black;"><source src="' + path + '" type="' + fileEx + '">Trình duyệt của bạn không hổ trợ xem video.</video>' + '</div><div style="width: 350px; color: #5C2B03; padding: 15px 0 0 0; font-weight: bold;">' + title + '</div>';
        myVideo = new MediaElementPlayer('#myVideo', {
            success: function (mediaElement, domObject) { mediaElement.play(); }
        });
        //myVideo.play();
        //pscjq('video').mediaelementplayer();
    }

    function changeMusic(title, path) {
        var musicDiv = document.getElementById('musicDiv');
        musicDiv.innerHTML = '<div style="color: #5C2B03; font-weight: bold; margin-top: 10px;">' + title + '<audio id="myAudio" controls style="width: 100%; margin-top: 5px;"><source src="' + path + '" type="audio/mpeg">Trình duyệt của bạn không hổ trợ nghe nhạc.</audio></div>';
        myAudio = document.getElementById('myAudio');
        myAudio.play();

    }

    pscjq(function () {
        (new System.Utility.Media('<%=mainContainer.ClientID%>', '<%#ResultList%>'));
    });
</script>
<div style="height: 20px; width: 675px; border-bottom: 1px solid #CCCCCC;"></div>
<div id="mainContainer" runat="server">
    <div class="Media_tabs">
        <div class="Media_tab">
            <input type="radio" id="tab-1" name="tab-group-1" checked>
            <label for="tab-1" onclick="phapthoai_Click();">
                Pháp thoại</label>
            <div id="VideoPlayer" class="Media_content">
                <table>
                    <tr valign="top">
                        <td>
                            <div id="videoDiv">
                            </div>
                        </td>
                        <td>
                            <ul data-bind="foreach: videos" style="list-style-type: square; background-color: white; padding: 0 5px 0 20px; margin: 15px 0 0 20px; height: 350px; width: 215px;">
                                <li style="margin-left: 2px;">
                                    <div style="padding: 8px 0 12px 5px; border-bottom: 1px solid #E6E6E6;">
                                        <a style="text-decoration: none; color: #5C2B03; cursor: pointer; font-size: 13px;" data-bind="text: Title, click: function () { changVideo(Title, Path); }"></a>
                                    </div>
                                </li>
                                <%-- <li style="margin-left: 2px;">
                                <div style="padding: 8px 0 12px 5px; border-bottom: 1px solid #E6E6E6;"><a style="text-decoration: none; color: #5C2B03; cursor: pointer; font-size: 13px;">Clip B Clip B Clip B Clip B Clip B Clip B Clip B Clip B Clip B Clip B</a></div>
                            </li>
                            <li style="margin-left: 2px;">
                                <div style="padding: 8px 0 12px 5px; border-bottom: 1px solid #E6E6E6;"><a style="text-decoration: none; color: #5C2B03; cursor: pointer; font-size: 13px;">Clip B Clip B Clip B Clip B Clip B Clip B Clip B Clip B Clip B Clip B</a></div>
                            </li>
                            <li style="margin-left: 2px;">
                                <div style="padding: 8px 0 12px 5px; border-bottom: 1px solid #E6E6E6;"><a style="text-decoration: none; color: #5C2B03; cursor: pointer; font-size: 13px;">Clip B Clip B Clip B Clip B Clip B Clip B Clip B Clip B Clip B Clip B</a></div>
                            </li>--%>
                            </ul>
                            <div style="position: relative; text-align: right; padding-right: 10px; top: -25px;"><a style="text-decoration: none; cursor: pointer; color: red; font-size: 12px; font-style: italic;" href="http://pgkhanhhoa.com/module/videolist/71e6f018-023e-4e21-8b31-f1b8f88c23a0">Xem thêm >></a></div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="Media_tab">
            <input type="radio" id="tab-2" name="tab-group-1">
            <label for="tab-2" onclick="amnhac_Click();">
                Âm nhạc</label>
            <div id="MusicPlayer" class="Media_content">
                <div id="musicDiv">
                    <div style="color: #5C2B03; font-weight: bold; margin-top: 10px;">
                        Một tiếng lòng - một tiếng Phật
                    <audio id="myAudio" controls style="width: 100%; margin-top: 5px;">
                        <source src="/Portlets/Media/MotTiengLongMotTiengPhat.mp3" type="audio/mpeg">
                        Trình duyệt của bạn không hổ trợ nghe nhạc.
                    </audio>
                    </div>
                </div>
                <div style="background-color: white;">
                    <div style="margin-top: 5px; height: 295px; padding: 2px; overflow-y: scroll;">
                        <ul data-bind="foreach: musics" style="font-size: 15px;">
                            <li>
                                <div style="padding: 10px 5px 10px 5px; margin: 0 5px 0 5px; border-bottom: 1px solid #E6E6E6;">
                                    <a style="text-decoration: none; color: #5C2B03; cursor: pointer; font-size: 15px;" data-bind="text: Title, click: function () { changeMusic(Title, Path); }"></a>
                                </div>
                            </li>
                            <%--<li>
                                <div style="padding: 10px 5px 10px 5px; margin: 0 5px 0 5px; border-bottom: 1px solid #E6E6E6;">
                                    <a style="text-decoration: none; color: #5C2B03; cursor: pointer; font-size: 15px;">&#9835 Bài hát 1</a>
                                </div>
                            </li>
                            <li>
                                <div style="padding: 10px 5px 10px 5px; margin: 0 5px 0 5px; border-bottom: 1px solid #E6E6E6;">
                                    <a style="text-decoration: none; color: #5C2B03; cursor: pointer; font-size: 15px;">&#9835 Bài hát 1</a>
                                </div>
                            </li>
                            <li>
                                <div style="padding: 10px 5px 10px 5px; margin: 0 5px 0 5px; border-bottom: 1px solid #E6E6E6;">
                                    <a style="text-decoration: none; color: #5C2B03; cursor: pointer; font-size: 15px;">&#9835 Bài hát 1</a>
                                </div>
                            </li>
                            <li>
                                <div style="padding: 10px 5px 10px 5px; margin: 0 5px 0 5px; border-bottom: 1px solid #E6E6E6;">
                                    <a style="text-decoration: none; color: #5C2B03; cursor: pointer; font-size: 15px;">&#9835 Bài hát 1</a>
                                </div>
                            </li>
                            <li>
                                <div style="padding: 10px 5px 10px 5px; margin: 0 5px 0 5px; border-bottom: 1px solid #E6E6E6;">
                                    <a style="text-decoration: none; color: #5C2B03; cursor: pointer; font-size: 15px;">&#9835 Bài hát 1</a>
                                </div>
                            </li>
                            <li>
                                <div style="padding: 10px 5px 10px 5px; margin: 0 5px 0 5px; border-bottom: 1px solid #E6E6E6;">
                                    <a style="text-decoration: none; color: #5C2B03; cursor: pointer; font-size: 15px;">&#9835 Bài hát 1</a>
                                </div>
                            </li>
                            <li>
                                <div style="padding: 10px 5px 10px 5px; margin: 0 5px 0 5px; border-bottom: 1px solid #E6E6E6;">
                                    <a style="text-decoration: none; color: #5C2B03; cursor: pointer; font-size: 15px;">&#9835 Bài hát 1</a>
                                </div>
                            </li>
                            <li>
                                <div style="padding: 10px 5px 10px 5px; margin: 0 5px 0 5px; border-bottom: 1px solid #E6E6E6;">
                                    <a style="text-decoration: none; color: #5C2B03; cursor: pointer; font-size: 15px;">&#9835 Bài hát 1</a>
                                </div>
                            </li>--%>
                        </ul>
                    </div>
                </div>
                <div style="position: relative; text-align: right; padding-right: 25px; top: -25px;"><a style="text-decoration: none; cursor: pointer; color: red; font-size: 12px; font-style: italic;" href="http://pgkhanhhoa.com/module/musiclist/cd197a39-e80f-42dd-9605-2fa24b53bea4">Xem thêm >></a></div>
            </div>
        </div>
    </div>
</div>
<%--<style>
    .Media_tempContainer {
        margin-top: 140px;
        /*background-color: aqua;*/
        width: 670px;
        height: 700px;
    }

    .Media_container {
        /*background-color: red;*/
    }

    ul.Media_tabs {
        margin: 0px;
        padding: 0px;
        list-style: none;
    }

        ul.Media_tabs li {
            background: #E3E3E3;
            color: #222;
            display: inline-block;
            padding: 15px 0 15px 0;
            cursor: pointer;
            width: 160px;
            text-align: center;
            font-size: 14px;
        }

            ul.Media_tabs li.current {
                background: #F2EBEB;
                color: #222;
            }

    .Media_tab-content {
        display: none;
        padding: 15px;
        height: 330px;
    }

        .Media_tab-content.current {
            display: inherit;
            background: #F2EBEB;
        }
</style>
<div class="Media_tempContainer">
    <div class="Media_container">
        <ul class="Media_tabs">
            <li class="Media_tab-link current" data-tab="tab-1">Pháp thoại</li>
            <li class="Media_tab-link" data-tab="tab-2">Âm nhạc</li>
        </ul>

        <div id="tab-1" class="Media_tab-content current">
            <table>
                <tr valign="top">
                    <td>
                        <div style="padding: 15px 0 0 0;">
                            <video width="350" height="240" controls style="background-color: black;">
                                <source src="/Portlets/Media/movie.mp4" type="video/mp4">k
                        Your browser does not support the video tag.
                            </video>
                        </div>
                        <div style="width: 350px; color: #705444; padding: 15px 0 0 0; font-weight: bold;">Clip a</div>
                    </td>
                    <td>
                        <div style="margin-left: 20px; width: 268px; margin: 15px 0 0 20px; padding: 0 2px 0 2px; background-color: white; height: 275px;">
                            <ul style="list-style-position: inside;">
                                <li style="margin-left: 2px;">
                                    <div style="padding: 5px 0 15px 5px; border-bottom: 1px solid gray;"><a>Clip B Clip B Clip B Clip B Clip B Clip B Clip B Clip B Clip B Clip B</a></div>
                                </li>
                                <li style="margin-left: 2px;">
                                    <div style="padding: 5px 0 15px 5px; border-bottom: 1px solid gray;"><a>Clip B Clip B Clip B Clip B Clip B Clip B Clip B Clip B Clip B Clip B</a></div>
                                </li>
                                <li style="margin-left: 2px;">
                                    <div style="padding: 5px 0 15px 5px; border-bottom: 1px solid gray;"><a>Clip B Clip B Clip B Clip B Clip B Clip B Clip B Clip B Clip B Clip B</a></div>
                                </li>
                                <li style="margin-left: 2px;">
                                    <div style="padding: 5px 0 15px 5px; border-bottom: 1px solid gray;"><a>Clip B Clip B Clip B Clip B Clip B Clip B Clip B Clip B Clip B Clip B</a></div>
                                </li>
                            </ul>
                            <div style="text-align: right; padding-right: 10px;">Xem thêm >></div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div id="tab-2" class="Media_tab-content">
        Âm nhạc
    </div>
</div>
</div>
<script>
    pscjq(document).ready(function () {

        pscjq('ul.Media_tabs li').click(function () {
            var tab_id = pscjq(this).attr('data-tab');

            pscjq('ul.Media_tabs li').removeClass('current');
            pscjq('.Media_tab-content').removeClass('current');

            pscjq(this).addClass('current');
            pscjq("#" + tab_id).addClass('current');
        })
    })
</script>--%>
