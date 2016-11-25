<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DisplayKhamPha.ascx.cs" Inherits="PSCPortal.Portlets.Rotator.DisplayKhamPha" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<style type="text/css">
    .RadRotator_Default .rrClipRegion {
        border: 0 !important;
    }

    .rrClipRegion .rrItemsList {
           list-style: none;
    margin-left: 6px;
    float: left;
    padding: 0;
    }
    .slide{
        float: left;
        margin-left: 20px;
    }
    .RadRotator ul.rrItemsList {
           list-style: none;
    margin-left: 6px;
    float: left;
    padding: 0;
    }

    .rrButton {
        width: 8px;
        height: 9px;
    }

    .rrItemsList {
        top: 16px !important;
    }

    .RadRotator {
        padding-left: 2px !important;
        padding-right: 0px !important;
    }

    .RadRotator_Default .rrClipRegion {
        /*margin-left: 10px;*/
        border: 0;
        width: 699px !important;
    }

    .rrButton.rrButtonLeft {
        background-position: 0 0;
        top: 60px;
        background-image: url('/Resources/ImagesPortal/HomePage/imgs/arrow_L.png');
        width: 18px;
        height: 64px;
    }

        .rrButton.rrButtonLeft:hover {
            background-position: 0 0;
        }

    .rrButton.rrButtonRight {
        /*background-repeat: no-repeat;*/
        background-position: 0 0;
        right: -15px;
        top: 60px !important;
        background-image: url('/Resources/ImagesPortal/HomePage/imgs/arrow_R.png');
        width: 18px;
        height: 64px;
    }

        .rrButton.rrButtonRight:hover {
            background-position: 0 0;
        }

    .RadRotator_Default {
        width: 737px !important;
    }
</style>
<div class="left">

<div class="slider-1">
    <div class="slide">   
        <div class="three" style="padding: 0 !important;">
            <telerik:RadRotator ID="RadRotator1" ItemHeight="100" ItemWidth="227" Height="142"
                Width="693" runat="server" ScrollDuration="500" FrameDuration="2000" RotatorType="Buttons">
                <ItemTemplate>
                    <a class="float-shadow" href="<%#Eval("Link") %>" target="_blank">
                        <img src="/<%#Eval("Url") %>" style="width: 224px; height: 100px;"></a>
                </ItemTemplate>
            </telerik:RadRotator>
        </div>
</div>
 </div>
</div>