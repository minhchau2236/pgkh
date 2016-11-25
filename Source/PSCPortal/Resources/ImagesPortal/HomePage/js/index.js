/*Gán chiều cao của item menu con cho menu cha
---------------------------------------------------------------------*/
function SetMenuHeightDefault(){
	 var secondMenuHeight=pscjq(".expanded").height();
	 pscjq("#Item-Menu-1").height(secondMenuHeight);
	 pscjq(".mega-dropdown-menu1").height(secondMenuHeight);
}
/* chọn item menu mặc định
----------------------------------------------------------------------*/
function SelectSubMenuDefault(){
    var borderActive='5px solid #870131';
	pscjq('#Item-Menu-1 > li').find('> a').first().addClass('active');
	pscjq('#selected').addClass('expanded').css("display","block");
	pscjq('#Item-Menu-1 > li').first().css('border-right',borderActive);
}
function RemoveSelectSubMenuDefault(){
    var borderDefault='5px solid #CA8397';	
	pscjq('#Item-Menu-1 > li').find('> a').first().removeClass('active');
	pscjq('#selected').css("display","none");
	pscjq('#Item-Menu-1 > li').first().css('border-right',borderDefault);
}

 /*---------------------------------------------------------------------------
 Cấu hình menu con (Cơ cấu tổ chức)
 ----------------------------------------------------------------------------*/
 function ConfigItemMenuChild() {	
    // border Active 
	var borderActive='5px solid #870131',
		borderDefault='5px solid #CA8397';	
    //Chọn submenu mặc định
    SelectSubMenuDefault();   
	
    //mất hover trên menu thì trả về chọn menu như pageload	
    pscjq('#Item-Menu-1').hover(
         
	   function () {},		
	   function () {
	      
			SelectSubMenuDefault();
			pscjq('#Item-Menu-1 >li:nth-child(1)').css('border-right',borderActive); 	
			SetMenuHeightDefault();
		}
	);	
	
	//hover trên menu con: set lại chiều cao cho "#Item-Menu-1", ".mega-dropdown-menu1"
	pscjq('#Item-Menu-1 > li ').on('mouseover',function(e){	
		//xóa bỏ chọn menu mặc định
		RemoveSelectSubMenuDefault();		
		
		pscjq(this).find('> a').addClass('active');		
		pscjq(this).find('> a').parent().css('border-right',borderActive);
		//chọn trên menu hover
		if(pscjq(this).has('ul').length) {
		    pscjq(this).find('ul').addClass('expanded'); 		  
			SetMenuHeightDefault();			  
		}
		else{
			pscjq(this).find('> a').addClass('active');			
		}
		
		pscjq('ul:first',this).parent().find('> a').addClass('active');
		pscjq('ul:first',this).show();				
		
	}).on('mouseout',function(e){	
		pscjq(this).find('> a').removeClass('active');	
        pscjq(this).find('> a').parent().css('border-right',borderDefault);		
		
		if(pscjq(this).has('ul').length) {
			pscjq(this).find('ul').removeClass('expanded');
			pscjq('ul:first',this).parent().find('> a').removeClass('active');
			pscjq('ul:first', this).hide();
		}
		else{
			pscjq(this).find('> a').removeClass('active');
		}
	}); 
 } 

 pscjq(document).ready(function () {		
	
	var screenWidth= pscjq(window).width();
    //Cấu hình menu con (Cơ cấu tổ chức)
	if (screenWidth > 768) {
	    ConfigItemMenuChild();
	}

     /*tìm kiếm mobile*/
	pscjq('#buttonsearch').click(function () {
	    pscjq('#formsearch').slideToggle("fast", function () {
	        pscjq('#content').toggleClass("moremargin");
	    });
	    pscjq('#searchbox').focus()
	    pscjq('.openclosesearch').toggle();
	});
	
 });
 
 // 19112015
 pscjq(window).bind('resize', function () {
     var screenWidth = pscjq(window).width();
     //Cấu hình menu con (Cơ cấu tổ chức)
     if (screenWidth > 768) {
         ConfigItemMenuChild();
     }
   
 });

		
