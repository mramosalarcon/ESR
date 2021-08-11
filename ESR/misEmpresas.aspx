<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" Inherits="misEmpresas" Codebehind="misEmpresas.aspx.cs" %>


<asp:Content ID="Content4" ContentPlaceHolderID="PlaceHolderPageTitle" Runat="Server">
Mis Empresas ESR®
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderMain" Runat="Server">
    <div>
<table border="0" width="100%" id="table1">
    <tr>
        <td>
        </td>
        <td>
        </td>
    </tr>
	<tr>
		<td>Seleccione la empresa para la cual va a contestar cuestionario:</td>
		<td>
            <asp:DropDownList ID="ddlEmpresas" runat="server">
            </asp:DropDownList></td>
	</tr>
    <tr>
        <td>
        </td>
        <td>
        </td>
    </tr>
	<tr>
		<td></td>
		<td>
            <asp:Button ID="btnAceptar" runat="server" OnClick="btnAceptar_Click" Text="Aceptar" /></td>
	</tr>
</table>
    </div>
    </asp:Content>

 <asp:Content ID="Content2" 
ContentPlaceHolderId="PlaceHolderPageTitleInTitleArea" runat="server">
Mis Empresas Socialmente Responsables
</asp:Content>