using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ESR.Data
{
    public class Usuario
    {
        //public string RecuperarPassword(string usrid)
        //{
        //    Database db = DatabaseFactory.CreateDatabase();
        //    string sqlCommand = "Select pwd from Usuarios Where idUsuario = '" + usrid + "'";
        //    DbCommand dbCommand = db.GetSqlStringCommand(sqlCommand);

        //    DataSet dsPwd = null;
        //    dsPwd = db.ExecuteDataSet(dbCommand);
        //    return dsPwd.Tables[0].Rows[0][0].ToString();
        //}
        public bool unsuscribe(string idUsuario)
        {
            bool result = false;
            Database db = DatabaseFactory.CreateDatabase();
            string text = "CESR_unsuscribeUsuario";
            DbCommand storedProcCommand = db.GetStoredProcCommand(text);
            db.AddInParameter(storedProcCommand, "idUsuario", DbType.String, (object)idUsuario);
            using (DbConnection dbConnection = db.CreateConnection())
            {
                dbConnection.Open();
                DbTransaction dbTransaction = dbConnection.BeginTransaction();
                try
                {
                    db.ExecuteNonQuery(storedProcCommand, dbTransaction);
                    dbTransaction.Commit();
                    result = true;
                }
                catch
                {
                    dbTransaction.Rollback();
                }
                dbConnection.Close();
                return result;
            }
        }

        public DataSet CargaUsuario(string idUsuario, bool adm)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                string sqlCommand = "CESR_CargaSesion";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(dbCommand, "usrid", DbType.String, idUsuario);
                db.AddInParameter(dbCommand, "adm", DbType.Boolean, adm);

                DataSet dsUser = null;
                dsUser = db.ExecuteDataSet(dbCommand);

                dsUser.Tables[0].TableName = "Existe";
                dsUser.Tables[1].TableName = "Usuario";
                dsUser.Tables[2].TableName = "Perfil";
                dsUser.Tables[3].TableName = "Tema";

                if (dsUser.Tables["Usuario"].Rows.Count > 0)
                {
                    dsUser.Relations.Add("Children",
                        dsUser.Tables["Usuario"].Columns["idUsuario"],
                        dsUser.Tables["Perfil"].Columns["idUsuario"]);
                }

                return dsUser;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error en ESR.Data.Usuario.CargaUsuario(): " + ex.Message, ex);
            }
        }

        public bool Guarda(string idUsuario, byte[] pwd, string nombre, string apellidoP,
                                   string apellidoM, string puesto, string telefono,
                                   string extension, bool bloqueado,int pais)
        {
            bool result = false;

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "CESR_GuardarUsuario";
            DbCommand contactoCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(contactoCommand, "idUsuario", DbType.String, idUsuario);
            db.AddInParameter(contactoCommand, "pwd", DbType.Binary, pwd);
            db.AddInParameter(contactoCommand, "nombre", DbType.String, nombre);
            db.AddInParameter(contactoCommand, "apellidoP", DbType.String, apellidoP);
            db.AddInParameter(contactoCommand, "apellidoM", DbType.String, apellidoM);
            db.AddInParameter(contactoCommand, "puesto", DbType.String, puesto);
            db.AddInParameter(contactoCommand, "telefono", DbType.String, telefono);
            db.AddInParameter(contactoCommand, "extension", DbType.String, extension);
            db.AddInParameter(contactoCommand, "bloqueado", DbType.Boolean, bloqueado);
            db.AddInParameter(contactoCommand, "pais", DbType.Int32, pais);

            //sqlCommand = "Delete from Usuario_Perfil Where idUsuario = '" + idUsuario + "'";
            //DbCommand deleteUsrPerfCommand = db.GetSqlStringCommand(sqlCommand);

            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();

                try
                {
                    db.ExecuteNonQuery(contactoCommand, transaction);
                    //db.ExecuteNonQuery(deleteUsrPerfCommand, transaction);
                    transaction.Commit();
                    result = true;
                }
                catch
                {
                    // Rollback transaction 
                    transaction.Rollback();
                }
                connection.Close();

                return result;
            }
        }

        public DataSet ValidaUsuario(string usrid, byte[] pwd, bool adm)
        {
            Database db = DatabaseFactory.CreateDatabase();
            string sqlCommand = "CESR_ValidaUsuario";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            
            db.AddInParameter(dbCommand, "usrId", DbType.String, usrid);
            db.AddInParameter(dbCommand, "pwd", DbType.Binary, pwd);
            db.AddInParameter(dbCommand, "adm", DbType.Boolean, adm);

            DataSet dsUser = null;

            dsUser = db.ExecuteDataSet(dbCommand);

            dsUser.Tables[0].TableName = "Existe";
            dsUser.Tables[1].TableName = "Usuario";
            dsUser.Tables[2].TableName = "Perfil";
            dsUser.Tables[3].TableName = "Tema";

            if (dsUser.Tables["Usuario"].Rows.Count > 0)
            {
                dsUser.Relations.Add("Children",
                    dsUser.Tables["Usuario"].Columns["idUsuario"],
                    dsUser.Tables["Perfil"].Columns["idUsuario"]);
            }
            //if (DateTime.Now <= Convert.ToDateTime("10/03/2008"))
            {
                return dsUser;
            }
            //else
            //{
            //    return null;
            //}
        }

        public DataSet CargaUsuarios(int idEmpresa)
        {
            Database db = DatabaseFactory.CreateDatabase();
            string sqlCommand = "CESR_CargaUsuariosEmpresa";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "idEmpresa", DbType.Int32, idEmpresa);

            DataSet dsUser = null;
            dsUser = db.ExecuteDataSet(dbCommand);

            dsUser.Tables[0].TableName = "Usuario";
            dsUser.Tables[1].TableName = "Perfil";

            dsUser.Relations.Add("Children",
                dsUser.Tables["Usuario"].Columns["idUsuario"],
                dsUser.Tables["Perfil"].Columns["idUsuario"]);

            return dsUser;
        }

        public bool GuardarPassword(string idUsuario, byte[] pwd)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                string sqlCommand = "CESR_GuardarPalabra";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(dbCommand, "idUsuario", DbType.String, idUsuario);
                db.AddInParameter(dbCommand, "pwd", DbType.Binary, pwd);

                db.ExecuteNonQuery(dbCommand);
                
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public bool Existe(string idUsuario)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                string sqlCommand = "CESR_ExisteUsuario";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
                db.AddInParameter(dbCommand, "idUsuario", DbType.String, idUsuario);
                int usuario = (Int32)db.ExecuteScalar(dbCommand);

                if (usuario > 0)
                    return true;
                else
                    return false;
            }
            catch 
            {
                return false;
            }
        }
        public bool Existe(string idUsuario, string RFC, string CP)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                string sqlCommand = "CESR_CargaUsuarioRFCCP";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(dbCommand, "idUsuario", DbType.String, idUsuario);
                db.AddInParameter(dbCommand, "rfc", DbType.String, RFC);
                db.AddInParameter(dbCommand, "cp", DbType.String, CP);

                DataSet dsEmpresa = db.ExecuteDataSet(dbCommand);

                if (dsEmpresa.Tables[0].Rows.Count > 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
        public bool CargaBitacora(string idUsuario,int status)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                string sqlCommand = "CESR_CargaBitacora";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
                db.AddInParameter(dbCommand, "idUsuario", DbType.String, idUsuario);
                db.AddInParameter(dbCommand, "idEvento", DbType.Int32, status);
                DataSet dsBitacora = db.ExecuteDataSet(dbCommand);
                if (dsBitacora.Tables[0].Rows.Count > 0)
                {
                    DataTable tblBitacora = new DataTable();
                    tblBitacora = dsBitacora.Tables[0];
                    if (tblBitacora.Rows[0][0].ToString() == "1")
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
