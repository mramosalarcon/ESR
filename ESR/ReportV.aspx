﻿<%@ Page Language="C#"  MasterPageFile="~/default.master" AutoEventWireup="true" CodeBehind="ReportV.aspx.cs" Inherits="ESR.ReportV" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" 
    Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" 
    Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PlaceHolderMain" Runat="Server">
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" 
        DocumentMapCollapsed="True" DocumentMapWidth="100%" ShowFindControls="False" 
        ShowRefreshButton="False" ShowZoomControl="False" SizeToReportContent="True" 
        Width="960px" Font-Names="Verdana" Font-Size="8pt" 
    InteractiveDeviceInfos="(Collection)" ProcessingMode="Remote" 
    WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" 
        AsyncRendering="False">
        <serverreport reportpath="https://esrv1.cemefi.org/Reports/rpt_retro_esr" 
            reportserverurl="https://esrv1.cemefi.org/reportserver" />
    </rsweb:ReportViewer>
</asp:Content>