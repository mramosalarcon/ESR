using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;


namespace ESR.Data
{
    public class Tema
    {
        public bool Guardar(int idTema, int idSubtema, string descripcion, int ordinal, bool bloqueado, string idUsuario)
        {
            bool result = false;

            Database db = DatabaseFactory.CreateDatabase();
            string sqlCommand = "CESR_GuardarSubtema";
            DbCommand subTemaCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(subTemaCommand, "idTema", DbType.Int32, idTema);
            db.AddInParameter(subTemaCommand, "idSubtema", DbType.Int32, idSubtema);
            db.AddInParameter(subTemaCommand, "descripcion", DbType.String, descripcion);
            db.AddInParameter(subTemaCommand, "ordinal", DbType.Int32, ordinal);
            db.AddInParameter(subTemaCommand, "bloqueado", DbType.Boolean, bloqueado);
            db.AddInParameter(subTemaCommand, "idUsuario", DbType.String, idUsuario);

            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    // Credit the first account
                    db.ExecuteNonQuery(subTemaCommand, transaction);
                    // Debit the second account
                    // db.ExecuteNonQuery(debitCommand, transaction);

                    // Commit the transaction
                    transaction.Commit();

                    result = true;
                }
                catch
                {
                    // Rollback transaction 
                    transaction.Rollback();
                }
                connection.Close();


            }
            return result;
        }

        public bool Guardar(int idTema, string descripcion, int ordinal, bool bloqueado, string descripcionCorta, string idUsuario)
        {
            bool result = false;

            Database db = DatabaseFactory.CreateDatabase();
            string sqlCommand = "CESR_GuardarTema";
            DbCommand temaCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(temaCommand, "idTema", DbType.Int32, idTema);
            db.AddInParameter(temaCommand, "descripcion", DbType.String, descripcion);
            db.AddInParameter(temaCommand, "ordinal", DbType.Int32, ordinal);
            db.AddInParameter(temaCommand, "bloqueado", DbType.Boolean, bloqueado);
            db.AddInParameter(temaCommand, "descripcionCorta", DbType.String, descripcionCorta);
            db.AddInParameter(temaCommand, "idUsuario", DbType.String, idUsuario);

            using (DbConnection connection = db.CreateConnection())
            {
               
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Credit the first account
                    db.ExecuteNonQuery(temaCommand, transaction);
                    // Debit the second account
                    // db.ExecuteNonQuery(debitCommand, transaction);

                    // Commit the transaction
                    transaction.Commit();

                    result = true;
                }
                catch
                {
                    // Rollback transaction 
                    transaction.Rollback();
                }
                connection.Close();

               
            }
            return result;
        }

        public bool ActualizaOrden(int idCuestionario, int idTema, int idIndicador, int iOrdinal)
        {
            Database db = DatabaseFactory.CreateDatabase();
            string sqlCommand = "CESR_Actualiza_Orden_Tema_Cuestionario";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "idCuestionario", DbType.Int32, idCuestionario);
            db.AddInParameter(dbCommand, "idTema", DbType.Int32, idTema);
            db.AddInParameter(dbCommand, "idIndicador", DbType.Int32, idIndicador);
            db.AddInParameter(dbCommand, "iOrdinalActual", DbType.Int32, iOrdinal);

            
            db.ExecuteNonQuery(dbCommand);
            
            return true;
        }


        public DataSet CargaOrdinales(int idTema)
        {
            Database db = DatabaseFactory.CreateDatabase();
            string sqlCommand = "CESR_CargaOrdinales_Tema_Solo";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "idTema", DbType.Int32, idTema);

            DataSet ordinales = null;
            ordinales = db.ExecuteDataSet(dbCommand);
            ordinales.Tables[0].TableName = "Ordinales";

            return ordinales;
        }

        public DataSet CargaOrdinales(int idCuestionario, int idTema)
        {
            Database db = DatabaseFactory.CreateDatabase();
            string sqlCommand = "CESR_CargaOrdinales_Tema";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "idCuestionario", DbType.Int32, idCuestionario);
            db.AddInParameter(dbCommand, "idTema", DbType.Int32, idTema);
       
            DataSet ordinales = null;
            ordinales = db.ExecuteDataSet(dbCommand);
            ordinales.Tables[0].TableName = "Ordinales";

            return ordinales;
        }

        public DataSet CargaTemas(bool bBanco)
        {
            Database db = DatabaseFactory.CreateDatabase();
            string sqlCommand = "";
            if (!bBanco)
                sqlCommand = "CESR_CargaTemas_Subtemas";
            else
                sqlCommand = "CESR_CargaTemas_Subtemas_Banco";

            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            DataSet temas = null;

            temas = db.ExecuteDataSet(dbCommand);

            temas.Tables[0].TableName = "Tema";
            temas.Tables[1].TableName = "Subtema";

            temas.Relations.Add("Children",
                temas.Tables["Tema"].Columns["idTema"],
                temas.Tables["Subtema"].Columns["idTema"]);

            return temas;
        }

        public DataSet CargaSubtemas(int selectedValue)
        {
            Database db = DatabaseFactory.CreateDatabase();
            string sqlCommand = "CESR_CargaSubtemas";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "selectedValue", DbType.Int32, selectedValue);

            DataSet subTemas = null;

            subTemas = db.ExecuteDataSet(dbCommand);
            return subTemas;

        }
    }
}
