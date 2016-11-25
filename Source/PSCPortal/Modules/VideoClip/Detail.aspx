<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="PSCPortal.Modules.VideoClip.Detail" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
	.videobody
	{
		background-color:#07396e;
	}
        ul.videoclip
        {
            font: normal 11px Arial, Helvetica, sans-serif;
            text-align: justify;
            clear: both;
            margin: 0 auto;
            padding: 5px 0px 5px 0px;
            margin-bottom: 5px;
            line-height: 18px;
        }
        ul.videoclip li
        {
            margin: 0 auto;
            padding: 0;
            list-style-image:url(/Resources/ImagesPortal/arrow_top.png);
            margin: 0px 15px 0px 25px;
            padding: 3px 0px 3px 0px;
            
        }
        
        ul.videoclip li:last-child
        {
            border: none;
        }
        
        ul.videoclip li a
        {
            text-decoration: none;
		color:#fff;
        }
        ul.videoclip li a:hover
        {
		text-decoration:underline;
		
        }
    </style>
    <script src="/Scripts/jquery-1.4.3.js" type="text/javascript"></script>
</head>
<body class="videobody">
    <form id="form1" runat="server">
    <div style="clear: both; float: left; width: 100%; text-align:center;">
        <div id="empty" style="display:none;">
            Chưa có video clip nào
        </div>
        <div  id="pnVideo" style="clear: both; float: left">
           <embed name="playlist" width="400" height="400" src="/mediaplayer.swf" type="application/x-shockwave-flash" quality="high" allowfullscreen="true" wmode="transparent" flashvars="file=<%#PathFirst%>&amp;autostart=true"></embed>
        </div>
        <div style="clear: both; float: left">
            <ul class="videoclip">
                <asp:Repeater ID="rptVideoClip" runat="server">
                    <ItemTemplate>
                        <li><a clip='<%#Eval("Path")%>' style="cursor: pointer" onclick="ShowVideo($(this).attr('clip'));">
                            <%#Eval("Name")%>
                        </a></li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
    </div>
    </form>
</body>
<script type="text/javascript" language="javascript">
    function ShowVideo(textScript) {
        var strEmbed = "<embed name='playlist' width='400' height='400' src='/mediaplayer.swf' type='application/x-shockwave-flash' quality='high' allowfullscreen='true' wmode='transparent' flashvars='file=" + textScript + "&amp;autostart=true'></embed>";
        document.getElementById("pnVideo").innerHTML = strEmbed;
    }
</script>
</html>
