using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;


namespace ESR.Data
{
    public class Sector
    {
        public DataSet CargaSectores()
        {
            Database db = DatabaseFactory.CreateDatabase();
            string sqlCommand = "CESR_CargaSectores";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            DataSet sectores = db.ExecuteDataSet(dbCommand);
            sectores.Tables[0].TableName = "Sector";

            return sectores;
        }

        public DataSet CargaSubSectores(int idSector)
        {
            Database db = DatabaseFactory.CreateDatabase();
            string sqlCommand = "CESR_CargaSubSectores";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "idSector", DbType.Int32, idSector);
            
            DataSet SubSectores = db.ExecuteDataSet(dbCommand);
            SubSectores.Tables[0].TableName = "SubSector";

            return SubSectores;
        }
    }
}
