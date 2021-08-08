using System.Data;
using System;

namespace ESR.Business
{
    public class Respuesta
    {
        private int _iIdTema;
        private int _iIdIndicador;
        private int _iIdEmpresa;
        private int _iIdCuestionario;
        private bool _bReelevancia;
       
        public bool reelevancia
        {
            get
            {
                return _bReelevancia;
            }
            set
            {
                _bReelevancia = value;
            }
        }

        public int idIndicador
        {
            get
            {
                return _iIdIndicador;
            }
            set
            {
                _iIdIndicador = value;
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
    
        public DataSet CargaTiposDeRespuesta()
        {
            ESR.Data.Respuesta rs = new ESR.Data.Respuesta();
            return rs.CargaTiposDeRespuesta();
        }

        public DataSet CargaTiposDeEvidencia()
        { 
            ESR.Data.Respuesta rs = new ESR.Data.Respuesta();
            return rs.CargaTiposDeEvidencia();
        }

        public bool GuardaRespuesta()
        {
            bool resultado = true;
            try
            {
                return resultado;
            }
            catch
            {
                return false;
            }
        }

        public DataSet Carga()
        {
            ESR.Data.Respuesta rs = new ESR.Data.Respuesta();
            return rs.Carga(this.idIndicador, this.idTema, this.idEmpresa, this.idCuestionario);
        }

        public bool Legalidad()
        {
            bool result = false;

            ESR.Data.Respuesta rs = new ESR.Data.Respuesta();
            DataSet dsLegalidad = rs.Legalidad(this.idTema, this.idEmpresa, this.idCuestionario);
            if (dsLegalidad.Tables[0].Rows.Count > 0)
            {
                DataRow drLegalidad = dsLegalidad.Tables[0].Rows[0];
                string respuesta = drLegalidad["descripcion"].ToString();
                if (respuesta.ToUpper() == "NO")
                {
                    this.idIndicador = Convert.ToInt32(drLegalidad["idIndicador"]);
                }
                else
                    result = true;
            }
            return result;
        }
    }
}
