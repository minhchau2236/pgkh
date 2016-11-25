Type.registerNamespace("System.Utility");

System.Utility.CarouselSlider = function CarouselSlider(containerId, imageList) {
    this.containerId = containerId;
    this.imageList = pscjq.parseJSON(imageList);
    ko.applyBindings(this.imageList, pscjq("#" + containerId)[0]);
}

System.Utility.CarouselSlider.prototype = {
    CreateSlider: function () {
        var self = this;
        pscjq("#" + this.containerId + ' .variable-width').slick({
                dots: true,
                infinite: true,                
                slidesToShow: 1,
                centerMode: true,
                variableWidth: true
        });

    }
}