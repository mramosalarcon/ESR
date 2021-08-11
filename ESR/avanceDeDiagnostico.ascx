<%@ Control Language="C#" AutoEventWireup="true" Inherits="avanceDeDiagnostico" Codebehind="avanceDeDiagnostico.ascx.cs" %>
<link href="Style/StyleSheet.css" rel="stylesheet" type="text/css" />
<style type="text/css">
<!--
.style1 {
	font-size: 24px;
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
        <td align="center">
            <span class="style1"><!--<p>Reporte de avance</p>-->
	        </span>
	    </td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td><span class="style1">
	    <p><asp:Label ID="lblTitulo" runat="server"></asp:Label></p>
	    <p><asp:Label ID="lblNombreDeCuestionario" runat="server"></asp:Label></p>
	</span></td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>

		
        <td style="height: 1px"></td>
        <td style="height: 1px"></td>
        <td style="height: 1px"></td>
    </tr>
    <tr>
        <td style="height: 1px">
	</td>
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