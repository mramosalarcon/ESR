<%@ Page Language="C#" AutoEventWireup="true" Inherits="menu" Codebehind="menu.aspx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server"><title>Menu Principal Cemefi</title>
<link href="css/esr_anterior.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <table>
            <tr>
                <td style="width: 135px" colspan="2">
	<asp:TreeView  ID="tvwMenu" runat="server" ImageSet="Msdn"
        NodeIndent="10" Target="main" ExpandDepth="0" OnSelectedNodeChanged="tvwMenu_SelectedNodeChanged">
        <ParentNodeStyle Font-Bold="False" />
        <HoverNodeStyle BackColor="#F1701C" Font-Underline="False" />
        <SelectedNodeStyle BackColor="White" BorderColor="#888888" BorderStyle="Solid" BorderWidth="1px"
         Font-Underline="False" HorizontalPadding="3px" VerticalPadding="1px" />
        <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px"
         NodeSpacing="1px" VerticalPadding="2px" />
	</asp:TreeView>
    <asp:AnimationExtender ID="tvwMenu_AnimationExtender" runat="server" 
        Enabled="True" TargetControlID="tvwMenu">
    </asp:AnimationExtender>
	</td>
            </tr>
            <tr>
                <td align="left" style="width: 135px" colspan="2">
                    <br />
                    <span class="txt_smal"><b>AVISO:</b> Para liberar cuestionario, vaya a la sección "Reporte
                        de avance" en el menú "Diagnóstico" y de clic en el botón "Liberar cuestionario"</span></td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <img src="images/aliarse_mexico_menu.jpg" width="80" height="57" alt="AliaRSE" /></td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <img
                        src="images/empresa_menugif.gif" width="80" height="37" alt="" /></td>
            </tr>
            <tr>
                <td align="center" colspan="2" style="height: 145px">
                <asp:UpdatePanel ID="updSoporte" runat="server">
                    <ContentTemplate>
                        <script type="text/javascript" src="http://download.skype.com/share/skypebuttons/js/skypeCheck.js"></script> Obtenga ayuda en línea:<br />
                <a href="skype:cemefirsepromocion?call"><img src="http://mystatus.skype.com/bigclassic/miguel.ramos.alarcon" style="border: none;" width="182" height="44" alt="Soporte en línea" /></a>
                        <asp:Timer ID="tmrConnections" runat="server" Interval="300000">
                        </asp:Timer>
                    </ContentTemplate>
                </asp:UpdatePanel>
                    <br />
                    <a href="http://www.cemefi.org/esr">http://www.cemefi.org/esr</a><br />
                    <a href="legales.aspx" target="main">Aviso Legal</a>
                    <br />
                    </td>
            </tr>
        </table>
    <asp:SiteMapDataSource ID="smdSiteMap" runat="server" ShowStartingNode="False" />
    </form>
</body>
</html>