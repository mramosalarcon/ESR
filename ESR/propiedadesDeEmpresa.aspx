<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" Inherits="propiedadesDeEmpresa" Codebehind="propiedadesDeEmpresa.aspx.cs" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderMain" Runat="Server">
    <table border="0" width="100%">
	<tr>
		<td colspan="2" align="center" style="height: 23px">
            <asp:TextBox ID="txtBuscarEmpresa" runat="server" Width="300px" MaxLength="48"></asp:TextBox><br />
            <asp:Button ID="btnBuscarEmpresa" runat="server" Text="Buscar empresa" OnClick="btnBuscarEmpresa_Click" /></td>
	</tr>
	<tr>
		<td>&nbsp;</td>
		<td>&nbsp;</td>
	</tr>
        <tr>
            <td colspan="2" align="center">&nbsp;
                </td>
        </tr>
</table>
    <table border="0" width="100%">
        <tr>
            <td style="height: 18px">
                <asp:Label ID="lblMensaje" runat="server"></asp:Label></td>
            <td style="height: 18px">
            </td>
        </tr>
	<tr>
		<td style="height: 18px">
            Empresa</td>
		<td style="height: 18px">
            <asp:Label ID="lblEmpresa" runat="server"></asp:Label></td>
	</tr>
	<tr>
		<td style="height: 21px">
            Cuestionarios disponibles</td>
		<td style="height: 21px">
            <asp:DropDownList ID="ddlCuestionarios" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCuestionarios_SelectedIndexChanged">
            </asp:DropDownList></td>
	</tr>
        <tr>
            <td style="height: 13px">
                Fecha límite para contestar</td>
            <td style="height: 13px">
                <asp:TextBox ID="txtFechaLimite" runat="server"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFechaLimite" Format="dd/MM/yyyy">
                </asp:CalendarExtender>

                
            </td>
        </tr>
        <tr>
            <td style="height: 13px">
            </td>
            <td style="height: 13px">
                <asp:Button ID="btnGuardar" runat="server" OnClick="btnGuardar_Click" Text="Guardar" /></td>
        </tr>
        <tr>
            <td align="center" colspan="2">
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
        <asp:GridView ID="grdEmpresas" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#E3EAEB" />
            <Columns>
                <asp:HyperLinkField DataNavigateUrlFields="idEmpresa,nombre" DataNavigateUrlFormatString="~/propiedadesDeEmpresa.aspx?idEmpresa={0}&amp;nombre={1}"
                    DataTextField="idEmpresa" HeaderText="ID Empresa" NavigateUrl="~/propiedadesDeEmpresa.aspx"
                    Text="idEmpresa" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="nombreCorto" HeaderText="Nombre Corto" />
            </Columns>
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#7C6F57" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
            </td>
        </tr>
</table>
</asp:Content>