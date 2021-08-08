<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeBehind="videos.aspx.cs" Inherits="ESR.videos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="cntHeader" ContentPlaceHolderID="head" runat="server">
    <title>Videos del III Encuentro ESR</title>
	
</asp:Content>
	
<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderMain" Runat="Server">
    <table style="width: 100%;">
        <tr>
            <td class="style1" align="center" colspan="3">
                <script language="Javascript">
                    if (g_bNetscape) {
                        document.writeln("<APPLET mayscript code=WMPNS.WMP name=WMP1 width=300 height=200 MAYSCRIPT >");
                    }
			</script>
			<OBJECT CLASSID="clsid:6BF52A52-394A-11D3-B153-00C04F79FAA6" ID="WMP">
			<PARAM NAME="Name" VALUE="WMP1">
			<PARAM NAME="URL" VALUE="mms://esr.cemefi.org/<%=Request["video"]%>.wmv">
			</OBJECT>
			<script language="Javascript">
			    if (g_bNetscape) {
			        document.writeln("</APPLET>");
			    }
			</script>
			</td>
        </tr>
        <tr>
            <td align="center">
                <asp:ImageButton ID="imb10anos" runat="server" Width="50%" 
                    ImageUrl="~/images/video10anos.png" 
                    PostBackUrl="videos.aspx?video=video10anos"  />
            </td>
            <td>
                Video ESR 10 años</td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:ImageButton ID="imbStat01" runat="server" Width="50%" 
                    ImageUrl="~/images/ESRfondoHomeElipse_3.jpg" 
                    PostBackUrl="videos.aspx?video=statements01"  />
            </td>
            <td>
                Declaraciones RSE 1</td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:ImageButton ID="imbStat02" runat="server" Width="50%" 
                    ImageUrl="~/images/ESRfondoHomeElipse_3.jpg" 
                    PostBackUrl="videos.aspx?video=statements02"  />
            </td>
            <td>
                Declaraciones RSE 2</td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:ImageButton ID="imbStat03" runat="server" Width="50%" 
                    ImageUrl="~/images/ESRfondoHomeElipse_3.jpg" 
                    PostBackUrl="videos.aspx?video=statements03"  />
            </td>
            <td>
                Declaraciones RSE 3</td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:ImageButton ID="imbStat0405" runat="server" Width="50%" 
                    ImageUrl="~/images/ESRfondoHomeElipse_3.jpg" 
                    PostBackUrl="videos.aspx?video=statements04-05"  />
            </td>
            <td>
                Declaraciones RSE 4 y 5</td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:ImageButton ID="imbStat0610" runat="server" Width="50%" 
                    ImageUrl="~/images/ESRfondoHomeElipse_3.jpg" 
                    PostBackUrl="videos.aspx?video=statements06-10"  />
            </td>
            <td>
                Declaraciones RSE 6 a 10 años</td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
