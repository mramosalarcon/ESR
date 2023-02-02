using System;
using System.Data;

namespace ESR.Business
{
    public class Tema
    {
        private int _iIdTema;
        private int _iIdCuestionario;
        private bool _bBanco;
        private string _sDescripcion;
        private string _sDescripcionCorta;
        private int _iOrdinal;
        private bool _bBloqueado;
        private int _iIdSubtema;

        public bool banco
        {
            get
            {
                return _bBanco;
            }
            set
            {
                _bBanco = value;
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

        public string descripcionCorta
        {
            get
            {
                return _sDescripcionCorta;
            }
            set
            {
                _sDescripcionCorta = value;
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

        public int idSubtema
        {
            get
            {
                return _iIdSubtema;
            }
            set
            {
                _iIdSubtema = value;
            }
        }
    
        public DataSet CargaTemas()
        {
            try
            {
                ESR.Data.Tema temas = new ESR.Data.Tema();
                return temas.CargaTemas(_bBanco);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public DataSet CargaTemasCuestionario(int idCuestionario)
        {
            try
            {
                ESR.Data.Tema temas = new ESR.Data.Tema();
                return temas.CargaTemasCuestionario(idCuestionario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet CargaSubtemas(int selectedValue)
        {
            ESR.Data.Tema subtemas = new ESR.Data.Tema();
            return subtemas.CargaSubtemas(selectedValue);
        }

        public DataSet CargaOrdinales()
        {
            ESR.Data.Tema ordinales = new ESR.Data.Tema();
            return ordinales.CargaOrdinales(this.idCuestionario, this.idTema);
        }


        public DataSet CargaOrdinales(int idTema)
        {
            ESR.Data.Tema ordinales = new ESR.Data.Tema();
            return ordinales.CargaOrdinales(idTema);
        }

        public bool ActualizaOrden(int idIndicador, int iOrdinal)
        {
            ESR.Data.Tema ordinales = new ESR.Data.Tema();
            return ordinales.ActualizaOrden(this.idCuestionario, this.idTema, idIndicador, iOrdinal); 
        }

        public bool Guardar(string idUsuario)
        {
            ESR.Data.Tema newTema = new ESR.Data.Tema();
            return newTema.Guardar(_iIdTema, _sDescripcion, _iOrdinal,_bBloqueado, _sDescripcionCorta, idUsuario); 
        }

        public bool GuardarSubtema(string idUsuario)
        {
            ESR.Data.Tema newTema = new ESR.Data.Tema();
            return newTema.Guardar(_iIdTema, _iIdSubtema, _sDescripcion, _iOrdinal, _bBloqueado, idUsuario);
        }
    }
}
