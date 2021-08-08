<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" Inherits="misCuestionarios" Codebehind="misCuestionarios.aspx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderMain" Runat="Server">

    <asp:Panel ID="panMenu" runat="server">        
    </asp:Panel>
    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
    <table border="0" width="100%" id="table1">
        <tr>
            <td>
            </td>
            <td align="right">
                </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblOpcion" runat="server" Text="Opcion:"></asp:Label></td>
            <td>
                <h4><asp:Label ID="lblOpcionSeleccionada" runat="server" Text="Contestar cuestionario"></asp:Label></h4></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblEmpresa" runat="server" Text="Empresa:"></asp:Label></td>
            <td>
                <h4><asp:Label ID="lblNombreEmpresa" runat="server"></asp:Label></h4></td>
        </tr>
	<tr>
		<td style="height: 22px">
            <asp:Label ID="lblCuestionarios" runat="server" Text="Cuestionario: "></asp:Label></td>
		<td style="height: 22px">
            <asp:DropDownList ID="ddlCuestionarios" runat="server">
            </asp:DropDownList>
                <asp:Button ID="btnIniciarCuestionario" runat="server" Text="Aceptar" OnClick="btnIniciarCuestionario_Click" /></td>
	</tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
        </tr>
	<tr>
		<td>&nbsp;</td>
		<td>&nbsp;</td>
	</tr>
</table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <table width="100%">
            <tr>
                <td align="center">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <img src="images/progress_bar_2_1.gif" />
            </ProgressTemplate>
        </asp:UpdateProgress>
    </td>
            </tr>
        </table>


    </div>
</asp:Content>
 <asp:Content ID="Content2" 
ContentPlaceHolderId="PlaceHolderPageTitleInTitleArea" runat="server">
Autodiagnóstico ESR 2015
</asp:Content>