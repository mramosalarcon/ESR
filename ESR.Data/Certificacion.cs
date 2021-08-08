using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.IO;
using System.Configuration;

namespace ESR.Data
{
    public class Certificacion
    {
        public DataSet CargaCertificaciones(int idEmpresa)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "CESR_CargaCertificaciones";
            DbCommand certificacionCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(certificacionCommand, "idEmpresa", DbType.Int32, idEmpresa);
            DataSet dsCertificaciones = db.ExecuteDataSet(certificacionCommand);
            dsCertificaciones.Tables[0].TableName = "Certificacion";

            return dsCertificaciones;
        }

        public bool Guardar(int idEmpresa, string institucion, string certificacion, int anio)
        {
            bool resultado = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase();

                // Two operations, one to credit an account, and one to debit another
                // account.
                string sqlCommand = "CESR_GuardarCertificacion";
                DbCommand certificacionCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(certificacionCommand, "idEmpresa", DbType.Int32, idEmpresa);
                db.AddInParameter(certificacionCommand, "institucion", DbType.String, institucion);
                db.AddInParameter(certificacionCommand, "certificacion", DbType.String, certificacion);
                db.AddInParameter(certificacionCommand, "anio", DbType.Int32, anio);

                 using (DbConnection connection = db.CreateConnection())
                 {
                     connection.Open();
                     DbTransaction transaction = connection.BeginTransaction();

                     try
                     {
                         db.ExecuteNonQuery(certificacionCommand, transaction);
                         // Commit the transaction
                         transaction.Commit();
                         resultado = true;
                     }
                     catch
                     {
                         // Rollback transaction 
                         transaction.Rollback();
                     }
                     connection.Close();

                     return resultado;
                 }
             }
             catch (Exception ex)
             {
                 using (StreamWriter sw = File.AppendText(ConfigurationManager.AppSettings["logFile"].ToString()))
                 {
                     sw.AutoFlush = true;
                     sw.WriteLine("Fecha: " + DateTime.Now.ToString());
                     sw.WriteLine("Empresa: " + idEmpresa);
                     sw.WriteLine("Institucion: " + institucion);
                     sw.WriteLine("Certificacion: " + certificacion);
                     sw.WriteLine("Anio: " + anio);
                     sw.WriteLine("Error en ESR.Data.Certificacion.Guardar(): " + ex.Message);
                     sw.WriteLine("InnerException: " + ex.InnerException);
                     sw.Close();
                 }
                 return false;
             }
        }

        public bool Eliminar(int idCertificacion, int idEmpresa)
        {
            bool resultado = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase();

                // Two operations, one to credit an account, and one to debit another
                // account.
                string sqlCommand = "CESR_EliminarCertificacion";
                DbCommand certificacionCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(certificacionCommand, "idCertificacion", DbType.Int32, idCertificacion);
                db.AddInParameter(certificacionCommand, "idEmpresa", DbType.Int32, idEmpresa);
                

                using (DbConnection connection = db.CreateConnection())
                {
                    connection.Open();
                    DbTransaction transaction = connection.BeginTransaction();

                    try
                    {

                        db.ExecuteNonQuery(certificacionCommand, transaction);

                        // Commit the transaction
                        transaction.Commit();
                        resultado = true;
                    }
                    catch
                    {
                        // Rollback transaction 
                        transaction.Rollback();
                    }
                    connection.Close();

                    return resultado;
                }
            }
            catch 
            {
                return false;
                //throw new Exception("ESR.Data.Certificacion.Guardar(): " + ex.Message);
            }
        }

        public bool Actualizar(int idEmpresa, string institucion, string certificacion, int anio, int idCertificacion)
        {
            bool resultado = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase();

                // Two operations, one to credit an account, and one to debit another
                // account.
                string sqlCommand = "CESR_ActualizarCertificacion";
                DbCommand certificacionCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(certificacionCommand, "idEmpresa", DbType.Int32, idEmpresa);
                db.AddInParameter(certificacionCommand, "institucion", DbType.String, institucion);
                db.AddInParameter(certificacionCommand, "certificacion", DbType.String, certificacion);
                db.AddInParameter(certificacionCommand, "anio", DbType.Int32, anio);
                db.AddInParameter(certificacionCommand, "idCertificacion", DbType.Int32, idCertificacion);

                using (DbConnection connection = db.CreateConnection())
                {
                    connection.Open();
                    DbTransaction transaction = connection.BeginTransaction();

                    try
                    {

                        db.ExecuteNonQuery(certificacionCommand, transaction);

                        // Commit the transaction
                        transaction.Commit();
                        resultado = true;
                    }
                    catch
                    {
                        // Rollback transaction 
                        transaction.Rollback();
                    }
                    connection.Close();

                    return resultado;
                }
            }
            catch 
            {
                return false;
                //throw new Exception("ESR.Data.Certificacion.Guardar(): " + ex.Message);
            }
        }
    }
}
