
Type.registerNamespace("System.Utility");

System.Utility.SliderTopicDisplay = function sliderPro(containerId, articleList, example5) {
	var self = this;
	this.containerId = containerId;

	this.articleList = pscjq.parseJSON(articleList);
	this.articleList.convertDateTime = function (datetime) {
		return moment(datetime).format('DD/MM/YYYY');
	}
	ko.applyBindings(this.articleList, pscjq("#" + containerId)[0]);
}

System.Utility.SliderTopicDisplay.prototype = {
	CreateSlider: function () {
		var self = this;
		pscjq("#" + this.containerId).sliderPro({
			width: 660,
			height: 267,
			orientation: 'horizontal',
			loop: false,
			arrows: true,
			slideAnimationDuration: 1000,
			buttons: false,
			thumbnailsPosition: 'bottom',
			thumbnailPointer: true,
			thumbnailWidth: 167,
			thumbnailHeight: 180

		});
	}
}