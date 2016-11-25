<%@ Page Title="" Language="C#" MasterPageFile="~/Systems/MasterPage.Master" AutoEventWireup="true" CodeBehind="ArticleInPageViewTimeReport.aspx.cs" Inherits="PSCPortal.Systems.CMS.ArticleInPageViewTimeReport" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" language="javascript">
        var rdtpTuNgay;
        var rdtpDenNgay;
        var gvThongke;
        function gvThongke_Command(sender, args) {
            args.set_cancel(true);
            sender.get_masterTableView().clearSelectedItems();
            var currentPageIndex = sender.get_masterTableView().get_currentPageIndex();
            var pageSize = sender.get_masterTableView().get_pageSize();
            var sortExpressions = sender.get_masterTableView().get_sortExpressions();
            PageIndexCurrent = currentPageIndex * pageSize;
            pageSizeCurrent = pageSize;
            sortExpressionsCurrent = sortExpressions;
            PageMethods.ArticleLoadCommand(currentPageIndex * pageSize, pageSize, sortExpressions.toString(), ArticleLoadCommandCallback);
        }
        function ArticleLoadCommandCallback(results) {
            ShowGrid();
            var tableView = gvThongke.get_masterTableView();
            tableView.set_dataSource(results["Data"]);
            tableView.set_virtualItemCount(results["Count"]);
            tableView.dataBind();
        }
        function Initialize() {
            rdtpTuNgay = $find("<%=rdtpTuNgay.ClientID %>");
            rdtpDenNgay = $find("<%=rdtpDenNgay.ClientID%>");
            gvThongke = $find("<%=gvThongke.ClientID %>");
        }
        function ShowGrid() {
            $find("<%=gvThongke.ClientID%>").get_element().style.display = "";
        }
        function HideGrid() {
            $find("<%=gvThongke.ClientID%>").get_element().style.display = "none";
        }
        function pageLoad() {
            Initialize();
            HideGrid();
        }
        //button Tìm
        function Search() {
            var tableView = gvThongke.get_masterTableView();
            var sortExpressions = tableView.get_sortExpressions();
            var tungay = rdtpTuNgay.get_dateInput().get_value();
            var denngay = rdtpDenNgay.get_dateInput().get_value();
            PageMethods.Search(tungay, denngay, 0, tableView.get_pageSize(), sortExpressions.toString(), ArticleLoadCommandCallback);
        }
        //button Xuất Excel
        function XuatExcel() {
            var tungay = rdtpTuNgay.get_dateInput().get_value();
            var denngay = rdtpDenNgay.get_dateInput().get_value();
            window.open("/Services/ExportToExcel.ashx?fromDate=" + tungay + "&toDate=" + denngay + "&option=TKBaiVietDaDang");
        }
 </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="clear: both; float: left; width: 700px; padding: 0px 10px 10px 10px;">
        <div style="clear: both; float: left; padding: 0px 0px 0px 130px">
            <fieldset style="border-right: #006699 1px solid; border-top: #006699 1px solid;
                border-left: #006699 1px solid; border-bottom: #006699 1px solid; width: 500px;">
                <div style="clear: both; float: left; padding: 0px 0px 0px 150px; height: 30px;
                    font-weight: bold;">
                    Thống kê số lượng bài viết được đăng</div>
                <div style="clear: both; float: left; width: 450px; padding-left: 50px; padding-bottom: 10px;">
                    <div style="line-height: 30px; padding-bottom: 5px">
                        <div style="float: left; width: 100px;" align="left">
                            Từ ngày:</div>
                        <div style="float: left">
                            <telerik:RadDatePicker ID="rdtpTuNgay" runat="server" DateFormat="d/M/yyyy" DisplayDateFormat="dddd, d MMMM, yyyy"
                                Culture="Vietnamese (Vietnam)" Width="250px">
                                <DateInput ID="DateInput2" runat="server" DateFormat="d/M/yyyy" DisplayDateFormat="d/M/yyyy"
                                    MinDate="1/1/0001 12:00:00 AM" ToolTip="Định dạng của [Ngày/Tháng/Năm]: [d/M/yyyy]">
                                </DateInput>
                            </telerik:RadDatePicker>
                        </div>
                    </div>
                    <div style="clear: both">
                    </div>
                    <div style="line-height: 30px; padding-bottom: 5px">
                        <div style="float: left; width: 100px" align="left">
                            Đến ngày:</div>
                        <div style="float: left">
                            <telerik:RadDatePicker ID="rdtpDenNgay" runat="server" DateFormat="d/M/yyyy" DisplayDateFormat="dddd, d MMMM, yyyy"
                                Culture="Vietnamese (Vietnam)" Width="250px">
                                <DateInput ID="DateInput3" runat="server" DateFormat="d/M/yyyy" DisplayDateFormat="d/M/yyyy"
                                    MinDate="1/1/0001 12:00:00 AM" ToolTip="Định dạng của [Ngày/Tháng/Năm]: [d/M/yyyy]">
                                </DateInput>
                            </telerik:RadDatePicker>
                        </div>
                    </div>
                    <div style="clear: both">
                    </div>
                    <div style="padding-top: 10px; padding-left: 100px; padding-bottom: 10px;">
                        <div style="float: left; padding-right: 20px">
                            <input type="button" id="btTim" value="Tìm kiếm" onclick="Search()" />
                        </div>
                        <div style="float: left; padding-right: 20px;">
                            <input type="button" id="btXuatExcel" value="Xuất Excel" onclick="XuatExcel()"/>
                        </div>
                    </div>
                </div>
            </fieldset>
           <%-- Tổng số bài viết: <asp:TextBox ID="txtCount" runat="server"></asp:TextBox>--%>
        </div>
        <div style="clear: both; float: left; padding: 10px 0px 0px 0px">
            <telerik:RadGrid ID="gvThongke" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                AllowSorting="True" GridLines="None" AllowMultiRowSelection="True" Height="350px"
                Width="800px">
                <MasterTableView ClientDataKeyNames="Id" AllowMultiColumnSorting="True">
                    <Columns>
                        <telerik:GridBoundColumn HeaderText='Tiêu đề' DataField="Title">
                            <ItemStyle HorizontalAlign="Justify" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText='Ngày đăng' DataField="CreatedDate" DataFormatString="{0:dd/MM/yyyy}">
                            <ItemStyle Width="100px" HorizontalAlign="Center" />
                            <HeaderStyle Width="100px" HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>
                       <%-- <telerik:GridBoundColumn HeaderText='Lượng truy cập' DataField="ViewTime">
                            <ItemStyle Width="100px" HorizontalAlign="Center" />
                            <HeaderStyle Width="100px" HorizontalAlign="Center" />
                        </telerik:GridBoundColumn>--%>
                    </Columns>
                </MasterTableView>
                <ClientSettings EnableRowHoverStyle="true">
                    <Selecting AllowRowSelect="True" />
                    <ClientEvents OnCommand="gvThongke_Command" />
                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                </ClientSettings>
            </telerik:RadGrid>           
        </div>
        
    </div>
</asp:Content>
