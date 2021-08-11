using System;
using System.Data;
using System.Web.UI.WebControls;
using ESR.Business;

public partial class rep_individual : System.Web.UI.UserControl
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

        Ranking ranking = new Ranking();
        ranking.idEmpresa = this.GetIdEmpresa();
        ranking.idCuestionario = this.GetIdCuestionario();
        DataSet dsRanking = ranking.CargaRanking();

        if (dsRanking.Tables["Ranking"].Rows.Count > 0)
        {
            lblTitulo.Text += ": " + dsRanking.Tables["Ranking"].Rows[0]["nombreCorto"].ToString();

            Table tabRanking = new Table();
            TableRow titulo = new TableRow();
            TableRow promedioEmpresa = new TableRow();
            TableRow menorValorRow = new TableRow();
            TableRow promedioGralRow = new TableRow();
            TableRow mayorValorRow = new TableRow();
            TableRow liderRow = new TableRow();
            TableRow posicionRow = new TableRow();

            TableCell vacio = new TableCell();
            vacio.BackColor = System.Drawing.Color.Teal;
            vacio.ForeColor = System.Drawing.Color.White;

            TableCell vacio2 = new TableCell();
            vacio2.BackColor = System.Drawing.Color.Beige;

            TableCell indiceRSE = new TableCell();
            indiceRSE.BackColor = System.Drawing.Color.Teal;
            indiceRSE.ForeColor = System.Drawing.Color.White;
            indiceRSE.Text = "INDICE RSE";

            TableCell nombreEmpresa = new TableCell();
            nombreEmpresa.BackColor = System.Drawing.Color.Teal;
            nombreEmpresa.ForeColor = System.Drawing.Color.White;
            nombreEmpresa.Text = dsRanking.Tables["Ranking"].Rows[0]["nombreCorto"].ToString();

            titulo.Cells.Add(vacio);
            titulo.Cells.Add(indiceRSE);

            promedioEmpresa.Cells.Add(nombreEmpresa);
            promedioEmpresa.Cells.Add(vacio2);

            // aqui pegue
            TableCell menorValor = new TableCell();
            menorValor.BackColor = System.Drawing.Color.Teal;
            menorValor.ForeColor = System.Drawing.Color.White;
            menorValor.Text = "Menor Valor";

            TableCell mayorValor = new TableCell();
            mayorValor.BackColor = System.Drawing.Color.Teal;
            mayorValor.ForeColor = System.Drawing.Color.White;
            mayorValor.Text = "Mayor Valor";

            TableCell promGral = new TableCell();
            promGral.BackColor = System.Drawing.Color.Teal;
            promGral.ForeColor = System.Drawing.Color.White;
            promGral.Text = "Promedio General";

            TableCell lider = new TableCell();
            lider.BackColor = System.Drawing.Color.Teal;
            lider.ForeColor = System.Drawing.Color.White;
            lider.Text = "Líder";

            TableCell posicion = new TableCell();
            posicion.BackColor = System.Drawing.Color.Teal;
            posicion.ForeColor = System.Drawing.Color.White;
            posicion.Text = "Posición";

            TableCell menorValorRSE = new TableCell();
            menorValorRSE.BackColor = System.Drawing.Color.Beige;

            TableCell promGralRSE = new TableCell();
            promGralRSE.BackColor = System.Drawing.Color.Beige;

            TableCell mayorValorRSE = new TableCell();
            mayorValorRSE.BackColor = System.Drawing.Color.Beige;

            TableCell liderRSE = new TableCell();
            liderRSE.BackColor = System.Drawing.Color.Beige;

            TableCell PosicionRSE = new TableCell();
            PosicionRSE.BackColor = System.Drawing.Color.Beige;
            PosicionRSE.HorizontalAlign = HorizontalAlign.Center;
            if (dsRanking.Tables["Posicion"].Select("idEmpresa = " + ranking.idEmpresa).GetLength(0) > 0)
            {
                PosicionRSE.Text = dsRanking.Tables["Posicion"].Select("idEmpresa = " + ranking.idEmpresa)[0]["Posicion"].ToString() + " / " + dsRanking.Tables["Posicion"].Rows.Count;
            }

            menorValorRow.Cells.Add(menorValor);
            menorValorRow.Cells.Add(menorValorRSE);

            promedioGralRow.Cells.Add(promGral);
            promedioGralRow.Cells.Add(promGralRSE);

            mayorValorRow.Cells.Add(mayorValor);
            mayorValorRow.Cells.Add(mayorValorRSE);

            liderRow.Cells.Add(lider);
            liderRow.Cells.Add(liderRSE);

            posicionRow.Cells.Add(posicion);
            posicionRow.Cells.Add(PosicionRSE);

            foreach (DataRow drRanking in dsRanking.Tables["Ranking"].Rows)
            {
                TableCell tituloTema = new TableCell();
                tituloTema.BackColor = System.Drawing.Color.Teal;
                tituloTema.ForeColor = System.Drawing.Color.White;
                tituloTema.HorizontalAlign = HorizontalAlign.Center;
                tituloTema.Text = drRanking["Tema"].ToString();

                titulo.Cells.Add(tituloTema);

                TableCell temaPromedio = new TableCell();
                temaPromedio.BackColor = System.Drawing.Color.Beige;
                temaPromedio.HorizontalAlign = HorizontalAlign.Center;
                temaPromedio.Text = String.Format("{0:F}", Convert.ToSingle(drRanking["promedio"].ToString()));

                promedioEmpresa.Cells.Add(temaPromedio);

                TableCell menorValorCell = new TableCell();
                menorValorCell.BackColor = System.Drawing.Color.Beige;
                menorValorCell.HorizontalAlign = HorizontalAlign.Center;
                if (dsRanking.Tables["Menor"].Select("idTema = " + drRanking["idTema"].ToString()).GetLength(0) > 0)
                {
                    menorValorCell.Text = String.Format("{0:F}", Convert.ToSingle(dsRanking.Tables["Menor"].Select("idTema = " + drRanking["idTema"].ToString())[0]["MenorValor"].ToString()));
                }
                menorValorRow.Cells.Add(menorValorCell);

                TableCell promedioGeneral = new TableCell();
                promedioGeneral.BackColor = System.Drawing.Color.Beige;
                promedioGeneral.HorizontalAlign = HorizontalAlign.Center;
                if (dsRanking.Tables["Promedio"].Select("idTema = " + drRanking["idTema"].ToString()).GetLength(0) > 0)
                {
                    promedioGeneral.Text = String.Format("{0:F}", Convert.ToSingle(dsRanking.Tables["Promedio"].Select("idTema = " + drRanking["idTema"].ToString())[0]["Promedio"].ToString()));
                }
                promedioGralRow.Cells.Add(promedioGeneral);

                TableCell mayorValorCell = new TableCell();
                mayorValorCell.BackColor = System.Drawing.Color.Beige;
                mayorValorCell.HorizontalAlign = HorizontalAlign.Center;
                if (dsRanking.Tables["Mayor"].Select("idTema = " + drRanking["idTema"].ToString()).GetLength(0) > 0)
                {
                    mayorValorCell.Text = String.Format("{0:F}", Convert.ToSingle(dsRanking.Tables["Mayor"].Select("idTema = " + drRanking["idTema"].ToString())[0]["MayorValor"].ToString()));
                }
                mayorValorRow.Cells.Add(mayorValorCell);

                TableCell liderCell = new TableCell();
                liderCell.BackColor = System.Drawing.Color.Beige;
                liderCell.HorizontalAlign = HorizontalAlign.Center;
                if (dsRanking.Tables["Lider"].Select("idTema = " + drRanking["idTema"].ToString()).GetLength(0) > 0)
                {
                    liderCell.Text = String.Format("{0:F}", Convert.ToSingle(dsRanking.Tables["Lider"].Select("idTema = " + drRanking["idTema"].ToString())[0]["promedio"].ToString()));
                }
                liderRow.Cells.Add(liderCell);

                TableCell posicionTema = new TableCell();
                posicionTema.BackColor = System.Drawing.Color.Beige;
                posicionTema.HorizontalAlign = HorizontalAlign.Center;
                if (dsRanking.Tables["Tema"].Select("idTema = " + drRanking["idTema"].ToString() + " And idEmpresa = " + ranking.idEmpresa).GetLength(0) > 0)
                {
                    posicionTema.Text = dsRanking.Tables["Tema"].Select("idTema = " + drRanking["idTema"].ToString() + " And idEmpresa = " + ranking.idEmpresa)[0]["Posicion"].ToString();
                }
                posicionRow.Cells.Add(posicionTema);

            }
            // poner la tabla con los resultados por tema
            tabRanking.Rows.Add(titulo);
            tabRanking.Rows.Add(promedioEmpresa);
            tabRanking.Rows.Add(menorValorRow);
            tabRanking.Rows.Add(promedioGralRow);
            tabRanking.Rows.Add(mayorValorRow);
            tabRanking.Rows.Add(liderRow);
            tabRanking.Rows.Add(posicionRow);

            Panel1.Controls.Add(tabRanking);
        }
        else
        {
            lblTitulo.Text = "Esta empresa no tiene asignado el cuestionario solicitado";
        }
    }
}
