using System;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using ESR.Business;
using Microsoft.SharePoint.Navigation;
using System.Linq;
using Microsoft.SharePoint.WebControls;

namespace ESR
{
    public partial class main : System.Web.UI.Page
    {
        protected int GetIdEmpresa()
        {
            if (Request.Params["idEmpresa"] != null)
                return Convert.ToInt32(Request.Params["idEmpresa"].ToString());
            else
                return Convert.ToInt32(Session["idEmpresa"].ToString());
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
                    Empresa empresa = new Empresa();
                    empresa.idEmpresa = Convert.ToInt32(Session["idEmpresa"]);
                    if (empresa.cargaNombre())
                    {
                        Session["empresa"] = empresa.nombre + " - " + empresa.nombreCorto;
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
                try
                {
                    if (Session["idEmpresa"] == null || Session["idEmpresa"].ToString() == "") 
                    {
                        if (LoadSession())
                        {
                            //SPSecurity.RunWithElevatedPrivileges(delegate()
                            //{
                            //    using (SPSite site = new SPSite("http://esr.cemefi.org"))
                            //    {
                            //        using (SPWeb web = site.OpenWeb())
                            //        {
                            //            web.AllowUnsafeUpdates = true;

                            //            string headingTitle = "Documentos";
                            //            string headingUrl = web.Navigation.Home.Url;
                            //            string itemTitle = "Biblioteca de Evidencias";
                            //            string itemUrl = Session["idEmpresa"].ToString() + "/Documentos%20compartidos/Forms/AllItems.aspx";

                            //            SPNavigationNodeCollection ql = web.Navigation.QuickLaunch;

                            //            // If a Resources heading exists, get it.
                            //            SPNavigationNode heading = ql
                            //                .Cast<SPNavigationNode>()
                            //                .FirstOrDefault(n => n.Title == headingTitle);

                            //            // If the heading has an item, get it.
                            //            SPNavigationNode item = heading
                            //                .Children
                            //                .Cast<SPNavigationNode>()
                            //                .FirstOrDefault(n => n.Title == itemTitle);

                            //            // If the item does not exist, create it.
                            //            if (item == null)
                            //            {
                            //                item = new SPNavigationNode(itemTitle, itemUrl, true);
                            //                item = heading.Children.AddAsLast(item);
                            //            }
                            //            if (item != null)
                            //            {
                            //                heading.Children.Delete(item);
                            //                item = new SPNavigationNode(itemTitle, itemUrl, true);
                            //                item = heading.Children.AddAsLast(item);
                            //            }

                            //        }
                            //    }
                            //});


                            //ClientScript.RegisterStartupScript(this.GetType(), "documentos",
                            //    "$(\"a#docs\").attr('href', 'http://esr.cemefi.org/" + Session["idEmpresa"].ToString() + "/Documentos%20compartidos/Forms/AllItems.aspx');",
                            //    true);
                            ////DateTime dtFechaDeLiberacion = Convert.ToDateTime("24/Oct/2011 17:59");
                            ////lblTiempoRestante.Text = String.Format("Faltan {0:D} horas, {1:D} minutos para la liberación del cuestionario ESR 2012 (1 a 5 años).", dtFechaDeLiberacion.Hour - DateTime.Now.Hour, dtFechaDeLiberacion.Minute - DateTime.Now.Minute);
                            //lblUsuariosConectados.Text = "Usuarios conectados: " + Application["TotalDeUsuarios"].ToString();
                            //lblUsuariosConectados.Visible = false;
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
                    if (Request.Params["idUsuario"] != null)
                    {
                        // Vincular al usuario con la empresa
                        Empresa emp = new Empresa();
                        emp.idEmpresa = this.GetIdEmpresa();
                        emp.idUsuario = Request.Params["idUsuario"].ToString();
                        if (Request.Params["cmd"].ToString() == "autorizar")
                        {
                            emp.AceptarVinculo();
                        }
                        else if (Request.Params["cmd"].ToString() == "eliminar")
                        {
                            emp.RechazarVinculo();
                        }
                    }

                    string strPerfiles = Session["perfil"].ToString();
                    if (strPerfiles.IndexOf("7") > -1 || strPerfiles.IndexOf("1") > -1 ||
                            strPerfiles.IndexOf("2") > -1 || strPerfiles.IndexOf("3") > -1 || strPerfiles.IndexOf("9") > -1)
                    {
                        Mensajes mensajes = new Mensajes();
                        mensajes.idEmpresa = this.GetIdEmpresa();
                        if (mensajes.idEmpresa != 0)
                        {
                            grvVinculos.DataSource = mensajes.CargaSolicitudesDeVinculacion();
                            grvVinculos.DataBind();
                        }
                    }

                }
                catch
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "App Error",
                        "alert('Hubo un error al cargar la página, por favor inicie sesión nuevamente.');", true);
                }
            }
        }

        protected void grvVinculos_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            // get the row index stored in the CommandArgument property
            int index = Convert.ToInt32(e.CommandArgument);

            // get the GridViewRow where the command is raised
            GridViewRow selectedRow = ((GridView)e.CommandSource).Rows[index];
            Response.Redirect("main.aspx?idUsuario=" + selectedRow.Cells[2].Text + "&cmd=" + e.CommandName);
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
}