Type.registerNamespace("System.Utility");

System.Utility.CarouselSlider = function CarouselSlider(containerId, imageList) {
    this.containerId = containerId;
    this.imageList = $.parseJSON(imageList);
    ko.applyBindings(this.imageList, $("#" + containerId)[0]);
}

System.Utility.CarouselSlider.prototype = {
    CreateSlider: function () {
        var self = this;
        $("#" + this.containerId + ' #sliderImgs').carouFredSel({
            next: '#nextCarousel1',
            responsive: true,
            width: '100%',
            scroll: {
                items: 1,
                duration: 1000,
                pauseOnHover: true
            },
            circular: true,
            items: {
                visible: {
                    min: 2,
                    max: 11
                }
            }
        });



    }
}