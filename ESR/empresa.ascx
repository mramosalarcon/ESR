<%@ Control Language="C#" AutoEventWireup="true" Inherits="empresa" Codebehind="empresa.ascx.cs" %>
<%@ Register Assembly="FUA" Namespace="Subgurim.Controles" TagPrefix="cc1" %>
<style type="text/css">
    .validator {     
        color: #FF0000;
	forecolor: Red; 
    } 
    .required {
	color: Red;
        forecolor: Red; 
    }
</style>  

<div id="infgeneral" visible="true" runat="server">
    <table border="0" width="100%" id="table1" style="background-color: transparent">

    <tr>
      <td colspan="4" style="height: 23px" class="borde_menu"><img src="/images/espacio_transparente.gif" width="40" height="10"></td>
      </tr>
    <tr>
        <td colspan="2" style="height: 23px">
            <h3>
                Registro de empresa</h3>
            <asp:TextBox ID="txtidEmpresa" runat="server" Visible="False"></asp:TextBox>
            <asp:Label ID="lblError" runat="server"></asp:Label></td>
        <td colspan="2" align="right" style="height: 23px">
            <asp:Button ID="btnSiguiente0" runat="server" Text="Siguiente >" OnClick="btnSiguiente0_Click" Width="110px" OnClientClick="_spFormOnSubmitCalled = false;"
 /></td>
    </tr>
    <tr>
        <td style="width: 214px; height: 54px;">
            <asp:Label ID="lblAutorizada" runat="server" Text="¿Esta empresa está autorizada para contestar cuestionarios?" Visible="False"></asp:Label></td>
        <td width="346" style="height: 54px; width: 347px;">
            <asp:UpdatePanel ID="upAutorizada" runat="server" Visible="False">
                <ContentTemplate>
                <table border="0" width="100%" id="table6">
	<tr>
		<td><asp:RadioButton ID="rbtAutorizadaSi" runat="server" GroupName="Autorizada" 
                Text="Si" Checked="True" />
            <asp:RadioButton ID="rbtAutorizadaNo" runat="server" GroupName="Autorizada" Text="No" /></td>
		<td>
            <asp:CheckBoxList ID="cblCuestionarios" runat="server">
            </asp:CheckBoxList></td>
	</tr>
	<tr>
		<td>&nbsp;</td>
		<td>&nbsp;</td>
	</tr>
