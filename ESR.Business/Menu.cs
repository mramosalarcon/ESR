using System.Data;

namespace ESR.Business
{
    public class Menu
    {
        public Menu()
        { }

        public Menu(string perfiles)
        {
            _sPerfiles = perfiles;
        }
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
    
        public DataSet Carga(string idMenuPadre)
        {
            //if (DateTime.Now <= Convert.ToDateTime("10/03/2008"))
            {
                ESR.Data.Menu menuBI = new ESR.Data.Menu();
                return menuBI.Carga(_sPerfiles, idMenuPadre);
            }
            //else
            //{
            //    return null;
            //}
        }

        public DataSet Carga()
        {
            //if (DateTime.Now <= Convert.ToDateTime("10/03/2008"))
            {
                ESR.Data.Menu menuBI = new ESR.Data.Menu();
                return menuBI.Carga(_sPerfiles);
            }
            //else
            //{
            //    return null;
            //}
        }

        public DataSet CargaTop()
        {
            ESR.Data.Menu menu = new ESR.Data.Menu();
            return menu.CargaTop();
        }

    }
}
