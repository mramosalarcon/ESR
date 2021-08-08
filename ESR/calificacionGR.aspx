<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/default.master" CodeBehind="calificacionGR.aspx.cs" Inherits="ESR.calificacionGR" %>

<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" Runat="Server">
    <link rel="stylesheet" href="css/esr_anterior.css" type="text/css" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderMain" Runat="Server">
<asp:Panel ID="panMenu" runat="server">
    <table class="style1">
        <tr>
            <td>
                Grupos de relación</td>
            <td class="style2">
                Muy influyente en percepción y operacion</td>
            <td class="style2">
                Influyente en la operacion</td>
            <td class="style2">
                Influyente en la percepcion</td>
            <td class="style2">
                Involucrado</td>
            <td class="style2">
                No reelevante</td>
        </tr>
        <tr>
            <td>
                Distribuidores, clientes y consumidores</td>
            <td class="style2">
                <asp:RadioButton ID="RadioButton1" runat="server" />
            </td>
            <td class="style2">
                <asp:RadioButton ID="RadioButton2" runat="server" />
            </td>
            <td class="style2">
                <asp:RadioButton ID="RadioButton3" runat="server" />
            </td>
            <td class="style2">
                <asp:RadioButton ID="RadioButton4" runat="server" />
            </td>
            <td class="style2">
                <asp:RadioButton ID="RadioButton5" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                Accionistas e inversionistas</td>
            <td class="style2">
                <asp:RadioButton ID="RadioButton6" runat="server" />
            </td>
            <td class="style2">
                <asp:RadioButton ID="RadioButton7" runat="server" />
            </td>
            <td class="style2">
                <asp:RadioButton ID="RadioButton8" runat="server" />
            </td>
            <td class="style2">
                <asp:RadioButton ID="RadioButton9" runat="server" />
            </td>
            <td class="style2">
                <asp:RadioButton ID="RadioButton10" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                Comunidad interna (colaboradores, sindicatos, familias)</td>
            <td class="style2">
                <asp:RadioButton ID="RadioButton11" runat="server" />
            </td>
            <td class="style2">
                <asp:RadioButton ID="RadioButton12" runat="server" />
            </td>
            <td class="style2">
                <asp:RadioButton ID="RadioButton13" runat="server" />
            </td>
            <td class="style2">
                <asp:RadioButton ID="RadioButton14" runat="server" />
            </td>
            <td class="style2">
                <asp:RadioButton ID="RadioButton15" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                Cadena de valor (proveedores)</td>
            <td class="style2">
                <asp:RadioButton ID="RadioButton16" runat="server" />
            </td>
            <td class="style2">
                <asp:RadioButton ID="RadioButton17" runat="server" />
            </td>
            <td class="style2">
                <asp:RadioButton ID="RadioButton18" runat="server" />
            </td>
            <td class="style2">
                <asp:RadioButton ID="RadioButton19" runat="server" />
            </td>
            <td class="style2">
                <asp:RadioButton ID="RadioButton20" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                Gobierno</td>
            <td class="style2">
                <asp:RadioButton ID="RadioButton21" runat="server" />
            </td>
            <td class="style2">
                <asp:RadioButton ID="RadioButton22" runat="server" />
            </td>
            <td class="style2">
                <asp:RadioButton ID="RadioButton23" runat="server" />
            </td>
            <td class="style2">
                <asp:RadioButton ID="RadioButton24" runat="server" />
            </td>
            <td class="style2">
                <asp:RadioButton ID="RadioButton25" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                Comunidad (Organizaciones de la sociedad civil, vecinos, autoridades locales,&nbsp; 
                grupos comunitarios)</td>
            <td class="style2">
                <asp:RadioButton ID="RadioButton26" runat="server" />
            </td>
            <td class="style2">
                <asp:RadioButton ID="RadioButton27" runat="server" />
            </td>
            <td class="style2">
                <asp:RadioButton ID="RadioButton28" runat="server" />
            </td>
            <td class="style2">
                <asp:RadioButton ID="RadioButton29" runat="server" />
            </td>
            <td class="style2">
                <asp:RadioButton ID="RadioButton30" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                Competidores</td>
            <td class="style2">
                <asp:RadioButton ID="RadioButton31" runat="server" />
            </td>
            <td class="style2">
                <asp:RadioButton ID="RadioButton32" runat="server" />
            </td>
            <td class="style2">
                <asp:RadioButton ID="RadioButton33" runat="server" />
            </td>
            <td class="style2">
                <asp:RadioButton ID="RadioButton34" runat="server" />
            </td>
            <td class="style2">
                <asp:RadioButton ID="RadioButton35" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                Medio ambiente (generaciones futuras)</td>
            <td class="style2">
                <asp:RadioButton ID="RadioButton36" runat="server" />
            </td>
            <td class="style2">
                <asp:RadioButton ID="RadioButton37" runat="server" />
            </td>
            <td class="style2">
                <asp:RadioButton ID="RadioButton38" runat="server" />
            </td>
            <td class="style2">
                <asp:RadioButton ID="RadioButton39" runat="server" />
            </td>
            <td class="style2">
                <asp:RadioButton ID="RadioButton40" runat="server" />
            </td>
        </tr>
    </table>
    <asp:Button ID="Button1" runat="server" Text="Ir al cuestionario" 
        onclick="Button1_Click" />
</asp:Panel>      
</asp:Content>
<asp:Content ID="Content4" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            text-align: center;
        }
    </style>
</asp:Content>

