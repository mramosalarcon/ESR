using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ESR.Data
{
    public class Mensajes
    {
        public DataSet CargaSolicitudesDeVinculacion(int idEmpresa)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                string sqlCommand = "CESR_CargaSolicitudesDeVinculacion";
                DbCommand command = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(command, "idEmpresa", DbType.Int32, idEmpresa);

                DataSet dsSolicitudes = null;

                dsSolicitudes = db.ExecuteDataSet(command);
                dsSolicitudes.Tables[0].TableName = "Solicitudes";

                return dsSolicitudes;
            }
            catch (Exception ex)
            {
                throw new Exception("ESR.Data.Mensajes.CargaSolicitudesDeVinculacion(int idEmpresa): " + ex.Message);
            }
        }
    }
}
