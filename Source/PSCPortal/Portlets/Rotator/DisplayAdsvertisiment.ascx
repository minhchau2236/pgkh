<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DisplayAdsvertisiment.ascx.cs" Inherits="PSCPortal.Portlets.Rotator.DisplayAdsvertisiment" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<style type="text/css">
    .advertisiment {
        clear: both;
        float: left;
    }
    .advertisiment .RadRotator_Default .rrClipRegion
    {
        border: 0 !important;
    }

    .advertisiment .rrClipRegion .rrItemsList
    {
        float: left;
        padding: 0;
        margin: 0;
        list-style: none !important;
    }

    .advertisiment .RadRotator ul.rrItemsList
    {
        padding: 0;
        margin: 0;
    }

    .advertisiment .rrButton
    {
        width: 8px;
        height: 9px;
    }

    .advertisiment .rrItemsList
    {
        
    }

    .advertisiment .RadRotator
    {
       
    }

    .advertisiment .RadRotator_Default .rrClipRegion
    {
       
    }

    .advertisiment .rrButton.rrButtonLeft
    {
        background-position: 0 0;
        left: -14px;
        background-image: url('/Resources/ImagesPortal/HomePage/arrow_L.png');
    }

        .khampha .rrButton.rrButtonLeft:hover
        {
            background-position: 0 0;
        }

    .advertisiment .rrButton.rrButtonRight
    {
        background-image: url('/Resources/ImagesPortal/HomePage/arrow_R.png');
        background-repeat: no-repeat;
        background-position: 0 0;
        right: -17px;
    }

        .advertisiment .rrButton.rrButtonRight:hover
        {
            background-position: 0 0;
        }
</style>
<div class="advertisiment">
    <telerik:RadRotator ID="RadRotator1" ItemHeight="93" ItemWidth="244" Height="93"
        Width="244" runat="server" ScrollDuration="1000" FrameDuration="5000" RotatorType="AutomaticAdvance">
        <ItemTemplate>
            <a href="<%#Eval("Link") %>" target="_blank">
                <img src="/<%#Eval("Url") %>" style="width: 244px; height: 93px;"></a>
        </ItemTemplate>
    </telerik:RadRotator>
</div>
