<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" Inherits="administradorDeUsuarios" Codebehind="administradorDeUsuarios.aspx.cs" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" 
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" 
    Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>

<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" Runat="Server">
    <link rel="stylesheet" href="css/esr_anterior.css" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderPageTitle" Runat="Server">
Administrador de usuarios
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderMain" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:Panel ID="panMenu" runat="server">        
    </asp:Panel>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
                <img src="images/progress_bar_2_1.gif" />
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
        <asp:Label ID="lblidEmpresa" runat="server" Visible="False"></asp:Label><br />
<table border="0" width="100%" id="table1">
<tr>
      <td colspan="4" style="height: 23px" class="borde_menu"><img src="/images/espacio_transparente.gif" width="40" height="10"></td>
      </tr>
    <tr>
        <td style="height: 21px; width: 216px;">
            Usuarios disponibles</td>
        <td style="height: 21px; width: 308px;">
            <asp:DropDownList ID="ddlUsuarios" runat="server" OnSelectedIndexChanged="ddlUsuarios_SelectedIndexChanged" AutoPostBack="True" >
        </asp:DropDownList></td>
        <td style="height: 21px">
        </td>
        <td style="height: 21px; width: 137px;">
        </td>
    </tr>
	<tr>
		<td style="height: 22px; width: 216px;">
            Nombre</td>
		<td style="width: 308px; height: 22px;">
            <asp:TextBox ID="txtNombre" runat="server" MaxLength="50" Width="220px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombre"
                ErrorMessage="Teclee un nombre válido">*</asp:RequiredFieldValidator></td>
		<td style="height: 22px; color: #333333;"></td>
		<td style="height: 22px; width: 137px; color: #333333;"></td>
	</tr>
	<tr style="color: #333333">
		<td style="width: 216px">
            Apellido Paterno</td>
		<td style="width: 308px">
            <asp:TextBox ID="txtApellidoP" runat="server" MaxLength="50" Width="220px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvApellidoP" runat="server" ControlToValidate="txtApellidoP"
                ErrorMessage="Teclee un Apellido Paterno válido">*</asp:RequiredFieldValidator></td>
		<td style="color: #333333"></td>
		<td style="width: 137px; color: #333333;"></td>
	</tr>
	<tr style="color: #333333">
		<td style="width: 216px">
            Apellido Materno</td>
		<td style="width: 308px">
            <asp:TextBox ID="txtApellidoM" runat="server" MaxLength="50" Width="220px"></asp:TextBox></td>
		<td></td>
		<td style="width: 137px"></td>
	</tr>
	<tr>
		<td style="width: 216px">
            Puesto</td>
		<td style="width: 308px">
            <asp:TextBox ID="txtPuesto" runat="server" MaxLength="50"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvPuesto" runat="server" ControlToValidate="txtPuesto"
                ErrorMessage="Teclee un Puesto válido" SetFocusOnError="True" Width="192px">Ingrese un puesto para el usuario</asp:RequiredFieldValidator></td>
		<td style="color: #333333"></td>
		<td style="width: 137px; color: #333333;"></td>
	</tr>
    <tr style="color: #333333">
        <td style="height: 23px; width: 216px;">
            Teléfono</td>
        <td style="height: 23px; width: 308px;">
            <asp:TextBox ID="txtTelefono" runat="server" MaxLength="50"></asp:TextBox></td>
        <td style="height: 23px">
        </td>
        <td style="height: 23px; width: 137px;">
        </td>
    </tr>
    <tr>
        <td style="width: 216px">
            Extensión</td>
        <td style="width: 308px">
            <asp:TextBox ID="txtExt" runat="server" MaxLength="50"></asp:TextBox></td>
        <td>
        </td>
        <td style="width: 137px">
        </td>
    </tr>
    <tr>
        <td style="width: 216px; height: 38px;">
            Correo electrónico</td>
        <td style="width: 308px; height: 38px;">
            <asp:TextBox ID="txtEmail" runat="server" Enabled="False" MaxLength="50" Width="220px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                ErrorMessage="Teclee un correo válido">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                ErrorMessage="Teclee un correo válido" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">Ingrese un correo válido</asp:RegularExpressionValidator></td>
        <td style="height: 38px">
        </td>
        <td style="width: 137px; height: 38px;">
        </td>
    </tr>
    <tr>
        <td style="width: 216px">
            <asp:Label ID="lblPerfil" runat="server" Text="Perfiles"></asp:Label></td>
        <td style="width: 308px">
            <asp:CheckBoxList ID="cblPerfiles" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cblPerfiles_SelectedIndexChanged">
            </asp:CheckBoxList></td>
        <td>
        </td>
        <td style="width: 137px">
        </td>
    </tr>
    <tr>
        <td style="width: 216px">
            ¿Es contacto primario?</td>
        <td style="width: 308px">
            <asp:RadioButton ID="rbtContactoSi" runat="server" GroupName="contactoPrimario" OnCheckedChanged="rbtContactoSi_CheckedChanged"
                Text="Si" />
            <asp:RadioButton ID="rbtContactoNo" runat="server" GroupName="contactoPrimario" Text="No" /></td>
        <td>
        </td>
        <td style="width: 137px">
        </td>
    </tr>
    <tr>
        <td style="width: 216px; height: 28px;">
            ¿Esta bloqueado?</td>
        <td style="width: 308px; height: 28px;">
            <asp:RadioButton ID="rbtBloqueadoSi" runat="server" GroupName="bloqueado" Text="Si" />
            <asp:RadioButton ID="rbtBloqueadoNo" runat="server" GroupName="bloqueado" Text="No" /></td>
        <td style="height: 28px">
        </td>
        <td style="width: 137px; height: 28px;">
        </td>
    </tr>
    <tr>
        <td style="width: 216px">
            Área del cuestionario asignada</td>
        <td style="width: 308px">
            <asp:CheckBoxList ID="cblTemas" runat="server">
            </asp:CheckBoxList></td>
        <td>
        </td>
        <td style="width: 137px">
        </td>
    </tr>
    <tr>
        <td style="width: 216px">
            </td>
        <td style="width: 308px">
            <asp:TextBox ID="txtPwd" runat="server" TextMode="Password" Visible="False"></asp:TextBox></td>
        <td>
        </td>
        <td style="width: 137px">
        </td>
    </tr>
    <tr>
        <td style="width: 216px">
            </td>
        <td style="width: 308px">
            </td>
        <td>
        </td>
        <td style="width: 137px">
        </td>
    </tr>
    <tr>
        <td style="width: 216px">
        </td>
        <td style="width: 308px">
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click"/>
            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" OnClick="btnEliminar_Click" /></td>
        <td>
        </td>
        <td style="width: 137px">
        </td>
    </tr>
</table>
            </ContentTemplate>
        </asp:UpdatePanel>
        
</asp:Content>