using System;
using ESR.Business;

namespace ESR
{
    public partial class fileAttach : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SharePoint sp = new SharePoint();
            sp.GetDocuments(Session["idEmpresa"].ToString());

            //grvDocumentos.DataSource = (object)
            //grvDocumentos.DataBind();
        }
    }
}
