
Type.registerNamespace("System.Utility");

System.Utility.GalleryView = function GalleryView() {
}

System.Utility.GalleryView.prototype = {
    createGalleryView: function (galleryId, galleryPath, galleryObj, galleryLink) {
        var gallery = document.getElementById(galleryId);
        //tao khung chua thumbnails
        var ulThumb = document.createElement('ul');
        ulThumb.setAttribute('class', 'filmstrip');
        ulThumb.setAttribute('className', 'filmstrip');
        ulThumb.style.width = 1000 + 'px';
        ulThumb.style.height = 123 + 'px';
        ulThumb.style.float = 'left';

        for (i = 0; i < galleryObj.length; i++) {
            //tao khung chua cac picture
            var panel = document.createElement('div');
            panel.setAttribute('class', 'panel');
            panel.setAttribute('className', 'panel');
            gallery.appendChild(panel);

            //tao 1 anh trong thumb
            var liThumb = document.createElement('li');

            var link = document.createElement('a');
            link.setAttribute('href', galleryLink[i]);
            //link.setAttribute('href', "google.com.vn");
            link.setAttribute("class", "NoBorder");
            link.setAttribute("className", "NoBorder");
            link.setAttribute("target", "_blank");
            //panelHolder.appendChild(link);

            var imgThumb = document.createElement('img');
            var imgThumbSrc = galleryPath + '/Thumbnail/frame-' + galleryObj[i];
            imgThumb.style.width = 100 + 'px';
            imgThumb.style.height = 70 + 'px';
            imgThumb.style.padding = 0 + 'px';
            imgThumb.setAttribute('src', imgThumbSrc);

            link.appendChild(imgThumb);
            liThumb.appendChild(link);
            ulThumb.appendChild(liThumb);
        }
        gallery.appendChild(ulThumb);
        /*
        $(document).ready(function() {
        $('#' + galleryId).galleryView({
        panel_width: 571,
        panel_height: 300,
        frame_width: 100,
        frame_height: 100
        });
        });*/
        jQuery(function ($) {
            $('#' + galleryId).galleryView({
                panel_width: 1000,
                panel_height: 0,
                frame_width: 100,
                frame_height: 100
            });
        });
    }
}