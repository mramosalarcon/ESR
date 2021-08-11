using System;
using System.Data;
using System.Web.UI.WebControls;
using ESR.Business;
using System.Web.UI;
using Microsoft.SharePoint;
using System.Diagnostics;
using Microsoft.SharePoint.WebControls;
using System.IO;
using System.Configuration;

public partial class administradorDeEmpresas : System.Web.UI.Page
{
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        if (!chkTodasLasEmpresas.Checked)
        {
            if (txtBuscar.Text.Length > 1)
            {
                lblMensaje.Text = "";
                Empresa empresas = new Empresa();
                empresas.liberado = chkCuestionarioLiberado.Checked;
                if (chkCuestionarioLiberado.Checked)
                {
                    empresas.idCuestionario = Convert.ToInt32(ddlCuestionarios.SelectedValue[ddlCuestionarios.SelectedIndex].ToString());
                }
                else if (chkCuestionario.Checked)
                {
                    empresas.idCuestionario = Convert.ToInt32(ddlCuestionarios.SelectedValue[ddlCuestionarios.SelectedIndex].ToString());
                }
                DataSet dsEmpresas = empresas.Buscar(txtBuscar.Text.Trim());
                if (dsEmpresas.Tables["Empresa"].Rows.Count > 0)
                {
                    grdEmpresas.DataSource = dsEmpresas;
                    grdEmpresas.DataBind();
                }
                else
                {
                    grdEmpresas.DataSource = null;
                    grdEmpresas.DataBind();
                    lblMensaje.Text = "El criterio de búsqueda no arrojó ningún resultado, intente de nuevo.";
                }
            }
            else
            {
                grdEmpresas.DataSource = null;
                grdEmpresas.DataBind();
                lblMensaje.Text = "Ingrese un criterio de búsqueda más específico.";
            }
        }
        else
        {
            lblMensaje.Text = "";
            Empresa empresas = new Empresa();
            empresas.liberado = chkCuestionarioLiberado.Checked;
            if (chkCuestionarioLiberado.Checked)
            {
                empresas.idCuestionario = Convert.ToInt32(ddlCuestionarios.SelectedValue[ddlCuestionarios.SelectedIndex].ToString());
            }
            else if (chkCuestionario.Checked)
            {
                empresas.idCuestionario = Convert.ToInt32(ddlCuestionarios.SelectedValue[ddlCuestionarios.SelectedIndex].ToString());
            }

            DataSet dsEmpresas = empresas.CargaTodas();
            if (dsEmpresas.Tables["Empresa"].Rows.Count > 0)
            {
                grdEmpresas.DataSource = dsEmpresas;
                grdEmpresas.DataBind();
            }
            else
            {
                grdEmpresas.DataSource = null;
                grdEmpresas.DataBind();
                lblMensaje.Text = "El criterio de búsqueda no arrojo ningún resultado, intente de nuevo.";
            }

        }

    }

    protected void cmdReporteDeAvance(Object src, GridViewCommandEventArgs e)
    {

        // get the row index stored in the CommandArgument property
        int index = Convert.ToInt32(e.CommandArgument);

        // get the GridViewRow where the command is raised
        GridViewRow selectedRow = ((GridView)e.CommandSource).Rows[index];
        if (e.CommandName == "evidencias")
        {		
            Response.Redirect("misCuestionarios.aspx?nombreReporte=EvidenciasPorCuestionario&idEmpresa=" + selectedRow.Cells[0].Text + "&idCuestionario=" + selectedRow.Cells[5].Text, false);
        }
        if (e.CommandName == "avance")
        {
            Response.Redirect("misCuestionarios.aspx?avance=true&evidencias=true&evaluacion=true&idEmpresa=" + selectedRow.Cells[0].Text + "&idCuestionario=" + selectedRow.Cells[5].Text, false);
        }
        else if (e.CommandName=="individual")
        {
            Response.Redirect("misCuestionarios.aspx?individual=true&idEmpresa=" + selectedRow.Cells[0].Text + "&idCuestionario=" + selectedRow.Cells[5].Text, false);
        }
        else if (e.CommandName == "retro")
        {
            Response.Redirect("misCuestionarios.aspx?nombreReporte=retroalimentacion&idEmpresa=" + selectedRow.Cells[0].Text + "&idCuestionario=" + selectedRow.Cells[5].Text, false);
        }
        else if (e.CommandName == "eliminar")
        {
            Empresa emp = new Empresa();
            emp.idEmpresa = Convert.ToInt32(selectedRow.Cells[0].Text);

            if (emp.EliminarRegistro())
            {
                this.btnBuscar_Click(null, null);
            }

        }

    }
    private bool LoadSession()
    {
        bool result = false;
        StreamWriter sw = File.AppendText("e:\\temp\\session.log");
        sw.AutoFlush = true;
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
            sw.WriteLine(DateTime.Now.ToString() + "Cargar usuario: " + usr.idUsuario);
            if (usr.CargaUsuario())
            {
                Session["idEmpresa"] = usr.idEmpresa;
                sw.WriteLine("idEmpresa: " + Session["idEmpresa"].ToString());
                Session["perfil"] = usr.perfiles;
                sw.WriteLine("perfil: " + Session["perfil"].ToString());
                Session["idUsuario"] = usr.idUsuario;
                sw.WriteLine("idUsuario: " + Session["idUsuario"].ToString());
                Session["temas"] = usr.temas;
                sw.WriteLine("temas: " + Session["temas"].ToString());
                Session["idPais"] = usr.pais;
                sw.WriteLine("idPais: " + Session["idPais"].ToString());
                Empresa empresa2 = new Empresa();
				empresa2.idEmpresa = Convert.ToInt32(Session["idEmpresa"]);
				if (empresa2.cargaNombre())
				{
					Session["empresa"] = empresa2.nombre + " - " + empresa2.nombreCorto;
					result = true;
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
        if (!IsPostBack)
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
                            //ClientScript.RegisterStartupScript(this.GetType(), "documentos",
                            //    "$(\"a#docs\").attr('href', 'http://esr.cemefi.org/" + Session["idEmpresa"].ToString() + "/Documentos%20compartidos/Forms/AllItems.aspx');",
                            //    true);
                        }
                        //else
                        //{
                        //    Response.Redirect("~/login.aspx");
                        //}
                    }
                    //lblUsuariosConectados.Text = "Usuarios conectados: " + Application["TotalDeUsuarios"].ToString();
                    //lblUsuariosConectados.Visible = false;
                    // MRA: Comentado el 21/10/2011 11:53 a.m.
                    //Cuestionario cuestionario = new Cuestionario();
                    //ddlCuestionarios.DataSource = cuestionario.CargaTodos();
                    //ddlCuestionarios.DataTextField = "nombre";
                    //ddlCuestionarios.DataValueField = "idCuestionario";
                    //ddlCuestionarios.DataBind();
                }
                catch(Exception ex)
                {
                    using (StreamWriter sw = File.AppendText(ConfigurationManager.AppSettings["logFile"].ToString()))
                    {
                        sw.AutoFlush = true;
                        sw.WriteLine("Fecha: " + DateTime.Now.ToString());
                        sw.WriteLine("Error en administradorDeEmpresas.Page_Load(): " + ex.Message);
                        sw.WriteLine("Stacktrace: " + ex.StackTrace);
                        sw.Close();
                    }

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
    }

    protected void chkCuestionario_CheckedChanged(object sender, EventArgs e)
    {
        if (chkCuestionario.Checked)
        {
            ddlCuestionarios.Visible = true;
        }
        else if(!chkCuestionarioLiberado.Checked)
        {
            ddlCuestionarios.Visible = false;
        }
    }
    protected void chkCuestionarioLiberado_CheckedChanged(object sender, EventArgs e)
    {
        if (chkCuestionarioLiberado.Checked)
        {
            ddlCuestionarios.Visible = true;
        }
        else if(!chkCuestionario.Checked)
        {
            ddlCuestionarios.Visible = false;
        }
    }

    protected void chkPago_CheckedChanged(object sender, EventArgs e)
    {
        GridViewRow selectedRow = (GridViewRow)((System.Web.UI.Control)(sender)).Parent.Parent.Parent.Parent;

        Empresa emp = new Empresa();
        emp.idEmpresa = Convert.ToInt32(selectedRow.Cells[0].Text);
        emp.idCuestionario = Convert.ToInt32(selectedRow.Cells[5].Text);
        emp.PagoCuestionario(((CheckBox)sender).Checked);
    }

    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
#if !Debug
        SPWeb Web = SPContext.Current.Web;
        string strUrl = Web.ServerRelativeUrl + "/_catalogs/masterpage/seattle.master";
        if (Session["idPais"] != null)
        {
            switch (Session["idPais"].ToString())
            {
                case "1":
                    strUrl = Web.ServerRelativeUrl + "/_catalogs/masterpage/seattle.master";
                    break;
                case "168":
                    strUrl = Web.ServerRelativeUrl + "/_catalogs/masterpage/default.peru.master";
                    break;
            }
            this.MasterPageFile = strUrl;
        }
#endif
    }

}
