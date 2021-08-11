using System;
using System.Collections.Generic;
using Microsoft.SharePoint;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Configuration;

namespace ESR
{
    public partial class ReportV : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            try
            {
                ClientScript.RegisterStartupScript(this.GetType(), "documentos",
                    "$(\"a#docs\").attr('href', 'http://esr.cemefi.org/" + Session["idEmpresa"].ToString() + "/Documentos%20compartidos/Forms/AllItems.aspx');", true);

                // Set Processing Mode
                ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                ReportViewer1.AsyncRendering = true;

                // Set report server and report path
                ReportViewer1.ServerReport.ReportServerUrl = new Uri("http://esr.cemefi.org/_layouts/15/ReportServer");

                ReportViewer1.ServerReport.ReportPath =
                   "/reportesESR/" + Request.QueryString["report"].ToString() + ".rdl";

                List<ReportParameter> paramList = new List<ReportParameter>();
                if (Request["idCuestionario"].ToString() != string.Empty)
                {
                    paramList.Add(new ReportParameter("idCuestionario", Request["idCuestionario"].ToString(), false));
                }
                if (Request["idEmpresa"] != null)
                {
                    paramList.Add(new ReportParameter("idEmpresa", Request["idEmpresa"].ToString(), false));
                }

                this.ReportViewer1.ServerReport.SetParameters(paramList);

                // Process and render the report
                ReportViewer1.ServerReport.Refresh();
                ReportViewer1.DataBind();
            }
            catch (Exception ex)
            {
                using (StreamWriter sw = File.AppendText(ConfigurationManager.AppSettings["logFile"].ToString()))
                {
                    sw.AutoFlush = true;
                    sw.WriteLine("Fecha: " + DateTime.Now.ToString());
                    sw.WriteLine("Error en reportV.Page_Load(idEmpresa: " + Session["idEmpresa"].ToString() + "): " + ex.Message);
                    sw.Close();
                }
                throw new Exception("Error en reportV.Page_Load(idEmpresa: " + Session["idEmpresa"].ToString() + "): " + ex.Message);

            }
            //}

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
        
}
