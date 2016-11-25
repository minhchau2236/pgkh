Type.registerNamespace("System.Utility");

System.Utility.ImageViewer = function NivoSlider(containerId, imageList) {
    this.containerId = containerId;
    this.imageList = pscjq.parseJSON(imageList);
    ko.applyBindings(this.imageList, pscjq("#" + containerId)[0]);
}

