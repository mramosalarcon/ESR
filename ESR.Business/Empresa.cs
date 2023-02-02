using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using ESR.Data;

namespace ESR.Business
{
	public class Empresa
	{
		private string _sRFC;

		private string _sNombre;

		private string _sNombreCorto;

		private string _sRazonSocial;

		private string _sDomicilioPrincipal;

		private string _sColonia;

		private string _sCiudad;

		private string _sEstado;

		private string _sCP;

		private int _iPais;

		private string _sSiglas;

		private string _sTelefono;

		private string _sFax;

		private string _sEmail;

		private string _sWebSite;

		private byte[] _arrLogo;

		private string _sDirector;

		private string _sSector;

		private string _sSubsector;

		private string _sPresidente;

		private string _sTamano;

		private string _sComposicionK;

		private string _sPostulante;

		private int _iNoEmpleados;

		private string _sProducto;

		private int _iAnoInicio;

		private string _sProductos;

		private string _sRSE;

		private string _sFundacion;

		private string _sAmbito;

		private int _iidEmpresa;

		private bool _bAutorizada;

		private string _sContacto;

		private string _sIdUsuario;

		private int _iIdCuestionario;

		private bool _bLiberado;

		private DateTime _fechaLimite;

		private int _iCadenaDeValor;

		public int cadenaDeValor
		{
			get
			{
				return _iCadenaDeValor;
			}
			set
			{
				_iCadenaDeValor = value;
			}
		}

		public DateTime fechaLimite
		{
			get
			{
				return _fechaLimite;
			}
			set
			{
				_fechaLimite = value;
			}
		}

		public byte[] Logo
		{
			get
			{
				return _arrLogo;
			}
			set
			{
				_arrLogo = value;
			}
		}

		public int AnoInicio
		{
			get
			{
				return _iAnoInicio;
			}
			set
			{
				_iAnoInicio = value;
			}
		}

		public string Estado
		{
			get
			{
				return _sEstado;
			}
			set
			{
				_sEstado = value;
			}
		}

		public string postulante
		{
			get
			{
				return _sPostulante;
			}
			set
			{
				_sPostulante = value;
			}
		}

		public int NoEmpleados
		{
			get
			{
				return _iNoEmpleados;
			}
			set
			{
				_iNoEmpleados = value;
			}
		}

		public int Pais
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

		public string Ambito
		{
			get
			{
				return _sAmbito;
			}
			set
			{
				_sAmbito = value;
			}
		}

		public string Ciudad
		{
			get
			{
				return _sCiudad;
			}
			set
			{
				_sCiudad = value;
			}
		}

		public string Colonia
		{
			get
			{
				return _sColonia;
			}
			set
			{
				_sColonia = value;
			}
		}

		public string ComposicionK
		{
			get
			{
				return _sComposicionK;
			}
			set
			{
				_sComposicionK = value;
			}
		}

		public string CP
		{
			get
			{
				return _sCP;
			}
			set
			{
				_sCP = value;
			}
		}

		public string Director
		{
			get
			{
				return _sDirector;
			}
			set
			{
				_sDirector = value;
			}
		}

