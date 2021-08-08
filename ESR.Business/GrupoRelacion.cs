
namespace ESR.Business
{
    public class GrupoRelacion
    {
        private int _iIdIndicador;
        private int _iIdTema;
        private int _iIdEmpresa;
        private int _iIdCuestionario;
        private int _iIdGrupo;
        private string _sIdUsuario;

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

        public int idGrupo
        {
            get
            {
                return _iIdGrupo;
            }
            set
            {
                _iIdGrupo = value;
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

        public bool Guardar()
        {
            ESR.Data.GrupoRelacion grpRlc = new ESR.Data.GrupoRelacion();
            return grpRlc.Guardar(_iIdIndicador, _iIdTema, _iIdEmpresa, _iIdCuestionario, _iIdGrupo, _sIdUsuario);
        }

        public bool Eliminar()
        {
            ESR.Data.GrupoRelacion grpRlc = new ESR.Data.GrupoRelacion();
            return grpRlc.Eliminar(_iIdIndicador, _iIdTema, _iIdEmpresa, _iIdCuestionario, _iIdGrupo);
        }
    }
}
