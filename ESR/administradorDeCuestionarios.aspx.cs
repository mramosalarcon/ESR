using System;
using System.Data;
using System.Web.UI.WebControls;
using ESR.Business;
using System.IO;
using System.Configuration;
using Microsoft.SharePoint;

public partial class administradorDeCuestionarios : System.Web.UI.Page
{


    protected int GetIdEmpresa()
    {
        if (Request.Params["idEmpresa"] == null)
            return Convert.ToInt32(Session["idEmpresa"].ToString());
        else
            return Convert.ToInt32(Request.Params["idEmpresa"].ToString());
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {   
                Cuestionario cuestionariosEmpresa = new Cuestionario();
                cuestionariosEmpresa.idEmpresa = this.GetIdEmpresa();
                DataSet dscca = cuestionariosEmpresa.CargaCuestionariosAdmin();

                ddlCuestionarios.DataSource = dscca;
                ddlCuestionarios.DataTextField = "nombre";
                ddlCuestionarios.DataValueField = "idCuestionario";
                ddlCuestionarios.DataBind();

                ListItem item1 = new ListItem("Nuevo cuestionario...", "0");
                ddlCuestionarios.Items.Insert(0, item1);
                ddlCuestionarios.SelectedIndex = 0;

                DataSet dsTemas;
                Tema temas = new Tema();
                dsTemas = temas.CargaTemas();
                ddlTemas.DataSource = dsTemas.Tables["Tema"].DefaultView;
                ddlTemas.DataTextField = "descripcion";
                ddlTemas.DataValueField = "idTema";
                ddlTemas.DataBind();

                ListItem item = new ListItem("Seleccione tema...", "0");
                ddlTemas.Items.Insert(0, item);
                ddlTemas.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                using (StreamWriter sw = File.AppendText(ConfigurationManager.AppSettings["logFile"].ToString()))
                {
                    sw.AutoFlush = true;
                    sw.WriteLine("Fecha: " + DateTime.Now.ToString());
                    sw.WriteLine("Empresa: " +Session["idEmpresa"].ToString());
                    sw.WriteLine("Error en administradorDeCuestionarios.Page_Load(): " + ex.Message);
                    sw.WriteLine("InnerException: " + ex.InnerException);
                    sw.Close();
                }
            }

            //GridView gv = new GridView();
            //gv.DataSource = category.Products;
            //gv.DataBind();
            //AjaxControlToolkit.TabPanel tab = new AjaxControlToolkit.TabPanel();
            //tab.Controls.Add(gv);
            //tab.HeaderText = category.CategoryName;
            //TabContainer1.Tabs.Add(tab); 
        }
    }



    protected void btnAgregarCuestionario_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnAgregarCuestionario.Text == "Agregar Cuestionario")
            {
                Cuestionario guardacuestionario = new Cuestionario();
                guardacuestionario.nombre = txtNombreCuestionario.Text;
                guardacuestionario.descripcion = txtDescripcionCuestionario.Text;
                guardacuestionario.anio = Convert.ToInt16(txtAnioCuestionario.Text);
                guardacuestionario.bloqueado = Convert.ToBoolean(Convert.ToInt16(rblSiNo.SelectedItem.Value));
                guardacuestionario.idUsuario = Session["idUsuario"].ToString();
                guardacuestionario.fecha = DateTime.Now;
                guardacuestionario.idUsuario = "";
                //Ojo
                guardacuestionario.fecha= DateTime.Now;
                guardacuestionario.GuardaCuestionario();


                txtNombreCuestionario.Enabled = false;
                txtDescripcionCuestionario.Enabled = false;
                txtAnioCuestionario.Enabled = false;
                rblSiNo.Enabled = false;
                btnAgregarCuestionario.Enabled = false;

                lblCuestionario.Visible = true;
                lblTema.Visible = true;
                ddlTemas.Visible = true;
                //btnSelTodos.Visible = true;
                //btnSelNinguno.Visible = true;
                grdIndicadores.Visible = true;

                Cuestionario traeidcuestionario = new Cuestionario();
                traeidcuestionario.nombre = txtNombreCuestionario.Text;
                DataSet idenCuestionario = traeidcuestionario.Traeidcuestionario();
                ViewState["idcuestionario"] = idenCuestionario.Tables["IdCuestionario"].Rows[0].ItemArray[0];
            }
            else
            {
                Cuestionario guardacuestionario = new Cuestionario();
                guardacuestionario.idCuestionario = Convert.ToInt16(ViewState["idcuestionario"].ToString());
                guardacuestionario.nombre = txtNombreCuestionario.Text;
                guardacuestionario.descripcion = txtDescripcionCuestionario.Text;
                guardacuestionario.anio = Convert.ToInt16(txtAnioCuestionario.Text);
                guardacuestionario.bloqueado = Convert.ToBoolean(Convert.ToInt16(rblSiNo.SelectedItem.Value));
                guardacuestionario.idUsuario = "";
                guardacuestionario.fecha = DateTime.Now;
                guardacuestionario.idUsuario= Session["idUsuario"].ToString();
                guardacuestionario.fecha= DateTime.Now;
                guardacuestionario.GuardaCuestionario();
            }
        }
        catch (Exception ex)
        {
            using (StreamWriter sw = File.AppendText(ConfigurationManager.AppSettings["logFile"].ToString()))
            {
                sw.AutoFlush = true;
                sw.WriteLine("Fecha: " + DateTime.Now.ToString());
                sw.WriteLine("Empresa: " + Session["idEmpresa"].ToString());
                sw.WriteLine("Error en administradorDeCuestionario.btnAgregarCuestionario_Click(): " + ex.Message);
                sw.WriteLine("InnerException: " + ex.InnerException);
                sw.Close();
            }
        }

    }
    protected void ddlTemas_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            grdIndicadores.DataSource = null;
            grdIndicadores.DataBind();

            Cuestionario cuestionarios = new Cuestionario();
            cuestionarios.idCuestionario = Convert.ToInt32(ViewState["idcuestionario"].ToString());
            cuestionarios.idTema = Convert.ToInt32(ddlTemas.SelectedValue);
            DataSet dsCuestionarios = cuestionarios.TraeCuestionario();
            DataView dv = new DataView(dsCuestionarios.Tables["Indicador"]);
            grdIndicadores.DataSource = dv;
            grdIndicadores.DataBind();

            //Cuestionario cuestionarioTema = new Cuestionario();
            //cuestionarioTema.idCuestionario = Convert.ToInt32(ViewState["idcuestionario"].ToString());
            //cuestionarioTema.idTema = Convert.ToInt32(ddlTemas.SelectedValue);
            DataSet dsIndicadorCuestionario = cuestionarios.LlenaGrid();
            if (dsIndicadorCuestionario.Tables["LLenaGrid"].Rows.Count > 0)
            {
                int i = 0;
                int j = 0;
                int cont = 0;
                int numregds = dsIndicadorCuestionario.Tables["LLenaGrid"].Rows.Count;

                foreach (GridViewRow row in grdIndicadores.Rows)
                {
                    // Access the CheckBox
                    if (numregds != cont)
                    {
                        int indicadorguardado = Convert.ToInt32(dsIndicadorCuestionario.Tables["LLenaGrid"].Rows[i].ItemArray[1].ToString());
                        int indicadortabla = Convert.ToInt32(grdIndicadores.Rows[j].Cells[1].Text.ToString());
                        CheckBox cb = (CheckBox)row.FindControl("chbSelecciona");
                        DropDownList ddlOrdinal = (DropDownList)row.FindControl("ddlOrdinal");

                        if (indicadorguardado == indicadortabla)
                        {
                            cb.Checked = true;
                            i++;
                            j++;
                            cont++;
                        }
                        else
                        {
                            cb.Checked = false;
                            //i++;
                            j++;
                        }
                        ddlOrdinal.Enabled = cb.Checked;
                    }
                }
            }

            //if (Convert.ToInt32(ddlTemas.SelectedValue) == 0)
            //{
            //    btnSelTodos.Visible = false;
            //    btnSelNinguno.Visible = false;
            //}
            //else
            //{
            //    btnSelTodos.Visible = true;
            //    btnSelNinguno.Visible = true;
            //}


            //Cuestionario LlenaIndicador = new Cuestionario();
            //LlenaIndicador.idCuestionario = 

        }
        catch (Exception ex)
        {
            using (StreamWriter sw = File.AppendText(ConfigurationManager.AppSettings["logFile"].ToString()))
            {
                sw.AutoFlush = true;
                sw.WriteLine("Fecha: " + DateTime.Now.ToString());
                sw.WriteLine("Empresa: " + Session["idEmpresa"].ToString());
                sw.WriteLine("Error en administradorDeCuestionarios.ddlTemas_SelectedIndexChanged(): " + ex.Message);
                sw.WriteLine("InnerException: " + ex.InnerException);
                sw.Close();
            }
        }

    }

    private void ToggleCheckState(bool checkState)
    {
        // Iterate through the Products.Rows property
        foreach (GridViewRow row in grdIndicadores.Rows)
        {
            // Access the CheckBox
            CheckBox cb = (CheckBox)row.FindControl("chbSelecciona");
            if (cb != null)
                cb.Checked = checkState;
        }
    }

    //protected void btnSelTodos_Click(object sender, EventArgs e)
    //{
    //    ToggleCheckState(true);
    //    GuardaIndicadores();

    //}
    //protected void btnSelNinguno_Click(object sender, EventArgs e)
    //{
    //    ToggleCheckState(false);
    //    GuardaIndicadores();

    //}


    protected void grdIndicadores_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void chbSelecciona_CheckedChanged(object sender, EventArgs e)
    {
        //int i = 0;

        Cuestionario insact = new Cuestionario();
        insact.idCuestionario = Convert.ToInt32(ViewState["idcuestionario"].ToString());
        insact.idTema = Convert.ToInt32(ddlTemas.SelectedValue);
        insact.idUsuario = Session["idUsuario"].ToString();

        CheckBox chkIndicador = (CheckBox)sender;
        GridViewRow gvrIndicador = (GridViewRow)chkIndicador.Parent.Parent;
        insact.idIndicador = Convert.ToInt32(gvrIndicador.Cells[1].Text);
        DropDownList ddlOrdinal = (DropDownList)gvrIndicador.Cells[3].FindControl("ddlOrdinal");
        insact.ordinal = insact.idIndicador;// Convert.ToInt32(ddlOrdinal.SelectedValue);

        if (chkIndicador.Checked)
        {
            insact.GuardaIndicadoresCuestionario();
            ddlOrdinal.Enabled = true;
        }
        else
        {
            insact.EliminaIndicadoresCuestionario();
            ddlOrdinal.Enabled = false;
        }

        this.ddlTemas_SelectedIndexChanged(sender, e);

        //foreach (GridViewRow row in grdIndicadores.Rows)
        //{
        //    // Access the CheckBox
        //    CheckBox cb = (CheckBox)row.FindControl("chbSelecciona");
        //    if (cb != null && cb.Checked)
        //    {
        //        //Inserta los indicadores en la tabla cuestionario_indicador
        //        insact.idindicador = i;
        //        insact.GuardaIndicadoresCuestionario();
        //    }
        //    else
        //    {
        //        //Elimina el registro de la tabla custionario_indicador
        //        insact.idindicador = i;
        //        insact.EliminaIndicadoresCuestionario();
        //    }
        //    i++;
        //}
    }

    protected void ddlCuestionarios_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt16(ddlCuestionarios.SelectedValue) == 0)
            {
                txtNombreCuestionario.Text = "";
                txtDescripcionCuestionario.Text = "";
                txtAnioCuestionario.Text = "";
                //rblSiNo.SelectedItem.Text = "No";
                rblSiNo.SelectedValue = "0";
                btnAgregarCuestionario.Text = "Agregar Cuestionario";

                lblCuestionario.Visible = false;
                lblTema.Visible = false;
                ddlTemas.Visible = false;
                btnSelTodos.Visible = false;
                btnSelNinguno.Visible = false;
                //grdIndicadores.Visible = false;
                grdIndicadores.DataSource = null;
                grdIndicadores.DataBind();
                ddlTemas.SelectedIndex = 0;
            }
            else
            {
                grdIndicadores.DataSource = null;
                grdIndicadores.DataBind();
                ddlTemas.SelectedIndex = 0;
                btnSelTodos.Visible = false;
                btnSelNinguno.Visible = false;
                Cuestionario datoscuestionario = new Cuestionario();
                datoscuestionario.idCuestionario = Convert.ToInt16(ddlCuestionarios.SelectedValue);
                DataSet datcuest = datoscuestionario.DatosCuestionario();
                txtNombreCuestionario.Text = datcuest.Tables["DatosCuestionario"].Rows[0].ItemArray[0].ToString();
                txtDescripcionCuestionario.Text = datcuest.Tables["DatosCuestionario"].Rows[0].ItemArray[1].ToString();
                txtAnioCuestionario.Text = datcuest.Tables["DatosCuestionario"].Rows[0].ItemArray[2].ToString();
                bool vf = Convert.ToBoolean(datcuest.Tables["DatosCuestionario"].Rows[0].ItemArray[3]);
                //rblSiNo.SelectedValue = datcuest.Tables["DatosCuestionario"].Rows[0].ItemArray[3].ToString();
                if (vf)
                {
                    //rblSiNo.SelectedItem.Text = "Si";
                    rblSiNo.SelectedValue = "1";
                }
                else
                {
                    //rblSiNo.SelectedItem.Text = "No";
                    rblSiNo.SelectedValue = "0";
                }
                btnAgregarCuestionario.Text = "Modificar Datos Cuestionario";
                //btnAgregarCuestionario.Visible = false;

                lblCuestionario.Visible = true;
                lblTema.Visible = true;
                ddlTemas.Visible = true;
                //btnSelTodos.Visible = true;
                //btnSelNinguno.Visible = true;
                grdIndicadores.Visible = true;

                Cuestionario traeidcuestionario = new Cuestionario();
                traeidcuestionario.nombre = txtNombreCuestionario.Text;
                DataSet idenCuestionario = traeidcuestionario.Traeidcuestionario();
                ViewState["idcuestionario"] = idenCuestionario.Tables["IdCuestionario"].Rows[0].ItemArray[0];
            }
        }
        catch (Exception ex)
        {
            using (StreamWriter sw = File.AppendText(ConfigurationManager.AppSettings["logFile"].ToString()))
            {
                sw.AutoFlush = true;
                sw.WriteLine("Fecha: " + DateTime.Now.ToString());
                sw.WriteLine("Empresa: " + Session["idEmpresa"].ToString());
                sw.WriteLine("Error en administradorDeCuestionarios.ddlCuestionarios_SelectedIndexChanged(): " + ex.Message);
                sw.WriteLine("InnerException: " + ex.InnerException);
                sw.Close();
            }
        }
    }

    protected void ddlOrdinal_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlOrdinalSelected = (DropDownList)sender;
        GridViewRow gvr = (GridViewRow)ddlOrdinalSelected.Parent.Parent;
		
        Tema tema = new Tema();
        tema.idCuestionario = Convert.ToInt32(ddlCuestionarios.SelectedValue);
        tema.idTema = Convert.ToInt32(ddlTemas.SelectedValue);
        tema.ActualizaOrden(Convert.ToInt32(gvr.Cells[1].Text), Convert.ToInt32(ddlOrdinalSelected.SelectedValue));
        ddlTemas_SelectedIndexChanged(ddlTemas, e);
    }

    protected DataSet GetOrdinales()
    {
        Tema tema = new Tema();
        tema.idCuestionario = Convert.ToInt32(ddlCuestionarios.SelectedValue);
        tema.idTema = Convert.ToInt32(ddlTemas.SelectedValue);

        DataSet dsOrdinales = tema.CargaOrdinales();

        if (dsOrdinales.Tables["Ordinales"].Rows.Count == 0)
            dsOrdinales = tema.CargaOrdinales(Convert.ToInt32(ddlTemas.SelectedValue));

        return dsOrdinales;

    }

    protected void grdIndicadores_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataRowView drv = (DataRowView)e.Row.DataItem;
        if (drv != null)
            ((DropDownList)e.Row.FindControl("ddlOrdinal")).SelectedValue = drv["Ordinal"].ToString();
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
