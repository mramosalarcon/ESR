using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.IO;
using System.Configuration;

namespace ESR.Data
{
    public class Menu
    {
        public DataSet Carga(string perfiles, string idMenuPadre)
        {
            DataSet dsMenu = new DataSet();
            if (perfiles != "0")
            {
                Database db = DatabaseFactory.CreateDatabase();
                string sqlCommand;

                if (perfiles.Contains("2"))
                {
                    sqlCommand = "Select Distinct nombre, url, target, idTipo, Menu.idMenu, idMenuPadre, habilitado, " +
                                    "Ordinal From Menu Right outer join Menu_Perfil	On Menu.idMenu = Menu_Perfil.idMenu " +
                                    "Where idTipo = 2 And Menu.idMenu in (" + idMenuPadre + ") And Menu_Perfil.idPerfil in (" + perfiles + ") " +
                                    "Order by Ordinal;" +
                                    "Select Distinct nombre, url, target, idTipo, Menu.idMenu, idMenuPadre, habilitado, " +
                                    "Ordinal From Menu Right outer join Menu_Perfil	On Menu.idMenu = Menu_Perfil.idMenu " +
                                    "Where idTipo = 2 And idMenuPadre in (" + idMenuPadre + ") And Menu_Perfil.idPerfil in (" + perfiles + ") " +
                                    "Order by Ordinal ";

                }
                else
                {
                    sqlCommand = "Select Distinct nombre, url, target, idTipo, Menu.idMenu, idMenuPadre, habilitado, " +
                                    "Ordinal From Menu Right outer join Menu_Perfil	On Menu.idMenu = Menu_Perfil.idMenu " +
                                    "Where idTipo = 2 And Menu.idMenu in (" + idMenuPadre + ") And Menu_Perfil.idPerfil in (" + perfiles + ") " +
                                    "And habilitado = 1 Order by Ordinal;" +
                                    "Select Distinct nombre, url, target, idTipo, Menu.idMenu, idMenuPadre, habilitado, " +
                                    "Ordinal From Menu Right outer join Menu_Perfil	On Menu.idMenu = Menu_Perfil.idMenu " +
                                    "Where idTipo = 2 And idMenuPadre in (" + idMenuPadre + ") And Menu_Perfil.idPerfil in (" + perfiles + ") " +
                                    "And habilitado = 1 Order by Ordinal ";
                }

                try
                {
                    DbCommand dbCommand = db.GetSqlStringCommand(sqlCommand);
                    db.LoadDataSet(dbCommand, dsMenu, new string[] { "Menu", "SubMenu" });


                    dsMenu.Relations.Add("Children",
                    dsMenu.Tables["Menu"].Columns["idMenu"],
                    dsMenu.Tables["SubMenu"].Columns["idMenuPadre"]);
                }
                catch (Exception ex)
                {
                    using (StreamWriter sw = File.AppendText(ConfigurationManager.AppSettings["logFile"].ToString()))
                    {
                        sw.AutoFlush = true;
                        sw.WriteLine("Fecha: " + DateTime.Now.ToString());
                        sw.WriteLine("Error en ESR.Data.Menu.Carga(perfiles: " + perfiles + ", idMenuPadre: " + idMenuPadre + "): " + ex.Message);
                        sw.Close();
                    }
                }
            }
            return dsMenu;
        }

        public DataSet Carga(string perfiles)
        {
            Database db = DatabaseFactory.CreateDatabase();
            string sqlCommand = "CESR_CargaMenu";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "perfiles", DbType.String, perfiles);

            DataSet dsMenu = null;
            dsMenu = db.ExecuteDataSet(dbCommand);

            dsMenu.Tables[0].TableName = "Menu";
            dsMenu.Tables[1].TableName = "SubMenu";

            dsMenu.Relations.Add("Children",
                dsMenu.Tables["Menu"].Columns["idMenu"],
                dsMenu.Tables["SubMenu"].Columns["idMenuPadre"]);
            //if (DateTime.Now <= Convert.ToDateTime("10/03/2008"))
            {
                return dsMenu;
            }
            //else
            //{
            //    return null;
            //}
        }

        public DataSet CargaTop()
        {
            Database db = DatabaseFactory.CreateDatabase();
            string sqlCommand = "CESR_CargaMenuTop";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            DataSet dsMenu = null;
            dsMenu = db.ExecuteDataSet(dbCommand);

            dsMenu.Tables[0].TableName = "Menu";

            return dsMenu;
        }
    }
}
