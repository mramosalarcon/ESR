using System.Data;

namespace ESR.Business
{
    public class Afinidad
    {
        private string _sDescripcion;
        private bool _bBloqueado;
        private string _sIdUsuario;
        private int _iOrdinal;
        private int _iIdAfinidad;

        public int idAfinidad
        {
            get
            {
                return _iIdAfinidad;
            }
            set
            {
                _iIdAfinidad = value;
            }
        }

        public string descripcion
        {
            get
            {
                return _sDescripcion;
            }
            set
            {
                _sDescripcion = value;
            }
        }

        public bool bloqueado
        {
            get
            {
                return _bBloqueado;
            }
            set
            {
                _bBloqueado = value;
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
    
        public DataSet CargaTodas()
        {
            ESR.Data.Afinidad afinidades = new ESR.Data.Afinidad();
            return afinidades.CargaTodas();
        }

        public bool Guarda()
        {
            ESR.Data.Afinidad afinidades = new ESR.Data.Afinidad();
            return afinidades.Guarda(_sDescripcion, _sIdUsuario, _iIdAfinidad);
        }

        public bool Elimina()
        {
            ESR.Data.Afinidad afinidades = new ESR.Data.Afinidad();
            return afinidades.Elimina(this.descripcion, this.idUsuario);
        }
    }
}
