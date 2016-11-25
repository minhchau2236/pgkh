

Type.registerNamespace("System.Utility");

System.Utility.SliderTopicDisplay = function NivoSlider(containerId, sliderId, sliderTopicDisplay) {
    var self = this;
    this.containerId = containerId;
    this.sliderId = sliderId;
   
    this.sliderTopicDisplay = pscjq.parseJSON(sliderTopicDisplay);
    this.sliderTopicDisplay.convertDate = function (datetime) {
        return moment(datetime).format('DD/MM/YYYY');
    }
   
    this.slider = pscjq('#' + sliderId);
    //this.setupCaption = function () {
    //    var navigate = self.slider.find('.nivo-directionNav').remove();
    //    var caption = self.slider.find('.nivo-caption').empty();
    //    var curSlide = self.slider.data('nivo:vars').currentSlide,
    //        curTitle = self.slider.find('img').eq(curSlide).attr('title');
    //    if (!curTitle) return

    //    var title = curTitle.split('___');
    //    var htmlString = '<div class="maudo-title"><a href="/?ArticleId=' + title[2] + '" style="text-decoration: none;" >' + title[0] + '</a></div>';
    //    htmlString += '<div class="maudo-noidung">' +
    //        	title[1] +
    //        '</div>';
    //    htmlString += '<div class="nut">' +
    //        	'<ul>'+
    //        	'<li><a class="nivo-prevNav"><img src="/Resources/ImagesPortal/HomePage/imgs/previous button.png" style="padding-right:3px" class="navigationImg"/></a></li>' +
    //            '<li><a class="nivo-nextNav"><img src="/Resources/ImagesPortal/HomePage/imgs/next button.png" class="navigationImg"/></a></li>' +
    //       ' </ul>'+
    //    '</div>';

    //    //htmlString += ' <div class="nivo-directionNav">'+ 
    //    //   '<a class="nivo-prevNav"><img height="30" width="30" src="/Resources/ImagesPortal/HomePage/imgs/previous button.png" class="navigationImg"/> </a><a class="nivo-nextNav"></a></div>';
    //    caption.append(htmlString);             
    //}
    //this.hideCaption= function () {
    //    setTimeout(function () {
    //        var navigate = self.slider.find('.nivo-directionNav').remove();
    //        //self.slider.find('.maudo_title').hide();
    //        //self.slider.find('.maudo_noidung').hide();
    //        var htmlString = '<div class="maudo-title"></div>';
    //        htmlString += '<div class="maudo-noidung">'+
    //            '</div>';
    //        htmlString += '<div class="nut">' +
    //                '<ul>' +
    //                '<li><a class="nivo-prevNav"><img src="/Resources/ImagesPortal/HomePage/imgs/previous button.png" style="padding-right:3px" class="navigationImg"/></a></li>' +
    //                '<li><a class="nivo-nextNav"><img src="/Resources/ImagesPortal/HomePage/imgs/next button.png" class="navigationImg"/></a></li>' +
    //           ' </ul>' +
    //        '</div>';
    //        var caption = self.slider.find('.nivo-caption').html(htmlString);
    //    }, 520);
    //    //caption.empty();
    //};
    ko.applyBindings(this.sliderTopicDisplay, pscjq("#" + containerId)[0]);
}



System.Utility.SliderTopicDisplay.prototype = {
    CreateSlider: function () {
        var self = this;
        pscjq("#" + this.sliderId).nivoSlider({
            slices: 15,
            boxCols: 8,
            boxRows: 4,
            animSpeed: 500,
            pauseTime: 300000000,
            startSlide: 0,
            controlNav: true,
            controlNavThumbs: true,
            pauseOnHover: true,
            manualAdvance: false,
            prevText: 'Prev',
            nextText: 'Next',
            randomStart: false,
            //beforeChange: function () { },
            //afterChange: function () {},
            //slideshowEnd: function () { },
            //lastSlide: function () { },
            //afterLoad: function () {  }
        });
    }
}