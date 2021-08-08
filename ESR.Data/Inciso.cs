using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.IO;

namespace ESR.Data
{
    public class Inciso
    {
        public byte[] CargaEvidencia(int idIndicador, int idTema, string idInciso, int idTipoEvidencia, int idEmpresa, int idCuestionario, string fileName)
        {

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                string sqlCommand = "CESR_CargaEvidenciaInciso";

                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(dbCommand, "idIndicador", DbType.Int32, idIndicador);
                db.AddInParameter(dbCommand, "idTema", DbType.Int32, idTema);
                db.AddInParameter(dbCommand, "idInciso", DbType.String, idInciso);
                db.AddInParameter(dbCommand, "idTipoEvidencia", DbType.Int32, idTipoEvidencia);
                db.AddInParameter(dbCommand, "idEmpresa", DbType.Int32, idEmpresa);
                db.AddInParameter(dbCommand, "idCuestionario", DbType.Int32, idCuestionario);
                db.AddInParameter(dbCommand, "fileName", DbType.String, fileName);

                DataSet ds = db.ExecuteDataSet(dbCommand);

                if (ds.Tables[0].Rows.Count > 0)
                    return (byte[])ds.Tables[0].Rows[0]["evidencia"];
                else
                    return null;
            }
            catch
            {
                return null;
            }

        }

        public bool EliminarEvidencia(int idIndicador, int idTema, int idTipoEvidencia, string idInciso, int idEmpresa, int idCuestionario, string fileName)
        {
            bool result = false;

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                string sqlCommand = "CESR_EliminaEvidenciaInciso";

                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(dbCommand, "idIndicador", DbType.Int32, idIndicador);
                db.AddInParameter(dbCommand, "idTema", DbType.Int32, idTema);
                db.AddInParameter(dbCommand, "idTipoEvidencia", DbType.Int32, idTipoEvidencia);
                db.AddInParameter(dbCommand, "idInciso", DbType.String, idInciso);
                db.AddInParameter(dbCommand, "idEmpresa", DbType.Int32, idEmpresa);
                db.AddInParameter(dbCommand, "idCuestionario", DbType.Int32, idCuestionario);
                db.AddInParameter(dbCommand, "descripcion", DbType.String, fileName);

                db.ExecuteNonQuery(dbCommand);
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }
        public bool GuardarEvidencia(int idIndicador, int idTema, int idTipoEvidencia, string idInciso, int idEmpresa, int idCuestionario, string fileName, int idTipoRespuesta, byte[] evidencia, Stream streEvidencia, string idUsuario)
        {
            Database db = DatabaseFactory.CreateDatabase();
            
            string sqlCommand = "CESR_GuardarEvidenciaInciso";
            DbCommand evidenciaIndicador = db.GetStoredProcCommand(sqlCommand);
            evidenciaIndicador.CommandTimeout = 0;

            db.AddInParameter(evidenciaIndicador, "idIndicador", DbType.Int32, idIndicador);
            db.AddInParameter(evidenciaIndicador, "idTema", DbType.Int32, idTema);
            db.AddInParameter(evidenciaIndicador, "idInciso", DbType.String, idInciso);
            db.AddInParameter(evidenciaIndicador, "idTipoEvidencia", DbType.Int32, idTipoEvidencia);
            db.AddInParameter(evidenciaIndicador, "idEmpresa", DbType.Int32, idEmpresa);
            db.AddInParameter(evidenciaIndicador, "idCuestionario", DbType.Int32, idCuestionario);
            db.AddInParameter(evidenciaIndicador, "descripcion", DbType.String, fileName);
            db.AddInParameter(evidenciaIndicador, "idTipoRespuesta", DbType.Int32, idTipoRespuesta);
            db.AddInParameter(evidenciaIndicador, "evidencia", DbType.Binary, evidencia);
            db.AddInParameter(evidenciaIndicador, "idUsuario", DbType.String, idUsuario);

            using (DbConnection connection = db.CreateConnection())
            {
                bool result = false;
                //connection.ConnectionTimeout = 0;
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();

                try
                {
                    db.ExecuteNonQuery(evidenciaIndicador, transaction);
                    // Commit the transaction

                    //int bufferLen = 128;

                    //SqlCommand appendEvidencia = new SqlCommand("UPDATETEXT Evidencia_Inciso.evidencia @Pointer @Offset 0 @Bytes", (SqlConnection)connection);
                    //SqlParameter ptrParm = appendEvidencia.Parameters.Add("@Pointer", SqlDbType.Binary, 16);
                    //ptrParm.Value = evidencia;
                    //SqlParameter bytesParm = appendEvidencia.Parameters.Add("@Bytes", SqlDbType.Image, bufferLen);
                    //SqlParameter offsetParm = appendEvidencia.Parameters.Add("@Offset", SqlDbType.Int);
                    //offsetParm.Value = 0;

                    //BinaryReader br = new BinaryReader(streEvidencia);

                    //byte[] buffer = br.ReadBytes(bufferLen);
                    //int offset_ctr = 0;

                    //while (buffer.Length > 0)
                    //{
                    //    bytesParm.Value = buffer;
                    //    appendEvidencia.ExecuteNonQuery();
                    //    offset_ctr += bufferLen;
                    //    offsetParm.Value = offset_ctr;
                    //    buffer = br.ReadBytes(bufferLen);
                    //}

                    //br.Close();

                    transaction.Commit();
                    result = true;
                }
                catch(Exception ex)
                {
                    // Rollback transaction 
                    transaction.Rollback();
                    throw new Exception("Ocurrió un error en ESR.Data.Inciso.GuardarEvidencia(): " + ex.Message, ex);
                    //result = false;
                }
                connection.Close();

                return result;
            }

        }

