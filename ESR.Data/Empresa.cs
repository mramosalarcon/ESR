using System;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ESR.Data
{
	public class Empresa
	{
		public bool PagoCuestionario(int idEmpresa, int idCuestionario, bool pago)
		{
			try
			{
				Database val = DatabaseFactory.CreateDatabase();
				string text = "CESR_PagoCuestionario";
				DbCommand storedProcCommand = val.GetStoredProcCommand(text);
				val.AddInParameter(storedProcCommand, "idEmpresa", DbType.Int32, (object)idEmpresa);
				val.AddInParameter(storedProcCommand, "idCuestionario", DbType.String, (object)idCuestionario);
				val.AddInParameter(storedProcCommand, "pago", DbType.Boolean, (object)pago);
				val.ExecuteNonQuery(storedProcCommand);
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception("ESR.Data.Empresa.VincularUsuario(): " + ex.Message);
			}
		}

		public string CargaResponsable(int idEmpresa)
		{
			string empty = string.Empty;
			string text = "Select Top 1 UE.idUsuario from Usuario_Empresa UE Inner Join Usuario U on UE.idUsuario = U.idUsuario Inner join Usuario_Perfil UP on UE.idUsuario = UP.idUsuario where U.bloqueado = 0 and UP.idPerfil = 7 and idEmpresa = " + idEmpresa.ToString();
			Database val = DatabaseFactory.CreateDatabase();
			DbCommand sqlStringCommand = val.GetSqlStringCommand(text);
			try
			{
				return (string)val.ExecuteScalar(sqlStringCommand);
			}
			catch (Exception ex)
			{
				throw new Exception("ESR.Data.Empresa.CargaReponsable(). Error: " + ex.Message, ex);
			}
		}

		public string NotificarVinculo(int idEmpresa, string idUsuario)
		{
			try
			{
				Database val = DatabaseFactory.CreateDatabase();
				string text = "CESR_NotificarVinculo";
				DbCommand storedProcCommand = val.GetStoredProcCommand(text);
				val.AddInParameter(storedProcCommand, "idEmpresa", DbType.Int32, (object)idEmpresa);
				val.AddInParameter(storedProcCommand, "idUsuario", DbType.String, (object)idUsuario);
				return (string)val.ExecuteScalar(storedProcCommand);
			}
			catch (Exception ex)
			{
				throw new Exception("ESR.Data.Empresa.VincularUsuario(): " + ex.Message);
			}
		}

		public bool VincularUsuario(int idEmpresa, string idUsuario)
		{
			try
			{
				Database val = DatabaseFactory.CreateDatabase();
				string text = "CESR_VincularUsuarioEmpresa";
				DbCommand storedProcCommand = val.GetStoredProcCommand(text);
				val.AddInParameter(storedProcCommand, "idEmpresa", DbType.Int32, (object)idEmpresa);
				val.AddInParameter(storedProcCommand, "idUsuario", DbType.String, (object)idUsuario);
				val.ExecuteNonQuery(storedProcCommand);
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception("ESR.Data.Empresa.VincularUsuario(): " + ex.Message);
			}
		}

		public bool AceptarVinculo(int idEmpresa, string idUsuario, int idPerfil)
		{
			try
			{
				Database val = DatabaseFactory.CreateDatabase();
				string text = "CESR_VincularUsuarioAEmpresa";
				DbCommand storedProcCommand = val.GetStoredProcCommand(text);
				val.AddInParameter(storedProcCommand, "idUsuario", DbType.String, (object)idUsuario);
				val.AddInParameter(storedProcCommand, "idEmpresa", DbType.Int32, (object)idEmpresa);
				val.AddInParameter(storedProcCommand, "idPerfil", DbType.Int32, (object)idPerfil);
				val.ExecuteNonQuery(storedProcCommand);
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception("ESR.Data.Empresa.AceptarVinculo(): " + ex.Message);
			}
		}

		public bool RechazarVinculo(int idEmpresa, string idUsuario)
		{
			try
			{
				Database val = DatabaseFactory.CreateDatabase();
				string text = "CESR_RechazarVinculoUsuarioAEmpresa";
				DbCommand storedProcCommand = val.GetStoredProcCommand(text);
				val.AddInParameter(storedProcCommand, "idEmpresa", DbType.Int32, (object)idEmpresa);
				val.AddInParameter(storedProcCommand, "idUsuario", DbType.String, (object)idUsuario);
				val.ExecuteNonQuery(storedProcCommand);
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception("ESR.Data.Empresa.RechazarVinculo(): " + ex.Message);
			}
		}

		public bool EliminarRegistro(int idEmpresa)
		{
			bool flag = false;
			try
			{
				Database val = DatabaseFactory.CreateDatabase();
				string text = "Delete from Practica where idEmpresa = " + idEmpresa.ToString();
				DbCommand sqlStringCommand = val.GetSqlStringCommand(text);
				string text2 = "Delete From Certificacion where idEmpresa = " + idEmpresa.ToString();
				DbCommand sqlStringCommand2 = val.GetSqlStringCommand(text2);
				string text3 = "Delete Usuario_Perfil From Usuario_Perfil UP Inner Join Usuario_Empresa UE On UP.idUsuario = UE.idUsuario where UE.idEmpresa = " + idEmpresa.ToString();
				DbCommand sqlStringCommand3 = val.GetSqlStringCommand(text3);
				string text4 = "Delete from Usuario_Empresa where idEmpresa = " + idEmpresa.ToString();
				DbCommand sqlStringCommand4 = val.GetSqlStringCommand(text4);
				string text5 = "Update Usuario set bloqueado = 'true' from Usuario U Inner Join Usuario_Empresa UE On U.idUsuario = UE.idUsuario Where UE.idEmpresa = " + idEmpresa.ToString();
				DbCommand sqlStringCommand5 = val.GetSqlStringCommand(text5);
				string text6 = "Delete from Cuestionario_Empresa where idEmpresa = " + idEmpresa.ToString();
				DbCommand sqlStringCommand6 = val.GetSqlStringCommand(text6);
				string text7 = "Delete from Empresa_Usuario_Tema where idEmpresa = " + idEmpresa.ToString();
				DbCommand sqlStringCommand7 = val.GetSqlStringCommand(text7);
				string text8 = "Delete from Empresa where idEmpresa = " + idEmpresa.ToString();
				DbCommand sqlStringCommand8 = val.GetSqlStringCommand(text8);
				using (DbConnection dbConnection = val.CreateConnection())
				{
					dbConnection.Open();
					DbTransaction dbTransaction = dbConnection.BeginTransaction();
					try
					{
						val.ExecuteNonQuery(sqlStringCommand, dbTransaction);
						val.ExecuteNonQuery(sqlStringCommand2, dbTransaction);
						val.ExecuteNonQuery(sqlStringCommand3, dbTransaction);
						val.ExecuteNonQuery(sqlStringCommand4, dbTransaction);
						val.ExecuteNonQuery(sqlStringCommand5, dbTransaction);
						val.ExecuteNonQuery(sqlStringCommand6, dbTransaction);
						val.ExecuteNonQuery(sqlStringCommand7, dbTransaction);
						val.ExecuteNonQuery(sqlStringCommand8, dbTransaction);
						dbTransaction.Commit();
						flag = true;
					}
					catch
					{
						dbTransaction.Rollback();
						flag = false;
					}
					dbConnection.Close();
					return flag;
				}
			}
			catch
			{
				return false;
			}
		}

		public DataSet CargaTodas(int idCuestionario, bool liberado)
		{
			try
			{
				Database db;
				if (idCuestionario > 121)
					db = DatabaseFactory.CreateDatabase("ESR");
				else if (idCuestionario > 110)
					db = DatabaseFactory.CreateDatabase("ESR");
				else if (idCuestionario > 102)
					db = DatabaseFactory.CreateDatabase("ESR_2020");
				else if (idCuestionario > 94)
					db = DatabaseFactory.CreateDatabase("ESR_2019");
				else if (idCuestionario > 86)
					db = DatabaseFactory.CreateDatabase("ESR_2018");
				else //if (idCuestionario > 53)
					db = DatabaseFactory.CreateDatabase("ESR_2017");

				string text = "CESR_CargarTodasLasEmpresas";
				DbCommand storedProcCommand = db.GetStoredProcCommand(text);
				db.AddInParameter(storedProcCommand, "idCuestionario", DbType.Int32, (object)idCuestionario);
				db.AddInParameter(storedProcCommand, "liberado", DbType.Boolean, (object)liberado);
				storedProcCommand.CommandTimeout = 0;
				DataSet dataSet = null;
				dataSet = db.ExecuteDataSet(storedProcCommand);
				dataSet.Tables[0].TableName = "Empresa";
				return dataSet;
			}
			catch (Exception ex)
			{
				throw new Exception("ESR.Data.Empresa.CargaTodas(). Error: " + ex.Message, ex);
			}
		}

		public DataSet CargaRanking(int idCuestionario)
		{
			try
			{
				Database val = DatabaseFactory.CreateDatabase();
				string text = "CESR_CargarRanking";
				DbCommand storedProcCommand = val.GetStoredProcCommand(text);
				storedProcCommand.CommandTimeout = 0;
				val.AddInParameter(storedProcCommand, "idCuestionario", DbType.Int32, (object)idCuestionario);
				DataSet dataSet = null;
				dataSet = val.ExecuteDataSet(storedProcCommand);
				dataSet.Tables[0].TableName = "Empresa";
				return dataSet;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public Image CargaLogo(int idEmpresa)
		{
			try
			{
				Image image = null;
				Database val = DatabaseFactory.CreateDatabase();
				string text = "CESR_CargaLogo";
				DbCommand storedProcCommand = val.GetStoredProcCommand(text);
				val.AddInParameter(storedProcCommand, "idEmpresa", DbType.Int32, (object)idEmpresa);
				byte[] buffer = (byte[])val.ExecuteScalar(storedProcCommand);
				MemoryStream stream = new MemoryStream(buffer);
				return Image.FromStream(stream);
			}
			catch
			{
				return null;
			}
		}

		public int Guardar(int idEmpresa, string RFC, string nombre, string nombreCorto, string razonSocial, string domicilio, string colonia, string ciudad, string estado, string cp, int idPais, string siglas, string telefono, string fax, string email, string webSite, byte[] logo, string director, string presidente, string tamano, string sector, string subsector, string composicionK, string postulante, int noEmpleados, string producto, int anoInicio, string productos, string rse, string fundacion, string ambito, bool autorizada, string idUsuario, int cadenaDeValor)
		{
			int result = 0;
			try
			{
				Database val = DatabaseFactory.CreateDatabase();
				string text = "CESR_GuardarEmpresa";
				DbCommand storedProcCommand = val.GetStoredProcCommand(text);
				val.AddInParameter(storedProcCommand, "idEmpresa", DbType.Int32, (object)idEmpresa);
				val.AddInParameter(storedProcCommand, "rfc", DbType.String, (object)RFC);
				val.AddInParameter(storedProcCommand, "nombre", DbType.String, (object)nombre);
				val.AddInParameter(storedProcCommand, "nombreCorto", DbType.String, (object)nombreCorto);
				val.AddInParameter(storedProcCommand, "razonSocial", DbType.String, (object)razonSocial);
				val.AddInParameter(storedProcCommand, "domicilio", DbType.String, (object)domicilio);
				val.AddInParameter(storedProcCommand, "colonia", DbType.String, (object)colonia);
				val.AddInParameter(storedProcCommand, "ciudad", DbType.String, (object)ciudad);
				val.AddInParameter(storedProcCommand, "estado", DbType.String, (object)estado);
				val.AddInParameter(storedProcCommand, "cp", DbType.String, (object)cp);
				val.AddInParameter(storedProcCommand, "idPais", DbType.Int32, (object)idPais);
				val.AddInParameter(storedProcCommand, "siglas", DbType.String, (object)siglas);
				val.AddInParameter(storedProcCommand, "telefono", DbType.String, (object)telefono);
				val.AddInParameter(storedProcCommand, "fax", DbType.String, (object)fax);
				val.AddInParameter(storedProcCommand, "email", DbType.String, (object)email);
				val.AddInParameter(storedProcCommand, "webSite", DbType.String, (object)webSite);
				val.AddInParameter(storedProcCommand, "logo", DbType.Binary, (object)logo);
				val.AddInParameter(storedProcCommand, "director", DbType.String, (object)director);
				val.AddInParameter(storedProcCommand, "presidente", DbType.String, (object)presidente);
				val.AddInParameter(storedProcCommand, "tamano", DbType.String, (object)tamano);
				val.AddInParameter(storedProcCommand, "sector", DbType.String, (object)sector);
				val.AddInParameter(storedProcCommand, "subsector", DbType.String, (object)subsector);
				val.AddInParameter(storedProcCommand, "composicionK", DbType.String, (object)composicionK);
				val.AddInParameter(storedProcCommand, "postulante", DbType.String, (object)postulante);
				val.AddInParameter(storedProcCommand, "noEmpleados", DbType.Int32, (object)noEmpleados);
				val.AddInParameter(storedProcCommand, "producto", DbType.String, (object)producto);
				val.AddInParameter(storedProcCommand, "anoInicio", DbType.Int32, (object)anoInicio);
				val.AddInParameter(storedProcCommand, "productos", DbType.String, (object)productos);
				val.AddInParameter(storedProcCommand, "rse", DbType.String, (object)rse);
				val.AddInParameter(storedProcCommand, "fundacion", DbType.String, (object)fundacion);
				val.AddInParameter(storedProcCommand, "ambito", DbType.String, (object)ambito);
				val.AddInParameter(storedProcCommand, "autorizada", DbType.Boolean, (object)autorizada);
				val.AddInParameter(storedProcCommand, "idUsuario", DbType.String, (object)idUsuario);
				val.AddInParameter(storedProcCommand, "cadenaDeValor", DbType.Int32, (object)cadenaDeValor);
				using (DbConnection dbConnection = val.CreateConnection())
				{
					dbConnection.Open();
					DbTransaction dbTransaction = dbConnection.BeginTransaction();
					try
					{
						result = (int)val.ExecuteScalar(storedProcCommand, dbTransaction);
						dbTransaction.Commit();
					}
					catch
					{
						dbTransaction.Rollback();
					}
					dbConnection.Close();
					return result;
				}
			}
			catch (Exception ex)
			{
				throw new Exception("ESR.Data.Empresa.Guardar(): " + ex.Message);
			}
		}

		public DataSet cargaNombre(int idEmpresa)
		{
			try
			{
				Database val = DatabaseFactory.CreateDatabase();
				string text = "CESR_CargarNombreEmpresa";
				DbCommand storedProcCommand = val.GetStoredProcCommand(text);
				val.AddInParameter(storedProcCommand, "idEmpresa", DbType.Int32, (object)idEmpresa);
				return val.ExecuteDataSet(storedProcCommand);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public DataSet Cargar(int idEmpresa)
		{
			try
			{
				Database val = DatabaseFactory.CreateDatabase();
				string text = "CESR_CargarEmpresa";
				DbCommand storedProcCommand = val.GetStoredProcCommand(text);
				val.AddInParameter(storedProcCommand, "idEmpresa", DbType.Int32, (object)idEmpresa);
				return val.ExecuteDataSet(storedProcCommand);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public void EstablecerFechaLimite(int idEmpresa, int idCuestionario, DateTime fechaLimite, string idUsuario)
		{
			try
			{
				Database val = DatabaseFactory.CreateDatabase();
				string text = "CESR_EstablecerFechaLimite";
				DbCommand storedProcCommand = val.GetStoredProcCommand(text);
				val.AddInParameter(storedProcCommand, "idEmpresa", DbType.Int32, (object)idEmpresa);
				val.AddInParameter(storedProcCommand, "idCuestionario", DbType.Int32, (object)idCuestionario);
				val.AddInParameter(storedProcCommand, "fechaLimite", DbType.Date, (object)fechaLimite);
				val.AddInParameter(storedProcCommand, "idUsuario", DbType.String, (object)idUsuario);
				val.ExecuteNonQuery(storedProcCommand);
			}
			catch (Exception ex)
			{
				throw new Exception("Ocurri� un error en ESR.Data.Empresa.EstablecerFechaLimite(). ", ex.InnerException);
			}
		}

		public DataSet Buscar2(string criterioDeBusqueda)
		{
			try
			{
				criterioDeBusqueda = "%" + criterioDeBusqueda + "%";
				Database val = DatabaseFactory.CreateDatabase();
				string text = "CESR_BuscarEmpresas2";
				DbCommand storedProcCommand = val.GetStoredProcCommand(text);
				val.AddInParameter(storedProcCommand, "criterio", DbType.String, (object)criterioDeBusqueda);
				DataSet dataSet = val.ExecuteDataSet(storedProcCommand);
				dataSet.Tables[0].TableName = "Empresa";
				return dataSet;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public DataSet Buscar(string criterioDeBusqueda, bool liberado, int idCuestionario, int idPais)
		{
			try
			{
				criterioDeBusqueda = "%" + criterioDeBusqueda + "%";
				Database val = DatabaseFactory.CreateDatabase();
				string text = "CESR_BuscarEmpresas";
				DbCommand storedProcCommand = val.GetStoredProcCommand(text);
				val.AddInParameter(storedProcCommand, "criterio", DbType.String, (object)criterioDeBusqueda);
				val.AddInParameter(storedProcCommand, "liberado", DbType.Boolean, (object)liberado);
				val.AddInParameter(storedProcCommand, "idCuestionario", DbType.Int32, (object)idCuestionario);
				val.AddInParameter(storedProcCommand, "idPais", DbType.Int32, (object)idPais);

				DataSet dataSet = val.ExecuteDataSet(storedProcCommand);
				dataSet.Tables[0].TableName = "Empresa";
				return dataSet;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public DataSet CargaEmpresasGrandes()
		{
			try
			{
				Database val = DatabaseFactory.CreateDatabase();
				string text = "CESR_CargarEmpresasGrandes";
				DbCommand storedProcCommand = val.GetStoredProcCommand(text);
				return val.ExecuteDataSet(storedProcCommand);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public DataSet CargaEmpresas(string idUsuario)
		{
			try
			{
				Database val = DatabaseFactory.CreateDatabase();
				string text = "CESR_CargarEmpresas";
				DbCommand storedProcCommand = val.GetStoredProcCommand(text);
				val.AddInParameter(storedProcCommand, "idUsuario", DbType.String, (object)idUsuario);
				return val.ExecuteDataSet(storedProcCommand);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public DateTime CargaFechaLimite(int idEmpresa, int idCuestionario)
		{
			try
			{
				Database val = DatabaseFactory.CreateDatabase();
				string text = "CESR_CargarFechaLimiteCuestionario";
				DbCommand storedProcCommand = val.GetStoredProcCommand(text);
				val.AddInParameter(storedProcCommand, "idEmpresa", DbType.Int32, (object)idEmpresa);
				val.AddInParameter(storedProcCommand, "idCuestionario", DbType.Int32, (object)idCuestionario);
				return Convert.ToDateTime(val.ExecuteScalar(storedProcCommand));
			}
			catch (Exception ex)
			{
				throw new Exception("ESR.Data.Empresa.CargaFechaLimite(): " + ex.Message);
			}
		}

		public string CargaCuestionario(int idEmpresa, int idCuestionario)
		{
			try
			{
				Database val = DatabaseFactory.CreateDatabase();
				string text = "CESR_CargaNombreCuestionario";
				DbCommand storedProcCommand = val.GetStoredProcCommand(text);
				val.AddInParameter(storedProcCommand, "idEmpresa", DbType.Int32, (object)idEmpresa);
				val.AddInParameter(storedProcCommand, "idCuestionario", DbType.Int32, (object)idCuestionario);
				return val.ExecuteScalar(storedProcCommand).ToString();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public DataSet CargaCuestionarios(int idEmpresa)
		{
			try
			{
				Database val = DatabaseFactory.CreateDatabase();
				string text = "CESR_CargarCuestionariosEmpresa";
				DbCommand storedProcCommand = val.GetStoredProcCommand(text);
				val.AddInParameter(storedProcCommand, "idEmpresa", DbType.Int32, (object)idEmpresa);
				DataSet dataSet = null;
				dataSet = val.ExecuteDataSet(storedProcCommand);
				dataSet.Tables[0].TableName = "Cuestionario";
				return dataSet;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public bool EliminarCuestionario(int idEmpresa, int idCuestionario)
		{
			bool flag = false;
			try
			{
				Database val = DatabaseFactory.CreateDatabase();
				string text = "CESR_EliminarEmpresaCuestionario";
				DbCommand storedProcCommand = val.GetStoredProcCommand(text);
				val.AddInParameter(storedProcCommand, "idEmpresa", DbType.Int32, (object)idEmpresa);
				val.AddInParameter(storedProcCommand, "idCuestionario", DbType.Int32, (object)idCuestionario);
				val.ExecuteNonQuery(storedProcCommand);
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception("ESR.Data.Empresa.EliminarCuestionario(): " + ex.Message);
			}
		}

		public bool EliminarCuestionarios(int idEmpresa)
		{
			try
			{
				Database val = DatabaseFactory.CreateDatabase();
				string text = "CESR_EliminarEmpresaCuestionarios";
				DbCommand storedProcCommand = val.GetStoredProcCommand(text);
				val.AddInParameter(storedProcCommand, "idEmpresa", DbType.Int32, (object)idEmpresa);
				val.ExecuteNonQuery(storedProcCommand);
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception("ESR.Data.Empresa.EliminarCuestionarios(): " + ex.Message);
			}
		}

		public int GuardarCuestionario(int idEmpresa, int idCuestionario)
		{
			try
			{
				Database val = DatabaseFactory.CreateDatabase();
				string text = "CESR_GuardarEmpresaCuestionario";
				DbCommand storedProcCommand = val.GetStoredProcCommand(text);
				val.AddInParameter(storedProcCommand, "idEmpresa", DbType.Int32, (object)idEmpresa);
				val.AddInParameter(storedProcCommand, "idCuestionario", DbType.Int32, (object)idCuestionario);
				return val.ExecuteNonQuery(storedProcCommand);
			}
			catch (Exception ex)
			{
				throw new Exception("ESR.Data.Empresa.GuardarCuestionario(): " + ex.Message);
			}
		}
	}
}