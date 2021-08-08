using System;
using System.Data;
using System.Web.UI.WebControls;
using ESR.Business;

public partial class avanceDeDiagnostico : System.Web.UI.UserControl
{
    
    Label lblError = new Label();
    CheckBox chkConfirmaLiberacion = new CheckBox();

    protected int GetIdEmpresa()
    {
        if (Request.Params["idEmpresa"] == null)
            return Convert.ToInt32(Session["idEmpresa"].ToString());
        else
            return Convert.ToInt32(Request.Params["idEmpresa"].ToString());
    }

    private int GetIdCuestionario()
    {
        int id = 0;
        if (lblIdCuestionario.Text.Trim() != "")
            id = Convert.ToInt32(lblIdCuestionario.Text);
        return id;
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        Cuestionario cuestionario = new Cuestionario();
        cuestionario.idEmpresa = this.GetIdEmpresa();
        cuestionario.idCuestionario = this.GetIdCuestionario();
        
        DataSet respuestas = cuestionario.CargaAvance(true);

        lblTitulo.Text = respuestas.Tables["Empresa"].Rows[0]["nombre"].ToString();
        lblTitulo.Visible = true;
        lblNombreDeCuestionario.Text = respuestas.Tables["Empresa"].Rows[0]["Cuestionario"].ToString();

        Table tblAvance = new Table();

        bool flgLiberaCuestionario = true;
        int iContadorIndicadores = 0;
        foreach (DataRow drTema in respuestas.Tables["Tema"].Rows)
        {
            TableRow tblRow = new TableRow();
            TableCell tblCellTema = new TableCell();
            TableCell tblCellTemaCalificacion = new TableCell();

            tblCellTema.BorderWidth = 1;
            tblCellTemaCalificacion.BorderWidth = 1;

            tblCellTema.BackColor = System.Drawing.Color.Teal;
            tblCellTemaCalificacion.BackColor = System.Drawing.Color.Teal;
            tblCellTema.ForeColor = System.Drawing.Color.White;
            tblCellTemaCalificacion.ForeColor = System.Drawing.Color.White;
            tblCellTema.Text = drTema["Tema"].ToString();
            tblCellTemaCalificacion.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;

            tblRow.Cells.Add(tblCellTema);
            tblRow.Cells.Add(tblCellTemaCalificacion);

            if (Request.Params["evidencias"] != null)
            {
                TableCell tblCellTituloTE = new TableCell();
                tblCellTituloTE.BorderWidth = 1;
                tblCellTituloTE.ColumnSpan = 7;
                tblCellTituloTE.HorizontalAlign = HorizontalAlign.Center;
                tblCellTituloTE.BackColor = System.Drawing.Color.Teal;
                tblCellTituloTE.ForeColor = System.Drawing.Color.White;
                tblCellTituloTE.Text = "Tipos de Evidencia";
                tblRow.Cells.Add(tblCellTituloTE);

                if (Request.Params["evaluacion"] != null)
                {
                    TableCell tblCellTituloEvaluacion = new TableCell();
                    tblCellTituloEvaluacion.BorderWidth = 1;
                    tblCellTituloEvaluacion.ColumnSpan = 3;
                    tblCellTituloEvaluacion.HorizontalAlign = HorizontalAlign.Center;
                    tblCellTituloEvaluacion.BackColor = System.Drawing.Color.Teal;
                    tblCellTituloEvaluacion.ForeColor = System.Drawing.Color.White;
                    tblCellTituloEvaluacion.Text = "Evaluación";
                    tblRow.Cells.Add(tblCellTituloEvaluacion);
                }
            }

            //TableCell tblCellTituloEvidencias = new TableCell();
            //tblCellTituloEvidencias.BorderWidth = 1;
            //tblCellTituloEvidencias.ColumnSpan = 3;
            //tblCellTituloEvidencias.HorizontalAlign = HorizontalAlign.Center;
            //tblCellTituloEvidencias.BackColor = System.Drawing.Color.Teal;
            //tblCellTituloEvidencias.ForeColor = System.Drawing.Color.White;
            //tblCellTituloEvidencias.Text = "Evidencias del ámbito: " + respuestas.Tables["Evidencia_Indicador"].Select("idTema = " + drTema["idTema"].ToString()).Length.ToString();
            //tblRow.Cells.Add(tblCellTituloEvidencias);

            tblAvance.Rows.Add(tblRow);

            int iContadorIndicadoresTema = 0;
            int iContadorRespuestasTema = 0;
            foreach (DataRow drSubtema in drTema.GetChildRows("S"))
            {
                TableRow tblRowSubtema = new TableRow();
                TableCell tblCellSubtema = new TableCell();
                TableCell tblCellSubtemaCalificacion = new TableCell();

                tblCellSubtema.BorderWidth = 1;
                tblCellSubtemaCalificacion.BorderWidth = 1;

                tblCellSubtema.BackColor = System.Drawing.Color.SteelBlue;
                tblCellSubtemaCalificacion.BackColor = System.Drawing.Color.SteelBlue;
                tblCellSubtema.ForeColor = System.Drawing.Color.White;
                tblCellSubtemaCalificacion.ForeColor = System.Drawing.Color.White;
                tblCellSubtema.Text = drSubtema["Subtema"].ToString();
                tblCellSubtemaCalificacion.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;

                tblRowSubtema.Cells.Add(tblCellSubtema);
                tblRowSubtema.Cells.Add(tblCellSubtemaCalificacion);
                tblAvance.Rows.Add(tblRowSubtema);

                iContadorIndicadores = 0;
                int iContadorRespuestas = 0;
                foreach (DataRow rowIndi in respuestas.Tables["Indicador"].Select("idTema = " + drTema["idTema"].ToString() + " and idSubtema = " + drSubtema["idSubtema"].ToString()))
                {
                    TableRow tblRowIndicador = new TableRow();
                    TableCell tblCellIndicador = new TableCell();
                    TableCell tblCellIndicadorCalificacion = new TableCell();
                    //TableCell tblCellIndicadorPrecalificacion = new TableCell();
                    //TableCell tblCellEvaluacion = new TableCell();

                    tblCellIndicador.BackColor = System.Drawing.Color.Beige;
                    tblCellIndicadorCalificacion.BackColor = System.Drawing.Color.Beige;
                    tblCellIndicadorCalificacion.VerticalAlign = VerticalAlign.Middle;

                    //Si viene el parametro del id de Empresa se muestra el de la empresa que viene como parámetro
                    if (Request.Params["idEmpresa"] == null)
                        tblCellIndicador.Text = "<a href=\"diagnostico.aspx?Content=visorDelCuestionario&idTema=" + drTema["idTema"].ToString() + "&idSubtema=" + drSubtema["idSubtema"].ToString() + "&idIndicador="
                            + rowIndi["idIndicador"].ToString() + "&idCuestionario=" + lblIdCuestionario.Text + "&cuestionario=" + lblNombreDeCuestionario.Text + "&Ordinal=" + rowIndi["ordinal"].ToString() + "\">" + rowIndi["ordinal"].ToString() + ". " + rowIndi["Indicador"].ToString() + "</a>";
                    else
                        tblCellIndicador.Text = "<a href=\"diagnostico.aspx?Content=visorDelCuestionario&idTema=" + drTema["idTema"].ToString() + "&idSubtema=" + drSubtema["idSubtema"].ToString() + "&idIndicador="
                            + rowIndi["idIndicador"].ToString() + "&idCuestionario=" + lblIdCuestionario.Text + "&idEmpresa=" + Request.Params["idEmpresa"].ToString() + "&cuestionario=" + lblNombreDeCuestionario.Text + "&Ordinal=" + rowIndi["ordinal"].ToString() + "&bloqueado=true\">" + rowIndi["ordinal"].ToString() + ". " + rowIndi["Indicador"].ToString() + "</a>";
                    
                    iContadorIndicadores++;
                    iContadorIndicadoresTema++;
                    DataRow[] rowResp = respuestas.Tables["Respuesta_Indicador"].Select("idTema = " + drTema["idTema"].ToString() + " and idIndicador = " + rowIndi["idIndicador"].ToString());
                    tblAvance.Rows.Add(tblRowIndicador);
                    if (rowResp.GetLength(0) > 0)
                    {
                        int iContadorIncisos = 0;
                        int iContadorRespuestasIncisos = 0;

                        //foreach (DataRow rowInciso in respuestas.Tables["Inciso"].Select("idTema = " + drTema["idTema"].ToString() + " and idIndicador = " + rowIndi["idIndicador"].ToString()))
                        //{
                        //    TableRow tblRowIndicadorInciso = new TableRow();
                        //    TableCell tblCellIndicadorInciso = new TableCell();
                        //    TableCell tblCellIndicadorIncisoCalificacion = new TableCell();

                        //    // Tiene incisos
                        //    // ¿Fue respondido ese inciso?
                        //    DataRow[] rowRespInciso = respuestas.Tables["Respuesta_Inciso"].Select("idTema = " + drTema["idTema"].ToString() + " and idIndicador = " + rowIndi["idIndicador"].ToString() + " and idInciso = '" + rowInciso["idInciso"].ToString() + "'");
                        //    if (rowRespInciso.GetLength(0) > 0)
                        //    {
                        //        iContadorRespuestasIncisos++;
                        //    }
                        //    iContadorIncisos++;

                        //    tblCellIndicadorInciso.Text = "&nbsp&nbsp&nbsp&nbsp" + rowInciso["idInciso"].ToString() + ". " + rowInciso["descInc"].ToString();

                        //    if (rowRespInciso.GetLength(0) > 0)
                        //    {
                        //        tblCellIndicadorIncisoCalificacion.Text = rowRespInciso[0]["respuesta"].ToString();
                        //        tblCellIndicadorIncisoCalificacion.ForeColor = System.Drawing.Color.Black;
                        //    }
                        //    else
                        //    {
                        //        if (Convert.ToInt32(rowResp[0]["idTipoRespuesta"]) > 2)
                        //        {
                        //            /// MRA: Checar esta condición
                        //            /// 13/11/2011 21:53
                        //            tblCellIndicadorIncisoCalificacion.Text = "Sin respuesta";
                        //            tblCellIndicadorIncisoCalificacion.ForeColor = System.Drawing.Color.Red;
                        //        }
                        //    }

                        //    tblCellIndicadorInciso.BackColor = System.Drawing.Color.Wheat;
                        //    tblCellIndicadorIncisoCalificacion.BackColor = System.Drawing.Color.Wheat;

                        //    tblRowIndicadorInciso.Cells.Add(tblCellIndicadorInciso);
                        //    tblRowIndicadorInciso.Cells.Add(tblCellIndicadorIncisoCalificacion);
                            
                        //    // Aqui vamos: MRA
                        //    // 22/07/2008
                        //    if (Request.Params["evidencias"] != null)
                        //    {
                        //        foreach (DataRow drTipoEvidencia in respuestas.Tables["Tipo_Evidencia"].Rows)
                        //        {
                        //            TableCell tblCellTipoEvidenciaInciso = new TableCell();
                        //            tblCellTipoEvidenciaInciso.BorderWidth = 1;
                        //            tblCellTipoEvidenciaInciso.BackColor = System.Drawing.Color.Wheat;

                        //            bool tieneArchivo = false;

                        //            // Hay que buscar que en el indicador
                        //            foreach (DataRow drEvidencia in respuestas.Tables["Evidencia_Inciso"].Select("idTema = " + drTema["idTema"].ToString() + " and idIndicador = " + rowIndi["idIndicador"].ToString() + " and idInciso = '" + rowInciso["idInciso"].ToString() + "'"))
                        //            {
                        //                //UpdatePanel upTipoEvidenciaInciso = new UpdatePanel();
                        //                //upTipoEvidencia.Triggers.Add

                        //                if (drTipoEvidencia["idTipoEvidencia"].ToString() == drEvidencia["idTipoEvidencia"].ToString())
                        //                {
                        //                    ImageButton cumple = new ImageButton();
                        //                    cumple.ImageUrl = "~/images/gridActulizar.gif";
                        //                    cumple.Click += new System.Web.UI.ImageClickEventHandler(this.btnCambiarTipoDeEvidenciaInciso_Click);
                        //                    tblCellTipoEvidenciaInciso.HorizontalAlign = HorizontalAlign.Center;
                        //                    tblCellTipoEvidenciaInciso.VerticalAlign = VerticalAlign.Middle;
                        //                    tblCellTipoEvidenciaInciso.Controls.Add(cumple);
                        //                    //upTipoEvidenciaInciso.ContentTemplateContainer.Controls.Add(cumple);
                        //                    tieneArchivo = true;
                        //                    break;
                        //                }
                        //                //tblCellTipoEvidenciaInciso.Controls.Add(upTipoEvidenciaInciso);
                        //            }
                        //            if (!tieneArchivo)
                        //            {
                        //                ImageButton noTieneArchivoInciso = new ImageButton();
                        //                noTieneArchivoInciso.ImageUrl = "~/images/blank.gif";
                        //                noTieneArchivoInciso.Click += new System.Web.UI.ImageClickEventHandler(this.btnCambiarTipoDeEvidenciaInciso_Click);
                        //                tblCellTipoEvidenciaInciso.HorizontalAlign = HorizontalAlign.Center;
                        //                tblCellTipoEvidenciaInciso.VerticalAlign = VerticalAlign.Middle;
                        //                tblCellTipoEvidenciaInciso.Controls.Add(noTieneArchivoInciso);
                        //            }
                        //            tblRowIndicadorInciso.Cells.Add(tblCellTipoEvidenciaInciso);
                        //        }
                        //    }
                        //    tblAvance.Rows.Add(tblRowIndicadorInciso);
                        //}

                        if (iContadorIncisos != iContadorRespuestasIncisos)
                        {
                            iContadorRespuestasIncisos++;
                            iContadorIncisos++;
                            float fPorcentajeRespuestasIncisos = (iContadorRespuestasIncisos * 100) / iContadorIncisos;

                            if (Convert.ToInt16(rowResp[0].ItemArray[2]) == 1 || Convert.ToInt16(rowResp[0].ItemArray[2]) == 2)
                            {
                                tblCellIndicadorCalificacion.Text = rowResp[0]["respuesta"].ToString();//"Completo";
                                tblCellIndicadorCalificacion.ForeColor = System.Drawing.Color.Black;
                                iContadorRespuestas++;
                                iContadorRespuestasTema++;
                            }
                            else
                            {
                                tblCellIndicadorCalificacion.Text = rowResp[0]["respuesta"].ToString(); //fPorcentajeRespuestasIncisos.ToString() + "%";
                                tblCellIndicadorCalificacion.ForeColor = System.Drawing.Color.Black;
                                flgLiberaCuestionario = false;
                            }
                        }
                        else
                        {
                            tblCellIndicadorCalificacion.Text = rowResp[0]["respuesta"].ToString(); //"Completo";
                            tblCellIndicadorCalificacion.ForeColor = System.Drawing.Color.Black;
                            iContadorRespuestas++;
                            iContadorRespuestasTema++;
                        }

                    }
                    else
                    {
                        /// MRA: Checar esta condición
                        /// 13/11/2011 21:54
                        tblCellIndicadorCalificacion.Text = "Sin Respuesta";
                        tblCellIndicadorCalificacion.ForeColor = System.Drawing.Color.Red;

                        if (!Convert.ToBoolean(rowIndi["obligatorio"].ToString()))
                        {
                            ////Como no es obligatorio se lo damos por bueno
                            iContadorRespuestas++;
                            iContadorRespuestasTema++;
                        }
                        else
                        {
                            //tblCellIndicadorCalificacion.ForeColor = System.Drawing.Color.Red;
                            //tblCellIndicadorCalificacion.Text = "Sin respuesta";
                            flgLiberaCuestionario = false;
                        }
                    }
                    tblRowIndicador.Cells.Add(tblCellIndicador);
                    tblRowIndicador.Cells.Add(tblCellIndicadorCalificacion);

                    if (Request.Params["evidencias"] != null)
                    {
                        //Poner aqui las evidencias del indicador
                        foreach (DataRow drTipoEvidencia in respuestas.Tables["Tipo_Evidencia"].Rows)
                        {
                            TableCell tblCellTipoEvidencia = new TableCell();
                            //tblCellTipoEvidencia.Text = drTipoEvidencia["siglas"].ToString();
                            tblCellTipoEvidencia.BorderWidth = 1;
                            tblCellTipoEvidencia.BackColor = System.Drawing.Color.Beige;

                            bool tieneArchivo = false;

                            // Hay que buscar que en el indicador
                            foreach (DataRow drEvidencia in respuestas.Tables["Evidencia_Indicador"].Select("idTema = " + drTema["idTema"].ToString() + " and idIndicador = " + rowIndi["idIndicador"].ToString()))
                            {
                                //UpdatePanel upTipoEvidenciaIndicador = new UpdatePanel();

                                if (drTipoEvidencia["idTipoEvidencia"].ToString() == drEvidencia["idTipoEvidencia"].ToString())
                                {
                                    ImageButton cumple = new ImageButton();
                                    cumple.ImageUrl = "~/images/gridActulizar.gif";
                                    cumple.Click += new System.Web.UI.ImageClickEventHandler(this.btnCambiarTipoDeEvidenciaIndicador_Click);
                                    tblCellTipoEvidencia.HorizontalAlign = HorizontalAlign.Center;
                                    tblCellTipoEvidencia.VerticalAlign = VerticalAlign.Middle;
                                    //upTipoEvidenciaIndicador.ContentTemplateContainer.Controls.Add(cumple);
                                    tblCellTipoEvidencia.Controls.Add(cumple);
                                    tieneArchivo = true;
                                    break;
                                }
                                //tblCellTipoEvidencia.Controls.Add(upTipoEvidenciaIndicador);
                                
                            }
                            if (!tieneArchivo)
                            {
                                ImageButton noTieneArchivoIndicador = new ImageButton();
                                noTieneArchivoIndicador.ImageUrl = "~/images/blank.gif";
                                noTieneArchivoIndicador.Click += new System.Web.UI.ImageClickEventHandler(this.btnCambiarTipoDeEvidenciaIndicador_Click);
                                tblCellTipoEvidencia.HorizontalAlign = HorizontalAlign.Center;
                                tblCellTipoEvidencia.VerticalAlign = VerticalAlign.Middle;
                                tblCellTipoEvidencia.Controls.Add(noTieneArchivoIndicador);
                            }

                            //
                            //if (!tieneArchivo)
                            //{
                            //    foreach (DataRow drEvidenciaInciso in respuestas.Tables["Respuesta_Inciso"].Select("idTema = " + drTema["idTema"].ToString() + " and idIndicador = " + rowIndi["idIndicador"].ToString()))
                            //    {
                            //        if (drTipoEvidencia["idTipoEvidencia"].ToString() == drEvidenciaInciso["idTipoEvidencia"].ToString())
                            //        {
                            //            Image cumple = new Image();
                            //            cumple.ImageUrl = "~/images/gridActulizar.gif";
                            //            tblCellTipoEvidencia.HorizontalAlign = HorizontalAlign.Center;
                            //            tblCellTipoEvidencia.Controls.Add(cumple);
                            //            tieneArchivo = true;
                            //            break;
                            //        }
                            //    }
                            //}
                            tblRowIndicador.Cells.Add(tblCellTipoEvidencia);
                        }
                    }

                    if (Request.Params["evaluacion"] != null)
                    {
                        TableCell tblCellPrecalificacion = new TableCell();
                        TableCell tblCellEvaluacion = new TableCell();
                        TableCell tblCellTotal = new TableCell();

                        Button btnCalcular = new Button();
                        TextBox txtEvaluacion = new TextBox();
                        Label lblCalificacion = new Label();
                  
                        btnCalcular.Text = "Calcular";
                        btnCalcular.Click += new System.EventHandler(this.btnCalcularEvaluacion_Click);
                        txtEvaluacion.Width = 30;
                        tblCellPrecalificacion.HorizontalAlign = HorizontalAlign.Center;
                        tblCellPrecalificacion.VerticalAlign = VerticalAlign.Middle;
                        tblCellPrecalificacion.BorderWidth = 1;
                        tblCellPrecalificacion.ForeColor = System.Drawing.Color.Red;

                        tblCellEvaluacion.BorderWidth = 1;
                        tblCellPrecalificacion.BorderWidth = 1;
                        tblCellTotal.BorderWidth = 1;

                        tblCellPrecalificacion.BackColor = System.Drawing.Color.Beige;
                        tblCellEvaluacion.BackColor = System.Drawing.Color.Beige;
                        tblCellTotal.BackColor = System.Drawing.Color.Beige;

                        tblCellEvaluacion.Controls.Add(txtEvaluacion);
                        tblCellEvaluacion.Controls.Add(btnCalcular);
                        tblCellEvaluacion.Controls.Add(lblCalificacion);


                        if (rowResp.GetLength(0) > 0)
                        {
                            if (rowResp[0]["valor"] == null)
                                lblCalificacion.Text = Convert.ToSingle(rowResp[0]["valor"].ToString()).ToString();

                            tblCellPrecalificacion.Controls.Add(lblCalificacion);
                            if (rowResp[0]["calificacionRevisor"].ToString() != "")
                            {
                                txtEvaluacion.Text = Convert.ToSingle(rowResp[0]["calificacionRevisor"].ToString()).ToString();
                            }
                        }

                        tblRowIndicador.Cells.Add(tblCellPrecalificacion);
                        tblRowIndicador.Cells.Add(tblCellEvaluacion);
                        tblRowIndicador.Cells.Add(tblCellTotal);
                    }
                    //TableCell tblCellEvidencias = new TableCell();
                    //tblCellEvidencias.BorderWidth = 1;
                    //tblCellEvidencias.BackColor = System.Drawing.Color.Beige;
                    //tblCellEvidencias.HorizontalAlign = HorizontalAlign.Center;
                    //tblCellEvidencias.Text = respuestas.Tables["Evidencia_Indicador"].Select("idTema = " + drTema["idTema"].ToString() + " and idIndicador = " + rowIndi["idIndicador"].ToString()).Length.ToString();
                    //tblRowIndicador.Cells.Add(tblCellEvidencias);

                    //tblAvance.Rows.Add(tblRowIndicador);
                }
                if (iContadorIndicadores > 0)
                {
                    float fPorcentajeRespuestas = (iContadorRespuestas * 100) / iContadorIndicadores;
                    tblCellSubtemaCalificacion.Text = fPorcentajeRespuestas.ToString() + "%";
                }
                else
                {
                    tblCellSubtemaCalificacion.Text = "0%";
                }
                

                if (Request.Params["evidencias"] != null)
                {
                    foreach (DataRow drTipoEvidencia in respuestas.Tables["Tipo_Evidencia"].Rows)
                    {
                        TableCell tblCellTipoEvidencia = new TableCell();
                        tblCellTipoEvidencia.Text = drTipoEvidencia["siglas"].ToString();
                        tblCellTipoEvidencia.BorderWidth = 1;
                        tblCellTipoEvidencia.BackColor = System.Drawing.Color.SteelBlue;
                        tblCellTipoEvidencia.ForeColor = System.Drawing.Color.White;
                        tblRowSubtema.Cells.Add(tblCellTipoEvidencia);
                    }
                }

                if (Request.Params["evaluacion"] != null)
                {
                    TableCell tblCellTituloPrecalificacion = new TableCell();
                    TableCell tblCellEvaluador = new TableCell();
                    TableCell tblCellFactorAnt = new TableCell();
                    TableCell tblCellNoDeDocumentos = new TableCell();

                    tblCellTituloPrecalificacion.BorderWidth = 1;
                    tblCellEvaluador.BorderWidth = 1;
                    tblCellFactorAnt.BorderWidth = 1;
                    tblCellNoDeDocumentos.BorderWidth = 1;

                    tblCellTituloPrecalificacion.BackColor = System.Drawing.Color.SteelBlue;
                    tblCellEvaluador.BackColor = System.Drawing.Color.SteelBlue;
                    tblCellFactorAnt.BackColor = System.Drawing.Color.SteelBlue;
                    tblCellNoDeDocumentos.BackColor = System.Drawing.Color.SteelBlue;
                        
                    tblCellTituloPrecalificacion.ForeColor = System.Drawing.Color.White;
                    tblCellEvaluador.ForeColor = System.Drawing.Color.White;
                    tblCellFactorAnt.ForeColor = System.Drawing.Color.White;
                    tblCellTituloPrecalificacion.Text = "Precalificación";
                    tblCellEvaluador.Text = "Evaluación";
                    tblCellFactorAnt.Text = "Total";

                    tblRowSubtema.Cells.Add(tblCellTituloPrecalificacion);
                    tblRowSubtema.Cells.Add(tblCellEvaluador);
                    tblRowSubtema.Cells.Add(tblCellFactorAnt);
                }

            }

            if (iContadorIndicadoresTema > 0)
            {
                float fPorcentajeRespuestasTema = (iContadorRespuestasTema * 100) / iContadorIndicadoresTema;
                tblCellTemaCalificacion.Text = fPorcentajeRespuestasTema.ToString() + "%";   
                // Guardar en la base de datos el porcentaje de avance
            }
        } //Aqui

        Panel1.Controls.Add(tblAvance);
        if (flgLiberaCuestionario && iContadorIndicadores > 0)
        {
            chkConfirmaLiberacion.Text = "Estoy de acuerdo y autorizo la evaluación de todo lo expresado en este cuestionario.<br>Una vez liberado ya no tendré acceso al cuestionario.<br>";
            chkConfirmaLiberacion.Checked = false;
            chkConfirmaLiberacion.AutoPostBack = false;

            Panel1.Controls.Add(chkConfirmaLiberacion);

            Button btnLiberarCuestionario = new Button();
            btnLiberarCuestionario.Text = "Liberar cuestionario";
            btnLiberarCuestionario.Click += new System.EventHandler(this.btnLiberarCuestionario_Click);
            btnLiberarCuestionario.Attributes.Add("onclick",
           "javascript:alert('¿Está seguro de querer liberar el cuestionario para evaluación?')");
//            var answer = confirm ("Are you having fun?")
//if (answer)
//alert ("Woo Hoo! So am I.")
//else
//alert ("Darn. Well, keep trying then.")
//            var answer = confirm ("Please click on OK to continue loading my page, or CANCEL to be directed to the Yahoo site.")
//if (!answer)
//window.location="http://www.yahoo.com/"
            Panel1.Controls.Add(btnLiberarCuestionario);
        }
    }

