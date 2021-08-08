<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeBehind="vincularEmpresa.aspx.cs" Inherits="ESR.vincularEmpresa" %>

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
            <td>
                &nbsp;
                <asp:Label ID="lblIdEmpresa" runat="server" Text="idEmpresa"></asp:Label>
            </td>
            <td>
                &nbsp;
                <asp:TextBox ID="txtIdEmpresa" runat="server"></asp:TextBox>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
                <asp:Label ID="Label1" runat="server" Text="idUsuario"></asp:Label>
            </td>
            <td>
                &nbsp;
                <asp:TextBox ID="txtIdUsuario" runat="server"></asp:TextBox>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
                <asp:Label ID="lblPerfil" runat="server" Text="Perfil"></asp:Label>
            </td>
            <td>
                &nbsp;
                <asp:RadioButton ID="rbtnResponsable" runat="server" Checked="True" 
                    Text="Responsable" ValidationGroup="idPerfil" GroupName="idPerfil" />
                <asp:RadioButton ID="rbtnColaborador" runat="server" Text="Colaborador" 
                    ValidationGroup="idPerfil" GroupName="idPerfil" />
            </td>
            <td>
                &nbsp;
            </td>
        </tr><tr>
            <td>
                
                &nbsp;</td>
            <td>
               
                <asp:Button ID="btnAceptar" runat="server" onclick="btnAceptar_Click" 
                    Text="Aceptar" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" />
               
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                
            </td>
            <td>
               
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>

</asp:Content>
