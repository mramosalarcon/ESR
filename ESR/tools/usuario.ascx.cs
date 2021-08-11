using System;
using System.Configuration;
using ESR.Business;

namespace ESR.tools
{
    public partial class tools_usuario : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnFinalizar_Click(object sender, EventArgs e)
        {
            Contacto contact = new Contacto();
            //contact.Carga(emp.contacto);
            txtContactoNombre.Text = contact.nombre;
            txtContactoApellidoP.Text = contact.ApellidoP;
            txtContactoApellidoM.Text = contact.ApellidoM;
            txtContactoPuesto.Text = contact.Puesto;
            txtContactoTelefono.Text = contact.Telefono;
            txtContactoExt.Text = contact.Extension;
            txtContactoEmail.Text = contact.Email;
            contact.idPerfil = Convert.ToInt32(ConfigurationManager.AppSettings["idPerfil_Auditor_interno"]); //idPerfil_Auditor_interno
            contact.idTema = 0;

            if (!contact.Existe())
            {
                contact.GeneratePassword();


                if (contact.GuardaContacto())
                {
                    Response.Redirect("~/tools/error.aspx?message=" +
                                            "El registro se ha realizado correctamente, " +
                                            "en un plazo no mayor a  24 horas recibirá un mensaje al correo especificado " +
                                            "con la contraseña para acceder a la aplicación. En caso de no recibir el correo, " +
                                            "utilice la herramienta de recuperación de contraseña, en la pantalla de acceso de la aplicacion.");
                }
            }
            else
            {
                Response.Redirect("~/tools/error.aspx?message=" +
                                            "El usuario especificado ya existe, para recuperar su contraseña " +
                                            "utilice la herramienta de recuperación de contraseña, en la pantalla de acceso de la aplicación.");
            }
        }
    }
}