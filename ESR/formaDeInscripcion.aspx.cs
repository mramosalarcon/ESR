using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using ESR.Business;

public partial class formaDeInscripcion : System.Web.UI.Page
{

    /// <summary>
    /// Return the value of our Content URL parameter
    /// </summary>
    /// <returns></returns>
    private string GetContent()
    {
        if (Request.Params["Content"] == null)
            return string.Empty;
        else
            return Request.QueryString["Content"];
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

    /// <summary>
    /// Load the page and initialize name, print, datetime and menu controls
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
                PopulateMenu();
                string content = this.GetContent();
                if (content != string.Empty)
                {
                    Control contentControl = Page.LoadControl(content + ".ascx");
                    if (contentControl != null)
                    {
                        this.content.Controls.Clear();
                        this.content.Controls.Add(contentControl);
                        SetTitle(content);
                    }
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
            ClientScript.RegisterStartupScript(this.GetType(), "Login Error",
                "alert('Para acceder al registro, primero debe iniciar sesión');", true);
            Response.Redirect("login.aspx");
        }
    }

    private void PopulateMenu()
    {

        // C�digo temporal para cambiar la imagen del bot�n del master page
        //
        //((ImageButton)this.Master.FindControl("imbRegistro")).ImageUrl = "images/ESRbotonMenuInteriorOver_Registro.jpg";
        //((ImageButton)this.Master.FindControl("imbRegistro")).Attributes["onmouseout"] = "'images/ESRbotonMenuInteriorOver_Registro.jpg'";
        if (Session["perfil"].ToString() != string.Empty)
        {
            ESR.Business.Menu menuOpciones = new ESR.Business.Menu(Session["perfil"].ToString());
            DataSet dsMenu = menuOpciones.Carga(ConfigurationManager.AppSettings["formaDeInscripcion"].ToString());
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
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Login Error", 
                "alert('Para acceder al registro, primero debe iniciar sesión');", true);
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
