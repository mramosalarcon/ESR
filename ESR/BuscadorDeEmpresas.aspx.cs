using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ESR.Business;
using Microsoft.Reporting.WebForms;
using Microsoft.SharePoint;

public partial class buscadorDeEmpresas : System.Web.UI.Page
{
    /// <summary>
    /// Si viene como parámetro el idEmpresa se muestra la información de la empresa
    /// si no, solo se muestra la información de la empresa que esta logeada.
    /// </summary>
    /// <returns></returns>
    protected int GetIdEmpresa()
    {
        if (Request.Params["idEmpresa"] != null)
            return Convert.ToInt32(Request.Params["idEmpresa"].ToString());
        else
            return 0;
    }

    protected string GetIdUsuario()
    {
        if (Request.Params["idUsuario"] != null)
            return Request.Params["idUsuario"].ToString();
        else
            if (Session["idUsuario"].ToString() != string.Empty)
                return Session["idUsuario"].ToString();
            else
                return string.Empty;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Primero pregunta si trae el parámetro idEmpresa
            if (this.GetIdEmpresa() != 0)
            {
                // Si lo trae entonces hay que vincular al usuario con la empresa
                Empresa empresa = new Empresa();
                empresa.idEmpresa = this.GetIdEmpresa();
                empresa.idUsuario = this.GetIdUsuario();
                empresa.VincularUsuario();

                string sMensaje = "Su solicitud ha sido enviada, recibirá un correo cuando sea autorizada.";
                ClientScript.RegisterStartupScript(this.GetType(), "Mensaje de Vinculación",
                String.Format("alert('{0}');", sMensaje.Replace("'", "\'")), true);

                Response.Redirect("default.aspx", false);
            }
        }
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        if (txtBuscar.Text.Length > 1)
        {
            Response.Redirect("/_layouts/15/ReportServer/RSViewerPage.aspx?rv:RelativeReportUrl=/reportesESR/rpt_vincular_empresas.rdl&rp:criterio=" + txtBuscar.Text, false);
                
            // Set Processing Mode
            //ReportViewer1.ProcessingMode = ProcessingMode.Remote;

            //// Set report server and report path
            //ReportViewer1.ServerReport.ReportServerUrl = new
            //   Uri("http://esr.cemefi.org/reportserver");

            //ReportViewer1.ServerReport.ReportPath =
            //   "/Reports/rpt_vincular_empresas.rdl";

            //List<ReportParameter> paramList = new List<ReportParameter>();
            //paramList.Add(new ReportParameter("criterio", "%" + txtBuscar.Text + "%", false));
            ////paramList.Add(new ReportParameter("idUsuario", Session["idUsuario"].ToString(), false));

            //this.ReportViewer1.ServerReport.SetParameters(paramList);

            //// Process and render the report
            //ReportViewer1.ServerReport.Refresh();

            lblMensaje.Text = "De click en el logotipo de la empresa para enviarle la solicitud de vinculación al responsable  de dicho registro";

        }
    }
    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);

        SPWeb Web = SPContext.Current.Web;
        string strUrl =
            Web.ServerRelativeUrl + "/_catalogs/masterpage/seattle.master";

        this.MasterPageFile = strUrl;
    }
}
