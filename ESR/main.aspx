<%@ Page Language="C#" MasterPageFile="~/default.master" CodeBehind="main.aspx.cs" Inherits="ESR.main" Title="Pagina principal" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content4" ContentPlaceHolderID="PlaceHolderPageTitle" Runat="Server">
Inicio ESR®
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderId="PlaceHolderMain" runat="server">
    <table style="width: 100%;">
        <tr>
            <td align="right">
                <asp:UpdatePanel ID="updConnections" runat="server">
                <ContentTemplate>
            <img src="images/usuario.gif" width="10" height="11" alt="Usuarios conectados" />
            <asp:Label ID="lblUsuariosConectados" runat="server" ForeColor="#C04000"></asp:Label>
                    <asp:Timer ID="tmrConnections" runat="server">
                    </asp:Timer>
            </ContentTemplate>
    </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
        <td>
            <asp:UpdatePanel ID="updTiempoRestante" runat="server" Visible="True">
                <ContentTemplate>
		    <asp:Label ID="lblTiempoRestante" runat="server" Font-Bold="True" Font-Size="14pt"
                        ForeColor="Red"></asp:Label>
                    <asp:Timer ID="tmrTiempoRestante" runat="server">
                    </asp:Timer>
                </ContentTemplate>
            </asp:UpdatePanel>
        
        </td>
        </tr>
    </table>
    <p><h1>Centro de Mensajes</h1></p>
<asp:UpdatePanel ID="udpVinculos" runat="server">
    <ContentTemplate>
        <p><h1>Solicitud de vínculos</h1></p>
        <asp:GridView ID="grvVinculos" runat="server" AutoGenerateColumns="False" 
            onrowcommand="grvVinculos_RowCommand">
            <Columns>
                <asp:BoundField DataField="fecha" HeaderText="Fecha">
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="cuerpo" HeaderText="Mensaje" />
                <asp:BoundField DataField="idUsuario" HeaderText="Usuario">
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:ButtonField ButtonType="Image" Text="Autorizar" CommandName="autorizar" 
                    DataTextField="idMensaje" DataTextFormatString="{0}" HeaderText="Autorizar" 
                    ImageUrl="~/images/gridActulizar.gif">
                <ItemStyle HorizontalAlign="Center" />
                </asp:ButtonField>
                <asp:ButtonField ButtonType="Image" CommandName="eliminar" 
                    DataTextField="idMensaje" DataTextFormatString="{0}" HeaderText="Eliminar" 
                    ImageUrl="~/images/failed.gif" Text="Eliminar">
                <ItemStyle HorizontalAlign="Center" />
                </asp:ButtonField>
            </Columns>
        </asp:GridView>
    </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="udpCV" runat="server">
    <ContentTemplate>
        <p><h1>Solicitud de cadena de valor</h1></p>
        <asp:GridView ID="grvCV" runat="server">
        </asp:GridView>
    </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="udpMensajes" runat="server">
    <ContentTemplate>
        <p><h1>Avisos</h1></p>
        <asp:webpartzone id="MainZone" runat="server" headertext="Main" 
            BorderColor="#CCCCCC" Font-Names="Verdana" Padding="6">
        <zonetemplate>
    	  <asp:label id="contentPart" runat="server" title="Biblioteca de Evidencias"
                Width="683px">
      	    <h2>Para acceder a la biblioteca de Evidencias de clic <a href=<%=Session["idEmpresa"].ToString() + "/Documentos%20compartidos/Forms/AllItems.aspx"  %> target=_blank>aquí</a>.</h2>
    	  </asp:label>
  	    </zonetemplate>
            <MenuLabelHoverStyle ForeColor="#E2DED6" />
            <MenuLabelStyle ForeColor="White" />
            <MenuPopupStyle BackColor="#5D7B9D" BorderColor="#CCCCCC" BorderWidth="1px" 
                Font-Names="Verdana" Font-Size="0.6em" />
            <MenuVerbHoverStyle BackColor="#F7F6F3" BorderColor="#CCCCCC" 
                BorderStyle="Solid" BorderWidth="1px" ForeColor="#333333" />
            <MenuVerbStyle BorderColor="#5D7B9D" BorderStyle="Solid" BorderWidth="1px" 
                ForeColor="White" />
            <TitleBarVerbStyle Font-Size="0.6em" Font-Underline="False" ForeColor="White" />
            <EmptyZoneTextStyle Font-Size="0.8em" />
            <HeaderStyle Font-Size="0.7em" ForeColor="#CCCCCC" HorizontalAlign="Center" />
            <PartChromeStyle BackColor="#F7F6F3" BorderColor="#E2DED6" Font-Names="Verdana" 
                ForeColor="White" />
            <PartStyle Font-Size="0.8em" ForeColor="#333333" />
            <PartTitleStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="0.8em" 
                ForeColor="White" />
        </asp:webpartzone>
        <asp:GridView ID="grvMensajes" runat="server">
        </asp:GridView>
    </ContentTemplate>
    </asp:UpdatePanel>    
 </asp:Content>

 <asp:Content ID="Content2" 
ContentPlaceHolderId="PlaceHolderPageTitleInTitleArea" runat="server">
¡Bienvenido a la aplicación del Distintivo ESR®!<br/>
     <asp:Label ID="lblEmpresa" runat="server" Text="Label"></asp:Label></asp:Content>

