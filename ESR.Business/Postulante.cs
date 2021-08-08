using System;
using System.Data;

namespace ESR.Business
{
    public class Postulante
    {
        public DataSet CargaPostulantes()
        {
            DataSet dsPostulantes;
            try
            {
                ESR.Data.Postulante postulante = new ESR.Data.Postulante();
                dsPostulantes = postulante.CargaPostulantes();
                return dsPostulantes;

            }
            catch (Exception ex)
            {
                throw new Exception("ESR.Business.Postulante.CargaPostulantes(): " + ex.Message);
            }
        }
    }
}
