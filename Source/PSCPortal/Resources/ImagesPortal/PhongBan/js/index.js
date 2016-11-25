 function closeTheOthers(x) {
	if (x != "CoCau") {
		$("#dropdown_CoCau_Btn").css("background-color", "");
		$("#dropdown_CoCau_Btn").css("color", "");
		$("#dropdown_CoCau").fadeOut(100);
	}  
	if (x != "TienIch") {
		$("#dropdown_TienIch_Btn").css("background-color", "");
		$("#dropdown_TienIch_Btn").css("color", "");
		$("#dropdown_TienIch").fadeOut(100);
	} 	
	if (x != "search") { 
		$("#dropdownSearchBtn").css("background-color","");
		$("#dropdownSearchBtn").css("color","");
		$("#w3dropdownsearch").fadeOut(100);
	}
}
function hideDropdownMenu() {
	closeTheOthers();
}  

/*Gán chiều cao của item menu con cho menu cha
---------------------------------------------------------------------*/
function SetMenuHeightDefault(){
	 var secondMenuHeight=$(".expanded").height();
	 $("#Item-Menu-1").height(secondMenuHeight);
	 $("#coCauToChucDropdown").height(secondMenuHeight);
}
/* chọn item menu mặc định
----------------------------------------------------------------------*/
function SelectSubMenuDefault()
{
    var borderActive='5px solid #3488e7';
	$('#Item-Menu-1 > li').find('> a').first().addClass('active');
	$('#selected').addClass('expanded').css("display","block");
	$('#Item-Menu-1 > li').first().css('border-right',borderActive);
}
function RemoveSelectSubMenuDefault(){
    var borderDefault='5px solid #acd0fa';	
	$('#Item-Menu-1 > li').find('> a').first().removeClass('active');
	$('#selected').css("display","none");
	$('#Item-Menu-1 > li').first().css('border-right',borderDefault);
}

/*---------------------------------------------------------------------
//Click menu cha => Ẩn/Hiện Item menu con
----------------------------------------------------------------------*/
 function ClickItemMenuRoot(){
	/*Click menu cơ cấu
	------------------------------------------------------------------*/
	$("#dropdown_CoCau_Btn").click(function (){	 
		closeTheOthers("CoCau");
		if ($("#dropdown_CoCau").css("display") == "none") {
			$("#dropdown_CoCau_Btn").css("background-color", "#f5f5f5");
			$("#dropdown_CoCau_Btn").css("color", "#555555");
		} else {
			$("#dropdown_CoCau_Btn").css("background-color", "");
			$("#dropdown_CoCau_Btn").css("color", "");
		}
		$("#dropdown_CoCau").fadeToggle(200, function () { $("#dropdown_CoCau_Btn").css("background-color", ""); $("#dropdown_CoCau_Btn").css("color", ""); });
		
		//Gán chiều cao submenu cho menu		
		SetMenuHeightDefault();

		return false;
	});  
	
	//show/hide drop menu
	/*
	 $("#dropdown_CoCau_Btn").mouseenter(function (){				
			//$("#dropdown_CoCau").show();
			$("#dropdown_CoCau").fadeToggle(200, function () { $("#dropdown_CoCau_Btn").css("background-color", ""); $("#dropdown_CoCau_Btn").css("color", ""); });
		}
	); 
	 $("#dropdown_CoCau").mouseleave(function(){							 
		$("#dropdown_CoCau").hide(); 
	  });
	*/		
	/*Click Tiện ích menu
	---------------------------------------------------------------------*/
	 $("#dropdown_TienIch_Btn").click(function (){				
		closeTheOthers("TienIch");
		if ($("#dropdown_TienIch").css("display") == "none") {
			$("#dropdown_TienIch_Btn").css("background-color", "#f5f5f5");
			$("#dropdown_TienIch_Btn").css("color", "#555555");
		} else {
			$("#dropdown_TienIch_Btn").css("background-color", "");
			$("#dropdown_TienIch_Btn").css("color", "");
		}
		$("#dropdown_TienIch").fadeToggle(200, function () { $("#dropdown_TienIch_Btn").css("background-color", ""); $("#dropdown_TienIch_Btn").css("color", ""); });
		return false;
	});
	/*click Tìm kiếm
	--------------------------------------------------------------------*/
	$("#dropdownSearchBtn").click(function(){
		closeTheOthers("search");
		if ($("#w3dropdownsearch").css("display") == "none") {
		  $("#dropdownSearchBtn").css("background-color","#f5f5f5");
		  $("#dropdownSearchBtn").css("color","#555555");
		} else {
		  $("#dropdownSearchBtn").css("background-color","");
		  $("#dropdownSearchBtn").css("color","");
		}
		$("#w3dropdownsearch").fadeToggle(200, function(){$("#gsc-i-id1").focus();$("#dropdownSearchBtn").css("background-color","");});
		return false;      
	});													
	$(".main").click(function () {
		closeTheOthers();
	});
 }
 
 /*---------------------------------------------------------------------------
 Cấu hình menu con (Cơ cấu tổ chức)
 ----------------------------------------------------------------------------*/
 function ConfigItemMenuChild() {
    // border Active 
	var borderActive='5px solid #3488e7',
		borderDefault='5px solid #acd0fa';	
    //Chọn submenu mặc định
    SelectSubMenuDefault();   
	
    //mất hover trên menu thì trả về chọn menu như pageload	
	$('#Item-Menu').hover(			
	   function () {},		
	   function () { 
			SelectSubMenuDefault();
			$('#Item-Menu-1 >li:nth-child(1)').css('border-right',borderActive); 	
			SetMenuHeightDefault();
		}
	);	
	
	//hover trên menu con: set lại chiều cao cho "#Item-Menu-1", "#coCauToChucDropdown"
	$('#Item-Menu-1 > li ').on('mouseover',function(e){	
		//xóa bỏ chọn menu mặc định
		RemoveSelectSubMenuDefault();		
		
		$(this).find('> a').addClass('active');		
		$(this).find('> a').parent().css('border-right',borderActive);
		//chọn trên menu hover
		if($(this).has('ul').length) {
		    $(this).find('ul').addClass('expanded'); 		  
			SetMenuHeightDefault();			  
		}
		else{
			$(this).find('> a').addClass('active');			
		}
		
		$('ul:first',this).parent().find('> a').addClass('active');
		$('ul:first',this).show();				
		
	}).on('mouseout',function(e){	
		$(this).find('> a').removeClass('active');	
        $(this).find('> a').parent().css('border-right',borderDefault);		
		
		if($(this).has('ul').length) {
			$(this).find('ul').removeClass('expanded');
			$('ul:first',this).parent().find('> a').removeClass('active');
			$('ul:first', this).hide();
		}
		else{
			$(this).find('> a').removeClass('active');
		}
	}); 
 } 
 
 $(document).ready(function () {
	 //Ẩn/Hiện Item menu con
	 ClickItemMenuRoot();
    //Cấu hình menu con (Cơ cấu tổ chức)
	ConfigItemMenuChild();	

});
		
