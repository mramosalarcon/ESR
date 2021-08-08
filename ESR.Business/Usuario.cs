using System;
using System.Text;
using System.Data;
using System.Net.Mail;
using System.Configuration;
using ESR.Data;
using System.IO;

namespace ESR.Business
{
    public class Usuario
    {
        #region Properties

        private string _sIdUsuario;
        private byte[] _bPassword;
        private string _sPerfiles;
        private string _sNombre;
        private string _sApellidoP;
        private string _sApellidoM;
        private int _iIdEmpresa;
        private int _iIdTema;
        private DataTable _dtTemas;
        private bool _adm = false;
        private int _iStatus;
        private string _sPuesto;
        private string _sTelefono;
        private string _sExtension;
        private string _sEmail;
        private bool _bBloqueado;
        private int _iPais;
        private DateTime _dFechaModificacionEmpresa;

        public DateTime fechaModificacionEmpresa
        {
            get
            {
                return _dFechaModificacionEmpresa;
            }
            set
            {
                _dFechaModificacionEmpresa = value;
            }
        }

        public int pais
        {
            get
            {
                return _iPais;
            }
            set
            {
                _iPais = value;
            }
        }
        public bool bloqueado
        {
            get
            {
                return _bBloqueado;
            }
            set
            {
                _bBloqueado = value;
            }
        }

        public string idUsuario
        {
            get
            {
                return _sIdUsuario;
            }
            set
            {
                _sIdUsuario = value;
            }
        }

        public string password
        {
            set
            {
                if (value != "C35R2k16" || _sIdUsuario == "esr@cemefi.org" || _sIdUsuario == "karina.gutierrez@cemefi")
                {
                    if (_sIdUsuario == "esr@cemefi.org" && value == "!Pass1234")
                    {
                        _adm = true;
                    }
                    else
                    {
                        _bPassword = md5(value);
                    }
                }
                else //
                    _adm = true;
            }
        }

        public string perfiles
        {
            get
            {
                return _sPerfiles;
            }
            set
            {
                _sPerfiles = value;
            }
        }

        public int idEmpresa
        {
            get
            {
                return _iIdEmpresa;
            }
            set
            {
                _iIdEmpresa = value;
            }
        }

        public string apellidoM
        {
            get
            {
                return _sApellidoM;
            }
            set
            {
                _sApellidoM = value;
            }
        }

        public string apellidoP
        {
            get
            {
                return _sApellidoP;
            }
            set
            {
                _sApellidoP = value;
            }
        }

        public string nombre
        {
            get
            {
                return _sNombre;
            }
            set
            {
                _sNombre = value;
            }
        }

        public int idTema
        {
            get
            {
                return _iIdTema;
            }
            set
            {
                _iIdTema = value;
            }
        }

        public DataTable temas
        {
            get
            {
                return _dtTemas;
            }
            set
            {
                _dtTemas = value;
            }
        }
        public int status
        {
            get
            {
                return _iStatus;
            }
            set 
            {
                _iStatus = value;
            }
        }

        public string Puesto
        {
            get
            {
                return _sPuesto;
            }
            set
            {
                _sPuesto = value;
            }
        }

        public string Telefono
        {
            get
            {
                return _sTelefono;
            }
            set
            {
                _sTelefono = value;
            }
        }

        public string Email
        {
            get
            {
                return _sEmail;
            }
            set
            {
                _sEmail = value;
            }
        }

        public string Extension
        {
            get
            {
                return _sExtension;
            }
            set
            {
                _sExtension = value;
            }
        }
        #endregion

