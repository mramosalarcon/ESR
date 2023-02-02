using System;
using System.Data;
using System.Web.UI.WebControls;
using ESR.Business;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

public partial class rep_ranking : System.Web.UI.UserControl
{
    protected int GetIdEmpresa()
    {
        if (Request.Params["idEmpresa"] == null)
            return Convert.ToInt32(Session["idEmpresa"].ToString());
        else
            return Convert.ToInt32(Request.Params["idEmpresa"].ToString());
    }

    private int GetIdCuestionario()
    {
        if (Request.Params["idCuestionario"] != null)
            return Convert.ToInt32(Request.Params["idCuestionario"]);
        else
            return 0;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //Cuestionario cuestionario = new Cuestionario();
        //cuestionario.idEmpresa = this.GetIdEmpresa();
        //cuestionario.idCuestionario = this.GetIdCuestionario();
        //DataSet reporte = cuestionario.CargaAvance();

        //lblTitulo.Text += ": " + reporte.Tables["Empresa"].Rows[0]["nombre"].ToString();
        bool flgGenera = false;
        Ranking ranking = new Ranking();
        ranking.idCuestionario = this.GetIdCuestionario();
        ranking.idUsuario = Session["idUsuario"].ToString();

        if (Request.Params["flgGenera"] != null)
            flgGenera = true;
        try
        {
            Task t = Task.Factory.StartNew(() =>
            { ranking.GeneraRanking(flgGenera); });
            t.Wait();


            Table tabRanking = new Table();
            TableRow titulo = new TableRow();

            TableCell tituloPosicion = new TableCell();
            tituloPosicion.BackColor = System.Drawing.Color.Teal;
            tituloPosicion.ForeColor = System.Drawing.Color.White;
            tituloPosicion.Text = "Posición";

            TableCell tituloEmpresa = new TableCell();
            tituloEmpresa.BackColor = System.Drawing.Color.Brown;
            tituloEmpresa.ForeColor = System.Drawing.Color.White;
            tituloEmpresa.ColumnSpan = 3;
            tituloEmpresa.Text = "Empresa";

            TableCell tituloEstado = new TableCell();
            tituloEstado.BackColor = System.Drawing.Color.Brown;
            tituloEstado.ForeColor = System.Drawing.Color.White;
            tituloEstado.Text = "Estado";

            TableCell tituloPromedio = new TableCell();
            tituloPromedio.BackColor = System.Drawing.Color.Teal;
            tituloPromedio.ForeColor = System.Drawing.Color.White;
            tituloPromedio.Text = "Promedio";

            TableCell tituloPGC = new TableCell();
            tituloPGC.BackColor = System.Drawing.Color.Teal;
            tituloPGC.ForeColor = System.Drawing.Color.White;
            tituloPGC.Text = "Promedio + GC";

            //TableCell indiceRSE = new TableCell();
            //indiceRSE.BackColor = System.Drawing.Color.Teal;
            //indiceRSE.ForeColor = System.Drawing.Color.White;
            //indiceRSE.Text = "INDICE RSE";
            titulo.Cells.Add(tituloPosicion);
            titulo.Cells.Add(tituloEmpresa);
            titulo.Cells.Add(tituloEstado);
            titulo.Cells.Add(tituloPromedio);
            titulo.Cells.Add(tituloPGC);

            Tema temas = new Tema();
            DataSet dsTemas = temas.CargaTemasCuestionario(this.GetIdCuestionario());

            // Ahora insertamos el titulo del tema
            foreach (DataRow drTema in dsTemas.Tables["Temas"].Rows)
            {
                TableCell tituloTema = new TableCell();
                tituloTema.BackColor = System.Drawing.Color.Teal;
                tituloTema.ForeColor = System.Drawing.Color.White;
                tituloTema.Text = drTema["Tema"].ToString();

                TableCell faseImplementacionI = new TableCell();
                faseImplementacionI.BackColor = System.Drawing.Color.Brown;
                faseImplementacionI.ForeColor = System.Drawing.Color.White;
                faseImplementacionI.Text = "Fase I"; //drTema["Tema"].ToString();

                TableCell faseImplementacionII = new TableCell();
                faseImplementacionII.BackColor = System.Drawing.Color.Brown;
                faseImplementacionII.ForeColor = System.Drawing.Color.White;
                faseImplementacionII.Text = "Fase II"; //drTema["Tema"].ToString();

                TableCell faseImplementacionIII = new TableCell();
                faseImplementacionIII.BackColor = System.Drawing.Color.Brown;
                faseImplementacionIII.ForeColor = System.Drawing.Color.White;
                faseImplementacionIII.Text = "Fase III"; //drTema["Tema"].ToString();

                titulo.Cells.Add(tituloTema);

                titulo.Cells.Add(faseImplementacionI);
                titulo.Cells.Add(faseImplementacionII);
                titulo.Cells.Add(faseImplementacionIII);
            }

            tabRanking.Rows.Add(titulo);

            // Por tema todas las empresas
            Empresa empresas = new Empresa();
            empresas.idCuestionario = this.GetIdCuestionario();
            DataSet dsTodas = empresas.CargaRanking();

            int contadorEmpresa = 0;
            float fltPGC = 0;
            float fltPM = 0;
            foreach (DataRow drEmpresa in dsTodas.Tables["Empresa"].Rows)
            {
                contadorEmpresa++;

                Ranking rkEmpresa = new Ranking();
                rkEmpresa.idEmpresa = Convert.ToInt32(drEmpresa["idEmpresa"].ToString());
                rkEmpresa.idCuestionario = this.GetIdCuestionario();
                DataSet rankingEmpresa = rkEmpresa.CargaDetalleRanking();

                TableRow empresa = new TableRow();

                TableCell posicionCell = new TableCell();
                posicionCell.Text = contadorEmpresa.ToString();
                empresa.Cells.Add(posicionCell);

                TableCell idEmpresaCell = new TableCell();
                idEmpresaCell.Text = drEmpresa["idEmpresa"].ToString();
                empresa.Cells.Add(idEmpresaCell);

                TableCell nombreEmpresaCell = new TableCell();
                nombreEmpresaCell.Text = drEmpresa["nombre"].ToString();
                empresa.Cells.Add(nombreEmpresaCell);

                TableCell nombreCortoempresaCell = new TableCell();
                nombreCortoempresaCell.Text = drEmpresa["nombreCorto"].ToString();
                empresa.Cells.Add(nombreCortoempresaCell);

                TableCell estadoCell = new TableCell();
                estadoCell.Text = drEmpresa["estado"].ToString();
                empresa.Cells.Add(estadoCell);


                //Promedio General
                TableCell promedioCell = new TableCell();
                CultureInfo culture = new CultureInfo("en-US");
                promedioCell.Text = String.Format(culture, "{0:#.#####}", drEmpresa["promedioGral"]);
                empresa.Cells.Add(promedioCell);


                //***************************************************************************
                // Cálculo del PGC + Promedio
                if (contadorEmpresa > 1 && contadorEmpresa <= 6)
                {
                    fltPGC += Convert.ToSingle(drEmpresa["promedioGral"]);
                }
                if (contadorEmpresa == 7)
                {
                    fltPGC = fltPGC / 5;
                    lblIRSE.Text = fltPGC.ToString(culture);
                    fltPM = Convert.ToSingle(fltPGC * 0.70);
                }

                TableCell PGCCell = new TableCell();
                //if (Convert.ToSingle(drEmpresa["promedioGral"]) < fltPM)
                {
                    if (rankingEmpresa.Tables["Ranking"].Select("idTema = 12").GetLength(0) > 0)
                    {
                        DataRow drGC = rankingEmpresa.Tables["Ranking"].Select("idTema = 12")[0];
                        float fltPGCP = 0;
                        if (Convert.ToSingle(drGC["promedio"]) > 1.2 && Convert.ToSingle(drGC["promedio"]) <= 1.4)
                        {
                            fltPGCP = Convert.ToSingle(Convert.ToSingle(drEmpresa["promedioGral"]) * 1.02);
                        }
                        if (Convert.ToSingle(drGC["promedio"]) > 1.4 && Convert.ToSingle(drGC["promedio"]) <= 1.5)
                        {
                            fltPGCP = Convert.ToSingle(Convert.ToSingle(drEmpresa["promedioGral"]) * 1.03);
                        }
                        if (Convert.ToSingle(drGC["promedio"]) > 1.5 && Convert.ToSingle(drGC["promedio"]) <= 1.9)
                        {
                            fltPGCP = Convert.ToSingle(Convert.ToSingle(drEmpresa["promedioGral"]) * 1.04);
                        }
                        if (Convert.ToSingle(drGC["promedio"]) > 1.9 && Convert.ToSingle(drGC["promedio"]) <= 2.2)
                        {
                            fltPGCP = Convert.ToSingle(Convert.ToSingle(drEmpresa["promedioGral"]) * 1.06);
                        }
                        if (Convert.ToSingle(drGC["promedio"]) > 2.2 && Convert.ToSingle(drGC["promedio"]) <= 2.4)
                        {
                            fltPGCP = Convert.ToSingle(Convert.ToSingle(drEmpresa["promedioGral"]) * 1.08);
                        }
                        if (Convert.ToSingle(drGC["promedio"]) > 2.4 && Convert.ToSingle(drGC["promedio"]) <= 2.7)
                        {
                            fltPGCP = Convert.ToSingle(Convert.ToSingle(drEmpresa["promedioGral"]) * 1.09);
                        }
                        if (Convert.ToSingle(drGC["promedio"]) > 2.7 && Convert.ToSingle(drGC["promedio"]) <= 3)
                        {
                            fltPGCP = Convert.ToSingle(Convert.ToSingle(drEmpresa["promedioGral"]) * 1.1);
                        }

                        PGCCell.Text = String.Format(culture, "{0:#.#####}", fltPGCP);
                    }
                }
                empresa.Cells.Add(PGCCell);
                //***************************************************************************

                
                foreach (DataRow drPromedioTema in rankingEmpresa.Tables["Ranking"].Rows)
                {
                    if (Convert.ToInt32(drPromedioTema["idTema"]) != 3)
                    {
                        
                        TableCell temaCell = new TableCell();
                        temaCell.Text = String.Format(culture, "{0:#.#####}", drPromedioTema["Promedio"]);

                        TableCell faseImplementacionI = new TableCell();
                        faseImplementacionI.Text = String.Format(culture, "{0:0%}", drPromedioTema["porcentajeFaseI"]);
                        TableCell faseImplementacionII = new TableCell();
                        faseImplementacionII.Text = String.Format(culture, "{0:0%}", drPromedioTema["porcentajeFaseII"]);
                        TableCell faseImplementacionIII = new TableCell();
                        faseImplementacionIII.Text = String.Format(culture, "{0:0%}", drPromedioTema["porcentajeFaseIII"]);

                        empresa.Cells.Add(temaCell);
                        empresa.Cells.Add(faseImplementacionI);
                        empresa.Cells.Add(faseImplementacionII);
                        empresa.Cells.Add(faseImplementacionIII);

                        tabRanking.Rows.Add(empresa);

                        // Aqui tengo que meter el Promedio + GC con un if


                        //if (fPromedioIndicadorTema < fMenorValor)
                        //{
                        //    fMenorValor = fPromedioIndicadorTema;
                        //}

                        //if (fPromedioIndicadorTema > fMayorValor)
                        //{
                        //    fMayorValor = fPromedioIndicadorTema;
                        //}
                        //fPromedioGeneral += fPromedioIndicadorTema;
                    }
                    else
                    {
                        
                    }

                }

                //fPromedioGeneral = fPromedioGeneral / dsTodas.Tables["Empresa"].Rows.Count;

                //TableCell temaMenorValor = new TableCell();
                //temaMenorValor.BackColor = System.Drawing.Color.Beige;
                //temaMenorValor.HorizontalAlign = HorizontalAlign.Center;
                //temaMenorValor.Text = fMenorValor.ToString();

                //menorValorRow.Cells.Add(temaMenorValor);

                //TableCell temaPromGral = new TableCell();
                //temaPromGral.BackColor = System.Drawing.Color.Beige;
                //temaPromGral.HorizontalAlign = HorizontalAlign.Center;
                //temaPromGral.Text = fPromedioGeneral.ToString();

                //promedioGralRow.Cells.Add(temaPromGral);

                //TableCell temaMayorValor = new TableCell();
                //temaMayorValor.BackColor = System.Drawing.Color.Beige;
                //temaMayorValor.HorizontalAlign = HorizontalAlign.Center;
                //temaMayorValor.Text = fMayorValor.ToString();

                //mayorValorRow.Cells.Add(temaMayorValor);

                //TableCell temaLider = new TableCell();
                //temaLider.BackColor = System.Drawing.Color.Beige;
                //temaLider.HorizontalAlign = HorizontalAlign.Center;
                //temaLider.Text = fLider.ToString();

                //liderRow.Cells.Add(temaLider);

                //promedioEmpresa.Cells.Add(temaPromedio);

                //TableCell temaPosicion = new TableCell();
                //temaPosicion.BackColor = System.Drawing.Color.Beige;
                //temaPosicion.HorizontalAlign = HorizontalAlign.Center;
                //temaPosicion.Text = sPosicion.ToString() + "/" + dsTodas.Tables["Empresa"].Rows.Count.ToString();

                //posicionRow.Cells.Add(temaPosicion);

                //tabRanking.Rows.Add(menorValorRow);
                //tabRanking.Rows.Add(promedioGralRow);
                //tabRanking.Rows.Add(mayorValorRow);
                //tabRanking.Rows.Add(liderRow);
                //tabRanking.Rows.Add(posicionRow);

            }


            Panel1.Controls.Add(tabRanking);
        }
        catch (Exception ex)
        {
            StreamWriter sw = File.AppendText("e:\\temp\\mranking.log");
            sw.AutoFlush = true;
            sw.WriteLine(DateTime.Now.ToString() + " - ESR.rep_ranking.Page_Load() Error: " + ex.Message);
            sw.Close();
        }
    }

    protected void cmdRegenerar_Click(object sender, EventArgs e)
    {
        if (Request.Url.OriginalString.IndexOf("&flgGenera=true") == -1)
            Response.Redirect(Request.Url.OriginalString + "&flgGenera=true", false);
    }

}
