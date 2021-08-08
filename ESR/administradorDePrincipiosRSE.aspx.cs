using System;
using System.Web.UI.WebControls;
using ESR.Business;
using Microsoft.SharePoint;

public partial class administradorDePrincipiosRSE : System.Web.UI.Page
{
   
    public void CargaPrincipiosRSE()
    {
        PrincipiosRSE principiosRSE = new PrincipiosRSE();
        ddlPrincipiosRSE.DataSource = principiosRSE.CargaTodos();
        ddlPrincipiosRSE.DataTextField = "descripcion";
        ddlPrincipiosRSE.DataValueField = "idPrincipioRSE";
        ddlPrincipiosRSE.DataBind();

        ListItem item = new ListItem("Nuevo", "99");
        ddlPrincipiosRSE.Items.Insert(0, item);

        item = new ListItem("Seleccione...", "0");
        ddlPrincipiosRSE.Items.Insert(0, item);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.CargaPrincipiosRSE();
        }
    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        PrincipiosRSE principio = new PrincipiosRSE();
        principio.descripcion = txtDescripcion.Text.Trim();
        principio.idUsuario = Session["idUsuario"].ToString();
        if (principio.Guarda())
        {
            ddlPrincipiosRSE.ClearSelection();
            this.LimpiarCampos();
            this.CargaPrincipiosRSE();
        }
    }
    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        PrincipiosRSE principio = new PrincipiosRSE();
        principio.descripcion = txtDescripcion.Text.Trim();
        principio.idUsuario = Session["idUsuario"].ToString();
        if (principio.Elimina())
        {
            ddlPrincipiosRSE.ClearSelection();
            this.LimpiarCampos();
            this.CargaPrincipiosRSE();
        }

    }

    protected void LimpiarCampos()
    {
        txtDescripcion.Text = "";
    }


    protected void ddlPrincipiosRSE_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPrincipiosRSE.SelectedValue.ToString() != "0")
        {
            LimpiarCampos();
            if (ddlPrincipiosRSE.SelectedValue.ToString() != "99")
            {
                txtDescripcion.Text = ddlPrincipiosRSE.SelectedItem.Text;
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