    /// <summary>
    /// Método para liberar el cuestionario
    /// Tiene que validar que esté completo al 100%
    /// Que estén seleccionadas todas las evidencias electrónicas.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLiberarCuestionario_Click(object sender, EventArgs e)
    {

        if (chkConfirmaLiberacion.Checked)
        {
            Cuestionario cuestionario = new Cuestionario();
            cuestionario.idCuestionario = Convert.ToInt32(lblIdCuestionario.Text);
            cuestionario.idEmpresa = this.GetIdEmpresa();//Convert.ToInt32(Session["idEmpresa"].ToString());
            cuestionario.idUsuario = Session["idUsuario"].ToString();
            cuestionario.idPais = Convert.ToInt32(Session["idPais"].ToString());

            //Falta mandar mensaje de confirmación
            if (cuestionario.LiberaCuestionario())
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Liberación de cuestionario", "alert('Su cuestionario fue liberado con éxito. ¡Felicidades!');", true);
                Response.Redirect("main.aspx", false);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Liberación de cuestionario", "alert('Hubo un error al liberar su cuestionario, intente salir, entrar a la aplicación y liberar cuestionario nuevamente.');", true);
                
            }
           
        }
        else
        {
            
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCambiarTipoDeEvidenciaIndicador_Click(object sender, EventArgs e)
    {
        ImageButton imgCumple = (ImageButton)sender;
        if (imgCumple.ImageUrl == "~/images/gridActulizar.gif")
        {
            imgCumple.ImageUrl = "~/images/failed.gif";
        }
        else if (imgCumple.ImageUrl == "~/images/failed.gif")
        {
            imgCumple.ImageUrl = "~/images/blank.gif";
        }
        else
        {
            imgCumple.ImageUrl = "~/images/gridActulizar.gif";
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCambiarTipoDeEvidenciaInciso_Click(object sender, EventArgs e)
    {
        ImageButton imgCumple = (ImageButton)sender;
        if (imgCumple.ImageUrl == "~/images/gridActulizar.gif")
        {
            imgCumple.ImageUrl = "~/images/failed.gif";
        }
        else if (imgCumple.ImageUrl == "~/images/failed.gif")
        {
            imgCumple.ImageUrl = "~/images/blank.gif";
        }
        else
        {
            imgCumple.ImageUrl = "~/images/gridActulizar.gif";
        }
    }

    protected void btnCalcularEvaluacion_Click(object sender, EventArgs e)
    {

        // Cambiar el tipo de Evidencia
        //float fResultado = 0;

        //if (lblCalificacion.Text.Trim() != "")
        //{
        //    fResultado = Convert.ToSingle(lblCalificacion.Text);
        //}

        //if (txtEvaluacion.Text.Trim() != "")
        //{
        //    fResultado += Convert.ToSingle(txtEvaluacion.Text);
        //}

        //if (fResultado <= 3)
        //{
        //    // Aqui guardar el valor de la precalificacion y la evaluación del revisor
        //    //Indicador indi = new Indicador();
        //    //indi.idIndicador = Convert.ToInt32(lblidIndicador.Text);
        //    //indi.idTema = Convert.ToInt32(lblidTema.Text); ;
        //    //indi.idEmpresa = this.GetIdEmpresa(); // Convert.ToInt32(Session["idEmpresa"].ToString());
        //    //indi.idCuestionario = Convert.ToInt32(lblIdCuestionario.Text);
        //    //indi.idUsuario = Session["idUsuario"].ToString();

        //    //indi.valor = Convert.ToSingle(lblCalificacion.Text);

        //    //if (txtEvaluacion.Text != "")
        //    //    indi.calificacionRevisor = Convert.ToSingle(txtEvaluacion.Text);
        //    //else
        //    //    indi.calificacionRevisor = 0;

        //    //indi.GuardarEvaluacion();

        //    //if (Request.Params["idEmpresa"] == null)
        //    //{
        //    //    Response.Redirect("diagnostico.aspx?Content=visorDelCuestionario&idTema=" + lblidTema.Text + "&idSubtema=" + lblidSubtema.Text + "&idIndicador=" + lblidIndicador.Text + "&idCuestionario=" + lblIdCuestionario.Text);
        //    //}
        //    //else
        //    //{
        //    //    Response.Redirect("diagnostico.aspx?Content=visorDelCuestionario&idTema=" + lblidTema.Text + "&idSubtema=" + lblidSubtema.Text + "&idIndicador=" + lblidIndicador.Text + "&idCuestionario=" + lblIdCuestionario.Text + "&idEmpresa=" + Request.Params["idEmpresa"].ToString());
        //    //}
        //}
        //else
        //{
        //    lblError.Text = "El valor ingresado en la evaluación supera los 3 puntos destinados para este indicador, por favor ingrese otro valor.";
        //}

    }



}   
