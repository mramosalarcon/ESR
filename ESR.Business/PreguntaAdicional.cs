using System.Data;

namespace ESR.Business
{
    public class PreguntaAdicional
    {
        private int _iIdTema;
        private int _iIdIndicador;
        private int _iIdPregunta;
        private string _sPregunta;
        private bool _bObligatorio;
        private int _iIdEmpresa;
        private int _iIdCuestionario;
        private string _sIdUsuario;

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

        public int idPregunta
        {
            get
            {
                return _iIdPregunta;
            }
            set
            {
                _iIdPregunta = value;
            }
        }

        public string pregunta
        {
            get
            {
                return _sPregunta;
            }
            set
            {
                _sPregunta = value;
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

        public bool Eliminar()
        {
            ESR.Data.PreguntaAdicional pa = new ESR.Data.PreguntaAdicional();
            return pa.Eliminar(_iIdTema, _iIdIndicador, _iIdPregunta);
        }

        public DataSet CargaPAs()
        {
            ESR.Data.PreguntaAdicional pa = new ESR.Data.PreguntaAdicional();
            return pa.CargaPAs(_iIdTema, _iIdIndicador);
        }

        public bool Guardar()
        {
            ESR.Data.PreguntaAdicional pa = new ESR.Data.PreguntaAdicional();
            return pa.Guardar(_iIdTema, _iIdIndicador, _iIdPregunta, _sPregunta, _bObligatorio);
        }

        public bool GuardarRespuestaEmpresa()
        {
            ESR.Data.PreguntaAdicional pa = new ESR.Data.PreguntaAdicional();
            return pa.GuardarRespuestaEmpresa(_iIdTema, _iIdIndicador,_iIdCuestionario, _iIdPregunta, _iIdEmpresa, _sPregunta, _sIdUsuario);

        }
    }
}
