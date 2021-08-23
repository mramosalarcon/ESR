<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" Inherits="administradorDeCuestionarios" Codebehind="administradorDeCuestionarios.aspx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" 
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" 
    Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderMain" Runat="Server">
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <img src="images/progress_bar_2_1.gif" alt="Espere un momento..."/>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>                
    <table border="0" width="100%" id="table1">
        <tr>
            <td style="height: 21px">
                <asp:Label ID="Label1" runat="server" Text="Cuestionarios disponibles" Width="7px"></asp:Label></td>
            <td style="height: 21px" colspan="3">
                <asp:DropDownList ID="ddlCuestionarios" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCuestionarios_SelectedIndexChanged">
                </asp:DropDownList></td>
        </tr>
	<tr>
		<td>
            Nombre</td>
        <td colspan="3">
            <asp:TextBox ID="txtNombreCuestionario" runat="server" Width="350px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNombreCuestionario"
                Display="None" ErrorMessage="El nombre del cuestionario es obligatorio"></asp:RequiredFieldValidator></td>
	</tr>
        <tr>
            <td>
                Descripcion</td>
            <td colspan="3">
                <asp:TextBox ID="txtDescripcionCuestionario" runat="server" Width="350px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDescripcionCuestionario"
                    Display="None" ErrorMessage="La descripción del cuestionario es obligatoria"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td>
                Año</td>
            <td colspan="3">
                <asp:TextBox ID="txtAnioCuestionario" runat="server" Width="80px"></asp:TextBox>
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAnioCuestionario"
                    Display="None" ErrorMessage="El año del cuestionario es obligatorio"></asp:RequiredFieldValidator>
                </td>
        </tr>
        <tr>
            <td>
                Bloqueado</td>
            <td colspan="3">
                <asp:RadioButtonList ID="rblSiNo" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1">Si</asp:ListItem>
                    <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td>
            </td>
            <td colspan="3">
                <asp:Button ID="btnAgregarCuestionario" runat="server" OnClick="btnAgregarCuestionario_Click"
                    Text="Agregar Cuestionario" />
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                </td>
            <td colspan="3">
                <asp:Label ID="lblCuestionario" runat="server" Text="Agregar Indicadores" Visible="False"></asp:Label></td>
        </tr>
        <tr>
            <td style="height: 20px">
                <asp:Label ID="lblTema" runat="server" Text="Tema" Visible="False"></asp:Label></td>
            <td colspan="3" style="height: 20px">
                <asp:DropDownList ID="ddlTemas" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTemas_SelectedIndexChanged" Visible="False">
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td style="height: 21px">
            </td>
            <td colspan="3" style="height: 21px">
                <asp:Button ID="btnSelTodos" runat="server" Text="Marcar todos" Visible="False" /><asp:Button ID="btnSelNinguno" runat="server" Text="Desmarcar Todos" Visible="False"  /></td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="grdIndicadores" runat="server" DataKeyNames="idIndicador" OnSelectedIndexChanged="grdIndicadores_SelectedIndexChanged" Visible="False" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="grdIndicadores_RowDataBound" >
                    <Columns>
                        <asp:TemplateField HeaderText="Seleccionar">
                            <ItemTemplate>
                                <asp:CheckBox ID="chbSelecciona" runat="server" AutoPostBack="True" OnCheckedChanged="chbSelecciona_CheckedChanged" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="idIndicador" HeaderText="ID Indicador" >
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion" HeaderText="Descripci&#243;n" >
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Ordinal">
                        <ItemTemplate>
                            <asp:DropDownList id="ddlOrdinal" AutoPostBack="True" runat="server" DataSource='<%# GetOrdinales() %>' DataTextField="Ordinal" DataValueField="Ordinal" OnSelectedIndexChanged="ddlOrdinal_SelectedIndexChanged">
                            </asp:DropDownList>
                        </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Width="20px" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                </asp:GridView>
            </td>
        </tr>
</table>
            </ContentTemplate>
        </asp:UpdatePanel>

</asp:Content>