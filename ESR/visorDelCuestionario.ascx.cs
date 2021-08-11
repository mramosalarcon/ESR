using System;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESR.Business;
using System.IO;

public partial class visorDelCuestionario : System.Web.UI.UserControl
{
    private Label lblError = new Label();
    private Label lblCalificacion = new Label();
    private TextBox txtCalificacionRevisor = new TextBox();
    private int id = 0;
    /// <summary>
    /// Si viene como par�metro el idEmpresa se muestra la informaci�n de la empresa
    /// si no, solo se muestra la informaci�n de la empresa que esta logeada.
    /// </summary>
    /// <returns></returns>
    protected int GetIdEmpresa()
    {
        try
        {
            if (Request.Params["idEmpresa"] == null)
                return Convert.ToInt32(Session["idEmpresa"].ToString());
            else
                return Convert.ToInt32(Request.Params["idEmpresa"].ToString());
        }
        catch (Exception ex)
        {
            using (StreamWriter sw = File.AppendText(ConfigurationManager.AppSettings["logFile"].ToString()))
            {
                sw.AutoFlush = true;
                sw.WriteLine("Fecha: " + DateTime.Now.ToString());
                sw.WriteLine("Empresa: " + Session["idEmpresa"].ToString());
                sw.WriteLine("idIndicador: " + lblidIndicador.Text);
                sw.WriteLine("idTema: " + lblidTema.Text);
                sw.WriteLine("idCuestionario: " + lblIdCuestionario.Text);
                sw.WriteLine("Error en visorDelCuestionario.GetIdEmpresa(): " + ex.Message);
                sw.WriteLine("InnerException: " + ex.InnerException);
                sw.Close();
                return -1;
            }
        }
    }

    protected string GetIdUsuario()
    {
        if (Request.Params["idUsuario"] == null)
            return Session["idUsuario"].ToString();
        else
            return Request.Params["idUsuario"].ToString();
    }

    /// <summary>
    /// P�gina que muestra el indicador seleccionado del menu. 
    /// Es el m�todo mas importante de la aplicaci�n
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Indicador indicadores = new Indicador();
            Respuesta respuestasEmpresa = new Respuesta();
            DataSet dsRespuestas;
            DataSet dsIndicadores;

            respuestasEmpresa.idIndicador = Convert.ToInt32(lblidIndicador.Text);
            respuestasEmpresa.idTema = Convert.ToInt32(lblidTema.Text);
            respuestasEmpresa.idEmpresa = this.GetIdEmpresa();
            respuestasEmpresa.idCuestionario = Convert.ToInt32(lblIdCuestionario.Text);

            // Verifica que el indicador de legalidad se haya contestado.
            if (!respuestasEmpresa.Legalidad())
            {
                // Si no cumple con el indicador de legalidad lo manda a ese indicador, no importa lo que haya seleccionado.
                lblidIndicador.Text = respuestasEmpresa.idIndicador.ToString();
            }