</table>
</ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td width="274" style="height: 54px">
        </td>
        <td width="252" style="width: 252px; height: 54px;">
        </td>
    </tr>
    <tr>
        <td>
            Razón social</td>
        <td>
            <asp:TextBox ID="txtRazonSocial" runat="server" MaxLength="100" Width="300px"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator
            ID="rfvRazonSocial" runat="server" ControlToValidate="txtRazonSocial" ErrorMessage="Razón social requerida" CssClass="required">*</asp:RequiredFieldValidator>
            </td>
        <td>
            Nombre corto:</td>
        <td>
            <asp:TextBox ID="txtNombreCorto" runat="server" MaxLength="50" Width="200px"></asp:TextBox><asp:RequiredFieldValidator ID="rfvNombreCorto" runat="server" ControlToValidate="txtNombreCorto" ErrorMessage="Nombre Corto requerido" CssClass="required">*</asp:RequiredFieldValidator></td>
    </tr>
	<tr>
		<td>Nombre de la empresa como desea que aparezca en la placa del distintivo ESR®</td>
		<td>
            <asp:TextBox ID="txtNombreEmpresa" runat="server" MaxLength="100" Width="300px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombreEmpresa"
                ErrorMessage="Nombre de empresa requerido" CssClass="required">*</asp:RequiredFieldValidator>    
		</td>
		<td>Siglas o acrónimo</td>
		<td>
            <asp:TextBox ID="txtSiglas" runat="server" MaxLength="20"></asp:TextBox><span style="color: blue"></span></td>
	</tr>
	    <tr>
		<td>
            RFC(Mexico) / RUC(Ecuador)</td>
		<td>
            <asp:TextBox ID="txtRFC" runat="server" MaxLength="20"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvRFC" runat="server" ControlToValidate="txtRFC"
                ErrorMessage="RFC Requerido" CssClass="required">*</asp:RequiredFieldValidator></td>
		<td></td>
		<td></td>
	    </tr>
	<tr>
		<td>
            Calle y número</td>
		<td>
            <asp:TextBox ID="txtDomicilio" runat="server" MaxLength="50" Width="300px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDomicilio" ErrorMessage="Calle y número requerido" CssClass="required">*</asp:RequiredFieldValidator></td>
		<td>
            Teléfono(s)
        </td>
		<td>
            <asp:TextBox ID="txtTelefonos" runat="server" MaxLength="50"></asp:TextBox>
            </td>
	</tr>
    <tr>
        <td>
            Colonia</td>
        <td>
            <asp:TextBox ID="txtColonia" runat="server" MaxLength="50" Width="300px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtColonia" ErrorMessage="Colonia requerida" CssClass="required">*</asp:RequiredFieldValidator></td>
        <td>
            Fax</td>
        <td>
            <asp:TextBox ID="txtFax" runat="server" MaxLength="50"></asp:TextBox>
            </td>
    </tr>
    <tr>
        <td>
            País</td>
        <td>
            <asp:DropDownList ID="ddlPais" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPais_SelectedIndexChanged">
            </asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlPais" ErrorMessage="País requerido" CssClass="required">*</asp:RequiredFieldValidator></td>
        <td>
            Correo electrónico de información 
            general:</td>
        <td>
            <asp:TextBox ID="txtEmail" runat="server" MaxLength="100"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                ErrorMessage="Introduzca una cuenta de correo válida" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td>
            Estado/Provincia</td>
        <td>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
            <asp:DropDownList ID="ddlEstado" runat="server" OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged" Visible="False" AutoPostBack="True">
            </asp:DropDownList>
                    <asp:TextBox ID="txtEstado" runat="server" Visible="False" MaxLength="50"></asp:TextBox>
                </ContentTemplate>
                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlPais" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            Ciudad</td>
        <td>
            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                <ContentTemplate>
            <asp:TextBox ID="txtCiudad" runat="server" MaxLength="50"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtCiudad" ErrorMessage="Ciudad requerida" CssClass="required">*</asp:RequiredFieldValidator>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td>
            Sitio web</td>
        <td>
            http://www.<asp:TextBox ID="txtWWW" runat="server" MaxLength="100"></asp:TextBox></td>
    </tr>
    <tr>
        <td>
            Código Postal</td>
        <td>
            <asp:TextBox ID="txtCP" runat="server" MaxLength="5"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtCP" ErrorMessage="Código Postal requerido" CssClass="required">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revCP" runat="server" ControlToValidate="txtCP"
                ErrorMessage="Introduzca un CP válido" ValidationExpression="\d{5}|\d{2}"></asp:RegularExpressionValidator>
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
	<tr>
		<td>Logo de la companía</td>
		<td>
            <asp:Image ID="imgLogo" Width="50%" runat="server"/><br />
            <asp:FileUpload ID="fuLogo" runat="server" /><br />
            (La imagen no debe ser mas grande que 1024 kb)</td>
		<td>
		</td>
		<td>
        </td>
	</tr>
        <tr>
            <td>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                    ForeColor="Red" />
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
</table>
</div>
<div id="infadicional" runat="server">
<table border="0" width="100%" id="table2" style="background-color: transparent">	
    <tr>
        <td colspan="2">
            Información adicional de la compañía</td>
        <td align="right">
        <asp:Button ID="btnAnterior1" runat="server" Text="< Anterior" OnClick="btnAnterior1_Click" Width="110px" OnClientClick="_spFormOnSubmitCalled = false;"
/></td>
        <td align="right">
            <asp:Button ID="btnFinalizar" runat="server" Text="Finalizar" OnClick="btnFinalizar_Click" Width="110px" OnClientClick="_spFormOnSubmitCalled = false;"
