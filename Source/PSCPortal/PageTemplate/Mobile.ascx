<%@ Control Language="C#" AutoEventWireup="true" Inherits="PSCPortal.Libs.PageEngine" %>
<link href="/Resources/ImagesPortal/HomePage/Mobile/style.css" rel="stylesheet" />
<style type="text/css">
    @media (max-width : 966px) {
      

        .panelCss {
            height: auto !important;
            width: auto !important;
            padding: 0 !important;
            width: 100% !important;
        }

        .left {
            margin: 0;
        }

        .right {
            margin: 0;
        }

        table {
            width: 100%;
        }

        .logo {
            margin: 0;
            float: none;
            width: auto;
        }

        .wrapper {
            width: 100%;
        }

        .gray {
            width: 100%;
        }

        .vien {
            width: 100%;
        }

        .ZebraDialog {
            left: 10px !important;
            right: 10px !important;
        }

        .mobileDisplay .container {
            height: 500px !important;
        }

        .nivoSlider {
            height: 500px !important;
        }

            .nivoSlider img {
                height: 500px !important;
            }

        a {
            font: inherit;
        }
    }

    @media (max-width : 768px) {
        .mobileDisplay .container {
            height: 400px !important;
        }

        .nivoSlider {
            height: 400px !important;
        }

            .nivoSlider img {
                height: 400px !important;
            }
    }

    @media (max-width : 640px) {
        .mobileDisplay .container {
            height: 300px !important;
        }

        .nivoSlider {
            height: 300px !important;
        }

            .nivoSlider img {
                height: 300px !important;
            }
    }

    @media (max-width : 500px) {
        .mobileDisplay .container {
            height: 200px !important;
        }

        .nivoSlider {
            height: 200px !important;
        }

            .nivoSlider img {
                height: 200px !important;
            }
    }
</style>
<div class="gray">
    <div class="vien">
        <div class="wrapper">
<asp:PlaceHolder ID="phDisplay" runat="server"></asp:PlaceHolder>
</div>
</div>
</div>