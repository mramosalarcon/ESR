using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web.UI.WebControls;
using ESR.Business;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

public partial class misCuestionarios : System.Web.UI.Page
{
    protected int GetIdEmpresa()
    {
        if (Request.Params["idEmpresa"] == null)
            return Convert.ToInt32(Session["idEmpresa"].ToString());
        else
            return Convert.ToInt32(Request.Params["idEmpresa"].ToString());
    }
    private bool LoadSession()
    {
        bool result = false;
        //StreamWriter sw = File.AppendText("d:\\temp\\session.log");
        //sw.AutoFlush = true;
        try
        {
#if !Debug
            SPWeb theSite = SPControl.GetContextWeb(Context);
            SPUser theUser = theSite.CurrentUser;
            string[] usrid = theUser.LoginName.Split('|');
#else
            string[] usrid = { "", "", "mramos@tralcom.com" };
#endif
            Usuario usr = new Usuario();
            usr.idUsuario = usrid[2].ToString();
            //sw.WriteLine(DateTime.Now.ToString() + "Cargar usuario: " + usr.idUsuario);
            if (usr.CargaUsuario())
            {
                Session["idEmpresa"] = usr.idEmpresa;
                //sw.WriteLine("idEmpresa: " + Session["idEmpresa"].ToString());
                Session["perfil"] = usr.perfiles;
                //sw.WriteLine("perfil: " + Session["perfil"].ToString());
                Session["idUsuario"] = usr.idUsuario;
                //sw.WriteLine("idUsuario: " + Session["idUsuario"].ToString());
                Session["temas"] = usr.temas;
                //sw.WriteLine("temas: " + Session["temas"].ToString());
                Session["idPais"] = usr.pais;
                //sw.WriteLine("idPais: " + Session["idPais"].ToString());
                Empresa empresa2 = new Empresa();
				empresa2.idEmpresa = Convert.ToInt32(Session["idEmpresa"]);
				if (empresa2.cargaNombre())
				{
					Session["empresa"] = empresa2.nombre + " - " + empresa2.nombreCorto;
					result = true;
				}
            }
            else
            {
                //sw.WriteLine(DateTime.Now.ToString() + ": Usuario no existe: " + usr.idUsuario);
                /// Registrar el usuario con perfil 6
                //Usuario newUsr = new Usuario();
                //usr.Email = usr.idUsuario;
                //usr.idUsuario = usr.idUsuario;
                //usr.pais = 1;

                //if (!usr.Existe())
                //{
                //    usr.Guarda();
                //}
            }
            return result;
        }
        catch  //(Exception ex)
        {
            //sw.WriteLine(DateTime.Now.ToString() + ": " + ex.Message+ex.StackTrace);
            return result;
        }
        //finally
        //{
        //    sw.Close();
        //}
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    if (Session["idEmpresa"] == null || Session["idEmpresa"].ToString() == "")
                    {
                        if (LoadSession())
                        {

                            ClientScript.RegisterStartupScript(this.GetType(), "documentos",
                                "$(\"a#docs\").attr('href', 'https://esrv1.cemefi.org/" + Session["idEmpresa"].ToString() + "/Documentos%20compartidos/Forms/AllItems.aspx');", true);
                        }
                        else
                        {
                            Response.Redirect("~/login.aspx");
                        }
                    }
                    if (Session["empresa"] != null && Session["empresa"].ToString() != "")
                    {
                        lblEmpresa.Text = Session["empresa"].ToString();
                    }
                    else
                    {
                        lblEmpresa.Visible = false;
                    }
                    PopulateMenu();

                    Empresa empresa = new Empresa();
                    if (this.GetIdEmpresa() != 0)
                    {
                        empresa.idEmpresa = this.GetIdEmpresa();
                        empresa.Cargar();
                    }

                    if (Session["idUsuario"].ToString() != "owner@cemefi.org")
                    {
                        // Cargar los datos de la empresa para verificar los cuestionarios a los que tiene acceso
                        if (empresa.autorizada)
                        {
                            lblNombreEmpresa.Text = empresa.nombre;
                            if (base.Request.Params["nombreReporte"] != null)
                            {
                                switch (base.Request.Params["nombreReporte"])
                                {
                                    case "evidenciasPorCuestionario":
                                        lblOpcionSeleccionada.Text = "Reporte de evidencias por cuestionario";
                                        break;
                                    case "avance":
                                        lblOpcionSeleccionada.Text = "Reporte de avance de cuestionario";
                                        break;
                                    case "individual":
                                        lblOpcionSeleccionada.Text = "Reporte de ranking individual";
                                        break;
                                    case "indicadores":
                                        lblOpcionSeleccionada.Text = "Consulta de indicadores";
                                        break;
                                    case "retroalimentacion":
                                        lblOpcionSeleccionada.Text = "Reporte de resultados por cuestionario";
                                        break;
                                }
                            }
                            string text = Session["perfil"].ToString();
                            Cuestionario cuestionario = new Cuestionario();
                            cuestionario.idEmpresa = empresa.idEmpresa;
                            if (base.Request.Params["ranking"] != null || base.Request.Params["individual"] != null || 
                                (base.Request.Params["nombreReporte"] != null && 
                                base.Request.Params["nombreReporte"] != "indicadores" && 
                                base.Request.Params["nombreReporte"] != "avance"))
                            {
                                cuestionario.liberado = true;
                            }
                            else
                            {
                                if (base.Request.Params["nombreReporte"] == null || base.Request.Params["nombreReporte"] == "indicadores" ||
                                    (text.IndexOf("3") <= -1 && base.Request.Params["nombreReporte"] == "avance"))  // El evaluador tiene permitido revisar el avance 
                                {
                                    cuestionario.liberado = false;
                                }
                                else
                                {
                                    cuestionario.liberado = true;
                                }
                            }
                            DataSet dataSet = null;
                            
                            // Carga todos los cuestionarios autorizados.
                            dataSet = ((text.IndexOf("0") <= -1 && text.IndexOf("1") <= -1 && text.IndexOf("2") <= -1 && text.IndexOf("9") <= -1 && text.IndexOf("4") <= -1) ? cuestionario.CargaCuestionarios(text) : cuestionario.CargaTodos());
                            
                            
                            // Objetivo: Cargar todos los cuestionarios
                            // Ya se porque la comente, porque al evaluador le aparecen todos los cuestionarios disponibles.
                            // 06/07/2022 03:44
                            //dataSet = ((text.IndexOf("0") <= -1 && text.IndexOf("1") <= -1 && text.IndexOf("2") <= -1 && text.IndexOf("3") <= -1 && text.IndexOf("9") <= -1 && text.IndexOf("4") <= -1) ? cuestionario.CargaCuestionarios(text) : cuestionario.CargaTodos());
                            
                            if (dataSet.Tables["Cuestionario"].Rows.Count > 0)
                            {
                                ddlCuestionarios.DataSource = dataSet;
                                ddlCuestionarios.DataTextField = "nombre";
                                ddlCuestionarios.DataValueField = "idCuestionario";
                                ddlCuestionarios.DataBind();
                                if (base.Request.Params["idCuestionario"] != null && Convert.ToInt32(base.Request.Params["idCuestionario"].ToString()) > 60)
                                {
                                    ddlCuestionarios.SelectedIndex = ddlCuestionarios.Items.IndexOf(ddlCuestionarios.Items.FindByValue(base.Request.Params["idCuestionario"].ToString()));
                                }
                            }
                            else
                            {
                                lblEmpresa.Text = "Su empresa no tiene cuestionario disponible. Solicite acceso al cuestionario al área de RSE del Cemefi - tel. (55) 52768530 ext. 128, 156, 182, 111";
                                ddlCuestionarios.Visible = false;
                                btnIniciarCuestionario.Visible = false;
                                lblCuestionarios.Visible = false;
                                lblEmpresa.Visible = true;
                                lblNombreEmpresa.Visible = false;
                                lblOpcion.Visible = false;
                                lblOpcionSeleccionada.Visible = false;
                            }
                        }
                        else
                        {
                            lblEmpresa.Visible = false;
                            Cuestionario cuestionario2 = new Cuestionario();
                            string text2 = Session["perfil"].ToString();
                            if (text2.IndexOf("7") == -1 && text2.IndexOf("8") == -1)
                            {
                                lblOpcionSeleccionada.Text = "Consulta de indicadores";
                                ddlCuestionarios.DataSource = cuestionario2.CargaTodos();
                                ddlCuestionarios.DataTextField = "nombre";
                                ddlCuestionarios.DataValueField = "idCuestionario";
                                ddlCuestionarios.DataBind();
                            }
                            else
                            {
                                lblEmpresa.Text = "Esta empresa no cuenta con acceso a los cuestionarios de Autodiagnóstico ESR®.";
                                ddlCuestionarios.Visible = false;
                                btnIniciarCuestionario.Visible = false;
                                lblCuestionarios.Visible = false;
                                lblEmpresa.Visible = true;
                                lblNombreEmpresa.Visible = false;
                            }
                        }
                    }
                    else
                    {
                        Cuestionario cuestionario3 = new Cuestionario();
                        DataSet dataSet2 = cuestionario3.CargaTodos();
                        lblNombreEmpresa.Text = empresa.nombre;
                        if (dataSet2.Tables["Cuestionario"].Rows.Count > 0)
                        {
                            ddlCuestionarios.DataSource = dataSet2;
                            ddlCuestionarios.DataTextField = "nombre";
                            ddlCuestionarios.DataValueField = "idCuestionario";
                            ddlCuestionarios.DataBind();
                        }
                    }
                }
                catch
                {
                    base.ClientScript.RegisterStartupScript(GetType(), "App Error", "alert('Hubo un error al cargar la página, por favor inicie sesión nuevamente.');", addScriptTags: true);
                }
                return;
            }
            string text3 = "Para acceder al autodiagnóstico, primero debe iniciar sesión";
            base.ClientScript.RegisterStartupScript(GetType(), "LoginError", string.Format("alert('{0}');", text3.Replace("'", "'")), addScriptTags: true);
            btnIniciarCuestionario.Visible = false;
            base.Response.Redirect("login.aspx");
        }
    }
        protected void btnIniciarCuestionario_Click(object sender, EventArgs e)
    {
        Session["idCuestionario"] = ddlCuestionarios.SelectedValue;

        string nombreReporte = "";
        if (Request.Params["nombreReporte"] != null)
            nombreReporte = Request.Params["nombreReporte"];
        else
            if (lblOpcionSeleccionada.Text == "Consulta de indicadores")
                nombreReporte = "indicadores";

        switch (nombreReporte)
        {
            case "avance":
                if (base.Request.Params["evidencias"] == null)
                {
                    Response.Redirect("diagnostico.aspx?Content=avanceDeDiagnostico&idCuestionario=" + ddlCuestionarios.SelectedValue);
                }
                else if (Request.Params["evaluacion"] == null)
                {
                    if (Request.Params["idEmpresa"] == null)
                    {
                        Response.Redirect("diagnostico.aspx?Content=avanceDeDiagnostico&idCuestionario=" + ddlCuestionarios.SelectedValue + "&evidencias=true");
                    }
                    else
                    {
                        Response.Redirect("diagnostico.aspx?Content=avanceDeDiagnostico&idCuestionario=" + ddlCuestionarios.SelectedValue + "&evidencias=true&idEmpresa=" + base.Request.Params["idEmpresa"].ToString());
                    }
                }
                else if (Request.Params["idEmpresa"] == null)
                {
                    base.Response.Redirect("diagnostico.aspx?Content=avanceDeDiagnostico&idCuestionario=" + ddlCuestionarios.SelectedValue + "&evidencias=true&evaluacion=true");
                }
                else
                {
                    base.Response.Redirect("diagnostico.aspx?Content=avanceDeDiagnostico&idCuestionario=" + ddlCuestionarios.SelectedValue + "&evidencias=true&evaluacion=true&idEmpresa=" + base.Request.Params["idEmpresa"].ToString());
                }
                break;
            case "individual":
                if (base.Request.Params["idEmpresa"] == null)
                {
                    base.Response.Redirect("reportes.aspx?content=rep_individual&idCuestionario=" + ddlCuestionarios.SelectedValue);
                }
                else
                {
                    base.Response.Redirect("reportes.aspx?content=rep_individual&idEmpresa=" + base.Request.Params["idEmpresa"].ToString() + "&idCuestionario=" + ddlCuestionarios.SelectedValue);
                }
                break;
            case "temas":
                //                Response.Redirect("ReportV.aspx?report=rpt_Evaluacion_Tema&idCuestionario=" + ddlCuestionarios.SelectedValue);
                Response.Redirect("/_layouts/15/ReportServer/RSViewerPage.aspx?rv:RelativeReportUrl=/reportesESR/rpt_Evaluacion_Tema.rdl&rp:idCuestionario=" + ddlCuestionarios.SelectedValue);
                break;
            case "indicadores":
                Response.Redirect("/_layouts/15/ReportServer/RSViewerPage.aspx?rv:RelativeReportUrl=/reportesESR/rpt_Consulta_Indicadores.rdl&rp:idCuestionario=" + ddlCuestionarios.SelectedValue + "&rp:idEmpresa=" + this.GetIdEmpresa());
                //Response.Redirect("ReportV.aspx?report=rpt_Consulta_Indicadores&idCuestionario=" + ddlCuestionarios.SelectedValue);
                break;
            case "liberado":
                Response.Redirect("/_layouts/15/ReportServer/RSViewerPage.aspx?rv:RelativeReportUrl=/reportesESR/rpt_Empresas_Cuestionario_Liberado.rdl&rp:idCuestionario=" + ddlCuestionarios.SelectedValue);
                //Response.Redirect("ReportV.aspx?report=rpt_Empresas_Cuestionario_Liberado&idCuestionario=" + ddlCuestionarios.SelectedValue);
                break;
            case "registradas":
                Response.Redirect("/_layouts/15/ReportServer/RSViewerPage.aspx?rv:RelativeReportUrl=/reportesESR/rpt_Empresas_Registradas.rdl&rp:idCuestionario=" + ddlCuestionarios.SelectedValue);
                //Response.Redirect("ReportV.aspx?report=rpt_Empresas_Registradas&idCuestionario=" + ddlCuestionarios.SelectedValue);
                break;
            case "retroalimentacion":

                if (ddlCuestionarios.SelectedItem.Text.ToUpper().Contains("PERÚ"))
                {
                    if (ddlCuestionarios.SelectedItem.Text.ToUpper().Contains("ESR"))
                    {
                        //Response.Redirect("~/ReportServer/Pages/ReportViewer.aspx?%2fReports%2frpt_retro_ESR_Peru&rs:Command=Render&idEmpresa=" + this.GetIdEmpresa());
                        Response.Redirect("/_layouts/15/ReportServer/RSViewerPage.aspx?rv:RelativeReportUrl=/reportesESR/rpt_retro_ESR_Peru.rdl&rp:idEmpresa=" + this.GetIdEmpresa());
                        //Response.Redirect("ReportV.aspx?report=rpt_retro_ESR_Peru&idCuestionario=" + ddlCuestionarios.SelectedValue + "&idEmpresa=" + this.GetIdEmpresa());
                    }
                    else
                    {
                        if (ddlCuestionarios.SelectedItem.Text.ToUpper().Contains("PYME"))
                        {
                            Response.Redirect("/_layouts/15/ReportServer/RSViewerPage.aspx?rv:RelativeReportUrl=/reportesESR/rpt_retro_PYME_Peru.rdl&rp:idEmpresa=" + this.GetIdEmpresa());
                            //Response.Redirect("ReportV.aspx?report=rpt_retro_PYME_Peru&idCuestionario=" + ddlCuestionarios.SelectedValue + "&idEmpresa=" + this.GetIdEmpresa());
                        }
                    }
                }
                else
                {
                    if (ddlCuestionarios.SelectedItem.Text.ToUpper().Contains("PYME"))
                    {
                        if (!ddlCuestionarios.SelectedItem.Text.ToUpper().Contains("2014"))
                        {

                            Response.Redirect("/_layouts/15/ReportServer/RSViewerPage.aspx?rv:RelativeReportUrl=/reportesESR/rpt_retro_PYME.rdl&rp:idCuestionario=" + ddlCuestionarios.SelectedValue + "&rp:idEmpresa=" + this.GetIdEmpresa());
                            
                            //Response.Redirect("ReportV.aspx?report=rpt_retro_PYME&idCuestionario=" + ddlCuestionarios.SelectedValue + "&idEmpresa=" + this.GetIdEmpresa());
                        }
                        else
                        {
                            Response.Redirect("/_layouts/15/ReportServer/RSViewerPage.aspx?rv:RelativeReportUrl=/reportesESR/rpt_retro_PYME_2014.rdl&rp:idCuestionario=" + ddlCuestionarios.SelectedValue + "&rp:idEmpresa=" + this.GetIdEmpresa());
                            //Response.Redirect("ReportV.aspx?report=rpt_retro_PYME_2014&idCuestionario=" + ddlCuestionarios.SelectedValue + "&idEmpresa=" + this.GetIdEmpresa());
                        }

                    }
                    else
                    {
                        if (ddlCuestionarios.SelectedItem.Text.Contains("ESR"))
                        {
                            if (ddlCuestionarios.SelectedItem.Text.Contains("11+ años"))
                            {
                                if (!ddlCuestionarios.SelectedItem.Text.Contains("2014"))
                                {
                                    Response.Redirect("/_layouts/15/ReportServer/RSViewerPage.aspx?rv:RelativeReportUrl=/reportesESR/rpt_retro_ESR.rdl&rp:idCuestionario=" + ddlCuestionarios.SelectedValue + "&rp:idEmpresa=" + this.GetIdEmpresa());
                                    //Response.Redirect("ReportV.aspx?report=rpt_retro_ESR_GC&idCuestionario=" + ddlCuestionarios.SelectedValue + "&idEmpresa=" + this.GetIdEmpresa());
                                }
                                else
                                {
                                    Response.Redirect("~/ReportServer/Pages/ReportViewer.aspxrpt_retro_ESR_GC_2014.rdl&rp:idCuestionario=" + ddlCuestionarios.SelectedValue + "&rp:idEmpresa=" + this.GetIdEmpresa());
                                    //Response.Redirect("ReportV.aspx?report=rpt_retro_ESR_GC_2014&idCuestionario=" + ddlCuestionarios.SelectedValue + "&idEmpresa=" + this.GetIdEmpresa());
                                }
                            }
                            else
                            {
                                if (!ddlCuestionarios.SelectedItem.Text.Contains("2014"))
                                {
                                    //Response.Redirect("~/ReportServer/Pages/ReportViewer.aspx?%2fReports%2frpt_retro_ESR&rs:Command=Render&idCuestionario=" + ddlCuestionarios.SelectedValue + "&idEmpresa=" + this.GetIdEmpresa());
                                    Response.Redirect("/_layouts/15/ReportServer/RSViewerPage.aspx?rv:RelativeReportUrl=/reportesESR/rpt_retro_ESR.rdl&rp:idCuestionario=" + ddlCuestionarios.SelectedValue + "&rp:idEmpresa=" + this.GetIdEmpresa());
                                    //Response.Redirect("ReportV.aspx?report=rpt_retro_ESR&idCuestionario=" + ddlCuestionarios.SelectedValue + "&idEmpresa=" + this.GetIdEmpresa());
                                }
                                else
                                {
                                    Response.Redirect("/_layouts/15/ReportServer/RSViewerPage.aspx?rv:RelativeReportUrl=/reportesESR/rpt_retro_ESR_2014.rdl&rp:idCuestionario=" + ddlCuestionarios.SelectedValue + "&rp:idEmpresa=" + this.GetIdEmpresa());
                                    //Response.Redirect("ReportV.aspx?report=rpt_retro_ESR_2014&idCuestionario=" + ddlCuestionarios.SelectedValue + "&idEmpresa=" + this.GetIdEmpresa());
                                }
                            }
                        }
                        else
                        {
                            if (ddlCuestionarios.SelectedItem.Text.Contains("Consumo"))
                            {
                                if (!ddlCuestionarios.SelectedItem.Text.Contains("2014"))
                                {
                                    Response.Redirect("/_layouts/15/ReportServer/RSViewerPage.aspx?rv:RelativeReportUrl=/reportesESR/rpt_retro_CR.rdl&rp:idCuestionario=" + ddlCuestionarios.SelectedValue + "&rp:idEmpresa=" + this.GetIdEmpresa());
                                    //Response.Redirect("ReportV.aspx?report=rpt_retro_CR&idCuestionario=" + ddlCuestionarios.SelectedValue + "&idEmpresa=" + this.GetIdEmpresa());
                                }
                                else
                                {
                                    Response.Redirect("/_layouts/15/ReportServer/RSViewerPage.aspx?rv:RelativeReportUrl=/reportesESR/rpt_retro_CR_2014.rdl&rp:idCuestionario=" + ddlCuestionarios.SelectedValue + "&rp:idEmpresa=" + this.GetIdEmpresa());
                                    //Response.Redirect("ReportV.aspx?report=rpt_retro_CR_2014&idCuestionario=" + ddlCuestionarios.SelectedValue + "&idEmpresa=" + this.GetIdEmpresa());
                                }
                            }
                        }
                    }
                }
                break;
            case "EmpresasSinEvidencia":
                Response.Redirect("/_layouts/15/ReportServer/RSViewerPage.aspx?rv:RelativeReportUrl=/reportesESR/rpt_Empresas_Sin_Evidencias.rdl&rp:idCuestionario=" + ddlCuestionarios.SelectedValue);
                //Response.Redirect("ReportV.aspx?report=rpt_Empresas_Sin_Evidencias&idCuestionario=" + ddlCuestionarios.SelectedValue);
                break;
            case "EvidenciasPorCuestionario":
                Response.Redirect("/_layouts/15/ReportServer/RSViewerPage.aspx?rv:RelativeReportUrl=/reportesESR/rpt_evidencias_cuestionario.rdl&rp:idCuestionario=" + ddlCuestionarios.SelectedValue + "&rp:idEmpresa=" + this.GetIdEmpresa());
                //Response.Redirect("ReportV.aspx?report=rpt_evidencias_cuestionario&idCuestionario=" + ddlCuestionarios.SelectedValue + "&idEmpresa=" + this.GetIdEmpresa());
                break;
            case "IndicadoresCuestionario":
                Response.Redirect("/_layouts/15/ReportServer/RSViewerPage.aspx?rv:RelativeReportUrl=/reportesESR/rpt_indicadores_cuestionario.rdl&rp:idCuestionario=" + ddlCuestionarios.SelectedValue);
                //Response.Redirect("ReportV.aspx?report=rpt_indicadores_cuestionario&idCuestionario=" + ddlCuestionarios.SelectedValue);
                break;
        }


        if (Request.Params["sector"] == null)
        {
            if (Request.Params["tamano"] == null)
            {
                if (Request.Params["composicionK"] == null)
                {
                    if (Request.Params["avance"] == null)
                    {
                        if (Request.Params["individual"] == null)
                        {
                            if (Request.Params["ranking"] == null)
                            {
                                if (Request.Params["ReadOnly"] == null)
                                {
                                    // Acceso al cuestionario
                                    Response.Redirect("diagnostico.aspx?idCuestionario=" + ddlCuestionarios.SelectedValue);
                                }
                                else
                                {
                                    Response.Redirect("diagnostico.aspx?idCuestionario=" + ddlCuestionarios.SelectedValue + "&ReadOnly=true");
                                }
                            }
                            else
                            {
                                Response.Redirect("reportes.aspx?content=rep_ranking&idCuestionario=" + ddlCuestionarios.SelectedValue);
                            }
                        }
                        else
                        {
                            if (Request.Params["idEmpresa"] == null)
                            {
                                Response.Redirect("reportes.aspx?content=rep_individual&idCuestionario=" + ddlCuestionarios.SelectedValue);
                            }
                            else
                            {
                                Response.Redirect("reportes.aspx?content=rep_individual&idEmpresa=" + Request.Params["idEmpresa"].ToString() + "&idCuestionario=" + ddlCuestionarios.SelectedValue);
                            }
                        }
                    }
                    else
                    {
                        if (Request.Params["evidencias"] == null)
                        {
                            Response.Redirect("diagnostico.aspx?Content=avanceDeDiagnostico&idCuestionario=" + ddlCuestionarios.SelectedValue);
                        }
                        else
                        {
                            if (Request.Params["evaluacion"] == null)
                            {
                                if (Request.Params["idEmpresa"] == null)
                                {
                                    Response.Redirect("diagnostico.aspx?Content=avanceDeDiagnostico&idCuestionario=" + ddlCuestionarios.SelectedValue + "&evidencias=true");
                                }
                                else
                                {
                                    Response.Redirect("diagnostico.aspx?Content=avanceDeDiagnostico&idCuestionario=" + ddlCuestionarios.SelectedValue + "&evidencias=true&idEmpresa=" + Request.Params["idEmpresa"].ToString());
                                }
                            }
                            else
                            {
                                if (Request.Params["idEmpresa"] == null)
                                {
                                    Response.Redirect("diagnostico.aspx?Content=avanceDeDiagnostico&idCuestionario=" + ddlCuestionarios.SelectedValue + "&evidencias=true&evaluacion=true");
                                }
                                else
                                {
                                    Response.Redirect("diagnostico.aspx?Content=avanceDeDiagnostico&idCuestionario=" + ddlCuestionarios.SelectedValue + "&evidencias=true&evaluacion=true&idEmpresa=" + Request.Params["idEmpresa"].ToString());
                                }
                            }
                        }
                    }
                }
                else
                {
                    Response.Redirect("/_layouts/15/ReportServer/RSViewerPage.aspx?rv:RelativeReportUrl=/reportesESR/rpt_rankingComposicionK.rdl&rp:idCuestionario=" + ddlCuestionarios.SelectedValue);
                    //Response.Redirect("ReportV.aspx?report=rpt_rankingComposicionK&idCuestionario=" + ddlCuestionarios.SelectedValue);
                }
            }
            else
            {
                Response.Redirect("/_layouts/15/ReportServer/RSViewerPage.aspx?rv:RelativeReportUrl=/reportesESR/rpt_rankingTamano.rdl&rp:idCuestionario=" + ddlCuestionarios.SelectedValue);
                //Response.Redirect("ReportV.aspx?report=rpt_rankingTamano&idCuestionario=" + ddlCuestionarios.SelectedValue);
            }
        }
        else
        {
            //Response.Redirect("~/ReportServer/Pages/ReportViewer.aspx?%2fReports%2frpt_rankingSector&rs:Command=Render&idCuestionario=" + ddlCuestionarios.SelectedValue);
            Response.Redirect("/_layouts/15/ReportServer/RSViewerPage.aspx?rv:RelativeReportUrl=/reportesESR/rpt_rankingSector.rdl&rp:idCuestionario=" + ddlCuestionarios.SelectedValue);
            
            //Response.Redirect("ReportV.aspx?report=rpt_rankingSector&idCuestionario=" + ddlCuestionarios.SelectedValue);
        }
    }

    private void PopulateMenu()
    {
        try
        {
            // C�digo temporal para cambiar la imagen del bot�n del master page
            //
            //((ImageButton)this.Master.FindControl("imbDiagnostico")).ImageUrl = "images/ESRbotonMenuInteriorOver_Diagnostico.jpg";
            //((ImageButton)this.Master.FindControl("imbDiagnostico")).Attributes["onmouseout"] = "'images/ESRbotonMenuInteriorOver_Diagnostico.jpg'";

            ESR.Business.Menu menuOpciones = new ESR.Business.Menu(Session["perfil"].ToString());
            DataSet dsMenu = menuOpciones.Carga(ConfigurationManager.AppSettings["misCuestionarios"].ToString());
            Table tblMenu = new Table();

            //tblMenu.Width = new Unit(50, UnitType.Percentage);

            TableRow trwMenu = new TableRow();
            trwMenu.Height = new Unit(30, UnitType.Pixel);

            foreach (DataRow masterRow in dsMenu.Tables["Menu"].Rows)
            {
                if (masterRow["habilitado"].ToString() == "True")
                {
                    TableCell tclMenu = new TableCell();

                    System.Web.UI.WebControls.Menu opcMenu = new System.Web.UI.WebControls.Menu();
                    opcMenu.ForeColor = Color.White;
                    opcMenu.BackColor = Color.Orange; //Color.FromArgb(06, 05, 00);

                    opcMenu.Orientation = Orientation.Horizontal;

                    opcMenu.DynamicMenuItemStyle.ForeColor = Color.White;
                    opcMenu.DynamicMenuItemStyle.BackColor = Color.Orange; //Color.FromArgb(06, 05, 00);

                    opcMenu.DynamicHoverStyle.ForeColor = Color.White;
                    opcMenu.DynamicHoverStyle.BackColor = Color.FromArgb(149, 131, 95);

                    MenuItem itmMenu = new MenuItem();
                    itmMenu.Text = masterRow["nombre"].ToString();

                    if (masterRow["nombre"].ToString() == "Documentos")
                        itmMenu.NavigateUrl = masterRow["url"].ToString() + Session["idEmpresa"].ToString();
                    else
                        itmMenu.NavigateUrl = masterRow["url"].ToString();

                    foreach (DataRow childRow in masterRow.GetChildRows("Children"))
                    {
                        MenuItem itmSubMenu = new MenuItem();
                        itmSubMenu.Text = childRow["nombre"].ToString();
                        itmSubMenu.NavigateUrl = childRow["url"].ToString();

                        itmMenu.ChildItems.Add(itmSubMenu);
                    }
                    opcMenu.Items.Add(itmMenu);
                    tclMenu.Controls.Add(opcMenu);
                    trwMenu.Cells.Add(tclMenu);
                }
            }
            tblMenu.Rows.Add(trwMenu);
            panMenu.Controls.Add(tblMenu);
        }
        catch (Exception ex)
        {
            using (StreamWriter sw = File.AppendText(ConfigurationManager.AppSettings["logFile"].ToString()))
            {
                sw.AutoFlush = true;
                sw.WriteLine("Fecha: " + DateTime.Now.ToString());
                sw.WriteLine("Error en misCuestionarios.PopulateMenu(" + Session["idEmpresa"].ToString() + " ): " + ex.Message);
                sw.Close();
            }
            throw new Exception("Error en misCuestionarios.PopulateMenu(" + Session["idEmpresa"].ToString() + " ): " + ex.Message);

        }
    }
    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
#if !Debug
        SPWeb Web = SPContext.Current.Web;
        string strUrl =
            Web.ServerRelativeUrl + "/_catalogs/masterpage/seattle.master";

        this.MasterPageFile = strUrl;
#endif
    }
}
