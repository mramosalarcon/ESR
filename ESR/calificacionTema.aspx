<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/default.master" CodeBehind="calificacionTema.aspx.cs" Inherits="ESR.calificacionTema" %>

<asp:Content ID="Content3" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" Runat="Server">
    <link rel="stylesheet" href="css/esr_anterior.css" type="text/css" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderMain" Runat="Server">
<asp:Panel ID="panMenu" runat="server">
    <table class="style1">
        <tr>
            <td>
                Subtema</td>
            <td class="style2">
                Fundamental para la empresa y los Grupos de relación</td>
            <td class="style2">
                Afecta a grupos de relación</td>
            <td class="style2">
                Importante para la empresa</td>
            <td class="style2">
                Cumplimiento legal</td>
            <td class="style2">
                Poco relevante</td>
        </tr>
        <tr>
            <td>
                Empleabilidad y relaciones laborales</td>
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
                Diálogo social</td>
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
                Condiciones de trabajo y protección social</td>
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
                Balance trabajo familia</td>
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
                Capacitación y desarrollo humano</td>
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
                Salud y seguridad laboral</td>
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

