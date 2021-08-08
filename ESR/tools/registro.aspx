<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="registro.aspx.cs" Inherits="ESR.tools.registro" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Registro de usuario</title>
    <link href="../css/esr_anterior.css" rel="stylesheet" type="text/css" />
</head>
<body background="../images/ESRhorizontal_color.png" 
style="background-repeat: no-repeat; background-position: center top; background-attachment: fixed; font-family: Verdana; font-size: x-small; color:Black;">
    <form id="form1" runat="server">
<div id="infcontacto" runat="server">
<table border="0" cellspacing="1" width="850px" align="center">
    <tr>
        <td colspan="3" style="height: 26px">
            <p>
                <asp:ToolkitScriptManager ID="ScriptManager1" runat="server">
                </asp:ToolkitScriptManager>
            </p>
            <p>&nbsp;</p>
            <p>&nbsp;</p>
            <p>&nbsp;</p>
            <p>&nbsp;</p>
            <p>&nbsp;</p>
            <p>&nbsp;</p>
            <p>&nbsp;</p>
            
            </td>
    </tr>
    <tr>
        <td colspan="3" style="height: 26px">
            <br />
            Llene los campos con la información solicitada</td>
    </tr>
	<tr>
		<td height="24" style="width: 214px">
            Nombre</td>
		<td height="24" colspan="2">
            <asp:TextBox ID="txtContactoNombre" runat="server" MaxLength="50" Width="300px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" 
                ControlToValidate="txtContactoNombre" 
                ErrorMessage="El nombre del usuario es requerido" SetFocusOnError="True" 
                ForeColor="Red">*</asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender ID="RequiredFieldValidator15_ValidatorCalloutExtender" 
                runat="server" Enabled="True" TargetControlID="RequiredFieldValidator15" 
                CssClass="ValidationExt">
            </asp:ValidatorCalloutExtender>
        </td>
	</tr>
    <tr>
        <td height="24" style="width: 214px">
            Apellido Paterno</td>
        <td height="24" colspan="2">
            <asp:TextBox ID="txtContactoApellidoP" runat="server" MaxLength="50" Width="300px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" 
                ControlToValidate="txtContactoApellidoP" 
                ErrorMessage="Apellido Paterno requerido" SetFocusOnError="True" 
                ForeColor="Red">*</asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender ID="RequiredFieldValidator16_ValidatorCalloutExtender" 
                runat="server" Enabled="True" TargetControlID="RequiredFieldValidator16" 
                CssClass="ValidationExt">
            </asp:ValidatorCalloutExtender>
        </td>
    </tr>
    <tr>
        <td style="width: 214px; height: 24px">
            Apellido Materno</td>
        <td style="height: 24px" colspan="2">
            <asp:TextBox ID="txtContactoApellidoM" runat="server" MaxLength="50" Width="300px"></asp:TextBox>
            </td>
    </tr>
    <tr>
        <td height="24" style="width: 214px">
            País</td>
        <td height="24" colspan="2">
            <asp:DropDownList ID="ddlPais" runat="server">
            </asp:DropDownList>
            <asp:RangeValidator ID="rvPais" runat="server" ControlToValidate="ddlPais" 
                ErrorMessage="Seleccione un país de la lista" MinimumValue="1" 
                SetFocusOnError="True" MaximumValue="243" Type="Integer" ForeColor="Red">*</asp:RangeValidator>
        </td>
    </tr>
    <tr>
        <td height="24" style="width: 214px">
            Teléfono</td>
        <td height="24" colspan="2">
            <asp:TextBox ID="txtContactoTelefono" runat="server" MaxLength="50"></asp:TextBox>Ext.<asp:TextBox ID="txtContactoExt"
                runat="server" MaxLength="50"></asp:TextBox></td>
    </tr>
    <tr>
        <td height="24" style="width: 214px">
            Email</td>
        <td height="24" colspan="2">
            <asp:TextBox ID="txtContactoEmail" runat="server" MaxLength="50" Width="300px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" 
                ControlToValidate="txtContactoEmail" 
                ErrorMessage="Correo electrónico requerido" SetFocusOnError="True" 
                ForeColor="Red">*</asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender ID="RequiredFieldValidator19_ValidatorCalloutExtender" 
                runat="server" Enabled="True" TargetControlID="RequiredFieldValidator19" 
                CssClass="ValidationExt">
            </asp:ValidatorCalloutExtender>
            <br />
            <asp:RegularExpressionValidator ID="revContactoEmail" runat="server" ControlToValidate="txtContactoEmail"
                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                SetFocusOnError="True" ForeColor="Red">Introduzca una cuenta de correo válida</asp:RegularExpressionValidator>
            <asp:ValidatorCalloutExtender ID="revContactoEmail_ValidatorCalloutExtender" 
                runat="server" Enabled="True" TargetControlID="revContactoEmail" 
                CssClass="ValidationExt">
            </asp:ValidatorCalloutExtender>
        </td>
    </tr>
    <tr>
        <td height="24" style="width: 214px">
            Capture nuevamente su email</td>
        <td height="24" colspan="2">
            <asp:TextBox ID="txtContactoEmail2" runat="server" MaxLength="50" Width="300px"></asp:TextBox>
            <asp:CompareValidator ID="cvEmail" runat="server" 
                ControlToCompare="txtContactoEmail" ControlToValidate="txtContactoEmail2" 
                ErrorMessage="La cuenta de correo no coincide, por favor verifique." 
                SetFocusOnError="True" ForeColor="Red">*</asp:CompareValidator>
            <asp:ValidatorCalloutExtender ID="cvEmail_ValidatorCalloutExtender" 
                runat="server" Enabled="True" TargetControlID="cvEmail" 
                CssClass="ValidationExt">
            </asp:ValidatorCalloutExtender><a></a>
        </td>
    </tr>
    <tr>
        <td style="height: 24px">
            &nbsp;</td>
        <td style="height: 24px">
            <asp:Button ID="btnAceptar" runat="server" onclick="btnAceptar_Click" 
                Text="Aceptar" Height="20px" />
            <input id="btnCancelar" type="button" value="Regresar" 
                onclick="history.back();" /></td>
        <td style="height: 24px">
        </td>
    </tr>
    <tr>
        <td colspan="3" style="height: 24px">
            <strong>
                <asp:Label ID="lblNotaPie" runat="server" 
                Text="Su registro ha sido completado. En las próximas 24 horas recibirá en el correo que indicó, el nombre de usuario y su contraseña. En caso de no recibir el correo, utilice la herramienta de recuperación de contraseña, en la pantalla de acceso de la aplicacion." 
                Visible="False"></asp:Label></strong></td>
    </tr>
		</table>
</div>
    </form>
</body>
</html>
