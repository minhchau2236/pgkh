Pager = function(pagerHolder,currentPage,sizeOfBlock,recordInPage,datasource,methodName,totalRecord)
{
    this.pagerHolder = pagerHolder;
    this.currentPage = currentPage;
    this.sizeOfBlock = sizeOfBlock;
    this.recordInPage= recordInPage;
    this.datasource = datasource;
    this.CallBackMethod = eval(methodName);
}

Pager.prototype =
{
    Paging : function (pageIndex) {
        this.currentPage  = pageIndex;
         var pagegingHolder = document.getElementById(this.pagerHolder);
         pagegingHolder.innerHTML = "";
         
         var length =0;
         if(this.datasource != null)
         {
            length = this.datasource.length;
         }
         else
         {
            length = this.totalRecord;
         }
        if (length >= this.recordInPage) {
            //paging
            //tinh current block page

            var blockIndex = Math.floor(this.currentPage / this.sizeOfBlock);
            if (this.currentPage % this.sizeOfBlock == 0) {
                blockIndex -= 1;
            }
            var totalPages = Math.ceil(length / this.recordInPage);
            var startIndexOfBlock = blockIndex * this.sizeOfBlock + 1;

            //tong so block co the co,cong thuc la lay tong so record chi cho tich cua kich thuoc block va so record tren trang
            var totalBlock = Math.ceil(length / (sizeOfBlock * recordInPage));
            
            //////////////////////////////////////////////
            //tao control de di den trang dau tien
            var first = document.createElement("a");
            var instance = this;

            first.setAttribute("href", "javascript:function a2DA19a_123543659478642_r64873568433476tdsgfhdsfe6s46378264732678_a2DA19a_123543659478642_r64873568433476tdsgfhdsfe6s46378264732678a2DA19a_123543659478642_r64873568433476tdsgfhdsfe6s46378264732678(){}");
             //   prev.setAttribute("href", "javascript:instance.PreviousBlock(" + blockIndex + ")");
                var instance = this;
            Pager.prototype.RegisterPreviousBlockOnclick(first,instance,1);

            first.appendChild(document.createTextNode("<<<"));
            pagegingHolder.appendChild(first);
            pagegingHolder.appendChild(document.createTextNode(" "));
            /////////////////////////////////////////////
            
            //////////////////////////////////////////////
            var prev = document.createElement("a");
            var instance = this;
            if (blockIndex > 0)
            {
                prev.setAttribute("href", "javascript:function a2DA19a_123543659478642_r64873568433476tdsgfhdsfe6s46378264732678_a2DA19a_123543659478642_r64873568433476tdsgfhdsfe6s46378264732678a2DA19a_123543659478642_r64873568433476tdsgfhdsfe6s46378264732678(){}");
             //   prev.setAttribute("href", "javascript:instance.PreviousBlock(" + blockIndex + ")");
                var instance = this;
                Pager.prototype.RegisterPreviousBlockOnclick(prev,instance,blockIndex);
            }
            prev.appendChild(document.createTextNode("<<"));
            pagegingHolder.appendChild(prev);
            pagegingHolder.appendChild(document.createTextNode(" "));
            /////////////////////////////////////////////


            for (var j = startIndexOfBlock; j > 0 && j < startIndexOfBlock + this.sizeOfBlock && j <= totalPages; j++) {
                var aPaging = document.createElement("a");
               // aPaging.setAttribute("href", "javascript:instance.Paging(" + j + ")");
                aPaging.setAttribute("href", "javascript:function a2DA19a_123543659478642_r64873568433476tdsgfhdsfe6s46378264732678_a2DA19a_123543659478642_r64873568433476tdsgfhdsfe6s46378264732678a2DA19a_123543659478642_r64873568433476tdsgfhdsfe6s46378264732678(){}");
                var instance = this;
                Pager.prototype.RegisterPagingOnclick(aPaging,instance,j);
                
                if (j != this.currentPage)
                    aPaging.style.cssText = "text-decoration: none; font-family: Tahoma; font-size: 8.5pt; font-weight: bold";
                else
                    aPaging.style.cssText = "text-decoration: underline; font-family: Tahoma; font-size: 8.5pt; font-weight: bold;color:#FF0000";
                aPaging.appendChild(document.createTextNode(j));
                pagegingHolder.appendChild(aPaging);
                pagegingHolder.appendChild(document.createTextNode(" "));
            }

            ////////////////////////////////////////////////
            var next = document.createElement("a");
            if (blockIndex < totalBlock - 1) {
                next.setAttribute("href", "javascript:function a2DA19a_123543659478642_r64873568433476tdsgfhdsfe6s46378264732678_a2DA19a_123543659478642_r64873568433476tdsgfhdsfe6s46378264732678a2DA19a_123543659478642_r64873568433476tdsgfhdsfe6s46378264732678(){}");
                var instance = this;
                Pager.prototype.RegisterNextBlockOnclick(next,instance,blockIndex);
              //  next.setAttribute("href", "javascript:instance.NextBlock(" + blockIndex + ")");
            }
            next.appendChild(document.createTextNode(">>"));
            pagegingHolder.appendChild(next);
            pagegingHolder.appendChild(document.createTextNode(" "));
            ////////////////////////////////////////////////
            
                        //////////////////////////////////////////////
            //tao control de di den trang cuoi
            var last = document.createElement("a");
            var instance = this;

            last.setAttribute("href", "javascript:function a2DA19a_123543659478642_r64873568433476tdsgfhdsfe6s46378264732678_a2DA19a_123543659478642_r64873568433476tdsgfhdsfe6s46378264732678a2DA19a_123543659478642_r64873568433476tdsgfhdsfe6s46378264732678(){}");
             //   prev.setAttribute("href", "javascript:instance.PreviousBlock(" + blockIndex + ")");
                var instance = this;
            Pager.prototype.RegisterPreviousBlockOnclick(last,instance,totalBlock);

            last.appendChild(document.createTextNode(">>>"));
            pagegingHolder.appendChild(last);
            pagegingHolder.appendChild(document.createTextNode(" "));
            /////////////////////////////////////////////
        }
          this.CallBackDisplayPagerContent();
    },
    
    CallBackDisplayPagerContent:function ()
    {
        var instance = this;
        instance.CallBackMethod(instance);
      //  var jonStr = Sys.Serialization.JavaScriptSerializer.serialize(instance);
       // eval(instance.CallBackMethod + "(" + jonStr+")");
    },
    
     NextBlock:function(currentBlockIndex) {
        
      //  alert(currentBlockIndex);
     //   var instance = this;
     //  document.write('<a href=javascript:instance.NextBlock(' + currentBlockIndex + ') >abc </a>'); 
       var nextBlockIndex = currentBlockIndex + 1;
       this.currentPage = nextBlockIndex * this.sizeOfBlock + 1;
       this.Paging(this.currentPage);
      // this.Paging(currentBlockIndex);
      //  GetStudentsByYearIdAndTimesIdAndSchoolIdAndStudentName(currentPage);
    },

    PreviousBlock:function (currentBlockIndex) {
        var nextBlockIndex = currentBlockIndex - 1;
        this.currentPage = nextBlockIndex * this.sizeOfBlock + 1;
        this.Paging(this.currentPage);
       // GetStudentsByYearIdAndTimesIdAndSchoolIdAndStudentName(currentPage);
    },
    
    GetStartOfRecord:function()
    {
        return this.currentPage * this.recordInPage - this.recordInPage;
    },
    
    GetEndOfRecord:function()
    {
        var endOfPosition = this.currentPage * this.recordInPage;
        if(this.datasource != null)
        {
            if(endOfPosition > this.datasource.length)
               endOfPosition = this.datasource.length;
        }
        else
        {
            if(endOfPosition > this.totalRecord)
                endOfPosition = this.totalRecord;
        }
        return endOfPosition
    },
    
    //Register Events
    RegisterPagingOnclick: function(element,objPaging,index)
    {
        element.onclick = function() { objPaging.Paging(index);/* Shadowbox.init();*/} 
    },
    
    RegisterNextBlockOnclick: function(element,objPaging,currentBlockIndex)
    {
        element.onclick = function() { objPaging.NextBlock(currentBlockIndex); } 
    },
    
    RegisterPreviousBlockOnclick: function(element,objPaging,currentBlockIndex)
    {
        element.onclick = function() { objPaging.PreviousBlock(currentBlockIndex); } 
    }
}
