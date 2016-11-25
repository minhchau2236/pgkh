<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Display2.ascx.cs" Inherits="PSCPortal.Portlets.PanelBar.Display2" %>
<link rel="stylesheet" type="text/css" href="/Portlets/PanelBar/styles.css" media="all" />
<script type="text/javascript">
    $(function () {
        // building select menu
        $('<select />').appendTo('nav');

        // building an option for select menu
        $('<option />', {
            'selected': 'selected',
            'value': '',
            'text': 'Choise Page...'
        }).appendTo('nav select');

        $('nav ul li a').each(function () {
            var target = $(this);

            $('<option />', {
                'value': target.attr('href'),
                'text': target.text()
            }).appendTo('nav select');

        });

        // on clicking on link
        $('nav select').on('change', function () {
            window.location = $(this).find('option:selected').val();
        });
    });

    // show and hide sub menu
    $(function () {
        $('nav ul').hover(
  function () {
      //show its submenu
      var countLi = $('ul', this).children().length;
      if (countLi > 0) {
          if (countLi > 15 && countLi <= 30)
              $('ul', this).css({ 'width': '400px' });
          if (countLi > 30 && countLi <= 45)
              $('ul', this).css({ 'width': '600px' });
          if ($('ul', this).is(":hidden")) {
              $('ul', this).slideDown(300);
          }
      }
  },
  function () {
      //hide its submenu
      $('ul', this).stop(true, false).slideDown();
      $('ul', this).hide();
  }
 );
    });
    //end
</script>
<div id="fdw" class="fdw">
    <nav>						
                        
                         <asp:Repeater ID="rptMennu" runat="server" onitemdatabound="Repeater1_ItemDataBound">
            <ItemTemplate>
            <ul>
            <li>
                <a target="_blank" href="<%# Eval("NavigationURL").ToString().Replace("~","") %>"><%#Eval("Name") %></a>
                <ul style="display: none; width:200px; margin-left:140px;" class="sub_menu">
            
                <asp:Repeater ID="Repeater2" runat="server" >
<ItemTemplate>

            <li>
            <a target="_blank" href="<%# Eval("NavigationURL").ToString().Replace("~/","")%>"><%#Eval("Name") %></a>
            </li>
           
</ItemTemplate> 
</asp:Repeater> 
 </ul>
            </li>
             </ul>
             </ItemTemplate>
        </asp:Repeater>

       
					</nav>
</div>
