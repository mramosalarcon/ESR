using System;
using Microsoft.SharePoint;

namespace ESR
{
    public partial class vincularEmpresa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
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

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            ESR.Business.Empresa emp = new ESR.Business.Empresa();
            
            emp.idUsuario = txtIdUsuario.Text;
            emp.idEmpresa = Convert.ToInt32(txtIdEmpresa.Text);
            emp.VincularUsuario();

        
        }
    }
}