        public DataSet CargaIncisos(int idTema, int idIndicador)
        {
            Database db = DatabaseFactory.CreateDatabase();
            string sqlCommand = "CESR_CargaIncisos";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "idTema", DbType.Int32, idTema);
            db.AddInParameter(dbCommand, "idIndicador", DbType.Int32, idIndicador);

            DataSet dsIncisos = null;
            dsIncisos = db.ExecuteDataSet(dbCommand);

            dsIncisos.Tables[0].TableName = "Inciso";

            return dsIncisos;
        }

        public DataSet CargaSiguienteInciso(int idTema, int idIndicador)
        {
            Database db = DatabaseFactory.CreateDatabase();
            string sqlCommand = "CESR_CargaSiguienteIdInciso";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "idTema", DbType.Int32, idTema);
            db.AddInParameter(dbCommand, "idIndicador", DbType.Int32, idIndicador);

            DataSet dsInciso = null;
            dsInciso = db.ExecuteDataSet(dbCommand);

            return dsInciso;
        }

        public int GuardaInciso(string idInciso, int idIndicador, int idTema, string descripcion)
        {
            int result = 0;

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "CESR_GuardarInciso";
            DbCommand respuestaCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(respuestaCommand, "idInciso", DbType.String, idInciso);
            db.AddInParameter(respuestaCommand, "idIndicador", DbType.Int32, idIndicador);
            db.AddInParameter(respuestaCommand, "idTema", DbType.Int32, idTema);
            db.AddInParameter(respuestaCommand, "descripcion", DbType.String, descripcion);


            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();

                try
                {
                    db.ExecuteNonQuery(respuestaCommand, transaction);
                    // Commit the transaction
                    transaction.Commit();
                    result = 0;
                }
                catch
                {
                    // Rollback transaction 
                    transaction.Rollback();
                    result = -1;
                }
                connection.Close();

                return result;
            }
        }

        public bool Eliminar(int idTema, int idIndicador, string idInciso)
        {
            try
            {
                bool result = false;
                Database db = DatabaseFactory.CreateDatabase();

                string sqlCommand = "CESR_EliminarInciso";
                DbCommand commandEliminarInciso = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(commandEliminarInciso, "idTema", DbType.Int32, idTema);
                db.AddInParameter(commandEliminarInciso, "idIndicador", DbType.Int32, idIndicador);
                db.AddInParameter(commandEliminarInciso, "idInciso", DbType.String, idInciso);

                //sqlCommand = "CESR_EliminarRespuestaInciso";
                //DbCommand eliminarRespuestaInciso = db.GetStoredProcCommand(sqlCommand);

                //db.AddInParameter(eliminarRespuestaInciso, "idTema", DbType.Int32, idTema);
                //db.AddInParameter(eliminarRespuestaInciso, "idIndicador", DbType.Int32, idIndicador);
                //db.AddInParameter(eliminarRespuestaInciso, "idInciso", DbType.String, idInciso);

                //sqlCommand = "CESR_EliminarEvidenciaInciso";
                //DbCommand eliminarEvidenciaInciso = db.GetStoredProcCommand(sqlCommand);

                //db.AddInParameter(eliminarEvidenciaInciso, "idTema", DbType.Int32, idTema);
                //db.AddInParameter(eliminarEvidenciaInciso, "idIndicador", DbType.Int32, idIndicador);
                //db.AddInParameter(eliminarEvidenciaInciso, "idInciso", DbType.String, idInciso);

                using (DbConnection connection = db.CreateConnection())
                {
                    connection.Open();
                    DbTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        //db.ExecuteNonQuery(eliminarEvidenciaInciso, transaction);
                        //db.ExecuteNonQuery(eliminarRespuestaInciso, transaction);
                        db.ExecuteNonQuery(commandEliminarInciso, transaction);
                        // Commit the transaction
                        transaction.Commit();
                        result = true;
                    }
                    catch
                    {
                        // Rollback transaction 
                        transaction.Rollback();
                    }
                    connection.Close();

                    return result;
                }
            }
            catch
            {
                return false;
            }
        }
        public bool GuardarRespuesta(int idIndicador, int idTema, string idInciso, int idEmpresa, int idCuestionario, int idTipoRespuesta, string idUsuario)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "CESR_GuardarRespuestaInciso";
            DbCommand respuestaIndicador = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(respuestaIndicador, "idIndicador", DbType.Int32, idIndicador);
            db.AddInParameter(respuestaIndicador, "idTema", DbType.Int32, idTema);
            db.AddInParameter(respuestaIndicador, "idInciso", DbType.String, idInciso);
            db.AddInParameter(respuestaIndicador, "idEmpresa", DbType.Int32, idEmpresa);
            db.AddInParameter(respuestaIndicador, "idCuestionario", DbType.Int32, idCuestionario);
            db.AddInParameter(respuestaIndicador, "idTipoRespuesta", DbType.Int32, idTipoRespuesta);
            db.AddInParameter(respuestaIndicador, "idUsuario", DbType.String, idUsuario);


            using (DbConnection connection = db.CreateConnection())
            {
                bool result = false;
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();

                try
                {
                    db.ExecuteNonQuery(respuestaIndicador, transaction);
                    // Commit the transaction
                    transaction.Commit();
                    result = true;
                }
                catch
                {
                    // Rollback transaction 
                    transaction.Rollback();
                }
                connection.Close();

                return result;
            }

        }
    }
}
