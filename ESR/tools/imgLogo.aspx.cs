using System;
using System.Configuration;
using System.Drawing.Imaging;
using ESR.Business;

namespace ESR.tools
{
    public partial class tools_imgLogo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int idEmpresa = 0;
            if (Request.QueryString["idEmpresa"] != null)
                idEmpresa = Convert.ToInt32(Request.QueryString["idEmpresa"]);
            else
                if (Session["idEmpresa"] != null)
                    idEmpresa = Convert.ToInt32(Session["idEmpresa"]);

            if (idEmpresa != 0)
            {
                Response.Clear();
                Response.ContentType = "image/jpeg";

                Empresa empLogo = new Empresa();
                empLogo.idEmpresa = idEmpresa;
                System.Drawing.Image image = empLogo.CargaLogo();
                if (image != null)
                {
                    image.Save(Response.OutputStream, ImageFormat.Gif);
                }
                else
                {
                    image = System.Drawing.Image.FromFile(ConfigurationManager.AppSettings["no_photo"].ToString());
                    image.Save(Response.OutputStream, ImageFormat.Gif);
                }
            }
            else
            {
                System.Drawing.Image image = System.Drawing.Image.FromFile(ConfigurationManager.AppSettings["no_photo"].ToString());
                image.Save(Response.OutputStream, ImageFormat.Gif);
            }
        }
    }
}