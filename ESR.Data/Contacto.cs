using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ESR.Data
{
    public class Contacto
    {
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public bool Elimina(string idUsuario)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                string sqlCommand = "CESR_EliminaUsuario";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
                db.AddInParameter(dbCommand, "idUsuario", DbType.String, idUsuario);
                db.ExecuteNonQuery(dbCommand);
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public DataSet Carga(string idUsuario, int idEmpresa)
        {
            Database db = DatabaseFactory.CreateDatabase();
            string sqlCommand = "CESR_CargaUsuario";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "idUsuario", DbType.String, idUsuario);
            db.AddInParameter(dbCommand, "idEmpresa", DbType.String, idEmpresa);


            DataSet dsUsuario = db.ExecuteDataSet(dbCommand);
            dsUsuario.Tables[0].TableName = "Usuario";
            dsUsuario.Tables[1].TableName = "Perfil";
            dsUsuario.Tables[2].TableName = "Tema";

            return dsUsuario;
            

        }
        public bool GuardaContacto(string idUsuario, byte[] pwd, int idEmpresa, string nombre, string apellidoP,
                                    string apellidoM, string puesto, string telefono,
                                    string extension, bool primario, bool bloqueado, string idUsuarioModificacion)
        {
            bool result = false;

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "CESR_GuardarContacto";
            DbCommand contactoCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(contactoCommand, "idUsuario", DbType.String, idUsuario);
            db.AddInParameter(contactoCommand, "pwd", DbType.Binary, pwd);
            db.AddInParameter(contactoCommand, "idEmpresa", DbType.Int32, idEmpresa);
            db.AddInParameter(contactoCommand, "nombre", DbType.String, nombre);
            db.AddInParameter(contactoCommand, "apellidoP", DbType.String, apellidoP);
            db.AddInParameter(contactoCommand, "apellidoM", DbType.String, apellidoM);
            db.AddInParameter(contactoCommand, "puesto", DbType.String, puesto);
            db.AddInParameter(contactoCommand, "telefono", DbType.String, telefono);
            db.AddInParameter(contactoCommand, "extension", DbType.String, extension);
            db.AddInParameter(contactoCommand, "primario", DbType.Boolean, primario);
            //db.AddInParameter(contactoCommand, "idTema", DbType.Int32, idTema);
            db.AddInParameter(contactoCommand, "bloqueado", DbType.Boolean, bloqueado);
            db.AddInParameter(contactoCommand, "idUsuarioModificacion", DbType.String, idUsuarioModificacion);

            sqlCommand = "Delete from Usuario_Perfil Where idUsuario = '" + idUsuario + "'";
            DbCommand deleteUsrPerfCommand = db.GetSqlStringCommand(sqlCommand);

            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();

                try
                {
                    db.ExecuteNonQuery(contactoCommand, transaction);
                    db.ExecuteNonQuery(deleteUsrPerfCommand, transaction);

                    //sqlCommand = "CESR_GuardaPerfil";
                    //char[] arrChar = { Convert.ToChar(","), Convert.ToChar(" ") };
                    //string[] arrPerfiles = perfiles.Split(arrChar);
                    //foreach (string idPerfil in arrPerfiles)
                    //{
                    //    if (idPerfil != "")
                    //    {
                    //        DbCommand perfilCommand = db.GetStoredProcCommand(sqlCommand);
                    //        db.AddInParameter(perfilCommand, "idUsuario", DbType.String, idUsuario);
                    //        db.AddInParameter(perfilCommand, "idPerfil", DbType.Int32, idPerfil);
                    //        db.ExecuteNonQuery(perfilCommand, transaction);
                    //    }
                    //}
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

        public bool Guarda(string idUsuario, byte[] pwd, string nombre, string apellidoP,
                                   string apellidoM, string puesto, string telefono,
                                   string extension, bool primario, bool bloqueado)
        {
            bool result = false;

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "CESR_GuardarContactoSinEmpresa";
            DbCommand contactoCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(contactoCommand, "idUsuario", DbType.String, idUsuario);
            db.AddInParameter(contactoCommand, "pwd", DbType.Binary, pwd);
            db.AddInParameter(contactoCommand, "nombre", DbType.String, nombre);
            db.AddInParameter(contactoCommand, "apellidoP", DbType.String, apellidoP);
            db.AddInParameter(contactoCommand, "apellidoM", DbType.String, apellidoM);
            db.AddInParameter(contactoCommand, "puesto", DbType.String, puesto);
            db.AddInParameter(contactoCommand, "telefono", DbType.String, telefono);
            db.AddInParameter(contactoCommand, "extension", DbType.String, extension);
            db.AddInParameter(contactoCommand, "primario", DbType.Boolean, primario);
            //db.AddInParameter(contactoCommand, "idTema", DbType.Int32, idTema);
            db.AddInParameter(contactoCommand, "bloqueado", DbType.Boolean, bloqueado);

            sqlCommand = "Delete from Usuario_Perfil Where idUsuario = '" + idUsuario + "'";
            DbCommand deleteUsrPerfCommand = db.GetSqlStringCommand(sqlCommand);

            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();

                try
                {
                    db.ExecuteNonQuery(contactoCommand, transaction);
                    db.ExecuteNonQuery(deleteUsrPerfCommand, transaction);
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

        public bool GuardaPerfil(string idUsuario, int idPerfil)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase();

                string sqlCommand = "CESR_GuardaPerfil";
                DbCommand contactoCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(contactoCommand, "idUsuario", DbType.String, idUsuario);
                db.AddInParameter(contactoCommand, "idPerfil", DbType.Int32, idPerfil);

                db.ExecuteNonQuery(contactoCommand);

                result = true;
            }
            catch 
            {
                result = false;
            }

            return result;

        }

        public bool EliminaTemas(int idEmpresa, string idUsuario)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase();

                string sqlCommand = "CESR_EliminaUsuarioTemas";
                DbCommand contactoCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(contactoCommand, "idEmpresa", DbType.String, idEmpresa);
                db.AddInParameter(contactoCommand, "idUsuario", DbType.String, idUsuario);

                db.ExecuteNonQuery(contactoCommand);

                result = true;
            }
            catch 
            {
                result = false;
            }

            return result;
        }

        public bool GuardaTema(int idEmpresa, string idUsuario, int idTema)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase();

                string sqlCommand = "CESR_GuardaUsuarioTema";
                DbCommand contactoCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(contactoCommand, "idEmpresa", DbType.String, idEmpresa);
                db.AddInParameter(contactoCommand, "idUsuario", DbType.String, idUsuario);
                db.AddInParameter(contactoCommand, "idTema", DbType.Int32, idTema);

                db.ExecuteNonQuery(contactoCommand);

                result = true;
            }
            catch 
            {
                result = false;
            }

            return result;
        }

        public bool GuardaEmpresa(int idEmpresa, string idUsuario)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase();

                string sqlCommand = "CESR_GuardaUsuarioEmpresa";
                DbCommand contactoCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(contactoCommand, "idEmpresa", DbType.Int32, idEmpresa);
                db.AddInParameter(contactoCommand, "idUsuario", DbType.String, idUsuario);

                db.ExecuteNonQuery(contactoCommand);

                result = true;
            }
            catch
            {
                result = false;
            }

            return result;
        }

        public bool GuardaTemas(int idEmpresa, string idUsuario)
        {
            bool result = false;
            try
            {
                Database db = DatabaseFactory.CreateDatabase();

                string sqlCommand = "CESR_GuardaUsuarioEmpresaTemas";
                DbCommand contactoCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(contactoCommand, "idEmpresa", DbType.Int32, idEmpresa);
                db.AddInParameter(contactoCommand, "idUsuario", DbType.String, idUsuario);

                db.ExecuteNonQuery(contactoCommand);

                result = true;
            }
            catch
            {
                result = false;
            }

            return result;
        }
    }
}
