<%@ Page EnableViewState="false" Language="C#" MasterPageFile="~/Systems/DialogMasterPage.Master" AutoEventWireup="true"
    CodeBehind="MenuDetail.aspx.cs" Inherits="PSCPortal.Systems.CMS.MenuDetail" Title="<%# Resources.Site.MenuDetail %>" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Systems/CMS/Scripts/MenuDetail.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">   
        function initialize() {
            editWindow = $find("<%= editRW.ClientID %>");

            strWarning="<%= Resources.Site.Warning %>";
            txtName = document.getElementById("<%= txtName.ClientID %>");
            txtDescription = document.getElementById("<%= txtDescription.ClientID %>");
            
            txtArticle=document.getElementById("<%= txtArticle.ClientID %>");
            rdoArticle=document.getElementById("<%= rdoArticle.ClientID %>");
            rdoTopic = document.getElementById("<%= rdoTopic.ClientID %>");
            rcbTopic = $find("<%= rcbTopic.ClientID %>");
            
            txtDocument=document.getElementById("<%= txtDocument.ClientID %>");
            rdoDocument=document.getElementById("<%= rdoDocument.ClientID %>");
            rdoDocumentType = document.getElementById("<%= rdoDocumentType.ClientID %>");
            rcbDocumentType = $find("<%= rcbDocumentType.ClientID %>");
            
            rcbPage = $find("<%= rcbPage.ClientID %>");
            rcbModule = $find("<%= rcbModule.ClientID %>");
            rdoPage = document.getElementById("<%= rdoPage.ClientID %>");
            rdoModule = document.getElementById("<%= rdoModule.ClientID %>");
            
            rdoURL=document.getElementById("<%= rdoURL.ClientID %>");
            txtURL=document.getElementById("<%= txtURL.ClientID %>");
            strNameCanNotEmpty="<%= Resources.Site.NameCanNotEmpty %>";
            strDescriptionCanNotEmpty="<%= Resources.Site.DescriptionCanNotEmpty %>";            
        }
    </script>
    <style type="text/css">
        .border {
            border: 1px solid #698AC0;
            -webkit-border-radius: 10px;
            -moz-border-radius: 10px;
            border-radius: 10px;
            -webkit-box-shadow: 3px 4px 5px  rgba(200, 193, 193, 0.75);
            -moz-box-shadow: 3px 4px 5px  rgba(200, 193, 193, 0.75);
            box-shadow: 3px 4px 5px rgba(200, 193, 193, 0.75);
        }
        input[type='text']{line-height:25px;}
        .item-lk{padding: 5px 0;}

        .submit-luu{padding:5px 10px; background-color:#698AC0; color:#fff; border-radius:4px; border:1px solid #698AC0;}
        .submit-luu:hover{background-color:#fff; text-decoration:none;}
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:radwindow id="editRW" runat="server" modal="True" visiblestatusbar="False" initialbehaviors="Minimize" onclientclose="EditRW_ClientClose">
    </telerik:radwindow>
    
    <table style="width:100%;padding-top:20px;" align="center" cellpadding="4px;">
	    <tr>
    	    <td style="width:30%;" class="textinput" align="right"><%= Resources.Site.Id %>:</td>
            <td style="width:70%"><asp:TextBox ID="txtId" runat="server" Width="400px" Enabled="false"></asp:TextBox></td>
        </tr>
        <tr>
    	    <td  class="textinput" align="right"><%= Resources.Site.Name %>:</td>
            <td ><asp:TextBox ID="txtName" runat="server" Width="400px"></asp:TextBox></td>
        </tr> 
        <tr>
    	    <td  class="textinput" align="right"><%= Resources.Site.Description %>:</td>
            <td ><asp:TextBox ID="txtDescription" runat="server" Width="400px"></asp:TextBox></td>
        </tr>            
        <tr>
    	    <td  class="textinput" align="right">&nbsp;</td>
            <td>
                <fieldset style="width: 600px; margin-top:10px;" class="border">
                    <legend style="color:#698AC0; font-weight:bold;"><%= Resources.Site.NavigateTo %></legend>
                    <div style="padding:5px 0 5px 10px;">
                        <div class="item-lk">
                            <asp:RadioButton ID="rdoArticle" runat="server" GroupName="Link" />
                            <span class="textinput"><%= Resources.Site.Article %></span>
                            <br />
                            <asp:TextBox ID="txtArticle" runat="server" Width="440px"></asp:TextBox>
                            <input id="btnBrowse" type="button" value="<%= Resources.Site.Browse %>" onclick="OnBrowseArticle();" style="height:30px" />                            
                        </div>
                       
                        <div class="item-lk">
                            <asp:RadioButton ID="rdoTopic" runat="server" GroupName="Link" />
                            <span class="textinput"><%= Resources.Site.Topic %></span>
                            <br />                        
                            <telerik:RadComboBox ID="rcbTopic" Width="500px" DataTextField="Path" DataValueField="Id" runat="server">
                            </telerik:RadComboBox>                        
                        </div>

                         <div class="item-lk">
                            <asp:RadioButton ID="rdoPage" runat="server" GroupName="Link" />
                            <span class="textinput"><%= Resources.Site.Page %></span>
                            <br />                        
                            <telerik:RadComboBox ID="rcbPage" Width="500px" DataTextField="Name" DataValueField="Id" runat="server">
                            </telerik:RadComboBox>  
                        </div>

                         <div class="item-lk">
                            <asp:RadioButton ID="rdoModule" runat="server" GroupName="Link" />
                            <span class="textinput"><%= Resources.Site.Module %></span>
                            <br />                        
                            <telerik:RadComboBox ID="rcbModule" Width="500px" DataTextField="Name" DataValueField="Id" runat="server">
                            </telerik:RadComboBox>                                                
                        </div>

                        <div style="display: none">
                            <asp:RadioButton ID="rdoDocumentType" runat="server" GroupName="Link" />
                            <span class="textinput"><%= Resources.Site.DocumentType %></span>
                            <br />                        
                            <telerik:RadComboBox ID="rcbDocumentType" Width="200px" DataTextField="Path" DataValueField="Id" runat="server">
                            </telerik:RadComboBox>                        
                            <br />
                             <asp:RadioButton ID="rdoDocument" runat="server" GroupName="Link" />
                            <span class="textinput"><%= Resources.Site.Document %></span>
                            <br />
                            <asp:TextBox ID="txtDocument" runat="server"></asp:TextBox>
                            <input id="Button1" type="button" value="<%= Resources.Site.Browse %>" onclick="OnBrowseDocument();" /><br />
                        </div>

                         <div class="item-lk">
                            <asp:RadioButton ID="rdoURL" runat="server" GroupName="Link" />
                            <span class="textinput"><%= Resources.Site.OtherPage %></span>
                            <br />
                            <asp:TextBox Width="500px" ID="txtURL" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </fieldset>
            </td>
        </tr> 
        <tr align="center">
            <td colspan="2">
                <hr style="width:800px;"/>
            </td>
        </tr>
        
        <tr>
            <td></td>
            <td>
                <table style="width:15%;" >
                    <tr>
                        <td style="width:50%;"><a href="javascript:void(0)" onclick="Save();" class="submit submit-luu"><%= Resources.Site.Save %></a></td>
                        <td style="width:50%"><a href="javascript:void(0)" onclick="Cancel();" class="submit "><%= Resources.Site.Cancel %></a></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>       
</asp:Content>
