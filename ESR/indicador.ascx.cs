using System;
using System.Data;
using System.Web.UI.WebControls;
using ESR.Business;
using System.IO;
using System.Configuration;

public partial class indicador : System.Web.UI.UserControl
{
    DataSet dsTemas;

    private void MuestraDatosDeIndicador()
    {
        try
        {
            int piIdTema = Convert.ToInt32(Request.QueryString["idTema"]);
            int piIdSubtema = Convert.ToInt32(Request.QueryString["idSubtema"]);
            int piIdIndicador = Convert.ToInt32(Request.QueryString["idIndicador"]);

            DataView dv = new DataView(dsTemas.Tables["Subtema"], "idTema = " + piIdTema, "idSubtema", DataViewRowState.OriginalRows);

            ddlSubtemas.DataSource = dv;
            ddlSubtemas.DataTextField = "descripcion";
            ddlSubtemas.DataValueField = "idSubtema";
            ddlSubtemas.DataBind();

            ListItem item = new ListItem("Seleccione subtema...", "0");
            ddlSubtemas.Items.Insert(0, item);

            ddlTemas.SelectedValue = piIdTema.ToString();
            ddlTemas.Enabled = false;
            ddlSubtemas.SelectedValue = piIdSubtema.ToString();
            btnEliminarIndicador.Visible = true;
            btnAnadirIncisos.Enabled = true;

            Indicador indicador = new Indicador();
            DataSet dsIndicador = indicador.Carga(piIdTema, piIdIndicador);

            DataTable tblIndicador = dsIndicador.Tables["Indicador"];
            foreach (DataRow row in tblIndicador.Rows)
            {
                txtIdIndicador.Text = row["idIndicador"].ToString();
                txtDescripcion.Text = row["Indicador"].ToString();
                txtDescripcion_alterna.Text = row["descripcionAlterna"].ToString();
                txtDescripcion_portugues.Text = row["descripcionPortugues"].ToString();

                if (row["evidencia"].ToString() != "")
                {
                    if (Convert.ToBoolean(row["evidencia"].ToString()))
                        rbtEvidenciaSi.Checked = true;
                    else
                        rbtEvidenciaNo.Checked = true;
                }
                txtNotas.Text = row["nota"].ToString();
                if (row["obligatorio"].ToString() != "")
                {
                    if (Convert.ToBoolean(row["obligatorio"].ToString()))
                        rbtObligatorioSi.Checked = true;
                    else
                        rbtObligatorioNo.Checked = true;
                }
                txtCorto.Text = row["corto"].ToString();
                txtRecomendacion.Text = row["recomendacion"].ToString();
                txtEvidencias.Text = row["evidencias"].ToString();
            }

            // Pone las afinidades del indicador
            DataTable tblAfinidades = dsIndicador.Tables["Afinidad"];
            foreach (DataRow row in tblAfinidades.Rows)
            {
                foreach (CheckBox chbx in phAfinidades.Controls)
                {
                    if (chbx.ID == "chkAfinidad_" + row["idAfinidad"].ToString())
                    {
                        chbx.Checked = true;
                    }
                }
            }

            // Pone los principios RSE que tiene asociado el indicador
            DataTable tblPrincipioRSE = dsIndicador.Tables["PrincipiosRSE"];
            foreach (DataRow row in tblPrincipioRSE.Rows)
            {
                foreach (CheckBox chbx in phPrincipiosRSE.Controls)
                {
                    if (chbx.ID == "chkPrincipioRSE_" + row["idPrincipioRSE"].ToString())
                    {
                        chbx.Checked = true;
                    }
                }
            }

            if (dsIndicador.Tables["Inciso"].Rows.Count > 0)
            {
                UpdatePanel3.Visible = true;
                grdIncisos.DataSource = dsIndicador.Tables["Inciso"];
                grdIncisos.DataBind();
            }

            if (dsIndicador.Tables["PreguntaAdicional"].Rows.Count > 0)
            {
                UpdatePanel4.Visible = true;
                grdPAs.DataSource = dsIndicador.Tables["PreguntaAdicional"];
                grdPAs.DataBind();
            }

        }
        catch (Exception ex)
        {
            using (StreamWriter sw = File.AppendText(ConfigurationManager.AppSettings["logFile"].ToString()))
            {
                sw.AutoFlush = true;
                sw.WriteLine("Fecha: " + DateTime.Now.ToString());
                sw.WriteLine("Error en indicador.MuestraDatosDeIndicador(" + Session["idEmpresa"].ToString() + " ): " + ex.Message);
                sw.Close();
            }
            throw new Exception("Error en indicador.MuestraDatosDeIndicador(" + Session["idEmpresa"].ToString() + " ): " + ex.Message);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Tema temas = new Tema();
            dsTemas = temas.CargaTemas();
            ddlTemas.DataSource = dsTemas.Tables["Tema"].DefaultView;
            ddlTemas.DataTextField = "descripcion";
            ddlTemas.DataValueField = "idTema";
            ddlTemas.DataBind();

            ListItem item = new ListItem("Seleccione tema...", "0");
            ddlTemas.Items.Insert(0, item);

            Catalogo afinidades = new Catalogo();
            DataSet dsAfinidades = afinidades.Carga("AFINIDAD");
            //poner los checkboxes de las afinidades
            foreach (DataRow rowAfinidad in dsAfinidades.Tables["Afinidad"].Rows)
            {
                CheckBox chkAfinidad = new CheckBox();
                chkAfinidad.ID = "chkAfinidad_" + rowAfinidad["idAfinidad"].ToString();
                chkAfinidad.Text = rowAfinidad["descripcion"].ToString() + "<br/>";
                phAfinidades.Controls.Add(chkAfinidad);
            }

            Catalogo principiosRSE = new Catalogo();
            DataSet dsPrincipiosRSE = principiosRSE.Carga("PRINCIPIOSRSE");
            //poner los checkboxes de las afinidades
            foreach (DataRow rowPrincipioRSE in dsPrincipiosRSE.Tables["PrincipiosRSE"].Rows)
            {
                CheckBox chkPrincipioRSE = new CheckBox();
                chkPrincipioRSE.ID = "chkPrincipioRSE_" + rowPrincipioRSE["idPrincipioRSE"].ToString();
                chkPrincipioRSE.Text = rowPrincipioRSE["descripcion"].ToString() + "<br/>";
                phPrincipiosRSE.Controls.Add(chkPrincipioRSE);
            }

            if (Request.QueryString["idIndicador"] != null)
            {
                MuestraDatosDeIndicador();
            }

            // Muestra el chkLiberado en caso de que cuente con los permisos de administrador

            if (Session["perfil"].ToString().IndexOf('2') > -1)
            {
                chkLiberado.Visible = true;
            }
        }
        catch (Exception ex)
        {

            using (StreamWriter sw = File.AppendText(ConfigurationManager.AppSettings["logFile"].ToString()))
            {
                sw.AutoFlush = true;
                sw.WriteLine("Fecha: " + DateTime.Now.ToString());
                sw.WriteLine("Empresa: " + Session["idEmpresa"].ToString());
                sw.WriteLine("Error en indicador.Page_Load(): " + ex.Message);
                sw.WriteLine("InnerException: " + ex.InnerException);
                sw.Close();
            }
            Response.Redirect("error.aspx?message=" + ex.Message);

        }
    }
    protected void ddlTemas_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Esta validación sirve para mantener el valor seleccionado de los subtemas

