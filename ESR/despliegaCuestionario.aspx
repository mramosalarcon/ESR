<%@ Page Language="C#" AutoEventWireup="true" Inherits="DespliegaCuestionario" Codebehind="despliegaCuestionario.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="Style/StyleSheet.css" rel="stylesheet" type="text/css" />
<script language="javascript" type="text/javascript">
// <!CDATA[

function Radio1_onclick() {

}

// ]]>
</script>
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
        <asp:CheckBox ID="CheckBox1" runat="server"  OnCheckedChanged="CheckBox1_CheckedChanged" />
        <asp:FileUpload ID="FileUpload1" runat="server" /><br />
        <input id="Radio1" type="radio" onclick="return Radio1_onclick()" />
        <br />
        <asp:RadioButton ID="RadioButton1" runat="server" OnCheckedChanged="RadioButton1_CheckedChanged" /></div>
    </form>
</body>
</html>
