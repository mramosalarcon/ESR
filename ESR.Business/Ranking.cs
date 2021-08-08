using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace ESR.Business
{
    public class Ranking
    {
        private int _iIdEmpresa;
        private int _iIdCuestionario;
        private float _fPromedio;
        private string _sIdUsuario;
        private int _iIdTema;
        private float _fPromedioGral;
        private int _iIdSubtema;
        private string _sRecomendaciones;
    
        public int idCuestionario
        {
            get
            {
                return _iIdCuestionario;
            }
            set
            {
                _iIdCuestionario = value;
            }
        }

        public float promedio
        {
            get
            {
                return _fPromedio;
            }
            set
            {
                _fPromedio = value;
            }
        }

        public int idEmpresa
        {
            get
            {
                return _iIdEmpresa;
            }
            set
            {
                _iIdEmpresa = value;
            }
        }

        public string idUsuario
        {
            get
            {
                return _sIdUsuario;
            }
            set
            {
                _sIdUsuario = value;
            }
        }

        public int idTema
        {
            get
            {
                return _iIdTema;
            }
            set
            {
                _iIdTema = value;
            }
        }

        public float promedioGral
        {
            get
            {
                return _fPromedioGral;
            }
            set
            {
                _fPromedioGral = value;
            }
        }

        public int idSubtema
        {
            get
            {
                return _iIdSubtema;
            }
            set
            {
                _iIdSubtema = value;
            }
        }

        public string recomendaciones
        {
            get
            {
                return _sRecomendaciones;
            }
            set
            {
                _sRecomendaciones = value;
            }
        }

        public DataSet CargaDetalleRanking()
        {
            try
            {

                ESR.Data.Ranking rank = new ESR.Data.Ranking();
                return rank.CargaDetalleRanking(this.idEmpresa, this.idCuestionario);

            }
            catch (Exception ex)
            {
                throw new Exception ("ESR.Business.Ranking.CargaDetalleRanking(). Error: " + ex.Message, ex);
            }
        }

        public bool GeneraRanking(bool flgGenera)
        {

            bool result = false;            
            ESR.Data.Ranking rank = new ESR.Data.Ranking();
            //StreamWriter sw = File.AppendText("d:\\temp\\monitor_ranking.log");
            //sw.AutoFlush = true;

            if (rank.PreparaRanking(this.idCuestionario, flgGenera))
            {
                try
                {
                    Empresa empresas = new Empresa();
                    empresas.idCuestionario = this.idCuestionario;
                    empresas.liberado = true; //No esta funcionando esta opci�n, se trae todas las empresas. MRA: 17/02/2012: 02:56
                    DataSet dsTodas = empresas.CargaTodas();

                    foreach (DataRow drEmpresa in dsTodas.Tables["Empresa"].Rows)
                    {
                        Cuestionario cuestionarioTodas = new Cuestionario();
                        cuestionarioTodas.idEmpresa = Convert.ToInt32(drEmpresa["idEmpresa"].ToString());
                        cuestionarioTodas.idCuestionario = this.idCuestionario;
                        DataSet reporteTodas = null;

                        Task t = Task.Factory.StartNew(() =>
                        {
                            reporteTodas = cuestionarioTodas.CargaAvance();
                        });
                        t.Wait();

                        float fPromedioGeneral = 0;
                        // La siguiente no está
                        float fPromedioGRSE = 0;
                        bool flgGRSE = false;
                        foreach (DataRow drTema in reporteTodas.Tables["Tema"].Rows)
                        {
                            // Para cada subtema hay que calcular el promedio. Para guardarlo en la tabla de Ranking
                            float fPromedioIndicadorTema = 0;
                            foreach (DataRow drSubtema in reporteTodas.Tables["Subtema"].Select("idTema = " + drTema["idTema"].ToString()))
                            {
                                string sRecomendaciones = "";
                                float fPromedioIndicadorSubtema = 0;
                                // Ciclo de los indicadores, del tema del cuestionario
                                foreach (DataRow drIndicador in reporteTodas.Tables["Indicador"].Select("idTema = " + drTema["idTema"].ToString() + " And idSubtema = " + drSubtema["idSubtema"].ToString()))
                                {
                                    //float calificacionIndicador = 0.5F;
                                    float calificacionIndicador = 0;
                                    DataRow[] rowRI = reporteTodas.Tables["Respuesta_Indicador"].Select("idIndicador = " + drIndicador["idIndicador"].ToString() + " And idTema = " + drIndicador["idTema"].ToString());
                                    if (rowRI.GetLength(0) > 0) //Hay respuesta. Solo es un registro. Pendiente: considerar el factor de antig�edad
                                    {
                                        //Realiza el c�lculo del promedio del indicador
                                        //if (reporteTodas.Tables["Inciso"].Select("idIndicador = " + drIndicador["idIndicador"].ToString() + " And idTema = " + drTema["idTema"].ToString()).GetLength(0) > 0 && this.idCuestionario != 24)
                                        //#region Calculo del promedio de Incisos
                                        //{
                                        //    //Tiene incisos
                                        //    if (Convert.ToInt32(rowRI[0]["idTipoRespuesta"].ToString()) > 2)
                                        //    {
                                        //        calificacionIndicador = 3;
                                        //        foreach (DataRow drInciso in reporteTodas.Tables["Inciso"].Select("idIndicador = " + drIndicador["idIndicador"].ToString() + " And idTema = " + drTema["idTema"].ToString()))
                                        //        {
                                        //            float fPromedioInciso = 0;
                                        //            bool bEvidencia = false;
                                        //            //bool bUnaVez = false;
                                        //            int idTE = 0;

                                        //            // Incisos del indicador. Sacar promedio.
                                        //            foreach (DataRow drRespuestaInciso in reporteTodas.Tables["Respuesta_Inciso"].Select("idIndicador = " + drIndicador["idIndicador"].ToString() + " And idTema = " + drTema["idTema"].ToString() + " And idInciso = '" + drInciso["idInciso"].ToString() + "'"))
                                        //            {

                                        //                //if (!bUnaVez)
                                        //                //{
                                        //                //    if (drRespuestaInciso["valor"].ToString() != "")
                                        //                //    {
                                        //                fPromedioInciso += Convert.ToSingle(drRespuestaInciso["valor"].ToString());
                                        //                //    }
                                        //                //    bUnaVez = true;
                                        //                //}


                                        //                foreach (DataRow drEvidenciaInciso in reporteTodas.Tables["Evidencia_Inciso"].Select("idIndicador = " + drIndicador["idIndicador"].ToString() + " And idTema = " + drTema["idTema"].ToString() + " And idInciso = '" + drInciso["idInciso"].ToString() + "'"))
                                        //                {
                                        //                    if (!bEvidencia)
                                        //                    {
                                        //                        //fPromedioInciso += 1;
                                        //                        bEvidencia = true;
                                        //                    }

                                        //                    if (idTE != Convert.ToInt32(drEvidenciaInciso["idTipoEvidencia"].ToString()))
                                        //                    {

                                        //                        fPromedioInciso += Convert.ToSingle(drEvidenciaInciso["valorEvidencia"].ToString());
                                        //                        idTE = Convert.ToInt32(drEvidenciaInciso["idTipoEvidencia"].ToString());
                                        //                    }
                                        //                }

                                        //                //if (drRespuestaInciso["idTipoEvidencia"].ToString() != "")
                                        //                //{


                                        //                //    if (idTE != Convert.ToInt32(drRespuestaInciso["idTipoEvidencia"].ToString()))
                                        //                //    {

                                        //                //        foreach (DataRow drTipoEvidencia in reporteTodas.Tables["Tipo_Evidencia"].Rows)
                                        //                //        {
                                        //                //            if (drTipoEvidencia["idTipoEvidencia"].ToString() == drRespuestaInciso["idTipoEvidencia"].ToString())
                                        //                //            {
                                        //                //                fPromedioInciso += Convert.ToSingle(drTipoEvidencia["valor"].ToString());
                                        //                //                break;
                                        //                //            }
                                        //                //        }

                                        //                //        idTE = Convert.ToInt32(drRespuestaInciso["idTipoEvidencia"].ToString());
                                        //                //    }
                                        //                //}
                                        //            }
                                        //            calificacionIndicador += fPromedioInciso;
                                        //        }
                                        //        calificacionIndicador = calificacionIndicador / (reporteTodas.Tables["Inciso"].Select("idIndicador = " + drIndicador["idIndicador"].ToString() + " And idTema = " + drTema["idTema"].ToString()).GetLength(0) + 1);
                                        //    }
                                        //    else
                                        //    {
                                        //        if (rowRI[0]["valor"].ToString() != "")
                                        //        {
                                        //            calificacionIndicador = Convert.ToSingle(rowRI[0]["valor"].ToString());
                                        //        }
                                        //    }
                                        //}
                                        //#endregion
                                        //else
                                        //{
                                        float fPromedioIndicador = Convert.ToSingle(rowRI[0]["valor"].ToString()); //Valor de la respuesta
                                        int idTE = 0;
                                        bool flgEAC = false;
                                        bool flgRPNA = false;
                                        float valorRPNA = 0;
                                        foreach (DataRow drEvidenciaIndicador in reporteTodas.Tables["Evidencia_Indicador"].Select("idIndicador = " + drIndicador["idIndicador"].ToString() + " And idTema = " + drTema["idTema"].ToString()))
                                        {
                                            if (idTE != Convert.ToInt32(drEvidenciaIndicador["idTipoEvidencia"].ToString()))
                                            {
                                                //fPromedioIndicador += Convert.ToSingle(drEvidenciaIndicador["valorEvidencia"].ToString()); //Suma el valor del tipo de evidencia, sin importar cuantas sean, con que sea una.
                                                float valor = Convert.ToSingle(drEvidenciaIndicador["valorEvidencia"].ToString());
                                                fPromedioIndicador += valor;

                                                idTE = Convert.ToInt32(drEvidenciaIndicador["idTipoEvidencia"].ToString());
                                                if (idTE == 3)
                                                {
                                                    flgEAC = true;
                                                }

                                                if (idTE == 6)
                                                {
                                                    flgRPNA = true;
                                                    valorRPNA = valor;
                                                }

                                                if (idTE == 4 && flgEAC)
                                                {
                                                    fPromedioIndicador -= valor;
                                                }

                                                if (idTE == 7 && flgRPNA)
                                                {
                                                    fPromedioIndicador -= valorRPNA;
                                                }
                                            }
                                        }
                                        calificacionIndicador += fPromedioIndicador; // A lo mas tiene 3, no deber�a tener mas
                                        //}
                                        if (rowRI[0]["calificacionRevisor"].ToString() != "")
                                        {
                                            calificacionIndicador += Convert.ToSingle(rowRI[0]["calificacionRevisor"].ToString());
                                        }

                                        if (calificacionIndicador < 1.5)
                                        {
                                            // Guardar recomendaci�n
                                            sRecomendaciones += drIndicador["idIndicador"].ToString() + ", ";

                                        }
                                        fPromedioIndicadorSubtema += calificacionIndicador;
                                    }
                                    else
                                    {
                                        // Si no hay respuesta hay que dar la recomendaci�n
                                        sRecomendaciones += drIndicador["idIndicador"].ToString() + ", ";
                                    }
                                }
                                //Tenemos que guardar el promedio del Subtema
                                if (reporteTodas.Tables["Indicador"].Select("idTema = " + drTema["idTema"].ToString() + "And idSubtema = " + drSubtema["idSubtema"].ToString()).GetLength(0) > 0)
                                {
                                    fPromedioIndicadorSubtema = fPromedioIndicadorSubtema / reporteTodas.Tables["Indicador"].Select("idTema = " + drTema["idTema"].ToString() + "And idSubtema = " + drSubtema["idSubtema"].ToString()).GetLength(0);
                                }

                                this.idTema = Convert.ToInt32(drTema["idTema"].ToString());
                                this.idSubtema = Convert.ToInt32(drSubtema["idSubtema"].ToString());
                                this.idEmpresa = Convert.ToInt32(drEmpresa["idEmpresa"].ToString());
                                this.promedio = fPromedioIndicadorSubtema;
                                if (sRecomendaciones.Length > 0)
                                {
                                    this.recomendaciones = sRecomendaciones.Substring(0, sRecomendaciones.Length - 2);
                                }
                                else
                                {
                                    this.recomendaciones = "";
                                }

                                // MRA
                                // 25/05/2010 20:36 hrs
                                // Calculando el promedio del tema, guardando el promedio del subtema
                                fPromedioIndicadorTema += fPromedioIndicadorSubtema;

                                // MRA
                                // 02/03/2010 05:05 hrs.
                                // Condicion insertada para topar los promedios a 3

                                // Comentadas las siguientes 4 el: 13072012
                                //if (this.promedio > 3)
                                //    this.promedio = (float)2.98999;
                                //if (this.promedioGral > 3)
                                //    this.promedioGral = (float)2.99999;

                                if (!rank.GuardarPromedio(this.idEmpresa, this.idCuestionario, this.idTema, this.idSubtema, this.promedio, this.promedioGral, this.idUsuario, this.recomendaciones, 0, 0, 0))
                                {
                                    /// Mandar el mensaje o el error de que no se guardo el promedio
                                    /// 
                                    StreamWriter swp = File.AppendText("d:\\temp\\monitor_ranking.log");
                                    swp.AutoFlush = true;
                                    swp.WriteLine("No fue posible guardar el promedio del tema");
                                    swp.Close();

                                    throw new Exception();
                                }
                            }


                            if (reporteTodas.Tables["Subtema"].Select("idTema = " + drTema["idTema"].ToString()).GetLength(0) > 0)
                            {
                                fPromedioIndicadorTema = fPromedioIndicadorTema / reporteTodas.Tables["Subtema"].Select("idTema = " + drTema["idTema"].ToString()).GetLength(0);
                            }

                            if (Convert.ToInt32(drTema["idTema"].ToString()) == 12 || Convert.ToInt32(drTema["idTema"].ToString()) == 13)
                            {
                                fPromedioGRSE = fPromedioIndicadorTema;
                                flgGRSE = true;
                            }
                            else
                            {
                                fPromedioGeneral += fPromedioIndicadorTema;
                            }

                            //Salvar el promedio del tema
                            this.idTema = Convert.ToInt32(drTema["idTema"].ToString());
                            this.idEmpresa = Convert.ToInt32(drEmpresa["idEmpresa"].ToString());
                            this.promedio = fPromedioIndicadorTema;

                            // MRA
                            // 02/03/2010 05:05 hrs.
                            // Condicion insertada para topar los promedios a 3

                            // Comentadas las siguientes 4 lineas el 13/07/2012
                            //if (this.promedio > 3)
                            //    this.promedio = (float)2.98999;
                            //if (this.promedioGral > 3)
                            //    this.promedioGral = (float)2.99999;

                            #region Calculo del promedio por fase de implementacion
                            ///Vamos a calcular el promedio por fase de implementacion
                            ///Para cada indicador del tema 
                            float fporcentajeFaseI = 0;
                            float fporcentajeFaseII = 0;
                            float fporcentajeFaseIII = 0;

                            foreach (DataRow drIndicador in reporteTodas.Tables["Indicador"].Select("idTema = " + drTema["idTema"].ToString()))
                            {
                                float calificacionIndicador = 0;
                                DataRow[] rowRI = reporteTodas.Tables["Respuesta_Indicador"].Select("idIndicador = " + drIndicador["idIndicador"].ToString() + " And idTema = " + drIndicador["idTema"].ToString());
                                if (rowRI.GetLength(0) > 0) //Hay respuesta. Solo es un registro. Pendiente: considerar el factor de antig�edad
                                {
                                    float fPromedioIndicador = Convert.ToSingle(rowRI[0]["valor"].ToString()); //Valor de la respuesta
                                    int idTE = 0;
                                    bool flgEAC = false;
                                    bool flgRPNA = false;
                                    float valorRPNA = 0;
                                    foreach (DataRow drEvidenciaIndicador in reporteTodas.Tables["Evidencia_Indicador"].Select("idIndicador = " + drIndicador["idIndicador"].ToString() + " And idTema = " + drTema["idTema"].ToString()))
                                    {
                                        if (idTE != Convert.ToInt32(drEvidenciaIndicador["idTipoEvidencia"].ToString()))
                                        {
                                            //fPromedioIndicador += Convert.ToSingle(drEvidenciaIndicador["valorEvidencia"].ToString()); //Suma el valor del tipo de evidencia, sin importar cuantas sean, con que sea una.
                                            float valor = Convert.ToSingle(drEvidenciaIndicador["valorEvidencia"].ToString());
                                            fPromedioIndicador += valor;

                                            idTE = Convert.ToInt32(drEvidenciaIndicador["idTipoEvidencia"].ToString());
                                            if (idTE == 3)
                                            {
                                                flgEAC = true;
                                            }

                                            if (idTE == 6)
                                            {
                                                flgRPNA = true;
                                                valorRPNA = valor;
                                            }

                                            if (idTE == 4 && flgEAC)
                                            {
                                                fPromedioIndicador -= valor;
                                            }

                                            if (idTE == 7 && flgRPNA)
                                            {
                                                fPromedioIndicador -= valorRPNA;
                                            }
                                        }
                                    }
                                    calificacionIndicador += fPromedioIndicador; // A lo mas tiene 3, no deber�a tener mas

                                    ///Preguntar a Felipe si para el calculo del promedio por fase se toma en cuenta la calificaci�n del revisor.
                                    if (rowRI[0]["calificacionRevisor"].ToString() != "")
                                    {
                                        calificacionIndicador += Convert.ToSingle(rowRI[0]["calificacionRevisor"].ToString());
                                    }

                                    if (drIndicador["faseImplementacion"].ToString() == "I")
                                    {
                                        fporcentajeFaseI += calificacionIndicador;
                                    }
                                    else if (drIndicador["faseImplementacion"].ToString() == "II")
                                    {
                                        fporcentajeFaseII += calificacionIndicador;
                                    }
                                    else if (drIndicador["faseImplementacion"].ToString() == "III")
                                    {
                                        fporcentajeFaseIII += calificacionIndicador;
                                    }
                                }
                            }
                            //Saca el promedio y el porcentaje

                            fporcentajeFaseI = ((fporcentajeFaseI / 10)) / 3;
                            fporcentajeFaseII = ((fporcentajeFaseII / 10)) / 3;
                            fporcentajeFaseIII = ((fporcentajeFaseIII / 10)) / 3;

                            if (fporcentajeFaseI > 1)
                                fporcentajeFaseI = 1;
                            if (fporcentajeFaseII > 1)
                                fporcentajeFaseII = 1;
                            if (fporcentajeFaseIII > 1)
                                fporcentajeFaseIII = 1;

                            #endregion Calculo del promedio por fase de implementacion

                            // Guarda el promedio del Tema y el porcentaje por fase de implementacion

                            if (!rank.GuardarPromedio(this.idEmpresa, this.idCuestionario, this.idTema, 0, this.promedio, this.promedioGral, this.idUsuario, this.recomendaciones, fporcentajeFaseI, fporcentajeFaseII, fporcentajeFaseIII))
                            {
                                /// Tambien checar si el promedio se guardo bien o no.
                                /// 
                                //StreamWriter sw = File.AppendText("d:\\temp\\monitor.log");
                                //sw.AutoFlush = true;
                                //sw.WriteLine ("No fue posible guardar el promedio por fase de implementaci�n");
                                //sw.Close();
                            }
                        }

                        if (flgGRSE)
                        {
                            fPromedioGeneral = fPromedioGeneral / (reporteTodas.Tables["Tema"].Rows.Count - 1);
                        }
                        else
                        {
                            fPromedioGeneral = fPromedioGeneral / (reporteTodas.Tables["Tema"].Rows.Count);
                        }


                        // Aqui hay que hacer el c�lculo del 70% y si es menor sumarle la calificaci�n del GRSE que no es mayor al 0.1
                        //if (fPromedioGeneral < 1.5) //Revisar esto, porque esta mal, hay que traer el 70% de la calificacion mas alta.
                        //{
                        //    if (fPromedioGRSE > 1.5 && fPromedioGRSE <= 1.9)
                        //        fPromedioGeneral += 0.02F;
                        //    if (fPromedioGRSE > 1.9 && fPromedioGRSE <= 2.3)
                        //        fPromedioGeneral += 0.04F;
                        //    if (fPromedioGRSE > 2.3 && fPromedioGRSE <= 2.5)
                        //        fPromedioGeneral += 0.06F;
                        //    if (fPromedioGRSE > 2.5 && fPromedioGRSE <= 2.8)
                        //        fPromedioGeneral += 0.08F;
                        //    if (fPromedioGRSE > 2.8 && fPromedioGRSE <= 3)
                        //        fPromedioGeneral += 0.1F;
                        //}

                        this.promedioGral = fPromedioGeneral;

                        // MRA
                        // 02/03/2010 05:05 hrs.
                        // Condicion insertada para topar los promedios a 3

                        // Comentadas las siguientes 4 lineas el 13/07/2012
                        //if (this.promedio > 3)
                        //    this.promedio = (float)2.98999;
                        //if (this.promedioGral > 3)
                        //    this.promedioGral = (float)2.99999;

                        if (!rank.GuardarPromedio(this.idEmpresa, this.idCuestionario, this.idTema, 0, this.promedio, this.promedioGral, this.idUsuario, this.recomendaciones, 0, 0, 0))
                        {
                            /// Checar si se guardo bien y si no registrar el error.
                            /// 
                            //StreamWriter sw = File.AppendText("d:\\temp\\monitor.log");
                            //sw.AutoFlush = true;
                            //sw.WriteLine ("No fue posible guardar el promedio general");
                            //sw.Close();
                        }
                        this.promedioGral = 0;
                    }
                    
                    result = true;
                }
                catch (Exception ex)
                {
                    StreamWriter swe = File.AppendText("d:\\temp\\monitor_err.log");
                    swe.AutoFlush = true;
                    swe.WriteLine(DateTime.Now + ": Error: " + ex.Message.ToString());
                    swe.Close();
                    throw new Exception("ESR.Business.Ranking.GeneraRanking(). Error: " + ex.Message, ex);
                }
            }
            //sw.Close();
            return result;

        }
        public DataSet CargaRanking()
        {
            ESR.Data.Ranking rank = new ESR.Data.Ranking();
            return rank.CargaRanking(this.idEmpresa, this.idCuestionario);
        }
    }
}
