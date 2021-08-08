using System;
using System.Web;
using ESR.Business;

public partial class Download : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        byte[] byteFile;
        string sExtension;

        if (Request.Params["idInciso"] == null)
        {
            //Evidencia_Indicador
            Indicador eviIndicador = new Indicador();
            eviIndicador.idIndicador = Convert.ToInt32(Request.Params["idIndicador"].ToString());
            eviIndicador.idTema = Convert.ToInt32(Request.Params["idTema"].ToString());
            eviIndicador.idTipoEvidencia = Convert.ToInt32(Request.Params["idTipoEvidencia"].ToString());
            eviIndicador.idEmpresa = Convert.ToInt32(Request.Params["idEmpresa"].ToString());
            eviIndicador.idCuestionario = Convert.ToInt32(Request.Params["idCuestionario"].ToString());
            eviIndicador.fileName = Request.Params["descripcion"].ToString();
            sExtension = eviIndicador.fileName.Substring(eviIndicador.fileName.Length - 4, 4).ToLower();
            byteFile = eviIndicador.CargaEvidencia();

        }
        else
        {
            //Evidencia_Incisp
            Inciso eviInciso = new Inciso();
            eviInciso.idIndicador = Convert.ToInt32(Request.Params["idIndicador"].ToString());
            eviInciso.idTema = Convert.ToInt32(Request.Params["idTema"].ToString());
            eviInciso.idInciso = Request.Params["idInciso"].ToString();
            eviInciso.idTipoEvidencia = Convert.ToInt32(Request.Params["idTipoEvidencia"].ToString());
            eviInciso.idEmpresa = Convert.ToInt32(Request.Params["idEmpresa"].ToString());
            eviInciso.idCuestionario = Convert.ToInt32(Request.Params["idCuestionario"].ToString());
            eviInciso.fileName = Request.Params["descripcion"].ToString();
            sExtension = eviInciso.fileName.Substring(eviInciso.fileName.Length - 4, 4).ToLower();
            byteFile = eviInciso.CargaEvidencia();
        }

        Response.Clear();
        Response.Buffer = true;

        if (sExtension.ToLower() == ".doc" || sExtension.ToLower() == "docx")
        {
           Response.ContentType = "application/msword";
           Response.AddHeader("content-disposition", "inline;filename="+ Request.Params["descripcion"].ToString());
        }
        else if (sExtension.ToLower() == ".xls" || sExtension.ToLower() == "xlsx")
        {
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", "attachment;filename=" + Request.Params["descripcion"].ToString());
        }
        else if (sExtension.ToLower() == ".pdf")
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=\"" + Request.Params["descripcion"].ToString() + "\"");
        }
        else if (sExtension.ToLower() == ".ppt" || sExtension.ToLower() == "pptx" || sExtension.ToLower() == ".pps")
        {
            Response.ContentType = "application/ms-powerpoint";
            Response.AddHeader("content-disposition", "attachment;filename=" + Request.Params["descripcion"].ToString());        
        }
        else if (sExtension.ToLower() == ".tif" || sExtension.ToLower() == "tiff")
        { 
            Response.ContentType = "image/tiff";
            Response.AddHeader("content-disposition", "attachment;filename=" + Request.Params["descripcion"].ToString());        
        }
        else if (sExtension.ToLower() == ".rar")
        {
            Response.ContentType = "application/x-rar-compressed";
            Response.AddHeader("content-disposition", "attachment;filename=" + Request.Params["descripcion"].ToString());
        }
        else if (sExtension.ToLower() == ".jpg" || sExtension.ToLower() == "jpeg")
        {
            Response.ContentType = "image/JPEG";
            Response.AddHeader("content-disposition", "attachment;filename=" + Request.Params["descripcion"].ToString());
        }
        else if (sExtension.ToLower() == ".png")
        {
            Response.ContentType = "image/png";
            Response.AddHeader("content-disposition", "attachment;filename=" + Request.Params["descripcion"].ToString());
        }
        else if (sExtension.ToLower() == ".gif")
        {
            Response.ContentType = "image/gif";
            Response.AddHeader("content-disposition", "attachment;filename=" + Request.Params["descripcion"].ToString());
        }
        else if (sExtension.ToLower() == ".zip")
        {
            Response.ContentType = "application/zip";
            Response.AddHeader("content-disposition", "attachment;filename=" + Request.Params["descripcion"].ToString());
        }
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        if (byteFile != null)
        {
            int bufferLength = byteFile.GetLength(0);
            Response.BinaryWrite(byteFile);
         
        }
        Response.End();
    }
}
