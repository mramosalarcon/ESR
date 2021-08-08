<%@ Page Language="C#" MasterPageFile="~/default.master"AutoEventWireup="true" Inherits="estructuraCuestionario" Codebehind="estructuraCuestionario.aspx.cs" %>

<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" 
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" 
    Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderMain" Runat="Server">
    <div>
        <table border="1" width="100%">
	    <tr>
                <td>
			Nombre del Archivo</td>
		<td>Descripción</td>
            </tr>
            <tr>
                <td>
                    <asp:HyperLink ID="hplGuiaESR" runat="server" NavigateUrl="guia.aspx">Guía ESR</asp:HyperLink></td>
		<td></td>
            </tr>
            <tr>
                <td>
                    <asp:HyperLink ID="hplnkDesEvi" runat="server" NavigateUrl="~/archivos/Descripción evidencias 2010.pdf">Descripción Evidencias 2010</asp:HyperLink></td>
		<td></td>
            </tr>
            <tr>
                <td>
                    <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/archivos/Evidencias_Consumo_2010.xls">Evidencias Consumo</asp:HyperLink></td>
		<td>Este archivo es un documento de apoyo  para que cada empresa pueda organizar las evidencias que incluye en cada indicador del cuestionario (es de uso interno para la empresa, no debe enviarse a Cemefi)</td>
            </tr>
            <tr>
                <td>
                    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/archivos/Evidencias_PyMEs_2010.xls">Evidencias PyMEs</asp:HyperLink></td>
		<td>Este archivo es un documento de apoyo  para que cada empresa pueda organizar las evidencias que incluye en cada indicador del cuestionario (es de uso interno para la empresa, no debe enviarse a Cemefi)</td>
            </tr>
            <tr>
                <td>
                    <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/archivos/Evidencias_ESR_2010.xls">Evidencias ESR</asp:HyperLink></td>
		<td>Este archivo es un documento de apoyo  para que cada empresa pueda organizar las evidencias que incluye en cada indicador del cuestionario (es de uso interno para la empresa, no debe enviarse a Cemefi)</td>
            </tr>
            <tr>
                <td>
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/archivos/Guia_de_hoja_de_evidencias.pdf">Guía de hoja de evidencias</asp:HyperLink></td>
		<td></td>
            </tr>
            <tr>
                <td>
                    <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/archivos/Calculo de Emisiones.xls">Cálculo de emisiones</asp:HyperLink></td>
                <td>
                    Archivo de apoyo con fórmulas en excel para realizar el cáculo de emisiones para
                    contestar algunas preguntas del tema de medio ambiente.</td>
            </tr>
        </table>
    </div>
</asp:Content>