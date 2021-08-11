using System;
using ESR.Business;

namespace ESR.tools
{
    public partial class getPass : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            Usuario usr = new Usuario();
            usr.idUsuario = txtCorreo.Text.Trim();
            if (usr.Existe())
            {
                usr.GeneratePassword();
                Response.Redirect("error.aspx?message=" +
                "Se ha generado una nueva contraseña y se ha enviado al correo: " + usr.idUsuario +
                ". Por favor verifique su correo. Gracias.");
            }
            else 
            {
                Response.Redirect("error.aspx?message=" +
                "La cuenta de correo no existe registrada en nuestra base de datos, " +
                "por favor verifique su información.");

            }
        }
    }
}
