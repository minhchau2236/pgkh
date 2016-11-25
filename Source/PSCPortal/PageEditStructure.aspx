<%@ Page Title="" EnableEventValidation="false" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="PageEditStructure.aspx.cs" Inherits="PSCPortal.PageEditStructure" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Components/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
  <%--  <link href="/Resources/ImagesPortal/HomePage/css/HomePage.css" rel="stylesheet" />
    <link href="/Resources/ImagesPortal/PhongBan/Css/defaul.css" rel="stylesheet" />
    <link href="/Resources/ImagesPortal/Khoa/Css/defaul.css" rel="stylesheet" />--%>


    <%--============================================== Link CSS  =========================================--%>
    <link href="/Components/jquery-ui-1.11.1.custom/jquery-ui.min.css" rel="stylesheet" />
    <link href="/Systems/Engine/CSS/PageEditStructure.css" rel="stylesheet" type="text/css" />

  

    <%--============================================== JS Framework ====================================== --%>
    <script src="Scripts/jquery-2.1.4.min.js"></script>
    <script src="Scripts/knockout-2.3.0.js"></script>
    <script src="Components/jquery-ui-1.11.1.custom/jquery-ui.min.js"></script>

    <%--============================================== JS Custom =========================================--%>

    <script src="Scripts/PageEditStructureForDiv.js"></script>
    <script src="Scripts/Common.js"></script>


    <%--Keo tha dung lon voi jqueryUI 1.9.0 o trong SliderTopicDisplay\Display_SliderShow.ascx--%>
    <%--<script src="/Portlets/SliderTopicDisplay/jquery-ui-1.9.0.custom.min.js"></script>--%>


    <script language="javascript" type="text/javascript">

        var pscjq = jQuery.noConflict();
       
        function PostBack() {
            Redirect(String.format("~/PageEditStructure.aspx?PageId={0}", "<%=PageId %>"));
        }

        window.onload = function (e) {
            editWindow = $find("<%= editRWStructure.ClientID %>");
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      
    <telerik:RadWindow ID="rwPageStructure" ClientIDMode="Static" runat="server" Modal="True" VisibleStatusbar="False" InitialBehaviors="Minimize">
    </telerik:RadWindow>
    <telerik:RadWindow ID="editRWStructure" ClientIDMode="Static" runat="server" Modal="True" VisibleStatusbar="False" InitialBehaviors="Minimize" OnClientClose="EditRW_ClientClose">
    </telerik:RadWindow>
    <div class="quantri">
        <div class="row">
            <div class="logo" style="padding-top: 10px">
                <div class="col-xs-2 col-sm-2 col-md-2">
                    <div class="logo_img">
                        <img src="/Systems/Engine/CSS/logo.png" />
                    </div>
                </div>
                <div class="col-xs-10 col-sm-10 col-md-10">
                    <div class="R_logo">
                        <a href="/Systems/Engine/PageManage.aspx">
                            <img src="/Systems/Engine/CSS/icon_trove.png" /></a>
                        <a href="javascript:void(0)" onclick="PagePreview();" class="ht">
                            <img src="/Systems/Engine/CSS/icon_hienthi.png" /></a>
                    </div>
                    <!--End R_logo-->
                </div>


            </div>
            <!--End logo-->
        </div>
        <br />
        <div class="qt-header">
            <span>QUẢN TRỊ : PANEL - PORTLET - TRANG</span>
        </div>
        <div class="qt-content">
            <div class="row">
                <div class="col-xs-12 col-sm-4 col-md-4">
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            Chọn loại Panel
                        </div>
                        <div class="panel-body">
                            <asp:Panel ID="pnPanelFunction" runat="server">
                                <div class="panel">
                                    <p style="float: left; clear: both">
                                        <span style="float: left; width: 150px">Panel:</span>
                                        <asp:DropDownList CssClass="form-control " ID="ddlPanel" runat="server" DataTextField="Name" DataValueField="Id" Width="200px">
                                        </asp:DropDownList>
                                    </p>
                                    <asp:LinkButton ID="lbtAddPanel" runat="server" CssClass="col-xs-6 col-sm-6 col-md-3 col-xs-offset-1 col-md-offset-0 btn btn-info " OnClick="lbtAddPanel_Click">Thêm</asp:LinkButton>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
                <!-- /.col-lg-4 -->
                <div class="col-xs-12 col-sm-4 col-md-4">
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            Chọn loại Portlet
                        </div>
                        <div class="panel-body">
                            <asp:Panel ID="pnPortletFunction" runat="server">
                                <div class="portlet1">
                                    <p style="float: left; clear: both">
                                        <span style="float: left; width: 150px">Loại Porlet: </span>
                                        <asp:DropDownList ID="ddlPortlet" ClientIDMode="Static" CssClass="form-control" DataTextField="Name" DataValueField="Id" runat="server" Width="200px">
                                        </asp:DropDownList>
                                    </p>
                                    <p style="float: left; clear: both">
                                        <span style="float: left; width: 150px">Tên Porlet: </span>
                                        <asp:TextBox ID="txtPortletInstanceName" ClientIDMode="Static" CssClass="form-control" Width="200px" runat="server"></asp:TextBox>
                                    </p>
                                    <p style="float: left; clear: both">
                                        <span style="float: left; width: 150px">Panel thêm Porlet: </span>
                                        <asp:DropDownList ID="ddlPanelAddPortlet" ClientIDMode="Static" CssClass="form-control" DataTextField="Name" DataValueField="Id" runat="server" Width="200px">
                                        </asp:DropDownList>
                                    </p>
                                    <a style="float: left; clear: both" class="col-xs-6 col-sm-6 col-md-3 btn btn-info" href="javascript:void(0)" onclick="PortletInstanceAdd();">Thêm</a>
                                </div>
                            </asp:Panel>
                        </div>

                    </div>
                </div>
                <!-- /.col-lg-4 -->
                <div class="col-xs-12 col-sm-4 col-md-4">
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            Chọn Trang
                        </div>
                        <div class="panel-body">
                            <div class="portlet2">
                                <p style="float: left; clear: both">
                                    <span style="float: left; width: 150px">Trang: </span>
                                    <asp:DropDownList ID="ddlPage" CssClass="form-control" onchange="LoadPortletReference(this.value);" DataTextField="Name" DataValueField="Id" runat="server" Width="200px">
                                    </asp:DropDownList>
                                </p>
                                <p style="float: left; clear: both">
                                    <span style="float: left; width: 150px">Tên Porlet: </span>
                                    <asp:DropDownList ID="ddlPagePortlet" ClientIDMode="Static" CssClass="form-control" DataTextField="Name" DataValueField="Id" runat="server" Width="200px">
                                    </asp:DropDownList>
                                </p>
                                <p style="float: left; clear: both">
                                    <span style="float: left; width: 150px">Panel thêm Porlet: </span>
                                    <asp:DropDownList ID="ddlPagePanelAdd" ClientIDMode="Static" CssClass="form-control" DataTextField="Name" DataValueField="Id" runat="server" Width="200px">
                                    </asp:DropDownList>
                                </p>
                                <a style="float: left" class=" col-xs-6 col-sm-6 col-md-3 btn btn-info" href="javascript:void(0)" onclick="PortletInstanceReferenceAdd();">Thêm</a>
                            </div>
                        </div>

                    </div>
                </div>
                <!-- /.col-lg-4 -->
            </div>

            <!--End add-->
        </div>
    </div>
    <!--End quantri-->
    <%-- <div style="width:; float: left; padding-left: 145px;">--%>
    <asp:PlaceHolder ID="phDisplay" runat="server"></asp:PlaceHolder>
    <%-- </div>--%>
</asp:Content>
