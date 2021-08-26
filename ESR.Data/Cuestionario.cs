using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ESR.Data
{
    public class Cuestionario
    {

        public DataSet DatosCuestionario(int idcuestionario)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                string sqlCommand = "CESR_DatosCuestionario";
                DbCommand indicadoresCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(indicadoresCommand, "idCuestionario", DbType.Int16, idcuestionario);

                DataSet indicadores = null;

                indicadores = db.ExecuteDataSet(indicadoresCommand);
                indicadores.Tables[0].TableName = "DatosCuestionario";

                return indicadores;
            }
            catch (Exception ex)
            {
                throw new Exception("ESR.Data.Indicador.CargaIndicadores(int idTema, int idSubtema): " + ex.Message);
            }
        }

        public DataSet LlenaGrid(int idcuestionario, int idtema)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                string sqlCommand = "CESR_LlenaGrid";
                DbCommand indicadoresCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(indicadoresCommand, "idCuestionario", DbType.Int16, idcuestionario);
                db.AddInParameter(indicadoresCommand, "idTema", DbType.Int16, idtema);

                DataSet indicadores = null;

                indicadores = db.ExecuteDataSet(indicadoresCommand);
                indicadores.Tables[0].TableName = "LLenaGrid";

                return indicadores;
            }
            catch (Exception ex)
            {
                throw new Exception("ESR.Data.Indicador.CargaIndicadores(int idTema, int idSubtema): " + ex.Message);
            }
        }


        public bool EliminaIndicadoresCuestionario(int idcuestionario, int idIndicador, int idTema)
        {
            bool result = false;

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                string sqlCommand = "CESR_EliminarIndicadorescuestionaio";
                DbCommand dbCuestionarioAvance = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(dbCuestionarioAvance, "idCuestionario", DbType.Int16, idcuestionario);
                db.AddInParameter(dbCuestionarioAvance, "idIndicador", DbType.Int16, idIndicador);
                db.AddInParameter(dbCuestionarioAvance, "idTema", DbType.Int16, idTema);
                db.ExecuteNonQuery(dbCuestionarioAvance);
                result = true;
            }
            catch
            {
                result = false;
            }

            return result;
        }


        public bool GuardaIndicadoresCuestionario(int idCuestionario, int idIndicador, int idTema, string idUsuario, int Ordinal)//, string usuariomodificacion, DateTime fechamodificacion)
        {
            bool result = false;

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                string sqlCommand = "CESR_GuardarIndicadoresCuestionario";
                DbCommand dbCuestionarioAvance = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(dbCuestionarioAvance, "idCuestionario", DbType.Int32, idCuestionario);
                db.AddInParameter(dbCuestionarioAvance, "idIndicador", DbType.Int32, idIndicador);
                db.AddInParameter(dbCuestionarioAvance, "idTema", DbType.Int32, idTema);
                db.AddInParameter(dbCuestionarioAvance, "idUsuario", DbType.String, idUsuario);
                db.AddInParameter(dbCuestionarioAvance, "Ordinal", DbType.Int32, Ordinal);

                db.ExecuteNonQuery(dbCuestionarioAvance);
                result = true;
            }
            catch
            {
                result = false;
            }

            return result;
        }

        public DataSet Traeidcuestionario(string nombre)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                string sqlCommand = "CESR_Traeidcuestionario";
                DbCommand indicadoresCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(indicadoresCommand, "nombre", DbType.String, nombre);

                DataSet indicadores = null;

                indicadores = db.ExecuteDataSet(indicadoresCommand);
                indicadores.Tables[0].TableName = "IdCuestionario";

                return indicadores;
            }
            catch (Exception ex)
            {
                throw new Exception("ESR.Data.Indicador.CargaIndicadores(int idTema, int idSubtema): " + ex.Message);
            }
        }


        public bool GuardaCuestionario(int idCuestionario, string nombre, string descripcion, int anio, bool bloqueado, string usuariocreacion, DateTime fechacreacion, string usuariomodificacion, DateTime fechamodificacion)
        {
            bool result = false;

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                string sqlCommand = "CESR_GuardarCuestionario";
                DbCommand dbCuestionarioAvance = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(dbCuestionarioAvance, "idCuestionario", DbType.Int32, idCuestionario);
                db.AddInParameter(dbCuestionarioAvance, "nombre", DbType.String, nombre);
                db.AddInParameter(dbCuestionarioAvance, "descripcion", DbType.String, descripcion);
                db.AddInParameter(dbCuestionarioAvance, "anio", DbType.Int16, anio);
                db.AddInParameter(dbCuestionarioAvance, "bloqueado", DbType.Boolean, bloqueado);
                db.AddInParameter(dbCuestionarioAvance, "usuariocreacion", DbType.String, usuariocreacion);
                db.AddInParameter(dbCuestionarioAvance, "fechacreacion", DbType.Date, fechacreacion);
                db.AddInParameter(dbCuestionarioAvance, "usuariomodificacion", DbType.String, usuariomodificacion);
                db.AddInParameter(dbCuestionarioAvance, "fechamodificacion", DbType.Date, fechamodificacion);


                db.ExecuteNonQuery(dbCuestionarioAvance);
                result = true;
            }
            catch 
            {
                result = false;
            }

            return result;
        }

        public bool LiberaCuestionario(int idCuestionario, int idEmpresa, string idUsuario)
        {
            bool result = false;

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                string sqlCommand = "CESR_LiberarCuestionario";
                DbCommand dbCuestionarioAvance = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(dbCuestionarioAvance, "idCuestionario", DbType.Int32, idCuestionario);
                db.AddInParameter(dbCuestionarioAvance, "idEmpresa", DbType.Int32, idEmpresa);
                db.AddInParameter(dbCuestionarioAvance, "idUsuario", DbType.String, idUsuario);


                db.ExecuteNonQuery(dbCuestionarioAvance);
                result = true;
            }
            catch
            {
                result = false;
            }

            return result;
        }

        public DataSet CargaAvanceXTema(int idCuestionario, int idEmpresa, int idTema)
        {
            Database db = DatabaseFactory.CreateDatabase();
            string sqlCommand = "CESR_CargaAvanceCuestionarioXTema";
            DbCommand dbCuestionarioAvance = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCuestionarioAvance, "idCuestionario", DbType.Int32, idCuestionario);
            db.AddInParameter(dbCuestionarioAvance, "idEmpresa", DbType.Int32, idEmpresa);
            db.AddInParameter(dbCuestionarioAvance, "idTema", DbType.Int32, idTema);

            DataSet dsRespuestas = null;
            dsRespuestas = db.ExecuteDataSet(dbCuestionarioAvance);

            dsRespuestas.Tables[0].TableName = "Tema";
            dsRespuestas.Tables[1].TableName = "Subtema";
            dsRespuestas.Tables[2].TableName = "Indicador";
            dsRespuestas.Tables[3].TableName = "Inciso";
            dsRespuestas.Tables[4].TableName = "Respuesta_Indicador";
            dsRespuestas.Tables[5].TableName = "Respuesta_Inciso";
            dsRespuestas.Tables[6].TableName = "Tipo_Evidencia";
            dsRespuestas.Tables[7].TableName = "Empresa";

            dsRespuestas.Relations.Add("S",
                          dsRespuestas.Tables["Tema"].Columns["idTema"],
                          dsRespuestas.Tables["Subtema"].Columns["idTema"]);

            //dsRespuestas.Relations.Add("I",
            //              dsRespuestas.Tables["Tema"].Columns["idTema"],
            //              dsRespuestas.Tables["Indicador"].Columns["idTema"]);

            //DataColumn[] colsIndicador = { dsRespuestas.Tables["Indicador"].Columns["idIndicador"], dsRespuestas.Tables["Indicador"].Columns["idTema"] };
            //DataColumn[] colsRespuesta = { dsRespuestas.Tables["Respuesta_Indicador"].Columns["idIndicador"], dsRespuestas.Tables["Respuesta_Indicador"].Columns["idTema"] };

            //dsRespuestas.Relations.Add("RI",
            //    colsIndicador,
            //    colsRespuesta);

            return dsRespuestas;

        }

        public DataSet CargaAvance(int idCuestionario, int idEmpresa, bool bIndicadoresExtra)
        {
            try
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

                string sqlCommand = "CESR_CargaAvanceCuestionario";
                DbCommand dbCuestionarioAvance = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(dbCuestionarioAvance, "idCuestionario", DbType.Int32, idCuestionario);
                db.AddInParameter(dbCuestionarioAvance, "idEmpresa", DbType.Int32, idEmpresa);
                db.AddInParameter(dbCuestionarioAvance, "indicadoresExtra", DbType.Boolean, bIndicadoresExtra);
                dbCuestionarioAvance.CommandTimeout = 0;

                DataSet dsRespuestas = null;
                dsRespuestas = db.ExecuteDataSet(dbCuestionarioAvance);

                dsRespuestas.Tables[0].TableName = "Tema";
                dsRespuestas.Tables[1].TableName = "Subtema";
                dsRespuestas.Tables[2].TableName = "Indicador";
                //dsRespuestas.Tables[3].TableName = "Inciso";
                dsRespuestas.Tables[3].TableName = "Respuesta_Indicador";
                dsRespuestas.Tables[4].TableName = "Evidencia_Indicador";
                //dsRespuestas.Tables[6].TableName = "Respuesta_Inciso";
                //dsRespuestas.Tables[7].TableName = "Evidencia_Inciso";
                dsRespuestas.Tables[5].TableName = "Tipo_Evidencia";
                dsRespuestas.Tables[6].TableName = "Empresa";

                dsRespuestas.Relations.Add("S",
                              dsRespuestas.Tables["Tema"].Columns["idTema"],
                              dsRespuestas.Tables["Subtema"].Columns["idTema"]);

                //dsRespuestas.Relations.Add("I",
                //              dsRespuestas.Tables["Tema"].Columns["idTema"],
                //              dsRespuestas.Tables["Indicador"].Columns["idTema"]);

                //DataColumn[] colsIndicador = { dsRespuestas.Tables["Indicador"].Columns["idIndicador"], dsRespuestas.Tables["Indicador"].Columns["idTema"] };
                //DataColumn[] colsRespuesta = { dsRespuestas.Tables["Respuesta_Indicador"].Columns["idIndicador"], dsRespuestas.Tables["Respuesta_Indicador"].Columns["idTema"] };

                //dsRespuestas.Relations.Add("RI",
                //    colsIndicador,
                //    colsRespuesta);

                return dsRespuestas;
            }
            catch (Exception ex)
            {
                throw new Exception("ESR.Data.Cuestionario.CargaAvance(). Error: " + ex.Message + " " + ex.InnerException, ex);
            }
        }

        public DataSet Carga(int idCuestionario, int idEmpresa)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                string sqlCommand = "CESR_CargaCuestionario";
                DbCommand dbCuestionarioCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(dbCuestionarioCommand, "idCuestionario", DbType.Int32, idCuestionario);
                db.AddInParameter(dbCuestionarioCommand, "idEmpresa", DbType.Int32, idEmpresa);

                DataSet dsCuestionario = null;
                dsCuestionario = db.ExecuteDataSet(dbCuestionarioCommand);

                dsCuestionario.Tables[0].TableName = "Tema";
                dsCuestionario.Tables[1].TableName = "Subtema";
                dsCuestionario.Tables[2].TableName = "Indicador";
                dsCuestionario.Tables[3].TableName = "Cuestionario";

                dsCuestionario.Relations.Add("subtema",
                    dsCuestionario.Tables["Tema"].Columns["idTema"],
                    dsCuestionario.Tables["Subtema"].Columns["idTema"]);

                DataColumn[] colsSubtemas = { dsCuestionario.Tables["Subtema"].Columns["idTema"], dsCuestionario.Tables["Subtema"].Columns["idSubtema"] };
                DataColumn[] colsIndicadores = { dsCuestionario.Tables["Indicador"].Columns["idTema"], dsCuestionario.Tables["Indicador"].Columns["idSubtema"] };

                dsCuestionario.Relations.Add("indicador",
                    colsSubtemas,
                    colsIndicadores);

                return dsCuestionario;
            }
            catch (Exception ex)
            {
                throw new Exception("ESR.Data.Cuestionario.Carga(int idCuestionario, int idEmpresa): " + ex.Message);
            }
        }

        public DataSet CargaCuestionarios(int idEmpresa, bool liberado)
        {
            Database db = DatabaseFactory.CreateDatabase();
            string sqlCommand = "CESR_CargaCuestionarios";
            DbCommand dbCuestionarioCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCuestionarioCommand, "idEmpresa", DbType.Int32, idEmpresa);
            db.AddInParameter(dbCuestionarioCommand, "liberado", DbType.Boolean, liberado);

            DataSet dsCuestionarios = null;
            dsCuestionarios = db.ExecuteDataSet(dbCuestionarioCommand);

            dsCuestionarios.Tables[0].TableName = "Cuestionario";

            return dsCuestionarios;
        }

        public DataSet cargaActivos()
        {
            Database db = DatabaseFactory.CreateDatabase();
            string sqlCommand = "CESR_cargaCuestionariosActivos";
            DbCommand storedProcCommand = db.GetStoredProcCommand(sqlCommand);
            DataSet ds = db.ExecuteDataSet(storedProcCommand);
            ds.Tables[0].TableName = "Cuestionario";
            return ds;
        }

        public DataSet CargaTodos()
        {
            Database db = DatabaseFactory.CreateDatabase();
            string sqlCommand = "CESR_CargaTodosLosCuestionarios";
            DbCommand dbCuestionarioCommand = db.GetStoredProcCommand(sqlCommand);

            DataSet dsCuestionarios = null;
            dsCuestionarios = db.ExecuteDataSet(dbCuestionarioCommand);

            dsCuestionarios.Tables[0].TableName = "Cuestionario";

            return dsCuestionarios;
        }



        public DataSet CargaCuestionariosAdmin()
        {
            Database db = DatabaseFactory.CreateDatabase();
            string sqlCommand = "CESR_CargaCuestionariosAdmin";
            DbCommand dbCuestionarioCommand = db.GetStoredProcCommand(sqlCommand);

            DataSet dsCuestionarios = null;
            dsCuestionarios = db.ExecuteDataSet(dbCuestionarioCommand);

            dsCuestionarios.Tables[0].TableName = "CargaCuestionariosAdmin";

            return dsCuestionarios;
        }

        public DataSet TraeIndicadores(int idCuestionario, int idTema)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                string sqlCommand = "CESR_TraeIndicadores_x_Tema";
                DbCommand indicadoresCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(indicadoresCommand, "idCuestionario", DbType.Int32, idCuestionario);
                db.AddInParameter(indicadoresCommand, "idTema", DbType.Int32, idTema);

                DataSet indicadores = null;

                indicadores = db.ExecuteDataSet(indicadoresCommand);
                indicadores.Tables[0].TableName = "Indicador";

                return indicadores;
            }
            catch (Exception ex)
            {
                throw new Exception("ESR.Data.Indicador.CargaIndicadores(int idTema, int idSubtema): " + ex.Message);
            }
        }
    }
}
