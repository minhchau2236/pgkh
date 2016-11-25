
Type.registerNamespace("System.Utility");

System.Utility.TopicTripleDisplay= function topicTripleDisplay(containerId, articleList) {
	var self = this;
	this.containerId = containerId;

	this.articleList = pscjq.parseJSON(articleList);

	ko.applyBindings(this.articleList, pscjq("#" + containerId)[0]);
}