<%@ Control Language="C#" AutoEventWireup="true" Inherits="indicador" Codebehind="indicador.ascx.cs" %>
<link href="Style/StyleSheet.css" rel="stylesheet" type="text/css" />

<style type="text/css">
    .style1
    {
        width: 198px;
        height: 5px;
    }
</style>

<div id="datosIndicador" runat="server">
<table id="table2" border="0" width="100%">
    <tr>
        <td style="width: 198px">
            &nbsp;</td>
        <td colspan="2">
        </td>
        <td style="width: 8px">
        </td>
    </tr>
    <tr>
        <td style="height: 3px; width: 198px;">
            Tema</td>
        <td rowspan="2" colspan="2">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <ContentTemplate>
        <table border="0" width="100%" id="table1">
	<tr>
		<td style="width: 835px"><asp:DropDownList ID="ddlTemas" runat="server" OnSelectedIndexChanged="ddlTemas_SelectedIndexChanged" AutoPostBack="True" Width="480px">
            </asp:DropDownList><asp:Label ID="lblTema" runat="server" Visible="False"></asp:Label></td>
	</tr>
	<tr>
		<td style="width: 835px"><asp:DropDownList ID="ddlSubtemas" runat="server" Width="480px">
            </asp:DropDownList>
            <asp:RangeValidator ID="rfvSubtema" runat="server" ControlToValidate="ddlSubtemas"
                ErrorMessage="Seleccione un subtema" MaximumValue="99" MinimumValue="1" SetFocusOnError="True"
                Type="Integer">*</asp:RangeValidator>
        </td>
	</tr>
