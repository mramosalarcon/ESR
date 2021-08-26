using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;


namespace ESR.Data
{
    public class Respuesta
    {
        public DataSet CargaTiposDeEvidencia()
        {
            Database db = DatabaseFactory.CreateDatabase();
            string sqlCommand = "CESR_CargaTiposDeEvidencia";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            DataSet rsTiposDeEvidencia = null;

            rsTiposDeEvidencia = db.ExecuteDataSet(dbCommand);
            return rsTiposDeEvidencia;
        }

        public DataSet CargaTiposDeRespuesta()
        {
            Database db = DatabaseFactory.CreateDatabase();
            string sqlCommand = "CESR_CargaTiposDeRespuesta";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            DataSet rsTiposDeRespuesta = null;

            rsTiposDeRespuesta = db.ExecuteDataSet(dbCommand);
            return rsTiposDeRespuesta;

        }

        public DataSet Carga(int idIndicador, int idTema, int idEmpresa, int idCuestionario)
        {
            Database db;
            if (idCuestionario > 121)
                db = DatabaseFactory.CreateDatabase("ESR");
            else if (idCuestionario > 110)
                db = DatabaseFactory.CreateDatabase("ESR_2021");
            else if (idCuestionario > 102)
                db = DatabaseFactory.CreateDatabase("ESR_2020");
            else if (idCuestionario > 94)
                db = DatabaseFactory.CreateDatabase("ESR_2019");
            else if (idCuestionario > 86)
                db = DatabaseFactory.CreateDatabase("ESR_2018");
            else //if (idCuestionario > 53)
                db = DatabaseFactory.CreateDatabase("ESR_2017");


            string sqlCommand = "CESR_CargaRespuestas";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "idIndicador", DbType.Int32, idIndicador);
            db.AddInParameter(dbCommand, "idTema", DbType.Int32, idTema);
            db.AddInParameter(dbCommand, "idEmpresa", DbType.Int32, idEmpresa);
            db.AddInParameter(dbCommand, "idCuestionario", DbType.Int32, idCuestionario);

            DataSet rsRespuestas = null;

            rsRespuestas = db.ExecuteDataSet(dbCommand);

            rsRespuestas.Tables[0].TableName = "Respuesta_Indicador";
            rsRespuestas.Tables[1].TableName = "Respuesta_Inciso";
            rsRespuestas.Tables[2].TableName = "Evidencia_Indicador";
            rsRespuestas.Tables[3].TableName = "Evidencia_Inciso";
            rsRespuestas.Tables[4].TableName = "Respuesta_Pregunta_Adicional";
            rsRespuestas.Tables[5].TableName = "Respuesta_Grupo";
            //rsRespuestas.Tables[6].TableName = "Reelevancia_Tema";


            return rsRespuestas;
        }

        public DataSet Legalidad(int idTema, int idEmpresa, int idCuestionario)
        {
            
            Database db = DatabaseFactory.CreateDatabase();
            string sqlCommand = "CESR_VerificarLegalidad";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "idTema", DbType.Int32, idTema);
            db.AddInParameter(dbCommand, "idEmpresa", DbType.Int32, idEmpresa);
            db.AddInParameter(dbCommand, "idCuestionario", DbType.Int32, idCuestionario);

            DataSet dsLegalidad = null;
            dsLegalidad = db.ExecuteDataSet(dbCommand);
            return dsLegalidad;
        }

    }
}
