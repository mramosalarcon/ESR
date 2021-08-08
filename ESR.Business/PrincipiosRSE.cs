using System.Data;

namespace ESR.Business
{
    public class PrincipiosRSE
    {
        private string _sDescripcion;
        private bool _bBloqueado;
        private string _sIdUsuario;
        private int _iOrdinal;

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
    
        public DataSet CargaTodos()
        {
            ESR.Data.PrincipiosRSE principioRSE = new ESR.Data.PrincipiosRSE();
            return principioRSE.CargaTodos();
        }

        public bool Guarda()
        {
            ESR.Data.PrincipiosRSE principioRSE = new ESR.Data.PrincipiosRSE();
            return principioRSE.Guarda(this.descripcion, this.idUsuario);
        }

        public bool Elimina()
        {
            ESR.Data.PrincipiosRSE principioRSE = new ESR.Data.PrincipiosRSE();
            return principioRSE.Elimina(this.descripcion, this.idUsuario);
        }
    }
}
