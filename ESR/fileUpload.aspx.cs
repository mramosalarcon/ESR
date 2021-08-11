using System;
using System.Configuration;
using System.IO;
using ESR.Business;

public partial class fileUpload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            lblMensaje.Visible = false;
            //imgWait.Visible = false;
        }
    }
    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        try
        {
            lblMensaje.Text = "Espere un momento, subiendo archivo...";
            lblMensaje.Visible = true;
            //imgWait.Visible = true;

            if (fuAdjuntarArchivo.HasFile)
            {
                int iEvidenciaLen = fuAdjuntarArchivo.PostedFile.ContentLength;

                if (Request.QueryString["idInciso"] == null)
                {

                    Indicador respuestaIndicador = new Indicador();
                    respuestaIndicador.idIndicador = Convert.ToInt32(Request.QueryString["idIndicador"]);
                    respuestaIndicador.idTema = Convert.ToInt32(Request.QueryString["idTema"]);
                    respuestaIndicador.idTipoEvidencia = Convert.ToInt32(Request.QueryString["idTipoEvidencia"]);
                    
                    // MRA 27/10/2010
                    // Esta linea esta rara
                    // �Que pasa si sube un documento una empresa que no es la que se esta consultando?
                    
                    respuestaIndicador.idEmpresa = Convert.ToInt32(Session["idEmpresa"].ToString());
                    respuestaIndicador.idCuestionario = Convert.ToInt32(Request.QueryString["idCuestionario"]);
                    respuestaIndicador.fileName = fuAdjuntarArchivo.FileName.ToUpper();
                    respuestaIndicador.idUsuario = Session["idUsuario"].ToString();
                    respuestaIndicador.idTipoRespuesta = Convert.ToInt32(Request.QueryString["idTipoRespuesta"]);

                    if (iEvidenciaLen <= 5242880) //Valida que sea menor a 5MB
                    {
                        byte[] arrEvidencia = new byte[iEvidenciaLen];
                        Stream streamLogo = fuAdjuntarArchivo.FileContent;

                        streamLogo.Read(arrEvidencia, 0, iEvidenciaLen);
                        respuestaIndicador.streEvidencia = fuAdjuntarArchivo.FileContent;
                        respuestaIndicador.arrEvidencia = arrEvidencia;
                    }
                    else
                    {
                        throw new Exception("El tamaño máximo permitido para subir archivos fue excedido");
                    }

                    if (respuestaIndicador.GuardarEvidencia())
                    {
                        lblMensaje.Text = "El archivo fue enviado con éxito <input type=\"button\" onclick=\"opener.location.reload(true);self.close();\" value=\"Cerrar\" name=\"btnCerrar\">";
                        //imgWait.Visible = false;
                    }
                    else
                    {
                        lblMensaje.Text = "Hubo un error al subir el archivo, intente de nuevo.";
                        //imgWait.Visible = false;
                    }
                }
                else
                {
                    // Inciso: strControl: _2_a_4_5
                    // idFu: fu_2_a_4_5

                    Inciso respuestaInciso = new Inciso();
                    respuestaInciso.idIndicador = Convert.ToInt32(Request.QueryString["idIndicador"]);
                    respuestaInciso.idTema = Convert.ToInt32(Request.QueryString["idTema"]);
                    respuestaInciso.idInciso = Request.QueryString["idInciso"];
                    respuestaInciso.idTipoEvidencia = Convert.ToInt32(Request.QueryString["idTipoEvidencia"]);
                    respuestaInciso.idEmpresa = Convert.ToInt32(Session["idEmpresa"].ToString());
                    respuestaInciso.idCuestionario = Convert.ToInt32(Request.QueryString["idCuestionario"]);
                    respuestaInciso.fileName = fuAdjuntarArchivo.FileName.ToUpper();
                    respuestaInciso.idUsuario = Session["idUsuario"].ToString();
                    respuestaInciso.idTipoRespuesta = Convert.ToInt32(Request.QueryString["idTipoRespuesta"]);

                    if (iEvidenciaLen <= 5242880) //Valida que sea menor a 5MB
                    {
                        byte[] arrEvidencia = new byte[iEvidenciaLen];
                        Stream streamLogo = fuAdjuntarArchivo.FileContent;

                        streamLogo.Read(arrEvidencia, 0, iEvidenciaLen);
                        respuestaInciso.streEvidencia = fuAdjuntarArchivo.FileContent;
                        respuestaInciso.arrEvidencia = arrEvidencia;
                    }
                    else
                    {
                        throw new Exception("El tamaño máximo permitido para subir archivos fue excedido");
                    }

                    if (respuestaInciso.GuardarEvidencia())
                    {
                        lblMensaje.Text = "El archivo fue enviado con éxito <input type=\"button\" onclick=\"opener.location.reload(true);self.close();\" value=\"Cerrar\" name=\"btnCerrar\">";
                        //imgWait.Visible = false;
                    }
                    else
                    {
                        lblMensaje.Text = "Hubo un error al subir el archivo, intente de nuevo.";
                        //imgProgress.Visible = false;
                    }

                }
            }
        }

        catch(Exception ex)
        {

            lblMensaje.Text = "Hubo un error al subir el archivo: " + ex.Message;
            lblMensaje.Visible = true;
            //using (StreamWriter sw = File.AppendText(ConfigurationManager.AppSettings["logFile"].ToString()))
            //{
            //    sw.AutoFlush = true;
            //    sw.WriteLine("Fecha: " + DateTime.Now.ToString());
            //    sw.WriteLine("Empresa: " + Session["idEmpresa"].ToString());
            //    sw.WriteLine("Error en fileUpload.btnAceptar_Click(): " + ex.Message);
            //    sw.WriteLine("InnerException: " + ex.InnerException);
            //    sw.Close();
            //}
            //imgWait.Visible = false; 
        }

    }
}
