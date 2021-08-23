using System;
using System.Data;
using System.Configuration;
using System.Net.Mail;
using ESR.Data;

namespace ESR.Business
{
    public class Cuestionario
    {
        private int _iIdCuestionario;
        private string _sNombre;
        private string _sDescripcion;
        private int _iAnio;
        private bool _bBloqueado;
        private int _iIdTema;
        private int _iIdEmpresa;
        private string _sidUsuario;
        private DateTime _dFecha;
        private bool _bLiberado;
        private int _iIdIndicador;
        private int _iOrdinal;
        private int _iidPais;
        public int idIndicador
        {
            get { return _iIdIndicador; }
            set { _iIdIndicador = value; }
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

        public int anio
        {
            get
            {
                return _iAnio;
            }
            set
            {
                _iAnio = value;
            }
        }

        public int idCuestionario
        {
            get
            {
                return _iIdCuestionario;
            }
            set
            {
                _iIdCuestionario = value;
            }
        }

        public string descripcion
        {
            get
            {
                return _sDescripcion;
            }
            set
            {
                _sDescripcion = value;
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

        public int idPais
        {
            get { return _iidPais; }
            set { _iidPais = value; }
        }

        public string idUsuario
        {
            get { return _sidUsuario; }
            set { _sidUsuario = value; }
        }

        public DateTime fecha
        {
            get { return _dFecha; }
            set { _dFecha = value; }
        }

        public bool liberado
        {
            get
            {
                return _bLiberado;
            }
            set
            {
                _bLiberado = value;
            }
        }

        public int ordinal
        {
            get
            {
                return _iOrdinal;
            }
            set
            {
                _iOrdinal = value;
            }
        }

        public DataSet carga(bool activos)
        {
            ESR.Data.Cuestionario cuestionario = new ESR.Data.Cuestionario();
            if (activos)
            {
                return cuestionario.cargaActivos();
            }
            return cuestionario.CargaTodos();
        }


        public DataSet CargaTodos()
        {
            ESR.Data.Cuestionario cuestionarios = new ESR.Data.Cuestionario();
            return cuestionarios.CargaTodos();
        }

        public DataSet CargaCuestionarios(string strPerfiles)
        {
            ESR.Data.Cuestionario cuestionarios = new ESR.Data.Cuestionario();
            return cuestionarios.CargaCuestionarios(this.idEmpresa, this.liberado);
        }

        public DataSet Carga()
        {
            ESR.Data.Cuestionario cuestionario = new ESR.Data.Cuestionario();
            return cuestionario.Carga(this.idCuestionario, this.idEmpresa);
        }

        public DataSet CargaAvance()
        {
            ESR.Data.Cuestionario cuestionario = new ESR.Data.Cuestionario();
            return cuestionario.CargaAvance(this.idCuestionario, this.idEmpresa, true);
        }

        public DataSet CargaAvance(bool bIndicadoresExtra)
        {
            ESR.Data.Cuestionario cuestionario = new ESR.Data.Cuestionario();
            DataSet dsIndicadores = cuestionario.CargaAvance(this.idCuestionario, this.idEmpresa, bIndicadoresExtra);
            //if (idCuestionario.ToString() == ConfigurationManager.AppSettings["idMedianas"].ToString())
            //{
                //dsIndicadores.Tables["Inciso"].Rows.Clear();
            //}
            
            return dsIndicadores;
            
        }

        public DataSet CargaAvanceXTema()
        {
            ESR.Data.Cuestionario cuestionario = new ESR.Data.Cuestionario();
            return cuestionario.CargaAvanceXTema(this.idCuestionario, this.idEmpresa, this.idTema);
        }

        public bool LiberaCuestionario()
        {
            bool result = false;
            ESR.Data.Cuestionario cuestionario = new ESR.Data.Cuestionario();
            if (cuestionario.LiberaCuestionario(_iIdCuestionario, _iIdEmpresa, _sidUsuario))
            {
                // Correo al usuario
                string sFrom = "";
                string sTo = "";
                if (this.idPais != 168)
                {
                    sFrom = ConfigurationManager.AppSettings["correoFrom"].ToString();
                    sTo = ConfigurationManager.AppSettings["correoTo"].ToString();
                }
                else
                {
                    sFrom = "distintivoesr@peru2021.org";
                    sTo = "distintivoesr@peru2021.org";
                }
                MailMessage correo = new MailMessage(sFrom, _sidUsuario);
                //MailMessage correo = new MailMessage(sFrom, "mramos@kicloud.com.mx");

                correo.Body = "<p>Estimad@ " + _sidUsuario + "</p>" +
"<p>Se le env�a la presente notificaci�n debido a que ha liberado " +
"satisfactoriamente el cuestionario de Empresa Socialmente Responsable.</p>" +
"<p>Fecha de liberaci�n: " + DateTime.Now.ToString() + "</p>" +

"<p>Atentamente </p>" +
"<p>Equipo t�cnico</p>";
                if (this.idPais != 168)
                {
                    correo.Body += "<p>Distintivo ESR</p>";
                }
                else
                {
                    correo.Body += "<p>Distintivo ESR-Per�</p>";
                }
                correo.IsBodyHtml = true;
                correo.Subject = "Liberaci�n de cuestionario: " + _iIdEmpresa.ToString();
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
                    
                    MailMessage correoConfirmacion = new MailMessage(sFrom, sTo);
                    correoConfirmacion.Body = "<p>Notificaci�n de liberaci�n de cuestionario</p>" +
"<p>Se le env�a la presente notificaci�n debido a que el usuario: " + _sidUsuario + " ha liberado " +
"satisfactoriamente el cuestionario: " + _iIdCuestionario.ToString() + " de Empresa Socialmente Responsable.</p>";
                    correoConfirmacion.IsBodyHtml = true;
                    correoConfirmacion.Subject = "Liberaci�n de cuestionario, empresa: " + _iIdEmpresa.ToString();
                    correoConfirmacion.Priority = MailPriority.Normal;
                    mSmtpClient.Send(correoConfirmacion);

                    result = true;
                }
                catch (Exception ex)
                {
                    throw new Exception("Ocurri� un error en ESR.Business.Cuestionario.LiberaCuestionario()", ex);
                }
            }
            return result;
        }

        public bool GuardaCuestionario()
        {
            ESR.Data.Cuestionario cuestionario = new ESR.Data.Cuestionario();
            return cuestionario.GuardaCuestionario(_iIdCuestionario, _sNombre, _sDescripcion, _iAnio, _bBloqueado, _sidUsuario, this.fecha, _sidUsuario, _dFecha);
        }



        public DataSet TraeCuestionario()
        {
            ESR.Data.Cuestionario cuestionario = new ESR.Data.Cuestionario();
            return cuestionario.TraeIndicadores(_iIdCuestionario, _iIdTema);
        }


        public DataSet Traeidcuestionario()
        {
            ESR.Data.Cuestionario cuestionario = new ESR.Data.Cuestionario();
            return cuestionario.Traeidcuestionario(this.nombre);
        }

        public bool GuardaIndicadoresCuestionario()
        {
            ESR.Data.Cuestionario cuestionario = new ESR.Data.Cuestionario();
            return cuestionario.GuardaIndicadoresCuestionario(_iIdCuestionario,_iIdIndicador, _iIdTema, _sidUsuario, _iOrdinal);
        }

        public bool EliminaIndicadoresCuestionario()
        {
            ESR.Data.Cuestionario cuestionario = new ESR.Data.Cuestionario();
            return cuestionario.EliminaIndicadoresCuestionario(this.idCuestionario, this.idIndicador, this.idTema);
        }

        public DataSet LlenaGrid()
        {
            ESR.Data.Cuestionario cuestionario = new ESR.Data.Cuestionario();
            return cuestionario.LlenaGrid(_iIdCuestionario, _iIdTema);
        }

        public DataSet DatosCuestionario()
        {
            ESR.Data.Cuestionario cuestionario = new ESR.Data.Cuestionario();
            return cuestionario.DatosCuestionario(this.idCuestionario);
        }

        //
        public DataSet CargaCuestionariosAdmin()
        {
            ESR.Data.Cuestionario cuestionario = new ESR.Data.Cuestionario();
            return cuestionario.CargaCuestionariosAdmin();
        }
    }
}
