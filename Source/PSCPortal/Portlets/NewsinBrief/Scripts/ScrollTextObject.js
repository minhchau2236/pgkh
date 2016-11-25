
        Type.registerNamespace("Utility");

        Utility.ScrollText = function (content, parentOfScrollText) {
            this.marqueecontent = content;
            this.parentOfScroll = parentOfScrollText;
            //Specify the marquee's marquee speed (larger is faster 1-10)
            this.marqueespeed = 1;
         
            //Specify initial pause before scrolling in milliseconds
            this.initPause = 100;
            //Specify start with Full(1)or Empty(0) Marquee
            this.full = 1;
            //Pause marquee onMousever (0=no. 1=yes)?
            this.pauseit = 1;
            //Specify Break characters for IE as the two iterations
            //of the marquee, if text, will be too close together in IE
            this.iebreak = '<p></p>';

            this.copyspeed = this.marqueespeed;
            this.pausespeed = (this.pauseit == 0) ? this.copyspeed : 0;
            this.iedom = document.all || document.getElementById;
            this.actualheight = '';

            this.cross_marquee = null;
            this.cross_marquee2 = null;
            this.ns_marquee = null;
        }

                            Utility.ScrollText.prototype =
        {
            ///////////////////////////
            populate: function (width, height, userControlClientId) {
                var marqueewidth = width + "px";
                var marqueeheight = height + "px";
                if (this.iedom) {
                    var lb = document.getElementById && !document.all ? '' : this.iebreak;
                    this.cross_marquee = document.getElementById ? document.getElementById("iemarquee" + userControlClientId) : document.all.iemarquee;
                    this.cross_marquee2 = document.getElementById ? document.getElementById("iemarquee2" + userControlClientId) : document.all.iemarquee2;
                    this.cross_marquee.style.top = (this.full == 1) ? '8px' : parseInt(marqueeheight) + 8 + "px";
                    this.cross_marquee2.innerHTML = this.marqueecontent + lb + lb + "<br/>" + "<br/>";
                    this.cross_marquee.innerHTML = this.marqueecontent + lb + lb + "<br/>" + "<br/>";
                    this.actualheight = this.cross_marquee.offsetHeight;

                    this.cross_marquee2.style.top = (parseInt(this.cross_marquee.style.top) + this.actualheight + 8) + "px"; //indicates following #1
                }
                else if (document.layers) {
                    this.ns_marquee = document.ns_marquee.document.ns_marquee2;
                    this.ns_marquee.top = parseInt(marqueeheight) + 8;
                    this.ns_marquee.document.write(this.marqueecontent);
                    this.ns_marquee.document.close();
                    this.actualheight = ns_marquee.document.height;
                }

                //var ham = "scrollmarquee(" + "200px" + ",50px)";  //"scrollmarquee()"
                //setTimeout('lefttime=setInterval("this.scrollmarquee()",20)',this.initPause);
                // var timeLeft =setInterval("Utility.ScrollText.scrollmarquee()",20);
                var objInstance = this;
                var timeLeft = setInterval(function () { objInstance.scrollmarquee() }, 20);
            },

            scrollmarquee: function () {
                if (this.iedom) {
                    if (parseInt(this.cross_marquee.style.top) < (this.actualheight * (-1) + 8))
                        this.cross_marquee.style.top = (parseInt(this.cross_marquee2.style.top) + this.actualheight + 8) + "px";
                    if (parseInt(this.cross_marquee2.style.top) < (this.actualheight * (-1) + 8))
                        this.cross_marquee2.style.top = (parseInt(this.cross_marquee.style.top) + this.actualheight + 8) + "px";
                    this.cross_marquee2.style.top = parseInt(this.cross_marquee2.style.top) - this.copyspeed + "px";
                    this.cross_marquee.style.top = parseInt(this.cross_marquee.style.top) - this.copyspeed + "px";
                }

                else if (document.layers) {
                    if (this.ns_marquee.top > (this.actualheight * (-1) + 8))
                        this.ns_marquee.top -= this.copyspeed;
                    else
                        this.ns_marquee.top = parseInt(this.marqueeheight) + 8;
                }
            },

            CreateDivElement: function (width, height, xPos, yPos, parentOfUserControl, userControlClientID) {
                var marqueewidth = width + "px";
                var marqueeheight = height + "px";
                var x = xPos + "px";
                var y = yPos + "px";
                
                var holder = document.createElement('div');
                holder.setAttribute('id', 'holder' + userControlClientID);
                holder.style.cssText = 'position:relative;width:' + /*"position:absolute;left:"+ x +";top:"+ y +";width:"+*/marqueewidth + ';height:' + marqueeheight + ';overflow:hidden;background:#ffffff;';

                var marqueeDiv = document.createElement('div');
                marqueeDiv.setAttribute('id', 'iemarquee' + userControlClientID); //tao id cho marquee bang cach cong them id cua usercontrol va iemarquee
                marqueeDiv.style.cssText = "position:absolute;left:" + x + ";top:" + y + ";width:100%;float:left;";
           
                var marquee2Div = document.createElement('div');
                marquee2Div.setAttribute('id', 'iemarquee2' + userControlClientID);
                marquee2Div.style.cssText = "position:absolute;left:" + x + ";" + "top:" + y + ";width:100%;z-index:100;background:#ffffff;float:left";
               

                //   this.RegisterMouseEvent(marqueeDiv,marquee2Div);
                RegisterMouseEvent(marqueeDiv, marquee2Div, this);

                holder.appendChild(marqueeDiv);
                holder.appendChild(marquee2Div);
                //  document.body.appendChild(holder);   
                //document.getElementById('ScrollArea').appendChild(holder);
                if (parentOfUserControl != null)
                    parentOfUserControl.appendChild(holder);
            },

            //Keep all content on ONE line, and backslash any single quotations (ie: that\'s great):
            LoadMarquee: function (userControlClientID) {

                // var marqueecontent='1<br/>2<br/>3<br/>4<br/>5<br/>6<br/>7<br/>8<br/>9<br/>0<br/>';
                //Specify the marquee's width (in pixels)
                //var marqueewidth = 208;
                var marqueewidth = '100%';
                //Specify the marquee's height
                var marqueeheight = 190;

                var xPos = 0;
                var yPos = 0;

                var parentOfUserControl = document.getElementById(this.parentOfScroll);
                if (parentOfUserControl != null) {
                    this.CreateDivElement(marqueewidth, marqueeheight, xPos, yPos, parentOfUserControl, userControlClientID);
                    this.populate(marqueewidth, marqueeheight, userControlClientID);
                }
            },

            //dang ky su kien mouse over va mouse out cho cac marque
            RegisterMouseEvent: function (marqueeDiv, marquee2Div) {
                marqueeDiv.onmouseover = function () {
                    this.copyspeed = this.pausespeed;
                }
                marqueeDiv.onmouseout = function () {
                    this.copyspeed = this.marqueespeed;
                }

                marquee2Div.onmouseover = function () {
                    this.copyspeed = this.pausespeed;
                }
                marquee2Div.onmouseout = function () {
                    this.copyspeed = this.marqueespeed;
                }
            }
        }   
       
       function RegisterMouseEvent(marqueeDiv,marquee2Div,scr)
                {
                    marqueeDiv.onmouseover=function()
                                            { 
                                                scr.copyspeed=scr.pausespeed;
                                            }
                    marqueeDiv.onmouseout =function()
                                            { 
                                                scr.copyspeed=scr.marqueespeed;
                                             }
                    
                    marquee2Div.onmouseover=function()
                                            { 
                                                scr.copyspeed=scr.pausespeed;
                                            }
                    marquee2Div.onmouseout =function()
                                            { 
                                                scr.copyspeed=scr.marqueespeed;
                                            }
                } 
       // var marqueecontent = "<a href='http://google.com'>1</a><br/><a href='http://vnexpress.net'>2</a><br/>3<br/>4<br/>5<br/>6<br/>7<br/>8<br/>9<br/>0<br/>";
       try
       { Utility.ScrollText.registerClass("Utility.ScrollText");
       }
       catch(e)
       {
       }
       // var scr = new Utility.ScrollText();
       // scr.LoadMarquee(); 