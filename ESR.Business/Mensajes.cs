using System.Data;

namespace ESR.Business
{
    public class Mensajes
    {
        private int _iidEmpresa;
        public int idEmpresa
        {
            get
            {
                return _iidEmpresa;
            }
            set
            {
                _iidEmpresa = value;
            }
        }

        public DataSet CargaSolicitudesDeVinculacion()
        {
            ESR.Data.Mensajes mensajes = new ESR.Data.Mensajes();
            return mensajes.CargaSolicitudesDeVinculacion(_iidEmpresa);
        }
    }
}
