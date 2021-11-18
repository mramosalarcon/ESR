using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.IO;

namespace ESR.Data
{
    public class Ranking
    {
        public bool PreparaRanking(int idCuestionario, bool flgGenera)
        {
            bool result = false;
            try
            {
                if (flgGenera)
                {
                    string sqlCommand = "Delete from Ranking where idCuestionario = " + idCuestionario;
                    Database db = DatabaseFactory.CreateDatabase();
                    DbCommand dbRanking = db.GetSqlStringCommand(sqlCommand);
                    dbRanking.CommandTimeout = 0;

                    db.ExecuteNonQuery(dbRanking);
                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("ESR.Data.Ranking.PreparaRanking(). Error: " + ex.Message, ex);
            }
        }
        public DataSet CargaDetalleRanking(int idEmpresa, int idCuestionario)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                string sqlCommand = "CESR_CargarDetalleRankingEmpresa";
                DbCommand dbRanking = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(dbRanking, "idEmpresa", DbType.Int32, idEmpresa);
                db.AddInParameter(dbRanking, "idCuestionario", DbType.Int32, idCuestionario);
                
                DataSet dsRanking = null;
                dsRanking = db.ExecuteDataSet(dbRanking);

                dsRanking.Tables[0].TableName = "Ranking";

                return dsRanking;
            }
            catch (Exception ex)
            {
                throw new Exception ("ESR.Data.Ranking.CargaDetalleRanking(). Error: " + ex.Message, ex);
            }
        }
        public DataSet CargaRanking(int idEmpresa, int idCuestionario)
        {
            //bool result = false;

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                string sqlCommand = "CESR_CargarRankingEmpresa";
                DbCommand dbCuestionarioAvance = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(dbCuestionarioAvance, "idCuestionario", DbType.Int32, idCuestionario);
                db.AddInParameter(dbCuestionarioAvance, "idEmpresa", DbType.Int32, idEmpresa);

                DataSet dsRanking = null;
                dsRanking = db.ExecuteDataSet(dbCuestionarioAvance);

                dsRanking.Tables[0].TableName = "Ranking";
                dsRanking.Tables[1].TableName = "Menor";
                dsRanking.Tables[2].TableName = "Promedio";
                dsRanking.Tables[3].TableName = "Mayor";
                dsRanking.Tables[4].TableName = "Lider";
                dsRanking.Tables[5].TableName = "Posicion";
                dsRanking.Tables[6].TableName = "Tema";

                return dsRanking;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool GuardarPromedio(int idEmpresa, int idCuestionario, int idTema, int idSubtema, float promedio, float promedioGral, string idUsuario, string recomendaciones, float porcentajeFaseI, float porcentajeFaseII, float porcentajeFaseIII)
        {
            bool result = false;

            try
            {
                Database db = DatabaseFactory.CreateDatabase();

                string sqlCommand = "CESR_GuardarRanking";
                DbCommand empresaCommand = db.GetStoredProcCommand(sqlCommand);
                empresaCommand.CommandTimeout = 0;

                db.AddInParameter(empresaCommand, "idEmpresa", DbType.Int32, idEmpresa);
                db.AddInParameter(empresaCommand, "idCuestionario", DbType.Int32, idCuestionario);
                db.AddInParameter(empresaCommand, "idTema", DbType.Int32, idTema);
                db.AddInParameter(empresaCommand, "idSubtema", DbType.Int32, idSubtema);
                db.AddInParameter(empresaCommand, "promedio", DbType.Single, promedio);
                db.AddInParameter(empresaCommand, "promedioGral", DbType.Single, promedioGral);
                db.AddInParameter(empresaCommand, "idUsuario", DbType.String, idUsuario);
                db.AddInParameter(empresaCommand, "recomendaciones", DbType.String, recomendaciones);
                db.AddInParameter(empresaCommand, "porcentajeFaseI", DbType.Single, porcentajeFaseI);
                db.AddInParameter(empresaCommand, "porcentajeFaseII", DbType.Single, porcentajeFaseII);
                db.AddInParameter(empresaCommand, "porcentajeFaseIII", DbType.Single, porcentajeFaseIII);

                db.ExecuteNonQuery(empresaCommand);

                result = true;
            }
            catch (Exception ex)
            {
                
                StreamWriter sw = File.AppendText("e:\\temp\\ranking.log");
                sw.AutoFlush = true;
                sw.WriteLine(DateTime.Now.ToString() + ": Error: " + ex.Message);
                sw.Close();
                result = false;
            }

            return result;
        }

    }
}
