<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="_unsubscribe.aspx.cs" Inherits="ESR.tools._unsubscribe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Remover de lista de distribución</title>
    <link href="../css/esr_anterior.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <table border="0" width="100%" id="table1">
	  <tr align="center" valign="top">
	    <td align="center">	      
            <img src="/images/ESR_header.gif" width="211" height="71" />
        </td>
      </tr>
      <tr>
      <td align="center">
      <div>
        <h2>&nbsp;</h2>
          <h2>Gracias por compartir con nosotros la experiencia RSE.</h2>
          
        <h3><asp:Label ID="lblMensaje" runat="server" Text="Tu correo ha sido removido de nuestra lista de distribución."></asp:Label></h3></div>
      </td>
      </tr>
    </table>
    </form>
</body>
</html>
