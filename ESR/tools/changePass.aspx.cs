using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESR.Business;

namespace ESR.tools
{
    public partial class changePass : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            // Recuperar la cuenta de correo
            //Instanciar y guardar la contraseña
            //Encriptar antes de guardar
            try
            {
                Usuario usr = new Usuario();

                if (Request.QueryString["token"] == null)
                    usr.idUsuario = Session["idUsuario"].ToString();
                else
                    usr.idUsuario = Request["token"].ToString();

                usr.password = txtConfirm.Text;
                if (usr.GuardarPassword())
                {
                    lblMensaje.Text = "La contraseña fue modificada satisfactoriamente, de clic <a href=\"../login.aspx\">aquí</a> para ir a la pagina de inicio.";
                }
                else
                {
                    lblMensaje.Text = "Hubo un error al guardar la contraseña, intente nuevamente.";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = ex.Message;
            }
        }
    }
}