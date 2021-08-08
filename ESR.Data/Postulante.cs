using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ESR.Data
{
    public class Postulante
    {
        public DataSet CargaPostulantes()
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                string sqlCommand = "CESR_CargaPostulantes";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

                DataSet postulantes = null;
                postulantes = db.ExecuteDataSet(dbCommand);
                postulantes.Tables[0].TableName = "Postulante";

                return postulantes;

            }
            catch (Exception ex)
            {
                throw new Exception("ESR.Data.Postulantes.CargaPostulantes(): " + ex.Message);
            }

        }
    }
}
