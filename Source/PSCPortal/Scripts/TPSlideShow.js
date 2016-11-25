TPSlideShow = function TPSlideShow(TPNiceSlideShowMenuWrpId, TPSlideShowMenuId, tpniceslideshow_scrollbarId, tpniceslideshow_handleId, currentPage, sizeOfBlock, recordInPage, menuItems, datasource) {
    this.TPSlideShowMenuId = TPSlideShowMenuId;
    this.TPNiceSlideShowMenuWrpId = TPNiceSlideShowMenuWrpId;
    this.tpniceslideshow_scrollbarId = tpniceslideshow_scrollbarId;
    this.tpniceslideshow_handleId = tpniceslideshow_handleId;
    this.currentPage = currentPage;
    this.sizeOfBlock = sizeOfBlock;
    this.recordInPage = recordInPage;
    this.menuItems = menuItems;
    this.datasource = datasource;
}

TPSlideShow.prototype =
{
    createTPSlideShowMenu: function () {
        this.createTPSlideShowMenuItem();
    },
    setMenuItems: function (datasource) {
        this.menuItems = datasource;
    },
    ChangeImage: function (url) {

        var image = new Image();
        image.onerror = function (aaa) {
            alert('error loading');
        };
        image.onload = function (aaa) {
            alert('loaded');
        };

        image.src = url;
    },
    createTPSlideShowMenuItem: function () {
        var menu = document.getElementById(this.TPSlideShowMenuId);
        var instance = this;
        var divblueberry = document.createElement('div');
        divblueberry.setAttribute('id', 'blueberry');
        divblueberry.setAttribute('class', 'blueberry');
        divblueberry.setAttribute('runat', 'server');
        menu.appendChild(divblueberry);

        var ul_slides = document.createElement('ul');
        ul_slides.setAttribute('class', 'slides');
        divblueberry.appendChild(ul_slides);


        for (var j = 0; j < this.datasource.length; j = j + 3) {
            var liItem = document.createElement('li');
            ul_slides.appendChild(liItem);
            var lenght = this.menuItems.length;
            for (var i = j; i < lenght + j; i++) {
                var item = this.datasource[i];
                if (item == undefined)
                    return;
                var div_ndtin = document.createElement('div');
                div_ndtin.setAttribute('class', 'ndtin');
                liItem.appendChild(div_ndtin);
                //             var avata=item.Avatar;
                //            var divimg=document.createElement('div');
                //            divimg.setAttribute('class','img_tin');
                //            divimg.innerHTML=avata;
                //            div_ndtin.appendChild(divimg);
                var img = item.ArtImage;

                //get url image in tag
                var imgelement = document.createElement('img');
                if (item.Article.ImageUrl != "")
                    imgelement.setAttribute('src', item.Article.ImageUrl);
                else
                    imgelement.setAttribute('src', "/Temp/ArticleImage/Image.jpg");
                imgelement.setAttribute('style', 'width:100px; height:80px; float:left;padding-right:5px;');
                div_ndtin.appendChild(imgelement);
                var div_ct_tin = document.createElement('div');
                div_ct_tin.setAttribute('class', 'ct_tin');
                div_ndtin.appendChild(div_ct_tin);

                var aLink = document.createElement('a');
                aLink.setAttribute('href', '/?ArticleId=' + item.Article.Id);
                aLink.setAttribute('style', 'line-height:1.2em;');
                div_ct_tin.appendChild(aLink);

                var title = document.createElement('span');
                title.setAttribute('style', 'line-height:1.2em;');
                var TextNodetitle = document.createTextNode(item.Article.Title);
                title.appendChild(TextNodetitle);
                aLink.appendChild(title);

                var description = item.Description;
                var pDescription = document.createElement('p');
                pDescription.innerHTML = description;
                div_ct_tin.appendChild(pDescription);
            }
        }
    },
}

