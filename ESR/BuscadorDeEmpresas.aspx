<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" Inherits="buscadorDeEmpresas" Codebehind="BuscadorDeEmpresas.aspx.cs" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" 
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" 
    Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderMain" Runat="Server">
    <table border="0" width="100%" id="table2">
    <tr>
        <td>
        </td>
        <td>
        </td>
        <td align="center">
            <asp:TextBox ID="txtBuscar" runat="server" MaxLength="48" Width="300px"></asp:TextBox><br /><asp:Button ID="btnBuscar" runat="server" Text="Buscar Empresa" OnClick="btnBuscar_Click" /></td>
    </tr>
	<tr>
		<td>
        </td>
		<td>
            <br />
            <br />
            </td>
        <td>
        </td>
	</tr>
	<tr>
		<td></td>
		<td></td>
        <td>
        </td>
	</tr>
    <tr>
        <td>
        </td>
        <td>
            &nbsp;</td>
        <td>
        </td>
    </tr>
</table>
    <table border="0" width="100%" id="table1">
	<tr>
		<td>&nbsp;</td>
		<td style="width: 834px"></td>
	</tr>
	<tr>
		<td style="height: 15px"></td>
		<td style="height: 15px; width: 834px;">
            <asp:Label ID="lblMensaje" runat="server"></asp:Label></td>
	</tr>
        <tr>
            <td colspan="2" align="center">

            <rsweb:ReportViewer ID="ReportViewer1" runat="server" 
                DocumentMapCollapsed="True" DocumentMapWidth="80%" ShowFindControls="False" 
                ShowRefreshButton="False" ShowZoomControl="False" SizeToReportContent="True" 
                Width="800px" ZoomPercent="80" ShowToolBar="False">
            </rsweb:ReportViewer>

            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td style="width: 834px">
            </td>
        </tr>
</table>
</asp:Content>

 <asp:Content ID="Content2" 
ContentPlaceHolderId="PlaceHolderPageTitleInTitleArea" runat="server">
Busca una empresa para vincular tu registro
</asp:Content>