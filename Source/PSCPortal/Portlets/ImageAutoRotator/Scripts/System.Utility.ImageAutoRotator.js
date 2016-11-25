Type.registerNamespace("System.Utility");

System.Utility.ImageAutoRotator = function ImageAutoRotator(containerId, imageList) {
    this.containerId = containerId;
    this.imageList = $.parseJSON(imageList);
    ko.applyBindings(this.imageList, $("#" + containerId)[0]);
}

System.Utility.ImageAutoRotator.prototype = {
    CreateSlider: function (isAnimated) {
        var self = this;
        $("#" + this.containerId).microfiche({
            autoplay: 3, // Automatically advances every `n` seconds.
            buttons: true, // If true, microfiche will create previous/next buttons.
            bullets: false, // If true, microfiche will create bullets for the pages available.
            cyclic: true, // If true, microfiche wraps around at front and beginning of the slideshow.
            keyboard: false,
            swipe: true,
            clickToAdvance: false,
            minDuration: 250,
            duration: 500,
            maxDuration: 500,
            dragThreshold: 25,
            elasticity: 0.5,
            swipeThreshold: 0.125
        });
        if (isAnimated) {
            $.each($(".microfiche_Slide img"), function (idx, item) {
                $(item).hover(function () {
                    if (!$(item).is(':animated')) {
                        $(item).animate({
                            paddingTop: "0px"
                        }, 150);
                    }
                },
                function () {
                    $(item).animate({
                        paddingTop: "10px"
                    }, 150);
                });
            });
        }
    }
}