using System;
using System.Configuration;
using System.IO;
using ESR.Business;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using System.Collections.Generic;

namespace ESR
{
    public partial class documentLibrary : System.Web.UI.Page
    {
        protected int GetIdEmpresa()
        {
            try
            {
                if (Request.Params["idEmpresa"] == null)
                    return Convert.ToInt32(Session["idEmpresa"].ToString());
                else
                    return Convert.ToInt32(Request.Params["idEmpresa"].ToString());
            }
            catch (Exception ex)
            {
                using (StreamWriter sw = File.AppendText(ConfigurationManager.AppSettings["logFile"].ToString()))
                {
                    sw.AutoFlush = true;
                    sw.WriteLine("Fecha: " + DateTime.Now.ToString());
                    sw.WriteLine("Empresa: " + Session["idEmpresa"].ToString());
                    sw.WriteLine("Error en visorDelCuestionario.GetIdEmpresa(): " + ex.Message);
                    sw.WriteLine("InnerException: " + ex.InnerException);
                    sw.Close();
                    return -1;
                }
            }
        }

        protected string GetIdUsuario()
        {
            if (Request.Params["idUsuario"] == null)
                return Session["idUsuario"].ToString();
            else
                return Request.Params["idUsuario"].ToString();
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void tvwEvidencias_TreeNodePopulate(object sender, System.Web.UI.WebControls.TreeNodeEventArgs e)
        {
            Populate(e.Node);
        }

        protected void Populate(TreeNode node)
        {
            SPFolder myLibrary;
            SPFileCollection myFiles;
            SPFolderCollection myFolders;

            //StreamWriter sw = File.AppendText(ConfigurationManager.AppSettings["logSharepoint"].ToString());
            //sw.WriteLine("Execute PopulateCategories(TreeNode node: " + node.Value + ")");
            try
            {
                using (SPSite oSite = new SPSite("https://esrv1.cemefi.org/"))
                {
                    using (SPWeb oWeb = oSite.OpenWeb(GetIdEmpresa().ToString()))
                    {
                        myLibrary = oWeb.GetFolder(node.ValuePath);
                        myFolders = myLibrary.SubFolders;
                        myFiles = myLibrary.Files;

                        //sw.WriteLine("myFolders.Count: " + myFolders.Count);
                        //sw.WriteLine("myFiles.Count: " + myFiles.Count);

                        if (myFolders.Count > 0)
                        {
                            List<SPFolder> spFolders = new List<SPFolder>();
                            foreach (SPFolder myFolder in myFolders)
                            {
                                if (myFolder.Name != "Forms")
                                {
                                    spFolders.Add(myFolder);
                                }
                            }
                            spFolders.Sort(delegate (SPFolder p1, SPFolder p2) { return p1.Name.CompareTo(p2.Name); });
                            foreach (SPFolder spfFolder in spFolders)
                            {
                                //sw.WriteLine("Agrega folder al nodo: " + spfFolder.Name);
                                TreeNode NewNode = new TreeNode(spfFolder.Name,
                                    spfFolder.Name);
                                NewNode.PopulateOnDemand = true;
                                NewNode.SelectAction = TreeNodeSelectAction.Expand;
                                NewNode.Expanded = false;
                                node.ChildNodes.Add(NewNode);
                                
                            }
                        }
                        //sw.WriteLine("myFiles.Count: " + myFiles.Count);
                        if (myFiles.Count > 0)
                        {
                            List<SPFile> spFiles = new List<SPFile>();
                            foreach (SPFile myFile in myFiles)
                            {
                                //sw.WriteLine("File: " + myFile.Name);
                                spFiles.Add(myFile);
                            }
                            spFiles.Sort(delegate (SPFile p1, SPFile p2) { return p1.Name.CompareTo(p2.Name); });
                            foreach (SPFile spfFile in spFiles)
                            {
                                //sw.WriteLine("Agrega evidencia al nodo: " + spfFile.Name);
                                TreeNode NewNode = new TreeNode(spfFile.Name, spfFile.Url);
                                NewNode.PopulateOnDemand = false;
                                NewNode.SelectAction = TreeNodeSelectAction.None;
                                NewNode.ShowCheckBox = true;
                                node.ChildNodes.Add(NewNode);
                            }
                        }

                    }
                }
                //sw.Close();
            }
            catch (Exception ex){
                StreamWriter sw = File.AppendText(ConfigurationManager.AppSettings["log"].ToString());
                sw.WriteLine("Fecha: " + DateTime.Now.ToString());
                sw.WriteLine("Empresa: " + Session["idEmpresa"].ToString());
                sw.WriteLine("Error en PopulateCategories(TreeNode node): " + ex.Message);
                sw.WriteLine("InnerException: " + ex.InnerException);
                sw.Close();
            }
        }

        //void PopulateEvidences(TreeNode node)
        //{
        //    SPFolder myLibrary;
        //    SPFileCollection myFiles;
        //    StreamWriter sw = File.AppendText(ConfigurationManager.AppSettings["logSharepoint"].ToString());
        //    sw.WriteLine("Execute PopulateEvidences(TreeNode node)");
        //    using (SPSite oSite = new SPSite("https://esrv1.cemefi.org/"))
        //    {
        //        using (SPWeb oWeb = oSite.OpenWeb(GetIdEmpresa().ToString()))
        //        {
        //            //Checar si el folder ya existe,
        //            SPFolderCollection myFolders = oWeb.Folders;
        //            myLibrary = oWeb.Folders["Documentos compartidos"];
        //            myFiles = myLibrary.SubFolders[node.Value].Files;
        //            List<SPFile> spFiles = new List<SPFile>();
        //            foreach (SPFile myFile in myFiles)
        //            {
        //                sw.WriteLine("File: " + myFile.Name);
        //                spFiles.Add(myFile);
        //            }
        //            spFiles.Sort(delegate(SPFile p1, SPFile p2) { return p1.Name.CompareTo(p2.Name); });
        //            foreach (SPFile spfFile in spFiles)
        //            {
        //                sw.WriteLine("Agrega evidencia al nodo: " + spfFile.Name);
        //                TreeNode NewNode = new TreeNode(spfFile.Name, spfFile.Url);
        //                NewNode.PopulateOnDemand = false;
        //                NewNode.SelectAction = TreeNodeSelectAction.None;
        //                NewNode.ShowCheckBox = true;
        //                node.ChildNodes.Add(NewNode);
        //            }
        //        }
        //    }
        //    sw.Close();
        //}

        protected void TraverseTree(TreeNodeCollection nodes)
        {
            bool result = false;
            foreach (TreeNode child in nodes)
            {
                if (child.Checked)
                {
                    //sw.WriteLine("Seleccionado");

                    Indicador respuestaIndicador = new Indicador();
                    respuestaIndicador.idIndicador = Convert.ToInt32(Request.QueryString["idIndicador"]);
                    respuestaIndicador.idTema = Convert.ToInt32(Request.QueryString["idTema"]);
                    respuestaIndicador.idTipoEvidencia = Convert.ToInt32(Request.QueryString["idTipoEvidencia"]);

                    // MRA 27/10/2010
                    // Esta linea esta rara
                    // ¿Que pasa si sube un documento una empresa que no es la que se esta consultando?

                    respuestaIndicador.idEmpresa = Convert.ToInt32(Session["idEmpresa"].ToString());
                    respuestaIndicador.idCuestionario = Convert.ToInt32(Request.QueryString["idCuestionario"]);
                    //sw.WriteLine("fileName: " + child.Text);
                    respuestaIndicador.fileName = child.Text;
                    //sw.WriteLine("Url: " + child.Value);
                    respuestaIndicador.Url = child.Value;
                    respuestaIndicador.idUsuario = Session["idUsuario"].ToString();
                    respuestaIndicador.idTipoRespuesta = Convert.ToInt32(Request.QueryString["idTipoRespuesta"]);
                    //sw.WriteLine("Guardar evidencia antes: " + result);
                    result = respuestaIndicador.GuardarEvidencia();
                    if (!result)
                        throw new Exception("Error al guardar la referencia a la evidencia en TraverseTree(TreeNodeCollection nodes)");
                }
                TraverseTree(child.ChildNodes);
            }
        }

        protected void btnAsignarDocumentos_Click(object sender, EventArgs e)
        {
            try
            {
                TraverseTree(tvwEvidencias.Nodes);
                lblMensaje.Text = "Las referencias fueron almacenadas correctamente. <input type=\"button\" onclick=\"opener.location.reload(true);self.close();\" value=\"Cerrar\" name=\"btnCerrar\">";
            }
            catch (Exception ex)
            {
                StreamWriter sw = File.AppendText(ConfigurationManager.AppSettings["logSharepoint"].ToString());
                sw.AutoFlush = true;
                sw.WriteLine("Fecha: " + DateTime.Now.ToString());
                sw.WriteLine("Empresa: " + Session["idEmpresa"].ToString());
                sw.WriteLine("idIndicador: " + Request.QueryString["idIndicador"]);
                sw.WriteLine("idTema: " + Request.QueryString["idTema"]);
                sw.WriteLine("idCuestionario: " + Request.QueryString["idCuestionario"]);
                sw.WriteLine("Error en visorDelCuestionario.Page_Load(): " + ex.Message);
                sw.WriteLine("InnerException: " + ex.InnerException);
                sw.Close();
                lblMensaje.Text = "Hubo un error al subir las referencias, intente de nuevo. <input type=\"button\" onclick=\"opener.location.reload(true);self.close();\" value=\"Cerrar\" name=\"btnCerrar\">";
            }
        }
    }
}