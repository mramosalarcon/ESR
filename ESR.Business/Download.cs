namespace ESR.Business
{
    public class Download
    {
        ESR.Data.Download arc = new ESR.Data.Download();
        private string _nombrearchivo;

        public string nombrearchivo
        {
            get { return _nombrearchivo; }
            set { _nombrearchivo = value; }
        }

        //public DataSet TraeArchivo()
        //{
        //    //return arc
        //}

    }
}
