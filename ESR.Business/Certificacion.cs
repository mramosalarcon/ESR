using System.Data;

namespace ESR.Business
{
    public class Certificacion
    {
        private int _iAnio;
        private int _iIdEmpresa;
        private string _sInstitucion;
        private string _sCertificacion;
        private int _idCertificacion;

        public int anio
        {
            get
            {
                return _iAnio;
            }
            set
            {
                _iAnio = value;
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

        public string certificacion
        {
            get
            {
                return _sCertificacion;
            }
            set
            {
                _sCertificacion = value;
            }
        }

        public string institucion
        {
            get
            {
                return _sInstitucion;
            }
            set
            {
                _sInstitucion = value;
            }
        }

        public int idcertificacion
        {
            get { return _idCertificacion; }
            set { _idCertificacion = value; }
        }

        public bool Guardar()
        {
            ESR.Data.Certificacion cert = new ESR.Data.Certificacion();
            return cert.Guardar(this.idEmpresa, this.institucion, this.certificacion, this.anio);
        }

        public DataSet CargaCertificaciones()
        {
            ESR.Data.Certificacion certificaciones = new ESR.Data.Certificacion();
            return certificaciones.CargaCertificaciones(this.idEmpresa);
        }

        public bool Eliminar()
        {
            ESR.Data.Certificacion eliminar = new ESR.Data.Certificacion();
            return eliminar.Eliminar(this.idcertificacion, this.idEmpresa);
        }

        public bool Actualizar()
        {
            ESR.Data.Certificacion actualizar = new ESR.Data.Certificacion();
            return actualizar.Actualizar(this.idEmpresa, this.institucion, this.certificacion, this.anio, this.idcertificacion);
        }

        
    }
}
