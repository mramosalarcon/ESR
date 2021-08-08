<%@ Assembly Name="Microsoft.SharePoint.IdentityModel, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Assembly Name="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" Inherits="login" Codebehind="login.aspx.cs" Debug="true" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    <SharePoint:EncodedLiteral runat="server" EncodeMethod="HtmlEncode" ID="ClaimsFormsPageTitle" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    <SharePoint:EncodedLiteral runat="server" EncodeMethod="HtmlEncode" ID="ClaimsFormsPageTitleInTitleArea" />
</asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <SharePoint:EncodedLiteral runat="server" EncodeMethod="HtmlEncode" ID="ClaimsFormsPageMessage" />

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
    <title>Acceso Autodiagnóstico ESR</title>
    <link rel="SHORTCUT ICON" href="images/favicon.ico"  type="image/x-icon" />
    <link rel="stylesheet" href="css/esr_anterior.css" type="text/css" />
 <style type="text/css">
        .style1
        {
            width: 300px;
        }
    </style>
</head>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />

<body onload="frmLogin.UserName.focus()"
style="background-repeat: no-repeat; background-position: center top; background-attachment: fixed; color: #000000; font-family: Verdana; font-size: x-small;">
<form id="frmLogin" runat="server">
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
<table width="50%" align="center">
<tr>
        <td align="center" colspan="2">
                                 <a href="http://esr.cemefi.org">
                                <img alt="Distintivo Empresa Socialmente Responsable" 
                                    src="images/ESRhorizontal_color.png" style="border-width: 0px" /></a></td>
    </tr>
    <tr>
        <td align="right" class="style1">
                                &nbsp;</td>
        <td align="left">
                                &nbsp;</td>
    </tr>
    <tr>
        <td align="right" class="style1">
                                <asp:Label ID="lblUser" runat="server" Text="Correo electrónico:"></asp:Label>
        </td>
        <td align="left">
                                <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvUserName" runat="server" 
                                    ErrorMessage="*" ControlToValidate="UserName" ForeColor="Red">*</asp:RequiredFieldValidator>
                                <br />
                                <asp:RegularExpressionValidator ID="revUserName" runat="server" 
                                    ControlToValidate="UserName" 
                                    ErrorMessage="*" 
                                    SetFocusOnError="True" 
                                    
                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">Debe ingresar una cuenta de correo válida</asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td align="right" class="style1">
                                <asp:Label ID="lblPassword" runat="server" Text="Contraseña:"></asp:Label>
                            </td>
        <td align="left">
                                <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox><br />
                                <asp:Label ID="lblBadPwd" runat="server" ForeColor="Red"></asp:Label><br />
                                Si no cuenta con acceso, primero <a href="tools/registro.aspx">regístrese</a>.<br />
                                Si olvidó su contraseña de clic <a href="tools/regPass.aspx">aquí</a>.
                                </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
                                <asp:Button ID="btnAccederESR" runat="server" Text="Acceder ESR" 
                                    onclick="btnAccederESR_Click" />
                            </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="2" align="center">
            <i>Aviso de Confidencialidad: El CEMEFI asegura absoluta confidencialidad sobre 
            la participación de las compañías en el proceso de llenado de los cuestionarios 
            y sobre la información que incluyan en ellos. El CEMEFI sólo accederá a las 
            respuestas para procesamiento estadístico después que las compañías liberen el 
            cuestionario para revisión.</i></td>
    </tr>
</table>
         </form>
    </body>
</html>
</asp:Content>
