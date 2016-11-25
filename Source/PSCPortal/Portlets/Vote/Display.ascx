<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Display.ascx.cs" Inherits="PSCPortal.Portlets.Vote.Display" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<style type="text/css">
            .ykien label {  margin-left:10px; font-family:Verdana; }
    </style>
<script type="text/javascript">
    function CloseModalPopupExtender() {
        $find("pnXemKetQua_ModalPopupExtender").hide();
    }
    function ViewResult() {
        $find("pnXemKetQua_ModalPopupExtender").show();
    }
</script>
<div class="thongbao" style="background:#f6f5f5;height:280px;float:left">
    <div>
        <a style="color:#074c92;font-weight:bold; margin-top:10px;text-transform:uppercase;text-align:center;margin-left:50px;float:left" href="#">Khảo sát ý kiến</a>
    </div>
    <div style="float: left; margin: 10px 10px 0px 10px;">
        <div style="padding: 10px; text-align: justify; color: #1d1d1d; margin-left:10px;">
            <asp:Label ID="lblVoteQuestion" runat="server" Text='<%# VoteQuestionName %>'></asp:Label>
        </div>
        <div style="padding-left: 42px; padding-bottom: 14px; padding-top: 6px">
            <asp:RadioButtonList CssClass="ykien" ID="rblVoteAnswer" Font-Size="8.5pt" runat="server"
                DataTextField="Name" DataValueField="Id" CellPadding="10" CellSpacing="10" >
            </asp:RadioButtonList>
        </div>
        <div style="float: left; margin-right: 5px;padding-left:30px;">
            <asp:ImageButton ID="ibt_Chon" OnClick="ibt_Chon_Click" runat="server" ImageUrl="/Resources/ImagePhoto/button_chon.gif" />
  
            <a onclick="ViewResult();" class="CloseButton">
                <img src="/Resources/ImagePhoto/button_ketqua.gif" /></a></div>
    </div>
</div>
<asp:HiddenField ID="HiddenField3" runat="server" />
<asp:Panel ID="pnXemKetQua" runat="server" Width="400px" CssClass="modalPanel_KetQuaThamDo" BackColor="#cccccc">
    <asp:Panel ID="pnXemKetQua_header" runat="server">
        <div class="title">
            <a href="#">Khảo sát ý kiến</a>
        </div>
    </asp:Panel>
    <div style="width: 100%">
        <div style="text-align: justify; padding: 7px 7px 0px 7px">
            <asp:Label ID="lb_XemKetQua_CauHoi" runat="server" Text="" CssClass="PKH_ykien"></asp:Label>
        </div>
        <div style="padding-top: 15px; padding-left: 30px; margin-right: 30px">
            <asp:GridView ID="gv_KetQua" runat="server" AutoGenerateColumns="false" Width="100%"
                BackColor="#d7e8f8" BorderWidth="1px" BorderColor="#2c76b7">
                <Columns>
                    <asp:BoundField DataField="Name" HeaderText="Câu trả lời " ItemStyle-CssClass="gv_Vote_Item1"
                        ItemStyle-BorderColor="#2c76b7" HeaderStyle-BorderColor="#2c76b7" />
                    <asp:BoundField DataField="Number" HeaderText="Số phiếu" ItemStyle-HorizontalAlign="Justify"
                        ItemStyle-CssClass="gv_Vote_Item" ItemStyle-BorderColor="#2c76b7" HeaderStyle-BorderColor="#2c76b7" />
                    <asp:TemplateField HeaderText="Tỉ lệ" ItemStyle-CssClass="gv_Vote_Item" ItemStyle-BorderColor="#2c76b7"
                        HeaderStyle-BorderColor="#2c76b7">
                        <ItemTemplate>
                            <%# ComputePercent((int)Eval("Number")) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="PKH_title1" ForeColor="#2c76b7" />
            </asp:GridView>
        </div>
        <div style="padding-top: 15px; padding-left: 30px; margin-right: 30px">
            <asp:Label ID="lb_TongSoPhieu" runat="server" Text="" CssClass="gv_Vote_Item1"></asp:Label>
        </div>
        <div style="padding: 7px 7px 7px 7px; text-align: center">
            <a onclick="CloseModalPopupExtender();" style="cursor: pointer">
                <img src="/Resources/ImagePhoto/button_Close.gif" /></a>
        </div>
    </div>
</asp:Panel>
<cc1:ModalPopupExtender BehaviorID="pnXemKetQua_ModalPopupExtender" ID="pnXemKetQua_ModalPopupExtender"
    runat="server" Enabled="True" TargetControlID="HiddenField3" PopupControlID="pnXemKetQua"
    PopupDragHandleControlID="pnXemKetQua_header" BackgroundCssClass="modalBackground">
</cc1:ModalPopupExtender>
