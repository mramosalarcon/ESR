using System;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;

namespace ESR
{
    public partial class blog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Código temporal para cambiar la imagen del botón del master page
            //
            //((ImageButton)this.Master.FindControl("imbBlog")).ImageUrl = "images/ESRbotonMenuInteriorOver_Blog.jpg";
            //((ImageButton)this.Master.FindControl("imbBlog")).Attributes["onmouseout"] = "'images/ESRbotonMenuInteriorOver_Blog.jpg'";

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
