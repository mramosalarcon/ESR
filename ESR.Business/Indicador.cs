using System;
using System.Data;
using System.IO;
using System.Configuration;

namespace ESR.Business
{

    public class Indicador
    {
        #region Propiedades
        private string _sUrl = "";
        public string Url
        {
            get
            {
                return _sUrl;
            }
            set
            {
                _sUrl = value;
            }
        }
        private int _iidIndicador = 0;
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

        private int _iidSubtema = 0;
        public int idSubtema
        {
            get
            {
                return _iidSubtema;
            }
            set
            {
                _iidSubtema = value;
            }
        }

        private int _iidTema = 0;
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

        private int _iOrdinal = 0;
        public int ordinal
        {
            get
            {
                return _iOrdinal;
            }
            set
            {
                _iOrdinal = value;
            }
        }

        private float _fvalor = 0;
        public float valor
        {
            get
            {
                return _fvalor;
            }
            set
            {
                _fvalor = value;
            }
        }

        private float _fCalificacionRevisor;
        public float calificacionRevisor
        {
            get
            {
                return _fCalificacionRevisor;
            }
            set
            {
                _fCalificacionRevisor = value;
            }
        }

        private string _sdescripcion;
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
        
        private string _sdescripcionAlterna;
        public string descripcionAlterna
        {
            get
            {
                return _sdescripcionAlterna;
            }
            set
            {
                _sdescripcionAlterna = value;
            }
        }
        
        private string _sdescripcionPortugues;
        public string descripcionPortugues
        {
            get
            {
                return _sdescripcionPortugues;
            }
            set
            {
                _sdescripcionPortugues = value;
            }
        }
        private string _sRecomendacion;
        public string recomendacion
        {
            get
            {
                return _sRecomendacion;
            }
            set
            {
                _sRecomendacion = value;
            }
        }
        private string _sEvidencias;
        public string evidencias
        {
            get
            {
                return _sEvidencias;
            }
            set
            {
                _sEvidencias = value;
            }
        }
        private bool _bEvidencia;

        public bool evidencia
        {
            get
            {
                return _bEvidencia;
            }
            set
            {
                _bEvidencia = value;
            }
        }

        private string _snotas;

        public string notas
        {
            get
            {
                return _snotas;
            }
            set
            {
                _snotas = value;
            }
        }

        private bool _bObligatorio;

        public bool obligatorio
        {
            get
            {
                return _bObligatorio;
            }
            set
            {
                _bObligatorio = value;
            }
        }

        private string _scorto;

        public string corto
        {
            get
            {
                return _scorto;
            }
            set
            {
                _scorto = value;
            }
        }

        private string _sPreguntaAdicional;

        public string preguntaAdicional
        {
            get
            {
                return _sPreguntaAdicional;
            }
            set
            {
                _sPreguntaAdicional = value;
            }
        }

        private bool _bObligatorioAdicional;

        public bool obligatorioAdicional
        {
            get
            {
                return _bObligatorioAdicional;
            }
            set
            {
                _bObligatorioAdicional = value;
            }
        }
        #endregion

        public int GuardaAfinidad(int idAfinidad)
        {
            try
            {
                ESR.Data.Indicador ind = new ESR.Data.Indicador();
                return ind.GuardaAfinidad(this.idTema, this.idIndicador, idAfinidad);
            }
            catch (Exception ex)
            {
                throw new Exception("ESR.Business.Indicador.GuardaAfinidad(): " + ex.Message);
            }
        }

        public int GuardaPrincipioRSE(int idPrincipioRSE)
        {
            try
            {
                ESR.Data.Indicador ind = new ESR.Data.Indicador();
                return ind.GuardaPrincipioRSE(this.idTema, this.idIndicador, idPrincipioRSE);
            }
            catch (Exception ex)
            {
                throw new Exception("ESR.Business.Indicador.GuardaAfinidad(): " + ex.Message);
            }
        }
        public int CargarSiguienteId(int idTema)
        {
            ESR.Data.Indicador ind = new ESR.Data.Indicador();
            return ind.CargarSiguienteId(idTema);
        }

        /// <summary>
        /// Método de la clase de negocio para guardar la información del indicador
        /// </summary>
        /// <returns></returns>
        public bool Guardar()
        {
            bool resultado = false;
            try
            {
                ESR.Data.Indicador ind = new ESR.Data.Indicador();
                resultado = ind.Guardar(_iidIndicador, _iidSubtema, _iidTema, _fvalor, _sdescripcion, _bEvidencia, this.obligatorio, this.notas, this.corto, this.idUsuario, this.recomendacion, this.evidencias, _bLiberado, _sdescripcionAlterna, _sdescripcionPortugues);
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception("ESR.Business.Indicador.Guardar(): " + ex.Message);
            }
        }