            dsRespuestas = respuestasEmpresa.Carga();
            respuestasEmpresa.reelevancia = true;
            if (!respuestasEmpresa.reelevancia)
            {
                Response.Redirect("calificacionTema.aspx");
            }
            else
            {

                if (lblidIndicador.Text != "")
                {
                    if (Request.Params["bloqueado"] == null)
                    {
                        dsIndicadores = indicadores.Carga(Convert.ToInt32(lblidTema.Text), Convert.ToInt32(lblidIndicador.Text), Convert.ToInt32(lblIdCuestionario.Text), false);
                    }
                    else
                    {
                        dsIndicadores = indicadores.Carga(Convert.ToInt32(lblidTema.Text), Convert.ToInt32(lblidIndicador.Text), Convert.ToInt32(lblIdCuestionario.Text), true);
                    }
                }
                else
                {
                    dsIndicadores = indicadores.CargaXSubtema(Convert.ToInt32(lblidTema.Text), Convert.ToInt32(lblidSubtema.Text));
                }

                // Con los indicadores llenar el menu
                // Para despues desplegar las respuestas

                // Inserta Indicadores
            
                foreach (DataRow drIndicador in dsIndicadores.Tables["Indicador"].Rows)
                {
                    //Muestra el tema -> subtema
                    Label lblUbicacion = new Label();
                    lblUbicacion.Text = "<p>Tema: <b>" + drIndicador["Tema"].ToString() + "</b>, Subtema: <b>" + drIndicador["Subtema"].ToString() + "</b>, Fase de Implementación: <b>" + drIndicador["faseImplementacion"].ToString() + "</b>.</p>";
                    if (respuestasEmpresa.idCuestionario == 10 && respuestasEmpresa.idTema == 3)
                    {
                        lblUbicacion.Text += "<p class=\"style1\">&nbsp;&nbsp;<b>Los indicadores del tema de Consumo Responsable son opcionales para este cuestionario.</b></p>";
                    }
                    Panel1.Controls.Add(lblUbicacion);

                    // Inserta el texto del indicador o la pregunta
                    Label lblPregunta = new Label();
                    lblPregunta.Text += "<p>" + drIndicador["ordinal"].ToString() + ". " + drIndicador["Indicador"].ToString();
                    if (drIndicador["nota"].ToString() != "")
                    {
                        //lblPregunta.Text += "&nbsp;&nbsp;<img onclick=\"openPopup(" + drIndicador["corto"].ToString().Replace(" ", "") + ")\"  src=\"images/help.gif\" />";
                        lblPregunta.Text += "&nbsp;&nbsp;<img id=\"IcnInfo\" src=\"images/help.gif\" />";

                        //Aqui insertamos el DIV para desplegar la nota del indicador. Un DIV por indicador.
                        Label lblNotaInformativa = new Label();
                        //lblNotaInformativa.Text = "<div id=\"" + drIndicador["corto"].ToString().Replace(" ", "") + "\" class=\"popupW\">" +
                        //        "<table border=\"0\" width=\"100%\">" +
                        //                        "<tr><td align=\"center\">Nota del indicador</td></tr><tr><td align=\"left\">" + drIndicador["nota"].ToString() + "</td></tr></table></div>";
                        if (Session["idPais"].ToString() == "1")
                        {
                            lblNotaInformativa.Text = drIndicador["nota"].ToString();
                        }
                        else if (Session["idPais"].ToString() == "168")
                        {
                            lblNotaInformativa.Text = drIndicador["descripcionAlterna"].ToString();
                        }
                        Notas.Controls.Add(lblNotaInformativa);

                    }
                    lblPregunta.Text += "</p>";
                    //lblNota.Text = "Comentario de prueba para validar funcionamiento (CESR)";

                    //Aqui meter la etiqueta y el texto para el revisor
                    Label lblMensajeCalificacion = new Label();
                    lblMensajeCalificacion.ForeColor = System.Drawing.Color.Red;
                    lblMensajeCalificacion.Text = "Precalificaci�n: ";
                    lblMensajeCalificacion.Visible = false;

                    lblCalificacion.ForeColor = System.Drawing.Color.Red;
                    lblError.ForeColor = System.Drawing.Color.Red;

                    Table tblCalificacion = new Table();

                    //tblCalificacion.BorderWidth = 1;

                    TableRow tblRowCal1 = new TableRow();
                    TableCell tblCell1 = new TableCell();

                    tblCell1.Controls.Add(lblMensajeCalificacion);
                    tblCell1.Controls.Add(lblCalificacion);
                    tblRowCal1.Cells.Add(tblCell1);

                    tblCalificacion.Rows.Add(tblRowCal1);

                    lblMensajeCalificacion.Visible = true;
                    Label lblCalifRevisor = new Label();
                    lblCalifRevisor.ForeColor = System.Drawing.Color.Red;
                    lblCalifRevisor.Text = "Calificación del evaluador: ";

                    CheckBox chkRevisado = new CheckBox();
                    chkRevisado.Text = "Indicador revisado";

                    if (dsRespuestas.Tables["Respuesta_Indicador"].Select("idIndicador = " + drIndicador["idIndicador"].ToString()).GetLength(0) > 0)
                    {
                        DataRow rowRI = dsRespuestas.Tables["Respuesta_Indicador"].Select("idIndicador = " + drIndicador["idIndicador"].ToString())[0];
                        txtCalificacionRevisor.Text = rowRI["calificacionRevisor"].ToString();
                        //chkRevisado.Checked = Convert.ToBoolean(rowRI["revisado"]);
                    }

                    txtCalificacionRevisor.Width = 30;

                    Button btnActualizarEvaluacion = new Button();
                    btnActualizarEvaluacion.Text = "Guardar Evaluaci�n";
                    btnActualizarEvaluacion.Click += new System.EventHandler(this.btnActualizarEvaluacion_Click);

                    TableRow tblRowCal2 = new TableRow();
                    TableCell tblCell2 = new TableCell();
                    tblCell2.Controls.Add(lblCalifRevisor);
                    tblCell2.Controls.Add(txtCalificacionRevisor);
                    tblCell2.Controls.Add(btnActualizarEvaluacion);
                    tblCell2.Controls.Add(chkRevisado);
                    tblCell2.Controls.Add(lblError);


                    tblRowCal2.Cells.Add(tblCell2);
                    tblCalificacion.Rows.Add(tblRowCal2);

                    string strPerfiles = Session["perfil"].ToString();
                    if (strPerfiles.IndexOf("0") > -1 || strPerfiles.IndexOf("1") > -1 ||
                    strPerfiles.IndexOf("2") > -1)
                    {
                        Panel1.Controls.Add(tblCalificacion);
                    }
                    Panel1.Controls.Add(lblPregunta);

                    #region Manejo de Incisos
                    //if (drIndicador["nota"].ToString() != "")
                    //{
                    //    Image imgIconInfo = new Image();
                    //    imgIconInfo.ID = "icnInfo";
                    //    imgIconInfo.ImageUrl = "images/help.gif";
                    //    Panel1.Controls.Add(imgIconInfo);
                    //}

                    // Hay que evaluar si el indicador cuenta con Incisos, si es asi
                    // Entonces insertamos las respuestas a los incisos y sus tipos de respuesta, si no
                    // insertamos los tipos de evidencia.

                    // Si tiene incisos
                    //if (dsIndicadores.Tables["Inciso"].Select("idIndicador = " + drIndicador["idIndicador"].ToString() +
                    //        " And idTema = " + drIndicador["idTema"].ToString()).Length > 0)
                    //{
                    //    UpdatePanel panTiposDeRespuestaInciso = new UpdatePanel();
                    //    panTiposDeRespuestaInciso.ID = "panTiposdeRespuestaInciso" + "_" + drIndicador["idIndicador"].ToString();
                    //    bool rbFlag = false;
                    //    if (lblReadOnly.Text == "") // �Para que sirve esta condici�n? MRA: 09/11/09 
                    //        //R. Para no mostrar los indicadores en modo de solo lectura. MRA: 16/06/2010
                    //    {
                    //        // Inserta el cat�logo del tipo de respuesta
                    //        foreach (DataRow drTipoRespuestaInciso in dsIndicadores.Tables["Tipo_Respuesta"].Rows)
                    //        {
                    //            RadioButton rb = new RadioButton();
                    //            // 1_No, 1_No aplica, 1_Compromiso P�blico, 1_Si, 1_Mejor pr�ctica
                    //            rb.ID = drIndicador["idIndicador"].ToString() + "_" + drTipoRespuestaInciso["idTipoRespuesta"].ToString();
                    //            rb.Text = drTipoRespuestaInciso["descripcion"].ToString() + "<br />";
                    //            rb.GroupName = "tipoRespuestaInciso" + drIndicador["idIndicador"].ToString();
                    //            rb.AutoPostBack = true;
                    //            if (dsRespuestas.Tables["Respuesta_Indicador"].Rows.Count > 0)
                    //            {
                    //                DataRow rowRespuestaIndicador = dsRespuestas.Tables["Respuesta_Indicador"].Select("idIndicador = " + drIndicador["idIndicador"].ToString())[0];

                    //                if (drTipoRespuestaInciso["idTipoRespuesta"].ToString() == rowRespuestaIndicador["idTipoRespuesta"].ToString())
                    //                {
                    //                    if (Convert.ToInt32(drTipoRespuestaInciso["idTipoRespuesta"].ToString()) > 2)
                    //                    {
                    //                        rbFlag = true;
                    //                        lblCalificacion.Text = "3";
                    //                    }
                    //                    else
                    //                        lblCalificacion.Text = drTipoRespuestaInciso["valor"].ToString();

                    //                    rb.Checked = true;
                    //                }
                    //            }
                    //            rb.CheckedChanged += new System.EventHandler(this.rbTiposDeRespuestaInciso_CheckedChanged);
                    //            panTiposDeRespuestaInciso.ContentTemplateContainer.Controls.Add(rb);
                    //            // Checa que tipo de respuesta es para meter la captura de evidencias
                    //            if (drTipoRespuestaInciso["descripcion"].ToString().ToUpper() != "NO" && drTipoRespuestaInciso["descripcion"].ToString().ToUpper() != "NO APLICA")
                    //            {
                    //                // Inserta Tipos de Evidencia
                    //                UpdatePanel panTiposDeEvidencia = new UpdatePanel();

                    //                // Asigna el id del updatepanel, ejemplo: panTiposDeEvidencia_1_Si
                    //                panTiposDeEvidencia.ID = "panTiposDeEvidencia" + "_" + drIndicador["idIndicador"].ToString() + "_" + drTipoRespuestaInciso["descripcion"].ToString();

                    //                // Para cada registro en la tabla Tipos_Evidencia
                    //                // crea un check box y a�ade los controles para subir las evidencias.
                    //                Table tblArchivos = new Table();
                    //                tblArchivos.BorderWidth = 0;

                    //                float promEvidenciaIndicador = 0;
                    //                bool cuentaEvidencia = false;

                    //                // Para cada tipo de evidencia
                    //                foreach (DataRow evi in dsIndicadores.Tables["Tipo_Evidencia"].Rows)
                    //                {
                    //                    TableRow tblRow = new TableRow();
                    //                    TableCell tblCellTab = new TableCell();
                    //                    tblCellTab.Text = "&nbsp; &nbsp; &nbsp;";
                    //                    tblRow.Cells.Add(tblCellTab);
                    //                    TableCell tblCellchk = new TableCell();
                    //                    TableCell tblCellfup = new TableCell();
                    //                    TableCell tblCellbtn = new TableCell();

                    //                    CheckBox chkB = new CheckBox();
                    //                    // Asigna el ID al checkbox, ejemplo: tipoEvidencia_1_1, tipoEvidencia_1_2
                    //                    chkB.ID = "tipoEvidencia_" + drIndicador["idIndicador"].ToString() + "_" + drTipoRespuestaInciso["idTipoRespuesta"].ToString() + "_" + evi["idTipoEvidencia"].ToString();
                    //                    // A�ade la descripci�n de la evidencia y agrega el control al UpdatePanel
                    //                    chkB.Text = evi["descripcion"].ToString();
                    //                    chkB.AutoPostBack = true;
                    //                    chkB.CheckedChanged += new System.EventHandler(this.chkB_CheckedChange);
                    //                    tblCellchk.Controls.Add(chkB);
                    //                    tblRow.Cells.Add(tblCellchk);
                    //                    tblArchivos.Rows.Add(tblRow);

                    //                    DataRow[] rowEvidenciaIndicador = dsRespuestas.Tables["Evidencia_Indicador"].Select("idTipoRespuesta = " + drTipoRespuestaInciso["idTipoRespuesta"].ToString() + " And idTipoEvidencia = " + evi["idTipoEvidencia"].ToString());
                    //                    if (rowEvidenciaIndicador.GetLength(0) > 0)
                    //                    {
                    //                        chkB.Checked = true;
                    //                        // Hay que poner las evidencias
                    //                        if (!cuentaEvidencia)
                    //                        {
                    //                            //promEvidenciaIndicador += 1;
                    //                            cuentaEvidencia = true;
                    //                        }

                    //                        float valor = Convert.ToSingle(evi["valor"].ToString());
                    //                        promEvidenciaIndicador = promEvidenciaIndicador + valor;

                    //                        foreach (DataRow drEvidencia in rowEvidenciaIndicador)
                    //                        {
                    //                            if (drEvidencia["descripcion"].ToString().ToUpper() != evi["descripcion"].ToString().ToUpper())
                    //                            {
                    //                                // Hay evidencia
                    //                                TableRow tblRowEvi = new TableRow();

                    //                                TableCell tblCellTabEvi = new TableCell();
                    //                                tblCellTab.Text = "&nbsp; &nbsp; &nbsp;";
                    //                                tblRowEvi.Cells.Add(tblCellTabEvi);

                    //                                TableCell tblCellchkEvi = new TableCell();
                    //                                TableCell tblCellfupEvi = new TableCell();
                    //                                TableCell tblCellbtnEvi = new TableCell();

                    //                                HyperLink newFile = new HyperLink();
                    //                                newFile.Text = drEvidencia["descripcion"].ToString();
                    //                                //newFile.NavigateUrl = "Download.aspx?archivo=" + newFile.Text; //Esta es la liga a desplegar
                    //                                newFile.NavigateUrl = "Download.aspx?idIndicador=" + drEvidencia["idIndicador"].ToString() + "&idTema=" + drEvidencia["idTema"].ToString() + "&idTipoEvidencia=" + drEvidencia["idTipoEvidencia"].ToString() +
                    //                                                            "&idEmpresa=" + drEvidencia["idEmpresa"].ToString() + "&idCuestionario=" + lblIdCuestionario.Text + "&descripcion=" + drEvidencia["descripcion"].ToString().Replace(" ", "%20");
                    //                                tblCellchkEvi.Controls.Add(newFile);

                    //                                ImageButton imgEliminar = new ImageButton();
                    //                                imgEliminar.ID = "imgEliminar_" + drEvidencia["idIndicador"].ToString() + "_" + drEvidencia["idTema"].ToString() + "_"
                    //                                    + drEvidencia["idTipoRespuesta"].ToString() + "_" + drEvidencia["idTipoEvidencia"].ToString() + "_" + drEvidencia["idEmpresa"].ToString() + "_"
                    //                                    + drEvidencia["descripcion"].ToString();
                    //                                imgEliminar.ImageUrl = "~/images/close.gif";
                    //                                imgEliminar.Click += new ImageClickEventHandler(this.imgEliminar_Click);
                    //                                //imgEliminar.PostBackUrl = "diagnostico.aspx?Content=visorDelCuestionario&idTema=" + lblidTema.Text + "&idSubtema=" + lblidSubtema.Text + "&idIndicador=" + lblidIndicador.Text + "&idCuestionario=" + lblIdCuestionario.Text;
                    //                                tblCellfupEvi.Controls.Add(imgEliminar);

                    //                                tblRowEvi.Cells.Add(tblCellchkEvi);
                    //                                tblRowEvi.Cells.Add(tblCellfupEvi);
                    //                                tblRowEvi.Cells.Add(tblCellbtnEvi);

                    //                                tblArchivos.Rows.Add(tblRowEvi);
                    //                            }
                    //                        }
                    //                    }
                    //                    //Bot�n "Adjuntar archivo"
                    //                    Button btnOtro = new Button();
                    //                    btnOtro.Enabled = chkB.Checked;
                    //                    btnOtro.ID = "btnOtro" + "_" + drIndicador["idIndicador"].ToString() + "_" + drTipoRespuestaInciso["idTipoRespuesta"].ToString() + "_" + evi["idTipoEvidencia"].ToString();
                    //                    btnOtro.Text = "Adjuntar archivo";
                    //                    btnOtro.OnClientClick = "window.open(\"fileUpload.aspx?idIndicador=" + drIndicador["idIndicador"].ToString() +
                    //                        "&idTema=" + drIndicador["idTema"].ToString() + "&idTipoEvidencia=" + evi["idTipoEvidencia"].ToString() +
                    //                        "&idCuestionario=" + lblIdCuestionario.Text + "&idTipoRespuesta=" + drTipoRespuestaInciso["idTipoRespuesta"].ToString() +
                    //                        "\",null,\"top=250, left=250, dependent=yes, height=400, width=600, menubar=no, location=no, status=no, toolbar=no, titlebar=no, scrollbar=no, resizable=no\");";
                    //                    //btnOtro.OnClientClick = "showWindow(\"adjuntar\");";
                    //                    //btnOtro.Click += new System.EventHandler(this.btnOtroIndicador_Click);
                    //                    tblCellbtn.Controls.Add(btnOtro);

                    //                    Button btnSharepoint = new Button();
                    //                    btnSharepoint.Enabled = chkB.Checked;
                    //                    btnSharepoint.ID = "btnSharepoint" + "_" + drIndicador["idIndicador"].ToString() + "_" + drTipoRespuestaInciso["idTipoRespuesta"].ToString() + "_" + evi["idTipoEvidencia"].ToString();
                    //                    btnSharepoint.Text = "Lista de documentos";
                    //                    btnSharepoint.OnClientClick = "window.open(\"fileAttach.aspx?idIndicador=" + drIndicador["idIndicador"].ToString() +
                    //                        "&idTema=" + drIndicador["idTema"].ToString() + "&idTipoEvidencia=" + evi["idTipoEvidencia"].ToString() +
                    //                        "&idCuestionario=" + lblIdCuestionario.Text + "&idTipoRespuesta=" + drTipoRespuestaInciso["idTipoRespuesta"].ToString() +
                    //                        "\",null,\"top=50, left=50, dependent=yes, height=600, width=1200, menubar=no, location=no, status=no, toolbar=no, titlebar=no, scrollbar=no, resizable=no\");";
                    //                    //btnSharepoint.OnClientClick = "window.open(\"http://esr.cemefi.org/" + Session["idEmpresa"].ToString() + "/Documentos%20compartidos/Forms/AllItems.aspx\",null,\"top=50, left=50, dependent=yes, height=600, width=1200, menubar=no, location=no, status=no, toolbar=no, titlebar=no, scrollbar=no, resizable=no\");";
                    //                    if (strPerfiles.IndexOf("0") > -1 || strPerfiles.IndexOf("1") > -1 ||
                    //                    strPerfiles.IndexOf("2") > -1 || strPerfiles.IndexOf("3") > -1)
                    //                    {
                    //                        tblCellbtn.Controls.Add(btnSharepoint);
                    //                    }
                    //                    tblRow.Cells.Add(tblCellbtn);
                    //                }

                    //                if (lblCalificacion.Text != "")
                    //                {
                    //                    promEvidenciaIndicador = Convert.ToSingle(lblCalificacion.Text) + promEvidenciaIndicador;
                    //                    lblCalificacion.Text = promEvidenciaIndicador.ToString();
                    //                }
                    //                panTiposDeEvidencia.ContentTemplateContainer.Controls.Add(tblArchivos);

                    //                // Oculta el UpdatePanel de tipos de evidencia y lo agrega al UpdatePanel de
                    //                // Tipos de respuesta.
                    //                if (rb.Checked)
                    //                    panTiposDeEvidencia.Visible = true;
                    //                else
                    //                    panTiposDeEvidencia.Visible = false;


                    //                panTiposDeRespuestaInciso.ContentTemplateContainer.Controls.Add(panTiposDeEvidencia);
                    //            }
                    //        }
                    //    }

                    //    //Inserta Incisos
                    //    UpdatePanel panIncisos = new UpdatePanel();
                    //    panIncisos.ID = drIndicador["idIndicador"] + "_" + "incisos";

                    //    Label lblEntonces = new Label();
                    //    lblEntonces.Text = "<p>&nbsp;&nbsp;&nbsp;<b>Si este fuera el caso:</b>";
                    //    panIncisos.ContentTemplateContainer.Controls.Add(lblEntonces);

                    //    DataTable tblIncisos = dsIndicadores.Tables["Inciso"];

                    //    float promIncisos = 0;
                    //    int noIncisos = tblIncisos.Select("idIndicador = " + drIndicador["idIndicador"].ToString(), "idInciso").Length + 1;

                    //    foreach (DataRow drInciso in tblIncisos.Select("idIndicador = " + drIndicador["idIndicador"].ToString(), "idInciso"))
                    //    {
                    //        Label lblInciso = new Label();
                    //        lblInciso.Text = "<p>&nbsp;&nbsp;&nbsp;" + drInciso["idInciso"] + ". " + drInciso["descripcion"].ToString() + "</p>";

                    //        panIncisos.ContentTemplateContainer.Controls.Add(lblInciso);

                    //        if (lblReadOnly.Text == "")
                    //        {
                    //            // Inserta Tipos de Respuesta de los incisos
                    //            UpdatePanel panRespuestasInciso = new UpdatePanel();
                    //            panRespuestasInciso.ID = drIndicador["idIndicador"] + "_" + drInciso["idInciso"];
                    //            foreach (DataRow drTiposDeRepuestaInciso in dsIndicadores.Tables["Tipo_Respuesta"].Rows)
                    //            {
                    //                RadioButton rbTiposDeRepuestaInciso = new RadioButton();
                    //                // 1_a_No, 1_a_No aplica, 1_a_Compromiso P�blico, 1_a_Si, 1_a_Mejor pr�ctica
                    //                rbTiposDeRepuestaInciso.ID = drIndicador["idIndicador"].ToString() + "_" + drInciso["idInciso"] + "_" + drTiposDeRepuestaInciso["idTipoRespuesta"].ToString();
                    //                rbTiposDeRepuestaInciso.Text = drTiposDeRepuestaInciso["descripcion"].ToString() + "<br>";
                    //                rbTiposDeRepuestaInciso.GroupName = "tipoRespuestaInciso" + "_" + drIndicador["idIndicador"].ToString() + "_" + drInciso["idInciso"].ToString();
                    //                rbTiposDeRepuestaInciso.AutoPostBack = true;
                    //                if (dsRespuestas.Tables["Respuesta_Inciso"].Rows.Count > 0)
                    //                {
                    //                    DataRow[] rowRespuestaInciso = dsRespuestas.Tables["Respuesta_Inciso"].Select("idInciso = '" + drInciso["idInciso"].ToString() + "'");

                    //                    if (rowRespuestaInciso.GetLength(0) > 0)
                    //                    {
                    //                        if (drTiposDeRepuestaInciso["idTipoRespuesta"].ToString() == rowRespuestaInciso[0]["idTipoRespuesta"].ToString())
                    //                        {
                    //                            //float valor = Convert.ToSingle(drTiposDeRepuestaInciso["valor"].ToString());
                    //                            //promIncisos = promIncisos + valor;
                    //                            rbTiposDeRepuestaInciso.Checked = true;
                    //                        }
                    //                    }
                    //                }

                    //                rbTiposDeRepuestaInciso.CheckedChanged += new System.EventHandler(this.rbInciso_CheckedChanged);

                    //                panRespuestasInciso.ContentTemplateContainer.Controls.Add(rbTiposDeRepuestaInciso);

                    //                if (drTiposDeRepuestaInciso["descripcion"].ToString().ToUpper() != "NO" && drTiposDeRepuestaInciso["descripcion"].ToString().ToUpper() != "NO APLICA")
                    //                {
                    //                    // Inserta Tipos de Evidencia
                    //                    UpdatePanel panTiposDeEvidenciaRA = new UpdatePanel();
                    //                    Table tblArchivos = new Table();
                    //                    tblArchivos.BorderWidth = 0;
                    //                    float promEvidenciaInciso = 0;
                    //                    bool cuentaEvidencia = false;
                    //                    foreach (DataRow evi in dsIndicadores.Tables["Tipo_Evidencia"].Rows)
                    //                    {
                    //                        TableRow tblRow = new TableRow();
                    //                        TableCell tblCellTab = new TableCell();
                    //                        tblCellTab.Text = "&nbsp; &nbsp; &nbsp;";
                    //                        tblRow.Cells.Add(tblCellTab);

                    //                        TableCell tblCellchk = new TableCell();
                    //                        TableCell tblCellfup = new TableCell();
                    //                        TableCell tblCellbtn = new TableCell();

                    //                        CheckBox chkB = new CheckBox();
                    //                        chkB.ID = "tipoEvidencia" + "_" + drIndicador["idIndicador"].ToString() + "_" + drInciso["idInciso"].ToString() + "_" + drTiposDeRepuestaInciso["idTipoRespuesta"].ToString() + "_" + evi["idTipoEvidencia"].ToString();
                    //                        chkB.Text = evi["descripcion"].ToString();
                    //                        chkB.AutoPostBack = true;
                    //                        chkB.CheckedChanged += new System.EventHandler(this.chkB_CheckedChangeInc);
                    //                        tblCellchk.Controls.Add(chkB);
                    //                        tblRow.Cells.Add(tblCellchk);
                    //                        tblArchivos.Rows.Add(tblRow);

                    //                        DataRow[] rowEvidenciaInciso = dsRespuestas.Tables["Evidencia_Inciso"].Select("idInciso = '" + drInciso["idInciso"].ToString() + "' And idTipoRespuesta = " + drTiposDeRepuestaInciso["idTipoRespuesta"].ToString() + " And idTipoEvidencia = " + evi["idTipoEvidencia"].ToString());
                    //                        if (rowEvidenciaInciso.GetLength(0) > 0)
                    //                        {
                    //                            if (evi["idTipoEvidencia"].ToString() == rowEvidenciaInciso[0]["idTipoEvidencia"].ToString())
                    //                            {
                    //                                chkB.Checked = true;
                    //                                // Hay que poner las evidencias
                    //                                if (!cuentaEvidencia)
                    //                                {
                    //                                    promEvidenciaInciso += Convert.ToSingle(drTiposDeRepuestaInciso["valor"].ToString());
                    //                                    //promEvidenciaInciso += 1;
                    //                                    cuentaEvidencia = true;
                    //                                }
                    //                                promEvidenciaInciso += Convert.ToSingle(evi["Valor"].ToString());

                    //                                foreach (DataRow drEvidencia in rowEvidenciaInciso)
                    //                                {
                    //                                    // En este if se realiza el c�lculo del valor de las evidencias
                    //                                    if (drEvidencia["descripcion"].ToString().ToUpper() != evi["descripcion"].ToString().ToUpper())
                    //                                    {
                    //                                        TableRow tblRowEvi = new TableRow();

                    //                                        TableCell tblCellTabEvi = new TableCell();
                    //                                        tblCellTab.Text = "&nbsp; &nbsp; &nbsp;";
                    //                                        tblRowEvi.Cells.Add(tblCellTabEvi);

                    //                                        TableCell tblCellchkEvi = new TableCell();
                    //                                        TableCell tblCellfupEvi = new TableCell();
                    //                                        TableCell tblCellbtnEvi = new TableCell();

                    //                                        HyperLink newFile = new HyperLink();
                    //                                        newFile.Text = drEvidencia["descripcion"].ToString();
                    //                                        //newFile.NavigateUrl = "Download.aspx?archivo=" + newFile.Text; //Esta es la liga a desplegar
                    //                                        newFile.NavigateUrl = "Download.aspx?idIndicador=" + drEvidencia["idIndicador"].ToString() + "&idTema=" + drEvidencia["idTema"].ToString() + "&idInciso=" + drEvidencia["idInciso"].ToString() + "&idTipoEvidencia=" + drEvidencia["idTipoEvidencia"].ToString() +
                    //                                                            "&idEmpresa=" + drEvidencia["idEmpresa"].ToString() + "&idCuestionario=" + lblIdCuestionario.Text + "&descripcion=" + drEvidencia["descripcion"].ToString().Replace(" ", "%20");
                    //                                        tblCellchkEvi.Controls.Add(newFile);

                    //                                        ImageButton imgEliminar = new ImageButton();
                    //                                        imgEliminar.ID = "imgEliminar_" + drEvidencia["idIndicador"].ToString() + "_" + drEvidencia["idTema"].ToString() + "_"
                    //                                            + drEvidencia["idInciso"].ToString() + "_" + drEvidencia["idTipoRespuesta"].ToString() + "_"
                    //                                            + drEvidencia["idTipoEvidencia"].ToString() + "_" + drEvidencia["idEmpresa"].ToString() + "_"
                    //                                            + drEvidencia["descripcion"].ToString();
                    //                                        imgEliminar.ImageUrl = "~/images/close.gif";
                    //                                        imgEliminar.Click += new ImageClickEventHandler(this.imgEliminarEviInciso_Click);
                    //                                        //imgEliminar.PostBackUrl = "diagnostico.aspx?Content=visorDelCuestionario&idTema=" + lblidTema.Text + "&idSubtema=" + lblidSubtema.Text + "&idIndicador=" + lblidIndicador.Text + "&idCuestionario=" + lblIdCuestionario.Text;
                    //                                        tblCellfupEvi.Controls.Add(imgEliminar);

                    //                                        tblRowEvi.Cells.Add(tblCellchkEvi);
                    //                                        tblRowEvi.Cells.Add(tblCellfupEvi);
                    //                                        tblRowEvi.Cells.Add(tblCellbtnEvi);

                    //                                        tblArchivos.Rows.Add(tblRowEvi);
                    //                                    }
                    //                                }
                    //                            }
                    //                        }
                    //                        Button btnOtro = new Button();
                    //                        btnOtro.Enabled = chkB.Checked;
                    //                        btnOtro.ID = "btnOtro" + "_" + drIndicador["idIndicador"].ToString() + "_" + drInciso["idInciso"].ToString() + "_" + drTiposDeRepuestaInciso["idTipoRespuesta"].ToString() + "_" + evi["idTipoEvidencia"].ToString();
                    //                        btnOtro.Text = "Adjuntar archivo";
                    //                        btnOtro.OnClientClick = "window.open(\"fileUpload.aspx?idIndicador=" + drIndicador["idIndicador"].ToString() +
                    //                            "&idTema=" + drIndicador["idTema"].ToString() + "&idInciso=" + drInciso["idInciso"].ToString() + "&idTipoEvidencia=" + evi["idTipoEvidencia"].ToString() +
                    //                            "&idCuestionario=" + lblIdCuestionario.Text + "&idTipoRespuesta=" + drTiposDeRepuestaInciso["idTipoRespuesta"].ToString() +
                    //                            "\",null,\"top=250, left=250, dependent=yes, height=400, width=600, menubar=no, location=no, status=no, toolbar=no, titlebar=no, scrollbar=no, resizable=no\");";
                    //                        //btnOtro.Click += new System.EventHandler(this.btnOtroIndicador_Click);
                    //                        tblCellbtn.Controls.Add(btnOtro);

                    //                        Button btnSharepoint = new Button();
                    //                        btnSharepoint.Enabled = chkB.Checked;
                    //                        btnSharepoint.ID = "btnSharepoint" + "_" + drIndicador["idIndicador"].ToString() + "_" + drInciso["idInciso"].ToString() + "_" + drTiposDeRepuestaInciso["idTipoRespuesta"].ToString() + "_" + evi["idTipoEvidencia"].ToString();
                    //                        btnSharepoint.Text = "Lista de documentos";
                    //                        btnSharepoint.OnClientClick = "window.open(\"fileAttach.aspx?idIndicador=" + drIndicador["idIndicador"].ToString() +
                    //                            "&idTema=" + drIndicador["idTema"].ToString() + "&idInciso=" + drInciso["idInciso"].ToString() + "&idTipoEvidencia=" + evi["idTipoEvidencia"].ToString() +
                    //                            "&idCuestionario=" + lblIdCuestionario.Text + "&idTipoRespuesta=" + drTiposDeRepuestaInciso["idTipoRespuesta"].ToString() +
                    //                            "\",null,\"top=50, left=50, dependent=yes, height=600, width=1200, menubar=no, location=no, status=no, toolbar=no, titlebar=no, scrollbar=no, resizable=no\");";
                    //                        //btnSharepoint.OnClientClick = "window.open(\"http://esr.cemefi.org/" + Session["idEmpresa"].ToString() + "/Documentos%20compartidos/Forms/AllItems.aspx\",null,\"top=50, left=50, dependent=yes, height=600, width=1200, menubar=no, location=no, status=no, toolbar=no, titlebar=no, scrollbar=no, resizable=no\");";
                    //                        if (strPerfiles.IndexOf("0") > -1 || strPerfiles.IndexOf("1") > -1 ||
                    //                        strPerfiles.IndexOf("2") > -1 || strPerfiles.IndexOf("3") > -1)
                    //                        {
                    //                            tblCellbtn.Controls.Add(btnSharepoint);
                    //                        }
                    //                        tblRow.Cells.Add(tblCellbtn);
                    //                    }
                    //                    // No se si tengo que dividir el valor que se obtuvo de las evidencias
                    //                    // De hecho no estoy seguro donde se tiene que sumar el valor obtenido de las evidencias
                    //                    promIncisos = promIncisos + promEvidenciaInciso;

                    //                    panTiposDeEvidenciaRA.ContentTemplateContainer.Controls.Add(tblArchivos);

                    //                    if (rbTiposDeRepuestaInciso.Checked)
                    //                        panTiposDeEvidenciaRA.Visible = true;
                    //                    else
                    //                        panTiposDeEvidenciaRA.Visible = false;
                    //                    panRespuestasInciso.ContentTemplateContainer.Controls.Add(panTiposDeEvidenciaRA);
                    //                }
                    //            }
                    //            panRespuestasInciso.Visible = true;
                    //            panIncisos.ContentTemplateContainer.Controls.Add(panRespuestasInciso);
                    //        }
                    //    } //Fin del for de inserci�n de incisos

                    //    if (lblCalificacion.Text != "")
                    //    {
                    //        if (rbFlag)
                    //        {
                    //            promIncisos += Convert.ToSingle(lblCalificacion.Text);
                    //            promIncisos = promIncisos / noIncisos;
                    //            lblCalificacion.Text = promIncisos.ToString();
                    //        }
                    //    }
                    //    if (Request.QueryString["ReadOnly"] == null)
                    //    {
                    //        if (rbFlag)
                    //            panIncisos.Visible = true;
                    //        else
                    //            panIncisos.Visible = false;
                    //    }
                    //    else
                    //    {
                    //        panIncisos.Visible = true;
                    //    }

                    //    // Agrega preguntas adicionales y grupos de relaci�n
                    //    panIncisos.ContentTemplateContainer.Controls.Add(CargaGruposDeRelacion(dsIndicadores.Tables["Grupos_Relacion"], dsRespuestas.Tables["Respuesta_Grupo"]));
                    //    panIncisos.ContentTemplateContainer.Controls.Add(CargaPreguntasAdicionales(dsIndicadores.Tables["Pregunta_Adicional"], dsRespuestas.Tables["Respuesta_Pregunta_Adicional"]));

                    //    panTiposDeRespuestaInciso.ContentTemplateContainer.Controls.Add(panIncisos);

                    //    Panel1.Controls.Add(panTiposDeRespuestaInciso);
                    //}
                    //else //Si no tiene incisos
                    //{
                    #endregion Manejo de Incisos


                    ///Por aqui hay que poner el codigo para presentar la evaluacion de la fase y si no alcanza la calificaci�n, entonces no puede avanzar
                    if (drIndicador["faseImplementacion"].ToString() != "I")
                    {
                        //lblReadOnly.Text = "ReadOnly";
                    }

                    if (lblReadOnly.Text == "")
                    {
                        // Crea un nuevo UpdatePanel y le pone el identificador del indicador.
                        UpdatePanel panTiposDeRespuesta = new UpdatePanel();
                        panTiposDeRespuesta.ID = "panTiposDeRespuesta" + "_" + drIndicador["idIndicador"].ToString();

                        bool flgPAsGR = false;
                        // Para cada registro en la tabla de Tipos de Respuesta
                        foreach (DataRow drTipoRespuesta in dsIndicadores.Tables["Tipo_Respuesta"].Rows)
                        {
                            // Crea un nuevo RadioButton para cada respuesta, le pone el id del indicador y 
                            // la descripcion del tipo de respuesta, ejemplo: 1_No
                            RadioButton rbTiposDeRespuesta = new RadioButton();
                            // 1_No, 1_No aplica, 1_Compromiso P�blico, 1_Si, 1_Mejor pr�ctica
                            // 1022_1, 1022_2, 1022_3, 1022_4, 1022_5
                            rbTiposDeRespuesta.ID = drIndicador["idIndicador"].ToString() + "_" + drTipoRespuesta["idTipoRespuesta"].ToString();
                            rbTiposDeRespuesta.Text = drTipoRespuesta["descripcion"].ToString() + "<br>";

                            // Los agrupa por indicador, ejemplo: respuesta_1, y asigna el m�todo del evento
                            rbTiposDeRespuesta.GroupName = "tipoRespuesta" + "_" + drIndicador["idIndicador"].ToString();
                            rbTiposDeRespuesta.AutoPostBack = true;

                            if (dsRespuestas.Tables["Respuesta_Indicador"].Rows.Count > 0)
                            {
                                DataRow rowRespuestaIndicador = dsRespuestas.Tables["Respuesta_Indicador"].Select("idIndicador = " + drIndicador["idIndicador"].ToString())[0];

                                if (drTipoRespuesta["idTipoRespuesta"].ToString() == rowRespuestaIndicador["idTipoRespuesta"].ToString())
                                {
                                    lblCalificacion.Text = drTipoRespuesta["valor"].ToString();
                                    rbTiposDeRespuesta.Checked = true;
                                    flgPAsGR = true;

                                }
                            }
                            rbTiposDeRespuesta.CheckedChanged += new System.EventHandler(rbInciso_CheckedChanged);
                            // A�ade el control al panel

                            panTiposDeRespuesta.ContentTemplateContainer.Controls.Add(rbTiposDeRespuesta);

                            if (!drTipoRespuesta["descripcion"].ToString().ToUpper().StartsWith("NO") && Convert.ToInt32(drIndicador["ordinal"]) > 0)
                            {
                                // Inserta Tipos de Evidencia
                                UpdatePanel panTiposDeEvidencia = new UpdatePanel();

                                // Asigna el id del updatepanel, ejemplo: panTiposDeEvidencia_1_Si
                                panTiposDeEvidencia.ID = "panTiposDeEvidencia" + "_" + drIndicador["idIndicador"].ToString() + "_" + drTipoRespuesta["descripcion"].ToString();

                                // Para cada registro en la tabla Tipos_Evidencia
                                // crea un check box y a�ade los controles para subir las evidencias.
                                Table tblArchivos = new Table();
                                tblArchivos.BorderWidth = 0;

                                float promEvidenciaIndicador = 0;
                                bool cuentaEvidencia = false;
                                bool flgEAC = false;
                                bool flgRPNA = false;
                                float valorRPNA = 0;
                                int id = 0;
                                foreach (DataRow evi in dsIndicadores.Tables["Tipo_Evidencia"].Rows)
                                {

                                    TableRow tblRow = new TableRow();
                                    TableCell tblCellTab = new TableCell();
                                    tblCellTab.Text = "&nbsp; &nbsp; &nbsp;";
                                    tblRow.Cells.Add(tblCellTab);
                                    TableCell tblCellchk = new TableCell();
                                    TableCell tblCellfup = new TableCell();
                                    TableCell tblCellbtn = new TableCell();

                                    CheckBox chkB = new CheckBox();
                                    // Asigna el ID al checkbox, ejemplo: tipoEvidencia_1_1, tipoEvidencia_1_2
                                    chkB.ID = "tipoEvidencia_" + drIndicador["idIndicador"].ToString() + "_" + drTipoRespuesta["idTipoRespuesta"].ToString() + "_" + evi["idTipoEvidencia"].ToString();
                                    // A�ade la descripci�n de la evidencia y agrega el control al UpdatePanel
                                    chkB.Text = evi["descripcion"].ToString();
                                    chkB.AutoPostBack = true;
                                    chkB.CheckedChanged += new System.EventHandler(this.chkB_CheckedChange);
                                    tblCellchk.Controls.Add(chkB);
                                    tblRow.Cells.Add(tblCellchk);
                                    tblArchivos.Rows.Add(tblRow);

                                    DataRow[] rowEvidenciaIndicador = dsRespuestas.Tables["Evidencia_Indicador"].Select("idTipoRespuesta = " + drTipoRespuesta["idTipoRespuesta"].ToString() + " And idTipoEvidencia = " + evi["idTipoEvidencia"].ToString());
                                    if (rowEvidenciaIndicador.GetLength(0) > 0)
                                    {
                                        chkB.Checked = true;
                                        // Hay que poner las evidencias
                                        if (!cuentaEvidencia)
                                        {
                                            // Autor: MRA
                                            // Fecha: 12/06/2012
                                            //promEvidenciaIndicador += 0.5F;
                                            cuentaEvidencia = true;
                                        }

                                        /// Si agrego uno o varios documentos de este tipo de evidencia, entonces se le toma en cuenta el valor solo de la evidencia
                                        float valor = Convert.ToSingle(evi["valor"].ToString());
                                        promEvidenciaIndicador += valor;

                                        if (evi["idTipoEvidencia"].ToString() == "3")
                                        {
                                            flgEAC = true;
                                        }

                                        if (evi["idTipoEvidencia"].ToString() == "6")
                                        {
                                            flgRPNA = true;
                                            valorRPNA = valor;
                                        }

                                        if (evi["idTipoEvidencia"].ToString() == "4" && flgEAC)
                                        {
                                            promEvidenciaIndicador = promEvidenciaIndicador - valor;
                                        }

                                        if (evi["idTipoEvidencia"].ToString() == "7" && flgRPNA)
                                        {
                                            promEvidenciaIndicador = promEvidenciaIndicador - valorRPNA;
                                        }
                                        
                                        foreach (DataRow drEvidencia in rowEvidenciaIndicador)
                                        {
                                            if (drEvidencia["descripcion"].ToString().ToUpper() != evi["descripcion"].ToString().ToUpper())
                                            {
                                                
                                                // Hay evidencia
                                                TableRow tblRowEvi = new TableRow();

                                                TableCell tblCellTabEvi = new TableCell();
                                                tblCellTab.Text = "&nbsp; &nbsp; &nbsp;";
                                                tblRowEvi.Cells.Add(tblCellTabEvi);

                                                TableCell tblCellchkEvi = new TableCell();
                                                TableCell tblCellfupEvi = new TableCell();
                                                TableCell tblCellbtnEvi = new TableCell();

                                                HyperLink newFile = new HyperLink();
                                                newFile.Text = drEvidencia["descripcion"].ToString();
                                                //newFile.NavigateUrl = "Download.aspx?archivo=" + newFile.Text; //Esta es la liga a desplegar
                                                if (drEvidencia["Url"] == null || drEvidencia["url"].ToString() == "")
                                                {
                                                    newFile.NavigateUrl = "Download.aspx?idIndicador=" + drEvidencia["idIndicador"].ToString() + "&idTema=" + drEvidencia["idTema"].ToString() + "&idTipoEvidencia=" + drEvidencia["idTipoEvidencia"].ToString() +
                                                                                "&idEmpresa=" + drEvidencia["idEmpresa"].ToString() + "&idCuestionario=" + lblIdCuestionario.Text + "&descripcion=" + drEvidencia["descripcion"].ToString().Replace(" ", "%20");
                                                }
                                                else
                                                {
                                                    //newFile.NavigateUrl = "/" + Session["idEmpresa"].ToString() + "/" + drEvidencia["Url"].ToString();
                                                    newFile.NavigateUrl = "/" + GetIdEmpresa().ToString() + "/" + drEvidencia["Url"].ToString();
                                                }
                                                tblCellchkEvi.Controls.Add(newFile);

                                                ImageButton imgEliminar = new ImageButton();
                                                imgEliminar.ID = "imgEliminar_" + drEvidencia["idIndicador"].ToString() + "_" + drEvidencia["idTema"].ToString() + "_"
                                                    + drEvidencia["idTipoRespuesta"].ToString() + "_" + drEvidencia["idTipoEvidencia"].ToString() + "_" + drEvidencia["idEmpresa"].ToString() + "_"
                                                    + String.Format("{0:00}", id);
                                                imgEliminar.ImageUrl = "~/images/close.gif";
                                                imgEliminar.ToolTip = drEvidencia["descripcion"].ToString();
                                                imgEliminar.Click += new ImageClickEventHandler(this.imgEliminar_Click);
                                                id++;
                                                //imgEliminar.PostBackUrl = "diagnostico.aspx?Content=visorDelCuestionario&idTema=" + lblidTema.Text + "&idSubtema=" + lblidSubtema.Text + "&idIndicador=" + lblidIndicador.Text + "&idCuestionario=" + lblIdCuestionario.Text;
                                                tblCellfupEvi.Controls.Add(imgEliminar);

                                                tblRowEvi.Cells.Add(tblCellchkEvi);
                                                tblRowEvi.Cells.Add(tblCellfupEvi);
                                                tblRowEvi.Cells.Add(tblCellbtnEvi);

                                                tblArchivos.Rows.Add(tblRowEvi);
                                            }
                                        }
                                    }
                                    Button btnOtro = new Button();
                                    btnOtro.Enabled = chkB.Checked;
                                    btnOtro.ID = "btnOtro" + "_" + drIndicador["idIndicador"].ToString() + "_" + drTipoRespuesta["idTipoRespuesta"].ToString() + "_" + evi["idTipoEvidencia"].ToString();
                                    btnOtro.Text = "Adjuntar evidencia";
                                    btnOtro.OnClientClick = "window.open(\"fileUpload.aspx?idIndicador=" + drIndicador["idIndicador"].ToString() +
                                        "&idTema=" + drIndicador["idTema"].ToString() + "&idTipoEvidencia=" + evi["idTipoEvidencia"].ToString() +
                                        "&idCuestionario=" + lblIdCuestionario.Text + "&idTipoRespuesta=" + drTipoRespuesta["idTipoRespuesta"].ToString() +
                                        "\",null,\"top=250, left=250, dependent=yes, height=400, width=600, menubar=no, location=no, status=no, toolbar=no, titlebar=no, scrollbars=no, resizable=no\");";
                                    //btnOtro.OnClientClick = "showWindow(\"adjuntar\");";
                                    //btnOtro.Click += new System.EventHandler(this.btnOtroIndicador_Click);
                                    //btnOtro.Visible = false;
                                    tblCellbtn.Controls.Add(btnOtro);

                                    Button btnSharepoint = new Button();
                                    btnSharepoint.Enabled = chkB.Checked;
                                    btnSharepoint.ID = "btnSharepoint" + "_" + drIndicador["idIndicador"].ToString() + "_" + drTipoRespuesta["idTipoRespuesta"].ToString() + "_" + evi["idTipoEvidencia"].ToString();
                                    btnSharepoint.Text = "Lista de documentos";
                                    //btnSharepoint.OnClientClick = "window.open(\"fileAttach.aspx?idIndicador=" + drIndicador["idIndicador"].ToString() +
                                    //    "&idTema=" + drIndicador["idTema"].ToString() + "&idTipoEvidencia=" + evi["idTipoEvidencia"].ToString() +
                                    //    "&idCuestionario=" + lblIdCuestionario.Text + "&idTipoRespuesta=" + drTipoRespuesta["idTipoRespuesta"].ToString() +
                                    //    "\",null,\"top=50, left=50, dependent=yes, height=600, width=1200, menubar=no, location=no, status=no, toolbar=no, titlebar=no, scrollbar=no, resizable=no\");";
                                    //btnSharepoint.OnClientClick = "window.open(\"http://esr.cemefi.org/" + Session["idEmpresa"].ToString() + "/Documentos%20compartidos/Forms/AllItems.aspx\",null,\"top=50, left=50, dependent=yes, height=600, width=1200, menubar=no, location=no, status=no, toolbar=no, titlebar=no, scrollbar=no, resizable=no\");";
                                    btnSharepoint.OnClientClick = "window.open(\"documentLibrary.aspx?idIndicador=" + drIndicador["idIndicador"].ToString() +
                                        "&idTema=" + drIndicador["idTema"].ToString() + "&idTipoEvidencia=" + evi["idTipoEvidencia"].ToString() +
                                        "&idCuestionario=" + lblIdCuestionario.Text + "&idTipoRespuesta=" + drTipoRespuesta["idTipoRespuesta"].ToString() +
                                        "\", null,\"top=250, left=650, dependent=yes, height=400, width=600, menubar=no, location=no, status=no, toolbar=no, titlebar=no,scrollbars=1, resizable=yes\");";
                                    btnSharepoint.Visible = true;
                                    //if (strPerfiles.IndexOf("0") > -1 || strPerfiles.IndexOf("1") > -1 ||
                                    //strPerfiles.IndexOf("2") > -1 || strPerfiles.IndexOf("3") > -1 || strPerfiles.IndexOf("7") > -1 || strPerfiles.IndexOf("8") > -1)
                                    //{
                                    tblCellbtn.Controls.Add(btnSharepoint);
                                    //}
                                    tblRow.Cells.Add(tblCellbtn);
                                }

                                if (lblCalificacion.Text != "")
                                {
                                    promEvidenciaIndicador = Convert.ToSingle(lblCalificacion.Text) + promEvidenciaIndicador;
                                    lblCalificacion.Text = promEvidenciaIndicador.ToString();
                                }
                                panTiposDeEvidencia.ContentTemplateContainer.Controls.Add(tblArchivos);

                                // Oculta el UpdatePanel de tipos de evidencia y lo agrega al UpdatePanel de
                                // Tipos de respuesta.
                                if (rbTiposDeRespuesta.Checked)
                                    panTiposDeEvidencia.Visible = true;
                                else
                                    panTiposDeEvidencia.Visible = false;

                                panTiposDeRespuesta.ContentTemplateContainer.Controls.Add(panTiposDeEvidencia);
                            }
                            else
                            {
                                flgPAsGR = false;
                            }
                        }


                        // Agrega preguntas adicionales y grupos de relaci�n
                        UpdatePanel updAfinidades = CargaAfinidades(dsIndicadores.Tables["Afinidad"]);
                        updAfinidades.Visible = flgPAsGR;
                        UpdatePanel updGR = CargaGruposDeRelacion(dsIndicadores.Tables["Grupos_Relacion"], dsRespuestas.Tables["Respuesta_Grupo"]);
                        updGR.Visible = flgPAsGR;
                        panTiposDeRespuesta.ContentTemplateContainer.Controls.Add(updAfinidades);
                        panTiposDeRespuesta.ContentTemplateContainer.Controls.Add(updGR);
                        // Agrega la estructura de respuestas al panel principal. 
                        Panel1.Controls.Add(panTiposDeRespuesta);
                    }
                    //}
                }
            }
        }
        catch (Exception ex)
        {
            using (StreamWriter sw = File.AppendText(ConfigurationManager.AppSettings["logFile"].ToString()))
            {
                sw.AutoFlush = true;
                sw.WriteLine("Fecha: " + DateTime.Now.ToString());
                sw.WriteLine("Empresa: " + Session["idEmpresa"].ToString());
                sw.WriteLine("idIndicador: " + lblidIndicador.Text);
                sw.WriteLine("idTema: " + lblidTema.Text);
                sw.WriteLine("idCuestionario: " + lblIdCuestionario.Text);
                sw.WriteLine("Error en visorDelCuestionario.Page_Load(): " + ex.Message);
                sw.WriteLine("InnerException: " + ex.InnerException);
                sw.Close();
            }
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dtAfinidades"></param>
    /// <returns></returns>
    protected UpdatePanel CargaAfinidades(DataTable dtAfinidades)
    {
        UpdatePanel updAfinidades = new UpdatePanel();
        id++;
        updAfinidades.ID = "updAfin" + id.ToString();
        // Pone la pregunta

        if (dtAfinidades.Rows.Count > 0)
        {
            Label lblTituloAfinidad = new Label();
            lblTituloAfinidad.Text = "<hr /><p>&nbsp;&nbsp;&nbsp;<b>Este indicador es afin a:</b>";
 
            updAfinidades.ContentTemplateContainer.Controls.Add(lblTituloAfinidad);

            Table tblAfinidades = new Table();
            tblAfinidades.BorderWidth = 0;

            foreach (DataRow drAfinidad in dtAfinidades.Rows)
            {
                TableRow tblRowLbl = new TableRow();
                TableCell tblCellTabLbl = new TableCell();
                tblCellTabLbl.Text = "&nbsp; &nbsp; &nbsp;";
                tblRowLbl.Cells.Add(tblCellTabLbl);
                TableCell tblCellLbl = new TableCell();

                Label lblAfinidad = new Label();
                lblAfinidad.Text = "<p>&nbsp;&nbsp;- " + drAfinidad["descripcion"].ToString() + "</p>";

                tblCellLbl.Controls.Add(lblAfinidad);
                tblRowLbl.Cells.Add(tblCellLbl);
                tblAfinidades.Rows.Add(tblRowLbl);
            }
            TableRow tblRowBtn = new TableRow();
            TableCell tblCellBtnTab = new TableCell();
            tblCellBtnTab.Text = "&nbsp; &nbsp; &nbsp;";
            tblRowBtn.Cells.Add(tblCellBtnTab);
            TableCell tblCellBtn = new TableCell();
            updAfinidades.ContentTemplateContainer.Controls.Add(tblAfinidades);

        }

        return updAfinidades;
    }

    /// <summary>
    /// M�todo para instanciar preguntas adicionales y ponerlas en pantalla
    /// 
    /// </summary>
    /// <returns></returns>
    protected UpdatePanel CargaPreguntasAdicionales(DataTable dtPreguntasAdicionales, DataTable dtRespuestas)
    {
        UpdatePanel updPAs = new UpdatePanel();
        id++;
        updPAs.ID = "updPAS" + id.ToString();
        // Pone la pregunta

        if (dtPreguntasAdicionales.Rows.Count > 0)
        {
            Label lblTituloPA = new Label();
            lblTituloPA.Text = "<hr /><p>&nbsp;&nbsp;&nbsp;<b>Para medir su desempe�o, sugerimos considerar los siguientes indicadores (con fines de referencia, no de evaluaci�n):</b>";
            //if (dtPreguntasAdicionales.Rows[0]["idTema"].ToString() == "5")
            //    lblTituloPA.Text += "<p>&nbsp;&nbsp;&nbsp;<b><a href=\"/archivos/Calculo de Emisiones.xls\">De clic aqu� para bajar la f�rmula de apoyo, para realizar el c�lculo de emisiones (si le son solicitadas).</a></b></p>";

            updPAs.ContentTemplateContainer.Controls.Add(lblTituloPA);

            Table tblPAs = new Table();
            tblPAs.BorderWidth = 0;

            foreach (DataRow drPA in dtPreguntasAdicionales.Rows)
            {
                TableRow tblRowLbl = new TableRow();
                TableCell tblCellTabLbl = new TableCell();
                tblCellTabLbl.Text = "&nbsp; &nbsp; &nbsp;";
                tblRowLbl.Cells.Add(tblCellTabLbl);
                TableCell tblCellLbl = new TableCell();

                Label lblPA = new Label();
                lblPA.Text = "<p>&nbsp;&nbsp;" + drPA["idPregunta"].ToString() + ". " + drPA["pregunta"].ToString() + "</p>";

                tblCellLbl.Controls.Add(lblPA);
                tblRowLbl.Cells.Add(tblCellLbl);
                tblPAs.Rows.Add(tblRowLbl);

                // Si es de solo lectura no pone el cuadro de texto para contestar
                if (lblReadOnly.Text == "")
                {
                    TableRow tblRow = new TableRow();
                    TableCell tblCellTab = new TableCell();
                    tblCellTab.Text = "&nbsp; &nbsp; &nbsp;";
                    tblRow.Cells.Add(tblCellTab);
                    TableCell tblCellTxt = new TableCell();
                    
                    TextBox txtRespuestaPA = new TextBox();
                    txtRespuestaPA.ID = drPA["idTema"].ToString() + "_" + drPA["idIndicador"].ToString() + "_" + drPA["idPregunta"].ToString();
                    DataRow[] drRespuestaPA = dtRespuestas.Select(String.Format("idIndicador = {0} and idTema = {1} and idPregunta = {2} and idEmpresa = {3} and idCuestionario = {4}",
                                drPA["idIndicador"].ToString(), drPA["idTema"].ToString(), drPA["idPregunta"].ToString(), this.GetIdEmpresa().ToString(), lblIdCuestionario.Text));

                    if (drRespuestaPA.GetUpperBound(0) == 0)
                    {
                        txtRespuestaPA.Text = drRespuestaPA[0]["Respuesta"].ToString();
                    }
                    txtRespuestaPA.Height = Unit.Pixel(60);
                    txtRespuestaPA.Width = Unit.Pixel(500);
                    txtRespuestaPA.TextMode = TextBoxMode.MultiLine;

                    tblCellTxt.Controls.Add(txtRespuestaPA);
                    tblRow.Cells.Add(tblCellTxt);
                    tblPAs.Rows.Add(tblRow);
                }
            }
            TableRow tblRowBtn = new TableRow();
            TableCell tblCellBtnTab = new TableCell();
            tblCellBtnTab.Text = "&nbsp; &nbsp; &nbsp;";
            tblRowBtn.Cells.Add(tblCellBtnTab);
            TableCell tblCellBtn = new TableCell();

            Button btnGuardar = new Button();
            btnGuardar.Text = "Guardar";
            btnGuardar.Click += new System.EventHandler(this.btnGuardarPAs_Click);
            tblCellBtn.Controls.Add(btnGuardar);
            tblRowBtn.Cells.Add(tblCellBtn);
            tblPAs.Rows.Add(tblRowBtn);
            updPAs.ContentTemplateContainer.Controls.Add(tblPAs);

        }

        return updPAs;
    }

    protected void btnGuardarPAs_Click(object sender, EventArgs e)
    {
        Table tblPAs = (Table)((Button)sender).Parent.Parent.Parent;
        foreach (Control tblRow in tblPAs.Controls)
        {
            if (tblRow.GetType() == typeof(TableRow))
            {
                foreach (Control tblCell in tblRow.Controls)
                {
                    if (tblCell.GetType() == typeof(TableCell))
                    {
                        foreach (Control txt in tblCell.Controls)
                        {
                            if (txt.GetType() == typeof(TextBox))
                            {
                                TextBox txtRespuestaPA = (TextBox)txt;
                                string[] ids = txtRespuestaPA.ID.Split('_');
                                PreguntaAdicional pa = new PreguntaAdicional();
                                pa.idTema = Convert.ToInt32(ids[0]);
                                pa.idIndicador = Convert.ToInt32(ids[1]);
                                pa.idPregunta = Convert.ToInt32(ids[2]);
                                pa.idEmpresa = this.GetIdEmpresa();
                                pa.pregunta = txtRespuestaPA.Text.Trim();
                                pa.idUsuario = this.GetIdUsuario();
                                pa.idCuestionario = Convert.ToInt32(lblIdCuestionario.Text);
                                pa.GuardarRespuestaEmpresa();
                            }
                        }
                    }
                }

            }
        }
    }

    /// <summary>
    /// M�todo para instanciar grupos de relaci�n y ponerlos en pantalla.
    /// </summary>
    /// <returns></returns>
    protected UpdatePanel CargaGruposDeRelacion(DataTable dtGruposDeRelacion, DataTable dtRespuestas)
    {
        
        UpdatePanel updGruposDeRelacion = new UpdatePanel();
        id++;
        updGruposDeRelacion.ID = "updGruposDeRelacion" + id.ToString(); ;
        // Si es de solo lectura que no ponga esta secci�n
        if (lblReadOnly.Text == "")
        {
            if (dtGruposDeRelacion.Rows.Count > 0)
            {
                // Pone la pregunta
                Label lblTituloGR = new Label();
                lblTituloGR.Text = "<hr /><p>&nbsp;&nbsp;&nbsp;<b>Para complementar su respuesta, seleccione los grupos de relaci�n (stakeholders) a los que beneficia con este indicador: </b>";
                updGruposDeRelacion.ContentTemplateContainer.Controls.Add(lblTituloGR);

                Table tblGP = new Table();
                tblGP.BorderWidth = 0;
                foreach (DataRow drGP in dtGruposDeRelacion.Rows)
                {
                    TableRow tblRow = new TableRow();
                    TableCell tblCellTab = new TableCell();
                    tblCellTab.Text = "&nbsp; &nbsp; &nbsp;";
                    tblRow.Cells.Add(tblCellTab);
                    TableCell tblCellchk = new TableCell();

                    CheckBox chkGP = new CheckBox();
                    chkGP.ID = drGP["idGrupo"].ToString();
                    chkGP.Text = drGP["nombre"].ToString();
                    DataRow[] drRespuestaGR = dtRespuestas.Select(String.Format("idIndicador = {0} and idTema = {1} and idEmpresa = {2} and idCuestionario = {3} and idGrupo = {4}",
                            lblidIndicador.Text, lblidTema.Text, this.GetIdEmpresa().ToString(), lblIdCuestionario.Text, drGP["idGrupo"].ToString()));
                    
                    if (drRespuestaGR.GetUpperBound(0) == 0)
                        chkGP.Checked = true;

                    chkGP.AutoPostBack = true;
                    chkGP.CheckedChanged += new System.EventHandler(this.chkGP_CheckedChange);

                    tblCellchk.Controls.Add(chkGP);
                    tblRow.Cells.Add(tblCellchk);
                    tblGP.Rows.Add(tblRow);
                }
                updGruposDeRelacion.ContentTemplateContainer.Controls.Add(tblGP);
            }
        }
        return updGruposDeRelacion;
        
    }
    /// <summary>
    /// M�todo para actualizar la selecci�n de grupos de relaci�n
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void chkGP_CheckedChange(object sender, EventArgs e)
    {
        CheckBox control = (CheckBox)sender;

        GrupoRelacion grpRlc = new GrupoRelacion();
        grpRlc.idIndicador = Convert.ToInt32(lblidIndicador.Text);
        grpRlc.idTema = Convert.ToInt32(lblidTema.Text);
        grpRlc.idEmpresa = this.GetIdEmpresa();
        grpRlc.idCuestionario = Convert.ToInt32(lblIdCuestionario.Text);
        grpRlc.idGrupo = Convert.ToInt32(control.ID);
        grpRlc.idUsuario = this.GetIdUsuario();

        if (control.Checked)
            grpRlc.Guardar();
        else
            grpRlc.Eliminar();
    }

    /// <summary>
    /// M�todo para eliminar los archivos adjuntos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void imgEliminarEviInciso_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton control = (ImageButton)sender;
        string strControl = control.ID.ToString().Substring(control.ID.ToString().IndexOf("_") + 1);

        int _pPosAnterior = 0;
        int _pos = strControl.IndexOf("_", 1);
        //int idIndicador = Convert.ToInt32(strControl.Substring(0, _pos));

        _pPosAnterior = _pos + 1;
        _pos = strControl.IndexOf("_", _pos + 1);
        //int idTema = Convert.ToInt32(strControl.Substring(_pPosAnterior, _pos - _pPosAnterior));

        _pPosAnterior = _pos + 1;
        _pos = strControl.IndexOf("_", _pos + 1);
        string idInciso = strControl.Substring(_pPosAnterior, _pos - _pPosAnterior);

        _pPosAnterior = _pos + 1;
        _pos = strControl.IndexOf("_", _pos + 1);
        //int idTipoRespuesta = Convert.ToInt32(strControl.Substring(_pPosAnterior, _pos - _pPosAnterior));

        _pPosAnterior = _pos + 1;
        _pos = strControl.IndexOf("_", _pos + 1);
        int idTipoEvidencia = Convert.ToInt32(strControl.Substring(_pPosAnterior, _pos - _pPosAnterior));

        _pPosAnterior = _pos + 1;
        _pos = strControl.IndexOf("_", _pos + 1);
        //int idEmpresa = Convert.ToInt32(strControl.Substring(_pPosAnterior, _pos - _pPosAnterior));

        _pPosAnterior = _pos + 1;
        string fileName = strControl.Substring(_pPosAnterior, strControl.Length - _pPosAnterior);


        Inciso borraEvidenciaInciso = new Inciso();

        borraEvidenciaInciso.idIndicador = Convert.ToInt32(lblidIndicador.Text);
        borraEvidenciaInciso.idTema = Convert.ToInt32(lblidTema.Text);
        borraEvidenciaInciso.idInciso = idInciso;
        borraEvidenciaInciso.idTipoEvidencia = idTipoEvidencia;
        borraEvidenciaInciso.idEmpresa = this.GetIdEmpresa(); //Convert.ToInt32(Session["idEmpresa"].ToString());
        borraEvidenciaInciso.idCuestionario = Convert.ToInt32(lblIdCuestionario.Text);
        borraEvidenciaInciso.fileName = fileName;
       
        // Implementar la eliminaci�n del archivo
        if (borraEvidenciaInciso.EliminarEvidencia())
        {
            if (Request.Params["idEmpresa"] == null)
            {
                Response.Redirect("diagnostico.aspx?Content=visorDelCuestionario&idTema=" + lblidTema.Text + "&idSubtema=" + lblidSubtema.Text + "&idIndicador=" + lblidIndicador.Text + "&idCuestionario=" + lblIdCuestionario.Text);
            }
            else
            {
                Response.Redirect("diagnostico.aspx?Content=visorDelCuestionario&idTema=" + lblidTema.Text + "&idSubtema=" + lblidSubtema.Text + "&idIndicador=" + lblidIndicador.Text + "&idCuestionario=" + lblIdCuestionario.Text + "&idEmpresa=" + Request.Params["idEmpresa"].ToString());
            }
        }


    }

    /// <summary>
    /// M�todo para eliminar los archivos adjuntos en los indicadores
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void imgEliminar_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton control = (ImageButton)sender;
        string strControl = control.ID.ToString().Substring(control.ID.ToString().IndexOf("_") + 1);

        //StreamWriter sw = File.AppendText(ConfigurationManager.AppSettings["logFile"].ToString());
        //sw.AutoFlush = true;
        //sw.WriteLine("Entrando a imgEliminar_Click(): " + strControl);

        int _pPosAnterior = 0;
        int _pos = strControl.IndexOf("_", 1);
        //int idIndicador = Convert.ToInt32(strControl.Substring(0, _pos));

        _pPosAnterior = _pos + 1;
        _pos = strControl.IndexOf("_", _pos + 1);
        //int idTema = Convert.ToInt32(strControl.Substring(_pPosAnterior, _pos - _pPosAnterior));

        _pPosAnterior = _pos + 1;
        _pos = strControl.IndexOf("_", _pos + 1);
        //int idTipoRespuesta = Convert.ToInt32(strControl.Substring(_pPosAnterior, _pos - _pPosAnterior));

        _pPosAnterior = _pos + 1;
        _pos = strControl.IndexOf("_", _pos + 1);
        int idTipoEvidencia = Convert.ToInt32(strControl.Substring(_pPosAnterior, _pos - _pPosAnterior));

        _pPosAnterior = _pos + 1;
        _pos = strControl.IndexOf("_", _pos + 1);
        //int idEmpresa = Convert.ToInt32(strControl.Substring(_pPosAnterior, _pos - _pPosAnterior));

        _pPosAnterior = _pos + 1;
        _pos = strControl.IndexOf("_", _pos + 1);
        string fileName = control.ToolTip; //strControl.Substring(_pPosAnterior, (strControl.Length - 3) - _pPosAnterior);
        //string fileName = strControl.Substring(_pPosAnterior, _pos - _pPosAnterior);
        //fileName = fileName.Replace("%20", " ");

        //StreamWriter sw = File.AppendText(ConfigurationManager.AppSettings["logFile"].ToString());
        //sw.AutoFlush = true;
        //sw.WriteLine("idIndicador: " + lblidIndicador.Text);
        //sw.WriteLine("Tema: " + lblidTema.Text);
        //sw.WriteLine("idTipoEvidencia: " + idTipoEvidencia.ToString());
        //sw.WriteLine("idEmpresa: " + this.GetIdEmpresa());// Convert.ToInt32(Session["idEmpresa"].ToString());
        //sw.WriteLine("idCuestionario: " + lblIdCuestionario.Text);
        //sw.WriteLine("fileName: " + fileName);


        Indicador borraEvidenciaIndicador = new Indicador();
        borraEvidenciaIndicador.idIndicador = Convert.ToInt32(lblidIndicador.Text);
        borraEvidenciaIndicador.idTema = Convert.ToInt32(lblidTema.Text);
        borraEvidenciaIndicador.idTipoEvidencia = idTipoEvidencia;
        borraEvidenciaIndicador.idEmpresa = this.GetIdEmpresa();// Convert.ToInt32(Session["idEmpresa"].ToString());
        borraEvidenciaIndicador.idCuestionario = Convert.ToInt32(lblIdCuestionario.Text);
        borraEvidenciaIndicador.fileName = fileName;
        //borraEvidenciaIndicador.idUsuario = Session["idUsuario"].ToString();
        //borraEvidenciaIndicador.idTipoRespuesta = idTipoRespuesta;

        // Implementar la eliminaci�n del archivo
        if (borraEvidenciaIndicador.EliminarEvidencia())
        {
            borraEvidenciaIndicador.fileName = fileName.Replace("%20", " ");
            if (borraEvidenciaIndicador.EliminarEvidencia())
            {
                if (Request.Params["idEmpresa"] == null)
                {
                    Response.Redirect("diagnostico.aspx?Content=visorDelCuestionario&idTema=" + lblidTema.Text + "&idSubtema=" + lblidSubtema.Text + "&idIndicador=" + lblidIndicador.Text + "&idCuestionario=" + lblIdCuestionario.Text);
                }
                else
                {
                    Response.Redirect("diagnostico.aspx?Content=visorDelCuestionario&idTema=" + lblidTema.Text + "&idSubtema=" + lblidSubtema.Text + "&idIndicador=" + lblidIndicador.Text + "&idCuestionario=" + lblIdCuestionario.Text + "&idEmpresa=" + Request.Params["idEmpresa"].ToString());
                }
            }
        }
        else
        {
            //sw.WriteLine("Error al eliminar la evidencia");
        }
        //sw.Close();
    }

    /// <summary>
    /// Este m�todo se manda llamar en el indicador cuando HAY incisos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rbTiposDeRespuestaInciso_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton control = (RadioButton)sender;
        Control pan = control.Parent;

        // No importando la respuesta, esconde todos los UpdatePanel
        foreach (Control ctrl in pan.Controls)
        {
            if (ctrl.GetType() == typeof(UpdatePanel))
            {
                ctrl.Visible = false;
            }
        }
        
        int idTiposEvidencia = pan.Controls.IndexOf(control);
        idTiposEvidencia++;

        this.GuardarRespuestaIndicador(control.ID);
        if (control.ID.Substring(control.ID.Length - 1, 1) != "1" && control.ID.Substring(control.ID.Length - 1, 1) != "2")
        {
            pan.Controls[idTiposEvidencia].Visible = true;
            pan.Controls[6].Visible = true;
        }
        else
        {
            //pan.Controls[idTiposEvidencia].Visible = false;
            pan.Controls[6].Visible = false; 
        }
    }

    /// <summary>
    /// Este m�todo se manda llamar en el indicador cuando no hay incisos 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rbInciso_CheckedChanged(object sender, EventArgs e)
    {

        try
        {
            RadioButton control = (RadioButton)sender;
            Control pan = control.Parent;

            // No importando la respuesta, esconde todos los UpdatePanel
            foreach (Control ctrl in pan.Controls)
            {
                if (ctrl.GetType() == typeof(UpdatePanel))
                {
                    ctrl.Visible = false;
                }
            }
            //30_10
            if (control.ID.Length <= 5)
            {
                this.GuardarRespuestaIndicador(control.ID);
                // 1_1, 1_2
                if (control.ID.Substring(control.ID.Length - 1, 1) != "6" && control.ID.Substring(control.ID.Length - 1, 1) != "7")
                {
                    int idTiposEvidencia = pan.Controls.IndexOf(control);
                    idTiposEvidencia++;
                    pan.Controls[idTiposEvidencia].Visible = true;
                    //if (control.ID.Substring(0, 1) != "0")
                    //{
                    //    sw.WriteLine("No. de paneles: " + pan.Controls.Count);
                    //    pan.Controls[10].Visible = true;
                    //    pan.Controls[11].Visible = true;
                    //}
                }
            }
            else
            {
                this.GuardarRespuestaInciso(control.ID);
                // 2_a_1, 2_a_2
                if (control.ID.Substring(4, 1) != "1" && control.ID.Substring(4, 1) != "2")
                {
                    int idTiposEvidencia = pan.Controls.IndexOf(control);
                    idTiposEvidencia++;
                    pan.Controls[idTiposEvidencia].Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            using (StreamWriter sw = File.AppendText(ConfigurationManager.AppSettings["logFile"].ToString()))
            {
                sw.AutoFlush = true;
                sw.WriteLine("Fecha: " + DateTime.Now.ToString());
                sw.WriteLine("Empresa: " + Session["idEmpresa"].ToString());
                sw.WriteLine("idIndicador: " + lblidIndicador.Text);
                sw.WriteLine("idTema: " + lblidTema.Text);
                sw.WriteLine("idCuestionario: " + lblIdCuestionario.Text);
                sw.WriteLine("Error en rbInciso_CheckedChanged(): " + ex.Message);
                sw.WriteLine("Stacktrace: " + ex.StackTrace);
                sw.Close();
            }
            throw new Exception("Error en visorDelCuestionario.rbInciso_CheckedChanged(): " + ex.Message);
        }
    }

    /// <summary>
    /// Guarda las respuestas del indicador
    /// </summary>
    /// <param name="controlID"></param>
    private void GuardarRespuestaIndicador(string controlID)
    {

        try
        {
            if (Session["idCuestionario"] != null && Session["idCuestionario"].ToString() != "")
            {

                // 1_1, 1_2, 1_3, 1_10
                //Salvar los tipos de respuestas de los indicadores
                Indicador indi = new Indicador();
                indi.idIndicador = Convert.ToInt32(controlID.Substring(0, controlID.IndexOf("_")));
                indi.idTema = Convert.ToInt32(lblidTema.Text); ;
                indi.idEmpresa = this.GetIdEmpresa();// Convert.ToInt32(Session["idEmpresa"].ToString());
                indi.idCuestionario = Convert.ToInt32(Session["idCuestionario"].ToString());
                indi.idTipoRespuesta = Convert.ToInt32(controlID.Substring(controlID.IndexOf("_") + 1));
                indi.idUsuario = Session["idUsuario"].ToString();
                indi.GuardarRespuesta();
            }
            else
            {
                Response.Redirect("~/login.aspx", false);
            }
        }
        catch (Exception ex)
        {
            using (StreamWriter sw = File.AppendText(ConfigurationManager.AppSettings["logFile"].ToString()))
            {
                sw.AutoFlush = true;
                sw.WriteLine("Fecha: " + DateTime.Now.ToString());
                sw.WriteLine("Error en visorDelCuestionario.GuardarRespuestaIndicador(): " + ex.Message);
                sw.WriteLine("Stacktrace: " + ex.StackTrace);
                sw.Close();
            }
            throw new Exception("Error en visorDelCuestionario.GuardarRespuestaIndicador(): " + ex.Message);
        }
    }

    /// <summary>
    /// Guarda las respuestas del inciso
    /// </summary>
    /// <param name="controlID"></param>
    private void GuardarRespuestaInciso(string controlID)
    {
        try
        {
            // 2_a_1, 2_a_2, 2_a_3
            //Salvar los tipos de respuestas de los incisos
            Inciso inci = new Inciso();
            inci.idIndicador = Convert.ToInt32(controlID.Substring(0, controlID.IndexOf("_")));
            inci.idTema = Convert.ToInt32(lblidTema.Text); ;
            inci.idInciso = controlID.Substring(controlID.IndexOf("_") + 1, 1);//"a";
            inci.idEmpresa = this.GetIdEmpresa();// Convert.ToInt32(Session["idEmpresa"].ToString());
            inci.idCuestionario = Convert.ToInt32(Session["idCuestionario"].ToString());
            inci.idTipoRespuesta = Convert.ToInt32(controlID.Substring(controlID.IndexOf("_", 3) + 1, 1));
            inci.idUsuario = Session["idUsuario"].ToString();
            inci.GuardarRespuesta();

        }
        catch (Exception ex)
        {
            using (StreamWriter sw = File.AppendText(ConfigurationManager.AppSettings["logFile"].ToString()))
            {
                sw.AutoFlush = true;
                sw.WriteLine("Fecha: " + DateTime.Now.ToString());
                sw.WriteLine("Empresa: " + Session["idEmpresa"].ToString());
                sw.WriteLine("idIndicador: " + lblidIndicador.Text);
                sw.WriteLine("idTema: " + lblidTema.Text);
                sw.WriteLine("idCuestionario: " + lblIdCuestionario.Text);
                sw.WriteLine("Error en visorDelCuestionario.GuardarRespuestaInciso(): " + ex.Message);
                sw.WriteLine("InnerException: " + ex.InnerException);
                sw.Close();
            }
        }
    }

    /// <summary>
    /// Se ejecuta cuando se selecciona un tipo de evidencia
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void chkB_CheckedChange(object sender, EventArgs e)
    {
        CheckBox control = (CheckBox)sender;
        //Celda
        Control celda = control.Parent;
        Control renglon = celda.Parent;
        string strControl = control.ID.ToString().Substring(control.ID.ToString().IndexOf("_"));
        string idbtn = "btnOtro" + strControl;
        string idbtnSharePoint = "btnSharepoint" + strControl;

        int _pPosAnterior = 0;
        int _pos = strControl.IndexOf("_", 1);
        //int idIndicador = Convert.ToInt32(strControl.Substring(0, _pos));

        _pPosAnterior = _pos + 1;
        _pos = strControl.IndexOf("_", _pos + 1);
        int idTipoRespuesta = Convert.ToInt32(strControl.Substring(_pPosAnterior, _pos - _pPosAnterior));

        _pPosAnterior = _pos + 1;
        _pos = strControl.IndexOf("_", _pos + 1);
        int idTipoEvidencia = Convert.ToInt32(strControl.Substring(_pPosAnterior, strControl.Length - _pPosAnterior));

        Indicador evidenciaIndicador = new Indicador();
        evidenciaIndicador.idIndicador = Convert.ToInt32(lblidIndicador.Text);
        evidenciaIndicador.idTema = Convert.ToInt32(lblidTema.Text);
        evidenciaIndicador.idTipoEvidencia = idTipoEvidencia;
        evidenciaIndicador.idEmpresa = this.GetIdEmpresa();// Convert.ToInt32(Session["idEmpresa"].ToString());
        evidenciaIndicador.idCuestionario = Convert.ToInt32(lblIdCuestionario.Text);
        evidenciaIndicador.fileName = control.Text.ToUpper();
        evidenciaIndicador.idTipoRespuesta = idTipoRespuesta;
        evidenciaIndicador.idUsuario = Session["idUsuario"].ToString();

        if (control.Checked)
            evidenciaIndicador.GuardarEvidencia();
        else
            evidenciaIndicador.EliminarEvidencia();

        Button btnEvi = (Button)renglon.FindControl(idbtn);
        btnEvi.Enabled = control.Checked;

        string strPerfiles = Session["perfil"].ToString();
        //if (strPerfiles.IndexOf("0") > -1 || strPerfiles.IndexOf("1") > -1 ||
        //strPerfiles.IndexOf("2") > -1 || strPerfiles.IndexOf("3") > -1 || strPerfiles.IndexOf("7") > -1)
        //{
        Button btnSharePoint = (Button)renglon.FindControl(idbtnSharePoint);
        btnSharePoint.Enabled = control.Checked;
        //}
    }

    /// <summary>
    /// Verifica el tipo de evidencia que se est� seleccionando.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void chkB_CheckedChangeInc(object sender, EventArgs e)
    {
        CheckBox control = (CheckBox)sender;
        //Celda
        Control celda = control.Parent;
        Control renglon = celda.Parent;
        string strControl = control.ID.ToString().Substring(control.ID.ToString().IndexOf("_"));
        string idbtn = "btnOtro" + strControl;
        string idbtnSharePoint = "btnSharepoint" + strControl;

        int _pPosAnterior = 0;
        int _pos = strControl.IndexOf("_", 1);
        //int idIndicador = Convert.ToInt32(strControl.Substring(0, _pos));

        _pPosAnterior = _pos + 1;
        _pos = strControl.IndexOf("_", _pos + 1);
        string idInciso = strControl.Substring(_pPosAnterior, _pos - _pPosAnterior);

        _pPosAnterior = _pos + 1;
        _pos = strControl.IndexOf("_", _pos + 1);
        int idTipoRespuesta = Convert.ToInt32(strControl.Substring(_pPosAnterior, _pos - _pPosAnterior));

        _pPosAnterior = _pos + 1;
        _pos = strControl.IndexOf("_", _pos + 1);
        int idTipoEvidencia = Convert.ToInt32(strControl.Substring(_pPosAnterior, strControl.Length - _pPosAnterior));

        Inciso evidenciaInciso = new Inciso();
        evidenciaInciso.idIndicador = Convert.ToInt32(lblidIndicador.Text);
        evidenciaInciso.idTema = Convert.ToInt32(lblidTema.Text);
        evidenciaInciso.idInciso = idInciso;
        evidenciaInciso.idTipoEvidencia = idTipoEvidencia;
        evidenciaInciso.idEmpresa = this.GetIdEmpresa();// Convert.ToInt32(Session["idEmpresa"].ToString());
        evidenciaInciso.idCuestionario = Convert.ToInt32(lblIdCuestionario.Text);
        evidenciaInciso.fileName = control.Text.ToUpper();
        evidenciaInciso.idTipoRespuesta = idTipoRespuesta;
        evidenciaInciso.idUsuario = Session["idUsuario"].ToString();

        if (control.Checked)
            evidenciaInciso.GuardarEvidencia();
        else
            evidenciaInciso.EliminarEvidencia();

        Button btnEvi = (Button)renglon.FindControl(idbtn);
        btnEvi.Enabled = control.Checked;

        string strPerfiles = Session["perfil"].ToString();
        //if (strPerfiles.IndexOf("0") > -1 || strPerfiles.IndexOf("1") > -1 ||
        //strPerfiles.IndexOf("2") > -1 || strPerfiles.IndexOf("3") > -1)
        //{
            Button btnSharePoint = (Button)renglon.FindControl(idbtnSharePoint);
            btnSharePoint.Enabled = control.Checked;
        //}
    }

    protected void btnActualizarEvaluacion_Click(object sender, EventArgs e)
    {
        float fResultado = 0;

        if (lblCalificacion.Text.Trim() != "")
        {
            fResultado = Convert.ToSingle(lblCalificacion.Text);
        }

        if (txtCalificacionRevisor.Text.Trim() != "")
        {
            fResultado += Convert.ToSingle(txtCalificacionRevisor.Text);
        }
        
        if (fResultado <= 3)
        {
            // Aqui guardar el valor de la precalificacion y la evaluaci�n del revisor
            Indicador indi = new Indicador();
            indi.idIndicador = Convert.ToInt32(lblidIndicador.Text);
            indi.idTema = Convert.ToInt32(lblidTema.Text); ;
            indi.idEmpresa = this.GetIdEmpresa(); // Convert.ToInt32(Session["idEmpresa"].ToString());
            indi.idCuestionario = Convert.ToInt32(lblIdCuestionario.Text);
            indi.idUsuario = Session["idUsuario"].ToString();

            indi.valor = Convert.ToSingle(lblCalificacion.Text);

            if (txtCalificacionRevisor.Text != "")
                indi.calificacionRevisor = Convert.ToSingle(txtCalificacionRevisor.Text);
            else
                indi.calificacionRevisor = 0;

            indi.GuardarEvaluacion();

            if (Request.Params["idEmpresa"] == null)
            {
                Response.Redirect("diagnostico.aspx?Content=visorDelCuestionario&idTema=" + lblidTema.Text + "&idSubtema=" + lblidSubtema.Text + "&idIndicador=" + lblidIndicador.Text + "&idCuestionario=" + lblIdCuestionario.Text);
            }
            else
            {
                Response.Redirect("diagnostico.aspx?Content=visorDelCuestionario&idTema=" + lblidTema.Text + "&idSubtema=" + lblidSubtema.Text + "&idIndicador=" + lblidIndicador.Text + "&idCuestionario=" + lblIdCuestionario.Text + "&idEmpresa=" + Request.Params["idEmpresa"].ToString());
            }
        }
        else
        {
            lblError.Text = "El valor ingresado en la evaluacio4n supera los 3 puntos destinados para este indicador, por favor ingrese otro valor.";
        }
    }
}
