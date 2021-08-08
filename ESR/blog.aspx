<%@ Page Language="C#" MasterPageFile="~/default.master" AutoEventWireup="true" CodeBehind="blog.aspx.cs" Inherits="ESR.blog" %>

<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" 
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" 
    Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>

<asp:Content ID="Header" ContentPlaceHolderID="PlaceHolderPageDescription" runat="server">
<script type="text/javascript" src="http://widgets.twimg.com/j/2/widget.js"></script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderMain" Runat="Server">
            
<script type="text/javascript">
new TWTR.Widget({
  version: 2,
  type: 'list',
  rpp: 15,
  interval: 10000,
  subject: 'Blog RSE Cemefi',
  width: 960,
  height: 400,
  theme: {
    shell: {
      background: '#95835f',
      color: '#ffffff'
    },
    tweets: {
        background: '#ffffff',
      color: '#000000',
      links: '#0000ff'
    }
  },
  features: {
    scrollbar: true,
    loop: true,
    live: true,
    hashtags: true,
    timestamp: true,
    avatars: true,
    behavior: 'all'
  }
}).render().setList('RSECemefi', 'blog-rse-cemefi').start();

</script>

</asp:Content>