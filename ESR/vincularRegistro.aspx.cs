using System;
using System.Web.UI;
using Microsoft.SharePoint;

namespace ESR
{
    public partial class vincularRegistro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void imgVincular_Click(object sender, ImageClickEventArgs e)
        {
            
        }

        protected void imgRegistroNuevo_Click(object sender, ImageClickEventArgs e)
        {
            
        }

        protected void cmdNext_Click(object sender, EventArgs e)
        {
            //Buscar una empresa para vincularse
            if (rblVincularRegistro.SelectedValue == "0")
            {
                Response.Redirect("buscadorDeEmpresas.aspx");
            }
            else //Registra una nueva empresa
            {
                Response.Redirect("formaDeInscripcion.aspx?Content=empresa&idEmpresa=0");
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
}