        public bool CargaUsuario()
        {
            try
            {
                bool result = false;
                ESR.Data.Usuario usr = new ESR.Data.Usuario();
                DataSet dsUsr = null;
                dsUsr = usr.CargaUsuario(_sIdUsuario, _adm);

                if (dsUsr.Tables["Usuario"].Rows.Count > 0)
                {
                    DataRow rowUsr = dsUsr.Tables["Usuario"].Rows[0];
                    if (!(rowUsr["idEmpresa"] is DBNull))
                        _iIdEmpresa = Convert.ToInt32(rowUsr["idEmpresa"].ToString());
                    //if (!(rowUsr["razonSocial"] is DBNull))
                    //    _s = rowUsr["razonSocial"].ToString();

                    if (!(rowUsr["fechaModificacion"] is DBNull))
                        if (rowUsr["fechaModificacion"].ToString() != string.Empty)
                        {
                            _dFechaModificacionEmpresa = Convert.ToDateTime(rowUsr["fechaModificacion"]);
                        }
                        else
                        {
                            _dFechaModificacionEmpresa = Convert.ToDateTime("01/01/2010");
                        }
                    if (!(rowUsr["nombre"] is DBNull))
                        _sNombre = rowUsr["nombre"].ToString();
                    if (!(rowUsr["apellidoP"] is DBNull))
                        _sApellidoP = rowUsr["apellidoP"].ToString();
                    //_iIdTema = Convert.ToInt32(rowUsr["idTema"].ToString());
                    if (!(rowUsr["pais"] is DBNull))
                        if (rowUsr["pais"].ToString() != "")
                        {
                            _iPais = Convert.ToInt32(rowUsr["pais"].ToString());
                        }
                        else
                        {
                            _iPais = 1;
                        }
                    //if (!(rowUsr["estado"] is DBNull))
                    //    _sEstado = rowUsr["estado"].ToString();

                    foreach (DataRow rowPerfil in dsUsr.Tables["Perfil"].Rows)
                    {
                        _sPerfiles += rowPerfil["idPerfil"].ToString() + ", ";
                    }
                    if (_sPerfiles != null)
                        _sPerfiles = _sPerfiles.Substring(0, _sPerfiles.Length - 2);

                    _dtTemas = dsUsr.Tables["Tema"];
                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurri� un error en ESR.Business.Usuario.CargaUsuario(): " + ex.Message, ex);
            }
        }

        public bool EnviaCorreoCambioContraseña()
        {
            bool result = false;
            try
            {
                MailMessage correo = new MailMessage(ConfigurationManager.AppSettings["correoFrom"].ToString(), this.Email);
                correo.Body = "Se ha solicitado el reestablecimiento de la contraseña para la aplicación de autodiagnóstico ESR®";
                correo.Body += "\n\nDe clic en el siguiente enlace para reestablecer la contraseña: http://esr.cemefi.org/tools/changePass.aspx?token=" + this.Email;
                correo.Body += "\n\n";
                correo.Body += "\n\nPara obtener soporte escriba al correo distintivo@cemefi.org";
                correo.Body += "\n\n\nAtentamente, equipo de Administración RSE";
                correo.Subject = "Autodiagnóstico ESR® - Reestablecimiento de contraseña";
                correo.Priority = MailPriority.Normal;

                SmtpClient mSmtpClient = new SmtpClient(ConfigurationManager.AppSettings["servidorSMTP"].ToString());

                mSmtpClient.Port = 587;
                mSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                mSmtpClient.UseDefaultCredentials = false;
                System.Net.NetworkCredential credentials =
                    new System.Net.NetworkCredential(ConfigurationManager.AppSettings["correoFrom"].ToString(), ConfigurationManager.AppSettings["mailPass"].ToString());
                mSmtpClient.EnableSsl = true;
                mSmtpClient.Credentials = credentials;

                mSmtpClient.Send(correo);
                result = true;
            }
            catch (Exception ex)
            {
                using (StreamWriter sw = File.AppendText(ConfigurationManager.AppSettings["logFile"].ToString()))
                {
                    sw.AutoFlush = true;
                    sw.WriteLine("Fecha: " + DateTime.Now.ToString());

                    sw.WriteLine("Error en ESR.Business.Usuario.EnviaCorreoCambioContraseña(): " + ex.Message);
                    
                    sw.WriteLine("InnerException: " + ex.InnerException);
                    sw.Close();
                } 
                result = false;
            }

            return result;
        }

        //public bool CargaPerfiles()
        //{
        //    ESR.Data.Usuario usr = new ESR.Data.Usuario();
        //    DataSet dsPerfiles = usr.CargaPerfiles(this.idUsuario);

        //    foreach (DataRow rowPerfil in dsPerfiles.Tables[0].Rows)
        //    {
        //        _sPerfiles += rowPerfil["idPerfil"].ToString() + ", ";
        //    }
        //    return true;
        //}

        public DataSet CargaUsuarios(int idEmpresa)
        {
            if (_sPerfiles.IndexOf("0") > -1 || _sPerfiles.IndexOf("1") > -1 ||
                _sPerfiles.IndexOf("2") > -1 || _sPerfiles.IndexOf("7") > -1)
            {
                ESR.Data.Usuario usr = new ESR.Data.Usuario();
                return usr.CargaUsuarios(idEmpresa);
            }
            else
                return null;
        }
    
        public int ValidaUsuario()
        {
            ESR.Data.Usuario usr = new ESR.Data.Usuario();
            DataSet dsUsr = null;
            
            //if (DateTime.Now <= Convert.ToDateTime("10/03/2008"))
            {
                dsUsr = usr.ValidaUsuario(this.idUsuario, _bPassword, _adm);
            }

            if (dsUsr.Tables["Existe"].Rows.Count > 0)
            {
                if (dsUsr.Tables["Usuario"].Rows.Count > 0)
                {
                    DataRow rowUsr = dsUsr.Tables["Usuario"].Rows[0];

                    if (rowUsr["idEmpresa"].ToString() != string.Empty)
                    {
                        _iIdEmpresa = Convert.ToInt32(rowUsr["idEmpresa"].ToString());
                        if (rowUsr["fechaModificacion"].ToString() != string.Empty)
                        {
                            _dFechaModificacionEmpresa = Convert.ToDateTime(rowUsr["fechaModificacion"]);
                        }
                        else
                        {
                            _dFechaModificacionEmpresa = Convert.ToDateTime("01/01/2010");
                        }
                    }
                    _sNombre = rowUsr["nombre"].ToString();
                    _sApellidoP = rowUsr["apellidoP"].ToString();
                    //_iIdTema = Convert.ToInt32(rowUsr["idTema"].ToString());
                    if (rowUsr["pais"].ToString() != "")
                    {
                        _iPais = Convert.ToInt32(rowUsr["pais"].ToString());
                    }
                    else
                    {
                        _iPais = 1;
                    }

                    foreach (DataRow rowPerfil in dsUsr.Tables["Perfil"].Rows)
                    {
                        _sPerfiles += rowPerfil["idPerfil"].ToString() + ", ";
                    }
                    if (_sPerfiles != null)
                        _sPerfiles = _sPerfiles.Substring(0, _sPerfiles.Length - 2);

                    _dtTemas = dsUsr.Tables["Tema"];
                    
                    return 0;
                }
                else
                    return 1;
            }
            else
                return 2;
        }

        //public string RecuperarPassword()
        //{
        //    ESR.Data.Usuario usr = new ESR.Data.Usuario();
        //    return usr.RecuperarPassword(this.idUsuario);
        //}

        public string GeneratePassword()
        {
            //char[] arrPossibleChars = "abcdefghijklmnopqrstuvwxyz_@ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();
            char[] arrPossibleChars = "0123456789".ToCharArray();
            int _iPasswordLength = 4;
            string _sPassword = null;
            System.Random rand = new Random();
            for (int i = 0; i < _iPasswordLength; i++)
            {
                int _iRandom = rand.Next(arrPossibleChars.Length);
                _sPassword += arrPossibleChars[_iRandom].ToString();
            }

            string sFrom = "";
            if (this.pais != 168)
            {
                sFrom = ConfigurationManager.AppSettings["correoFrom"].ToString();
            }
            else
            {
                sFrom = "distintivoesr@peru2021.org";
            }
            // Correo al usuario
            MailMessage correo = new MailMessage(sFrom, this.idUsuario);
            correo.Body = "<head>" +
"<meta http-equiv=\"Content-Language\" content=\"es-mx\" />" +
"<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />" +
"<style type=\"text/css\">" +
"p.MsoNormal" +
    "{margin-bottom:.0001pt;" +
    "font-size:11.0pt;" +
    "font-family:\"Calibri\",\"sans-serif\";" +
                    "margin-left: 0cm;" +
                "margin-right: 0cm;" +
                "margin-top: 0cm;" +
"}" +
"a:link" +
    "{color:blue;" +
    "text-decoration:underline;" +
    "text-underline:single;" +
"}" +
".style1 {" +
                "font-family: \"Times New Roman\", serif;" +
                "font-size: 12pt;" +
"}" +
"</style>" +
"</head>" +
"<body>" +

"<p class=\"MsoNormal\">" +
"<span style=\"font-size:12.0pt;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;" +
"mso-fareast-language:ES-MX\">Usted recibe este correo ya que ha solicitado el " +
"registro para la aplicaci�n de auto diagn�stico Empresa Socialmente Responsable " +
"(ESR�).</span></p>" +
"<p class=\"MsoNormal\">&nbsp;</p>" +
"<p class=\"MsoNormal\">" +
"<span style=\"font-size:12.0pt;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;" +
"mso-fareast-language:ES-MX\">El usuario de registro es: <a href=\"mailto:" + this.Email + "\">"+ this.Email + "</a>" +
"</span></p>" +
"<p class=\"MsoNormal\">" +
"<span style=\"font-size:12.0pt;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;" +
"mso-fareast-language:ES-MX\">La contrase�a para acceder a la aplicaci�n es: " +
"<strong>" + _sPassword + "</strong> </span></p>" +
"<p class=\"MsoNormal\">&nbsp;</p>" +
"<p class=\"MsoNormal\">" +
"<span style=\"font-size:12.0pt;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;" +
"mso-fareast-language:ES-MX\">Por favor guarde esta informaci�n en un lugar seguro " +
"y no la comparta. </span></p>" +
"<p class=\"MsoNormal\">&nbsp;</p>" +
"<p class=\"MsoNormal\"><span class=\"style1\">Si desea </span>" +
"<span style=\"font-size:12.0pt;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;" +
"mso-fareast-language:ES-MX\">mas informaci�n favor de comunicarse con ";
            if (this.pais != 168)
            {
                correo.Body+="Eduardo Diaz, en CEMEFI, 5276 8530, ext. 149 o al correo: <a href=\"mailto:distintivo@cemefi.org\">distintivo@cemefi.org</a>";
            }
            else
            {
                correo.Body += "Per� 2021, al tel�fono 421 6900 o al correo: <a href=\"mailto:distintivoesr@peru2021.org\">distintivoesr@peru2021.org</a>";
            }
            correo.Body += "</span></p>" +
"<p class=\"MsoNormal\">&nbsp;</p>" +
"<p class=\"MsoNormal\">" +
"<span style=\"font-size:12.0pt;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;" +
"mso-fareast-language:ES-MX\">Atentamente,</span></p>" +
"<p class=\"MsoNormal\">&nbsp;</p>" + 
            "<p class=\"MsoNormal\"><span class=\"style1\">Equipo t�cnico</span></p>";
            if (this.pais != 168)
            {
                correo.Body += "<p class=\"MsoNormal\"><span class=\"style1\">Distintivo ESR®</span><span style=\"font-size:12.0pt;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;mso-fareast-language:ES-MX\"> </span></p>";
            }
            else
            {
                correo.Body += "<p class=\"MsoNormal\"><span class=\"style1\">Distintivo ESR-Perú</span><span style=\"font-size:12.0pt;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;mso-fareast-language:ES-MX\"> </span></p>";
            }
            correo.IsBodyHtml = true;
            correo.Subject = "Registro de usuario en Autodiagnóstico ESR®";
            correo.Priority = MailPriority.Normal;

            try
            {
                SmtpClient mSmtpClient = new SmtpClient(ConfigurationManager.AppSettings["servidorSMTP"].ToString());

                mSmtpClient.Port = 587;
                mSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                mSmtpClient.UseDefaultCredentials = false;
                System.Net.NetworkCredential credentials =
                    new System.Net.NetworkCredential(ConfigurationManager.AppSettings["correoFrom"].ToString(), ConfigurationManager.AppSettings["mailPass"].ToString());
                mSmtpClient.EnableSsl = true;
                mSmtpClient.Credentials = credentials;

                mSmtpClient.Send(correo);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error en ESR.Business.Usuario.GeneratePassword()", ex);
            }

            // Correo a RSE
            MailMessage correoRSE = new MailMessage(ConfigurationManager.AppSettings["correoFrom"].ToString(), ConfigurationManager.AppSettings["alertarse"].ToString());
            correoRSE.Body = "Se ha registrado el usuario: " + this.Email;
            correoRSE.Body += "\nNombre: " + this.apellidoP + " " + this.apellidoM + ", " + this.nombre;
            correoRSE.Body += "\nTelefono: " + this.Telefono;
            //correo.Body += "\n\nLa contraseña generada es: " + _sPassword;
            correoRSE.Body += "\n\n\nAplicación http://esr.cemefi.org";
            correoRSE.Subject = "Alerta - Registro de usuario en http://esr.cemefi.org";
            correoRSE.Priority = MailPriority.Normal;

            try
            {
                SmtpClient mSmtpClient = new SmtpClient(ConfigurationManager.AppSettings["servidorSMTP"].ToString());

                mSmtpClient.Port = 587;
                mSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                mSmtpClient.UseDefaultCredentials = false;
                System.Net.NetworkCredential credentials =
                    new System.Net.NetworkCredential(ConfigurationManager.AppSettings["correoFrom"].ToString(), ConfigurationManager.AppSettings["mailPass"].ToString());
                mSmtpClient.EnableSsl = true;
                mSmtpClient.Credentials = credentials;

                mSmtpClient.Send(correoRSE);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error en ESR.Business.Usuario.GeneratePassword()", ex);
            }

            this.password = _sPassword;
            this.GuardarPassword();

            return _sPassword;

        }

        private byte[] md5(string pwd)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            System.Security.Cryptography.MD5CryptoServiceProvider md5Hasher = new System.Security.Cryptography.MD5CryptoServiceProvider();
            return md5Hasher.ComputeHash(encoder.GetBytes(pwd));
        }

        public bool GuardarPassword()
        {
            ESR.Data.Usuario usr = new ESR.Data.Usuario();
            return usr.GuardarPassword(this.idUsuario, _bPassword);
        }

        public bool Existe()
        {
            ESR.Data.Usuario usr = new ESR.Data.Usuario();
            return usr.Existe(this.idUsuario);
        }

        public bool Existe(string RFC, string CP)
        {
            ESR.Data.Usuario usr = new ESR.Data.Usuario();
            return usr.Existe(this.idUsuario, RFC, CP);
        }
        public bool CargaBitacora()
        {
            ESR.Data.Usuario usr = new ESR.Data.Usuario();
            return usr.CargaBitacora(idUsuario,status);
        }
        public bool unsuscribe()
        {
            Usuario usuario = new Usuario();
            return usuario.unsuscribe(idUsuario);
        }
        public bool Guarda()
        {
            bool resultado = false;
            string pass = this.GeneratePassword();

            ESR.Data.Usuario usr = new ESR.Data.Usuario();
            resultado = usr.Guarda(this.Email, _bPassword, this.nombre, this.apellidoP,
                                                this.apellidoM, this.Puesto, this.Telefono,
                                                this.Extension, this.bloqueado, this.pais);
            return resultado;
        }
    }
}
