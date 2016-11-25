TPSlideShow = function TPSlideShow(TPNiceSlideShowMenuWrpId, TPSlideShowMenuId, tpniceslideshow_scrollbarId, tpniceslideshow_handleId, videoHolderId, menuItems) {
    this.TPSlideShowMenuId = TPSlideShowMenuId;
    this.TPNiceSlideShowMenuWrpId = TPNiceSlideShowMenuWrpId;
    this.tpniceslideshow_scrollbarId = tpniceslideshow_scrollbarId;
    this.tpniceslideshow_handleId = tpniceslideshow_handleId;
    this.videoHolderId = videoHolderId;
    this.menuItems = menuItems;
}

TPSlideShow.prototype =
{
    RegisterEvent: function (instance, element, value) {
        element.onclick = function () { instance.LoadVideo(value, 'true') };
    },
    createTPSlideShowMenu: function () {
        this.createTPSlideShowMenuItem();
    },
    setMenuItems: function (datasource) {
        this.menuItems = datasource;
    },
    createTPSlideShowMenuItem: function () {
        var menu = document.getElementById(this.TPSlideShowMenuId);
        var instance = this;
        for (i = 0; i < this.menuItems.length; i++) {
            var item = this.menuItems[i];
            var divItem = document.createElement('div');
            divItem.setAttribute('class', 'TPNiceSlideShowMenuContent');
            divItem.setAttribute('className', 'TPNiceSlideShowMenuContent');
            menu.appendChild(divItem);

            //     var imgItem = document.createElement('img');
            //     imgItem.setAttribute('src', '/Portlets/FlvVideo/ImageFactory.ashx?Img=' + item.Id);

            var pItem = document.createElement('p');
            var aLink = document.createElement('a');
            aLink.setAttribute('href', 'javascript:void(0);');

            TPSlideShow.prototype.RegisterEvent(instance, aLink, item.Link.substring(1));
            pItem.appendChild(aLink);

            var spanTitle = document.createElement('span');
            spanTitle.setAttribute('class', 'tpnssmenu_title');
            spanTitle.setAttribute('className', 'tpnssmenu_title');
            var titleTextNode = document.createTextNode(item.Name);
            spanTitle.appendChild(titleTextNode);

            var brTag = document.createElement('br');

            var spanDesc = document.createElement('span');
            spanDesc.setAttribute('class', 'tpnssmenu_desc');
            spanDesc.setAttribute('className', 'tpnssmenu_desc');
            var descTextNode = document.createTextNode(item.Description);
            spanDesc.appendChild(descTextNode);

     //       aLink.appendChild(spanTitle);
 //           aLink.appendChild(brTag);
            aLink.appendChild(spanDesc);

            //      divItem.appendChild(imgItem);
            divItem.appendChild(pItem);

        }

        this.startTPNiceSlideShow();
        if (this.menuItems.length > 0)
            this.LoadVideo(this.menuItems[0].Link.substring(1), this.menuItems[0].Id, 'false');
    },

    startTPNiceSlideShow: function () {
        var TPNiceSlideShow = new TPSlideShowClass($('TPNiceSlideShow'), {
            timed: true,
            showInfopane: true,
            showMenu: true,
            embedLinks: true,
            fadeDuration: 500,
            readMore: true,
            readMoreText: 'Read More',
            showTitle: true,
            titleLink: true,
            showDescription: true,

            scrollBar: true,
            itemCount: 10,
            scrollBarContent: $(this.TPNiceSlideShowMenuWrpId),
            scrollBarArea: $(this.tpniceslideshow_scrollbarId),
            scrollBarHandle: $(this.tpniceslideshow_handleId),

            delay: 3000
        });
    },

    LoadVideo: function (urlVideo, idPicture, autostart) {
        var flashHolder = document.getElementById(this.videoHolderId);
        var sFlashPlayer15374 = new SWFObject("/Portlets/FlvVideo/Scripts/mediaplayer.swf", "playlist", "218", "208", "7");
        sFlashPlayer15374.addParam("allowfullscreen", "true");
        sFlashPlayer15374.addParam("wmode", "transparent");
        sFlashPlayer15374.addVariable("file", urlVideo);
        //        sFlashPlayer15374.addVariable("image", "/Portlets/FlvVideo/Images/khanhhoa.jpg");
        sFlashPlayer15374.addVariable("image", "/Resources/ClipNewsTemp/"+ idPicture+".jpg");
        sFlashPlayer15374.addVariable("displayheight", "208");
        sFlashPlayer15374.addVariable("width", "218");
        sFlashPlayer15374.addVariable("height", "208");
        sFlashPlayer15374.addVariable("backcolor", "0x000000");
        sFlashPlayer15374.addVariable("frontcolor", "0xCCCCCC");
        sFlashPlayer15374.addVariable("lightcolor", "0x557722");
        sFlashPlayer15374.addVariable("shuffle", "false");
        sFlashPlayer15374.addVariable("autostart", autostart);
        sFlashPlayer15374.addVariable("repeat", "list");
        sFlashPlayer15374.write(flashHolder);
    }

}

