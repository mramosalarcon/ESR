using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;
using ESR.Business;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace ESR
{
    public partial class administracion : System.Web.UI.Page
    {
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
                            "$(\"a#docs\").attr('href', 'http://esr.cemefi.org/" + Session["idEmpresa"].ToString() + "/Documentos%20compartidos/Forms/AllItems.aspx');",
                            true);
                            
                        }
                        else
                        {
                            Response.Redirect("~/login.aspx", false);
                        }
                    }
                    PopulateMenu();
                }
                catch
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "App Error",
                        "alert('Hubo un error al cargar la página, por favor inicie sesión nuevamente.');", true);
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

        private void PopulateMenu()
        {
            // Código temporal para cambiar la imagen del botón del master page
            //
            //((ImageButton)this.Master.FindControl("imbAdministracion")).ImageUrl = "images/ESRbotonMenuInteriorOver_Administracion.jpg";
            //((ImageButton)this.Master.FindControl("imbAdministracion")).Attributes["onmouseout"] = "'images/ESRbotonMenuInteriorOver_Administracion.jpg'";

            ESR.Business.Menu menuOpciones = new ESR.Business.Menu(Session["perfil"].ToString());
            DataSet dsMenu = menuOpciones.Carga(ConfigurationManager.AppSettings["administracion"].ToString());
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
#if !Debug
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            SPWeb Web = SPContext.Current.Web;
            string strUrl =
                Web.ServerRelativeUrl + "/_catalogs/masterpage/seattle.master";

            this.MasterPageFile = strUrl;
        }
#endif
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
    }
}
