$(function() {	
	// main menu toggler
	//$('.Menu .container').prepend('<div class="menu-icon"><span>Menu</span></div>');
	$('.Menu .container').prepend('<div class="menu-icon">'
										+'<div class="navbar-header">'
											+'<button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#example-navbar-collapse">'
											   +'  <span class="sr-only">Toggle navigation</span>'
											   +'  <span class="icon-bar"></span>'
											   +'  <span class="icon-bar"></span>'
											   +'  <span class="icon-bar"></span>'
										    +'</button>'      
										+'</div>'
								   +'</div>');
	$('.Menu .container').prepend('<div class="logo_menuchinh"></div>');
	$('.menu-icon').click(function(){						
		$('.Menu .container div:nth-child(3)').slideToggle(600);
		$(this).toggleClass("active");	
	});	
	
	// sub menu accordion-like toggler 
	/*
	var $menuToggler = $('.sub > a');
	$menuToggler.click(function(e) {
		e.preventDefault();
		var $this = $(this);
		$this.toggleClass('current').next('ul').toggleClass('current');
	});	
	*/
});

