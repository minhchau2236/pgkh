

Type.registerNamespace("System.Utility");

System.Utility.SliderTopicDisplay12 = function sliderPro(containerId, sliderId, example5) {
    var self = this;
    this.containerId = containerId;
    this.sliderId = pscjq.parseJSON(sliderId);
    this.sliderId.convertDateTime = function (datetime) {
        return moment(datetime).format('DD/MM/YYYY');
    }
    ko.applyBindings(this.sliderId, pscjq("#" + containerId)[0]);
}



System.Utility.SliderTopicDisplay12.prototype = {
    CreateSlider: function () {
        var self = this;
        pscjq("#featured").tabs({
            fx: {
                opacity: "toggle"
            }
       
        }).tabs("rotate", 5088800, true);
    }
}