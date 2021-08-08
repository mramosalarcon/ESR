using System;
using System.Data;
using System.Web.UI.WebControls;
using ESR.Business;
using System.IO;
using System.Configuration;
using Microsoft.SharePoint;

public partial class propiedadesDeEmpresa : System.Web.UI.Page
{
    protected int GetIdEmpresa()
    {
        if (Request.Params["idEmpresa"] != null)
            return Convert.ToInt32(Request.Params["idEmpresa"].ToString());
        else
            return 0;
    }

    protected string GetNombreEmpresa()
    {
        if (Request.Params["nombre"] != null)
            return Request.Params["nombre"].ToString();
        else
            return string.Empty;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblEmpresa.Text = this.GetNombreEmpresa();
            if (lblEmpresa.Text != string.Empty)
            {
                Empresa empresaCuestionario = new Empresa();
                empresaCuestionario.idEmpresa = this.GetIdEmpresa();

                DataSet dsCuestionarios = empresaCuestionario.CargaCuestionarios();
                ddlCuestionarios.DataValueField = "idCuestionario";
                ddlCuestionarios.DataTextField = "nombre";
                ddlCuestionarios.DataSource = dsCuestionarios.Tables["Cuestionario"];
                ddlCuestionarios.DataBind();

                ListItem item = new ListItem("Seleccione...", "0", true);
                ddlCuestionarios.Items.Insert(0, item);
            }
        }
    }

    protected void ddlCuestionarios_SelectedIndexChanged(object sender, EventArgs e)
    {
        Empresa empresaCuestionario = new Empresa();
        empresaCuestionario.idEmpresa = this.GetIdEmpresa();
        empresaCuestionario.idCuestionario = Convert.ToInt32(ddlCuestionarios.SelectedValue);
        txtFechaLimite.Text = empresaCuestionario.CargaFechaLimiteCuestionario();
    }

    protected void btnBuscarEmpresa_Click(object sender, EventArgs e)
    {
        if (txtBuscarEmpresa.Text.Length > 1)
        {
            lblMensaje.Text = "";
            Empresa empresas = new Empresa();
            DataSet dsEmpresas = empresas.Buscar2(txtBuscarEmpresa.Text.Trim());
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
        else
        {
            grdEmpresas.DataSource = null;
            grdEmpresas.DataBind();
            lblMensaje.Text = "Ingrese un criterio de búsqueda más específico.";
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Empresa empCuestionario = new Empresa();
            empCuestionario.idEmpresa = this.GetIdEmpresa();
            empCuestionario.idCuestionario = Convert.ToInt32(ddlCuestionarios.SelectedValue);
            empCuestionario.fechaLimite = Convert.ToDateTime(txtFechaLimite.Text);
            empCuestionario.idUsuario = Session["idUsuario"].ToString();
            empCuestionario.EstablecerFechaLimite();

            lblEmpresa.Text = "";
            ddlCuestionarios.Items.Clear();
            txtFechaLimite.Text = "";
        }
        catch (Exception ex)
        {
           
            using (StreamWriter sw = File.AppendText(ConfigurationManager.AppSettings["logFile"].ToString()))
            {
                sw.AutoFlush = true;
                sw.WriteLine("Fecha: " + DateTime.Now.ToString());
                sw.WriteLine("Empresa: " + Session["idEmpresa"].ToString());
                sw.WriteLine("Error en propiedadesDeEmpresa.btnGuardar_Click(): " + ex.Message);
                sw.WriteLine("InnerException: " + ex.InnerException);
                sw.Close();
            }
        
            throw ex;
            //Guardar en un archivo de log
        }
    }
    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
#if !Debug
        SPWeb Web = SPContext.Current.Web;
        string strUrl = "";
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
#endif
    }
}