        public DataSet CargaXSubtema(int idTema, int idSubtema)
        {
            ESR.Data.Indicador indicador = new ESR.Data.Indicador();
            //return indicador.CargaXSubtema(idTema, idSubtema);
            return null;
        }

        public DataSet Carga(int idTema, int idIndicador)
        {
            try
            {

                ESR.Data.Indicador indicador = new ESR.Data.Indicador();
                return indicador.Carga(idTema, idIndicador);
            }
            catch (Exception ex)
            {
                throw new Exception("ESR.Business.Indicador.Carga(int idTema, int idIndicador): " + ex.Message);
            }

        }

        public DataSet Carga(int idTema, int idIndicador, int idCuestionario, bool bloqueado)
        {
            try
            {

                ESR.Data.Indicador indicador = new ESR.Data.Indicador();
                DataSet dsIndicadores = indicador.Carga(idTema, idIndicador, idCuestionario, bloqueado);
                if (idCuestionario.ToString() == ConfigurationManager.AppSettings["idMedianas"].ToString())
                {
                    dsIndicadores.Tables["Inciso"].Rows.Clear();
                }
                return dsIndicadores;
            }
            catch (Exception ex)
            {
                throw new Exception("ESR.Business.Indicador.Carga(int idTema, int idIndicador): " + ex.Message);
            }
 
        }

        public DataSet CargaIndicadores(int idTema)
        {
            ESR.Data.Indicador indicadores = new ESR.Data.Indicador();
            return indicadores.CargaIndicadores(idTema);
        }

        public DataSet CargaIndicadores()
        {
            ESR.Data.Indicador indicadores = new ESR.Data.Indicador();
            return indicadores.CargaIndicadores();
        }

        public bool Eliminar()
        {
            ESR.Data.Indicador indicador = new ESR.Data.Indicador();
            return indicador.Eliminar(this.idTema, this.idIndicador);
        }

        public bool GuardarRespuesta()
        {
            ESR.Data.Indicador indicador = new ESR.Data.Indicador();
            return indicador.GuardarRespuesta(this.idIndicador, this.idTema, this.idEmpresa, this.idCuestionario, this.idTipoRespuesta, this.idUsuario);
        }

        private int _iIdEmpresa;

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

        private int _iIdCuestionario;

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

        private int _iIdTipoRespuesta;

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

        private string _sIdUsuario;

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

        private int _iIdTipoEvidencia;

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

        private byte[] _arrEvidencia;
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

        private string _sFileName;
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

        public bool GuardarEvidencia()
        {
            try
            {
                ESR.Data.Indicador indicador = new ESR.Data.Indicador();
                return indicador.GuardarEvidencia(this.idIndicador, this.idTema, this.idTipoEvidencia, this.idEmpresa, this.idCuestionario, this.fileName, this.idTipoRespuesta, this.arrEvidencia, this.streEvidencia, this.idUsuario, this.Url);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error en ESR.Business.Indicador.GuardarEvidencia(): " + ex.Message, ex);
            }
        }

        public bool EliminarEvidencia()
        {
            ESR.Data.Indicador indicador = new ESR.Data.Indicador();
            return indicador.EliminarEvidencia(this.idIndicador, this.idTema, this.idTipoEvidencia, this.idEmpresa, this.idCuestionario, this.fileName); 
        }

        public bool GuardarEvaluacion()
        {
            ESR.Data.Indicador indicador = new ESR.Data.Indicador();
            return indicador.GuardarEvaluacion(this.idIndicador, this.idTema, this.idEmpresa, this.idCuestionario, this.idUsuario, this.valor, this.calificacionRevisor);  
        }

        public byte[] CargaEvidencia()
        {
            ESR.Data.Indicador indicador = new ESR.Data.Indicador();
            return indicador.CargaEvidencia(this.idIndicador, this.idTema, this.idTipoEvidencia, this.idEmpresa, this.idCuestionario, this.fileName);   
        }

        public int CargaIdIndicador()
        {
            ESR.Data.Indicador indicador = new ESR.Data.Indicador();
            return indicador.CargaIdIndicador(this.idCuestionario, this.idTema, this.ordinal);
        }

        private bool _bLiberado;

        public bool liberado
        {
            get
            {
                return _bLiberado;
            }
            set
            {
                _bLiberado = value;
            }
        }
    }
}
