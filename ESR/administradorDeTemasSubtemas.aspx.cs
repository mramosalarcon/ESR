using System;
using System.Data;
using System.Web.UI.WebControls;
using ESR.Business;
using Microsoft.SharePoint;

public partial class administradorDeTemasSubtemas : System.Web.UI.Page
{
    private void LimpiarCampos()
    {
        txtTema.Text = "";
        txtTemaCorto.Text = "";
        txtTemaOrdinal.Text = "";
        chkTemaBloqueado.Checked = false;

        txtSubtema.Text = "";
        txtSubtemaOrdinal.Text = "";
        chkSubtemaBloqueado.Checked = false;

        ddlSubtemas.Items.Clear();
    }

    private void cargaTemas()
    {
        Tema temas = new Tema();
        temas.banco = false;

        DataSet dsTemas = temas.CargaTemas();
        ddlTemas.DataSource = dsTemas.Tables["Tema"].DefaultView;
        ddlTemas.DataTextField = "descripcion";
        ddlTemas.DataValueField = "idTema";
        ddlTemas.DataBind();

        ListItem item = new ListItem("<Nuevo tema>", "-1");
        ddlTemas.Items.Insert(0, item);

        item = new ListItem("Seleccione tema...", "0");
        ddlTemas.Items.Insert(0, item);

        Session["dsTemas"] = dsTemas;

        LimpiarCampos();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cargaTemas();
        }
    }

    protected void ddlTemas_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtTema.Text != ddlTemas.SelectedItem.ToString() && ddlTemas.SelectedIndex != 0)
        {
            if (Convert.ToInt32(ddlTemas.SelectedValue) != -1)
            {
                DataSet dsTemas = (DataSet)Session["dsTemas"];
                DataRow drTema = dsTemas.Tables["Tema"].Rows[Convert.ToInt32(ddlTemas.SelectedValue) - 1];
                txtTema.Text = ddlTemas.SelectedItem.ToString();
                txtTemaCorto.Text = drTema["descripcionCorta"].ToString();
                txtTemaOrdinal.Text = drTema["Ordinal"].ToString();
                chkTemaBloqueado.Checked = Convert.ToBoolean(drTema["bloqueado"]);

                DataView dv = new DataView(dsTemas.Tables["Subtema"], string.Format("idTema = {0}", ddlTemas.SelectedValue), "idSubtema", DataViewRowState.OriginalRows);

                ddlSubtemas.Enabled = true;
                txtSubtema.Enabled = true;
                txtSubtema.Text = "";

                ddlSubtemas.DataSource = dv;
                ddlSubtemas.DataTextField = "descripcion";
                ddlSubtemas.DataValueField = "idSubtema";
                ddlSubtemas.DataBind();

                ListItem item = new ListItem("<Nuevo subtema>", "-1");
                ddlSubtemas.Items.Insert(0, item);

                item = new ListItem("Seleccione subtema...", "0");
                ddlSubtemas.Items.Insert(0, item);

            }
            else
            { //Nuevo tema
                LimpiarCampos();
                ddlSubtemas.Enabled = false;
                txtSubtema.Enabled = false;
            }
        }
        else if (ddlTemas.SelectedIndex == 0)
        {//Seleccione tema...
            LimpiarCampos();
        }
    }

    protected void ddlSubtemas_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSubtemas.SelectedIndex != 1)
        {
            if (txtSubtema.Text != ddlSubtemas.SelectedItem.ToString() && ddlSubtemas.SelectedIndex != 0)
            {
                DataSet dsTemas = (DataSet)Session["dsTemas"];
                txtSubtema.Text = ddlSubtemas.SelectedItem.ToString();
                DataRow[] drSubtema = dsTemas.Tables["Subtema"].Select(string.Format("idTema = {0} and idSubtema = {1}", ddlTemas.SelectedValue, ddlSubtemas.SelectedValue));
                txtSubtemaOrdinal.Text = drSubtema[0]["ordinal"].ToString();
                chkSubtemaBloqueado.Checked = Convert.ToBoolean(drSubtema[0]["bloqueado"]);
            }
        }
        else
        {
            txtSubtema.Text = "";
            txtSubtemaOrdinal.Text = "";
            chkSubtemaBloqueado.Checked = false;
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        if (ddlTemas.SelectedIndex == 1)
        {
            //Agregar Tema
            Tema newTema = new Tema();
            newTema.descripcion = txtTema.Text.Trim();
            newTema.descripcionCorta = txtTemaCorto.Text.Trim();
            newTema.bloqueado = chkTemaBloqueado.Checked;
            newTema.Guardar(Session["idUsuario"].ToString());

            //ddlSubtemas.Enabled = true;
            //txtSubtema.Enabled = true;

            //ListItem item = new ListItem("<Nuevo subtema>", "1");
            //ddlSubtemas.Items.Insert(0, item);
        }
        else
        { //Actualizar tema
            Tema actTema = new Tema();
            actTema.idTema = Convert.ToInt32(ddlTemas.SelectedValue);
            actTema.descripcion = txtTema.Text.Trim();
            actTema.descripcionCorta = txtTemaCorto.Text.Trim();
            actTema.ordinal = Convert.ToInt32(txtTemaOrdinal.Text.Trim());
            actTema.bloqueado = chkTemaBloqueado.Checked;
            actTema.Guardar(Session["idUsuario"].ToString());
            
        }

        if (ddlSubtemas.SelectedIndex == 1)
        {
            //Agregar subtema
            Tema newSubtema = new Tema();
            newSubtema.idTema = Convert.ToInt32(ddlTemas.SelectedValue);
            newSubtema.descripcion = txtSubtema.Text;
            if (txtSubtemaOrdinal.Text != string.Empty)
                newSubtema.ordinal = Convert.ToInt32(txtSubtemaOrdinal.Text.Trim());
            else
                newSubtema.ordinal = 0;

            newSubtema.bloqueado = chkSubtemaBloqueado.Checked;
            newSubtema.GuardarSubtema(Session["idUsuario"].ToString());

            
        }
        else if (ddlSubtemas.SelectedIndex != -1)
        {// Actualizar un subtema
            Tema actTema = new Tema();
            actTema.idTema = Convert.ToInt32(ddlTemas.SelectedValue);
            actTema.idSubtema = Convert.ToInt32(ddlSubtemas.SelectedValue);
            actTema.descripcion = txtSubtema.Text.Trim();
            if (txtSubtemaOrdinal.Text != string.Empty)
                actTema.ordinal = Convert.ToInt32(txtSubtemaOrdinal.Text.Trim());
            else
                actTema.ordinal = 0;
            actTema.bloqueado = chkSubtemaBloqueado.Checked;

            //Revisar esta linea.
            actTema.GuardarSubtema(Session["idUsuario"].ToString());

            //cargaTemas();

            //ddlTemas.SelectedValue = actTema.idTema;
            //ddlSubtemas.SelectedValue = actTema.idSubtema;
        }

        cargaTemas();
        
    }
    protected void btnEliminar_Click(object sender, EventArgs e)
    {

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
