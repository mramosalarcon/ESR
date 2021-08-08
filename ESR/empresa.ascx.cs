using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;
using ESR.Business;

public partial class empresa : System.Web.UI.UserControl
{
    private string GetProfiles()
    {
        return Session["perfil"].ToString();
    }

    private int GetEmpresa()
    {
        if (Request.QueryString["idEmpresa"] == null)
            return Convert.ToInt32(Session["idEmpresa"]);
        else
            return Convert.ToInt32(Request.QueryString["idEmpresa"]);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.infadicional.Visible = false;

                // Carga los sectores del catálogo
                Sector sectores = new Sector();
                DataSet dsSectores = sectores.CargaSectores();

                ddlSector.DataSource = dsSectores.Tables["Sector"].DefaultView;
                ddlSector.DataTextField = "nombre";
                ddlSector.DataValueField = "idSector";
                ddlSector.DataBind();

                ListItem item = new ListItem("Seleccione sector...", "0");
                ddlSector.Items.Insert(0, item);

                // Carga el catálogo de las empresas postulantes
                Postulante postulante = new Postulante();
                DataSet dsPostulantes = postulante.CargaPostulantes();

                ddlPostulante.DataSource = dsPostulantes.Tables["Postulante"].DefaultView;
                ddlPostulante.DataTextField = "nombre";
                ddlPostulante.DataValueField = "nombre";
                ddlPostulante.DataBind();

                ListItem itemN = new ListItem("Ninguno", "Ninguno");
                ddlPostulante.Items.Insert(0, itemN);

                // Carga el catálogo de países
                Catalogo paises = new Catalogo();
                DataSet dsPaises = paises.Carga("PAIS");
                ddlPais.DataSource = dsPaises.Tables[0].DefaultView;
                ddlPais.DataTextField = "nombre";
                ddlPais.DataValueField = "idPais";
                ddlPais.DataBind();

                item = new ListItem("Seleccione pais...", "0");
                ddlPais.Items.Insert(0, item);

                // Carga el catálogo de estados
                Catalogo estados = new Catalogo();
                DataSet dsEstados = estados.Carga("ESTADO");
                ddlEstado.DataSource = dsEstados.Tables[0].DefaultView;
                ddlEstado.DataTextField = "nombre";
                ddlEstado.DataValueField = "idEstado";
                ddlEstado.DataBind();

                item = new ListItem("Seleccione estado...", "0");
                ddlEstado.Items.Insert(0, item);

                // Falta cargar el catálogo de empresas grandes para seleccionarlas como Cadena de Valor
                Empresa cadenaDeValor = new Empresa();
                DataSet dsCadenaDeValor = cadenaDeValor.CargaEmpresasGrandes();
                ddlEmpresaReferencia.DataSource = dsCadenaDeValor.Tables[0].DefaultView;
                ddlEmpresaReferencia.DataTextField = "nombre";
                ddlEmpresaReferencia.DataValueField = "idEmpresa";
                ddlEmpresaReferencia.DataBind();

                item = new ListItem("Ninguna", "0");
                ddlEmpresaReferencia.Items.Insert(0, item);

                // Obtiene el idEmpresa desde la variable de sesión si no viene en el querystring 
                int idEmpresa = this.GetEmpresa();
                if (idEmpresa != 0)
                {
                    // Encontró el idEmpresa
                    Empresa emp = new Empresa();
                    emp.idEmpresa = idEmpresa;
                    if (emp.Cargar())
                    {
                        txtRFC.Text = emp.RFC;
                        txtNombreEmpresa.Text = emp.nombre;
                        txtNombreCorto.Text = emp.nombreCorto;
                        txtRazonSocial.Text = emp.RazonSocial;
                        txtDomicilio.Text = emp.Domicilio;
                        txtColonia.Text = emp.Colonia;
                        txtCiudad.Text = emp.Ciudad;
                        txtCP.Text = emp.CP;
                        txtEstado.Text = emp.Estado;
                        ddlPais.SelectedValue = emp.Pais.ToString();
                        if (emp.Pais == 1)
                        {
                            try
                            {
                                ddlEstado.Items.FindByText(emp.Estado).Selected = true;
                            }
                            catch (Exception ex)
                            {
                                using (StreamWriter sw = File.AppendText(ConfigurationManager.AppSettings["logFile"].ToString()))
                                {
                                    sw.AutoFlush = true;
                                    sw.WriteLine("Fecha: " + DateTime.Now.ToString());
                                    //sw.WriteLine("Empresa: " + Session["idEmpresa"].ToString());
                                    sw.WriteLine("Error en empresa.Page_Load(), al buscar el estado por texto: " + ex.Message);
                                    sw.WriteLine("InnerException: " + ex.InnerException);
                                    sw.Close();
                                }
                            }
                            finally
                            {
                                ddlEstado.Visible = true;
                            }
                        }
                        else
                        {
                            txtEstado.Visible = true;
                        }

                        txtSiglas.Text = emp.Siglas;
                        txtTelefonos.Text = emp.Telefono;
                        txtFax.Text = emp.Fax;
                        txtEmail.Text = emp.Email;
                        txtWWW.Text = emp.WebSite;
                        imgLogo.ImageUrl = "~/tools/imgLogo.aspx?idEmpresa=" + idEmpresa.ToString();

                        txtDirector.Text = emp.Director;
                        txtPresidente.Text = emp.Presidente;
                        lblTamano.Text = emp.Tamano;

                        //txtSectorOtro.Text = emp.Sector;
                        try
                        {
                            ddlSector.Items.FindByText(emp.Sector).Selected = true;

                            Sector subSector = new Sector();
                            subSector.idSector = ddlSector.SelectedIndex;
                            DataSet dsSubsectores = subSector.CargaSubSectores();

                            ddlSubsector.DataSource = dsSubsectores.Tables["SubSector"].DefaultView;
                            ddlSubsector.DataTextField = "nombre";
                            ddlSubsector.DataValueField = "idSubsector";
                            ddlSubsector.DataBind();

                            item = new ListItem("Seleccione Subsector...", "0");
                            ddlSubsector.Items.Insert(0, item);

                            ddlSubsector.Items.FindByText(emp.Subsector).Selected = true;
                        }
                        catch (Exception ex)
                        {
                            using (StreamWriter sw = File.AppendText(ConfigurationManager.AppSettings["logFile"].ToString()))
                            {
                                sw.AutoFlush = true;
                                sw.WriteLine("Fecha: " + DateTime.Now.ToString());
                                //sw.WriteLine("Empresa: " + Session["idEmpresa"].ToString());
                                sw.WriteLine("Error en empresa.Page_Load(), al buscar el sector por texto: " + ex.Message);
                                sw.WriteLine("InnerException: " + ex.InnerException);
                                sw.Close();
                            }
                        }
                        txtCapitalOtro.Text = emp.ComposicionK;
                        try
                        {
                            ddlCapital.Items.FindByText(emp.ComposicionK).Selected = true;
                        }
                        catch
                        {
                            ddlCapital.SelectedValue = "Otro";
                            txtCapitalOtro.Visible = true;
                        }

                        txtPostulanteOtro.Text = emp.postulante.ToString();
                        try
                        {
                            ddlPostulante.Items.FindByText(emp.postulante).Selected = true;
                        }
                        catch
                        {
                            ddlPostulante.SelectedValue = "Otro";
                            txtPostulanteOtro.Visible = true;
                        }
                        txtNoEmpleados.Text = emp.NoEmpleados.ToString();
                        txtNombreProducto.Text = emp.Producto;
                        txtAnoInicio.Text = emp.AnoInicio.ToString();

                        txtPrincipalesProductos.Text = emp.Productos;
                        txtRSE.Text = emp.RSE;
                        txtFundacion.Text = emp.Fundacion;
                        txtAmbito.Text = emp.Ambito;

                        try
                        {
                            ddlEmpresaReferencia.Items.FindByValue(emp.cadenaDeValor.ToString()).Selected = true;
                        }
                        catch (Exception ex)
                        {
                            using (StreamWriter sw = File.AppendText(ConfigurationManager.AppSettings["logFile"].ToString()))
                            {
                                sw.AutoFlush = true;
                                sw.WriteLine("Fecha: " + DateTime.Now.ToString());
                                //sw.WriteLine("Empresa: " + Session["idEmpresa"].ToString());
                                sw.WriteLine("Error en empresa.Page_Load(), al buscar la referencia por valor: " + ex.Message);
                                sw.WriteLine("InnerException: " + ex.InnerException);
                                sw.Close();
                            }
                        }

                        //Aqui recuperar la información de las certificaciones y ponerlas en el GRID

                        Certificacion certificaciones = new Certificacion();
                        certificaciones.idEmpresa = idEmpresa;

                        DataSet dsCertificaciones = certificaciones.CargaCertificaciones();
                        DataView dv = new DataView(dsCertificaciones.Tables["Certificacion"]);

                        //dsCertificaciones.Tables["Certificacion"].Rows

                        if (dsCertificaciones.Tables["Certificacion"].Rows.Count > 0)
                            ViewState.Add("idEmpresa", dsCertificaciones.Tables["Certificacion"].Rows[0].ItemArray[1].ToString());

                        grdCertificaciones.DataSource = dv;
                        grdCertificaciones.DataBind();

                        // Esto es para poner visible la administración de cuestionarios
                        string _sPerfiles = this.GetProfiles();
                        if (_sPerfiles != "")
                        {
                            Usuario users = new Usuario();
                            users.perfiles = _sPerfiles;
                            if (users.perfiles.IndexOf("0") > -1 || users.perfiles.IndexOf("1") > -1 ||
                                users.perfiles.IndexOf("2") > -1 || users.perfiles.IndexOf("9") > -1)
                            {
                                // Solo esos perfiles permiten asignación de cuestionarios
                                Cuestionario cuestionarios = new Cuestionario();
                                // Carga la lista de los cuestionarios disponibles y los pone en la lista de checkboxes
                                DataSet dsCuestionarios = cuestionarios.CargaTodos();
                                if (dsCuestionarios.Tables["Cuestionario"].Rows.Count > 0)
                                {
                                    cblCuestionarios.DataSource = dsCuestionarios;
                                    cblCuestionarios.DataTextField = "nombre";
                                    cblCuestionarios.DataValueField = "idCuestionario";
                                    cblCuestionarios.DataBind();

                                    // Carga la lista de cuestionarios asignados a la empresa para prender los checkboxes
                                    DataSet dsCuestionariosEmpresa = emp.CargaCuestionarios();
                                    if (dsCuestionariosEmpresa.Tables["Cuestionario"].Rows.Count > 0)
                                    {
                                        foreach (DataRow row in dsCuestionariosEmpresa.Tables["Cuestionario"].Rows)
                                        {
                                            for (int i = 0; i < cblCuestionarios.Items.Count; i++)
                                            {
                                                // MRA 13/07/2010
                                                // Asignación automática del prediagnóstico
                                                if (cblCuestionarios.Items[i].Value == row["idCuestionario"].ToString()) //|| cblCuestionarios.Items[i].Value == "16")
                                                {
                                                    cblCuestionarios.Items[i].Selected = true;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                    if (emp.autorizada)
                                    {
                                        rbtAutorizadaSi.Checked = true;
                                    }
                                    else
                                    {
                                        rbtAutorizadaNo.Checked = true;
                                        for (int i = 0; i < cblCuestionarios.Items.Count; i++)
                                        {
                                            cblCuestionarios.Items[i].Selected = false;
                                        }

                                    }
                                    lblAutorizada.Visible = true;
                                    upAutorizada.Visible = true;
                                }
                            }
                            else
                            {
                                lblAutorizada.Visible = false;
                                upAutorizada.Visible = false;
                            }
                        }

                        txtidEmpresa.Text = emp.idEmpresa.ToString();

                        // MRA: 06/07/2010 19:25
                        // Aqui ya no son necesarios los datos del contacto, solo hay que registrar como contacto al usuario que registra
                        // Para esto traen el usuario de contacto, para ponerlo en la forma.
                        //Contacto contact = new Contacto();
                        //contact.idEmpresa = emp.idEmpresa;
                        //contact.Carga(emp.contacto);
                        //txtContactoNombre.Text = contact.nombre;
                        //txtContactoApellidoP.Text = contact.ApellidoP;
                        //txtContactoApellidoM.Text = contact.ApellidoM;
                        //txtContactoPuesto.Text = contact.Puesto;
                        //txtContactoTelefono.Text = contact.Telefono;
                        //txtContactoExt.Text = contact.Extension;
                        //txtContactoEmail.Text = contact.Email;
                        //txtContactoEmail.Enabled = false;
                        lblNotaPie.Visible = false;
                        lblError.Text = "";

                        //Crear el sitio e Sharepoint
                        SharePoint sp = new SharePoint();
                        if (sp.CreateSite("http://esr.cemefi.org", emp.idEmpresa.ToString(), emp.nombre, emp.nombreCorto) != 0)
                        {
                            //Response.Redirect("default.aspx");
                        }

                    }
                    else
                    {
                        lblError.Text = "Existe un error en el registro de la empresa, verifique la información.";
                        using (StreamWriter sw = File.AppendText(ConfigurationManager.AppSettings["logFile"].ToString()))
                        {
                            sw.AutoFlush = true;
                            sw.WriteLine("Fecha: " + DateTime.Now.ToString() + ", " + lblError.Text);
                            //sw.WriteLine("Empresa: " + Session["idEmpresa"].ToString());
                            sw.Close();
                        }
                    }
                }
                else
                {
                    // Si no encuentra el idEmpresa nada mas esconde el logo y pone todo para cargar
                    imgLogo.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            //lblError.Text = "Error en empresa.Page_Load(" + Session["idEmpresa"].ToString() + " ): " + ex.Message;
            using (StreamWriter sw = File.AppendText(ConfigurationManager.AppSettings["logFile"].ToString()))
            {
                sw.AutoFlush = true;
                sw.WriteLine("Fecha: " + DateTime.Now.ToString() + ", " + lblError.Text);
                //sw.WriteLine("Error en empresa.Page_Load(" + Session["idEmpresa"].ToString() + "): " + ex.Message);
                sw.WriteLine("Error en empresa.Page_Load(): " + ex.Message);
                sw.Close();
            }
        }
    }
    protected void btnSiguiente0_Click(object sender, EventArgs e)
    {
        try
        {
            this.GuardarEmpresa();
            this.infgeneral.Visible = false;
            this.infadicional.Visible = true;
        }
        catch(Exception ex)
        {
            lblError.Text = "Error en empresa.btnSiguiente0_Click(" + Session["idEmpresa"].ToString() + "): " + ex.Message;
            using (StreamWriter sw = File.AppendText(ConfigurationManager.AppSettings["logFile"].ToString()))
            {
                sw.AutoFlush = true;
                sw.WriteLine("Fecha: " + DateTime.Now.ToString() + ", " + lblError.Text);
                sw.WriteLine("Error en empresa.btnSiguiente0_Click(" + Session["idEmpresa"].ToString() + "): " + ex.Message);
                sw.Close();
            }
        }
    }

    //protected void btnSiguiente1_Click1(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        this.GuardarEmpresa();
    //        this.infadicional.Visible = false;
    //    }
    //    catch (Exception ex)
    //    {
    //        //Mandar la pantalla de error
    //        throw new Exception("CEMEFI.CESR.UI.Empresa.btnSiguiente1_Click1(): " + ex.Message);
    //    }
    //}
    protected void btnAnterior1_Click(object sender, EventArgs e)
    {
        this.infadicional.Visible = false;
        this.infgeneral.Visible = true;
    }
    //protected void btnAnterior2_Click1(object sender, EventArgs e)
    //{
    //    this.infadicional.Visible = true;
    //}

    protected void btnFinalizar_Click(object sender, EventArgs e)
    {
        this.GuardarEmpresa();

        if (Session["perfil"].ToString().IndexOf('2') == -1 && Session["perfil"].ToString().IndexOf('9') == -1)
        {
            if (txtidEmpresa.Text != "")
            {
                Contacto contacto = new Contacto();
                contacto.idEmpresa = Convert.ToInt32(txtidEmpresa.Text);
                contacto.Email = Session["idUsuario"].ToString();
                // MRA 07/07/2010 00:36
                // Ejecutar un método que cargue los datos del contacto
                // ¿Para que?
                // ¿Que pasa cuando es el administrador? o el staff

                // MRA 07/07/2010 00:27
                // Si ya tiene perfil le deja el mismo idPerfil
                // En caso de que sea 6 lo sube a 7 y le deja los dos perfiles 6 y 7
                if (Session["perfil"] == null)
                    contacto.idPerfil = Convert.ToInt32(ConfigurationManager.AppSettings["idPerfil_Responsable_de_empresa"]); //Responsable de la empresa
                else
                {
                    contacto.idPerfil = Convert.ToInt32(Session["perfil"].ToString().Substring(0,1));
                    if (Session["perfil"].ToString().IndexOf('6') > -1)
                    {
                        contacto.idPerfil = Convert.ToInt32(ConfigurationManager.AppSettings["idPerfil_Responsable_de_empresa"]);
                    }
                }

                // MRA 07/07/2010 01:10
                // Falta guardar la relación con la empresa y los temas
                if (contacto.GuardaEmpresa())
                {
                    Response.Redirect("~/tools/error.aspx?message=" +
                            "El registro de la empresa se realizó correctamente, " +
                            "por favor vuelva a firmarse para actualizar los cambios.");
                }
            }
        }
        else
        {
            Response.Redirect("~/administradorDeEmpresas.aspx");
        }
    }

    protected void ddlPais_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlPais.SelectedValue) > 1)
        {
            ddlEstado.Visible = false;
            txtEstado.Visible = true;
        }
        else
        {
            ddlEstado.SelectedIndex = 0;
            ddlEstado.Visible = true;
            txtEstado.Visible = false;
        }
    }
    protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlPais.SelectedValue) > 1)
        {
            txtEstado.Text = "";
        }
        else
        {
            txtEstado.Text = ddlEstado.SelectedItem.ToString();
            if (txtEstado.Text == "Distrito Federal")
            {
                txtCiudad.Text = "México";
                txtCiudad.Enabled = false;
            }
            else
            {
                txtCiudad.Text = "";
                txtCiudad.Enabled = true;
            }
        }        
    }


    protected void ddlSector_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSector.SelectedIndex > 0)
        {
            Sector subSector = new Sector();
            subSector.idSector = ddlSector.SelectedIndex;
            DataSet dsSubsectores = subSector.CargaSubSectores();

            ddlSubsector.DataSource = dsSubsectores.Tables["SubSector"].DefaultView;
            ddlSubsector.DataTextField = "nombre";
            ddlSubsector.DataValueField = "idSubsector";
            ddlSubsector.DataBind();

            ListItem item = new ListItem("Seleccione Subsector...", "0");
            ddlSubsector.Items.Insert(0, item);

        }
        else
        {
            ddlSubsector.Items.Clear();
        }
    }
    protected void ddlCapital_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtCapitalOtro.Text = "";
        if (ddlCapital.SelectedItem.ToString().StartsWith("Otro"))
        {
            txtCapitalOtro.Visible = true;
        }
        else
        {
            txtCapitalOtro.Text = ddlCapital.SelectedItem.ToString();
            txtCapitalOtro.Visible = false;
        }
    }
    protected void ddlPostulante_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtPostulanteOtro.Text = "";
        if (ddlPostulante.SelectedItem.ToString().StartsWith("Otro"))
        {
            txtPostulanteOtro.Visible = true;
        }
        else
        {
            txtPostulanteOtro.Text = ddlPostulante.SelectedItem.ToString();
            txtPostulanteOtro.Visible = false;
        }
    }
    protected void btnMasCertificaciones_Click(object sender, EventArgs e)
    {
        if (txtRecoAno.Text != "" && txtRecoInstitucion.Text != "" && txtReco.Text != "")
        {
            Certificacion cert = new Certificacion();
            cert.anio = Convert.ToInt32(txtRecoAno.Text.ToUpper());
            cert.institucion = txtRecoInstitucion.Text.ToUpper();
            cert.certificacion = txtReco.Text.ToUpper();
            cert.idEmpresa = Convert.ToInt32(txtidEmpresa.Text.ToUpper());


            if (cert.Guardar())
            {
                DataSet dsCert = cert.CargaCertificaciones();
                grdCertificaciones.DataSource = dsCert;
                grdCertificaciones.DataBind();

                //lblCertificaciones.Text += "<tr><td>" + txtRecoAno.Text + "</td><td>" + txtRecoInstitucion.Text + "</td><td>" + txtReco.Text + "</td></tr>";
                txtRecoAno.Text = "";
                txtRecoInstitucion.Text = "";
                txtReco.Text = "";
            }
            else
            {
                lblCertificaciones.Text += "<tr><td>Error al salvar la certificacion.</td><td>";
            }
        }
    }
    
    private void GuardarEmpresa()
    {
        Empresa newEmpresa = new Empresa();
        // MRA 06/07/2010 20:19
        // Esto esta chingon, si el método ya se mandó llamar txtidEmpresa tiene el idEmpresa
        // Si es la primera vez viene vació e inserta un nuevo registro
        // Me fumé un churro
        if (txtidEmpresa.Text != "")
        {
            newEmpresa.idEmpresa = Convert.ToInt32(txtidEmpresa.Text);
        }
        newEmpresa.RFC = txtRFC.Text.Replace("-","").Replace(" ", "").Trim().ToUpper();
        newEmpresa.nombre = txtNombreEmpresa.Text.ToUpper();
        newEmpresa.nombreCorto = txtNombreCorto.Text.ToUpper();
        newEmpresa.RazonSocial = txtRazonSocial.Text.Trim().ToUpper();
        newEmpresa.Domicilio = txtDomicilio.Text.Trim().ToUpper();
        newEmpresa.Colonia = txtColonia.Text.Trim().ToUpper();
        newEmpresa.Ciudad = txtCiudad.Text.Trim().ToUpper();
        newEmpresa.Estado = txtEstado.Text.Trim();
        newEmpresa.CP = txtCP.Text.Trim();
        newEmpresa.Pais = Convert.ToInt32(ddlPais.SelectedValue);
        newEmpresa.Siglas = txtSiglas.Text.Trim().ToUpper();
        newEmpresa.Telefono = txtTelefonos.Text.Trim();
        newEmpresa.Fax = txtFax.Text.Trim();
        newEmpresa.Email = txtEmail.Text.Trim().ToLower();
        newEmpresa.WebSite = txtWWW.Text.Trim();

        int ilogoLen;
        if (fuLogo.HasFile)
        {
            ilogoLen = fuLogo.PostedFile.ContentLength;
            if (ilogoLen <= 10485760) //Valida que sea menor a 10 MB
            {
                byte[] arrLogo = new byte[ilogoLen];
                Stream streamLogo = fuLogo.FileContent;
                streamLogo.Read(arrLogo, 0, ilogoLen);
                newEmpresa.Logo = arrLogo;
            }
            else
            {
                throw new Exception("El archivo es mayor a 10 MB");
            }
        }
        newEmpresa.Director = txtDirector.Text.Trim().ToUpper();
        newEmpresa.Presidente = txtPresidente.Text.Trim().ToUpper();

        if (ddlSector.SelectedIndex > 0)
        {
            newEmpresa.Sector = ddlSector.Items[ddlSector.SelectedIndex].Text;
        }
        if (ddlSubsector.SelectedIndex > 0)
        {
            newEmpresa.Subsector = ddlSubsector.Items[ddlSubsector.SelectedIndex].Text;
        }
        if (txtNoEmpleados.Text != "")
        {
            newEmpresa.NoEmpleados = Convert.ToInt32(txtNoEmpleados.Text);
        }

        // Se va a calcular el tamaño de la empresa
        // Dependiendo del tamaño de la empresa se le asigna el cuestionario correspondiente
        if (newEmpresa.Sector != "" && txtNoEmpleados.Text != "")
        {
            int noEmpleados = Convert.ToInt32(txtNoEmpleados.Text);
            switch (newEmpresa.Sector)
            {
                case "Industria":
                    if (newEmpresa.Subsector == "Agrícola" || newEmpresa.Subsector == "Ganadería" || newEmpresa.Subsector == "Aprovechamiento forestal" || newEmpresa.Subsector == "Pesca" || newEmpresa.Subsector == "Caza")
                    {
                        if (noEmpleados >= 2 && noEmpleados <= 10)
                        {
                            newEmpresa.Tamano = "Microempresa";
                        }
                        else if (noEmpleados >= 11 && noEmpleados <= 25)
                        {
                            newEmpresa.Tamano = "Pequeña";
                        }
                        else if (noEmpleados >= 26 && noEmpleados <= 100)
                        {
                            newEmpresa.Tamano = "Mediana";
                        }
                        else if (noEmpleados >= 100)
                        {
                            newEmpresa.Tamano = "Grande";
                        }
                    }
                    else
                    {
                        if (noEmpleados >= 1 && noEmpleados <= 20)
                        {
                            newEmpresa.Tamano = "Microempresa";
                        }
                        else if (noEmpleados >= 1 && noEmpleados <= 50)
                        {
                            newEmpresa.Tamano = "Pequeña";
                        }
                        else if (noEmpleados >= 51 && noEmpleados <= 250)
                        {
                            newEmpresa.Tamano = "Mediana";
                        }
                        else if (noEmpleados >= 251)
                        {
                            newEmpresa.Tamano = "Grande";
                        }
                    }
                    break;
                case "Comercio":
                    if (noEmpleados >= 1 && noEmpleados <= 10)
                    {
                        newEmpresa.Tamano = "Microempresa"; 
                    }
                    else if (noEmpleados >= 1 && noEmpleados <= 30)
                    {
                        newEmpresa.Tamano = "Pequeña";
                    }
                    else if (noEmpleados >= 31 && noEmpleados <= 100)
                    {
                        newEmpresa.Tamano = "Mediana";
                    }
                    else if (noEmpleados >= 101)
                    {
                        newEmpresa.Tamano = "Grande";
                    }
                    break;
                case "Servicios":
                    if (noEmpleados >= 1 && noEmpleados <= 10)
                    {
                        newEmpresa.Tamano = "Microempresa"; //ddlTamano.SelectedItem.ToString();
                    }
                    else if (noEmpleados >= 1 && noEmpleados <= 50)
                    {
                        newEmpresa.Tamano = "Pequeña";
                    }
                    else if (noEmpleados >= 51 && noEmpleados <= 100)
                    {
                        newEmpresa.Tamano = "Mediana";
                    }
                    else if (noEmpleados >= 101)
                    {
                        newEmpresa.Tamano = "Grande";
                    }
                    break;
            }
        }

        if (txtAnoInicio.Text != "")
        {
            newEmpresa.AnoInicio = Convert.ToInt32(txtAnoInicio.Text);
        }

        newEmpresa.ComposicionK = txtCapitalOtro.Text;
        if (txtPostulanteOtro.Text == "")
            newEmpresa.postulante = "0";
        else
            newEmpresa.postulante = txtPostulanteOtro.Text; //ddlPostulante.SelectedValue;

        newEmpresa.Producto = txtNombreProducto.Text;
        newEmpresa.Productos = txtPrincipalesProductos.Text;
        newEmpresa.RSE = txtRSE.Text;
        newEmpresa.Fundacion = txtFundacion.Text;
        newEmpresa.Ambito = txtAmbito.Text;
        if (ddlEmpresaReferencia.SelectedIndex > -1)
        {
            newEmpresa.cadenaDeValor = Convert.ToInt32(ddlEmpresaReferencia.SelectedValue);
        }
        else
        {
            newEmpresa.cadenaDeValor = 0;
        }
        //Autorizada para contestar cuestionarios, para las nuevas siempre es falso, ademas de que está oculto
        newEmpresa.autorizada = rbtAutorizadaSi.Checked;
        // MRA 06/07/2010 23:56
        // Para la nueva versión siempre será diferente de nulo, pero lo dejamos por si acaso
        // Se manda el idUsuario solo para efectos de bitácora.
        if (Session["idUsuario"] == null)
            newEmpresa.idUsuario = txtEmail.Text;
        else
            newEmpresa.idUsuario = Session["idUsuario"].ToString();
        // Si todo sale bien al guardar, 
        // se checa si esta visible el updatepanel de administración y se asigna el idEmpresa al txtidEmpresa
        if (newEmpresa.Guardar())
        {
            if (cblCuestionarios.Visible)
            {
                // MRA: Estoy quitando esta linea para que no se reactiven los cuestionarios
                // Fecha: 12/09/2008 23:43
                //newEmpresa.EliminarCuestionarios();
                for (int i = 0; i < cblCuestionarios.Items.Count; i++)
                {
                    newEmpresa.idCuestionario = Convert.ToInt32(cblCuestionarios.Items[i].Value);
                    if (cblCuestionarios.Items[i].Selected)
                    {
                        newEmpresa.GuardarCuestionario();
                    }
                    else
                    {
                        newEmpresa.EliminarCuestionario();
                    }
                }
            }
            // Asignación automática de cuestionario
            else //if (Session["perfil"].ToString().IndexOf('2') == -1 && Session["perfil"].ToString().IndexOf('9') == -1) // Si es administrador y si es staff
            {

                // MRA 13/07/2010 10:26 a.m.
                // Ya quedó lista esta funcionalidad solo hay que tener cuidado si cambian el tamaño les van a quedar
                // asignados los demas cuestionarios, se tendrán que desasignar.
                //newEmpresa.idCuestionario = Convert.ToInt32(ConfigurationManager.AppSettings["idPrediagnostico"]);
                //newEmpresa.GuardarCuestionario();


                // MRA 22/10/2010 06:18 a.m.
                // Deshabilitar la asignación automática de cuestionario
                //if (newEmpresa.Tamano != "")
                //{
                //    switch (newEmpresa.Tamano)
                //    {
                //        case "Grande":
                //            newEmpresa.idCuestionario = Convert.ToInt32(ConfigurationManager.AppSettings["idESR1_5"]);
                //            newEmpresa.GuardarCuestionario();
                //            break;
                //        case "Mediana":
                //            newEmpresa.idCuestionario = Convert.ToInt32(ConfigurationManager.AppSettings["idMedianas"]);
                //            newEmpresa.GuardarCuestionario();
                //            break;
                //        case "Pequeña":
                //            newEmpresa.idCuestionario = Convert.ToInt32(ConfigurationManager.AppSettings["idPYME1_5"]);
                //            newEmpresa.GuardarCuestionario();
                //            break;
                //        case "Microempresa":
                //            newEmpresa.idCuestionario = Convert.ToInt32(ConfigurationManager.AppSettings["idPYME1_5"]);
                //            newEmpresa.GuardarCuestionario();
                //            break;
                //    }
                //}
            }
            txtidEmpresa.Text = newEmpresa.idEmpresa.ToString();
        }
        else
        {
            Response.Redirect("~/tools/error.aspx?message=" +
                        "No se realizó el registro de la empresa, " +
                        "debido a que la empresa ya existe en nuestra base de datos, " +
                        "por favor verifique su información.", false);
        }
    }
    protected void grdCertificaciones_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (Session["idEmpresa"] != null)
        {
            try
            {
                string idcertificacion = grdCertificaciones.DataKeys[e.RowIndex].Value.ToString();
                string idempresa = ViewState["idEmpresa"].ToString();

                Certificacion certi = new Certificacion();
                certi.idEmpresa = Convert.ToInt32(idempresa);
                certi.idcertificacion = Convert.ToInt32(idcertificacion);


                if (certi.Eliminar())
                {
                    DataSet dsCert = certi.CargaCertificaciones();
                    grdCertificaciones.DataSource = dsCert;
                    grdCertificaciones.DataBind();

                    //lblCertificaciones.Text += "<tr><td>" + txtRecoAno.Text + "</td><td>" + txtRecoInstitucion.Text + "</td><td>" + txtReco.Text + "</td></tr>";
                    txtRecoAno.Text = "";
                    txtRecoInstitucion.Text = "";
                    txtReco.Text = "";
                }
            }
            catch (Exception ex)
            {
                using (StreamWriter sw = File.AppendText(ConfigurationManager.AppSettings["logFile"].ToString()))
                {
                    sw.AutoFlush = true;
                    sw.WriteLine("Fecha: " + DateTime.Now.ToString());
                    sw.WriteLine("Empresa: " + this.Session["idEmpresa"].ToString());
                    sw.WriteLine("Error en empresa.grdCertificaciones_RowDeleting(): " + ex.Message);
                    sw.WriteLine("InnerException: " + ex.InnerException);
                    sw.Close();
                }
            }
        }
        else
        {
            Response.Redirect("~/login.aspx", false);
        }
    }

    protected void CargaCertificaciones()
    {
        Certificacion certificaciones = new Certificacion();
        certificaciones.idEmpresa = Convert.ToInt32(ViewState["idEmpresa"]);
        DataSet dsCertificaciones = certificaciones.CargaCertificaciones();
        DataView dv = new DataView(dsCertificaciones.Tables["Certificacion"]);
        grdCertificaciones.DataSource = dv;
        grdCertificaciones.DataBind();
    }

    protected void grdCertificaciones_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            grdCertificaciones.DataSource =  null;
            grdCertificaciones.DataBind();
            grdCertificaciones.EditIndex = e.NewEditIndex;
            CargaCertificaciones();

        }
        catch (Exception ex)
        {
            using (StreamWriter sw = File.AppendText(ConfigurationManager.AppSettings["logFile"].ToString()))
            {
                sw.AutoFlush = true;
                sw.WriteLine("Fecha: " + DateTime.Now.ToString());
                sw.WriteLine("Empresa: " + this.Session["idEmpresa"].ToString());
                sw.WriteLine("Error en empresa.grdCertificaciones_RowEditing(): " + ex.Message);
                sw.WriteLine("InnerException: " + ex.InnerException);
                sw.Close();
            }
        }
        //grdCertificaciones.EditIndex = e.NewEditIndex;
        //String country = grdCertificaciones.Rows[e.NewEditIndex].Cells[1].Text;


        //string idempresa = ViewState["idEmpresa"].ToString();
    }
    protected void grdCertificaciones_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            GridViewRow fila = grdCertificaciones.Rows[e.RowIndex];

            if (fila != null)
            {
                Certificacion cert = new Certificacion();
                cert.anio = Convert.ToInt32((fila.FindControl("TextBox1") as TextBox).Text.ToUpper());
                cert.institucion = (fila.FindControl("TextBox2") as TextBox).Text.ToUpper();
                cert.certificacion = (fila.FindControl("TextBox3") as TextBox).Text.ToUpper();
                cert.idEmpresa = Convert.ToInt32(txtidEmpresa.Text.ToUpper());
                string idcertificacion = grdCertificaciones.DataKeys[e.RowIndex].Value.ToString().ToUpper();
                cert.idcertificacion = Convert.ToInt32(idcertificacion);


                if (cert.Actualizar())
                {
                    grdCertificaciones.EditIndex = -1;
                    CargaCertificaciones();

                    DataSet dsCert = cert.CargaCertificaciones();
                    grdCertificaciones.DataSource = dsCert;
                    grdCertificaciones.DataBind();

                    //lblCertificaciones.Text += "<tr><td>" + txtRecoAno.Text + "</td><td>" + txtRecoInstitucion.Text + "</td><td>" + txtReco.Text + "</td></tr>";
                    txtRecoAno.Text = "";
                    txtRecoInstitucion.Text = "";
                    txtReco.Text = "";
                }
                else
                {
                    lblCertificaciones.Text += "<tr><td>Error al salvar la certificacion</td><td>";
                }
            }
        }
        catch (Exception ex)
        {
            using (StreamWriter sw = File.AppendText(ConfigurationManager.AppSettings["logFile"].ToString()))
            {
                sw.AutoFlush = true;
                sw.WriteLine("Fecha: " + DateTime.Now.ToString());
                sw.WriteLine("Empresa: " + this.Session["idEmpresa"].ToString());
                sw.WriteLine("Error en empresa.grdCertificaciones_RowUpdating(): " + ex.Message);
                sw.WriteLine("InnerException: " + ex.InnerException);
                sw.Close();
            }
        }
        
    }
    protected void grdCertificaciones_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdCertificaciones.EditIndex = -1;
        CargaCertificaciones();
    }

    /// <summary>
    /// Método para buscar la empresa que pertenece a la cadena de valor de la empresa que se está registrando
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnBuscarCV_Click(object sender, EventArgs e)
    {
        Empresa cadenaValor = new Empresa();
        //DataSet dsCadenaValor = cadenaValor.Buscar();
    }

    //protected override void OnInit(EventArgs e)
    //{
    //    btnSiguiente0.Click += new EventHandler(btnSiguiente0_Click);
    //    btnAnterior1.Click += new EventHandler(btnAnterior1_Click);
    //    btnFinalizar.Click += new EventHandler(btnFinalizar_Click);
    //    btnMasCertificaciones.Click += new EventHandler(btnMasCertificaciones_Click);
    //    ddlEstado.SelectedIndexChanged += new EventHandler(ddlEstado_SelectedIndexChanged);
    //    ddlCapital.SelectedIndexChanged += new EventHandler(ddlCapital_SelectedIndexChanged);
    //}

}
