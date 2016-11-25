//Type.registerNamespace("System.Utility");

//System.Utility.CarouselSlider = function CarouselSlider(containerId, imageList) {
//    this.containerId = containerId;
//    this.imageList = pscjq.parseJSON(imageList);
//    ko.applyBindings(this.imageList, pscjq("#" + containerId)[0]);
//}

//System.Utility.CarouselSlider.prototype = {
//    CreateSlider: function () {
//        var self = this;
//        pscjq("#" + this.containerId + ' #sliderImgs').carouFredSel({
//            prev: '#prev2',
//            next: '#next2',
//            pagination: "#pager2",
//            responsive: true,
//            width: '100%',
//            scroll: 2,
//            speed: 300,
//            items: {
//                width: 300,
//                //	height: '30%',	//	optionally resize item-height
//                visible: {
//                    min: 2,
//                    max: 6
//                }
//            }
//        });


//        //pscjq("#" + this.containerId + ' .variable-width').slick({
//        //    dots: true,
//        //    infinite: false,
//        //    speed: 300,
//        //    slidesToShow: 4,
//        //    slidesToScroll: 4,
//        //    responsive: [
//        //      {
//        //          breakpoint: 1024,
//        //          settings: {
//        //              slidesToShow: 3,
//        //              slidesToScroll: 3,
//        //              infinite: true,
//        //              dots: true
//        //          }
//        //      },
//        //      {
//        //          breakpoint: 600,
//        //          settings: {
//        //              slidesToShow: 2,
//        //              slidesToScroll: 2
//        //          }
//        //      },
//        //      {
//        //          breakpoint: 480,
//        //          settings: {
//        //              slidesToShow: 1,
//        //              slidesToScroll: 1
//        //          }
//        //      }
//        //      // You can unslick at a given breakpoint now by adding:
//        //      // settings: "unslick"
//        //      // instead of a settings object
//        //    ]

//        //});

//    }
//}






Type.registerNamespace("System.Utility");

System.Utility.CarouselSlider1 = function CarouselSlider(containerId, sliderId, sliderTopicDisplay) {
    var self = this;
    this.containerId = containerId;
    this.sliderId = sliderId;
  
    this.sliderTopicDisplay = pscjq.parseJSON(sliderTopicDisplay);
    this.sliderTopicDisplay.convertDateTime = function (datetime) {
        return moment(datetime).format('DD/MM/YYYY');
    }
    this.sliderTopicDisplay.convertDate = function (datetime) {
        return moment(datetime).format('DD');
    }

    this.sliderTopicDisplay.convertMonth = function (datetime) {
        return "tháng " + moment(datetime).format('MM');
    }
   
    ko.applyBindings(this.sliderTopicDisplay, pscjq("#" + containerId)[0]);
}



System.Utility.CarouselSlider1.prototype = {
    CreateSlider: function () {
        var self = this;
        pscjq("#" + this.containerId + ' #sliderImgs').carouFredSel({

            next: '#next234',
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