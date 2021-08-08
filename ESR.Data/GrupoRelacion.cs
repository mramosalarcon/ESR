using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ESR.Data
{
    public class GrupoRelacion
    {
        public bool Guardar(int idIndicador, int idTema, int idEmpresa, int idCuestionario, int idGrupo, string idUsuario)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                string sqlCommand = "CESR_GuardarGpoRlc";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(dbCommand, "idIndicador", DbType.Int32, idIndicador);
                db.AddInParameter(dbCommand, "idTema", DbType.Int32, idTema);
                db.AddInParameter(dbCommand, "idEmpresa", DbType.Int32, idEmpresa);
                db.AddInParameter(dbCommand, "idCuestionario", DbType.Int32, idCuestionario);
                db.AddInParameter(dbCommand, "idGrupo", DbType.Int32, idGrupo);
                db.AddInParameter(dbCommand, "idUsuario", DbType.String, idUsuario);

                db.ExecuteNonQuery(dbCommand);

                result = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un error en ESR.Data.GrupoRelacion.Guardar(): " + ex.Message);
            }
            return result;
        }

        public bool Eliminar(int idIndicador, int idTema, int idEmpresa, int idCuestionario, int idGrupo)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                string sqlCommand = "CESR_EliminarGpoRlc";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(dbCommand, "idIndicador", DbType.Int32, idIndicador);
                db.AddInParameter(dbCommand, "idTema", DbType.Int32, idTema);
                db.AddInParameter(dbCommand, "idEmpresa", DbType.Int32, idEmpresa);
                db.AddInParameter(dbCommand, "idCuestionario", DbType.Int32, idCuestionario);
                db.AddInParameter(dbCommand, "idGrupo", DbType.Int32, idGrupo);

                db.ExecuteNonQuery(dbCommand);

                result = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un error en ESR.Data.GrupoRelacion.Eliminar(): " + ex.Message);
            }
            return result;
        }
    }
}
