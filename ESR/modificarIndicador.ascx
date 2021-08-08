<%@ Control Language="C#" AutoEventWireup="true" Inherits="modificarIndicador" Codebehind="modificarIndicador.ascx.cs" %>
<link href="Style/StyleSheet.css" rel="stylesheet" type="text/css" />
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<table id="table1" border="0" width="100%">
    <tr>
        <td style="width: 151px">
            Tema</td>
        <td>
            <asp:DropDownList ID="ddlTemas" runat="server" OnSelectedIndexChanged="ddlTemas_SelectedIndexChanged" AutoPostBack="True" Width="480px">
            </asp:DropDownList></td>
    </tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>

<asp:UpdatePanel ID="UpdatePanel5" runat="server">
    <ContentTemplate>
<table>
    <tr>
        <td>
                    <asp:GridView ID="grdIndicadores" runat="server" AutoGenerateColumns="False">
                    <SelectedRowStyle CssClass="gridSelectedItem" />
                    <HeaderStyle CssClass="gridHeader" />
                    <AlternatingRowStyle CssClass="gridAlternatingItem" />
                    <PagerStyle CssClass="gridPager" />
                    <FooterStyle CssClass="gridFooter" />
                        <Columns>
                            <asp:HyperLinkField AccessibleHeaderText="N&#250;mero de Indicador" DataTextField="idIndicador"
                                HeaderText="No."  DataNavigateUrlFields="idTema,idSubtema,idIndicador" DataNavigateUrlFormatString="~/administradorDeIndicadores.aspx?Content=indicador&amp;idTema={0}&amp;idSubtema={1}&amp;idIndicador={2}" >
                                <ItemStyle Width="1px" />
                            </asp:HyperLinkField>
                            <asp:HyperLinkField AccessibleHeaderText="Descripci&#243;n del indicador" HeaderText="Descripci&#243;n"
                                DataTextField="descripcion"></asp:HyperLinkField>
                            <asp:HyperLinkField AccessibleHeaderText="Descripci&#243;n corta del indicador" HeaderText="Nombre corto"
                                DataTextField="corto"></asp:HyperLinkField>                                
                            <asp:HyperLinkField DataTextField="Subtema" Text="Subtema" HeaderText="Subtema" />
                        </Columns>
                    </asp:GridView>
        </td>
    </tr>
</table>
    </ContentTemplate>
</asp:UpdatePanel>
