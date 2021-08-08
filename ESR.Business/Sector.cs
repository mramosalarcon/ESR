using System.Data;

namespace ESR.Business
{
    public class Sector
    {
        private int _iIdSector;

        public int idSector
        {
            get
            {
                return _iIdSector;
            }
            set
            {
                _iIdSector = value;
            }            
        }
        public DataSet CargaSectores()
        {
            ESR.Data.Sector sector = new ESR.Data.Sector();
            return sector.CargaSectores();
        }

        public DataSet CargaSubSectores()
        {
            ESR.Data.Sector sector = new ESR.Data.Sector();
            return sector.CargaSubSectores(_iIdSector);
 
        }
    }
}
