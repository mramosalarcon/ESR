<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" Inherits="administradorDeEmpresas" Codebehind="administradorDeEmpresas.aspx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" 
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" 
    Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderMain" Runat="Server">

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
    </table>
<table border="0" width="100%" id="table2">
    <tr>
        <td>
            <asp:CheckBox ID="chkTodasLasEmpresas" runat="server" Text="Todas las empresas" Visible="false" /><br />
            <asp:CheckBox ID="chkCuestionarioLiberado" runat="server" Text="Con cuestionario liberado" AutoPostBack="True" OnCheckedChanged="chkCuestionarioLiberado_CheckedChanged" Visible="false" />
            <br />
            <asp:CheckBox ID="chkCuestionario" runat="server" AutoPostBack="True" OnCheckedChanged="chkCuestionario_CheckedChanged"
                Text="Por cuestionario" Visible="false" /></td>
        <td>
        </td>
        <td align="center">
            <asp:TextBox ID="txtBuscar" runat="server" MaxLength="48" Width="300px"></asp:TextBox><br /><asp:Button ID="btnBuscar" runat="server" Text="Buscar ESR" OnClick="btnBuscar_Click" /></td>
    </tr>
	<tr>
		<td>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="chkCuestionarioLiberado" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="chkCuestionario" EventName="CheckedChanged" />                
            </Triggers>
                <ContentTemplate>
                    <asp:DropDownList ID="ddlCuestionarios" runat="server" Visible="False">
                    </asp:DropDownList>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
		<td>
            <br />
            <br />
            </td>
        <td>
        </td>
	</tr>
	<tr>
		<td></td>
		<td></td>
        <td>
        </td>
	</tr>
    <tr>
        <td>
        </td>
        <td>
            &nbsp;</td>
        <td>
        </td>
    </tr>
</table>
    <table border="0" width="100%" id="table1">
	<tr>
		<td>&nbsp;</td>
		<td style="width: 834px"></td>
	</tr>
	<tr>
		<td style="height: 15px"></td>
		<td style="height: 15px; width: 834px;">
            <asp:Label ID="lblMensaje" runat="server"></asp:Label></td>
	</tr>
        <tr>
            <td colspan="2" style="height: 20px">
            <center>
                <asp:GridView ID="grdEmpresas" runat="server" CellPadding="4" 
                    ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" 
                    OnRowCommand="cmdReporteDeAvance" Width="100%" EnableModelValidation="True">
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <EditRowStyle BackColor="#999999" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="lightgrey" Font-Bold="True" ForeColor="#FFFFFF" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="idEmpresa" HeaderText="ID" />
                        <asp:HyperLinkField AccessibleHeaderText="RFC" DataTextField="rfc" HeaderText="RFC"
                            Text="RFC" DataNavigateUrlFields="idEmpresa" DataNavigateUrlFormatString="~/formaDeInscripcion.aspx?Content=empresa&amp;idEmpresa={0}" NavigateUrl="~/formaDeInscripcion.aspx" />
                        <asp:BoundField AccessibleHeaderText="Razón social" DataField="razonSocial" 
                            HeaderText="Razón social" />
                        <asp:BoundField AccessibleHeaderText="Nombre" DataField="nombre" HeaderText="Nombre" />
                        <asp:BoundField AccessibleHeaderText="Nombre corto" DataField="nombreCorto" HeaderText="Nombre corto" />
                        <asp:BoundField DataField="idCuestionario" />
                        <asp:BoundField AccessibleHeaderText="Cuestionario" DataField="cuestionario"
                            HeaderText="Cuestionario" />
                        <asp:BoundField AccessibleHeaderText="C&#243;digo postal" DataField="cp" HeaderText="C&#243;digo postal" />
                        <asp:HyperLinkField AccessibleHeaderText="Contacto" DataNavigateUrlFields="idUsuario"
                            DataNavigateUrlFormatString="mailto:{0}" DataTextField="idUsuario" HeaderText="Email del contacto"
                            NavigateUrl="mailto:" Text="Contacto" DataTextFormatString="{0}" />
                        <asp:BoundField AccessibleHeaderText="Fecha de actualizaci&#243;n de registro" DataField="fechaModificacion"
                            HeaderText="Fecha de actualizaci&#243;n de registro">
			    <ItemStyle HorizontalAlign="center" />
			</asp:BoundField>
                        <asp:ButtonField ButtonType="Image"
                            HeaderText="Rep. de Avance" ImageUrl="~/images/lupa.jpg" 
                            DataTextField="idEmpresa" DataTextFormatString="{0}" Text="idEmpresa" 
                            CommandName="avance"  >
                        <ItemStyle HorizontalAlign="center" />
                        </asp:ButtonField>
                        <asp:ButtonField ButtonType="Image" CommandName="individual" DataTextField="idEmpresa"
                            DataTextFormatString="{0}" HeaderText="Rep. Individual" ImageUrl="~/images/lupa.jpg"
                            Text="idEmpresa" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:ButtonField>
                        <asp:ButtonField ButtonType="Image" CommandName="retro" DataTextField="idEmpresa"
                            DataTextFormatString="{0}" HeaderText="Rep. de Resultados" ImageUrl="~/images/lupa.jpg"
                            Text="idEmpresa" >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:ButtonField>
                        <asp:ButtonField ButtonType="Image"
                            HeaderText="Rep. de Evidencias" ImageUrl="~/images/lupa.jpg" 
                            DataTextField="idEmpresa" DataTextFormatString="{0}" Text="idEmpresa" 
                            CommandName="evidencias"  >
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:ButtonField>                            
                        <asp:TemplateField AccessibleHeaderText="¿Realizó el pago del distintivo?" 
                            HeaderText="Pago realizado">
                            <ItemTemplate>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:CheckBox ID="chkPago" runat="server" AutoPostBack="True" 
                                             oncheckedchanged="chkPago_CheckedChanged" 
                                            Checked='<%# Convert.ToBoolean(Eval("pagado").ToString().Equals("True"))   %>' />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </center>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td style="width: 834px">
            </td>
        </tr>
</table>
</asp:Content>

 <asp:Content ID="Content2" 
ContentPlaceHolderId="PlaceHolderPageTitleInTitleArea" runat="server">
Buscador de Empresas Socialmente Responsables
</asp:Content>