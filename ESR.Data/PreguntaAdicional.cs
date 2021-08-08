using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ESR.Data
{
    public class PreguntaAdicional
    {
        public bool GuardarRespuestaEmpresa(int idTema, int idIndicador, int idCuestionario, int idPregunta, int idEmpresa, string respuesta, string idUsuario)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                string sqlCommand = "CESR_GuardarRespuestaPA";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(dbCommand, "idIndicador", DbType.Int32, idIndicador);
                db.AddInParameter(dbCommand, "idTema", DbType.Int32, idTema);
                db.AddInParameter(dbCommand, "idCuestionario", DbType.Int32, idCuestionario);
                db.AddInParameter(dbCommand, "idPregunta", DbType.Int32, idPregunta);
                db.AddInParameter(dbCommand, "idEmpresa", DbType.Int32, idEmpresa);
                db.AddInParameter(dbCommand, "Respuesta", DbType.String, respuesta);
                db.AddInParameter(dbCommand, "idUsuario", DbType.String, idUsuario);

                db.ExecuteNonQuery(dbCommand);

                result = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un error en ESR.Data.PreguntaAdicional.GuardarRespuestaEmpresa(): " + ex.Message);
            }
            return result;
        }

        public bool Guardar(int idTema, int idIndicador, int idPregunta, string pregunta, bool obligatorio)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                string sqlCommand = "CESR_GuardarPA";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(dbCommand, "idTema", DbType.Int32, idTema);
                db.AddInParameter(dbCommand, "idIndicador", DbType.Int32, idIndicador);
                db.AddInParameter(dbCommand, "idPregunta", DbType.Int32, idPregunta);
                db.AddInParameter(dbCommand, "pregunta", DbType.String, pregunta);
                db.AddInParameter(dbCommand, "obligatorio", DbType.Boolean, obligatorio);

                db.ExecuteNonQuery(dbCommand);

                result = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un error en CEMEFI.CESR.PreguntaAdicional.Guardar(): " + ex.Message);
            }
            return result;
        }

        public bool Eliminar(int idTema, int idIndicador, int idPregunta)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                string sqlCommand = "CESR_EliminarPA";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(dbCommand, "idTema", DbType.Int32, idTema);
                db.AddInParameter(dbCommand, "idIndicador", DbType.Int32, idIndicador);
                db.AddInParameter(dbCommand, "idPregunta", DbType.Int32, idPregunta);

                db.ExecuteNonQuery(dbCommand);

                result = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un error en CEMEFI.CESR.PreguntaAdicional.Eliminar(): " + ex.Message);
            }
            return result;
        }
        public DataSet CargaPAs(int idTema, int idIndicador)
        {
            Database db = DatabaseFactory.CreateDatabase();
            string sqlCommand = "CESR_CargaPAs";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "idTema", DbType.Int32, idTema);
            db.AddInParameter(dbCommand, "idIndicador", DbType.Int32, idIndicador);

            DataSet dsPAs = null;
            dsPAs = db.ExecuteDataSet(dbCommand);

            dsPAs.Tables[0].TableName = "PreguntaAdicional";

            return dsPAs;
        }
    }
}
