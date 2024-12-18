<%@ Page Language="C#" AutoEventWireup="true" Inherits="ESR.tools.regPass" Codebehind="regPass.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Recuperaci�n de contrase�a</title>
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
              <div align="center"><h2>Recuperaci�n de contrase�a</h2></div></td>
	  </tr>
	  <tr>
		  <td colspan="2">
              <div align="center"><h3>Por favor introduzca la cuenta de correo de registro</h3></div></td>
	  </tr>
      <tr>
          <td width="27%">        <div align="left"></div></td>
          <td width="73%">        <div align="left"></div></td>
      </tr>
      <tr>
          <td>
              <div align="left"><h4>Cuenta de correo del contacto:</h4></div></td>
          <td>
            <div align="left">
                <asp:TextBox  ID="txtEmail" runat="server" MaxLength="50" Width="300px"></asp:TextBox>
              <asp:RequiredFieldValidator ID="rfvEmail" runat="server" 
                      ControlToValidate="txtEmail" 
                      ErrorMessage="Introduzca una cuenta de correo v�lida" ForeColor="Red">*</asp:RequiredFieldValidator>
              <br />
              <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                ErrorMessage="Introduzca una cuenta de correo v�lida" 
                      ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                      ForeColor="Red">Introduzca una cuenta de correo v�lida</asp:RegularExpressionValidator>
                </div></td>
      </tr>
      <tr>
          <td>        <div align="left"></div></td>
          <td>
            <div align="left">
                <asp:Label ID="lblMensaje" runat="server" Text="Label"></asp:Label>
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

