<%@ Page Title="" Language="C#" MasterPageFile="~/Systems/MasterPage.Master" AutoEventWireup="true" CodeBehind="SubDomainManage.aspx.cs" Inherits="PSCPortal.Systems.Engine.SubDomainManage" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Systems/Engine/Scripts/SubDomainManage.js" type="text/javascript">
    </script>

    <script type="text/javascript" language="javascript">
        function initialize() {
            oWnd = $find("<%= rwSubDomain.ClientID %>");
            strWarning = "<%= Resources.Site.Warning %>";
            strInformation = "<%= Resources.Site.Information %>";
            grid = $find("<%=gvSubDomain.ClientID%>");
            strDomainNotChoose = "Bạn chưa chọn Domain";
            strMenuConfirmDelete = "Bạn có chắc muốn xóa Domain này không?";
            strDomainDeleteSuccess = "Xóa thành công";

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindow ID="rwSubDomain" runat="server" Modal="true" VisibleStatusbar="false" OnClientClose="RadWindowSubDomianClose"></telerik:RadWindow>
    <div style="width: 818px; float: left; padding-left: 16px;">
        <div style="width: 815px; float: left; margin-right: 12px;">
            <div class="left_box"></div>
            <div style="width: 795px;" class="box">
                <div class="title_box" align="left">SubDomain</div>
            </div>
            <div class="right_box"></div>
            <div style="background-color: #edeeef; float: left; width: 815px; padding-top: 19px; padding-bottom: 19px;">
                <div>
                    <%--<a href="javascript:void(0)" id="btnNew" style=" visibility:visible;" onclick="SubDomainNew();" class="Header">[Thêm]</a>
                <a href="javascript:void(0)" id="btnEdit" style=" visibility:visible;" onclick="SubDomainEdit();" class="Header">[Sửa]</a>                
                <a href="javascript:void(0)" id="btnDelete" style=" visibility:visible" onclick="SubDomainDelete();" class="Header">[Xóa]</a>                
                <a href="javascript:void(0)" id="btnConfig" style=" visibility:visible" onclick="SubDomainConfig();" class="Header">[Cấu hình]</a>--%>

                    <button type="button" class="btn btn-success btn-xs" onclick="SubDomainNew()"><i class="fa fa-plus"></i>Thêm</button>
                    <button type="button" class="btn btn-success btn-xs" onclick="SubDomainEdit()"><i class="fa fa-edit"></i>Sửa</button>
                    <button type="button" class="btn btn-success btn-xs" onclick="SubDomainConfig()"><i class="fa fa-gears"></i>Cấu hình</button>
                    <button type="button" class="btn btn-success btn-xs" onclick="SubDomainDelete()"><i class="fa fa-close"></i>Xóa</button>
                </div>
                <hr />
                <div>
                    <telerik:RadGrid ID="gvSubDomain" runat="server" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" GridLines="None" AllowMultiRowSelection="True">
                        <HeaderContextMenu EnableAutoScroll="True"></HeaderContextMenu>
                        <MasterTableView ClientDataKeyNames="Id" AllowMultiColumnSorting="True">
                            <Columns>
                                <telerik:GridClientSelectColumn>
                                    <HeaderStyle Width="3%"/>
                                </telerik:GridClientSelectColumn>
                                <telerik:GridBoundColumn HeaderText="Tên" DataField="Name">
                                    <HeaderStyle Width="17%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Link" DataField="PageName">
                                    <HeaderStyle Width="30%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="Mô tả" DataField="Description">
                                    <HeaderStyle Width="50%" />
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings EnableRowHoverStyle="true">
                            <Selecting AllowRowSelect="True" />
                            <ClientEvents OnCommand="gvSubDomain_Command" />
                            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </div>
            </div>
            <div class="bottombox_left">&nbsp;</div>
            <div style="width: 795px;" class="bottombox_center">&nbsp;</div>
            <div class="bottombox_right">&nbsp;</div>
        </div>
    </div>
</asp:Content>
