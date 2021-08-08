using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ESR.Data
{
    public class Perfil
    {
        public DataSet Carga(string perfiles)
        {
            Database db = DatabaseFactory.CreateDatabase();
            string sqlCommand = "CESR_CargaPerfiles";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "perfiles", DbType.String, perfiles);

            DataSet profiles = null;
            profiles = db.ExecuteDataSet(dbCommand);
            profiles.Tables[0].TableName = "Perfil";
            
            return profiles;
        }
    }
}
