<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Map.ascx.cs" Inherits="PSCPortal.Modules.Map.Map" %>
   <script src="/Modules/Map/Scripts/shiftzoom.js" type="text/javascript"></script>
  
  	<style type="text/css">
		
		#crosshair_x { 
			display: block; 
			position: absolute;
			top: 150px;
			left: 0px;
			width: 600px; 
			height: 1px;
			padding: 0px; 
			margin: 0px;
			line-height: 0px;
			border: none;
			background: white;
			visibility: hidden;
			opacity: 0.5;
			z-index: 99999;
		}
		#crosshair_y { 
			display: block; 
			position: absolute;
			top: 0px;
			left: 300px;
			width: 1px; 
			height: 300px;
			padding: 0px; 
			margin: 0px;
			line-height: 0px;
			border: none;
			background: white;
			visibility: hidden;
			opacity: 0.5;
			z-index: 99999;
		}

	</style>
	<script type="text/javascript">
<!--
if(typeof $=='undefined') {function $(v) {return(document.getElementById(v));}}
if(document.images&&document.createElement&&document.getElementById){ 
    document.writeln('<style type="text/css">'); 
    document.writeln('img.shiftzoom { visibility: hidden; }');
    document.writeln('<\/style>'); 
} 
function nocrosshair(v) {
	$('crosshair_x').style.visibility='hidden';
	$('crosshair_y').style.visibility='hidden';
	var p=getGeoPosition('world',v.toUpperCase(),2700,600,1350,300); last_icon=v;
	shiftzoom.destruct($('world'),geodata['world'][v].lc);
	shiftzoom.construct($('world'),[{x:p.l, y:p.t, w:53, h:64, id:geodata['world'][v].lc, pos:7, title:geodata['world'][v].sn+' ('+geodata['world'][v].cn+')', href:'http://maps.google.com/?q='+geodata['world'][v].sn, target:'Google_Maps', src:'images/icons/pin_out.png', src2:'images/icons/pin_over.png'}]);
}
function clearicons() {last_icon=""; shiftzoom.destruct($('world'),true);}
function get_Country(v) {
	$('crosshair_x').style.visibility='visible';
	$('crosshair_y').style.visibility='visible';
	if(last_icon!='') {
		shiftzoom.destruct($('world'),geodata['world'][last_icon].lc);
		var q=getGeoPosition('world',last_icon.toUpperCase(),2700,600,1350,300);
		shiftzoom.construct($('world'),[{x:q.l, y:q.t, w:28, h:48, id:geodata['world'][last_icon].lc, pos:7, title:geodata['world'][last_icon].sn+' ('+geodata['world'][last_icon].cn+')', src:'images/icons/pin_flag.png'}]);
	}
	var p=getGeoPosition('world',v.toUpperCase(),2700,600,1350,300);
	shiftzoom.kenburns($('world'),[p.x,p.y,p.z,3,false,false,'nocrosshair',v]);
}
function getGeoPosition(map,lc,xw,iw,xh,ih) {
	function lat2y(lat,h) {return ((lat*-1)+90)*(h/180);};
	function lng2x(lng,w) {return (lng+180)*(w/360);};
	var x,y,z,f,l,t,k,x1,y1,x2,y2,f=(iw/xw)*100,s=100,d=geodata[map][lc];
	x1=lng2x(d.bw,s); y1=lat2y(d.bn,s); x2=lng2x(d.be,s); y2=lat2y(d.bs,s);	
	x=((x2-x1)/2)+x1; y=((y2-y1)/2)+y1; k=Math.max(x2-x1,y2-y1); 
	z=s-(k<f?0:k); l=parseFloat((x/100)*xw); t=parseFloat((y/100)*xh);
	f=x/100; x1=f*((xw-iw)*(z/100)); x2=(0.5-f)*iw; x=(((x1-x2))/((xw-iw)*(z/100)))*100;
	f=y/100; y1=f*((xh-ih)*(z/100)); y2=(0.5-f)*ih; y=(((y1-y2))/((xh-ih)*(z/100)))*100;
	return {x:x,y:y,z:z,l:l,t:t};
}
var last_icon="";
//-->
</script>
<table alig="center" style=" border:solid 1px black">
<tr><td>
<div style="width:560px; height:720px; background: url(/Modules/Map/images/map.jpg) 50% 50% no-repeat; -webkit-box-shadow: 0px 0px 8px black; -moz-box-shadow: 0px 0px 8px black;">
<img id="world" class="shiftzoom" onload="shiftzoom.add(this,{fading:true,showcoords:false,pixelcoords:false,lowres:'/Modules/Map/images/map.jpg'});" src="/Modules/Map/images/map.jpg" width="560px" height="720px" alt="large image" border="0" />
<div id="crosshair_x"></div><div id="crosshair_y"></div></div>
</td></tr>
</table>