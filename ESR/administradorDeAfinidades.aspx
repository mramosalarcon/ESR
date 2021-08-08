<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" Inherits="administradorDeAfinidades" Codebehind="administradorDeAfinidades.aspx.cs" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>


<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" 
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" 
    Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderMain" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                &nbsp;<table border="0" width="100%" id="table1">
	<tr>
		<td style="width: 321px; height: 15px">
            Afinidades disponibles:
        </td>
		<td style="height: 15px">
            <asp:DropDownList ID="ddlAfinidades" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAfinidades_SelectedIndexChanged">
            </asp:DropDownList></td>
	</tr>
	<tr>
		<td style="width: 321px; height: 13px">
            Nombre</td>
		<td style="height: 13px">
            <asp:TextBox ID="txtDescripcion" runat="server" Width="350px" MaxLength="50"></asp:TextBox></td>
	</tr>
        <tr>
            <td style="width: 321px; height: 13px">
                Adjuntar archivo de referencia</td>
            <td style="height: 13px">
                <asp:FileUpload ID="FileUpload1" runat="server" Height="16px" Width="348px" />
                <asp:AnimationExtender ID="FileUpload1_AnimationExtender" runat="server" 
                    Enabled="True" TargetControlID="FileUpload1">
                </asp:AnimationExtender>
            </td>
        </tr>
                    <tr>
                        <td style="width: 321px; height: 13px">
                        </td>
                        <td style="height: 13px">
                            <asp:Button ID="btnGuardar" runat="server" OnClick="btnGuardar_Click" 
                                Text="Guardar" />
                            <asp:Button ID="btnEliminar" runat="server" OnClick="btnEliminar_Click" 
                                Text="Eliminar" />
                        </td>
                    </tr>
        <tr>
            <td style="width: 321px; height: 13px">
            </td>
            <td style="height: 13px">
            </td>
        </tr>
        <tr>
            <td style="width: 321px; height: 13px">
            </td>
            <td style="height: 13px">
            </td>
        </tr>
</table>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>