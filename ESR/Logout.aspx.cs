using System;
using System.Web.Security;
using ESR.Business;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web;
using System.IO;

public partial class Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
#if !Debug
        SPWeb theSite = SPControl.GetContextWeb(Context);
        SPUser theUser = theSite.CurrentUser;
        string[] usrid = theUser.LoginName.Split('|');
#else
            string[] usrid = { "", "", "mramos@tralcom.com" };
#endif
        //StreamWriter swsec = File.AppendText("e:\\temp\\logout.log");
        //swsec.AutoFlush = true;
        try
        {

            //swsec.WriteLine(DateTime.Now.ToString() + ": Inicio");
            Usuario usr = new Usuario();
            usr.idUsuario = usrid[2].ToString();

            ESR.Business.Usuario user = new ESR.Business.Usuario();
            user.idUsuario = usr.idUsuario;
            user.status = 0;
            bool alta = user.CargaBitacora();

            //swsec.WriteLine(DateTime.Now.ToString() + ": Antes de remover y abandonar la sesion.");

            Session.RemoveAll();
            Session.Abandon();

            //swsec.WriteLine(DateTime.Now.ToString() + ": Antes de hacer signout");

            FormsAuthentication.SignOut();
            //FormsAuthentication.RedirectToLoginPage();

            /// Aqui comienza el codigo para cerrar la sesion con Sharepoint. 07/12/2017
            Session.Abandon();
            // Clear authentication cookie 
            //swsec.WriteLine(DateTime.Now.ToString() + ": Antes de limpiar la cookie...");
            Response.Cookies.Add(
                         new HttpCookie(FormsAuthentication.FormsCookieName, string.Empty)
                         {
                             Expires = DateTime.Now.AddYears(-1)
                         });
            // Clear session cookies
            //swsec.WriteLine(DateTime.Now.ToString() + ": Antes de limpiar las cookies de la sesion...");
            Response.Cookies.Add(
         new HttpCookie("ASP.NET_SessionId", string.Empty)
         {
             Expires = DateTime.Now.AddYears(-1)
         });

        }
        catch //(Exception ex)
        {
            Response.Redirect("/_layouts/15/signout.aspx");
            //swsec.WriteLine("Fecha: " + DateTime.Now.ToString());
            //swsec.WriteLine("Error en logout.Page_Load(): " + ex.Message);
            //swsec.WriteLine("InnerException: " + ex.InnerException);
        }
        finally
        {
            Response.Redirect("/_layouts/15/signout.aspx");
            //swsec.WriteLine(DateTime.Now.ToString() + ": Antes del redirect");
            //swsec.Close();
        }
    }
}
    