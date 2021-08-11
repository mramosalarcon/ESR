using System;
using ESR.Business;

namespace ESR.tools
{
    public partial class regPass : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            Usuario usr = new Usuario();
            usr.Email = txtEmail.Text.Trim();
            usr.idUsuario = txtEmail.Text.Trim();
            if (usr.Existe())
            {
                if (usr.EnviaCorreoCambioContraseña())
                {
                    txtEmail.Text = "";
                    lblMensaje.Text = "Se ha enviado un correo a la cuenta especificada con las instrucciones para reestablecer la contraseña. Gracias.";
                }
                else
                {
                    lblMensaje.Text = "Hubo un error al enviar el correo. Contacte al administrador del sistema en el correo ayudarse@cemefi.org";
                }
            }
            //txtRFC.Text = txtRFC.Text.Replace("-", "").Replace(" ", "").Trim().ToUpper();
            //if (usr.Existe(txtRFC.Text.Trim().ToUpper(), txtCP.Text.Trim()))
            //{
            //    btnAceptar.Visible = false;
            //    string sPwd = usr.GeneratePassword();
            //    lblPwd.Text = "Su nueva contrase�a es: " + sPwd + ".<br/><a href=\"../default.aspx\">Ir a los cuestionarios de evaluaci�n</a>";
            //}
            else
            {
                lblMensaje.Text= "La cuenta de correo no existe en nuestro registro, por favor verifique su información.";
            }
        }
    }

}


