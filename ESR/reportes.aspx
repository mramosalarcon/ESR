<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" Inherits="reportes" Codebehind="reportes.aspx.cs" %>

<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" 
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" 
    Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>


<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderMain" Runat="Server">
<asp:Panel ID="panMenu" runat="server">        
</asp:Panel>   
    <div>
        <asp:PlaceHolder ID="reportesESR" runat="server"></asp:PlaceHolder>
    </div>
</asp:Content>
 <asp:Content ID="Content2" 
ContentPlaceHolderId="PlaceHolderPageTitleInTitleArea" runat="server">
Reportes del Proceso Empresa Socialmente Responsable
</asp:Content>
