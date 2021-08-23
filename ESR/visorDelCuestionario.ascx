<%@ Control Language="C#" AutoEventWireup="true" Inherits="visorDelCuestionario" EnableViewState="false" Codebehind="visorDelCuestionario.ascx.cs" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<style type="text/css">
     #UpdateProgress1 {
      width: 400px; background-color: #FFC080; 
      bottom: 0%; left: 0px; position: absolute;
      border-right: gray 1px solid; border-top: gray 1px solid; 
      border-left: gray 1px solid; border-bottom: gray 1px solid;
     }
    .style1
    {
        color: #FF0000;
    }
</style>  
<table id="table1" border="0" width="100%">
    <tr>
        <td colspan="4" align="left" class="style1">
            </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblidTema" runat="server"></asp:Label>
            <asp:Label ID="lblidSubtema" runat="server"></asp:Label>
            <asp:Label ID="lblidIndicador" runat="server"></asp:Label>
            <asp:Label ID="lblReadOnly" runat="server"></asp:Label>
            <asp:Label ID="lblIdCuestionario" runat="server"></asp:Label>
            <asp:Label ID="lblOrdinal" runat="server"></asp:Label></td>
        <td>
        </td>
        <td>
        </td>
        <td>
</td>
    </tr>
</table>
<asp:UpdateProgress ID="UpdateProgress2" runat="server">
    <ProgressTemplate>
      Espere un momento...
    </ProgressTemplate>
</asp:UpdateProgress>
<div id="notaInformativa" style="display:none; background-color:#D5EDEF; 
    color:#4f6b72; width:400px; border: 1px solid #C1DAD7; 
    left:650px; top:400px; position:absolute;">
<asp:PlaceHolder ID="Notas" runat="server"></asp:PlaceHolder>
</div>
<script>
    $(document).ready(function () {
        $("img#IcnInfo").click(function () {
            $("div#notaInformativa").show("slow");
        });
        $("div#notaInformativa").mouseout(function () {
            $("div#notaInformativa").hide(1000);
        });
    });
</script> 
<div>  
<asp:Panel ID="Panel1" runat="server" Height="50px" Width="820px">
</asp:Panel>
</div> 


