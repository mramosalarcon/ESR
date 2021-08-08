<%@ Control Language="C#" AutoEventWireup="true" Inherits="avanceDeDiagnostico" Codebehind="avanceDeDiagnostico.ascx.cs" %>
<link href="Style/StyleSheet.css" rel="stylesheet" type="text/css" />
<style type="text/css">
<!--
.style1 {
	font-size: 12px;
	font-weight: bold;
}
-->
</style>
<table id="table1" border="0" width="100%">
    <tr>
        <td colspan="4" align="left">
            <asp:Label ID="lblidTema" runat="server"></asp:Label>
            <asp:Label ID="lblidSubtema" runat="server"></asp:Label>
            <asp:Label ID="lblidIndicador" runat="server"></asp:Label>
            <asp:Label ID="lblReadOnly" runat="server"></asp:Label>
            <asp:Label ID="lblIdCuestionario" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td>
            </td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td style="height: 1px" align="center" ><span class="style1"><p>
         REPORTE DE AVANCE</p>
        </span>
        <td style="height: 1px"></td>
        <td style="height: 1px"></td>
        <td style="height: 1px"></td>
    </tr>
    <tr>
        <td style="height: 1px"> <h5><asp:Label ID="lblTitulo" runat="server"></asp:Label><p>
            <asp:Label ID="lblNombreDeCuestionario" runat="server"></asp:Label></p></h5></td>
        <td style="height: 1px">
        </td>
        <td style="height: 1px">
        </td>
        <td style="height: 1px">
        </td>
    </tr>
    <tr>
        <td style="height: 1px"></td>
        <td style="height: 1px">
        </td>
        <td style="height: 1px">
        </td>
        <td style="height: 1px">
        </td>
    </tr>
</table>
<asp:Panel ID="Panel1" runat="server" Height="2px" Width="737px">
</asp:Panel>