using System;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;

namespace ESR.Data
{
    public class Afinidad
    {
        public bool Elimina(string descripcion, string idUsuario)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                string sqlCommand = "CESR_EliminaAfinidad";
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
        public bool Guarda(string descripcion, string idUsuario, int idAfinidad)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                string sqlCommand = "CESR_GuardaAfinidad";
                DbCommand afinidadesCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(afinidadesCommand, "descripcion", DbType.String, descripcion);
                db.AddInParameter(afinidadesCommand, "idUsuario", DbType.String, idUsuario);
                db.AddInParameter(afinidadesCommand, "idAfin", DbType.Int32, idAfinidad);

                db.ExecuteNonQuery(afinidadesCommand);
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public DataSet CargaTodas()
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                string sqlCommand = "CESR_CargaAfinidades";
                DbCommand afinidadesCommand = db.GetStoredProcCommand(sqlCommand);

                DataSet afinidades = null;

                afinidades = db.ExecuteDataSet(afinidadesCommand);
                afinidades.Tables[0].TableName = "Afinidad";

                return afinidades;
            }
            catch (Exception ex)
            {
                throw new Exception("ESR.Data.Afinidad.CargaTodas(): " + ex.Message);
            }
        }
    }
}
