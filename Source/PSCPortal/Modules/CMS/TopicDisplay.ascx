<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopicDisplay.ascx.cs"
    Inherits="PSCPortal.Modules.CMS.TopicDisplay" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<link href="/Modules/TopicArticleDisplay.css" rel="stylesheet" type="text/css" />
<link href="/Components/CarouselSlider/CSS/slick.css" rel="stylesheet" />
<link href="/Components/CarouselSlider/CSS/slick-theme.css" rel="stylesheet" />
<style type="text/css">
    .title_topicdisplay {
        text-align: center;
        width: 800px;
        margin-top: -9px;
        padding-right: 0px;
        margin-left: 20px;
        text-decoration: none;
        font-size: 14px;
        color: #676363;
    }

        .title_topicdisplay a {
            text-align: center;
            text-decoration: none;
            color: #676363;
            font-size: 17px;
            line-height: 25px;
        }

        .title_topicdisplay:hover {
            text-decoration: none;
            color: #90082f;
        }

    .h3_content {
        text-align: justify;
        font: 13px Arial, Helvetica, sans-serif;
        color: #333;
    }

    .RadDataPager .rdpNumPart a {
        padding: 0px 5px 3px 0px;
    }

    .Link_Unselected {
        font-family: Tahoma;
        font-size: 12px;
        color: #999;
        padding: 0px 5px 0px 5px;
        text-decoration: none;
        font-size: 23px;
    }

        .Link_Unselected:hover {
            color: #900;
            text-decoration: none;
        }


    .Link_Selected {
        font-family: Tahoma;
        font-size: 12px;
        color: #900;
        padding: 0px 5px 0px 5px;
        font-size: 23px;
    }

        .Link_Selected:hover {
            color: #900;
            text-decoration: none;
        }

    .gopy {
        margin-top: 10px;
        margin-right: 25px;
        margin-left: 20px;
        text-align: justify;
        float: left;
        margin-bottom: 10px;
    }

    /*tintuc*/
    .tintuc {
        float: left;
        padding-top: 20px;
    }

    
    .tieude a {
        float: left;
        font: 18px Arial, Helvetica, sans-serif;
        text-transform: uppercase;
        color: #1c78b4;
        font-weight: bold;
        padding-left: 20px;
        text-decoration: none;
    }

    .line_tt {
        float: left;
        padding: 5px 0px 5px 0px;
    }

    .line_tt1 {
        float: left;
        padding: 5px 0px 5px 0px;
    }

        .line_tt1 img {
            width: 244px;
            height: 4px;
        }

    .arrow {
        float: right;
        margin-right: 20px;
    }

    .banner-tintuc div {
        margin: 0px 5px !important;
    }




    .PageColumns {
        clear: both;
        padding-top: 20px;
        margin-bottom: 50px;
        text-align: center;
    }
    /*width <250*/
    .img-transform250 {
        -moz-transform: translate(-10%,5%);
        -ms-transform: translate(-10%,5%);
        -o-transform: translate(-10%,5%);
        -webkit-transform: translate(-10%,5%);
        transform: translate(-10%,5%);        
    }
    /*full*/
       .img-transformfull {
        -moz-transform: translate(-29%,5%);
        -ms-transform: translate(-29%,5%);
        -o-transform: translate(-29%,5%);
        -webkit-transform: translate(-29%,5%);
        transform: translate(-29%,5%);        
    }
       .slick-dots{display:none!important;}
       .outter-slick{
           padding:0px;
       }
       .topic-columns{
           list-style:none;
       }

       .topic-columns li{
           width:100%;
           height: 100px;
              border-bottom: 1px #333333 dashed;
                padding-bottom: 23px;
                margin-bottom: 20px;
                margin-right: 30px;
                float: left;
                
       }
        .topic-columns li a {
            color: #5f2b04;
            font-weight:bold;
        }

       .box-img {
            width: 100px;
            height: 100px;
            float: left;
       }        
</style>
<script src="/Components/CarouselSlider/Scripts/slick.js"></script>
<script>
    function pageLoad() {
        pscjq(".bg_banner").hide();
    }

   

</script>
 <div class="home">
        <div class="topic">
            <div class="L_topic">
                <img alt="" src="/Resources/ImagesPortal/HomePage/Imgs/arrow_yellow.jpg" />
                <a href="/?TopicId=<%#TopicId %>" style="text-transform: capitalize;"><%# TopicName%></a>
            </div>
            <!--L_topic-->
        </div>
    </div>
<!--end home-->

<div class="list-thong-bao">
    
    <ul class="container topic-columns">
        <telerik:RadListView ID="RadListView1" runat="server" AllowPaging="True">
            <ItemTemplate>
                <li>
                       <div class="box-img"><img class="images_Topic" width="90px" height="90px" style="cursor: hand; <%# PSCPortal.CMS.Article.CheckImage((Guid)Eval("Id")) == true? "margin-top: 0px": "margin-top: 8px;"%>; float: <%#  Convert.ToInt32(Eval("ArticleTemplate")) ==0 ? "left":Convert.ToInt32(Eval("ArticleTemplate")) ==1 ?"right":"none" %>;"
                        src="<%# "/Services/GetArticleImage.ashx?Id=" + Eval("Id")+"&type=0" %>" alt="" /></div>
                    <a href="/?ArticleId=<%#Eval("Id") %>"><%#Eval("Title")%></a>
          <%--          <p><%#Eval("Description")%></p>--%>
                   <br />
                    <div style="float: right;"><span  ><%# Convert.ToBoolean(Eval("IsVisibleCreateDate")) ==true ? "" :string.Format("{0:dd/MM/yyyy}",Eval("CreatedDate"))%></span></div> 
                </li>
            </ItemTemplate>
        </telerik:RadListView>
    </ul>
</div>
<div class="PageColumns">
    <asp:Panel ID="pnPager" runat="server" ForeColor="Black">
    </asp:Panel>

</div>



<style type="text/css">
    .TelerikModalOverlay {
        filter: alpha(opacity=95) !important; /*for IE 5.5+*/
        opacity: 1 !important; /*for FF 2x, Opera 9x*/
        -moz-opacity: 1 !important; /*for FF 1x*/
        background-color: #BE1E2D !important;
    }
</style>