/></td>
    </tr>
	<tr>
		<td>Presidente ejecutivo o Director General</td>
		<td>
            <asp:TextBox ID="txtDirector" runat="server" MaxLength="50" Width="250px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDirector"
                ErrorMessage="Nombre del presidente requerido" CssClass="required">*</asp:RequiredFieldValidator></td>
		<td></td>
		<td></td>
	</tr>
	<tr>
		<td>Presidente del consejo de administración</td>
		<td>
            <asp:TextBox ID="txtPresidente" runat="server" MaxLength="50" Width="250px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtPresidente" ErrorMessage="Nombre del presidente del consejo de administración requerido" CssClass="required">*</asp:RequiredFieldValidator></td>
		<td></td>
		<td></td>
	</tr>
	<tr>
		<td></td>
		<td></td>
		<td></td>
		<td></td>
	</tr>
	<tr>
		<td>Tamaño</td>
		<td>
            <asp:Label ID="lblTamano" runat="server"></asp:Label>
        </td>
        <td>
        </td>
        <td>
        </td>
	</tr>
	<tr align="left" valign="middle">
		<td>Sector</td>
		<td>
            <asp:DropDownList ID="ddlSector" runat="server" OnSelectedIndexChanged="ddlSector_SelectedIndexChanged" AutoPostBack="True" Width="150px">
            </asp:DropDownList>
            <asp:RangeValidator ID="rvSector" runat="server" ControlToValidate="ddlSector" ErrorMessage="Seleccione un sector"
                MinimumValue="1" Type="Integer" MaximumValue="100">*</asp:RangeValidator></td>
		<td>
            
        </td>
		<td>
                </td>
	</tr>
	<tr>
		<td>Subsector</td>
		<td>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="ddlSubsector" runat="server">
                    </asp:DropDownList>
                    <asp:RangeValidator ID="rvSubsector" runat="server" 
                        ControlToValidate="ddlSubsector" ErrorMessage="Seleccione un subsector" 
                        MaximumValue="100" MinimumValue="1" Type="Integer">*</asp:RangeValidator>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlSector" 
                        EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
		<td>
            </td>
		<td></td>
	</tr>
	<tr>
		<td>No. total de empleados</td>
		<td>
            <asp:TextBox ID="txtNoEmpleados" runat="server" MaxLength="12"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvNoEmpleados" runat="server" 
                ControlToValidate="txtNoEmpleados" 
                ErrorMessage="Ingrese el número de empleados de su organización" CssClass="required">*</asp:RequiredFieldValidator>
        </td>
		<td>
            &nbsp;</td>
		<td>&nbsp;</td>
	</tr>
	<tr>
		<td>Año en que inició operaciones</td>
		<td>
            <asp:TextBox ID="txtAnoInicio" runat="server" MaxLength="4"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtAnoInicio" ErrorMessage="Año en que inició operaciones requerido" CssClass="required">*</asp:RequiredFieldValidator>
            <asp:RangeValidator ID="rvAnio" runat="server" ControlToValidate="txtAnoInicio" ErrorMessage="RangeValidator"
                MaximumValue="2099" MinimumValue="1800" SetFocusOnError="True" 
                Type="Integer">Ingrese un valor válido para el año</asp:RangeValidator></td>
		<td>
            &nbsp;</td>
		<td>&nbsp;</td>
	</tr>
	<tr>
		<td>Composición de capital</td>
		<td>
            <asp:DropDownList ID="ddlCapital" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCapital_SelectedIndexChanged" Width="150px">
                <asp:ListItem Value="0">Seleccione...</asp:ListItem>
                <asp:ListItem Value="Extranjero">Extranjero</asp:ListItem>
                <asp:ListItem Value="Nacional">Nacional</asp:ListItem>
                <asp:ListItem Value="Mixto">Mixto</asp:ListItem>
                <asp:ListItem Value="Otro">Otro (especifique)</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlCapital" ErrorMessage="Composición de capital requerido" CssClass="required">*</asp:RequiredFieldValidator></td>
		<td>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <asp:TextBox ID="txtCapitalOtro" runat="server" Visible="False" MaxLength="50"></asp:TextBox>&nbsp;
                </ContentTemplate>
                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlCapital" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
		<td></td>
	</tr>
    <tr>
        <td>
            Organismo de referencia</td>
        <td>
            <asp:DropDownList ID="ddlPostulante" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPostulante_SelectedIndexChanged" Width="150px">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlPostulante" ErrorMessage="Organismo de referencia requerido" CssClass="required">*</asp:RequiredFieldValidator></td>
        <td>
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <asp:TextBox ID="txtPostulanteOtro" runat="server" Visible="False"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvOtroPostulante" runat="server" ControlToValidate="txtPostulanteOtro"
                        ErrorMessage="Otro organismo de referencia requerido" CssClass="required">*</asp:RequiredFieldValidator>
                </ContentTemplate>
                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlPostulante" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td>
        </td>
    </tr>
	<tr style="background-color: #808080">
		<td>
            Pertenece a la cadena de valor de</td>
		<td>            
            <asp:DropDownList ID="ddlEmpresaReferencia" runat="server" Visible="True" Width="350px">
            </asp:DropDownList>
        </td>
		<td></td>
		<td></td>
	</tr>
	<tr>
		<td>Nombre de su principal producto o servicio</td>
		<td>
            <asp:TextBox ID="txtNombreProducto" runat="server" MaxLength="50" Width="400px"></asp:TextBox></td>
		<td></td>
		<td></td>
	</tr>
	<tr>
		<td colspan="4">Principales productos y/o servicios:<br />
		<asp:TextBox ID="txtPrincipalesProductos" runat="server" TextMode="MultiLine" Width="600px" Height="80px" MaxLength="500"></asp:TextBox></td>
	</tr>
	<tr>
		<td colspan="4">¿Como define la responsabilidad social de la 
		empresa?<br />
		<asp:TextBox ID="txtRSE" runat="server" TextMode="MultiLine" Width="600px" 
                MaxLength="900" Height="80px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtRSE" ErrorMessage="RSE requerida" CssClass="required">*</asp:RequiredFieldValidator></td>
	</tr>
	<tr>
		<td>Nombre de la fundación empresarial (si 
		cuenta con ella)</td>
		<td>
            <asp:TextBox ID="txtFundacion" runat="server" MaxLength="50"></asp:TextBox></td>
		<td colspan="2">
        </td>
	</tr>
	<tr>
		<td>Ámbito o causa en la que se desarrolla</td>
		<td>
            <asp:TextBox ID="txtAmbito" runat="server" MaxLength="50"></asp:TextBox></td>
		<td></td>
		<td></td>
	</tr>
    <tr>
        <td>
            <asp:ValidationSummary ID="ValidationSummary2" runat="server" 
                ForeColor="Red" />
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
	<tr>
		<td colspan="2">Reconocimientos o certificaciones que ha 
		recibido la empresa:</td>
		<td></td>
		<td></td>
	</tr>
    <tr>
        <td colspan="4">
            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                <ContentTemplate>
                <center>
                    <asp:GridView ID="grdCertificaciones" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDeleting="grdCertificaciones_RowDeleting" DataKeyNames="idCertificacion" OnRowEditing="grdCertificaciones_RowEditing" OnRowCancelingEdit="grdCertificaciones_RowCancelingEdit" OnRowUpdating="grdCertificaciones_RowUpdating">
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            <asp:TemplateField AccessibleHeaderText="A&#241;o" HeaderText="A&#241;o">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Anio") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Anio") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField AccessibleHeaderText="Instituci&#243;n" HeaderText="Instituci&#243;n">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("institucion") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("institucion") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField AccessibleHeaderText="Certificaci&#243;n" HeaderText="Certificaci&#243;n">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("certificacion") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("certificacion") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/images/DELETE.GIF" HeaderText="Eliminar"
                                ShowDeleteButton="True" >
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:CommandField>
                            <asp:CommandField HeaderText="Modificar" ShowEditButton="True" ButtonType="Image" EditImageUrl="~/images/gridEditar.gif" CancelImageUrl="~/images/failed.gif" UpdateImageUrl="~/images/gridActulizar.gif" >
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:CommandField>
                        </Columns>
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <EditRowStyle BackColor="#999999" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    </asp:GridView>
                </center>
<table border="0" width="100%" id="table4">
    <tr>
		<td>Año</td>
		<td>Institución que otorga</td>
		<td>Premio, reconocimiento o certificación</td>
		<td></td>
	</tr>
	<asp:Label ID="lblCertificaciones" runat="server"></asp:Label><tr>
		<td><asp:TextBox ID="txtRecoAno" runat="server" MaxLength="4"></asp:TextBox></td>
		<td><asp:TextBox ID="txtRecoInstitucion" runat="server" MaxLength="50"></asp:TextBox></td>
		<td><asp:TextBox ID="txtReco" runat="server" MaxLength="50"></asp:TextBox></td>
        <td>
            <asp:Button ID="btnMasCertificaciones" runat="server" Text="Agregar Certificación" OnClick="btnMasCertificaciones_Click" OnClientClick="_spFormOnSubmitCalled = false;"
/></td>
	</tr>
</table>             
            
            
            
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
    <td colspan="4" align="center">
    <asp:Label ID="lblNotaPie" runat="server" Text="Una vez completada esta información haga clic en el botón FINALIZAR para guardar la información de la empresa."></asp:Label>
    </td>
    </tr>
</table>
</div>



