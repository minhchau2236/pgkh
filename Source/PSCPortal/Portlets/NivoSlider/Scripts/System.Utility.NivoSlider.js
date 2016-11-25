Type.registerNamespace("System.Utility");

System.Utility.NivoSlider = function NivoSlider(containerId, imageList) {
    this.containerId = containerId;
    this.imageList = pscjq.parseJSON(imageList);
    ko.applyBindings(this.imageList, pscjq("#" + containerId)[0]);
}

System.Utility.NivoSlider.prototype = {
    CreateSlider: function () {
        var total = pscjq('.nivo-control').length;
        var current_slide_no = 1;
        var self = this;
        pscjq("#" + this.containerId).nivoSlider({
            //effect: 'fade',
            slices: 15,
            boxCols: 8,
            boxRows: 4,
            animSpeed: 500,
            pauseTime: 5000,
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
            afterChange: function() {
            current_slide_no = pscjq('.nivoSlider').data('nivo:vars').currentSlide;
            pscjq('#nivo-slider-status > .current-slide').html((current_slide_no + 1)+" / ");
            },
            slideshowEnd: function () { },
            lastSlide: function () { },
            afterLoad: function () { }
            
        });
    }
}