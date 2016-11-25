Type.registerNamespace("System.Utility");

System.Utility.Media = function VideoClipPlayer(containerId, resultList) {
    this.containerId = containerId;
    this.resultList = pscjq.parseJSON(resultList);

    ko.applyBindings(this.resultList, pscjq("#" + containerId)[0]);

    //if()
    if (this.resultList.videos.length != 0) {
        var fileEx = this.resultList.videos[0].Path.substring(this.resultList.videos[0].Path.toString().lastIndexOf(".") + 1).toLowerCase();
        if (fileEx == "flv")
            fileEx = "flv";
        fileEx = "video/" + fileEx;

        var videoDiv = document.getElementById('videoDiv');
        videoDiv.innerHTML = '<div style="padding: 15px 0 0 0;">' + '<video id="myVideo" width="400" height="315" controls style="background-color: black;"><source src="' + this.resultList.videos[0].Path + '" type="' + fileEx + '">Trình duyệt của bạn không hổ trợ xem video.</video>' + '</div><div style="width: 350px; color: #5C2B03; padding: 15px 0 0 0; font-weight: bold;">' + this.resultList.videos[0].Title + '</div>';
        myVideo = document.getElementById('myVideo');
        //pscjq('video').mediaelementplayer();
    }
    else {
        document.getElementById('VideoPlayer').innerHTML = 'Không có dữ liệu.'
    }

    if (this.resultList.musics.length != 0) {
        var musicDiv = document.getElementById('musicDiv');
        musicDiv.innerHTML = '<div style="color: #5C2B03; font-weight: bold; margin-top: 10px;">' + this.resultList.musics[0].Title + '<audio id="myAudio" controls style="width: 100%; margin-top: 5px;"><source src="' + this.resultList.musics[0].Path + '" type="audio/mpeg">Trình duyệt của bạn không hổ trợ nghe nhạc.</audio></div>';
        myAudio = document.getElementById('myAudio');
    }
    else {
        document.getElementById('MusicPlayer').innerHTML = 'Không có dữ liệu.'
    }
}