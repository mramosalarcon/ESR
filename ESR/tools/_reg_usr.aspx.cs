using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web.Security;
using Microsoft.SharePoint;

namespace ESR.tools
{
    public partial class _reg_usr : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCargarUsuarios_Click(object sender, EventArgs e)
        {
            StreamWriter sw = File.AppendText("d:\\temp\\usr.log");
            sw.AutoFlush = true;
            MembershipCreateStatus userStatus;
            try
            {
                string sql = "Select idUsuario from Usuario where bloqueado = 0";
                string cnnString = ConfigurationManager.ConnectionStrings["ESR"].ConnectionString;
                if (null != cnnString)
                {
                    using (SqlConnection cnn = new SqlConnection(cnnString))
                    {
                        SqlCommand cmdUsuarios = new SqlCommand(sql, cnn);
                        cmdUsuarios.CommandTimeout = 0;
                        cnn.Open();
                        SqlDataReader rdrUsuarios = cmdUsuarios.ExecuteReader();
                        if (rdrUsuarios.HasRows)
                        {
                            while (rdrUsuarios.Read())
                            {
                                // Fido
                                MembershipUser newUser = Membership.CreateUser(rdrUsuarios[0].ToString(), "!Pass1234", rdrUsuarios[0].ToString(), "YourDog", "Reus", true, out userStatus);
                                if (newUser == null)
                                {
                                    if (userStatus != MembershipCreateStatus.DuplicateUserName)
                                    {
                                        sw.WriteLine(DateTime.Now.ToString() + ": " + rdrUsuarios[0].ToString() + ": " + GetErrorMessage(userStatus));
                                    }
                                }
                            }
                        }
                    }
                    lblResult.Text = "¡ Usuarios registrados !";
                }
            }
            catch (Exception ex)
            {
                sw.WriteLine(DateTime.Now.ToString() + ": Error: " + ex.Message);
            }

            sw.Close();
        }
        public string GetErrorMessage(MembershipCreateStatus status)
        {
            switch (status)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Username already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A username for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
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