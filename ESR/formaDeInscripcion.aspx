<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" Inherits="formaDeInscripcion" Codebehind="formaDeInscripcion.aspx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" 
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" 
    Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>

<asp:Content ID="Content4" ContentPlaceHolderID="PlaceHolderPageTitle" Runat="Server">
    Registro ESR�
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderMain" Runat="Server">
    <p>&nbsp</p>

    <asp:Panel ID="panMenu" runat="server">        
    </asp:Panel>      
    <div>
        <asp:PlaceHolder ID="content" runat="server"></asp:PlaceHolder>
    </div>
</asp:Content>

 <asp:Content ID="Content2" 
ContentPlaceHolderId="PlaceHolderPageTitleInTitleArea" runat="server">
     Forma de registro Distintivo ESR� <br />
     <asp:Label ID="lblEmpresa" runat="server" Text="Label"></asp:Label>
</asp:Content>