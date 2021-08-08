<%@ Page Language="C#" AutoEventWireup="true" Inherits="error" Codebehind="error.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Error</title>
    <link href="../css/esr_anterior.css" rel="stylesheet" type="text/css" />    
</head>
<body>
    <form id="form1" runat="server">
        &nbsp;<div>
        <center>
            <img src="/images/ESR_header.gif"width="211" height="71"/><br />
        </center>

        <center>
        <asp:Label ID="lblErrorMessage" runat="server"></asp:Label>&nbsp;</center>
        <center>
            <a href="../main.aspx" target=_top>Inicio</a></center>
    </div>
    </form>
</body>
</html>
