using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;
using ESR.Business;
using Microsoft.SharePoint;
using System.IO;
using System.Web.Security;

public partial class administradorDeUsuarios : System.Web.UI.Page
{
    /// <summary>
    /// Return the value of our URL parameter
    /// </summary>
    /// <returns></returns>
    private int GetEmpresa()
    {
        return Convert.ToInt32(Session["idEmpresa"]);
    }

    private void CargaUsuarios()
    {
        int idEmpresa = this.GetEmpresa();
        if (idEmpresa != 0)
        {
            lblidEmpresa.Text = idEmpresa.ToString();
            Usuario users = new Usuario();
            users.perfiles = Session["perfil"].ToString();
            if (users.perfiles.IndexOf("0") > -1 || users.perfiles.IndexOf("1") > -1 ||
            users.perfiles.IndexOf("2") > -1 || users.perfiles.IndexOf("7") > -1)
            {
                cblPerfiles.Visible = true;
                lblPerfil.Visible = true;
            }
            else
            {
                cblPerfiles.Visible = false;
                lblPerfil.Visible = false;
            }
            DataSet dsUsers = users.CargaUsuarios(idEmpresa);
            if (dsUsers != null)
            {
                ddlUsuarios.DataSource = dsUsers;
                ddlUsuarios.DataTextField = "idUsuario";
                ddlUsuarios.DataValueField = "idUsuario";
                ddlUsuarios.DataBind();


                ListItem item = new ListItem("- Crear nuevo usuario", "1");
                ddlUsuarios.Items.Insert(0, item);

                item = new ListItem("Seleccione...", "0");
                ddlUsuarios.Items.Insert(0, item);
            }
            else
            {
                Response.Redirect("login.aspx", false);
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "documentos",
            //    "$(\"a#docs\").attr('href', 'http://esr.cemefi.org/" + Session["idEmpresa"].ToString() + "/Documentos%20compartidos/Forms/AllItems.aspx');", true);

            PopulateMenu();

            this.CargaUsuarios();
            Tema temas = new Tema();
            cblTemas.DataSource = temas.CargaTemas();
            cblTemas.DataTextField = "descripcion";
            cblTemas.DataValueField = "idTema";
            cblTemas.DataBind();

            Perfil perfiles = new Perfil();
            perfiles.perfiles = Session["perfil"].ToString();
            cblPerfiles.DataSource = perfiles.Carga();
            cblPerfiles.DataTextField = "nombre";
            cblPerfiles.DataValueField = "idPerfil";
            cblPerfiles.DataBind();            
        }
        EnsureChildControls();
    }

    private void PopulateMenu()
    {
        ESR.Business.Menu menuOpciones = new ESR.Business.Menu(Session["perfil"].ToString());
        DataSet dsMenu = menuOpciones.Carga(ConfigurationManager.AppSettings["administradorDeUsuarios"].ToString());
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

    protected void ddlUsuarios_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlUsuarios.SelectedValue.ToString() != "0")
            {
                LimpiarCampos();
                if (ddlUsuarios.SelectedValue.ToString() == "1")
                {
                    txtEmail.Enabled = true;
                }
                else
                {
                    txtEmail.Enabled = false;
                    // Mostrar los datos del usuario seleccionado
                    Contacto contacto = new Contacto();
                    contacto.idEmpresa = Convert.ToInt32(Session["idEmpresa"].ToString());
                    contacto.Carga(ddlUsuarios.SelectedValue);
                    this.txtNombre.Text = contacto.nombre;
                    this.txtApellidoP.Text = contacto.ApellidoP;
                    this.txtApellidoM.Text = contacto.ApellidoM;
                    this.txtPuesto.Text = contacto.Puesto;
                    this.txtTelefono.Text = contacto.Telefono;
                    this.txtExt.Text = contacto.Extension;
                    this.txtEmail.Text = contacto.Email;

                    foreach (DataRow row in contacto.temas.Rows)
                    {
                        for (int i = 0; i < cblTemas.Items.Count; i++)
                        {

                            if (cblTemas.Items[i].Value == row["idTema"].ToString())
                            {
                                cblTemas.Items[i].Selected = true;
                                break;
                            }
                        }
                    }


                    if (contacto.primario)
                        this.rbtContactoSi.Checked = true;
                    else
                        this.rbtContactoNo.Checked = true;

                    if (contacto.bloqueado)
                        this.rbtBloqueadoSi.Checked = true;
                    else
                        this.rbtBloqueadoNo.Checked = true;

                    foreach (DataRow row in contacto.perfiles.Rows)
                    {
                        for (int i = 0; i < cblPerfiles.Items.Count; i++)
                        {

                            if (cblPerfiles.Items[i].Value == row["idPerfil"].ToString())
                            {
                                cblPerfiles.Items[i].Selected = true;
                                break;
                            }
                        }
                    }

                    int j = 0;
                    bool entro = false;
                    while (j < cblPerfiles.Items.Count)
                    {
                        if (cblPerfiles.Items[j].Text == "Responsable de empresa" || cblPerfiles.Items[j].Text == "Administrador CESR")
                        {
                            if (cblPerfiles.Items[j].Selected == true)
                            {
                                rbtContactoSi.Checked = true;
                                rbtContactoSi.Enabled = false;
                                rbtContactoNo.Checked = false;
                                rbtContactoNo.Enabled = false;
                                cblPerfiles.Items[j].Enabled = false;
                                entro = true;
                            }
                            else
                            {
                                if (!entro)
                                {
                                    rbtContactoSi.Checked = false;
                                    rbtContactoSi.Enabled = true;
                                    rbtContactoNo.Checked = true;
                                    rbtContactoNo.Enabled = true;
                                    cblPerfiles.Items[j].Enabled = true;
                                    entro = false;
                                }
                                else
                                    cblPerfiles.Items[j].Enabled = true;
                            }
                        }
                        j++;
                    }
                    //char[] arrChar = { Convert.ToChar(","), Convert.ToChar(" ") };
                    //string[] arrPerfiles = contacto.perfiles.Split(arrChar);
                    //foreach (string idPerfil in arrPerfiles)
                    //{
                    //    if (idPerfil != "")
                    //    {
                    //        foreach (ListItem item in ddlPerfil.Items)
                    //        {
                    //            if (item.Value == idPerfil)
                    //            {
                    //                item.Selected = true;
                    //                break;
                    //            }
                    //        }
                    //    }
                    //}

                    //this.ddlPerfil.SelectedValue = contacto.perfiles;
                }
            }
        }
        catch(Exception ex)
        {
            StreamWriter sw = File.AppendText("e:\\temp\\useradmin.log");
            sw.AutoFlush = true;
            sw.WriteLine(DateTime.Now.ToString() + ": " + ex.Message + ex.StackTrace);
            sw.Close();
        }
    }
    private void LimpiarCampos()
    {
        cblPerfiles.ClearSelection();
        txtNombre.Text = "";
        txtApellidoP.Text = "";
        txtApellidoM.Text = "";
        txtPuesto.Text = "";
        txtTelefono.Text = "";
        txtExt.Text = "";
        txtEmail.Text = "";
        txtEmail.Enabled = false;
        rbtContactoNo.Checked = false; 
        rbtContactoSi.Checked = false;
        rbtBloqueadoSi.Checked = false;
        rbtBloqueadoNo.Checked = false;
        cblTemas.ClearSelection();

    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        Contacto contacto = new Contacto();
        contacto.idEmpresa = Convert.ToInt32(lblidEmpresa.Text);
        contacto.nombre = txtNombre.Text;
        contacto.ApellidoP = txtApellidoP.Text;
        contacto.ApellidoM = txtApellidoM.Text;
        contacto.Puesto = txtPuesto.Text;
        contacto.Telefono = txtTelefono.Text;
        contacto.Extension = txtExt.Text;
        contacto.Email = txtEmail.Text;
        contacto.primario = rbtContactoSi.Checked; 
        // Validar esto, que solo el administrador pueda cambiar el contacto primario
        contacto.bloqueado = rbtBloqueadoSi.Checked;
        contacto.idUsuarioModificacion = Session["idUsuario"].ToString();

        if (ddlUsuarios.SelectedValue.ToString() == "1")
        {
            contacto.GeneratePassword();
        }

        if (contacto.GuardaContacto())
        {
            MembershipCreateStatus userStatus;

            MembershipUser newUser = Membership.CreateUser(contacto.Email, "!Pass1234", contacto.Email, "YourDog", "Reus", true, out userStatus);
            if (cblPerfiles.Visible)
            {
                for (int i = 0; i < cblPerfiles.Items.Count; i++)
                {
                    if (cblPerfiles.Items[i].Selected)
                    {
                        contacto.idPerfil = Convert.ToInt32(cblPerfiles.Items[i].Value);
                        contacto.GuardaPerfil();
                    }
                }
            }
            else
            {
                contacto.idPerfil = Convert.ToInt32(ConfigurationManager.AppSettings["idPerfil_Auditor_interno"]); //Responsable de la empresa
                contacto.GuardaPerfil();
            }

            contacto.EliminaTemas();
            for (int i = 0; i < cblTemas.Items.Count; i++)
            {
                if (cblTemas.Items[i].Selected)
                {
                    contacto.idTema = Convert.ToInt32(cblTemas.Items[i].Value);
                    contacto.GuardaTema();
                }
            }

        }
        ddlUsuarios.ClearSelection();
        LimpiarCampos();
        this.CargaUsuarios();
    }
    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        Contacto contacto = new Contacto();
        contacto.Elimina(ddlUsuarios.SelectedValue);
        ddlUsuarios.ClearSelection();
        LimpiarCampos();
        this.CargaUsuarios();
    }
    protected void rbtContactoSi_CheckedChanged(object sender, EventArgs e)
    {

    }
    /// <summary>
    /// Gregorio: Ponle comentarios a tu código.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cblPerfiles_SelectedIndexChanged(object sender, EventArgs e)
    {
        int j = 0;
        bool entro = false;
        while (j < cblPerfiles.Items.Count)
        {
            if (cblPerfiles.Items[j].Text == "Responsable de empresa" || cblPerfiles.Items[j].Text == "Administrador CESR")
            {
                if (cblPerfiles.Items[j].Selected == true)
                {
                    rbtContactoSi.Checked = true;
                    rbtContactoSi.Enabled = false;
                    rbtContactoNo.Checked = false;
                    rbtContactoNo.Enabled = false;
                    entro = true;
                }
                else
                {
                    if (!entro)
                    {
                        rbtContactoSi.Checked = false;
                        rbtContactoSi.Enabled = true;
                        rbtContactoNo.Checked = true;
                        rbtContactoNo.Enabled = true;
                        entro = false;
                    }
                }
            }
            j++;
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

    //protected override void OnInit(EventArgs e)
    //{
    //    base.OnInit(e);
    //    btnGuardar.Click += new EventHandler(btnGuardar_Click);
    //    btnEliminar.Click += new EventHandler(btnEliminar_Click);
    //}
}