        if (lblTema.Text != ddlTemas.SelectedValue)
        {
            lblTema.Text = ddlTemas.SelectedValue;
            DataView dv = new DataView(dsTemas.Tables["Subtema"], "idTema = " + ddlTemas.SelectedValue, "idSubtema", DataViewRowState.OriginalRows);

            ddlSubtemas.DataSource = dv;
            ddlSubtemas.DataTextField = "descripcion";
            ddlSubtemas.DataValueField = "idSubtema";
            ddlSubtemas.DataBind();

            ListItem item = new ListItem("Seleccione subtema...", "0");
            ddlSubtemas.Items.Insert(0, item);

            Indicador indica = new Indicador();
            txtIdIndicador.Text = indica.CargarSiguienteId(Convert.ToInt32(ddlTemas.SelectedValue)).ToString();

            btnAnadirIncisos.Enabled = true;
            btnPreguntaAdicional.Enabled = true;
        }

    }

    private void LimpiarControles()
    {
        ddlTemas.ClearSelection();
        ddlSubtemas.Items.Clear();

        foreach (CheckBox chkAfinidad in phAfinidades.Controls)
        {
            if (chkAfinidad.Checked)
            {
                chkAfinidad.Checked = false;
            }
        }

        foreach (CheckBox chkPrincipioRSE in phPrincipiosRSE.Controls)
        {
            if (chkPrincipioRSE.Checked)
            {
                chkPrincipioRSE.Checked = false;
            }
        }

        txtIdIndicador.Text = "";
        txtDescripcion.Text = "";
        txtDescripcion_alterna.Text = "";
        txtDescripcion_portugues.Text = "";
        rbtEvidenciaSi.Checked = true;
        txtNotas.Text = "";
        rbtObligatorioSi.Checked = true;
        //btnAnadirIncisos.Enabled = false;
        //btnPreguntaAdicional.Enabled = false;
        txtCorto.Text = "";
        lblEditar.Text = "";
        
        txtInciso.Text = "";
        UpdatePanel3.Visible = false;
        UpdatePanel4.Visible = false;
        txtPreguntaAdicional.Text = "";
        lblIdPregunta.Text = "";
        
        //Checar si solo ocultando el panel es suficiente
        txtPreguntaAdicional.Visible = false;
        lblObligatorioAdicional.Visible = false;
        rbtObligatorioAdicionalSi.Visible = false;
        rbtObligatorioAdicionalNo.Visible = false;
        rbtObligatorioAdicionalNo.Checked = true;
        
        txtRecomendacion.Text = "";
        txtEvidencias.Text = "";
    }

    private void GuardarPAs()
    {
        PreguntaAdicional pa = new PreguntaAdicional();
        if (lblIdPregunta.Text != "")
            pa.idPregunta = Convert.ToInt32(lblIdPregunta.Text);
        else
            if (grdPAs.Rows.Count > 0)
                pa.idPregunta = Convert.ToInt32(grdPAs.DataKeys[grdPAs.Rows.Count - 1].Values[0]) + 1;
            else
                pa.idPregunta = 1;

        pa.idIndicador = Convert.ToInt32(txtIdIndicador.Text);
        pa.idTema = Convert.ToInt32(ddlTemas.SelectedValue);
        pa.pregunta = txtPreguntaAdicional.Text;
        pa.obligatorio = rbtObligatorioAdicionalSi.Checked;
        pa.Guardar();
        lblIdPregunta.Text = "";
        txtPreguntaAdicional.Text = "";
    }

    private void GuardarInciso()
    {
        Inciso inciso = new Inciso();
        if (lblEditar.Text != "")
            inciso.idInciso = lblEditar.Text;
        else
            inciso.idInciso = ObtieneSiguienteIdInciso();

        inciso.idIndicador = Convert.ToInt32(txtIdIndicador.Text);
        inciso.idTema = Convert.ToInt32(ddlTemas.SelectedValue);
        inciso.descripcion = txtInciso.Text;
        inciso.GuardaInciso();
        lblEditar.Text = "";
        txtInciso.Text = "";
    }

    private void GuardarIndicador()
    {
        
        try
        {
            Indicador indica = new Indicador();
            indica.idTema = Convert.ToInt32(ddlTemas.SelectedValue);
            indica.idSubtema = Convert.ToInt32(ddlSubtemas.SelectedValue);
            indica.idIndicador = Convert.ToInt32(txtIdIndicador.Text);
            indica.descripcion = txtDescripcion.Text;
            indica.descripcionAlterna = txtDescripcion_alterna.Text;
            indica.descripcionPortugues = txtDescripcion_portugues.Text;
            indica.valor = 3;
            indica.evidencia = rbtEvidenciaSi.Checked;
            indica.obligatorio = rbtObligatorioSi.Checked;
            indica.notas = txtNotas.Text;
            indica.corto = txtCorto.Text;
            indica.idUsuario = Session["idUsuario"].ToString();
            indica.recomendacion = txtRecomendacion.Text;
            indica.evidencias = txtEvidencias.Text;
            indica.liberado = chkLiberado.Checked;

            if (indica.Guardar())
            {
                // Guarda las afinidades del indicador
                foreach (CheckBox chkAfinidad in phAfinidades.Controls)
                {
                    if (chkAfinidad.Checked)
                    {
                        string id = chkAfinidad.ID.Substring(chkAfinidad.ID.IndexOf('_')+1);
                        indica.GuardaAfinidad(Convert.ToInt32(id));
                    }
                }

                // Guarda los principios RSE asociados con el indicador
                foreach (CheckBox chkPrincipioRSE in phPrincipiosRSE.Controls)
                {
                    if (chkPrincipioRSE.Checked)
                    {
                        string id = chkPrincipioRSE.ID.Substring(chkPrincipioRSE.ID.IndexOf('_')+1);
                        indica.GuardaPrincipioRSE(Convert.ToInt32(id));
                    }
                }
            }
            else
            {
                throw new Exception("Error al guardar el indicador");
            }
        }
        catch (Exception ex)
        {
           
            using (StreamWriter sw = File.AppendText(ConfigurationManager.AppSettings["logFile"].ToString()))
            {
                sw.AutoFlush = true;
                sw.WriteLine("Fecha: " + DateTime.Now.ToString());
                sw.WriteLine("Empresa: " + Session["idEmpresa"].ToString());
                sw.WriteLine("Error en indicador.GuardarIndicador(): " + ex.Message);
                sw.WriteLine("InnerException: " + ex.InnerException);
                sw.Close();
            }
       
            Response.Redirect("error.aspx?message=" + ex.Message);
        }
    }
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        if (txtDescripcion.Text != "")
        {
            GuardarIndicador();
            if (txtInciso.Text != "")
            {
                GuardarInciso();
            }
            if (txtPreguntaAdicional.Text != "")
            {
                GuardarPAs();
            }

            LimpiarControles();
            if (Request.QueryString["idIndicador"] != null)
            {
                Response.Redirect("administradorDeIndicadores.aspx?Content=modificarIndicador");
            }
        }
    }

    private string ObtieneSiguienteIdInciso()
    {
        string[] inciso = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
        int indice = 0;
        Inciso rsp = new Inciso();
        DataSet dsInciso = rsp.CargaSiguienteInciso(Convert.ToInt32(ddlTemas.SelectedValue), Convert.ToInt32(txtIdIndicador.Text));
        
        DataTable tblInciso = dsInciso.Tables[0];


        if (tblInciso.Rows.Count > 0)
        {
            foreach (DataRow row in tblInciso.Rows)
            {
                if (inciso[indice] != row["idInciso"].ToString())
                {
                    break;
                }
                indice++;
            }
        }
        else
        {
            return inciso[0];
        }
        return inciso[indice];
    }

    protected void btnGuardarYAgregar_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtInciso.Text != "")
            {
                GuardarInciso();
                CargaIncisos();
            }

        }
        catch (Exception ex)
        {
           
            using (StreamWriter sw = File.AppendText(ConfigurationManager.AppSettings["logFile"].ToString()))
            {
                sw.AutoFlush = true;
                sw.WriteLine("Fecha: " + DateTime.Now.ToString());
                sw.WriteLine("Empresa: " + Session["idEmpresa"].ToString());
                sw.WriteLine("Error en indicador.btnGuardarYAgregar_Click(): " + ex.Message);
                sw.WriteLine("InnerException: " + ex.InnerException);
                sw.Close();
            }
        
            Response.Redirect("error.aspx?message=" + ex.Message);
        }
    }

    protected void btnGuardarYAgregarPA_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtPreguntaAdicional.Text != "")
            {
                GuardarPAs();
                CargaPAs();
            }

        }
        catch (Exception ex)
        {
            
            using (StreamWriter sw = File.AppendText(ConfigurationManager.AppSettings["logFile"].ToString()))
            {
                sw.AutoFlush = true;
                sw.WriteLine("Fecha: " + DateTime.Now.ToString());
                sw.WriteLine("Empresa: " + Session["idEmpresa"].ToString());
                sw.WriteLine("Error en indicador.btnGuardarYAgregarPA_Clic(): " + ex.Message);
                sw.WriteLine("InnerException: " + ex.InnerException);
                sw.Close();
            }
        
            Response.Redirect("error.aspx?message=" + ex.Message);
        }
    }

    protected void btnAnadirIncisos_Click(object sender, EventArgs e)
    {
        if (txtDescripcion.Text != "")
        {
            GuardarIndicador();
            txtInciso.Visible = true;
            btnGuardarYAgregar.Visible = true;
        }
    }
    protected void btnPreguntaAdicional_Click(object sender, EventArgs e)
    {
        if (txtDescripcion.Text != "")
        {
            GuardarIndicador();
            UpdatePanel4.Visible = true;
            btnGuardarYAgregarPA.Visible = true;
            txtPreguntaAdicional.Visible = true;
            lblObligatorioAdicional.Visible = true;
            rbtObligatorioAdicionalSi.Visible = true;
            rbtObligatorioAdicionalNo.Visible = true;
        }
    }

    private void CargaIncisos()
    {
        Inciso inciso = new Inciso();
        inciso.idTema = Convert.ToInt32(ddlTemas.SelectedValue);
        inciso.idIndicador = Convert.ToInt32(txtIdIndicador.Text);
        DataSet incisos = inciso.CargaIncisos();

        DataView dv = new DataView(incisos.Tables["Inciso"]);

        grdIncisos.DataSource = dv;
        grdIncisos.DataBind();
    }

    private void CargaPAs()
    {
        PreguntaAdicional pa= new PreguntaAdicional();
        pa.idTema = Convert.ToInt32(ddlTemas.SelectedValue);
        pa.idIndicador = Convert.ToInt32(txtIdIndicador.Text);
        DataSet dsPAs = pa.CargaPAs();

        DataView dv = new DataView(dsPAs.Tables["PreguntaAdicional"]);

        grdPAs.DataSource = dv;
        grdPAs.DataBind();
    }
    protected void grdIncisos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToUpper() == "SELECT")
        {
            txtInciso.Visible = true;
            btnGuardarYAgregar.Visible = true;
            lblEditar.Text = grdIncisos.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString();
            txtInciso.Text = grdIncisos.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[1].ToString();
        }
        else if (e.CommandName.ToUpper() == "DELETE")
        {
            Inciso inciso = new Inciso();
            inciso.idTema = Convert.ToInt32(ddlTemas.SelectedValue);
            inciso.idIndicador = Convert.ToInt32(txtIdIndicador.Text);
            inciso.idInciso = grdIncisos.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString();
            inciso.Eliminar();
        }
    }
    protected void btnEliminarIndicador_Click(object sender, EventArgs e)
    {
        //Código para eliminar el indicador y sus incisos
        Indicador indicador = new Indicador();
        indicador.idTema = Convert.ToInt32(ddlTemas.SelectedValue);
        indicador.idIndicador = Convert.ToInt32(txtIdIndicador.Text);
        indicador.Eliminar();
        LimpiarControles();
        if (Request.QueryString["idIndicador"] != null)
        {
            Response.Redirect("administradorDeIndicadores.aspx?Content=modificarIndicador");
        }
    }

    protected void grdIncisos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        CargaIncisos();
    }

    protected void grdPAs_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToUpper() == "SELECT")
        {
            UpdatePanel4.Visible = true;
            btnGuardarYAgregarPA.Visible = true;
            txtPreguntaAdicional.Visible = true;
            lblObligatorioAdicional.Visible = true;
            rbtObligatorioAdicionalSi.Visible = true;
            rbtObligatorioAdicionalNo.Visible = true;
            lblIdPregunta.Text = grdPAs.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString();
            txtPreguntaAdicional.Text = grdPAs.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[1].ToString();
        }
        else if (e.CommandName.ToUpper() == "DELETE")
        {
            PreguntaAdicional pa = new PreguntaAdicional();
            pa.idTema = Convert.ToInt32(ddlTemas.SelectedValue);
            pa.idPregunta = Convert.ToInt32(grdPAs.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0]);
            pa.idIndicador = Convert.ToInt32(txtIdIndicador.Text);
            pa.Eliminar();
        }
    }
    protected void grdPAs_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        CargaPAs();
    }
    
}
