<%@ Control Language="C#" AutoEventWireup="true" Inherits="rep_ranking" Codebehind="rep_ranking.ascx.cs" %>
<link href="Style/StyleSheet.css" rel="stylesheet" type="text/css" />
<table id="table1" border="0" width="100%">
    <tr>
        <td style="height: 1px">
            <h3>
                <asp:Label ID="lblTitulo" runat="server" Text="Reporte de ranking"></asp:Label>&nbsp;</h3></td>
        <td style="height: 1px">
            <asp:Label ID="lblIRSElbl" runat="server" Text="IRSE:"></asp:Label></td>
        <td style="height: 1px">
            <asp:Label ID="lblIRSE" runat="server" Text=""></asp:Label></td>
        <td style="height: 1px">
            <asp:Button ID="cmdRegenerar" runat="server" onclick="cmdRegenerar_Click" 
                Text="Regenerar" />
        </td>
    </tr>
</table>
<asp:Panel ID="Panel1" runat="server" Height="2px" Width="737px">
</asp:Panel>