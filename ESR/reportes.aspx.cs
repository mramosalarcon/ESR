using System;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using Microsoft.SharePoint;
using ESR.Business;
using Microsoft.SharePoint.WebControls;

public partial class reportes : System.Web.UI.Page
{
    private int GetIdCuestionario()
    {
        if (Request.Params["idCuestionario"] != null)
            return Convert.ToInt32(Request.Params["idCuestionario"]);
        else
            return 0;
    }
    private string GetContent()
    {
        if (Request.Params["Content"] != null)
            return Request.Params["Content"];
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
                            "$(\"a#docs\").attr('href', 'http://esr.cemefi.org/" + Session["idEmpresa"].ToString() + "/Documentos%20compartidos/Forms/AllItems.aspx');", true);
                    }
                    else
                    {
                        Response.Redirect("~/login.aspx");
                    }
                }

                PopulateMenu();
                string content = GetContent();
                if (content != string.Empty)
                {
                    Control contentControl = Page.LoadControl(content + ".ascx");
                    this.reportesESR.Controls.Clear();

                    this.reportesESR.Controls.Add(contentControl);
                    SetTitle(content);
                }
                else
                {
                    this.reportesESR.Visible = false;
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
            string sMensaje = "Para acceder a sus reportes, primero debe iniciar sesión";
            ClientScript.RegisterStartupScript(this.GetType(), "LoginError",
                String.Format("alert('{0}');", sMensaje.Replace("'", "\'")), true);
            Response.Redirect("login.aspx");
        }
    }

    private void PopulateMenu()
    {
        // C�digo temporal para cambiar la imagen del bot�n del master page
        //
        //((ImageButton)this.Master.FindControl("imbReportes")).ImageUrl = "images/ESRbotonMenuInteriorOver_Reportes.jpg";
        //((ImageButton)this.Master.FindControl("imbReportes")).Attributes["onmouseout"] = "'images/ESRbotonMenuInteriorOver_Reportes.jpg'";

        ESR.Business.Menu menuOpciones = new ESR.Business.Menu(Session["perfil"].ToString());
        DataSet dsMenu = menuOpciones.Carga(ConfigurationManager.AppSettings["reportes"].ToString());
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
