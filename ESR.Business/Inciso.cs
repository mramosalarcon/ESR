using System;
using System.Data;
using System.IO;

namespace ESR.Business
{
    public class Inciso
    {
        private string _sidInciso;
        private int _iidIndicador;
        private int _iidTema;
        private string _sdescripcion;
        private int _iIdEmpresa;
        private int _iIdCuestionario;
        private int _iIdTipoRespuesta;
        private string _sIdUsuario;

        private int _iIdTipoEvidencia;
        private byte[] _arrEvidencia;
        private string _sFileName;
        private Stream _streEvidencia;
        public Stream streEvidencia
        {
            get
            {
                return _streEvidencia;
            }
            set
            {
                _streEvidencia = value;
            }
        }
        public string fileName
        {
            get
            {
                return _sFileName;
            }
            set
            {
                _sFileName = value;
            }
        }
        

        public byte[] arrEvidencia
        {
            get
            {
                return _arrEvidencia;
            }
            set
            {
                _arrEvidencia = value;
            }
        }

        public int idTipoEvidencia
        {
            get
            {
                return _iIdTipoEvidencia;
            }
            set
            {
                _iIdTipoEvidencia = value;
            }
        }
        public int idIndicador
        {
            get
            {
                return _iidIndicador;
            }
            set
            {
                _iidIndicador = value;
            }
        }

        public string idInciso
        {
            get
            {
                return _sidInciso;
            }
            set
            {
                _sidInciso = value;
            }
        }

        public int idTema
        {
            get
            {
                return _iidTema;
            }
            set
            {
                _iidTema = value;
            }
        }

        public string descripcion
        {
            get
            {
                return _sdescripcion;
            }
            set
            {
                _sdescripcion = value;
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

        public int idTipoRespuesta
        {
            get
            {
                return _iIdTipoRespuesta;
            }
            set
            {
                _iIdTipoRespuesta = value;
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

        public DataSet CargaSiguienteInciso(int idTema, int idIndicador)
        {
            ESR.Data.Inciso inc = new ESR.Data.Inciso();
            return inc.CargaSiguienteInciso(idTema, idIndicador);
        }
        public int GuardaInciso()
        {
            ESR.Data.Inciso inc = new ESR.Data.Inciso();
            return inc.GuardaInciso(this.idInciso, this.idIndicador, this.idTema, this.descripcion);
        }
        public DataSet CargaIncisos()
        {
            ESR.Data.Inciso inc = new ESR.Data.Inciso();
            return inc.CargaIncisos(this.idTema, this.idIndicador);
        }

        public bool Eliminar()
        {
            ESR.Data.Inciso inc = new ESR.Data.Inciso();
            return inc.Eliminar(this.idTema, this.idIndicador, this.idInciso);
        }

        public bool GuardarRespuesta()
        {
            ESR.Data.Inciso inc = new ESR.Data.Inciso();
            return inc.GuardarRespuesta(this.idIndicador, this.idTema, this.idInciso, this.idEmpresa, this.idCuestionario, this.idTipoRespuesta, this.idUsuario);
        }

        public bool GuardarEvidencia()
        {
            try
            {
                ESR.Data.Inciso inciso = new ESR.Data.Inciso();
                return inciso.GuardarEvidencia(this.idIndicador, this.idTema, this.idTipoEvidencia, this.idInciso, this.idEmpresa, this.idCuestionario, this.fileName, this.idTipoRespuesta, this.arrEvidencia, this.streEvidencia, this.idUsuario);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error en ESR.Business.Inciso.GuardarEvidencia(): " + ex.Message, ex);
            }
        }

        public bool EliminarEvidencia()
        {
            ESR.Data.Inciso inciso = new ESR.Data.Inciso();
            return inciso.EliminarEvidencia(this.idIndicador, this.idTema, this.idTipoEvidencia, this.idInciso, this.idEmpresa, this.idCuestionario, this.fileName); 
        }

        public byte[] CargaEvidencia()
        {
            ESR.Data.Inciso inciso = new ESR.Data.Inciso();
            return inciso.CargaEvidencia(this.idIndicador, this.idTema, this.idInciso, this.idTipoEvidencia, this.idEmpresa, this.idCuestionario, this.fileName);
        }
    }
}