		public string Domicilio
		{
			get
			{
				return _sDomicilioPrincipal;
			}
			set
			{
				_sDomicilioPrincipal = value;
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

		public string Fax
		{
			get
			{
				return _sFax;
			}
			set
			{
				_sFax = value;
			}
		}

		public string Fundacion
		{
			get
			{
				return _sFundacion;
			}
			set
			{
				_sFundacion = value;
			}
		}

		public string Presidente
		{
			get
			{
				return _sPresidente;
			}
			set
			{
				_sPresidente = value;
			}
		}

		public string Producto
		{
			get
			{
				return _sProducto;
			}
			set
			{
				_sProducto = value;
			}
		}

		public string Productos
		{
			get
			{
				return _sProductos;
			}
			set
			{
				_sProductos = value;
			}
		}

		public string RazonSocial
		{
			get
			{
				return _sRazonSocial;
			}
			set
			{
				_sRazonSocial = value;
			}
		}

		public string RSE
		{
			get
			{
				return _sRSE;
			}
			set
			{
				_sRSE = value;
			}
		}

		public string Sector
		{
			get
			{
				return _sSector;
			}
			set
			{
				_sSector = value;
			}
		}

		public string Subsector
		{
			get
			{
				return _sSubsector;
			}
			set
			{
				_sSubsector = value;
			}
		}

		public string Siglas
		{
			get
			{
				return _sSiglas;
			}
			set
			{
				_sSiglas = value;
			}
		}

		public string Tamano
		{
			get
			{
				return _sTamano;
			}
			set
			{
				_sTamano = value;
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

		public string WebSite
		{
			get
			{
				return _sWebSite;
			}
			set
			{
				_sWebSite = value;
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

		public string RFC
		{
			get
			{
				return _sRFC;
			}
			set
			{
				_sRFC = value;
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

		public string nombreCorto
		{
			get
			{
				return _sNombreCorto;
			}
			set
			{
				_sNombreCorto = value;
			}
		}

		public bool autorizada
		{
			get
			{
				return _bAutorizada;
			}
			set
			{
				_bAutorizada = value;
			}
		}

		public string contacto
		{
			get
			{
				return _sContacto;
			}
			set
			{
				_sContacto = value;
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

		public bool PagoCuestionario(bool pago)
		{
			ESR.Data.Empresa empresa = new ESR.Data.Empresa();
			return empresa.PagoCuestionario(_iidEmpresa, _iIdCuestionario, pago);
		}

		public bool Notificar(string tipoSolicitud)
		{
			bool result = false;
			switch (tipoSolicitud)
			{
				case "VINCULO":
					{
						ESR.Data.Empresa empresa = new ESR.Data.Empresa();
						_sNombre = empresa.NotificarVinculo(_iidEmpresa, _sIdUsuario);
						if (_sNombre != string.Empty)
						{
							string text = empresa.CargaResponsable(_iidEmpresa);
							if (text != string.Empty && text != null)
							{
								MailMessage mailMessage = new MailMessage(ConfigurationManager.AppSettings["correoFrom"].ToString(), text);
								mailMessage.Body = "Se le envía este correo debido a que el usuario " + _sIdUsuario;
								string body = mailMessage.Body;
								mailMessage.Body = body + " ha solicitado la vinculación a la empresa: " + _iidEmpresa.ToString() + " - " + _sNombre + ", para acceder al Distintivo Empresa Socialmente Responsable®";
								mailMessage.Body += "\n\nPor favor acceda a la aplicación en https://esrv1.cemefi.org para autorizar al usuario y asignarle permisos para el cuestionario";
								mailMessage.Body += "\n\n\nAtentamente";
								mailMessage.Body += "\nResponsabilidad Social Empresarial";
								mailMessage.Body += "\nEquipo de administraci�n RSE";
								mailMessage.Subject = "ESR® alerta de vínculo, " + _sNombre;
								mailMessage.Priority = MailPriority.Normal;
								SmtpClient smtpClient = new SmtpClient(ConfigurationManager.AppSettings["servidorSMTP"].ToString());
								smtpClient.Port = 587;
								smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
								smtpClient.UseDefaultCredentials = false;
								NetworkCredential credentials = new NetworkCredential(ConfigurationManager.AppSettings["correoFrom"].ToString(), ConfigurationManager.AppSettings["mailPass"].ToString());
								smtpClient.EnableSsl = true;
								smtpClient.Credentials = credentials;
								smtpClient.Send(mailMessage);
							}
						}
						else
						{
							MailMessage mailMessage2 = new MailMessage(ConfigurationManager.AppSettings["correoFrom"].ToString(), "mramos@kicloud.com.mx");
							mailMessage2.Body = "Se le envía este correo debido a que el usuario " + _sIdUsuario;
							mailMessage2.Body = mailMessage2.Body + " ha solicitado la vinculación a la empresa: " + _iidEmpresa.ToString();
							mailMessage2.Body += "\n\nPor favor verifique, porque no aparece el nombre.";
							mailMessage2.Body += "\n\n\nAtentamente";
							mailMessage2.Body += "\nesrv1.cemefi.org";
							mailMessage2.Subject = "Problema con empresa, " + _iidEmpresa.ToString();
							mailMessage2.Priority = MailPriority.Normal;
							SmtpClient smtpClient2 = new SmtpClient(ConfigurationManager.AppSettings["servidorSMTP"].ToString());
							smtpClient2.Port = 587;
							smtpClient2.DeliveryMethod = SmtpDeliveryMethod.Network;
							smtpClient2.UseDefaultCredentials = false;
							NetworkCredential credentials2 = new NetworkCredential(ConfigurationManager.AppSettings["correoFrom"].ToString(), ConfigurationManager.AppSettings["mailPass"].ToString());
							smtpClient2.EnableSsl = true;
							smtpClient2.Credentials = credentials2;
							smtpClient2.Send(mailMessage2);
						}
						break;
					}
			}
			return result;
		}

		public bool VincularUsuario()
		{
			bool result = false;
			ESR.Data.Empresa empresa = new ESR.Data.Empresa();
			if (empresa.VincularUsuario(_iidEmpresa, _sIdUsuario))
			{
				result = Notificar("VINCULO");
			}
			return result;
		}

		public bool AceptarVinculo()
		{
			bool result = false;
			ESR.Data.Empresa empresa = new ESR.Data.Empresa();
			if (empresa.AceptarVinculo(_iidEmpresa, _sIdUsuario, 8))
			{
				result = Notificar("VINCULO ACEPTADO");
			}
			return result;
		}

		public bool RechazarVinculo()
		{
			bool result = false;
			ESR.Data.Empresa empresa = new ESR.Data.Empresa();
			if (empresa.RechazarVinculo(_iidEmpresa, _sIdUsuario))
			{
				result = Notificar("VINCULO RECHAZADO");
			}
			return result;
		}

		public bool Guardar()
		{
			bool result = true;
			int num = 0;
			try
			{
				ESR.Data.Empresa empresa = new ESR.Data.Empresa();
				num = empresa.Guardar(idEmpresa, RFC, nombre, nombreCorto, RazonSocial, Domicilio, Colonia, Ciudad, Estado, CP, Pais, Siglas, Telefono, Fax, Email, WebSite, Logo, Director, Presidente, Tamano, Sector, Subsector, ComposicionK, postulante, NoEmpleados, Producto, AnoInicio, Productos, RSE, Fundacion, Ambito, autorizada, idUsuario, cadenaDeValor);
				if (num != -1)
				{
					if (idEmpresa != 0)
					{
						ESR.Business.Empresa empresa2 = new ESR.Business.Empresa();
						MailMessage mailMessage = new MailMessage(ConfigurationManager.AppSettings["correoFrom"].ToString(), ConfigurationManager.AppSettings["alertarse"].ToString());
						mailMessage.Body = "Se ha registrado la empresa: " + nombre;
						mailMessage.Body = mailMessage.Body + "\nNombre corto: " + nombreCorto;
						mailMessage.Body = mailMessage.Body + "\nCiudad: " + Ciudad;
						mailMessage.Body = mailMessage.Body + "\nEstado: " + Estado;
						mailMessage.Body = mailMessage.Body + "\nTama~o: " + Tamano;
						mailMessage.Body = mailMessage.Body + "\nSector: " + Sector;
						mailMessage.Body = mailMessage.Body + "\nSubsector: " + Subsector;
						mailMessage.Body = mailMessage.Body + "\nComposicion de capital: " + ComposicionK;
						mailMessage.Body = mailMessage.Body + "\nTelefono: " + Telefono;
						if (cadenaDeValor != 0)
						{
							empresa2.idEmpresa = cadenaDeValor;
							empresa2.Cargar();
							string body = mailMessage.Body;
							mailMessage.Body = body + "\nSe ha inscrito como cadena de valor de: " + empresa2.nombre + "-" + empresa2.nombreCorto;
						}
						mailMessage.Body += "\n\n\nAplicacion https://esrv1.cemefi.org";
						mailMessage.Subject = "Alerta - Registro de empresa en https://esrv1.cemefi.org";
						mailMessage.Priority = MailPriority.Normal;
						SmtpClient smtpClient = new SmtpClient(ConfigurationManager.AppSettings["servidorSMTP"].ToString());
						smtpClient.Port = 587;
						smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
						smtpClient.UseDefaultCredentials = false;
						NetworkCredential credentials = new NetworkCredential(ConfigurationManager.AppSettings["correoFrom"].ToString(), ConfigurationManager.AppSettings["mailPass"].ToString());
						smtpClient.EnableSsl = true;
						smtpClient.Credentials = credentials;
						smtpClient.Send(mailMessage);
						if (cadenaDeValor != 0)
						{
							MailMessage mailMessage2 = new MailMessage(ConfigurationManager.AppSettings["correoFrom"].ToString(), "ayudarse@cemefi.org");
							mailMessage2.Body = "Se ha registrado la empresa: " + nombre;
							mailMessage2.Body = mailMessage2.Body + "\nNombre corto: " + nombreCorto;
							mailMessage2.Body = mailMessage2.Body + "\nCiudad: " + Ciudad;
							mailMessage2.Body = mailMessage2.Body + "\nEstado: " + Estado;
							mailMessage2.Body = mailMessage2.Body + "\nTama~o: " + Tamano;
							mailMessage2.Body = mailMessage2.Body + "\nSector: " + Sector;
							mailMessage2.Body = mailMessage2.Body + "\nSubsector: " + Subsector;
							mailMessage2.Body = mailMessage2.Body + "\nComposicion de capital: " + ComposicionK;
							mailMessage2.Body = mailMessage2.Body + "\nTelefono: " + Telefono;
							string body2 = mailMessage2.Body;
							mailMessage2.Body = body2 + "\nY ha solicitado incribirse como cadena de valor de su empresa: " + empresa2.nombre + "-" + empresa2.nombreCorto;
							mailMessage2.Body += "\n\nEn caso de aprobar esta solicitud, por favor pongase en contacto el área de Acreditación RSE al teléfono 5552768530 a las siguientes extensiones::";
							mailMessage2.Body += "\n\n - Ingrid Monter, ext. 182 o al correo serviciosrse@cemefi.org";
							mailMessage2.Body += "\n\n - Victor González, ext. 156 o al correo victor.gonzalez@cemefi.org";
							mailMessage2.Body += "\n\n - Miguel Cordero, al correo miguel.cordero@cemefi.org";
							mailMessage2.Body += "\n\n - <b>Responsable de acreditación RSE</b>, Miriam Ortega ext. 128 o al correo miriam.ortega@cemefi.org";
							mailMessage2.Body += "\n\n\nAplicacion https://esrv1.cemefi.org";
							mailMessage2.Subject = "Solicitud de autorización para registro en Cadena de Valor ESR";
							mailMessage2.Priority = MailPriority.Normal;
							smtpClient.Send(mailMessage2);
						}
					}
					else
					{
						idEmpresa = num;
					}
				}
				else
				{
					result = false;
				}
				return result;
			}
			catch (Exception ex)
			{
				throw new Exception("ESR.Business.Empresa.Guardar(): " + ex.Message);
			}
		}

		public bool cargaNombre()
		{
			bool result = false;
			try
			{
				ESR.Data.Empresa empresa = new ESR.Data.Empresa();
				DataSet dataSet = empresa.cargaNombre(idEmpresa);
				if (dataSet.Tables[0].Rows.Count > 0)
				{
					DataRow dataRow = dataSet.Tables[0].Rows[0];
					nombre = dataRow["nombre"].ToString();
					nombreCorto = dataRow["nombreCorto"].ToString();
					result = true;
				}
				return result;
			}
			catch (Exception innerException)
			{
				throw new Exception("Ha ocurrido un error en ESR.Business.Empresa.Cargar().", innerException);
			}
		}

		public bool Cargar()
		{
			bool result = false;
			try
			{
				ESR.Data.Empresa empresa = new ESR.Data.Empresa();
				DataSet dataSet = empresa.Cargar(idEmpresa);
				if (dataSet.Tables[0].Rows.Count > 0)
				{
					DataRow dataRow = dataSet.Tables[0].Rows[0];
					RFC = dataRow["rfc"].ToString();
					nombre = dataRow["nombre"].ToString();
					nombreCorto = dataRow["nombreCorto"].ToString();
					RazonSocial = dataRow["razonSocial"].ToString();
					Domicilio = dataRow["domicilio"].ToString();
					Colonia = dataRow["colonia"].ToString();
					Ciudad = dataRow["ciudad"].ToString();
					Estado = dataRow["estado"].ToString();
					CP = dataRow["cp"].ToString();
					Pais = Convert.ToInt32(dataRow["idPais"]);
					Siglas = dataRow["siglas"].ToString();
					Telefono = dataRow["telefono"].ToString();
					Fax = dataRow["fax"].ToString();
					Email = dataRow["email"].ToString();
					WebSite = dataRow["webSite"].ToString();
					Director = dataRow["director"].ToString();
					Presidente = dataRow["presidente"].ToString();
					Tamano = dataRow["tamano"].ToString();
					Sector = dataRow["sector"].ToString();
					Subsector = dataRow["subsector"].ToString();
					ComposicionK = dataRow["composicionK"].ToString();
					postulante = dataRow["postulante"].ToString();
					NoEmpleados = Convert.ToInt32(dataRow["noEmpleados"]);
					Producto = dataRow["producto"].ToString();
					if (dataRow["anoInicio"].ToString() != "")
					{
						AnoInicio = Convert.ToInt32(dataRow["anoInicio"]);
					}
					else
					{
						AnoInicio = 0;
					}
					Productos = dataRow["productos"].ToString();
					RSE = dataRow["rse"].ToString();
					Fundacion = dataRow["fundacion"].ToString();
					Ambito = dataRow["ambito"].ToString();
					autorizada = Convert.ToBoolean(dataRow["autorizada"]);
					contacto = dataRow["idUsuario"].ToString();
					if (dataRow["cadenaDeValor"].ToString() != "")
					{
						cadenaDeValor = Convert.ToInt32(dataRow["cadenaDeValor"]);
					}
					result = true;
				}
				return result;
			}
			catch (Exception innerException)
			{
				throw new Exception("Ha ocurrido un error en ESR.Business.Empresa.Cargar().", innerException);
			}
		}

		public void EstablecerFechaLimite()
		{
			try
			{
				ESR.Data.Empresa empresa = new ESR.Data.Empresa();
				empresa.EstablecerFechaLimite(idEmpresa, idCuestionario, fechaLimite, idUsuario);
			}
			catch (Exception ex)
			{
				throw new Exception("Ocurri� un error en ESR.Business.Empresa.EstablecerFechaLimite(). ", ex.InnerException);
			}
		}

		public DataSet Buscar2(string criterioDeBusqueda)
		{
			ESR.Data.Empresa empresa = new ESR.Data.Empresa();
			return empresa.Buscar2(criterioDeBusqueda);
		}

		public DataSet Buscar(string criterioDeBusqueda, string sidPais)
		{
			ESR.Data.Empresa empresa = new ESR.Data.Empresa();
			return empresa.Buscar(criterioDeBusqueda, liberado, idCuestionario, Convert.ToInt32(sidPais));
		}

		public Image CargaLogo()
		{
			ESR.Data.Empresa empresa = new ESR.Data.Empresa();
			return empresa.CargaLogo(idEmpresa);
		}

		public DataSet CargaEmpresasGrandes()
		{
			ESR.Data.Empresa empresa = new ESR.Data.Empresa();
			return empresa.CargaEmpresasGrandes();
		}

		public DataSet CargaEmpresas()
		{
			ESR.Data.Empresa empresa = new ESR.Data.Empresa();
			return empresa.CargaEmpresas(idUsuario);
		}

		public string CargaFechaLimiteCuestionario()
		{
			ESR.Data.Empresa empresa = new ESR.Data.Empresa();
			return $"{empresa.CargaFechaLimite(idEmpresa, idCuestionario):d}";
		}

		public DataSet CargaCuestionarios()
		{
			ESR.Data.Empresa empresa = new ESR.Data.Empresa();
			return empresa.CargaCuestionarios(idEmpresa);
		}

		public string CargaCuestionario()
		{
			ESR.Data.Empresa empresa = new ESR.Data.Empresa();
			return empresa.CargaCuestionario(idEmpresa, idCuestionario);
		}

		public int GuardarCuestionario()
		{
			ESR.Data.Empresa empresa = new ESR.Data.Empresa();
			return empresa.GuardarCuestionario(idEmpresa, idCuestionario);
		}

		public bool EliminarCuestionario()
		{
			ESR.Data.Empresa empresa = new ESR.Data.Empresa();
			return empresa.EliminarCuestionario(idEmpresa, idCuestionario);
		}

		public bool EliminarCuestionarios()
		{
			ESR.Data.Empresa empresa = new ESR.Data.Empresa();
			return empresa.EliminarCuestionarios(idEmpresa);
		}

		public DataSet CargaRanking()
		{
			ESR.Data.Empresa empresa = new ESR.Data.Empresa();
			return empresa.CargaRanking(idCuestionario);
		}

		public DataSet CargaTodas()
		{
			ESR.Data.Empresa empresa = new ESR.Data.Empresa();
			return empresa.CargaTodas(idCuestionario, liberado);
		}

		public bool EliminarRegistro()
		{
			ESR.Data.Empresa empresa = new ESR.Data.Empresa();
			return empresa.EliminarRegistro(idEmpresa);
		}
	}
}
