<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeBehind="vincularRegistro.aspx.cs" Inherits="ESR.vincularRegistro" %>

<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" 
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" 
    Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderMain" Runat="Server">
    <div>
        <table width="100%">
        <tr>
      <td colspan="2" style="height: 23px" class="borde_menu"><img src="/images/espacio_transparente.gif" width="40" height="10"></td>
      </tr>
      <tr>
      <td colspan="2">
      Seleccione una de las siguientes opciones:
      </td>
      </tr>
            <tr>
                <td>
                    <asp:RadioButtonList ID="rblVincularRegistro" runat="server">
                        <asp:ListItem Value="0">Buscar una empresa para vincularme</asp:ListItem>
                        <asp:ListItem Value="1">Registrar una nueva empresa</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="cmdNext" runat="server" onclick="cmdNext_Click" 
                        Text="Siguiente &gt;&gt;" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    
    </div>
</asp:Content>

 <asp:Content ID="Content2" 
ContentPlaceHolderId="PlaceHolderPageTitleInTitleArea" runat="server">
Vincular tu registro a una Empresa
</asp:Content>
