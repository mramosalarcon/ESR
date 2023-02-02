using System;
using System.Web.Security;
using ESR.Business;
using Microsoft.SharePoint;
using Microsoft.SharePoint.IdentityModel;
using System.IO;
using System.Configuration;

public partial class login : System.Web.UI.Page // Microsoft.SharePoint.IdentityModel.Pages.FormsSignInPage //
{
    private string sMensaje = "";
    
    //protected void LoginButton_Click(object sender, ImageClickEventArgs e)
    //{
    //    if (UserName.Text == "Owner") //if (Login1.UserName == "Owner")
    //    {
    //        if (Password.Text == "c3m3f1") //if (Login1.Password == "c3mEf1")
    //        {
    //            Session["idEmpresa"] = ConfigurationManager.AppSettings["idEmpresa_CEMEFI"];
    //            Session["perfil"] = ConfigurationManager.AppSettings["idPerfil_Owner"];
    //            Session["idUsuario"] = ConfigurationManager.AppSettings["idUsuario_Owner"];
    //            //Session["temas"] = usr.temas;
    //            FormsAuthentication.RedirectFromLoginPage("Due�o del sistema", false);

    //        }
    //    }
    //    else
    //    {
    //        Usuario usr = new Usuario();
    //        usr.idUsuario = UserName.Text;
    //        usr.password = Password.Text;
    //        int resultado = 3;
    //        //if (DateTime.Now <= Convert.ToDateTime("dec/09/2008"))
    //        {
    //            resultado = usr.ValidaUsuario();
    //        }

    //        switch (resultado)
    //        {
    //            case 0:
    //                if (usr.perfiles != null)
    //                {
    //                    Session["idEmpresa"] = usr.idEmpresa;
    //                    Session["perfil"] = usr.perfiles;
    //                    Session["idUsuario"] = usr.idUsuario;
    //                    Session["temas"] = usr.temas;

    //                    if (usr.perfiles.IndexOf('2') > -1 || usr.perfiles.IndexOf('3') > -1 || usr.perfiles.IndexOf('4') > -1 || usr.perfiles.IndexOf('5') > -1 || usr.perfiles.IndexOf('6') > -1 || usr.perfiles.IndexOf('7') > -1 || usr.perfiles.IndexOf('8') > -1)
    //                    {
    //                        Usuario user = new Usuario();
    //                        user.idUsuario = UserName.Text;
    //                        user.status = 1;
    //                        bool alta = user.CargaBitacora();
    //                    }

    //                    Application.Lock();
    //                    Application["TotalDeUsuarios"] = Convert.ToInt32(Application["TotalDeUsuarios"]) + 1;
    //                    Application.UnLock();

    //                    FormsAuthentication.RedirectFromLoginPage(usr.nombre + " " + usr.apellidoP, false);

    //                }
    //                break;
    //            case 1:
    //                sMensaje = "La contrase�a no es v�lida, verifique su informaci�n.";
    //                LoginError(this, e);
    //                break;
    //            case 2:
    //                sMensaje = "El usuario ingresado no existe, por favor realice el registro de su empresa o de su usuario.";
    //                LoginError(this, e);
    //                break;
    //            case 3:
    //                sMensaje = "Los cuestionarios ESR est�n en proceso de evaluaci�n, no se puede otorgar el acceso.";
    //                LoginError(this, e);
    //                break;

    //        }
    //    }
    //}

