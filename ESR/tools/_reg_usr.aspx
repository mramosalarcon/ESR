<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="_reg_usr.aspx.cs" Inherits="ESR.tools._reg_usr" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Button ID="btnCargarUsuarios" runat="server" 
            onclick="btnCargarUsuarios_Click" Text="Cargar usuarios" />
        <br />
        <asp:Label ID="lblResult" runat="server"></asp:Label>
    
    </div>
    </form>
</body>
</html>
