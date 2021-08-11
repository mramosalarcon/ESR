<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="documentLibrary.aspx.cs" Inherits="ESR.documentLibrary" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<!-- Global site tag (gtag.js) - Google Analytics -->
<script async src="https://www.googletagmanager.com/gtag/js?id=UA-120281557-1"></script>
<script>
  window.dataLayer = window.dataLayer || [];
  function gtag(){dataLayer.push(arguments);}
  gtag('js', new Date());

  gtag('config', 'UA-120281557-1');
</script>
    </head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%;">
            <tr>
                <td>
                    &nbsp;
                <asp:Button ID="btnAsignarDocumentos" runat="server" 
            onclick="btnAsignarDocumentos_Click" Text="Asignar documentos" />
                    <asp:Label ID="lblMensaje" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    &nbsp;
                    </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                <asp:TreeView ID="tvwEvidencias" runat="server" 
            NodeIndent="15" MaxDataBindDepth="2" 
            ontreenodepopulate="tvwEvidencias_TreeNodePopulate">
            <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
            <Nodes>
                <asp:TreeNode Text="Libreria de documentos" 
                    Value="Libreria de documentos" PopulateOnDemand="True">
                </asp:TreeNode>
            </Nodes>
            <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" 
                HorizontalPadding="2px" NodeSpacing="0px" VerticalPadding="2px" />
            <ParentNodeStyle Font-Bold="False" />
            <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" 
                HorizontalPadding="0px" VerticalPadding="0px" />
        </asp:TreeView>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            </table>
    </div>
    </form>
</body>
</html>
