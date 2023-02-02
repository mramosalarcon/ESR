using System;
using ESR.Business;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

public partial class misEmpresas : System.Web.UI.Page
{
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
            string[] usrid = { "", "", "mramos@kicloud.com.mx" };
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
        if (User.Identity.IsAuthenticated)
        {
            try
            {
                if (Session["idEmpresa"] == null || Session["idEmpresa"].ToString() == "")
                {
                    if (LoadSession())
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "documentos",
                        "$(\"a#docs\").attr('href', 'https://esrv1.cemefi.org/" + Session["idEmpresa"].ToString() + "/Documentos%20compartidos/Forms/AllItems.aspx');",
                        true);
                    }
                    else
                    {
                        Response.Redirect("~/login.aspx", false);
                    }
                }
                if (!IsPostBack)
                {
                    Empresa empresas = new Empresa();
                    empresas.idUsuario = Session["idUsuario"].ToString();
                    ddlEmpresas.DataSource = empresas.CargaEmpresas();
                    ddlEmpresas.DataTextField = "nombre";
                    ddlEmpresas.DataValueField = "idEmpresa";
                    ddlEmpresas.DataBind();

                    ddlEmpresas.SelectedValue = Session["idEmpresa"].ToString();
                    //lblEmpresa.Text = "";
                }
            }
            catch
            {
                ClientScript.RegisterStartupScript(this.GetType(), "App Error",
                    "alert('Hubo un error al cargar la página, por favor inicie sesión nuevamente.');", true);
            }
        }
        else
        {
            string sMensaje = "Para acceder a sus empresas, primero debe iniciar sesión";
            ClientScript.RegisterStartupScript(this.GetType(), "LoginError",
                String.Format("alert('{0}');", sMensaje.Replace("'", "\'")), true);
            Response.Redirect("login.aspx", false);
        }
    }
    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        Session["idEmpresa"] = ddlEmpresas.SelectedValue;
        Empresa empresa2 = new Empresa();
		empresa2.idEmpresa = Convert.ToInt32(ddlEmpresas.SelectedValue);
		if (empresa2.cargaNombre())
		{
			Session["empresa"] = empresa2.nombre + " - " + empresa2.nombreCorto;
		}
        SPSecurity.RunWithElevatedPrivileges(delegate()
        {
            //swsec.WriteLine("Sitio: " + SPContext.Current.Web.Url + "/" + usr.idEmpresa.ToString());
            using (SPSite site = new SPSite(SPContext.Current.Web.Url + "/" + ddlEmpresas.SelectedValue.ToString()))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    SPWeb theSite = SPControl.GetContextWeb(Context);
                    SPUser theUser = theSite.CurrentUser;
                    string[] usrid = theUser.LoginName.Split('|');

                    Usuario usr = new Usuario();
                    usr.idUsuario = usrid[2].ToString();
                    if (usr.CargaUsuario())
                    {
                        //i:0#.f|esrmembers|esr@cemefi.org
                        string _usernameWithProvider = String.Format("{0}:0#.f|esrmembers|{1}", System.Web.Security.Membership.Provider.Name, usr.idUsuario);
                        web.AllowUnsafeUpdates = true;
                        //swsec.WriteLine("Usuario: " + _usernameWithProvider + ", Nombre: " + usr.nombre + ", ApellidoP: " + usr.apellidoP + ", ApellidoM: " + usr.apellidoM);

                        SPRoleAssignment MyRoleAssign = new SPRoleAssignment(_usernameWithProvider, usr.idUsuario, usr.nombre + " " + usr.apellidoP + " " + usr.apellidoM, "");
                        SPRoleDefinition MyRoleDef = web.RoleDefinitions["Colaborar"];
                        MyRoleAssign.RoleDefinitionBindings.Add(MyRoleDef);
                        web.RoleAssignments.Add(MyRoleAssign);


                        SPUser user = web.SiteUsers[_usernameWithProvider];
                        if (user != null)
                        {
                            web.Update();
                            web.AllowUnsafeUpdates = false;
                        }
                    }
                    //swsec.WriteLine("Usuario valido");
                }
            }
        });
        Response.Redirect("default.aspx", false);
        //lblEmpresa.Text = "La empresa fue cambiada exit�samente a: " + ddlEmpresas.SelectedItem.ToString();
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
