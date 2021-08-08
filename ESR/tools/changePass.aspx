<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="changePass.aspx.cs" Inherits="ESR.tools.changePass" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Reestablecimiento de contraseña</title>
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
              <div align="center"><strong> Reestablecimiento de contraseña</strong></div></td>
	  </tr>
      <tr>
          <td>
              <div align="right">Introduzca su nueva contraseña:</div></td>
          <td>
            <div align="left">
                  <asp:TextBox ID="txtPass" runat="server" MaxLength="20" TextMode="Password" 
                      Width="180px"></asp:TextBox>
              <asp:RequiredFieldValidator
                ID="rfvPass" runat="server" ControlToValidate="txtPass" ForeColor="Red">*</asp:RequiredFieldValidator>
                </div></td>
      </tr>
      <tr>
          <td>
          <div align="right">
             Confirme su nueva contraseña:</div>
             </td>
          <td>
            <div align="left">
                  <asp:TextBox ID="txtConfirm" runat="server" MaxLength="20" TextMode="Password" 
                      Width="180px"></asp:TextBox>
              <asp:RequiredFieldValidator
                ID="rfvConfirm" runat="server" ControlToValidate="txtConfirm" 
                      ErrorMessage="Confirme su contraseña" ForeColor="Red">*</asp:RequiredFieldValidator>
                  <br /><asp:CompareValidator ID="cvConfirm" runat="server" ControlToCompare="txtPass" 
                      ControlToValidate="txtConfirm" ErrorMessage="La contraseña no coincide. Por favor verifique." 
                      ForeColor="Red"></asp:CompareValidator>
              <br />
                </div></td>
      </tr>
      <tr>
          <td>        <div align="left"></div></td>
          <td>
            <div align="left">
                  <asp:Label ID="lblMensaje" runat="server"></asp:Label>
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
