<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" Inherits="diagnostico" Codebehind="diagnostico.aspx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" Runat="Server">
    <link rel="stylesheet" href="css/esr_anterior.css" type="text/css" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderMain" Runat="Server">
    <table width="100%"  border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td align="left" valign="top"  class="borde_menu">
          <div> <img src="/images/espacio_transparente.gif" width="40" height="13" alt="" /><br />
              <asp:Label ID="lblIdCuestionario" runat="server" Font-Size="14pt" Text="Contestando el cuestionario: "
            Visible="False"></asp:Label>
              <br />
              <asp:Panel ID="pMenu" runat="server">
              
              </asp:Panel>
              <asp:Menu ID="menuArea" runat="Server" DynamicHorizontalOffset="2"
            Font-Names="tahoma" Font-Size="11px" ForeColor="white" Orientation="horizontal"
            StaticSubMenuIndent="10px" OnMenuItemClick="menuArea_MenuItemClick" Visible="true"> 
            <StaticMenuStyle BackColor="#FFFFFF" BorderColor="#D25304" BorderStyle="Solid" BorderWidth="1px" /> 
            <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" /> 
            <DynamicHoverStyle BackColor="#D3D3D3" ForeColor="black" /> 
            <DynamicMenuStyle BackColor="#D3D3D3" ForeColor="white" BorderColor="#ffffff" BorderStyle="Solid" BorderWidth="1px" /> 
            <StaticSelectedStyle BackColor="#D25304" ForeColor="White"/> 
            <DynamicSelectedStyle BackColor="#FFFFFF" /> 
            <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" /> 
            <StaticHoverStyle BackColor="#D3D3D3" ForeColor="white" /> 
            </asp:Menu>

              <table border="0" width="100%" id="table1">
                <tr>
                  <td>&nbsp;</td>
                  <td align="right"><br />
                      <input type="button" id="btnImprimir" name="btnImprimir" value="Imprimir" onclick="window.print();" runat="server" onclientclick="_spFormOnSubmitCalled = false;"/>
                      <asp:Button ID="btnAnterior" runat="server" OnClick="btnAnterior_Click" Text="<< Anterior" OnClientClick="_spFormOnSubmitCalled = false;"/>            
                      <asp:Button ID="btnSiguiente" runat="server" Text="Siguiente >>" OnClick="btnSiguiente_Click" OnClientClick="_spFormOnSubmitCalled = false;"/></td>
                </tr>
              </table>
          <br />        
          </div>
        </td>
      </tr>
    </table>
	<br />
        <asp:PlaceHolder ID="indicadores" runat="server"></asp:PlaceHolder>
        <br />
        <br />        
        <br />
        <br />
</asp:Content>