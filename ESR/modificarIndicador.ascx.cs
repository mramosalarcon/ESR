using System;
using System.Data;
using System.Web.UI.WebControls;
using ESR.Business;
using System.IO;
using System.Configuration;

public partial class modificarIndicador : System.Web.UI.UserControl
{
    DataSet dsTemas;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                Tema temas = new Tema();
                dsTemas = temas.CargaTemas();
                ddlTemas.DataSource = dsTemas.Tables["Tema"].DefaultView;
                ddlTemas.DataTextField = "descripcion";
                ddlTemas.DataValueField = "idTema";
                ddlTemas.DataBind();

                ListItem item = new ListItem("Seleccione tema...", "0");
                ddlTemas.Items.Insert(0, item);
                ddlTemas.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                using (StreamWriter sw = File.AppendText(ConfigurationManager.AppSettings["logFile"].ToString()))
                {
                    sw.AutoFlush = true;
                    sw.WriteLine("Fecha: " + DateTime.Now.ToString());
                    sw.WriteLine("Empresa: " + Session["idEmpresa"].ToString());
                    sw.WriteLine("Error en modificarIndicador.Page_Load(): " + ex.Message);
                    sw.WriteLine("InnerException: " + ex.InnerException);
                    sw.Close();
                }
            }
        }
    }
    protected void ddlTemas_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DataView dv = new DataView(dsTemas.Tables["Subtemas"], "idTema = " + ddlTemas.SelectedValue, "idSubtema", DataViewRowState.OriginalRows);

        //ddlSubtemas.DataSource = dv;
        //ddlSubtemas.DataTextField = "descripcion";
        //ddlSubtemas.DataValueField = "idSubtema";
        //ddlSubtemas.DataBind();

        //ListItem item = new ListItem("Seleccione subtema...", "0");
        //ddlSubtemas.Items.Insert(0, item);

        Indicador indica = new Indicador();

        DataSet indicadores = indica.CargaIndicadores(Convert.ToInt32(ddlTemas.SelectedValue));

        DataView dv = new DataView(indicadores.Tables["Indicador"]);

        grdIndicadores.DataSource = dv;
        grdIndicadores.DataBind();


    }

}
