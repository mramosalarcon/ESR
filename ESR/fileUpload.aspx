<%@ Page Language="C#" AutoEventWireup="true" Inherits="fileUpload" Codebehind="fileUpload.aspx.cs" %>
<%@ Register Assembly="FUA" Namespace="Subgurim.Controles" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Adjuntar evidencia</title>
    <link href="Style/StyleSheet.css" rel="stylesheet" type="text/css" />
<script language="javascript" type="text/javascript">
function showprogress()
{
    alert("Subiendo archivo, de clic en aceptar");
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table id="table1" border="0" width="100%">
            <tr>
                <td>
                    <strong>Adjuntar archivo</strong></td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    Seleccione el archivo a adjuntar</td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:FileUpload ID="fuAdjuntarArchivo" runat="server" /></td>
                <td>&nbsp;
                    </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnAceptar" runat="server" Text="Adjuntar archivo" OnClick="btnAceptar_Click" /></td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    (El archivo no debe ser mas grande que 4 MB (4096 KB))</td>
                <td>&nbsp;
                    </td>
            </tr>
            <tr>
                <td style="height: 22px">
                <div id="divProgress" visible="false">
                    <asp:Label ID="lblMensaje" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>&nbsp;</div>
                    </td>
                <td style="height: 22px">
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
        </table>
    
    </div>
        &nbsp;
    </form>
</body>
</html>
