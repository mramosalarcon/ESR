<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" Inherits="misCuestionarios" Codebehind="misCuestionarios.aspx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content4" ContentPlaceHolderID="PlaceHolderPageTitle" Runat="Server">
Cuestionario ESR®
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderMain" Runat="Server">
<p>&nbsp</p>
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
                <h3><asp:Label ID="lblOpcion" runat="server" Text="Operación:"></asp:Label></h3></td>
            <td>
                <h3><asp:Label ID="lblOpcionSeleccionada" runat="server" Text="Contestar cuestionario"></asp:Label></h3></td>
        </tr>
        <tr>
            <td>
                <h3><asp:Label ID="lblNomEmpresa" runat="server" Text="Empresa:"></asp:Label></h3></td>
            <td>
                <h3><asp:Label ID="lblNombreEmpresa" runat="server"></asp:Label></h3></td>
        </tr>
	<tr>
		<td><h3>
            <asp:Label ID="lblCuestionarios" runat="server" Text="Cuestionario: "></asp:Label></h3></td>
		<td><h3>
            <asp:DropDownList ID="ddlCuestionarios" runat="server">
            </asp:DropDownList>
                <asp:Button ID="btnIniciarCuestionario" runat="server" Text="Aceptar" OnClick="btnIniciarCuestionario_Click" /></h3></td>
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
Distintivo ESR®<br />
<asp:Label ID="lblEmpresa" runat="server" Text=""></asp:Label>
</asp:Content>