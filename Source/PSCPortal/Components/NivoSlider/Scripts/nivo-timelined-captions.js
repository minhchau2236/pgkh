/*
* Improve your Nivo slider's captions!
* (Timeline technique)
* http://www.soslignes-ecrivain-public.fr/English-Blog.html
* v1.2
* November 2013
* Use and abuse!
*/

var topCaption, leftCaption, speed, delayFactor, captionsOpac, animDirection, animPosDelta, spacing, centering, bounce, upsideDown, alternateDirection, zoom,
	caption = new Array(),
	captionsNb,
	settingNb,
	captionWidth = new Array(),
captionHeight = new Array(),
captionFont = new Array();

function resetDefaults() {
    topCaption = 40; 				// First caption is at top:40px
    leftCaption = 40;				// and left:40px
    speed = 250;					// Caption animation time (ms)
    delayFactor = 0.8;			// caption#n+1 starts animating after (speed*0.8)ms
    captionsOpac = 0.4;			// Caption opacity
    animDirection = 'horizontal';	// 'horizontal', 'vertical' or 'diagonal'
    animPosDelta = -200;			// Initial shift position (here: -200px)
    spacing = 10;					// Distance between captions (here: 10px)
    centering = false;			// true: centered captions
    bounce = false;				// true: elastic effect
    upsideDown = false;			// true: caption#n+1 will be above caption#n
    alternateDirection = false;	// true: accordion effect
    zoom = false;					// true: captions zoomIn
}

function timelineCaption() {
    resetDefaults();
    slider = $('.nivoSlider');
    var curSlide = slider.data('nivo:vars').currentSlide,
		curTitle = slider.find('img').eq(curSlide).attr('title'),
		curData = slider.find('img').eq(curSlide).attr('data-captions');
    if (!curTitle) return

    if (curData) {
        var setting = curData.split(';'),
			parameters = ['topCaption', 'leftCaption', 'speed', 'delayFactor', 'captionsOpac', 'animDirection', 'animPosDelta', 'spacing', 'centering', 'bounce', 'upsideDown', 'alternateDirection', 'zoom'];
        settingNb = setting.length;
        for (var i = 1; i < settingNb + 1; i++) {
            var j = setting[i - 1].split('='),
				k = $.inArray(j[0], parameters);
            if (k > -1) {
                eval(parameters[k] + "=" + j[1]);
            };
        };
    };

    title = curTitle.split(','),
	captionsNb = title.length;

    caption[1] = slider.find('.nivo-caption')
	.stop(1, 1)
	.css({
	    left: 0,
	    top: 0,
	    marginLeft: 0,
	    marginTop: 0,
	    opacity: 0
	})
	.empty()
	.append(title[0]);

    for (var i = 1; i < captionsNb + 1; i++) {

        if (i != 1) {
            slider.append('<div class="nivo-caption' + i + '"></div>');
            caption[i] = slider.find('.nivo-caption' + i)
			.append(title[i - 1]);
        };

        captionWidth[i] = caption[i].outerWidth();
        captionHeight[i] = caption[i].outerHeight();
        captionFont[i] = parseInt(caption[i].css('font-size'), 10);

        caption[i].css({
            top: topCaption,
            left: leftCaption,
            opacity: 0,
            fontSize: !zoom * captionFont[i]
        });
    };

    var yShift = topCaption,
		moveTop = 0,
		centerShift = 0,
		moveLeft = animPosDelta,
		ad;

    if (animDirection == 'vertical') {
        moveLeft = 0;
        moveTop = animPosDelta;
    };
    if (animDirection == 'diagonal') {
        moveTop = moveLeft = animPosDelta;
    };

    for (var i = 1; i < captionsNb + 1; i++) {

        if (i > 1) {
            centerShift = centering * (captionWidth[1] - captionWidth[i]) / 2;
        }
        caption[i]
		.css({
		    marginLeft: moveLeft + centerShift + (zoom * captionWidth[i] / 2),
		    marginTop: moveTop + (zoom * captionHeight[i] / 2),
		    top: yShift
		})
		.delay(speed * (delayFactor * (i - 1)))
		.animate({
		    opacity: captionsOpac,
		    marginLeft: centerShift - (bounce * moveLeft * 0.1),
		    marginTop: -(bounce * moveTop * 0.1),
		    fontSize: captionFont[i]
		}, speed * (1 - (0.4 * bounce)));

        if (bounce) {
            caption[i].animate({
                marginLeft: centerShift,
                marginTop: 0
            }, speed * 0.4);
        };

        yShift += (spacing + captionHeight[i + upsideDown]) * Math.pow(-1, upsideDown);
        ad = Math.pow(-1, alternateDirection);
        moveLeft *= ad;
        moveTop *= ad;
    };
};

function hideCaptions() {
    setTimeout(function() {
        curTitle = slider.find('img').eq(slider.data('nivo:vars').currentSlide).attr('title');
        if (!curTitle) caption[1].animate({ marginLeft: 9999 }, 1);
    }, 20);
    var j = 0;
    for (var i = 1; i < captionsNb + 1; i++) {
        caption[i].animate({ opacity: 0 }, 300);
        setTimeout(function () {
            j++;
            if (j != 1) caption[j].remove();
        }, 300);
    };
};