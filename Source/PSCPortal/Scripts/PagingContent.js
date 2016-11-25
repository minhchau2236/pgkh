var currentPage = 1;
var sizeOfBlock = 10;
var recordInPage = 20;
var totalRecords = 0;
var IsFirstTimeSearch = true;
var BindingModel = null;


function BindingKnockout() {
    BindingModel = {
        DataModel: ko.observable({
            Count: 0,
            Data: []
        })
    }
    var results = "<div class='searchResult' data-bind=\"template: { name: 'search-binding-template', data: DataModel }\"></div>";
    if (pscjq(window).width() <= 966) {
        if (document.getElementById("content_M"))
            document.getElementById("content_M").innerHTML = results;
        else
            document.getElementById("psc-divPanelMid").innerHTML = results;
    }
    else {
        document.getElementById("psc-divPanelMid").innerHTML = results;
    }
    ko.applyBindings(BindingModel, document.getElementById("psc-divPanelMid"));
}

/////////////////////////////////////////
function LoadContentAndPaging(results) {
    pscjq(".bg_banner").hide();
    if (IsFirstTimeSearch) {
        BindingKnockout();
        IsFirstTimeSearch = false;
    }
    BindingModel.DataModel(pscjq.parseJSON(results));
    totalRecords = BindingModel.DataModel().Count;

    if (totalRecords > 20) {
        //paging
        var pagingHolder = document.getElementById("pagingContentHolder");
        pagingHolder.innerHTML = "";
        //tinh current block page

        var totalPages = Math.ceil(parseInt(totalRecords) / recordInPage);

        //tong so block co the co,cong thuc la lay tong so record chi cho tich cua kich thuoc block va so record tren trang
        //////////////////////////////////////////////


        if (currentPage > 1) {
            var firstPage = document.createElement("a");
            firstPage.setAttribute("href", "javascript:Paging(" + 1 + ")");
            firstPage.appendChild(document.createTextNode("Trang Đầu"));

            firstPage.style.cssText = "text-decoration: none; font-family: Tahoma; font-size: 8.5pt; font-weight: bold";

            pagingHolder.appendChild(firstPage);
            pagingHolder.appendChild(document.createTextNode(" "));

            var prev = document.createElement("a");
            var iPos = parseInt(currentPage) - 1;
            prev.setAttribute("href", "javascript:Paging(" + iPos + ")");
            prev.appendChild(document.createTextNode("Trang Trước"));
            prev.style.cssText = "text-decoration: none; font-family: Tahoma; font-size: 8.5pt; font-weight: bold";

            pagingHolder.appendChild(prev);
            pagingHolder.appendChild(document.createTextNode(" "));

            var spanText = document.createElement('span');
            spanText.style.cssText = "font-family: Tahoma; font-size: 8.5pt; font-weight: bold";
            spanText.appendChild(document.createTextNode(" ... "));
            pagingHolder.appendChild(spanText);
        }

        /////////////////////////////////////////////   

        for (var j = currentPage; j > 0 && j < currentPage + 5 && j <= totalPages ; j++) {
            var aPaging = document.createElement("a");
            aPaging.setAttribute("href", "javascript:Paging(" + j + ")");
            if (j != currentPage)
                aPaging.style.cssText = "text-decoration: none; font-family: Tahoma; font-size: 8.5pt; font-weight: bold";
            else
                aPaging.style.cssText = "text-decoration: underline; font-family: Tahoma; font-size: 8.5pt; font-weight: bold;color:#FF0000";
            aPaging.appendChild(document.createTextNode(j));
            pagingHolder.appendChild(aPaging);
            pagingHolder.appendChild(document.createTextNode(" "));
        }

        ////////////////////////////////////////////////
        var next = document.createElement("a");
        if (currentPage < totalPages) {
            var spanText = document.createElement('span');
            spanText.style.cssText = "font-family: Tahoma; font-size: 8.5pt; font-weight: bold";
            spanText.appendChild(document.createTextNode(" ... "));
            pagingHolder.appendChild(spanText);

            var iPos = parseInt(currentPage) + 1;
            next.setAttribute("href", "javascript:Paging(" + iPos + ")");
            next.appendChild(document.createTextNode("Trang Sau"));
            next.style.cssText = "text-decoration: none; font-family: Tahoma; font-size: 8.5pt; font-weight: bold";

            pagingHolder.appendChild(next);
            pagingHolder.appendChild(document.createTextNode(" "));

            var lastPage = document.createElement("a");
            lastPage.setAttribute("href", "javascript:Paging(" + totalPages + ")");
            lastPage.appendChild(document.createTextNode("Trang Cuối"));
            lastPage.style.cssText = "text-decoration: none; font-family: Tahoma; font-size: 8.5pt; font-weight: bold";

            pagingHolder.appendChild(lastPage);
            pagingHolder.appendChild(document.createTextNode(" "));
        }
        ////////////////////////////////////////////////
    }
}

function Paging(pageIndex) {
    currentPage = pageIndex;
    var query = document.getElementById('txtSearchArticle').value == "" ? document.getElementById('txtSearchArticle_M').value : document.getElementById('txtSearchArticle').value;
    if (query == "")
        return;
    OnSearch(query);
}

