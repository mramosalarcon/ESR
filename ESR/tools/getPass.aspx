<%@ Page Language="C#" AutoEventWireup="true" Inherits="getPass" Codebehind="getPass.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Recuperación de contraseña</title>
    <link href="../css/esr_anterior.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">

      <div align="center">
  <table border="0" width="750" id="table1">
	  <tr align="center" valign="top">
	    <td colspan="2"><div align="left"></div>	      
        <div align="center"><br />
          <br />
        <img src="/images/ESR_header.gif" width="211" height="71" /></div></td>
      </tr>
	  <tr>
		  <td colspan="2">
              <div align="center"><strong> Recuperación de contraseña</strong></div></td>
	  </tr>
	  <tr>
		  <td colspan="2">
              <div align="center">Por favor introduzca la cuenta de correo de registro</div></td>
	  </tr>
      <tr>
          <td width="27%">        <div align="left"></div></td>
          <td width="73%">        <div align="left"></div></td>
      </tr>
      <tr>
          <td>
              <div align="left">Cuenta de correo del contacto:</div></td>
          <td>
            <div align="left">
                  <asp:TextBox ID="txtCorreo" runat="server" MaxLength="50" Width="300px"></asp:TextBox>
              <asp:RequiredFieldValidator ID="rfvEmail" runat="server" 
                      ControlToValidate="txtEmail" 
                      ErrorMessage="Introduzca una cuenta de correo válida" ForeColor="Red">*</asp:RequiredFieldValidator>
              <br />
              <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                ErrorMessage="Introduzca una cuenta de correo válida" 
                      ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                      ForeColor="Red">Introduzca una cuenta de correo válida</asp:RegularExpressionValidator>
                </div></td>
      </tr>
      <tr>
          <td>        <div align="left"></div></td>
          <td>
            <div align="left">
                  <asp:Label ID="lblPwd" runat="server"></asp:Label>
                </div></td>
      </tr>
      <tr>
          <td>        <div align="left"></div></td>
          <td>
            <div align="left">
                  <asp:Button ID="btnAceptar" runat="server" OnClick="btnAceptar_Click" Text="Aceptar" />                              
            <input id="btnCancelar" type="button" value="Cancelar" onclick="history.back();" />
        </div></td>
      </tr>
  </table>
      </div>
    </form>
</body>
</html>
