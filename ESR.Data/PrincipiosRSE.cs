using System;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;

namespace ESR.Data
{
    public class PrincipiosRSE
    {
        public bool Elimina(string descripcion, string idUsuario)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                string sqlCommand = "CESR_EliminaPrincipioRSE";
                DbCommand afinidadesCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(afinidadesCommand, "descripcion", DbType.String, descripcion);
                db.AddInParameter(afinidadesCommand, "idUsuario", DbType.String, idUsuario);

                db.ExecuteNonQuery(afinidadesCommand);
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }
        public bool Guarda(string descripcion, string idUsuario)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                string sqlCommand = "CESR_GuardaPrincipioRSE";
                DbCommand afinidadesCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(afinidadesCommand, "descripcion", DbType.String, descripcion);
                db.AddInParameter(afinidadesCommand, "idUsuario", DbType.String, idUsuario);

                db.ExecuteNonQuery(afinidadesCommand);
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public DataSet CargaTodos()
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                string sqlCommand = "CESR_CargaPrincipiosRSE";
                DbCommand afinidadesCommand = db.GetStoredProcCommand(sqlCommand);

                DataSet afinidades = null;

                afinidades = db.ExecuteDataSet(afinidadesCommand);
                afinidades.Tables[0].TableName = "PrincipiosRSE";

                return afinidades;
            }
            catch (Exception ex)
            {
                throw new Exception("ESR.Data.PrincipiosRSE.CargaTodos(): " + ex.Message);
            }
        }
    }
}