    protected void LoginError(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "LoginError",
           String.Format("alert('{0}');", sMensaje.Replace("'", "\'")), true);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    lblPassword.Visible = false;
        //    Password.Visible = false;
        //    btnAccederESR.Visible = false;
        //    btnOk.Visible = true;
        //}
    }

    //protected void btnOk_Click(object sender, EventArgs e)
    //{
    //    Usuario usr = new Usuario();
    //    usr.idUsuario = UserName.Text;
    //    if (usr.Existe())
    //    {
    //        lblPassword.Visible = true;
    //        Password.Visible = true;
    //        btnAccederESR.Visible = true;
    //        btnOk.Visible = false;
    //        Password.Focus();
    //    }
    //    else
    //    {
    //        Response.Redirect("tools/registro.aspx?email=" + UserName.Text);
    //        //sMensaje = "El usuario ingresado no existe, por favor realice el registro de su empresa o de su usuario.";
    //        //LoginError(this, e);
    //    }
    //}

    protected void btnAccederESR_Click(object sender, EventArgs e)
    {
        //StreamWriter swsec = File.AppendText("e:\\temp\\" + UserName.Text + ".log");
        //swsec.AutoFlush = true;
        try
        {
            Usuario usr = new Usuario();
            usr.idUsuario = UserName.Text;
            usr.password = Password.Text;
            int resultado = 3;
            //if (DateTime.Now <= Convert.ToDateTime("dec/09/2008"))
            resultado = usr.ValidaUsuario();
            switch (resultado)
            {
                case 0:

                    // 
                    // SPContext.Current.Web.Url == "https://esrv1.cemefi.org"
                    //
                    bool valid = false;
#if !Debug
                    //valid = SPClaimsUtility.AuthenticateFormsUser(Context.Request.UrlReferrer, usr.idUsuario, "!Pass1234");

                    if (SPClaimsUtility.AuthenticateFormsUser(new Uri(SPContext.Current.Web.Url), usr.idUsuario, "!Pass1234"))
                    {
                        if (ConfigurationManager.AppSettings["MSSHSec"].ToString() == "1")
                        {
                            SPSecurity.RunWithElevatedPrivileges(delegate()
                            {
                                //swsec.WriteLine("Sitio: " + SPContext.Current.Web.Url + "/" + usr.idEmpresa.ToString());
                                using (SPSite site = new SPSite(SPContext.Current.Web.Url + "/" + usr.idEmpresa.ToString()))
                                {
                                    using (SPWeb web = site.OpenWeb())
                                    {
                                        //i:0#.f|esrmembers|esr@cemefi.org
                                        string _usernameWithProvider = String.Format("{0}:0#.f|esrmembers|{1}", System.Web.Security.Membership.Provider.Name, usr.idUsuario);
                                        web.AllowUnsafeUpdates = true;
                                        //swsec.WriteLine("Usuario: " + _usernameWithProvider + ", Nombre: " + usr.nombre + ", ApellidoP: " + usr.apellidoP + ", ApellidoM: " + usr.apellidoM);

                                        //web.SiteUsers.Add(_usernameWithProvider, usr.idUsuario, usr.nombre + " " + usr.apellidoP + " " + usr.apellidoM, "");
                                       

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
                                        //swsec.WriteLine("Usuario valido");
                                        valid = true;
                                    }
                                }
                            });
                        }
                        else
                        {
                            valid = true;
                        }
                    }
#else
                valid = true;
#endif
                    if (valid)
                    {
                        if (usr.perfiles != null)
                        {
                            Session["idEmpresa"] = usr.idEmpresa;
                            Session["perfil"] = usr.perfiles;
                            Session["idUsuario"] = usr.idUsuario;
                            Session["temas"] = usr.temas;
                            Session["idPais"] = usr.pais;
                            //StreamWriter swsec = File.AppendText("e:\\temp\\nuevo.log");
                            //swsec.AutoFlush = true;
                            //swsec.WriteLine("Antes de cargar bitacora");
                            if (usr.perfiles.IndexOf('2') > -1 || usr.perfiles.IndexOf('3') > -1 || usr.perfiles.IndexOf('4') > -1 || usr.perfiles.IndexOf('5') > -1 || usr.perfiles.IndexOf('6') > -1 || usr.perfiles.IndexOf('7') > -1 || usr.perfiles.IndexOf('8') > -1)
                            {
                                Usuario user = new Usuario();
                                user.idUsuario = UserName.Text;
                                user.status = 1;
                                bool alta = user.CargaBitacora();
                            }

                            //swsec.WriteLine("Cargo bitacora");
                            Empresa empresa2 = new Empresa();
                            empresa2.idEmpresa = Convert.ToInt32(Session["idEmpresa"]);
                            if (empresa2.cargaNombre())
                            {
                                Session["empresa"] = empresa2.nombre + " - " + empresa2.nombreCorto;
                            }
                            
                            Application.Lock();
                            Application["TotalDeUsuarios"] = Convert.ToInt32(Application["TotalDeUsuarios"]) + 1;
                            Application.UnLock();

                            FormsAuthentication.RedirectFromLoginPage(usr.idUsuario, false);
                            //swsec.WriteLine("Redirect");

                            if (Session["idEmpresa"].ToString() == "1")
                            {
                                //swsec.WriteLine("administradorDeEmpresas.aspx");
                                Response.Redirect("administradorDeEmpresas.aspx", false);
                            }
                            else
                            {
                                //swsec.WriteLine("formaDeInscripcion.aspx");
                                TimeSpan dateDifference = DateTime.Today.Subtract(usr.fechaModificacionEmpresa);
                                if (usr.idEmpresa > 0)
                                {
                                    if (dateDifference.TotalDays > 180) // Se solicita la actualizacion de datos cada 6 meses
                                    {
                                        Response.Redirect("formaDeInscripcion.aspx?Content=empresa&idEmpresa=" + usr.idEmpresa, false);
                                    }
                                }
                                else
                                {
                                    //swsec.WriteLine("VIncular registro");
                                    Response.Redirect("vincularRegistro.aspx", false);
                                }
                            }
                            //Response.Redirect("/" + Session["idEmpresa"].ToString() + "/Documentos%20compartidos/Forms/AllItems.aspx");
                            //swsec.WriteLine("default.aspx");

                            // MRA: 04/04/2019 7:57
                            //Response.Redirect("default.aspx", false);

                            //if (Context.Request.QueryString.Keys.Count > 1)
                            //{
                            //    Response.Redirect(Context.Request.QueryString["Source"].ToString());
                            //}
                            //else
                            //    Response.Redirect(Context.Request.QueryString["ReturnUrl"].ToString());

                        }
                        else
                        {
                            sMensaje = "El acceso fue denegado. Favor de contactar al administrador del sistema."; ;
                            LoginError(this, e);
                        }
                        //});

                    }
                    break;
                case 1:
                    sMensaje = "La contrase�a no es v�lida, verifique su información.";
                    LoginError(this, e);
                    break;
                case 2:
                    sMensaje = "El usuario ingresado no existe, por favor realice el registro de su usuario.";
                    LoginError(this, e);
                    break;
                case 3:
                    //default:
                    sMensaje = "Los cuestionarios ESR se encuentran en proceso de evaluación, no se puede otorgar el acceso." + resultado.ToString();
                    LoginError(this, e);
                    break;
            }
        }

        catch (Exception ex)
        {
            using (StreamWriter sw = File.AppendText(ConfigurationManager.AppSettings["logFile"].ToString()))
            {
                sw.AutoFlush = true;
                sw.WriteLine("Fecha: " + DateTime.Now.ToString());
                sw.WriteLine("Usuario: " + UserName.Text);
                sw.WriteLine("Error en login.btnAccederESR_Click(): " + ex.Message);
                if (Session["idEmpresa"] != null)
                {
                    sw.WriteLine("idEmpresa: " + Session["idEmpresa"].ToString());
                }
                sw.WriteLine("InnerException: " + ex.InnerException);
                sw.Close();
            }
        }
    }
}

