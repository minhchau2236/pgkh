Type.registerNamespace("System.Utility");

System.Utility.NivoSlider = function NivoSlider(containerId, imageList) {
    this.containerId = containerId;
    this.imageList = $.parseJSON(imageList);
    ko.applyBindings(this.imageList, $("#" + containerId)[0]);
}

System.Utility.NivoSlider.prototype = {
    CreateSlider: function () {
        var self = this;
        $("#" + this.containerId).nivoSlider({
            effect: 'fade',
            slices: 15,
            boxCols: 8,
            boxRows: 4,
            animSpeed: 500,
            pauseTime: 3000,
            startSlide: 0,
            directionNav: true,
            controlNav: true,
            controlNavThumbs: false,
            pauseOnHover: true,
            manualAdvance: false,
            prevText: 'Prev',
            nextText: 'Next',
            randomStart: false,
            beforeChange: function () { },
            afterChange: function () { },
            slideshowEnd: function () { },
            lastSlide: function () { },
            afterLoad: function () { }
        });
    }
}