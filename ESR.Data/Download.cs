using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ESR.Data
{
    public class Download
    {
        public DataSet CargaTiposDeEvidencia()
        {
            Database db = DatabaseFactory.CreateDatabase();
            string sqlCommand = "CESR_TraeArchivo";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            DataSet dstraearchivo = null;

            dstraearchivo = db.ExecuteDataSet(dbCommand);
            return dstraearchivo;
        }
    }
}
