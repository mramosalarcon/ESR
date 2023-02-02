using System;
using System.Configuration;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using ESR.Data;

namespace ESR.Business
{
    public class Contacto
    {
        #region Properties
        private string _sNombre;
        private string _sApellidoP;
        private string _sApellidoM;
        private string _sPuesto;
        private string _sTelefono;
        private string _sEmail;
        private string _sExtension;
        private byte[] _bPwd;
        private int _iidEmpresa;
        private DataTable _dtPerfiles;
        private int _iIdTema;
        private int _iIdPerfil;
        private DataTable _dtTemas;
        private string _sIdUsuarioModificacion;

        public string idUsuarioModificacion
        {
            get
            {
                return _sIdUsuarioModificacion;
            }
            set
            {
                _sIdUsuarioModificacion = value;
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

        public int idPerfil
        {
            get
            {
                return _iIdPerfil;
            }
            set
            {
                _iIdPerfil = value;
            }
        }
        public string ApellidoM
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

        public string ApellidoP
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

        public string Pwd
        {
            set
            {
                _bPwd = md5(value);
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

        public int idEmpresa
        {
            get
            {
                return _iidEmpresa;
            }
            set
            {
                _iidEmpresa = value;
            }
        }

        public DataTable perfiles
        {
            get
            {
                return _dtPerfiles;
            }
            set
            {
                _dtPerfiles = value;
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
        private bool _bPrimario;

        public bool primario
        {
            get
            {
                return _bPrimario;
            }
            set
            {
                _bPrimario = value;
            }
        }
        #endregion

        public bool Elimina(string idUsuario)
        {
            bool resultado = false;
            try
            {
                ESR.Data.Contacto contact = new ESR.Data.Contacto();
                resultado = contact.Elimina(idUsuario);
                return resultado;
            }
            catch
            {
                return false;
            }
        }

        public bool Carga(string idUsuario)
        {
            try
            {
                ESR.Data.Contacto contact = new ESR.Data.Contacto();
                DataSet dsContacto = contact.Carga(idUsuario, this.idEmpresa);

                foreach (DataRow drContacto in dsContacto.Tables["Usuario"].Rows)
                {
                    this.nombre = drContacto["nombre"].ToString();
                    this.ApellidoP = drContacto["apellidoP"].ToString();
                    this.ApellidoM = drContacto["apellidoM"].ToString();
                    this.Puesto = drContacto["puesto"].ToString();
                    this.Telefono = drContacto["telefono"].ToString();
                    this.Extension = drContacto["extension"].ToString();
                    this.Email = idUsuario;
                    this.primario = Convert.ToBoolean(drContacto["contacto"].ToString());
                    this.bloqueado = Convert.ToBoolean(drContacto["bloqueado"].ToString());

                    this.perfiles = dsContacto.Tables["Perfil"];
                    this.temas = dsContacto.Tables["Tema"];   
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool GuardaContacto()
        {
            bool resultado = false;
            ESR.Data.Contacto contact = new ESR.Data.Contacto();
            resultado = contact.GuardaContacto(this.Email, _bPwd, this.idEmpresa, this.nombre, this.ApellidoP, 
                                                this.ApellidoM, this.Puesto, this.Telefono, 
                                                this.Extension, this.primario, this.bloqueado, this.idUsuarioModificacion);
            return resultado;
        }
        
        public bool Guarda()
        {
            bool resultado = false;
            ESR.Data.Contacto contact = new ESR.Data.Contacto();
            resultado = contact.GuardaContacto(this.Email, _bPwd, this.idEmpresa, this.nombre, this.ApellidoP,
                                                this.ApellidoM, this.Puesto, this.Telefono,
                                                this.Extension, this.primario, this.bloqueado, this.idUsuarioModificacion);
            return resultado;
        }

        public bool Existe()
        {
            ESR.Data.Contacto contact = new ESR.Data.Contacto();
            return contact.Existe(this.Email);
        }

        public void GeneratePassword()
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

            MailMessage correo = new MailMessage(ConfigurationManager.AppSettings["correoFrom"].ToString(), this.Email);
            correo.Body = "Se concluyó con éxito el registro para la aplicación de evaluación del Distintivo ESR®, \nel usuario registrado es: " + this.Email;
            correo.Body += "\n\nLa contraseña para acceder a la aplicación es: " + _sPassword;
            correo.Body += "\n\nPor favor guarde esta información en un lugar seguro y no la comparta con nadie. La contraseña es personal e intransferible.";
            correo.Body += "\n\nPara mayor información favor de comunicarse con el área de Acreditación RSE al teléfono 5552768530 a las siguientes extensiones:: ";
            correo.Body += "\n\n - Ingrid Monter, ext. 182 o al correo serviciosrse@cemefi.org";
            correo.Body += "\n\n - Victor González, ext. 156 o al correo victor.gonzalez@cemefi.org";
            correo.Body += "\n\n - Miguel Cordero, al correo miguel.cordero@cemefi.org";
            correo.Body += "\n\n - <b>Responsable de acreditación RSE</b>, Miriam Ortega ext. 128 o al correo miriam.ortega@cemefi.org";

            correo.Body += "\n\n\nAtentamente, \nequipo de administración del Distintivo ESR®.";
            correo.Subject = "Generación de contraseña (Distintivo ESR®)";
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

            _bPwd = md5(_sPassword);
        }

        private byte[] md5(string pwd)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            System.Security.Cryptography.MD5CryptoServiceProvider md5Hasher = new System.Security.Cryptography.MD5CryptoServiceProvider();
            return md5Hasher.ComputeHash(encoder.GetBytes(pwd));
        }

        private bool _bBloqueado;

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
        public bool EliminaTemas()
        {
            ESR.Data.Contacto contacto = new ESR.Data.Contacto();
            return contacto.EliminaTemas(this.idEmpresa, this.Email);
        }

        public bool GuardaTema()
        {
            ESR.Data.Contacto contact = new ESR.Data.Contacto();
            return contact.GuardaTema(this.idEmpresa, this.Email, this.idTema);
        }

        public bool GuardaTemas()
        {
            bool result = false;

            ESR.Data.Contacto contact = new ESR.Data.Contacto();
            if (this.EliminaTemas())
            {
                result = contact.GuardaTemas(this.idEmpresa, this.Email);
            }
            return result;
        }

        public bool GuardaEmpresa()
        {
            bool result = false;

            ESR.Data.Contacto contact = new ESR.Data.Contacto();
            if (contact.GuardaEmpresa(this.idEmpresa, this.Email))
            {
                if (GuardaPerfil())
                {
                    if (this.GuardaTemas())
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        public bool GuardaPerfil()
        {
            ESR.Data.Contacto contact = new ESR.Data.Contacto();
            return contact.GuardaPerfil(this.Email, this.idPerfil);
        }

        
    }
}