</table>
</ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td style="height: 3px; width: 8px;">
        </td>
        <td style="height: 3px">
        </td>
    </tr>
    <tr>
        <td style="width: 198px">
            Subtema</td>
        <td style="width: 8px">
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td style="width: 198px">
            Afinidad</td>
        <td>Afinidad del indicador: <br />
            <asp:PlaceHolder ID="phAfinidades" runat="server"></asp:PlaceHolder>
        </td>
        <td>Principios de Responsabilidad Social:<br />
            <asp:PlaceHolder ID="phPrincipiosRSE" runat="server"></asp:PlaceHolder>
        </td>
        <td style="width: 8px">
            </td>
        <td>
        </td>
    </tr>
    <tr>
        <td style="height: 21px; width: 198px;">
            ¿Permitir cargar evidencias?</td>
        <td colspan="4" style="height: 21px">
            <asp:RadioButton ID="rbtEvidenciaSi" runat="server" Checked="True" Text="Si" GroupName="Evidencia" /><asp:RadioButton ID="rbtEvidenciaNo" runat="server" Text="No" GroupName="Evidencia" />
        </td>
    </tr>
    <tr>
        <td style="height: 26px; width: 198px;">
            ¿Es obligatorio?</td>
        <td style="height: 26px" colspan="2"><asp:RadioButton ID="rbtObligatorioSi" runat="server" Checked="True" Text="Si" GroupName="Obligatorio" /><asp:RadioButton ID="rbtObligatorioNo" runat="server" Text="No" GroupName="Obligatorio" /></td>
        <td style="height: 26px; width: 8px;">
            </td>
        <td style="height: 26px">
            </td>
    </tr>
    <tr>
        <td align="center" colspan="5">
        </td>
    </tr>
    <tr>
        <td class="style1">
            &nbsp;&nbsp;</td>
        <td colspan="3" style="height: 26px" rowspan="3">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <table width="100%">
                        <tr>
                            <td><asp:TextBox ID="txtIdIndicador" runat="server" Width="30px" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkLiberado" Text="Liberar indicador" runat="server" 
                                    ForeColor="Red" Visible="False" />
                            </td>
                        </tr>
                    </table>
                    <asp:TextBox ID="txtDescripcion" runat="server" Height="80px" Rows="3" 
                        TextMode="MultiLine" Width="550px"></asp:TextBox>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlTemas" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
            </td>
        <td style="height: 26px" rowspan="3">
        </td>
    </tr>
    <tr>
        <td style="height: 26px; width: 198px;">
            Texto del indicador:
        </td>
    </tr>
    <tr>
        <td style="height: 26px; width: 198px;">
            &nbsp;</td>
    </tr>
    <tr>
        <td style="height: 62px; width: 198px;">
            Texto alterno del indicador (Perú)</td>
        <td colspan="4" style="height: 62px">
            <asp:TextBox ID="txtDescripcion_alterna" runat="server" Rows="3" TextMode="MultiLine" 
                        Width="550px" Height="80px"></asp:TextBox>
                </td>
    </tr>
    <tr>
        <td style="height: 62px; width: 198px;">
            Texto en portugués</td>
        <td colspan="4" style="height: 62px">
            <asp:TextBox ID="txtDescripcion_portugues" runat="server" Rows="3" TextMode="MultiLine" 
                        Width="550px" Height="80px"></asp:TextBox>
                </td>
    </tr>
    <tr>
        <td style="height: 62px; width: 198px;">
            Texto corto:</td>
        <td colspan="4" style="height: 62px">
            <asp:TextBox ID="txtCorto" runat="server" Width="550px" Height="55px"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="height: 36px; width: 198px;">
            Notas de ayuda:</td>
        <td colspan="4" style="height: 36px">
            <asp:TextBox ID="txtNotas" runat="server" Width="550px" Rows="3" 
                TextMode="MultiLine" Height="80px"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="height: 30px; width: 198px;">
            Recomendaciones:</td>
        <td colspan="4" style="height: 30px">
            <asp:TextBox ID="txtRecomendacion" runat="server" Width="550px" Rows="3" 
                TextMode="MultiLine" Height="80px"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="height: 21px; width: 198px;">
            Evidencias:</td>
        <td colspan="4" style="height: 21px">
            <asp:TextBox ID="txtEvidencias" runat="server" Width="550px" Rows="3" 
                TextMode="MultiLine" Height="80px"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="height: 210px; width: 198px;">
            <asp:Button ID="btnAnadirIncisos" runat="server" 
                Text="Añadir Incisos" Width="155px" OnClick="btnAnadirIncisos_Click" 
                Visible="False"/></td>
        <td style="height: 210px" colspan="2">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" Visible="True">
                <ContentTemplate>
                    <table border="0" width="100%" id="table4">
                    <tr>
                        <td style="height: 15px" colspan="2">
                            <asp:GridView ID="grdIncisos" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" OnRowCommand="grdIncisos_RowCommand" DataKeyNames="idInciso,descripcion" OnRowDeleting="grdIncisos_RowDeleting" Width="480px">
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <EditRowStyle BackColor="#999999" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:HyperLinkField Text="Inciso" DataTextField="idInciso" AccessibleHeaderText="Id Inciso" HeaderText="Id Inciso" >
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:HyperLinkField>
                                    <asp:HyperLinkField DataTextField="descripcion" HeaderText="Descripci&#243;n" >
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:HyperLinkField>
                                    <asp:CommandField ShowSelectButton="True" HeaderText="Seleccionar Inciso" >
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:CommandField>
                                    <asp:CommandField ButtonType="Image" DeleteImageUrl="~/images/close.gif" HeaderText="Eliminar Inciso"
                                        ShowDeleteButton="True">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:CommandField>
                                </Columns>
                            </asp:GridView>
                            <asp:Label ID="lblEditar" runat="server" Visible="False"></asp:Label>
                        </td>
                    </tr>
	                    <tr>
		                    <td style="height: 83px"><asp:TextBox ID="txtInciso" runat="server" Rows="1" TextMode="MultiLine"
                Width="450px" Visible="False" Height="81px"></asp:TextBox></td>
		                    <td style="height: 83px"><asp:Button ID="btnGuardarYAgregar" runat="server" 
                Text="+" OnClick="btnGuardarYAgregar_Click" Width="20px" Height="22px" Visible="False" /></td>
	                    </tr>
                    </table>                
                </ContentTemplate>
               <Triggers>
               <asp:AsyncPostBackTrigger ControlID="btnAnadirIncisos" EventName="Click" />
               </Triggers>
            </asp:UpdatePanel>
            </td>
        <td style="width: 8px; height: 210px">
        </td>
        <td style="height: 210px">
        </td>
    </tr>
    <tr>
        <td style="width: 198px">
            <asp:Button ID="btnPreguntaAdicional" runat="server" 
                Text="Pregunta Adicional" Width="155px" OnClick="btnPreguntaAdicional_Click"/></td>
        <td colspan="3">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
        <table border="0" width="100%">
	<tr>
		<td colspan="3">
            <asp:GridView ID="grdPAs" runat="server" AutoGenerateColumns="False" CellPadding="4"
                ForeColor="#333333" GridLines="None" DataKeyNames="idPregunta,pregunta" OnRowCommand="grdPAs_RowCommand" OnRowDeleting="grdPAs_RowDeleting">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:BoundField DataField="idPregunta" HeaderText="Id Pregunta" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Pregunta" HeaderText="Pregunta" />
                    <asp:CommandField HeaderText="Seleccionar Pregunta" ShowSelectButton="True">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:CommandField>
                    <asp:CommandField ButtonType="Image" DeleteImageUrl="~/images/close.gif" HeaderText="Eliminar pregunta"
                        ShowDeleteButton="True">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:CommandField>
                </Columns>
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#999999" />
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            </asp:GridView>
            <asp:Label ID="lblIdPregunta" runat="server" Visible="False"></asp:Label>&nbsp;
        </td>
	</tr>
            <tr>
                <td colspan="2" >
                    <asp:TextBox ID="txtPreguntaAdicional" runat="server" Visible="False" Width="480px" Height="131px" TextMode="MultiLine"></asp:TextBox></td>
                <td colspan="1" >
                    <asp:Button ID="btnGuardarYAgregarPA" runat="server" 
                Text="+" OnClick="btnGuardarYAgregarPA_Click" Width="20px" Height="22px" Visible="False" /></td>
            </tr>
	<tr>
		<td >&nbsp;<asp:Label ID="lblObligatorioAdicional" runat="server" Text="¿Es obligatoria?" Visible="False"></asp:Label></td>
		<td>&nbsp;<asp:RadioButton ID="rbtObligatorioAdicionalSi" runat="server" GroupName="PreguntaAdicional"
                Text="Si" Visible="False" />
            <asp:RadioButton ID="rbtObligatorioAdicionalNo" runat="server" Checked="True" GroupName="PreguntaAdicional"
                Text="No" Visible="False" /></td>
	</tr>
</table>
            </ContentTemplate>
            <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnPreguntaAdicional" EventName="Click" />
            </Triggers>
            </asp:UpdatePanel>

            </td>
        <td >
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td >
            </td>
        <td colspan="2">
            </td>
        <td >
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            </td>
    </tr>
    <tr>
        <td colspan="5" align="center" style="height: 21px">
            <asp:Button ID="btnSalvar" runat="server" Text="Guardar Indicador" OnClick="btnSalvar_Click" />
            <asp:Button ID="btnEliminarIndicador" runat="server" Text="Eliminar Indicador" Visible="False" OnClick="btnEliminarIndicador_Click" /></td>
    </tr>
</table>
</div>
<br />
<asp:PlaceHolder ID="visorDelCuestionario" runat="server"></asp:PlaceHolder>
<table border="0" width="100%" id="table3">
	<tr>
		<td align="center">
            <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <EditRowStyle BackColor="#999999" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            </asp:GridView>
        </td>
	</tr>
</table>