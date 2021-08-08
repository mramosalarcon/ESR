using System;
using System.Data;

namespace ESR.Business
{
    public class Perfil
    {
        private string _sPerfiles;

        public string perfiles
        {
            get
            {
                return _sPerfiles;
            }
            set
            {
                _sPerfiles = value;
            }
        }
    
        public DataSet Carga()
        {
            try
            {
                ESR.Data.Perfil perfiles = new ESR.Data.Perfil();
                return perfiles.Carga(this.perfiles);
            }
            catch (Exception ex)
            {
                throw ex;
            }
   
        }
    }
}
