$(function () {
    $("#menu #liNghienCuu").hover(function () {
        $(".menu-container-2").show();
        $(".menu-container-2").css("left", $("#liDaoTao").offset().left -($("#liTrangChu").width() * 120 / 100));
    },
    function () {
        $(".menu-container-2").hide();
    });


    $("#menu #liNghienCuu_res").hover(function () {
        $(".menu-container-2_res").show();
        $(".menu-container-2_res").css("left", $("#liDaoTao_EN").offset().left);
    },
    function () {
        $(".menu-container-2_res").hide();
    });



    $("#menu #liGioiThieu").hover(function () {
        $(".menu-container-4").show();
   
        var positionLeft = $("#liTrangChu").offset().left - ($("#liTrangChu").width() * 120 / 100);
        $(".menu-container-4").css("left", positionLeft);
    },
    function () {
        $(".menu-container-4").hide();
    });

    $("#menu #liGioiThieu").hover(function () {
        $(".menu-container-4_EN").show();
        var positionLeft = $("#liTrangChu").offset().left + ($("#liTrangChu").width() * 42 / 100);
        $(".menu-container-4_EN").css("left", positionLeft);
    },
    function () {
        $(".menu-container-4_EN").hide();
    });

    $("#menu #liDaoTao").hover(function () {
        $(".menu-container-3").show();
        var positionLeft = $("#liGioiThieu").offset().left - ($("#liGioiThieu").width() * 10 / 100);
        $(".menu-container-3").css("left", positionLeft);
    },
    function () {
        $(".menu-container-3").hide();
    });

    $("#menu #liDaoTao_EN").hover(function () {
        $(".menu-container-3_EN").show();
        var positionLeft = ($("#liTrangChu").offset().left + $("#liGioiThieu").offset().left) / 2;
        $(".menu-container-3_EN").css("left", positionLeft);
    },
    function () {
        $(".menu-container-3_EN").hide();
    });

    $("#menu #liMoiTruongHoc").hover(function () {
        $(".menu-container-1").show();
        var positionLeft = $("#liTrangChu").offset().left - ($("#liTrangChu").width()-133);
        $(".menu-container-1").css("left", positionLeft);
    },
    function () {
        $(".menu-container-1").hide();
    });


    $("#menu #liMoiTruongHoc").hover(function () {
        $(".menu-container-1_EN").show();
        var positionLeft = ($("#liTrangChu").offset().left - $("#liGioiThieu").offset().left) / 2;
        $(".menu-container-1_EN").css("left", positionLeft);
    },
    function () {
        $(".menu-container-1_EN").hide();
    });


    $("#menu #liTuyenSinh").hover(function () {
        $(".menu-container-0").show();
        var positionLeft = $("#liTuyenSinh").offset().left + ($("#liTuyenSinh").width() * 96 / 100) - $(".menu-container-0").width();
        //var positionLeft = $("#pnCenter").offset().left + $("#pnCenter").width() - $(".menu-container-0").width()-5;
        $(".menu-container-0").css("left", positionLeft);
    },
    function () {
        $(".menu-container-0").hide();
    });

    $("#menu #liTuyenSinh").hover(function () {
        $(".menu-container-0_EN").show();
        var positionLeft = $("#liTuyenSinh").offset().left + ($("#liTuyenSinh").width() * 107 / 100) - $(".menu-container-0_EN").width();
        //var positionLeft = $("#pnCenter").offset().left + $("#pnCenter").width() - $(".menu-container-0").width()-5;
        $(".menu-container-0_EN").css("left", positionLeft);
    },
    function () {
        $(".menu-container-0_EN").hide();
    });
});