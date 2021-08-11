<%@ Control Language="C#" AutoEventWireup="true" Inherits="ESR.tools.usuario" MasterPageFile="~/oslo.Master" Codebehind="usuario.ascx.cs" %>
<link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
<table border="0" width="100%" id="table3" style="background-color: transparent">
<tr>
      <td colspan="4" style="height: 23px" class="borde_menu"><img src="/images/espacio_transparente.gif" width="40" height="10"></td>
      </tr>
    <tr>
        <td colspan="2" style="height: 26px">
            <h3>
                Inscripción a ESR</h3></td>
        <td colspan="2" align="right" style="height: 26px">
            </td>
    </tr>
	<tr>
		<td height="24" style="width: 214px">
            Nombre</td>
		<td height="24">
            <asp:TextBox ID="txtContactoNombre" runat="server" MaxLength="50" Width="300px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtContactoNombre" ErrorMessage="Nombre requerido">*</asp:RequiredFieldValidator></td>
		<td height="24">&nbsp;</td>
		<td height="24">&nbsp;</td>
	</tr>
    <tr>
        <td height="24" style="width: 214px">
            Apellido Paterno</td>
        <td height="24">
            <asp:TextBox ID="txtContactoApellidoP" runat="server" MaxLength="50" Width="300px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtContactoApellidoP" ErrorMessage="Apellido Paterno requerido">*</asp:RequiredFieldValidator></td>
        <td height="24">
        </td>
        <td height="24">
        </td>
    </tr>
    <tr>
        <td style="width: 214px; height: 24px">
            Apellido Materno</td>
        <td style="height: 24px">
            <asp:TextBox ID="txtContactoApellidoM" runat="server" MaxLength="50" Width="300px"></asp:TextBox>
            </td>
        <td style="height: 24px">
        </td>
        <td style="height: 24px">
        </td>
    </tr>
    <tr>
        <td height="24" style="width: 214px">
            Puesto</td>
        <td height="24">
            <asp:TextBox ID="txtContactoPuesto" runat="server" MaxLength="50" Width="300px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtContactoPuesto" ErrorMessage="Puesto requerido">*</asp:RequiredFieldValidator></td>
        <td height="24">
        </td>
        <td height="24">
        </td>
    </tr>
    <tr>
        <td height="24" style="width: 214px">
            Teléfono</td>
        <td height="24">
            <asp:TextBox ID="txtContactoTelefono" runat="server" MaxLength="50"></asp:TextBox>Ext.<asp:TextBox ID="txtContactoExt"
                runat="server" MaxLength="50"></asp:TextBox></td>
        <td height="24">
        </td>
        <td height="24">
        </td>
    </tr>
    <tr>
        <td height="24" style="width: 214px">
            Correo Electrónico</td>
        <td height="24">
            <asp:TextBox ID="txtContactoEmail" runat="server" MaxLength="50" Width="300px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtContactoEmail" ErrorMessage="Correo electrónico requerido">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revContactoEmail" runat="server" ControlToValidate="txtContactoEmail"
                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">Introduzca una cuenta de correo válida</asp:RegularExpressionValidator></td>
        <td height="24">
        </td>
        <td height="24">
        </td>
    </tr>
    <tr>
        <td colspan="4" style="height: 24px">
            <asp:ValidationSummary ID="ValidationSummary3" runat="server" />
        </td>
    </tr>
    <tr>
        <td colspan="4" style="height: 24px">
            <strong>
                <asp:Label ID="lblNotaPie" runat="server" Text="Una vez completada esta información haga clik en CREAR MI CUENTA. En las próximas 24 horas recibirá en el correo que indicó, el nombre de usuario y su contraseña. En caso de no recibir el correo, utilice la herramienta de recuperación de contraseña, en la pantalla de acceso de la aplicacion."></asp:Label></strong></td>
    </tr>
		<tr>
		<td colspan="4" align="center">
        <asp:Button ID="btnFinalizar" runat="server" Text="Crear mi cuenta" OnClick="btnFinalizar_Click" Width="135px" /></td>
	</tr>
</table>