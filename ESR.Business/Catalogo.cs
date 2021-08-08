using System.Data;

namespace ESR.Business
{
    public class Catalogo
    {
        public DataSet Carga(string nombre)
        {
            ESR.Data.Catalogo cat = new ESR.Data.Catalogo();
            return cat.Carga(nombre);
        }
    }
}
