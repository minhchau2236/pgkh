
Type.registerNamespace("System.Utility");

System.Utility.ImageInnerFade = function InnerFadeEffect(containerId, portletId) {
    this.containerId = containerId;
    this.portletId = portletId;
}

System.Utility.ImageInnerFade.prototype =
{
    createImageInnerFade: function (picArr) {
        var temp = picArr;
        var containerInnerFade = document.getElementById(this.containerId);
        var ulHolder = document.createElement('ul');
        ulHolder.setAttribute("class", "portfolio");
        ulHolder.setAttribute("className", "portfolio");
        containerInnerFade.appendChild(ulHolder);
        for (i = 0; i < temp.length; i++) {
            var item = temp[i];
            var liParent = document.createElement('li');
            var link = document.createElement('a');
            link.setAttribute('href', item.Link);
            link.setAttribute('target', "_blank");
            var imgTag = document.createElement('img');
            imgTag.setAttribute('src', '/' + item.Url);
            imgTag.setAttribute('class', 'imgSlider');
            imgTag.setAttribute('title', item.Title);
            link.appendChild(imgTag);
            liParent.appendChild(link);
            ulHolder.appendChild(liParent);
        }
        this.registerJquery();
    },
    registerJquery: function () {
        jQuery(document).ready(
		            function () {
		                jQuery('ul.portfolio').innerfade({
		                    animationtype: 'fade',
		                    speed: 'slow',
		                    timeout: 5000,
		                    type: 'sequence'
		                });
		            });
    }
}
try {
    System.Utility.registerClass("System.Utility.ImageInnerFade");
}
catch (e) {
}

