using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.IO;

namespace ESR.Data
{
    public class Indicador
    {
        public byte[] CargaEvidencia(int idIndicador, int idTema, int idTipoEvidencia, int idEmpresa, int idCuestionario, string fileName)
        {

            try
            {
                Database db;
                if (idCuestionario > 110)
                    db = DatabaseFactory.CreateDatabase("ESR");
                else if (idCuestionario > 102)
                    db = DatabaseFactory.CreateDatabase("ESR_2020");
                else if (idCuestionario > 94)
                    db = DatabaseFactory.CreateDatabase("ESR_2019");
                else if (idCuestionario > 86)
                    db = DatabaseFactory.CreateDatabase("ESR_2018");
                else //if (idCuestionario > 53)
                    db = DatabaseFactory.CreateDatabase("ESR_2017");


                string sqlCommand = "CESR_CargaEvidenciaIndicador";

                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(dbCommand, "idIndicador", DbType.Int32, idIndicador);
                db.AddInParameter(dbCommand, "idTema", DbType.Int32, idTema);
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
        public bool GuardarEvaluacion(int idIndicador, int idTema, int idEmpresa, int idCuestionario, string idUsuario, float valor, float calificacionRevisor)
        {
            bool result = false;

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                string sqlCommand = "CESR_GuardarEvaluacionIndicador";

                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(dbCommand, "idIndicador", DbType.Int32, idIndicador);
                db.AddInParameter(dbCommand, "idTema", DbType.Int32, idTema);
                db.AddInParameter(dbCommand, "idEmpresa", DbType.Int32, idEmpresa);
                db.AddInParameter(dbCommand, "idCuestionario", DbType.Int32, idCuestionario);
                db.AddInParameter(dbCommand, "idUsuario", DbType.String, idUsuario);
                db.AddInParameter(dbCommand, "valor", DbType.Single, valor);
                db.AddInParameter(dbCommand, "calificacionRevisor", DbType.Single, calificacionRevisor);

                db.ExecuteNonQuery(dbCommand);
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        
        }

        public bool EliminarEvidencia(int idIndicador, int idTema, int idTipoEvidencia, int idEmpresa, int idCuestionario, string fileName)
        {
            bool result = false;

            try
            {
                Database db;
                if (idCuestionario > 110)
                    db = DatabaseFactory.CreateDatabase("ESR");
                else if (idCuestionario > 102)
                    db = DatabaseFactory.CreateDatabase("ESR_2020");
                else if (idCuestionario > 94)
                    db = DatabaseFactory.CreateDatabase("ESR_2019");
                else if (idCuestionario > 86)
                    db = DatabaseFactory.CreateDatabase("ESR_2018");
                else //if (idCuestionario > 53)
                    db = DatabaseFactory.CreateDatabase("ESR_2017");


                string sqlCommand = "CESR_EliminaEvidenciaIndicador";

                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(dbCommand, "idIndicador", DbType.Int32, idIndicador);
                db.AddInParameter(dbCommand, "idTema", DbType.Int32, idTema);
                db.AddInParameter(dbCommand, "idTipoEvidencia", DbType.Int32, idTipoEvidencia);
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

        public bool GuardarEvidencia(int idIndicador, int idTema, int idTipoEvidencia, int idEmpresa, int idCuestionario, string fileName, int idTipoRespuesta, byte[] evidencia, Stream streEvidencia, string idUsuario, string Url)
        {
            Database db = DatabaseFactory.CreateDatabase();
           
            string sqlCommand = "CESR_GuardarEvidenciaIndicador";
            DbCommand evidenciaIndicador = db.GetStoredProcCommand(sqlCommand);
            evidenciaIndicador.CommandTimeout = 0;

            db.AddInParameter(evidenciaIndicador, "idIndicador", DbType.Int32, idIndicador);
            db.AddInParameter(evidenciaIndicador, "idTema", DbType.Int32, idTema);
            db.AddInParameter(evidenciaIndicador, "idTipoEvidencia", DbType.Int32, idTipoEvidencia);
            db.AddInParameter(evidenciaIndicador, "idEmpresa", DbType.Int32, idEmpresa);
            db.AddInParameter(evidenciaIndicador, "idCuestionario", DbType.Int32, idCuestionario);
            db.AddInParameter(evidenciaIndicador, "descripcion", DbType.String, fileName);
            db.AddInParameter(evidenciaIndicador, "idTipoRespuesta", DbType.String, idTipoRespuesta);
            db.AddInParameter(evidenciaIndicador, "evidencia", DbType.Binary, evidencia);
            db.AddInParameter(evidenciaIndicador, "idUsuario", DbType.String, idUsuario);
            db.AddInParameter(evidenciaIndicador, "Url", DbType.String, Url);


            using (DbConnection connection = db.CreateConnection())
            {
                bool result = false;
                
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();

                try
                {
                    db.ExecuteNonQuery(evidenciaIndicador, transaction);
                    // Commit the transaction

                    //int bufferLen = 128;

                    //SqlCommand appendEvidencia = new SqlCommand("UPDATETEXT Evidencia_Indicador.evidencia @Pointer @Offset 0 @Bytes", (SqlConnection)connection);
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
                    throw new Exception("Ocurrio un error en ESR.Data.Indicador.GuardarEvidencia(): " + ex.Message, ex);
                }
                connection.Close();

                return result;
            }

        }

        public bool GuardarRespuesta(int idIndicador, int idTema, int idEmpresa, int idCuestionario, int idTipoRespuesta, string idUsuario)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "CESR_GuardarRespuestaIndicador";
            DbCommand respuestaIndicador = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(respuestaIndicador, "idIndicador", DbType.Int32, idIndicador);
            db.AddInParameter(respuestaIndicador, "idTema", DbType.Int32, idTema);
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

        public int GuardaAfinidad(int idTema, int idIndicador, int idAfinidad)
        {
            
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "CESR_GuardarAfinidad";
            DbCommand afinidadCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(afinidadCommand, "idIndicador", DbType.Int32, idIndicador);
            db.AddInParameter(afinidadCommand, "idTema", DbType.Int32, idTema);
            db.AddInParameter(afinidadCommand, "idAfinidad", DbType.Int32, idAfinidad);

            using (DbConnection connection = db.CreateConnection())
            {
                int result = -1;
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();

                try
                {
                    db.ExecuteNonQuery(afinidadCommand, transaction);
                    // Commit the transaction
                    transaction.Commit();
                    result = 0;
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

        public int GuardaPrincipioRSE(int idTema, int idIndicador, int idPrincipioRSE)
        {

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "CESR_GuardarPrincipioRSE";
            DbCommand principioRSECommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(principioRSECommand, "idIndicador", DbType.Int32, idIndicador);
            db.AddInParameter(principioRSECommand, "idTema", DbType.Int32, idTema);
            db.AddInParameter(principioRSECommand, "idPrincipioRSE", DbType.Int32, idPrincipioRSE);

            using (DbConnection connection = db.CreateConnection())
            {
                int result = -1;
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();

                try
                {
                    db.ExecuteNonQuery(principioRSECommand, transaction);
                    // Commit the transaction
                    transaction.Commit();
                    result = 0;
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
        public int CargaIdIndicador(int idCuestionario, int idTema, int ordinal)
        {
            Database db = DatabaseFactory.CreateDatabase();
            string sqlCommand = "Select idIndicador from Cuestionario_Indicador where idCuestionario = @idCuestionario and idTema = @idTema and ordinal = @ordinal";
            DbCommand dbCommand = db.GetSqlStringCommand(sqlCommand);

            db.AddInParameter(dbCommand, "idCuestionario", DbType.Int32, idCuestionario);
            db.AddInParameter(dbCommand, "idTema", DbType.Int32, idTema);
            db.AddInParameter(dbCommand, "ordinal", DbType.Int32, ordinal);

            return Convert.ToInt32(db.ExecuteScalar(dbCommand));
        }

        public DataSet Carga(int idTema, int idIndicador)
        {
            Database db = DatabaseFactory.CreateDatabase();
            string sqlCommand = "CESR_CargaIndicadorSinCuestionario";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "idTema", DbType.Int32, idTema);
            db.AddInParameter(dbCommand, "idIndicador", DbType.Int32, idIndicador);

            DataSet indicador = null;

            indicador = db.ExecuteDataSet(dbCommand);

            indicador.Tables[0].TableName = "Tipo_Respuesta";
            indicador.Tables[1].TableName = "Tipo_Evidencia";
            indicador.Tables[2].TableName = "Indicador";
            indicador.Tables[3].TableName = "Inciso";
            indicador.Tables[4].TableName = "Afinidad";
            indicador.Tables[5].TableName = "PreguntaAdicional";
            indicador.Tables[6].TableName = "PrincipiosRSE";

            return indicador;

        }

        public DataSet Carga(int idTema, int idIndicador, int idCuestionario, bool bloqueado)
        {
            Database db;
            if (idCuestionario > 110)
                db = DatabaseFactory.CreateDatabase("ESR");
            else if (idCuestionario > 102)
                db = DatabaseFactory.CreateDatabase("ESR_2020");
            else if (idCuestionario > 94)
                db = DatabaseFactory.CreateDatabase("ESR_2019");
            else if (idCuestionario > 86)
                db = DatabaseFactory.CreateDatabase("ESR_2018");
            else //if (idCuestionario > 53)
                db = DatabaseFactory.CreateDatabase("ESR_2017");

            string sqlCommand = "CESR_CargaIndicador";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "idTema", DbType.Int32, idTema);
            db.AddInParameter(dbCommand, "idIndicador", DbType.Int32, idIndicador);
            db.AddInParameter(dbCommand, "idCuestionario", DbType.Int32, idCuestionario);
            db.AddInParameter(dbCommand, "bloqueado", DbType.Boolean, bloqueado);

            DataSet indicador = null;

            indicador = db.ExecuteDataSet(dbCommand);

            indicador.Tables[0].TableName = "Tipo_Respuesta";
            indicador.Tables[1].TableName = "Tipo_Evidencia";
            indicador.Tables[2].TableName = "Indicador";
            indicador.Tables[3].TableName = "Inciso";
            indicador.Tables[4].TableName = "Afinidad";
            indicador.Tables[5].TableName = "Pregunta_Adicional";
            indicador.Tables[6].TableName = "Grupos_Relacion";

            return indicador;

        }

        public DataSet CargaXSubtema(int idTema, int idSubtema)
        {
            Database db = DatabaseFactory.CreateDatabase();
            string sqlCommand = "CESR_CargaIndicadores";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "idTema", DbType.Int32, idTema);
            db.AddInParameter(dbCommand, "idSubtema", DbType.Int32, idSubtema);

            DataSet indicadores = null;

            indicadores = db.ExecuteDataSet(dbCommand);

            indicadores.Tables[0].TableName = "Tipo_Respuesta";
            indicadores.Tables[1].TableName = "Tipo_Evidencia";
            indicadores.Tables[2].TableName = "Indicador";
            indicadores.Tables[3].TableName = "Inciso";
            indicadores.Tables[4].TableName = "Afinidad";

            indicadores.Relations.Add("Afinidad",
                indicadores.Tables["Indicador"].Columns["idIndicador"],
                indicadores.Tables["Afinidad"].Columns["idIndicador"]);

            return indicadores;
        }

        public int CargarSiguienteId(int idTema)
        {
            int idIndicador = 0;
            Database db = DatabaseFactory.CreateDatabase();
            string sqlCommand = "CESR_CargaSiguienteIdIndicador";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "idTema", DbType.Int32, idTema);
            db.AddOutParameter(dbCommand, "idIndicador", DbType.Int32, idIndicador);

            db.ExecuteNonQuery(dbCommand);
            return Convert.ToInt32(dbCommand.Parameters["@idIndicador"].Value);
        }

        public bool Guardar(int idIndicador, int idSubtema, int idTema, float valor, 
            string descripcion, bool evidencia, bool obligatorio, string nota, 
            string corto, string idUsuario, string recomendacion, string evidencias,
            bool liberado, string descripcionAlterna, string descripcionPortugues)
        { 
            bool result = false;

            Database db = DatabaseFactory.CreateDatabase();

            // Two operations, one to credit an account, and one to debit another
            // account.
            string sqlCommand = "CESR_GuardarIndicador";
            DbCommand indicadorCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(indicadorCommand, "idIndicador", DbType.Int32, idIndicador);
			db.AddInParameter(indicadorCommand, "idSubtema", DbType.Int32, idSubtema);
            db.AddInParameter(indicadorCommand, "idTema", DbType.Int32, idTema);
            db.AddInParameter(indicadorCommand, "valor", DbType.Single, valor);
            db.AddInParameter(indicadorCommand, "descripcion", DbType.String, descripcion);
            db.AddInParameter(indicadorCommand, "evidencia", DbType.Boolean, evidencia);
            db.AddInParameter(indicadorCommand, "obligatorio", DbType.Boolean, obligatorio);
            db.AddInParameter(indicadorCommand, "nota", DbType.String, nota);
            db.AddInParameter(indicadorCommand, "corto", DbType.String, corto);
            db.AddInParameter(indicadorCommand, "idUsuario", DbType.String, idUsuario);
            db.AddInParameter(indicadorCommand, "recomendacion", DbType.String, recomendacion);
            db.AddInParameter(indicadorCommand, "evidencias", DbType.String, evidencias);
            db.AddInParameter(indicadorCommand, "liberado", DbType.Boolean, liberado);
            db.AddInParameter(indicadorCommand, "descripcionAlterna", DbType.String, descripcionAlterna);
            db.AddInParameter(indicadorCommand, "descripcionPortugues", DbType.String, descripcionPortugues);

            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Credit the first account
                    db.ExecuteNonQuery(indicadorCommand, transaction);
                    // Debit the second account
                    // db.ExecuteNonQuery(debitCommand, transaction);

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

        public DataSet CargaIndicadores(int idTema)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                string sqlCommand = "CESR_CargaIndicadores_x_Tema";
                DbCommand indicadoresCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(indicadoresCommand, "idTema", DbType.Int32, idTema);

                DataSet indicadores = null;

                indicadores = db.ExecuteDataSet(indicadoresCommand);
                indicadores.Tables[0].TableName = "Indicador";

                return indicadores;
            }
            catch (Exception ex)
            {
                throw new Exception("ESR.Data.Indicador.CargaIndicadores(int idTema, int idSubtema): " + ex.Message);
            }
        }

        public DataSet CargaIndicadores()
        {
            Database db = DatabaseFactory.CreateDatabase();
            string sqlCommand = "CESR_CargaTemas_Subtemas_Indicadores";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            DataSet temas = null;

            temas = db.ExecuteDataSet(dbCommand);

            temas.Tables[0].TableName = "Tema";
            temas.Tables[1].TableName = "Subtema";
            temas.Tables[2].TableName = "Indicador";

            temas.Relations.Add("subtema",
                temas.Tables["Tema"].Columns["idTema"],
                temas.Tables["Subtema"].Columns["idTema"]);

            DataColumn[] colsSubtemas = {temas.Tables["Subtema"].Columns["idTema"], temas.Tables["Subtema"].Columns["idSubtema"]};
            DataColumn[] colsIndicadores = {temas.Tables["Indicador"].Columns["idTema"], temas.Tables["Indicador"].Columns["idSubtema"]};

            //temas.Relations.Add("indicador",
            //    temas.Tables["Tema"].Columns["idTema"],
            //    temas.Tables["Indicador"].Columns["idTema"]);
            temas.Relations.Add("indicador",
                colsSubtemas,
                colsIndicadores);

            
            return temas;
        }

        public bool Eliminar(int idTema, int idIndicador)
        {
            bool result = false;

            Database db = DatabaseFactory.CreateDatabase();

            // En un futuro solo hay que hacer un update a la tabla Indicador para
            // realizar la eliminación lógica

            //string sqlCommand = "CESR_EliminarCuestionarioIndicador";
            //DbCommand eliminarCuestionarioIndicador = db.GetStoredProcCommand(sqlCommand);

            //db.AddInParameter(eliminarCuestionarioIndicador, "idTema", DbType.Int32, idTema);
            //db.AddInParameter(eliminarCuestionarioIndicador, "idIndicador", DbType.Int32, idIndicador);

            //sqlCommand = "CESR_EliminarRespuestaPreguntaAdicional";
            //DbCommand eliminarRespuestaPreguntaAdicional = db.GetStoredProcCommand(sqlCommand);

            //db.AddInParameter(eliminarRespuestaPreguntaAdicional, "idTema", DbType.Int32, idTema);
            //db.AddInParameter(eliminarRespuestaPreguntaAdicional, "idIndicador", DbType.Int32, idIndicador);

            string sqlCommand = "CESR_EliminarPreguntaAdicionalIndicador";
            DbCommand eliminarPreguntaAdicionalIndicador = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(eliminarPreguntaAdicionalIndicador, "idTema", DbType.Int32, idTema);
            db.AddInParameter(eliminarPreguntaAdicionalIndicador, "idIndicador", DbType.Int32, idIndicador);

            //sqlCommand = "CESR_EliminarEvidenciaIndicador";
            //DbCommand eliminarEvidenciaIndicador = db.GetStoredProcCommand(sqlCommand);

            //db.AddInParameter(eliminarEvidenciaIndicador, "idTema", DbType.Int32, idTema);
            //db.AddInParameter(eliminarEvidenciaIndicador, "idIndicador", DbType.Int32, idIndicador);

            //sqlCommand = "CESR_EliminarEvidenciaIncisos";
            //DbCommand eliminarEvidenciaIncisos = db.GetStoredProcCommand(sqlCommand);

            //db.AddInParameter(eliminarEvidenciaIncisos, "idTema", DbType.Int32, idTema);
            //db.AddInParameter(eliminarEvidenciaIncisos, "idIndicador", DbType.Int32, idIndicador);

            //sqlCommand = "CESR_EliminarRespuestaIndicador";
            //DbCommand eliminarRespuestaIndicador = db.GetStoredProcCommand(sqlCommand);

            //db.AddInParameter(eliminarRespuestaIndicador, "idTema", DbType.Int32, idTema);
            //db.AddInParameter(eliminarRespuestaIndicador, "idIndicador", DbType.Int32, idIndicador);

            //sqlCommand = "CESR_EliminarRespuestaIncisos";
            //DbCommand eliminarRespuestaIncisos = db.GetStoredProcCommand(sqlCommand);

            //db.AddInParameter(eliminarRespuestaIncisos, "idTema", DbType.Int32, idTema);
            //db.AddInParameter(eliminarRespuestaIncisos, "idIndicador", DbType.Int32, idIndicador);

            sqlCommand = "CESR_EliminarIncisos";
            DbCommand eliminarIncisosCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(eliminarIncisosCommand, "idTema", DbType.Int32, idTema);
            db.AddInParameter(eliminarIncisosCommand, "idIndicador", DbType.Int32, idIndicador);

            sqlCommand = "CESR_EliminarIndicadorAfinidad";
            DbCommand eliminarIndicadorAfinidad = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(eliminarIndicadorAfinidad, "idTema", DbType.Int32, idTema);
            db.AddInParameter(eliminarIndicadorAfinidad, "idIndicador", DbType.Int32, idIndicador);

            sqlCommand = "CESR_EliminarIndicador";
            DbCommand eliminarIndicadorCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(eliminarIndicadorCommand, "idTema", DbType.Int32, idTema);
            db.AddInParameter(eliminarIndicadorCommand, "idIndicador", DbType.Int32, idIndicador);

            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();

                try
                {
                    //db.ExecuteNonQuery(eliminarCuestionarioIndicador, transaction);
                    //db.ExecuteNonQuery(eliminarRespuestaPreguntaAdicional, transaction);
                    db.ExecuteNonQuery(eliminarPreguntaAdicionalIndicador, transaction);
                    //db.ExecuteNonQuery(eliminarEvidenciaIndicador, transaction);
                    //db.ExecuteNonQuery(eliminarEvidenciaIncisos, transaction);
                    //db.ExecuteNonQuery(eliminarRespuestaIndicador, transaction);
                    //db.ExecuteNonQuery(eliminarRespuestaIncisos, transaction);
                    db.ExecuteNonQuery(eliminarIncisosCommand, transaction);
                    db.ExecuteNonQuery(eliminarIndicadorAfinidad, transaction);
                    db.ExecuteNonQuery(eliminarIndicadorCommand, transaction);
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
