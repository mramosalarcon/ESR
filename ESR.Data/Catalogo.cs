using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ESR.Data
{
    public class Catalogo
    {
        public DataSet Carga(string nombre)
        {
            string sqlCommand = "";
            switch (nombre.ToUpper())
            {
                case "PRINCIPIOSRSE":
                    sqlCommand = "CESR_CargaPrincipiosRSE";
                    break;
                case "AFINIDAD":
                    sqlCommand = "CESR_CargaAfinidades";
                    break;
                case "TEMA":
                    sqlCommand = "CESR_CargaTemas_Subtemas";
                    break;
                case "SUBTEMA":
                    sqlCommand = "CESR_CargaSubtemas";
                    break;
                case "RESPUESTA":
                    sqlCommand = "CESR_CargaTiposDeRespuesta";
                    break;
                case "PAIS":
                    sqlCommand = "CESR_CargaPaises";
                    break;
                case "ESTADO":
                    sqlCommand = "CESR_CargaEstados";
                    break;
            }

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            DataSet ds = null;
            ds = db.ExecuteDataSet(dbCommand);
            ds.Tables[0].TableName = nombre.ToUpper();
            return ds;

        }
    }
}
