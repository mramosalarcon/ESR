using System;
using System.Data;
using System.Web.UI.WebControls;
using ESR.Business;
using System.Web.Security;

namespace ESR.tools
{
    public partial class registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Catalogo paises = new Catalogo();
                DataSet dsPaises = paises.Carga("PAIS");
                ddlPais.DataSource = dsPaises.Tables[0].DefaultView;
                ddlPais.DataTextField = "nombre";
                ddlPais.DataValueField = "idPais";
                ddlPais.DataBind();

                ListItem item = new ListItem("Seleccione pais...", "0");
                ddlPais.Items.Insert(0, item);

                if (Request.Params["email"] != null)
                {
                    txtContactoEmail.Text = Request.Params["email"].ToString();
                }
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            MembershipCreateStatus userStatus;

            lblNotaPie.Text = "";
            ESR.Business.Usuario usr = new ESR.Business.Usuario();
            usr.nombre = txtContactoNombre.Text;
            usr.apellidoP = txtContactoApellidoP.Text;
            usr.apellidoM = txtContactoApellidoM.Text;
            //usr.Puesto = txtContactoPuesto.Text;
            usr.Telefono = txtContactoTelefono.Text;
            usr.Extension = txtContactoExt.Text;
            usr.Email = txtContactoEmail.Text;
            usr.idUsuario = txtContactoEmail.Text;
            usr.pais = Convert.ToInt32(ddlPais.SelectedValue);
            lblNotaPie.Visible = true;

            if (!usr.Existe())
            {
                if (!usr.Guarda())
                {
                    lblNotaPie.Text = "Ha ocurrido un error al procesar su registro, intente nuevamente.";
                }
                else
                {
                    MembershipUser newUser = Membership.CreateUser(usr.Email, "!Pass1234", usr.Email, "YourDog", "Reus", true, out userStatus);

                    txtContactoNombre.Text = "";
                    txtContactoApellidoP.Text = "";
                    txtContactoApellidoM.Text = "";
                    txtContactoTelefono.Text = "";
                    txtContactoExt.Text = "";
                    txtContactoEmail.Text = "";
                    txtContactoEmail2.Text = "";
                    ddlPais.ClearSelection();
                    if (usr.pais != 168)
                    {
                        lblNotaPie.Text = "Su registro fue un éxito. Hemos enviado a su correo la contraseña para acceder. En caso de no recibirla verifique su folder de correo no deseado o utilice la herramienta de recuperación de contraseña.\n Para regresar a la pantalla principal, de clic <a href=\"http://esr.cemefi.org\">aquí</a>.";
                    }
                    else
                    {
                        lblNotaPie.Text = "Su registro fue un éxito. Hemos enviado a su correo la contraseña para acceder. En caso de no recibirla verifique su folder de correo no deseado o utilice la herramienta de recuperación de contraseña.\n Para regresar a la pantalla principal, de clic <a href=\"http://esr.peru2021.org\">aquí</a>.";
                    }

                    Response.Redirect("http://esr.cemefi.org", false);
                }
            }
            else
            {
                lblNotaPie.Text = "Esta cuenta de correo ya existe, intente nuevamente.";
            }

        }
        
    }
}
