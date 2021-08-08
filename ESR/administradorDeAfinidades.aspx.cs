using System;
using System.Web.UI.WebControls;
using ESR.Business;
using Microsoft.SharePoint;

public partial class administradorDeAfinidades : System.Web.UI.Page
{
    /// <summary>
    /// Mensaje de prueba
    /// </summary>
    public void CargaAfinidades()
    {
        Afinidad afinidades = new Afinidad();
        ddlAfinidades.DataSource = afinidades.CargaTodas();
        ddlAfinidades.DataTextField = "descripcion";
        ddlAfinidades.DataValueField = "idAfinidad";
        ddlAfinidades.DataBind();

        ListItem item = new ListItem("Nuevo", "99");
        ddlAfinidades.Items.Insert(0, item);

        item = new ListItem("Seleccione...", "0");
        ddlAfinidades.Items.Insert(0, item);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.CargaAfinidades();
        }
    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        Afinidad afin = new Afinidad();
        afin.descripcion = txtDescripcion.Text.Trim();
        afin.idUsuario = Session["idUsuario"].ToString();
        afin.idAfinidad = Convert.ToInt32(ddlAfinidades.SelectedValue);
        if (afin.Guarda())
        {
            ddlAfinidades.ClearSelection();
            this.LimpiarCampos();
            this.CargaAfinidades();
        }
    }
    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        Afinidad afin = new Afinidad();
        afin.descripcion = txtDescripcion.Text.Trim();
        afin.idUsuario = Session["idUsuario"].ToString();
        if (afin.Elimina())
        {
            ddlAfinidades.ClearSelection();
            this.LimpiarCampos();
            this.CargaAfinidades();
        }

    }

    protected void LimpiarCampos()
    {
        txtDescripcion.Text = "";
    }

    protected void ddlAfinidades_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAfinidades.SelectedValue.ToString() != "0")
        {
            LimpiarCampos();
            if (ddlAfinidades.SelectedValue.ToString() != "99")
            {
                txtDescripcion.Text = ddlAfinidades.SelectedItem.Text;
            }
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
}
