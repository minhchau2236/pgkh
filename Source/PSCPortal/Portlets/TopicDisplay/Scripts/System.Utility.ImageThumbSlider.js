Type.registerNamespace("System.Utility");
System.Utility.ImageThumbSlider = function ImageThumbSlider(containerId, articleList) {
    this.containerId = containerId;
    this.articleList = articleList;
}
System.Utility.ImageThumbSlider.prototype = {
    toJavaScriptDate: function (value) {
        var pattern = /Date\(([^)]+)\)/;
        var results = pattern.exec(value);
        var dt = new Date(parseFloat(results[1]));
        return dt.getDate() + "/" + (dt.getMonth() + 1) + "/" + dt.getFullYear();
    },
    AutoScroll: function (containerId) {
        $("#" + containerId).als({
            visible_items: 4,
            scrolling_items: 1,
            orientation: "vertical",
            circular: "yes",
            autoscroll: "yes",
            start_from: 0,
            interval: 4000  //set time to scroll
        });
    },
    CreateSlider: function (containerId) {
        var containerSlider = document.getElementById(this.containerId);
        containerSlider.setAttribute("class", "root");
        containerSlider.setAttribute("id", this.containerId);
        var h2 = document.createElement('h2');
        var aLink = document.createElement('a');
        aLink.setAttribute('href', '/?ArticleId=' + this.articleList[0].Id);
        var addName = document.createTextNode(this.articleList[0].Name);
        aLink.appendChild(addName);
        h2.appendChild(aLink);

        var small = document.createElement('small');
        //var date = new Date(this.articleList[0].CreatedDate);
        var smallName = document.createTextNode(this.toJavaScriptDate(this.articleList[0].CreatedDate));
        small.appendChild(smallName);

        var block = document.createElement('div');
        block.setAttribute("class", "block");
        block.appendChild(h2);
        block.appendChild(small);

        var desc = document.createElement('div');
        desc.setAttribute("class", "desc");
        desc.appendChild(block);

        var main_image = document.createElement('div');
        main_image.setAttribute("class", "main_image");
        main_image.setAttribute("id", this.containerId);

        var imgTag = document.createElement('img');
        imgTag.setAttribute('src', '/Services/GetArticleImage.ashx?Id=' + this.articleList[0].Id);
        imgTag.setAttribute('alt', '');
        imgTag.setAttribute('style', 'cursor: hand; width: 100%; height: 309px; float: left;');

        main_image.appendChild(imgTag);
        main_image.appendChild(desc);

        var image_thumb = document.createElement('div');
        image_thumb.setAttribute("id", this.containerId);
        image_thumb.setAttribute("class", "image_thumb");

        var alsviewport = document.createElement('div');
        alsviewport.setAttribute("class", "als-viewport");

        image_thumb.appendChild(alsviewport);

        var ul_slides = document.createElement('ul');
        alsviewport.appendChild(ul_slides);
        for (var i = 0; i < this.articleList.length; i++) {
            var item = this.articleList[i];
            var liItem = document.createElement('li');
            liItem.setAttribute("class", "als-item");

            var link = document.createElement('a');
            link.setAttribute('href', '/Services/GetArticleImage.ashx?Id=' + item.Id);

            var img = document.createElement('img');
            img.setAttribute('src', '/Services/GetArticleImage.ashx?Id=' + item.Id);
            img.setAttribute('alt', '');
            img.setAttribute('style', 'cursor: hand; width: 62px; height: 50px; float: left;');

            var h2thumb = document.createElement('h2');
            var aLinkthumb = document.createElement('a');
            aLinkthumb.setAttribute('href', '/?ArticleId=' + item.Id);
            var addNamethumb = document.createTextNode(item.Name);
            aLinkthumb.appendChild(addNamethumb);
            h2thumb.appendChild(aLinkthumb);

            var smallthumb = document.createElement('small');
            var date = new Date(item.CreatedDate);
            var namesmall = document.createTextNode(this.toJavaScriptDate(item.CreatedDate));
            smallthumb.appendChild(namesmall);

            var blockthumb = document.createElement('div');
            blockthumb.setAttribute("class", "block");
            blockthumb.appendChild(h2thumb);
            blockthumb.appendChild(smallthumb);
            liItem.appendChild(link);
            liItem.appendChild(img);
            liItem.appendChild(blockthumb);
            ul_slides.appendChild(liItem);
        }
        containerSlider.appendChild(main_image);
        containerSlider.appendChild(image_thumb);
        this.AutoScroll(this.containerId);
        this.AutoPlaySlider(this.containerId);
    },
    AutoPlaySlider: function (containerId) {
        var intervalId;
        var slidetime = 4000; // milliseconds between automatic transitions
        // Comment out this line to disable auto-play
        intervalID = setInterval(cycleImage, slidetime);

        $("#" + containerId + ".main_image .desc").show(); // Show Banner
        $("#" + containerId + ".main_image .block").animate({ opacity: 0.85 }, 1); // Set Opacity
        //// Click and Hover events for thumbnail list
        $("#" + containerId + ".image_thumb ul li:first").addClass('active');
        $("#" + containerId + ".image_thumb ul li").click(function () {
            // Set Variables
            var imgAlt = $(this).find('img').attr("alt"); //  Get Alt Tag of Image
            var imgTitle = $(this).find('a').attr("href"); // Get Main Image URL
            var imgDesc = $(this).find('.block').html(); 	//  Get HTML of block
            var imgDescHeight = $("#" + containerId + ".main_image").find('.block').height();	// Calculate height of block	
            if ($(this).is(".active")) {  // If it's already active, then...
                return false; // Don't click through
            } else {
                // Animate the Teaser				
                $("#" + containerId + ".main_image .block").animate({ opacity: 0, marginBottom: -imgDescHeight }, 250, function () {
                    $("#" + containerId + ".main_image .block").html(imgDesc).animate({ opacity: 0.85, marginBottom: "0" }, 250);
                    $("#" + containerId + ".main_image img").attr({ src: imgTitle, alt: imgAlt });
                });
            }
            $("#" + containerId + ".image_thumb ul li").removeClass('active'); // Remove class of 'active' on all lists
            $(this).addClass('active');  // add class of 'active' on this list only
            return false;
        }).hover(function () {
            $(this).addClass('hover');
        }, function () {
            $(this).removeClass('hover');
        });
        // Toggle Teaser
        $("a.collapse").click(function () {
            $(".main_image .block").slideToggle();
            $("a.collapse").toggleClass("show");
        });
        // Function to autoplay cycling of images
        // Source: http://stackoverflow.com/a/9259171/477958
        function cycleImage() {
            var onLastLi = $("#" + containerId + ".image_thumb ul li:last").hasClass("active");
            var currentImage = $("#" + containerId + ".image_thumb ul li.active");
            if (onLastLi) {
                var nextImage = $("#" + containerId + ".image_thumb ul li:first");
            } else {
                var nextImage = $("#" + containerId + ".image_thumb ul li.active").next();
            }
            $(currentImage).removeClass("active");
            $(nextImage).addClass("active");
            // Duplicate code for animation
            var imgAlt = $(nextImage).find('img').attr("alt");
            var imgTitle = $(nextImage).find('a').attr("href");
            var imgDesc = $(nextImage).find('.block').html();
            var imgDescHeight = $(".main_image").find('.block').height();
            $("#" + containerId + ".main_image .block").animate({ opacity: 0, marginBottom: -imgDescHeight }, 250, function () {
                $("#" + containerId + ".main_image .block").html(imgDesc).animate({ opacity: 0.85, marginBottom: "0" }, 250);
                $("#" + containerId + ".main_image img").attr({ src: imgTitle, alt: imgAlt });
            });
        };
    }
};