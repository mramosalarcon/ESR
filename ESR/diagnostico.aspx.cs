using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESR.Business;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

public partial class diagnostico : System.Web.UI.Page
{
    //DataSet dsIndicadores;
    /// <summary>
    /// Return the value of our Content URL parameter
    /// </summary>
    /// <returns></returns>
    private string GetContent()
    {
        if (Request.Params["Content"] != null)
            return Request.QueryString["Content"];
        else
            return string.Empty;
    }

    /// <summary>
    /// Set the title of the Content section using the Content string being loaded.
    /// </summary>
    /// <param name="title"></param>
    private void SetTitle(string content)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        foreach (char c in content)
        {
            if (Char.IsUpper(c))
                sb.Append(' ');
            sb.Append(c);
        }

        this.Title = sb.ToString();
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
				if (base.Request.Params["idCuestionario"] != null)
				{
					empresa2.idCuestionario = Convert.ToInt32(base.Request.Params["idCuestionario"]);
					Session["cuestionario"] = empresa2.CargaCuestionario();
				}
                result = true;
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
        if (User.Identity.IsAuthenticated)
        {
            try
            {
                if (Session["idEmpresa"] == null || Session["idEmpresa"].ToString() == "")
                {
                    if (!LoadSession())
                    {
                        Response.Redirect("~/login.aspx", false);
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
				if (Session["cuestionario"] != null && Session["cuestionario"].ToString() != "")
				{
					lblProceso.Text = Session["cuestionario"].ToString();
					lblProceso.Visible = true;
				}
				else
				{
					lblProceso.Text = "Atención: No hay cuestionario disponible. Favor de cerrar sesión.";
					lblProceso.Visible = false;
				}
                string content = GetContent();
                // Hay que analizar el querystring porque aqui podemos poner solo el menu del tema
                // que se est� contestando y no todo el menu

                if (content != null && content != string.Empty)
                {
                    if (content == "avanceDeDiagnostico")
                    {
                        btnAnterior.Visible = false;
                        btnSiguiente.Visible = false;
                        btnImprimir.Visible = true;
                    }
                    Control contentControl = Page.LoadControl(content + ".ascx");
                    this.indicadores.Controls.Clear();

                    if (Request.Params["idTema"] != null)
                    {
                        ((Label)contentControl.Controls[1]).Text = Request.Params["idTema"];
                    }
                    contentControl.Controls[1].Visible = false;

                    if (Request.Params["idSubtema"] != null)
                    {
                        ((Label)contentControl.Controls[3]).Text = Request.Params["idSubtema"];
                        //lblTemaSubtema.Text = "Tema: " + Request.Params["idTema"] + " -> Subtema: " + Request.Params["idSubtema"] + "<br>";
                    }
                    contentControl.Controls[3].Visible = false;


                    // Cuando se ejecuta esta condici�n?
                    if (Request.Params["idIndicador"] != null)
                    {
                        ((Label)contentControl.Controls[5]).Text = Request.Params["idIndicador"];
                        if (Convert.ToInt16(Request.Params["Ordinal"]) == 31)
                            btnSiguiente.Text = "Fin Cuestionario";
                        //lblTemaSubtema.Text = "Tema: " + Request.Params["idTema"] + " -> Subtema: " + Request.Params["idSubtema"] + " -> Indicador: " + Request.Params["idIndicador"] + "<br>";
                    }
                    contentControl.Controls[5].Visible = false;

                    if (Request.Params["ReadOnly"] != null)
                    {
                        ((Label)contentControl.Controls[7]).Text = Request.Params["ReadOnly"];
                    }
                    contentControl.Controls[7].Visible = false;

                    if (Request.Params["idCuestionario"] != null)
                    {
                        ((Label)contentControl.Controls[9]).Text = Request.Params["idCuestionario"];
                    }
                    contentControl.Controls[9].Visible = false;

                    if (Request.Params["ordinal"] != null)
                    {
                        ((Label)contentControl.Controls[11]).Text = Request.Params["ordinal"];
                    }
                    contentControl.Controls[11].Visible = false;

                    this.indicadores.Controls.Add(contentControl);
                    SetTitle(content);
                }
                else
                {
                    btnAnterior.Visible = false;
                    btnSiguiente.Visible = false;
                    btnImprimir.Visible = false;
                    PopulateMenu();
                }

            }
            catch (Exception ex)
            {
                using (StreamWriter sw = File.AppendText(ConfigurationManager.AppSettings["logFile"].ToString()))
                {
                    sw.AutoFlush = true;
                    sw.WriteLine("Fecha: " + DateTime.Now.ToString());
                    //sw.WriteLine("Empresa: " + Session["idEmpresa"].ToString());
                    sw.WriteLine("idCuestionario: " + lblIdCuestionario.Text);
                    sw.WriteLine("Error en diagnostico.aspx.Page_Load(): " + ex.Message);
                    sw.WriteLine("InnerException: " + ex.InnerException);
                    sw.Close();
                }
            }
        }
        else
        {
            string sMensaje = "Para acceder a la herramienta de administración, primero debe iniciar sesión";
            ClientScript.RegisterStartupScript(this.GetType(), "LoginError",
                String.Format("alert('{0}');", sMensaje.Replace("'", "\'")), true);
            Response.Redirect("login.aspx", false);
        }
    }
    private int GetCuestionario()
    {
        if (Request.QueryString["idCuestionario"] != null)
            return Convert.ToInt32(Request.QueryString["idCuestionario"]);
        else
            return 0;
    }

    private void PopulateMenu()
    {
        Cuestionario cuestionario = new Cuestionario();
        cuestionario.idCuestionario = this.GetCuestionario();
        cuestionario.idEmpresa = Convert.ToInt32(Session["idEmpresa"]);
        DataSet dsCuestionario = cuestionario.Carga();
        DataTable dtCuestionario = dsCuestionario.Tables["Cuestionario"];

        if (dtCuestionario.Rows.Count > 0)
        {
            lblIdCuestionario.Text += dtCuestionario.Rows[0]["nombre"].ToString();
            lblIdCuestionario.Visible = false;
			Session["cuestionario"] = dataTable.Rows[0]["nombre"].ToString();
			lblProceso.Text = Session["cuestionario"].ToString();
        }

        // Lo que est� en la variable de sesi�n son los temas a los que tiene permiso el usuario
        // esa tabla se llena en el login por eso es necesario salir y entrar al hacer cambio de permisos
        DataTable dtTemas = (DataTable)Session["temas"];
        if (dtTemas.Rows.Count == 0 && Request.Params["ReadOnly"] != null)
        {
            // Aqui entra cuando no tiene permisos o cuando se quiere entrar al cuestionario en modo ReadOnly
            Tema tm = new Tema();
            dtTemas = tm.CargaTemas().Tables[0];
        }

        if (dtTemas.Rows.Count > 0 || Request.Params["ReadOnly"] != null)
        {
            Table tblTemas = new Table();
            TableRow trwTemas = new TableRow();

            foreach (DataRow drTema in dsCuestionario.Tables["Tema"].Rows)
            {
                // Preguntar si el tema en turno se encuentra en dtTemas que son los permisos del usuario
                DataRow[] arrTema = dtTemas.Select("idTema = " + drTema["idTema"].ToString());
                if (arrTema.GetLength(0) <= 0 && Session["idUsuario"].ToString() == "owner@cemefi.org" && Request.Params["ReadOnly"] == null)                
                {
                    continue;
                }
                // El usuario tiene permiso para contestar el �rea del cuestionario
                //foreach (DataRow temaPermiso in dtTemas.Rows)
                //{
                TableCell tclTema = new TableCell();

                //System.Web.UI.WebControls.Menu menuArea = new System.Web.UI.WebControls.Menu();
                //menuArea.MenuItemClick += new MenuEventHandler(menuArea_MenuItemClick);

                MenuItem tema = new MenuItem("", drTema["idTema"].ToString());
                //MenuItem tema = new MenuItem();
                if (drTema["imageUrl"].ToString() != string.Empty)
                {
                    tema.ImageUrl = drTema["imageUrl"].ToString();
                }

                tema.Selectable = false;
                menuArea.Items.Add(tema);

                foreach (DataRow drSubtema in drTema.GetChildRows("subtema"))
                {
                    MenuItem subtema = new MenuItem(drSubtema["descripcion"].ToString(), drSubtema["idSubtema"].ToString());
                    subtema.Selectable = false;
                    tema.ChildItems.Add(subtema);
                    foreach (DataRow drIndicador in drSubtema.GetChildRows("indicador"))
                    {
                        MenuItem indicador = new MenuItem(drIndicador["ordinal"].ToString() + ". " + drIndicador["corto"].ToString(), drIndicador["idIndicador"].ToString());
                        subtema.ChildItems.Add(indicador);
                    }
                }
                tclTema.Controls.Add(menuArea);
                trwTemas.Cells.Add(tclTema);
            }
            tblTemas.Rows.Add(trwTemas);
            pMenu.Controls.Add(tblTemas);
        }
        else
        {
            // Mensaje de error
            //Response.Write("<span style=\"color: white\">Su cuenta de usuario no cuenta con permisos para contestar este cuestionario, contacte al responsable de ESR en su empresa.</span>");
            lblIdCuestionario.Text = "<span style=\"color: red\">Este usuario no cuenta con permisos para contestar el cuestionario, contacte al Responsable RSE en SU empresa para que le asigne areas del cuestionario o vaya a Administración de usuarios y seleccione las areas.</span>";
            btnAnterior.Visible = false;
            btnSiguiente.Visible = false;
            btnImprimir.Visible = false;
        }

    }

    protected void menuArea_MenuItemClick(object sender, MenuEventArgs e)
    {
        string indicador = "";
        string subtema = "";
        string tema = "";
        string ordinal = "";
        
        //lblTemaSubtema.Text = "";

        if (e.Item.ValuePath.Length > 4) //Carga el indicador
        {
            indicador = e.Item.Value;
            subtema = e.Item.Parent.Value;
            tema = e.Item.Parent.Parent.Value;
            ordinal = e.Item.Text.Substring(0, e.Item.Text.IndexOf("."));
            //Response.Redirect("diagnostico.aspx?idTema=" + tema + "&idSubtema=" + subtema + "&idIndicador=" + indicador);
        }
        else if (e.Item.ValuePath.Length > 2) //Carga los indicadores del tema-subtema
        {
            subtema = e.Item.Value;
            tema = e.Item.Parent.Value;
            indicador = "";
            ordinal = "";
        }
        //btnImprimir.Visible = true;
        if (Request.Params["idEmpresa"] == null)
        {
            if (Request.QueryString["Content"] == "consultaIndicadores" || Request.Params["ReadOnly"] != null)
            {
                Response.Redirect("diagnostico.aspx?Content=visorDelCuestionario&idTema=" + tema + "&idSubtema=" + subtema + "&idIndicador=" + indicador + "&idCuestionario=" + Request.QueryString["idCuestionario"] + "&ReadOnly=true&cuestionario=" + Request.QueryString["cuestionario"] + "&ordinal=" + ordinal, false);
            }
            else
            {
                Response.Redirect("diagnostico.aspx?Content=visorDelCuestionario&idTema=" + tema + "&idSubtema=" + subtema + "&idIndicador=" + indicador + "&idCuestionario=" + Request.QueryString["idCuestionario"] + "&cuestionario=" + Request.QueryString["cuestionario"] + "&ordinal=" + ordinal, false);
            }
        }
        else
        {
            if (Request.QueryString["Content"] == "consultaIndicadores" || Request.Params["ReadOnly"] != null)
            {
                Response.Redirect("diagnostico.aspx?Content=visorDelCuestionario&idTema=" + tema + "&idSubtema=" + subtema + "&idIndicador=" + indicador + "&idCuestionario=" + Request.QueryString["idCuestionario"] + "&ReadOnly=true&idEmpresa=" + Request.Params["idEmpresa"].ToString() + "&cuestionario=" + Request.QueryString["cuestionario"] + "&ordinal=" + ordinal, false);
            }
            else
            {
                Response.Redirect("diagnostico.aspx?Content=visorDelCuestionario&idTema=" + tema + "&idSubtema=" + subtema + "&idIndicador=" + indicador + "&idCuestionario=" + Request.QueryString["idCuestionario"] + "&idEmpresa=" + Request.Params["idEmpresa"].ToString() + "&cuestionario=" + Request.QueryString["cuestionario"] + "&ordinal=" + ordinal, false);
            }

        }
    }

    /// <summary>
    /// Manda llamar un m�todo para obtener el idIndicador en base al Ordinal que sigue
    /// </summary>
    /// <param name="idCuestionario"></param>
    /// <param name="idTema"></param>
    /// <param name="iOrdinal"></param>
    /// <returns></returns>
    private int CargaIdIndicador(string idCuestionario, int idTema, int iOrdinal)
    {
        Indicador indicador = new Indicador();
        indicador.idCuestionario = Convert.ToInt32(idCuestionario);
        indicador.idTema = idTema;
        indicador.ordinal = iOrdinal;
        return indicador.CargaIdIndicador();
    }

    protected void btnSiguiente_Click(object sender, EventArgs e)
    {
        if (btnSiguiente.Text == "Fin Cuestionario")
        {
            Response.Redirect("misCuestionarios.aspx", false);
        }
        else
        {
            int iOrdinal = Convert.ToInt32(Request.Params["ordinal"]) + 1;
            if (iOrdinal < 32)
            {
                int tema = Convert.ToInt32(Request.Params["idTema"]);
                int subtema = Convert.ToInt32(Request.Params["idSubtema"]);
                int indicador = CargaIdIndicador(Request.QueryString["idCuestionario"], tema, iOrdinal);
                if (indicador > 0)
                {
                    if (Request.Params["idEmpresa"] == null)
                    {
                        if (Request.QueryString["Content"] == "consultaIndicadores" || Request.Params["ReadOnly"] != null)
                        {
                            Response.Redirect("diagnostico.aspx?Content=visorDelCuestionario&idTema=" + tema + "&idSubtema=" + subtema + "&idIndicador=" + indicador + "&idCuestionario=" + Request.QueryString["idCuestionario"] + "&ReadOnly=true&cuestionario=" + Request.QueryString["cuestionario"] + "&ordinal=" + iOrdinal, false);
                        }
                        else
                        {
                            Response.Redirect("diagnostico.aspx?Content=visorDelCuestionario&idTema=" + tema + "&idSubtema=" + subtema + "&idIndicador=" + indicador + "&idCuestionario=" + Request.QueryString["idCuestionario"] + "&cuestionario=" + Request.QueryString["cuestionario"] + "&ordinal=" + iOrdinal, false);
                        }
                    }
                    else
                    {
                        if (Request.QueryString["Content"] == "consultaIndicadores" || Request.Params["ReadOnly"] != null)
                        {
                            Response.Redirect("diagnostico.aspx?Content=visorDelCuestionario&idTema=" + tema + "&idSubtema=" + subtema + "&idIndicador=" + indicador + "&idCuestionario=" + Request.QueryString["idCuestionario"] + "&ReadOnly=true&idEmpresa=" + Request.Params["idEmpresa"].ToString() + "&cuestionario=" + Request.QueryString["cuestionario"] + "&ordinal=" + iOrdinal, false);
                        }
                        else
                        {
                            Response.Redirect("diagnostico.aspx?Content=visorDelCuestionario&idTema=" + tema + "&idSubtema=" + subtema + "&idIndicador=" + indicador + "&idCuestionario=" + Request.QueryString["idCuestionario"] + "&idEmpresa=" + Request.Params["idEmpresa"].ToString() + "&cuestionario=" + Request.QueryString["cuestionario"] + "&ordinal=" + iOrdinal, false);
                        }

                    }
                }
                else
                {
                    Response.Redirect("misCuestionarios.aspx", false);
                }
            }
            else
            {
                Response.Write("<b>Los indicadores para este tema se han terminado, por favor seleccione un tema distinto del menú.</b>");
            }
        }
        

    }
    protected void btnAnterior_Click(object sender, EventArgs e)
    {
        int iOrdinal = Convert.ToInt32(Request.Params["ordinal"]) - 1;
        if (iOrdinal > -1)
        {
            int tema = Convert.ToInt32(Request.Params["idTema"]);
            int subtema = Convert.ToInt32(Request.Params["idSubtema"]);
            int indicador = CargaIdIndicador(Request.Params["idCuestionario"], tema, iOrdinal);
            if (indicador > 0)
            {
                if (Request.Params["idEmpresa"] == null)
                {
                    if (Request.Params["Content"] == "consultaIndicadores" || Request.Params["ReadOnly"] != null)
                    {
                        Response.Redirect("diagnostico.aspx?Content=visorDelCuestionario&idTema=" + tema + "&idSubtema=" + subtema + "&idIndicador=" + indicador + "&idCuestionario=" + Request.Params["idCuestionario"] + "&ReadOnly=true&cuestionario=" + Request.Params["cuestionario"] + "&ordinal=" + iOrdinal, false);
                    }
                    else
                    {
                        Response.Redirect("diagnostico.aspx?Content=visorDelCuestionario&idTema=" + tema + "&idSubtema=" + subtema + "&idIndicador=" + indicador + "&idCuestionario=" + Request.Params["idCuestionario"] + "&cuestionario=" + Request.Params["cuestionario"] + "&ordinal=" + iOrdinal, false);
                    }
                }
                else
                {
                    if (Request.Params["Content"] == "consultaIndicadores" || Request.Params["ReadOnly"] != null)
                    {
                        Response.Redirect("diagnostico.aspx?Content=visorDelCuestionario&idTema=" + tema + "&idSubtema=" + subtema + "&idIndicador=" + indicador + "&idCuestionario=" + Request.Params["idCuestionario"] + "&ReadOnly=true&idEmpresa=" + Request.Params["idEmpresa"].ToString() + "&cuestionario=" + Request.Params["cuestionario"] + "&ordinal=" + iOrdinal, false);
                    }
                    else
                    {
                        Response.Redirect("diagnostico.aspx?Content=visorDelCuestionario&idTema=" + tema + "&idSubtema=" + subtema + "&idIndicador=" + indicador + "&idCuestionario=" + Request.Params["idCuestionario"] + "&idEmpresa=" + Request.Params["idEmpresa"].ToString() + "&cuestionario=" + Request.Params["cuestionario"] + "&ordinal=" + iOrdinal, false);
                    }
                }
            }
            else
            {
                Response.Redirect("misCuestionarios.aspx", false);
            }
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
