using System;
using Microsoft.SharePoint;

namespace ESR
{
    public partial class calificacionGR : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("misCuestionarios.aspx", false);
        }
    }
}