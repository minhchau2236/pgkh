

Type.registerNamespace("System.Utility");

System.Utility.SliderTopicDisplay = function SliderPGW(containerId, sliderId, example5) {
    var self = this;
    this.containerId = containerId;
    this.sliderId = $.parseJSON(sliderId);
    this.sliderId.convertDateTime = function (datetime) {
        return moment(datetime).format('DD/MM/YYYY');
    }
    ko.applyBindings(this.sliderId, $("#" + containerId)[0]);
}



System.Utility.SliderTopicDisplay.prototype = {
    CreateSlider: function () {
        var self = this;
        $("#" + this.containerId).SliderPGW();
    }
}