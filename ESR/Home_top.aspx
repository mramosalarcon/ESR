<%@ Page Language="C#" AutoEventWireup="true" Inherits="Home_top" Codebehind="Home_top.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" ><head runat="server"><title>Header_Cemefi</title>
<link href="Style/StyleSheet.css" rel="stylesheet" type="text/css" /><meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <table class="style1" width="100%">
            <tr>
                <td colspan="5">
                <asp:Menu ID="Menu1" runat="server" BackColor="Navy" DynamicHorizontalOffset="2"
            Font-Names="Verdana" Font-Size="10px" ForeColor="#ffffff" Orientation="Horizontal"
            StaticSubMenuIndent="10px" Width="100%">
              <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
              <DynamicHoverStyle BackColor="#666666" ForeColor="White" />
              <DynamicMenuStyle BackColor="#D25304" />
              <StaticSelectedStyle BackColor="#1C5E55" />
              <DynamicSelectedStyle BackColor="#1C5E55" />
              <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
              <StaticHoverStyle BackColor="#D25304" ForeColor="White" />
  
        </asp:Menu>   </td>
            </tr>
<tr>
                <td colspan="5" align="center">
            <asp:UpdatePanel ID="updTiempoRestante" runat="server" Visible="True">
                <ContentTemplate>
                    <asp:Label ID="lblMensaje" runat="server" Text="Label"></asp:Label><br /><br />
		    <asp:Label ID="lblTiempoRestante" runat="server" Font-Bold="True" Font-Size="14pt"
                        ForeColor="Red"></asp:Label>
                    <asp:Timer ID="tmrTiempoRestante" runat="server">
                    </asp:Timer>
                </ContentTemplate>
            </asp:UpdatePanel>

</td>
            </tr>
            <tr>
                <td>
                    </td>
                <td colspan="2">
                   <asp:Menu ID="MenuTop" runat="server" Orientation="Horizontal" 
                        OnMenuItemClick="MenuTop_MenuItemClick" Height="19px">
                        <Items>
                            <asp:MenuItem Selectable="False" Text="Bienvenid@, " Value="Bienvenido, " Target="main"></asp:MenuItem></Items>
                        <StaticHoverStyle BorderColor="Transparent" />
                    </asp:Menu>
            </td>
                <td >
                <asp:Label ID="lblNombreEmpresa" runat="server" Font-Bold="True"></asp:Label></td>
                <td>
                <asp:UpdatePanel ID="updConnections" runat="server">
                <ContentTemplate>
            <img src="images/usuario.gif" width="10" height="11" />
            <asp:Label ID="lblUsuariosConectados" runat="server" ForeColor="#C04000"></asp:Label>
                    <asp:Timer ID="tmrConnections" runat="server" ontick="tmrConnections_Tick">
                    </asp:Timer>
            </ContentTemplate>
          </asp:UpdatePanel>
        </td>
            </tr>
            <tr>
                <td>
                    <img src="images/ESR_header.gif" /></td>
                <td align="center" valign="middle">
                    <asp:Image ID="imgLogoTop" runat="server" Width="40%" ImageAlign="Right" ImageUrl="~/tools/imgLogo.aspx" /></td>
                <td align="center" valign="middle">
                    <img src="images/login03.gif"/></td>
                <td align="center" valign="middle">
                 <img src="images/login01.jpg"/>
                    </td>
                <td align="center" valign="middle">
                    <img src="images/login02.gif" /></td>
            </tr>
        </table>
	<div align="right">
        &nbsp;</div>
    <div>
    <table border="0" width="100%" id="table2" cellspacing="0">
	<tr>
		<td></td>
		<td align="left" style="width: 526px">
            &nbsp;</td>
        <td align="right">
        </td>
        <td align="right">
        </td>
        <td align="right">
            &nbsp;
        </td>
        <td align="right">
            &nbsp;</td>
	</tr>
</table>
<table border="0" width="100%" id="table3" cellspacing="0">
	<tr>
		<td width="150" align="center" valign="top" >
					</td>
        <td align="center" valign="middle" ></td>
		<td width="350" align="center" valign="middle" >
		</td>
	</tr>
</table>
    </div>    
    </form>    
</body>
</html>