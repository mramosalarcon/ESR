<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" Inherits="administradorDeTemasSubtemas" Codebehind="administradorDeTemasSubtemas.aspx.cs" %>

<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" 
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" 
    Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderMain" Runat="Server">

        &nbsp;<table style="width: 100%" border="0">
    <tr>
      <td colspan="2" style="height: 23px" class="borde_menu"><img src="/images/espacio_transparente.gif" width="40" height="10"></td>
      </tr>
    <tr>
    <td colspan="2"><h3>Administrador de temas y subtemas</h3></td>
    </tr>
    
        <tr>
            <td>
                Temas</td>
            <td>
                <asp:DropDownList ID="ddlTemas" runat="server" OnSelectedIndexChanged="ddlTemas_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList></td>
        </tr>
	<tr>
		<td>
            Tema</td>
		<td>
            <asp:TextBox ID="txtTema" runat="server" Width="500px"></asp:TextBox></td>
	</tr>
        <tr>
            <td>
                Descripción</td>
            <td>
                <asp:TextBox ID="txtTemaCorto" runat="server" Width="500px"></asp:TextBox></td>
        </tr>
            <tr>
                <td>
                    Ordinal</td>
                <td>
                    <asp:TextBox ID="txtTemaOrdinal" runat="server" Width="30px"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    bloqueado</td>
                <td>
                    <asp:CheckBox ID="chkTemaBloqueado" runat="server" /></td>
            </tr>
        <tr>
            <td>
                Subtemas</td>
            <td>
                <asp:DropDownList ID="ddlSubtemas" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSubtemas_SelectedIndexChanged">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td>
                Subtema</td>
            <td>
                <asp:TextBox ID="txtSubtema" runat="server" Width="500px"></asp:TextBox></td>
        </tr>
            <tr>
                <td style="height: 14px">
                    Ordinal</td>
                <td style="height: 14px">
                    <asp:TextBox ID="txtSubtemaOrdinal" runat="server" Width="30px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="height: 14px">
                    bloqueado</td>
                <td style="height: 14px">
                    <asp:CheckBox ID="chkSubtemaBloqueado" runat="server" /></td>
            </tr>
            <tr>
                <td style="height: 14px">
                </td>
                <td style="height: 14px">
                </td>
            </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />
                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" Visible="False" OnClick="btnEliminar_Click" /></td>
        </tr>
    </table>
</asp:Content>